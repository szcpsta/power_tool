using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZedGraph;

using Pt5Viewer.Common;

namespace Pt5Viewer.Views
{
    public partial class GraphView : ZedGraphControl, IGraphView
    {
        GraphPane gp;
        TextObj y2AxisTitleObj;
        ContextMenuStrip contextMenuStrip;
        PointPairList ppl;
        LineItem lineItem;

        public event MouseEventHandler TimeOffsetChanged;
        public event EventHandler<ScaleFormatEventArgs> ScaleFormatEventTriggered;
        public event EventHandler<DisplayFormatEventArgs> DisplayFormatChanged;

        public string XAxisFormattedLabel { get; set; }

        public GraphView()
        {
            InitializeComponent();

            ppl = new PointPairList();

            gp = GraphPane;

            // Set Common
            gp.IsFontsScaled = false;

            gp.Legend.IsVisible = false;
            gp.Fill = new Fill(SystemColors.Control);

            IsEnableHZoom = false;
            IsEnableVZoom = false;

            // Set Title
            gp.Title.Text = "Measured Power Data";
            gp.Title.FontSpec.Size = 16;

            // Set XAxis
            gp.XAxis.IsVisible = true;

            gp.XAxis.Title.FontSpec.Size = 14;

            gp.XAxis.Scale.Mag = 0;
            gp.XAxis.Scale.Format = "0";
            gp.XAxis.Scale.IsSkipFirstLabel = true;
            gp.XAxis.Scale.IsSkipLastLabel = true;
            gp.XAxis.Scale.FontSpec.Size = 11;

            gp.XAxis.MajorGrid.IsVisible = false;
            gp.XAxis.MinorGrid.IsVisible = false;

            gp.XAxis.MajorTic.IsOpposite = false;
            gp.XAxis.MinorTic.IsOpposite = false;

            gp.XAxis.ScaleFormatEvent += XAxis_ScaleFormatEvent;

            gp.X2Axis.IsVisible = false;

            // Set YAxis
            gp.YAxis.IsVisible = false;

            gp.Y2Axis.IsVisible = true;

            gp.Y2Axis.Title.IsVisible = false;

            y2AxisTitleObj = new TextObj("", 1, 1, CoordType.XChartFractionYPaneFraction, AlignH.Left, AlignV.Bottom);
            y2AxisTitleObj.FontSpec = gp.XAxis.Title.FontSpec;
            gp.GraphObjList.Add(y2AxisTitleObj);

            gp.Y2Axis.Scale.Mag = 0;
            gp.Y2Axis.Scale.FontSpec.Size = 11;

            gp.Y2Axis.MajorGrid.IsVisible = true;
            gp.Y2Axis.MinorGrid.IsVisible = true;
            gp.Y2Axis.MajorGrid.IsZeroLine = false;

            gp.Y2Axis.MajorTic.IsOpposite = false;
            gp.Y2Axis.MajorTic.IsInside = true;
            gp.Y2Axis.MajorTic.IsOutside = true;

            gp.Y2Axis.MinorTic.IsOpposite = false;
            gp.Y2Axis.MinorTic.IsInside = false;
            gp.Y2Axis.MinorTic.IsOutside = false;

            // ContextMenuStrip
            contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem displayInTimeFormatItem = new ToolStripMenuItem("Display in Time Format");
            displayInTimeFormatItem.CheckOnClick = true;
            displayInTimeFormatItem.Click += (s, e) => {
                DisplayFormatChanged?.Invoke(s, new DisplayFormatEventArgs(displayInTimeFormatItem.Checked));
            };

            contextMenuStrip.Items.Add(displayInTimeFormatItem);
            ContextMenuStrip = contextMenuStrip;

            // Update
            UpdateGraph();
        }

        private string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            ScaleFormatEventTriggered?.Invoke(this, new ScaleFormatEventArgs(pane, axis, val, index));

            return XAxisFormattedLabel;
        }

        public void SetXAxisTitle(string title)
        {
            gp.XAxis.Title.Text = title;
        }

        public void SetXAxisScale(double unitsPerTick, int numberOfTicks)
        {
            gp.XAxis.Scale.Max = gp.XAxis.Scale.Min + unitsPerTick * numberOfTicks;

            gp.XAxis.Scale.MajorStep = unitsPerTick;
            gp.XAxis.Scale.MinorStep = unitsPerTick;
        }

        public void SetXAxisOffset(double offset)
        {
            var delta = gp.XAxis.Scale.Max - gp.XAxis.Scale.Min;
            gp.XAxis.Scale.Min = offset;
            gp.XAxis.Scale.Max = gp.XAxis.Scale.Min + delta;
        }

        public void SetYAxisTitle(string title)
        {
            y2AxisTitleObj.Text = title;
        }

        public void SetYAxisScale(double unitsPerTick, int numberOfTicks)
        {
            gp.Y2Axis.Scale.Max = gp.Y2Axis.Scale.Min + unitsPerTick * numberOfTicks;

            gp.Y2Axis.Scale.MajorStep = unitsPerTick;
            gp.Y2Axis.Scale.MinorStep = gp.Y2Axis.Scale.MajorStep / 4;
        }

        public void SetYAxisOffset(double offset)
        {
            var delta = gp.Y2Axis.Scale.Max - gp.Y2Axis.Scale.Min;
            gp.Y2Axis.Scale.Min = offset;
            gp.Y2Axis.Scale.Max = gp.Y2Axis.Scale.Min + delta;
        }

        public void ClearLineItem()
        {
            ppl.Clear();
        }

        public void LoadLineItem(string label)
        {
            lineItem = gp.AddCurve(label, ppl, Color.Orange, SymbolType.None);
            lineItem.IsY2Axis = true;
        }

        public void InsertPoint(int index, double x, double y)
        {
            if (y == Constant.Missing)
            {
                y = PointPair.Missing;
            }
            ppl.Insert(index, x, y);
        }

        public void RemoveRange(int index, int count)
        {
            ppl.RemoveRange(index, count);
        }

        public void AddPoint(double x, double y)
        {
            if (y == Constant.Missing)
            {
                y = PointPair.Missing;
            }
            ppl.Add(x, y);
        }

        public void UpdateGraph()
        {
            AxisChange();
            Invalidate();
        }

        public void Clear()
        {
            ppl.Clear();
            gp.CurveList.Remove(lineItem);
            lineItem = null;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            TimeOffsetChanged?.Invoke(this, e);
        }
    }

    public class ScaleFormatEventArgs : EventArgs
    {
        public GraphPane Pane { get; }
        public Axis Axis { get; }
        public double Val { get; }
        public int Index { get; }

        public ScaleFormatEventArgs(GraphPane pane, Axis axis, double val, int index)
        {
            Pane = pane;
            Axis = axis;
            Val = val;
            Index = index;
        }
    }

    public class DisplayFormatEventArgs : EventArgs
    {
        public bool IsDisplayInTimeFormat { get; }

        public DisplayFormatEventArgs(bool isDisplayInTimeFormat)
        {
            IsDisplayInTimeFormat = isDisplayInTimeFormat;
        }
    }
}
