﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer.Views
{
    public interface IGraphView
    {
        void SetXAxisTitle(string title);

        void SetXAxisScale(double unitsPerTick, int numberOfTicks);

        void SetXAxisOffset(double offset);

        void SetYAxisTitle(string title);

        void SetYAxisScale(double unitsPerTick, int numberOfTicks);

        void SetYAxisOffset(double offset);

        void UpdateGraph();

        event MouseEventHandler TimeOffsetChanged;

        event EventHandler<ScaleFormatEventArgs> ScaleFormatEventTriggered;

        event EventHandler<DisplayFormatEventArgs> DisplayFormatChanged;

        event EventHandler<OffsetChangedEventArgs> ScrollEventDone;

        event EventHandler<SelectionRangeChangedEventArgs> SelectionRangeChanged;

        event EventHandler<OffsetChangedEventArgs> CurrentOffsetChanged;

        string XAxisFormattedLabel { get; set; }

        void ClearLineItem();

        void LoadLineItem(double scrollMaxX);

        void AddPoint(double x, double y);

        void InsertPoint(int index, double x, double y);

        void RemoveRange(int index, int count);

        void Clear();
    }
}
