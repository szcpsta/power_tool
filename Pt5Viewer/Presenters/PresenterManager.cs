using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Models;

namespace Pt5Viewer.Presenters
{
    public class PresenterManager
    {
        private Pt5Model model = new Pt5Model();

        private List<Presenter> presenters = new List<Presenter>();

        private List<IScaleSync> scaleSyncPresenters = new List<IScaleSync>();

        public TimeUnitEnum TimeUnit { get; private set; }
        public TimeUnitsPerTickEnum TimeUnitsPerTick { get; private set; }
        public TimeNumberOfTicksEnum TimeNumberOfTicks { get; private set; }
        public double TimeOffset { get; private set; }

        public double TimeConversionFactor;

        public bool IsDisplayInTimeFormat;

        private Dictionary<TimeUnitEnum, double> TimeConversionFactors = new Dictionary<TimeUnitEnum, double>
        {
            { TimeUnitEnum.Microsecond, 1_000_000.0 },
            { TimeUnitEnum.Millisecond, 1_000.0 },
            { TimeUnitEnum.Second, 1.0 },
            { TimeUnitEnum.Minute, 1 / 60.0 },
            { TimeUnitEnum.Hour, 1 / 3_600.0 },
        };

        public string CurrentUnit { get; private set; }
        public double CurrentUnitsPerTick { get; private set; }
        public int CurrentNumberOfTicks { get; private set; }
        public double CurrentOffset { get; private set; }

        public PresenterManager()
        {
            TimeUnit = TimeUnitEnum.Millisecond;
            TimeUnitsPerTick = TimeUnitsPerTickEnum.Hundred;
            TimeNumberOfTicks = TimeNumberOfTicksEnum.Ten;
            TimeOffset = 0.0;

            CurrentUnit = "mA";
            CurrentUnitsPerTick = 20;
            CurrentNumberOfTicks = 10;
            CurrentOffset = -30.0;
        }

        public DateTime GetDateTime(double timestamp)
        {
            return new DateTime(model.CaptureDate.Ticks + (long)(timestamp * 10_000_000));
        }

        public double GetTimestamp(DateTime datetime)
        {
            return (datetime.Ticks - model.CaptureDate.Ticks) / 10_000_000;
        }

        public void Start()
        {
            UpdateTimeScale();
            UpdateTimeOffset();

            UpdateCurrentScale();
            UpdateCurrentOffset();
        }

        public void Start(string pt5FilePath)
        {
            model.SetParser(pt5FilePath);
        }

        public void AddPresenter(Presenter presenter)
        {
            presenter.PresenterManager = this;

            if (presenter is IScaleSync scaleSyncPresenter)
            {
                scaleSyncPresenters.Add(scaleSyncPresenter);
            }

            presenters.Add(presenter);
        }

        public void TimeScaleChanged(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            TimeUnit = unit;
            TimeUnitsPerTick = unitsPerTick;
            TimeNumberOfTicks = numberOfTicks;

            TimeConversionFactor = TimeConversionFactors[TimeUnit];

            UpdateTimeScale();
        }

        public void TimeOffsetChanged(double offset)
        {
            double delta = (int)TimeUnitsPerTick * (int)TimeNumberOfTicks / TimeConversionFactor;
            if (model.TimeScaleMax - delta < offset)
            {
                offset = model.TimeScaleMax - delta;
            }

            offset = offset < 0 ? 0 : offset;
            TimeOffset = offset;

            UpdateTimeOffset();
        }

        public void CurrentScaleChanged(string unit, double unitsPerTick, int numberOfTicks)
        {
            CurrentUnit = unit;
            CurrentUnitsPerTick = unitsPerTick;
            CurrentNumberOfTicks = numberOfTicks;

            UpdateCurrentScale();
        }

        public void CurrentOffsetChanged(double offset)
        {
            CurrentOffset = offset;

            UpdateCurrentOffset();
        }

        public void DisplayFormatChanged(bool isDisplayInTimeFormat)
        {
            IsDisplayInTimeFormat = isDisplayInTimeFormat;

            UpdateDisplayInTimeFormat();
        }

        private void UpdateTimeScale()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateTimeScale(TimeUnit, TimeUnitsPerTick, TimeNumberOfTicks);
            }
        }

        private void UpdateTimeOffset()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateTimeOffset(TimeOffset);
            }
        }

        private void UpdateCurrentScale()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateCurrentScale(CurrentUnit, CurrentUnitsPerTick, CurrentNumberOfTicks);
            }
        }

        private void UpdateCurrentOffset()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateCurrentOffset(CurrentOffset);
            }
        }

        private void UpdateDisplayInTimeFormat()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateDisplayFormat(IsDisplayInTimeFormat);
            }
        }

        public override string ToString()
        {
            return model.ToString();
        }
    }
}
