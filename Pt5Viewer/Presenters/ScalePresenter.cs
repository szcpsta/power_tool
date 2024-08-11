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
    public class ScalePresenter : Presenter, IScaleSync
    {
        private IScaleView view;

        public ScalePresenter(IScaleView scaleView)
        {
            view = scaleView;

            view.SetTimeUnitComboBoxItems(Enum.GetValues(typeof(TimeUnitEnum))
                                                        .Cast<TimeUnitEnum>()
                                                        .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                        .ToList());

            view.SetTimeUnitsPerTickComboBoxItems(Enum.GetValues(typeof(TimeUnitsPerTickEnum))
                                                        .Cast<TimeUnitsPerTickEnum>()
                                                        .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                        .ToList());

            view.SetTimeNumberOfTicksComboBoxItems(Enum.GetValues(typeof(TimeNumberOfTicksEnum))
                                                        .Cast<TimeNumberOfTicksEnum>()
                                                        .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                        .ToList());

            view.SetCurrentUnitComboBoxItems(Constant.CurrentUnitList.ToArray());
            view.SetCurrentUnitsPerTickComboBoxItems(Constant.CurrentUnitsPerTickList.ToArray());
            view.SetCurrentNumberOfTicksComboBoxItems(Constant.CurrentNumberOfTicksList.ToArray());

            // OnTimeScaleChanged
            view.TimeScaleChanged += (s, e) =>
            {
                TimeUnitEnum unit = view.TimeUnit;
                TimeUnitsPerTickEnum unitsPerTick = view.TimeUnitsPerTick;
                TimeNumberOfTicksEnum numberOfTicks = view.TimeNumberOfTicks;

                PresenterManager.TimeScaleChanged(unit, unitsPerTick, numberOfTicks);
            };

            // OnTimeOffsetChanged
            view.TimeOffsetChanged += (s, e) =>
            {
                double offset;
                if (double.TryParse(view.TimeOffset, out offset) == true)
                {
                    PresenterManager.TimeOffsetChanged(offset);
                }
            };

            // OnCurrentScaleChanged
            view.CurrentScaleChanged += (s, e) =>
            {
                if (Constant.CurrentUnitList.Contains(view.CurrentUnit) == false) return;
                if (Constant.CurrentUnitsPerTickList.Contains(view.CurrentUnitsPerTick) == false) return;
                if (Constant.CurrentNumberOfTicksList.Contains(view.CurrentNumberOfTicks) == false) return;

                string unit = view.CurrentUnit;
                int unitsPerTick = int.Parse(view.CurrentUnitsPerTick);
                int numberOfTicks = int.Parse(view.CurrentNumberOfTicks);

                PresenterManager.CurrentScaleChanged(unit, unitsPerTick, numberOfTicks);
            };

            // OnCurrentOffsetChanged
            view.CurrentOffsetChanged += (s, e) =>
            {
                double offset;
                if (double.TryParse(view.CurrentOffset, out offset) == true)
                {
                    PresenterManager.CurrentOffsetChanged(offset);
                }
            };
        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            view.TimeUnit = unit;
            view.TimeUnitsPerTick = unitsPerTick;
            view.TimeNumberOfTicks = numberOfTicks;
        }

        public void UpdateTimeOffset(double offset)
        {
            view.TimeOffset = offset.ToString("F2");
        }

        public void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks)
        {
            view.CurrentUnit = unit;
            view.CurrentUnitsPerTick = (unitsPerTick * PresenterManager.TimeConversionFactor).ToString();
            view.CurrentNumberOfTicks = numberOfTicks.ToString();
        }

        public void UpdateCurrentOffset(double offset)
        {
            view.CurrentOffset = offset.ToString("F2");
        }
    }
}
