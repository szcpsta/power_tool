﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Views;

namespace Pt5Viewer.Presenters
{
    public class GraphPresenter : Presenter, IScaleSync
    {
        private IGraphView view;

        public GraphPresenter(IGraphView graphView)
        {
            view = graphView;
        }

        public void UpdateTimeScale(string unit, int unitsPerTick, int numberOfTicks)
        {
            view.SetXAxisTitle($"Time({unit})");
            view.SetXAxisScale(unitsPerTick, numberOfTicks);

            view.UpdateGraph();
        }

        public void UpdateTimeOffset(double offset)
        {
            view.SetXAxisOffset(offset);

            view.UpdateGraph();
        }

        public void UpdateCurrentScale(string unit, int unitsPerTick, int numberOfTicks)
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
    }
}
