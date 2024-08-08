using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Models;

namespace Pt5Viewer.Presenters
{
    public class PresenterManager
    {
        private Pt5Model model = new Pt5Model();

        private List<Presenter> presenters = new List<Presenter>();

        private List<IScaleSync> scaleSyncPresenters = new List<IScaleSync>();

        public string TimeUnit { get; private set; }
        public int TimeUnitsPerTick { get; private set; }
        public int TimeNumberOfTicks { get; private set; }
        public double TimeOffset { get; private set; }

        public string CurrentUnit { get; private set; }
        public int CurrentUnitsPerTick { get; private set; }
        public int CurrentNumberOfTicks { get; private set; }
        public double CurrentOffset { get; private set; }

        public PresenterManager()
        {
            TimeUnit = "ms";
            TimeUnitsPerTick = 100;
            TimeNumberOfTicks = 10;
            TimeOffset = 0.0;

            CurrentUnit = "mA";
            CurrentUnitsPerTick = 20;
            CurrentNumberOfTicks = 10;
            CurrentOffset = -30.0;
        }

        public void Start()
        {
            UpdateTimeScale();
            UpdateTimeOffset();

            UpdateCurrentScale();
            UpdateCurrentOffset();
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

        public void TimeScaleChanged(string unit, int unitsPerTick, int numberOfTicks)
        {
            TimeUnit = unit;
            TimeUnitsPerTick = unitsPerTick;
            TimeNumberOfTicks = numberOfTicks;

            UpdateTimeScale();
        }

        public void TimeOffsetChanged(double offset)
        {
            if (model.TimeScaleMax - (TimeUnitsPerTick * TimeNumberOfTicks) < offset)
            {
                offset = model.TimeScaleMax - (TimeUnitsPerTick * TimeNumberOfTicks);
            }

            offset = offset < 0 ? 0 : offset;
            TimeOffset = offset;

            UpdateTimeOffset();
        }

        public void CurrentScaleChanged(string unit, int unitsPerTick, int numberOfTicks)
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
    }
}
