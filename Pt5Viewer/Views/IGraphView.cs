﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Views
{
    public interface IGraphView
    {
        void SetXAxisTitle(string title);

        void SetXAxisScale(int unitsPerTick, int numberOfTicks);

        void SetXAxisOffset(double offset);

        void SetYAxisTitle(string title);

        void SetYAxisScale(int unitsPerTick, int numberOfTicks);

        void SetYAxisOffset(double offset);

        void UpdateGraph();
    }
}
