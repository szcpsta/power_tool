using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Views;

namespace Pt5Viewer.Presenters
{
    public class StatisticsPresenter : Presenter, IScaleSync
    {
        private IStatisticsView view;

        public StatisticsPresenter(IStatisticsView statisticsView)
        {
            view = statisticsView;
        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
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
    }
}
