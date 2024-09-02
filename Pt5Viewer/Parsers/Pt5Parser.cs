using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;

namespace Pt5Viewer.Parsers
{
    public class Pt5Parser : IDisposable
    {
        #region Constants
        // Fixed file offsets
        private const long headerOffset = 0;
        private const long statusOffset = 272;
        private const long sampleOffset = 1024;

        // Bitmasks
        private const short coarseMask = 1;
        private const ushort marker0Mask = 1;
        private const ushort marker1Mask = 2;
        private const ushort markerMask = marker0Mask | marker1Mask;

        // Missing data indicators
        private const short missingRawCurrent = unchecked((short)0x8001);
        private const ushort missingRawVoltage = unchecked((ushort)(~0));
        #endregion

        #region Structs and enums for file header
        private enum CalibrationStatus : int
        {
            OK, Failed
        }

        private enum VoutSetting : int
        {
            Typical, Low, High, Custom
        }

        [FlagsAttribute]  // bitwise-maskable
        private enum SelectField : int
        {
            None = 0x00,
            Avg = 0x01,
            Min = 0x02,
            Max = 0x04,
            Main = 0x08,
            Usb = 0x10,
            Aux = 0x20,
            Marker = 0x40,
            All = Avg | Min | Max
        }

        private enum RunMode : int
        {
            NoGUI, GUI
        }

        [FlagsAttribute] // bitwise-maskable
        private enum CaptureMask : ushort
        {
            chanMain = 0x1000,
            chanUsb = 0x2000,
            chanAux = 0x4000,
            chanMarker = 0x8000,
            chanMask = 0xf000,
        }

        // File header structure
        private struct Pt5Header
        {
            public int headSize;
            public string name;
            public int batterySize;
            public DateTime captureDate;
            public string serialNumber;
            public CalibrationStatus calibrationStatus;
            public VoutSetting voutSetting;
            public float voutValue;
            public int hardwareRate;
            public float softwareRate; // ignore
            public SelectField powerField;
            public SelectField currentField;
            public SelectField voltageField;
            public string captureSetting;
            public string swVersion;
            public RunMode runMode;
            public int exitCode;
            public long totalCount;
            public ushort statusOffset;
            public ushort statusSize;
            public ushort sampleOffset;
            public ushort sampleSize;
            public ushort initialMainVoltage;
            public ushort initialUsbVoltage;
            public ushort initialAuxVoltage;
            public CaptureMask captureDataMask;
            public ulong sampleCount;
            public ulong missingCount;
            public float avgMainVoltage;
            public float avgMainCurrent;
            public float avgMainPower;
            public float avgUsbVoltage;
            public float avgUsbCurrent;
            public float avgUsbPower;
            public float avgAuxVoltage;
            public float avgAuxCurrent;
            public float avgAuxPower;

            /// <summary>
            /// ToString display method
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string str = "Pt5Header:\n";
                str += "headsize: " + headSize + "\n";
                str += "name: " + name + "\n";
                str += "batterySize: " + batterySize + "\n";
                str += "captureDate: " + captureDate + "\n";
                str += "serialNumber: " + serialNumber + "\n";
                str += "calibrationStatus : " + calibrationStatus + "\n";
                str += "voutSetting: " + voutSetting + "\n";
                str += "voutValue: " + voutValue + "\n";
                str += "hardwareRate: " + hardwareRate + "\n";
                str += "powerField: " + powerField + "\n";
                str += "currentField: " + currentField + "\n";
                str += "voltageField: " + voltageField + "\n";
                str += "captureSetting: " + captureSetting + "\n";
                str += "swVersion: " + swVersion + "\n";
                str += "runMode: " + runMode + "\n";
                str += "exitCode: " + exitCode + "\n";
                str += "totalCount: " + totalCount + "\n";
                str += "statusOffset: " + statusOffset + "\n";
                str += "statusSize: " + statusSize + "\n";
                str += "initialMainVoltage: " + initialMainVoltage + "\n";
                str += "initialUsbVoltage: " + initialUsbVoltage + "\n";
                str += "initialAuxVoltage: " + initialAuxVoltage + "\n";
                str += "captureDataMask: 0x" + ((int)captureDataMask).ToString("x4") + "\n";
                str += "sampleCount: " + sampleCount + "\n";
                str += "missingCount: " + missingCount + "\n";
                str += "avgMainVoltage: " + avgMainVoltage + "\n";
                str += "avgMainCurrent: " + avgMainCurrent + "\n";
                str += "avgMainPower: " + avgMainPower + "\n";
                str += "avgUsbVoltage: " + avgUsbVoltage + "\n";
                str += "avgUsbCurrent: " + avgUsbCurrent + "\n";
                str += "avgUsbPower: " + avgUsbPower + "\n";
                str += "avgAuxVoltage: " + avgAuxVoltage + "\n";
                str += "avgAuxCurrent: " + avgAuxCurrent + "\n";
                str += "avgAuxPower: " + avgAuxPower + "\n";
                return str;
            }
        }
        #endregion

        #region Structs and enums for Status packet
        private enum PacketType : byte
        {
            set = 1, start, stop,
            status = 0x10, sample = 0x20
        }

        private struct Observation
        {
            public short mainCurrent;
            public short usbCurrent;
            public short auxCurrent;
            public ushort voltage;
        }

        [FlagsAttribute]
        private enum PmStatus : byte
        {
            unitNotAtVoltage = 0x01,
            cannotPowerOutput = 0x02,
            checksumError = 0x04,
            followsLastSamplePacket = 0x08,
            responseToStopPacket = 0x10,
            responseToResetCommand = 0x20,
            badPacketReceived = 0x40,
        }

        [FlagsAttribute]
        private enum Leds : byte
        {
            disableButtonPressed = 0x01,
            errorLedOn = 0x02,
            fanMotorOn = 0x04,
            voltageIsAux = 0x08,
        }

        private enum HardwareRev : byte
        {
            revA = 1, revB, revC, revD,
        }

        private enum UsbPassthroughMode : byte
        {
            off, on, auto, trigger, sync,
        }

        private enum EventCode : byte
        {
            noEvent = 0,
            usbConnectionLost,
            tooManyDroppedObservations,
            resetRequestedByHost,
        }

        // Status packet structure
        private struct StatusPacket
        {
            public byte length;                                     // packet length (not incl. this byte)
            public PacketType type;                                 // packet type (PacketType.status)
            public byte firmwareVersion;                            // firmware version
            public byte protocolVersion;                            // protocol version
            public Observation fineObs;                             // fine-scale measurement
            public Observation coarseObs;                           // coarse-scale measurement
            public byte mainVoltageSetting;                         // main voltage setting (volts = 2.0 + 0.01*mainVoltageSetting)
            public sbyte temperature;                               // signed degrees Celsius (-128...+127)
            public byte pmStatus;                                   // see bitmasks in pmStatus enum
            public byte reserved;                                   // reserved for future use
            public Leds leds;                                       // see masks in pmLeds enum
            public sbyte mainFineResistorOffset;                    // signed, ohms = 0.05 + 0.0001*offset
            public ushort serialNumber;                             // two-byte serial number (big endian)
            public byte sampleRate;                                 // sample rate (kHz)
            public ushort dacCalLow;                                // DAC calibration setting for 2.5V
            public ushort dacCalHigh;                               // DAC calibration setting for 4.096V
            public ushort powerupCurrentLimit;
            public ushort runtimeCurrentLimit;
            public byte powerupTime;                                // ms
            public sbyte usbFineResistorOffset;                     // signed, ohms = 0.05 + 0.0001*offset
            public sbyte auxFineResistorOffset;                     // signed, ohms = 0.10 + 0.0001*offset
            public ushort usbVoltage;                               // volts = 2 * usbVoltage * 62.5 / 1e6
            public ushort auxVoltage;                               // volts =     auxVoltage * 62.5 / 1e6 (*2 for Rev C boards)
            public HardwareRev hardwareRevision;                           // 1=A, 2=B., etc...
            public float temperatureLimit;                          // Signed Q7.8 format
            public UsbPassthroughMode usbPassthroughMode;           // 0=off, 1=on, 2=auto
            public sbyte mainCoarseResistorOffset;                  // signed, ohms = 0.05 + 0.0001*offset
            public sbyte usbCoarseResistorOffset;                   // signed, ohms = 0.05 + 0.0001*offset
            public sbyte auxCoarseResistorOffset;                   // signed, ohms = 0.10 + 0.0001*offset
            public sbyte factoryMainFineResistorOffset;             // signed, ohms = 0.05 + 0.0001*offset
            public sbyte factoryUsbFineResistorOffset;              // signed, ohms = 0.05 + 0.0001*offset
            public sbyte factoryAuxFineResistorOffset;              // signed, ohms = 0.10 + 0.0001*offset
            public sbyte factoryMainCoarseResistorOffset;           // signed, ohms = 0.05 + 0.0001*offset
            public sbyte factoryUsbCoarseResistorOffset;            // signed, ohms = 0.05 + 0.0001*offset
            public sbyte factoryAuxCoarseResistorOffset;            // signed, ohms = 0.10 + 0.0001*offset
            public EventCode eventCode;                                  // event code
            public ushort eventData;                                // event data


            //HVPM Protocol Fields
            public byte measurementDataset;                         // 00 = Main + USB, 01 = BNC + USB
            public int HVmainVoltageSetting;                        //Main Vout
            public UInt32 mainFineScale;                            //Current scaling by channel.
            public UInt32 mainCoarseScale;                          //Current scaling by channel.
            public UInt32 usbFineScale;                             //Current scaling by channel.
            public UInt32 usbCoarseScale;                           //Current scaling by channel.
            public UInt32 auxFineScale;                             //Current scaling by channel.
            public UInt32 auxCoarseScale;                           //Current scaling by channel.
            public short mainFineZeroOffset;                        //Zero-level offset for the PGA.
            public short mainCoarseZeroOffset;                      //Zero-level offset for the PGA.
            public short USBFineZeroOffset;                         //Zero-level offset for the PGA.
            public short USBCoarseZeroOffset;                       //Zero-level offset for the PGA.
            public byte hardwareModel;                              //0 = unknown, 1 = 5V, 2 = HV
            public byte checksum;                                   // checksum
        }
        #endregion

        #region Sample struct
        public struct Sample
        {
            public long sampleIndex;   // 0...N-1
            public double timeStamp;   // fractional seconds

            public bool mainPresent;   // whether Main was recorded
            public double mainCurrent; // current in milliamps
            public double mainVoltage; // volts

            public bool usbPresent;    // whether Usb was recorded
            public double usbCurrent;  // current in milliamps
            public double usbVoltage;  // volts

            public bool auxPresent;    // whether Aux was recorded
            public double auxCurrent;  // current in milliamps
            public double auxVoltage;  // volts;

            public bool markerPresent; // whether markers/voltages
                                       //      were recorded
            public bool marker0;       // Marker 0
            public bool marker1;       // Marker 1
            public bool missing;       // true if this sample was
                                       //      missing
        }
        #endregion

        #region Read file header
        private void ReadHeader(BinaryReader reader,
                                      ref Pt5Header header)
        {
            // Remember original position
            long oldPos = reader.BaseStream.Position;

            // Move to start of file
            reader.BaseStream.Position = 0;

            // Read file header
            header.headSize = reader.ReadInt32();
            header.name = reader.ReadString().Trim();
            header.batterySize = reader.ReadInt32();
            header.captureDate =
                   DateTime.FromBinary(reader.ReadInt64());
            header.serialNumber = reader.ReadString().Trim();
            header.calibrationStatus =
                   (CalibrationStatus)reader.ReadInt32();
            header.voutSetting = (VoutSetting)reader.ReadInt32();
            header.voutValue = reader.ReadSingle();
            header.hardwareRate = reader.ReadInt32();
            header.softwareRate = (float)header.hardwareRate;
            reader.ReadSingle(); // ignore software rate
            header.powerField = (SelectField)reader.ReadInt32();
            header.currentField = (SelectField)reader.ReadInt32();
            header.voltageField = (SelectField)reader.ReadInt32();
            header.captureSetting = reader.ReadString().Trim();
            header.swVersion = reader.ReadString().Trim();
            header.runMode = (RunMode)reader.ReadInt32();
            header.exitCode = reader.ReadInt32();
            header.totalCount = reader.ReadInt64();
            header.statusOffset = reader.ReadUInt16();
            header.statusSize = reader.ReadUInt16();
            header.sampleOffset = reader.ReadUInt16();
            header.sampleSize = reader.ReadUInt16();
            header.initialMainVoltage = reader.ReadUInt16();
            header.initialUsbVoltage = reader.ReadUInt16();
            header.initialAuxVoltage = reader.ReadUInt16();
            header.captureDataMask = (CaptureMask)reader.ReadUInt16();
            header.sampleCount = reader.ReadUInt64();
            header.missingCount = reader.ReadUInt64();

            // Convert sums to averages
            ulong count = Math.Max(1, header.sampleCount -
                                      header.missingCount);
            header.avgMainVoltage = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgMainCurrent = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgMainPower = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgUsbVoltage = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgUsbCurrent = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgUsbPower = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgAuxVoltage = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgAuxCurrent = count > 0 ? reader.ReadSingle() / count : 0;
            header.avgAuxPower = count > 0 ? reader.ReadSingle() / count : 0;

            // Restore original position
            reader.BaseStream.Position = oldPos;
        }
        #endregion

        #region Read status packet
        private void ReadStatusPacket(BinaryReader reader,
                                            ref StatusPacket status)
        {
            // Remember original position
            long oldPos = reader.BaseStream.Position;

            // Move to start of status packet
            reader.BaseStream.Position = statusOffset;

            // Read status packet
            status.length = reader.ReadByte();
            status.type = (PacketType)reader.ReadByte();
            Debug.Assert(status.type == PacketType.status);
            status.firmwareVersion = reader.ReadByte();
            status.protocolVersion = reader.ReadByte();
            status.fineObs.mainCurrent = reader.ReadInt16();
            status.fineObs.usbCurrent = reader.ReadInt16();
            status.fineObs.auxCurrent = reader.ReadInt16();
            status.fineObs.voltage = reader.ReadUInt16();
            status.coarseObs.mainCurrent = reader.ReadInt16();
            status.coarseObs.usbCurrent = reader.ReadInt16();
            status.coarseObs.auxCurrent = reader.ReadInt16();
            status.coarseObs.voltage = reader.ReadUInt16();
            status.mainVoltageSetting = reader.ReadByte();
            status.temperature = reader.ReadSByte();
            status.pmStatus = reader.ReadByte();
            status.reserved = reader.ReadByte();
            status.leds = (Leds)reader.ReadByte();
            status.mainFineResistorOffset = reader.ReadSByte();
            status.serialNumber = reader.ReadUInt16();
            status.sampleRate = reader.ReadByte();
            status.dacCalLow = reader.ReadUInt16();
            status.dacCalHigh = reader.ReadUInt16();
            status.powerupCurrentLimit = reader.ReadUInt16();
            status.runtimeCurrentLimit = reader.ReadUInt16();
            status.powerupTime = reader.ReadByte();
            status.usbFineResistorOffset = reader.ReadSByte();
            status.auxFineResistorOffset = reader.ReadSByte();
            status.usbVoltage = reader.ReadUInt16();
            status.usbVoltage = reader.ReadUInt16();
            status.hardwareRevision = (HardwareRev)reader.ReadByte();
            status.temperatureLimit = reader.ReadByte();
            status.usbPassthroughMode =
                       (UsbPassthroughMode)reader.ReadByte();
            status.mainCoarseResistorOffset = reader.ReadSByte();
            status.usbCoarseResistorOffset = reader.ReadSByte();
            status.auxCoarseResistorOffset = reader.ReadSByte();
            status.factoryMainFineResistorOffset = reader.ReadSByte();
            status.factoryUsbFineResistorOffset = reader.ReadSByte();
            status.factoryAuxFineResistorOffset = reader.ReadSByte();
            status.factoryMainCoarseResistorOffset =
                       reader.ReadSByte();
            status.factoryUsbCoarseResistorOffset =
                       reader.ReadSByte();
            status.factoryAuxCoarseResistorOffset =
                       reader.ReadSByte();
            status.eventCode = (EventCode)reader.ReadByte();
            status.eventData = reader.ReadUInt16();

            status.measurementDataset = reader.ReadByte();
            status.HVmainVoltageSetting = reader.ReadInt16();
            status.mainFineScale = reader.ReadUInt32();
            status.mainCoarseScale = reader.ReadUInt32();
            status.usbFineScale = reader.ReadUInt32();
            status.usbCoarseScale = reader.ReadUInt32();
            status.auxFineScale = reader.ReadUInt32();
            status.auxCoarseScale = reader.ReadUInt32();
            status.mainFineZeroOffset = reader.ReadInt16();
            status.mainCoarseZeroOffset = reader.ReadInt16();
            status.USBFineZeroOffset = reader.ReadInt16();
            status.USBCoarseZeroOffset = reader.ReadInt16();
            status.hardwareModel = reader.ReadByte();
            status.checksum = reader.ReadByte();

            // Restore original position
            reader.BaseStream.Position = oldPos;
        }
        #endregion

        #region Utility functions
        private long BytesPerSample(CaptureMask captureDataMask)
        {
            long result = sizeof(ushort); // voltage is always present

            // Add lengths for optional current channels
            if ((captureDataMask & CaptureMask.chanMain) != 0)
                result += sizeof(UInt32);

            if ((captureDataMask & CaptureMask.chanUsb) != 0)
                result += sizeof(UInt32);

            if ((captureDataMask & CaptureMask.chanAux) != 0)
                result += sizeof(UInt32);

            return result;
        }

        private long SamplePosition(long sampleIndex,
                                          CaptureMask captureDataMask)
        {
            long result = sampleOffset +
                BytesPerSample(captureDataMask) * sampleIndex;

            return result;
        }

        private long SamplePosition(double seconds,
                                   CaptureMask captureDataMask,
                                   ref StatusPacket statusPacket)
        {
            seconds = Math.Max(0, seconds);

            long bytesPerSample = BytesPerSample(captureDataMask);
            long freq = 1000 * statusPacket.sampleRate;

            long result = (long)(seconds * freq * bytesPerSample);
            long err = result % bytesPerSample;
            if (err > 0)   // must fall on boundary
                result += (bytesPerSample - err);
            result += sampleOffset;

            return result;
        }

        private long _SampleCount(BinaryReader reader,
                                       CaptureMask captureDataMask)
        {
            return (reader.BaseStream.Length - sampleOffset)
                   / BytesPerSample(captureDataMask);
        }
        #endregion


        #region Retrieve a sample from the file
        private void GetSample(long sampleIndex,
                              CaptureMask captureDataMask,
                              StatusPacket statusPacket,
                              BinaryReader reader,
                              ref Sample sample)
        {
            int intToFloatConversion = 1048576;
            // Remember the index and time
            sample.sampleIndex = sampleIndex;
            sample.timeStamp = sampleIndex
                               / (1000.0 * statusPacket.sampleRate);

            // Intial settings for all flags
            sample.mainPresent =
                  (captureDataMask & CaptureMask.chanMain) != 0;
            sample.usbPresent =
                  (captureDataMask & CaptureMask.chanUsb) != 0;
            sample.auxPresent =
                  (captureDataMask & CaptureMask.chanAux) != 0;
            sample.markerPresent = true;
            sample.missing = false;

            // Abort if no data was selected
            long bytesPerSample = BytesPerSample(captureDataMask);
            if (bytesPerSample == 0)
                return;

            // Remember original position
            long oldPos = reader.BaseStream.Position;

            // Position the file to the start of the desired sample (if necessary)
            long newPos = SamplePosition(sampleIndex, captureDataMask);
            if (oldPos != newPos)
                reader.BaseStream.Position = newPos;

            // Get default voltages (V) for the three channels
            sample.mainVoltage =
                statusPacket.HVmainVoltageSetting / intToFloatConversion;

            sample.usbVoltage =
                (double)statusPacket.usbVoltage * 125 / 1e6f;
            if (statusPacket.hardwareRevision < HardwareRev.revB)
                sample.usbVoltage /= 2;

            sample.auxVoltage =
                (double)statusPacket.auxVoltage * 125 / 1e6f;
            if (statusPacket.hardwareRevision < HardwareRev.revC)
                sample.auxVoltage /= 2;

            // Main current (mA)
            if (sample.mainPresent)
            {
                Int32 raw = reader.ReadInt32();

                sample.missing = sample.missing ||
                                 raw == missingRawCurrent;
                if (!sample.missing)
                {
                    sample.mainCurrent = raw / 1000f;   // uA -> mA
                }
            }

            // USB current (mA)
            if (sample.usbPresent)
            {
                Int32 raw = reader.ReadInt32();

                sample.missing = sample.missing ||
                                 raw == missingRawCurrent;
                if (!sample.missing)
                {
                    sample.usbCurrent = raw / 1000f;   // uA -> mA
                }
            }

            // Aux current (mA)
            if (sample.auxPresent)
            {
                Int32 raw = reader.ReadInt32();

                sample.missing = sample.missing ||
                                 raw == missingRawCurrent;
                if (!sample.missing)
                {
                    sample.auxCurrent = raw / 1000f;   // uA -> mA

                }
            }

            // Markers and Voltage (V)
            {
                ushort uraw = reader.ReadUInt16();

                sample.missing = sample.missing ||
                                 uraw == missingRawVoltage;
                if (!sample.missing)
                {
                    // Strip out marker bits
                    sample.marker0 = (uraw & marker0Mask) != 0;
                    sample.marker1 = (uraw & marker1Mask) != 0;
                    uraw &= unchecked((ushort)~markerMask);

                    // Calculate voltage
                    double voltage = (double)uraw * 125 / 1e6f * 4;

                    // Assign the high-res voltage, as appropriate
                    if ((statusPacket.leds & Leds.voltageIsAux) != 0)
                    {
                        sample.auxVoltage = voltage;
                        if (statusPacket.hardwareRevision
                               < HardwareRev.revC)
                        {
                            sample.auxVoltage /= 2;
                        }
                    }
                    else
                    {
                        sample.mainVoltage = voltage;
                        if (statusPacket.hardwareRevision
                               < HardwareRev.revB)
                        {
                            sample.mainVoltage /= 2;
                        }
                    }
                }
            }

            // Restore original position, if we moved it earlier
            if (oldPos != newPos)
                reader.BaseStream.Position = oldPos;
        }
        #endregion

        private string filePath;
        private Pt5Header header;
        private StatusPacket statusPacket;

        private FileStream pt5Stream;
        private BinaryReader pt5Reader;

        private Sample sample;

        private bool disposed = false;

        private Pt5Parser(string pt5FilePath)
        {
            filePath = pt5FilePath;

            pt5Stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            pt5Reader = new BinaryReader(pt5Stream);

            // Read the file header
            header = new Pt5Header();
            ReadHeader(pt5Reader, ref header);
#if DEBUG
            Console.WriteLine("header : " + header.ToString());
#endif
            // Read the Status Packet
            statusPacket = new StatusPacket();
            ReadStatusPacket(pt5Reader, ref statusPacket);


            // Determine the number of samples in the file
            long sampleCount = _SampleCount(pt5Reader, header.captureDataMask);
#if DEBUG
            Console.WriteLine("Sample count :" + sampleCount);
#endif
            sample = new Sample();
            sample.sampleIndex = -1;

            //// Pre-position input file to the beginning of the sample
            //// data (saves a lot of repositioning in the GetSample
            //// routine)
            //pt5Reader.BaseStream.Position = sampleOffset;

            //long missingCount = 0;

            //// Process the samples sequentially, beginning to end
            //Sample sample = new Sample();
            //for (long sampleIndex = 0;
            //     sampleIndex < sampleCount;
            //     sampleIndex++)
            //{
            //    // Read the next sample
            //    GetSample(sampleIndex, header.captureDataMask,
            //                  statusPacket, pt5Reader, ref sample);

            //    // Process the sample
            //    Console.WriteLine("#{0} {1} sec MAIN: {2} mA {3} V USB: {4} mA {5} V",
            //                      sampleIndex,
            //                      sample.timeStamp,
            //                      sample.mainCurrent,
            //                      sample.mainVoltage,
            //                      sample.usbCurrent,
            //                      sample.usbVoltage);

            //    if (sample.missing)
            //        missingCount++;
            //}

        }

        public DateTime CaptureDate => header.captureDate;

        public double TimeScaleMax => header.sampleCount / (1000.0 * statusPacket.sampleRate);

        public ulong SampleCount => header.sampleCount;

        public float AverageCurrent => header.avgMainCurrent;

        public long GetIndexFromTimestamp(double timestamp)
        {
            return (long)Math.Round(1000.0 * statusPacket.sampleRate * timestamp, 5);
        }

        public double GetTimestampFromIndex(long index)
        {
            return index / (1000.0 * statusPacket.sampleRate);
        }

        public double GetCurrentFromIndex(long index)
        {
            if (sample.sampleIndex != index)
            {
                GetSample(index, header.captureDataMask, statusPacket, pt5Reader, ref sample);
            }

            if (sample.missing == true) return Constant.Missing;
            return sample.mainCurrent;
        }

        public override string ToString()
        {
            return header.ToString();
        }

        public static bool TryParse(string pt5FilePath, out Pt5Parser parser)
        {
            if (File.Exists(pt5FilePath) == false)
            {
                parser = null;
                return false;
            }

            //if this is a pt3 or pt4 file, jump ship here. the rest of the format is different
            //technically the header format is a bit different too.. but we can get enough
            //out of it for testing purposes.
            if (pt5FilePath.EndsWith(".pt5") == false)
            {
                parser = null;
                return false;
            }

            parser = new Pt5Parser(pt5FilePath);
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed == false)
            {
                if (disposing == true)
                {
                    // 관리되는 리소스 해제
                    if (pt5Reader != null)
                    {
                        pt5Reader.Dispose();
                        pt5Reader = null;
                    }

                    if (pt5Stream != null)
                    {
                        pt5Stream.Dispose();
                        pt5Stream = null;
                    }
                }

                // 관리되지 않는 리소스 해제는 여기에 추가 (필요시)

                disposed = true;
            }
        }

        ~Pt5Parser()
        {
            Dispose(false);
        }
    }
}