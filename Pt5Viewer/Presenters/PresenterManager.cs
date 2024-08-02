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

        private string timeUnit;
        private int timeUnitsPerTick;
        private int timeNumberOfTicks;
        private double timeOffset;

        private string currentUnit;
        private int currentUnitsPerTick;
        private int currentNumberOfTicks;
        private double currentOffset;

        public PresenterManager()
        {
            timeUnit = "ms";
            timeUnitsPerTick = 100;
            timeNumberOfTicks = 10;
            timeOffset = 0.0;

            currentUnit = "mA";
            currentUnitsPerTick = 20;
            currentNumberOfTicks = 10;
            currentOffset = -30.0;
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
            timeUnit = unit;
            timeUnitsPerTick = unitsPerTick;
            timeNumberOfTicks = numberOfTicks;

            UpdateTimeScale();
        }

        public void TimeOffsetChanged(double offset)
        {
            timeOffset = offset;

            UpdateTimeOffset();
        }

        public void CurrentScaleChanged(string unit, int unitsPerTick, int numberOfTicks)
        {
            currentUnit = unit;
            currentUnitsPerTick = unitsPerTick;
            currentNumberOfTicks = numberOfTicks;

            UpdateCurrentScale();
        }

        public void CurrentOffsetChanged(double offset)
        {
            currentOffset = offset;

            UpdateCurrentOffset();
        }

        private void UpdateTimeScale()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateTimeScale(timeUnit, timeUnitsPerTick, timeNumberOfTicks);
            }
        }

        private void UpdateTimeOffset()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateTimeOffset(timeOffset);
            }
        }

        private void UpdateCurrentScale()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateCurrentScale(currentUnit, currentUnitsPerTick, currentNumberOfTicks);
            }
        }

        private void UpdateCurrentOffset()
        {
            foreach (var scaleSyncPresenter in scaleSyncPresenters)
            {
                scaleSyncPresenter.UpdateCurrentOffset(currentOffset);
            }
        }
    }
}
