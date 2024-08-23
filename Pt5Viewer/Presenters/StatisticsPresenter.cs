using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Models;
using Pt5Viewer.Views;

namespace Pt5Viewer.Presenters
{
    public class StatisticsPresenter : Presenter, IScaleSync
    {
        private IStatisticsView view;
        private Pt5Model model;

        public StatisticsPresenter(IStatisticsView statisticsView)
        {
            view = statisticsView;
        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            UpdateTimeValue();
            view.TimeUnit = Util.GetEnumDescription(unit);
        }

        public void UpdateTimeOffset(double offset)
        {
            //throw new NotImplementedException();
        }

        public void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks)
        {
            view.CurrentUnit = unit;
        }

        public void UpdateCurrentOffset(double offset)
        {
            //throw new NotImplementedException();
        }

        public void UpdateDisplayFormat(bool isDisplayInTimeFormat)
        {
            //
        }

        public override void Clear()
        {
        }

        public override void Restart()
        {
            view.TimeValue = "-.--";
            view.SamplesValue = "-.--";
            view.AverageCurrentValue = "-.--";
        }

        public override void ModelClosing()
        {
            model = null;
        }

        public override void ModelCreated(Pt5Model pt5Model)
        {
            model = pt5Model;
        }

        public override void ModelStarted()
        {
            UpdateTimeValue();
            view.SamplesValue = model.SampleCount.ToString();
            view.AverageCurrentValue = model.AverageCurrent.ToString("F2");
        }

        private void UpdateTimeValue()
        {
            view.TimeValue = (model.TimeScaleMax * PresenterManager.TimeConversionFactor).ToString("F2");
        }
    }
}
