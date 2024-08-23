using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Models;
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
                if (PresenterManager.IsDisplayInTimeFormat == true)
                {
                    DateTime datetime;
                    if (DateTime.TryParseExact(view.TimeOffset, "HH:mm:ss.ffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime) == true)
                    {
                        PresenterManager.TimeOffsetChanged(PresenterManager.GetTimestamp(datetime));
                    }
                }
                else
                {
                    double offset;
                    if (double.TryParse(view.TimeOffset, out offset) == true)
                    {
                        PresenterManager.TimeOffsetChanged(offset / PresenterManager.TimeConversionFactor);
                    }
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

            UpdateTimeOffset(PresenterManager.TimeOffset);
        }

        public void UpdateTimeOffset(double offset)
        {
            if (PresenterManager.IsDisplayInTimeFormat == true)
            {
                view.TimeOffset = $"{PresenterManager.GetDateTime(offset).ToString("HH:mm:ss.ffff")}";
            }
            else
            {
                view.TimeOffset = (offset * PresenterManager.TimeConversionFactor).ToString("F2");
            }
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

        public void UpdateDisplayFormat(bool isDisplayInTimeFormat)
        {
            UpdateTimeOffset(PresenterManager.TimeOffset);
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override void Restart()
        {
            base.Restart();
        }
        public override void ModelClosing()
        {
            base.ModelClosing();
        }

        public override void ModelCreated(Pt5Model pt5Model)
        {
            base.ModelCreated(pt5Model);
        }

        public override void ModelStarted()
        {
            base.ModelStarted();
        }
    }
}
