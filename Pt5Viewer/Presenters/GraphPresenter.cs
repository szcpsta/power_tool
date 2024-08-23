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
    public class GraphPresenter : Presenter, IScaleSync
    {
        private IGraphView view;
        private Pt5Model model;

        public GraphPresenter(IGraphView graphView)
        {
            view = graphView;

            view.TimeOffsetChanged += (s, e) =>
            {
                double nextOffset = PresenterManager.TimeOffset + (e.Delta / 120 * -(int)PresenterManager.TimeUnitsPerTick / PresenterManager.TimeConversionFactor);
                PresenterManager.TimeOffsetChanged(nextOffset);
            };

            view.ScaleFormatEventTriggered += (s, e) =>
            {
                if (PresenterManager.IsDisplayInTimeFormat)
                {
                    view.XAxisFormattedLabel = $"{PresenterManager.GetDateTime(e.Val).ToString("HH:mm:ss.ffff")}";
                }
                else
                {
                    view.XAxisFormattedLabel = $"{e.Val * PresenterManager.TimeConversionFactor}";
                }
            };

            view.DisplayFormatChanged += (s, e) =>
            {
                PresenterManager.DisplayFormatChanged(e.IsDisplayInTimeFormat);
            };
        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            view.SetXAxisTitle($"Time({Util.GetEnumDescription(unit)})");
            view.SetXAxisScale((int)unitsPerTick / PresenterManager.TimeConversionFactor, (int)numberOfTicks);

            view.UpdateGraph();
        }

        public void UpdateTimeOffset(double offset)
        {
            view.SetXAxisOffset(offset);

            view.UpdateGraph();
        }

        public void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks)
        {
            view.SetYAxisTitle($"{unit}");
            view.SetYAxisScale(unitsPerTick, numberOfTicks);

            view.UpdateGraph();
        }

        public void UpdateCurrentOffset(double offset)
        {
            view.SetYAxisOffset(offset);

            view.UpdateGraph();
        }

        public void UpdateDisplayFormat(bool isDisplayInTimeFormat)
        {
            view.UpdateGraph();
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
            model = null;
        }

        public override void ModelCreated(Pt5Model pt5Model)
        {
            model = pt5Model;
        }

        public override void ModelStarted()
        {
            base.ModelStarted();
        }
    }
}
