using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Parsers;

namespace Pt5Viewer.Models
{
    public class Pt5Model
    {
        public double TimeScaleMax => parser != null ?
                                    parser.TimeScaleMax : 60_000;    // s

        public DateTime CaptureDate => parser != null?
                                    parser.CaptureDate : DateTime.Now;

        public ulong SampleCount => parser.SampleCount;

        public float AverageCurrent => parser.AverageCurrent;

        private Pt5Parser parser = null;

        public bool IsStarted => parser != null;

        public double GetX(long index)
        {
            return parser.GetTimestampFromIndex(index);
        }

        public double GetY(long index)
        {
            return parser.GetCurrentFromIndex(index);
        }

        public long GetIndexFromTimestamp(double timestamp)
        {
            return parser.GetIndexFromTimestamp(timestamp);
        }

        public bool SetParser(string pt5FilePath)
        {
            bool isSuccess = Pt5Parser.TryParse(pt5FilePath, out parser);
            if (isSuccess == true)
            {

            }

            return isSuccess;
        }

        public void Clear()
        {
            parser = null;
        }

        public override string ToString()
        {
            if (parser == null)
            {
                return "NULL";
            }
            else
            {
                return parser.ToString();
            }
        }
    }
}