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

        #region DragBox
        private PointF _startDragPoint;
        private PointF _endDragPoint;
        private BoxObj _dragBox;
        private bool _isDragging = false;
        #endregion DragBox

        private bool preventContextMenuStrip = false;

        public event MouseEventHandler TimeOffsetChanged;
        public event EventHandler<ScaleFormatEventArgs> ScaleFormatEventTriggered;
        public event EventHandler<DisplayFormatEventArgs> DisplayFormatChanged;
        public event EventHandler<ScrollEventArgs> ScrollEventDone;
        public event EventHandler<SelectionRangeChangedEventArgs> SelectionRangeChanged;

        public string XAxisFormattedLabel { get; set; }

        public GraphView()
        {
            InitializeComponent();

            ppl = new PointPairList();

            IsShowHScrollBar = true;
            ScrollMinX = 0;
            ScrollMaxX = 60_000;

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
            displayInTimeFormatItem.Click += (s, e) =>
            {
                DisplayFormatChanged?.Invoke(s, new DisplayFormatEventArgs(displayInTimeFormatItem.Checked));
            };

            contextMenuStrip.Items.Add(displayInTimeFormatItem);
            ContextMenuStrip = contextMenuStrip;
            ContextMenuStrip.Opening += (s, e) =>
            {
                if (preventContextMenuStrip == true)
                {
                    e.Cancel = true;
                    preventContextMenuStrip = false;
                }
            };

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

        public void LoadLineItem(double scrollMaxX)
        {
            lineItem = gp.AddCurve("Current", ppl, Color.DarkOrange, SymbolType.None);
            lineItem.IsY2Axis = true;

            ScrollMinX = 0;
            ScrollMaxX = scrollMaxX;
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

            _isDragging = false;
            gp.GraphObjList.Remove(_dragBox);
            _dragBox = null;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            TimeOffsetChanged?.Invoke(this, e);
        }

        /// <summary>
        /// PointPairList 내에서 timestamp의 값보다 크거나 같은 값의 index를 반환한다.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private int GetIndexOf(double timestamp)
        {
            int s = 0;
            int e = ppl.Count;

            while (s + 1 < e)
            {
                int mid = (s + e) >> 1;
                if (timestamp < ppl[mid].X)
                {
                    e = mid;
                }
                else
                {
                    s = mid;
                }
            }

            return s;
        }

        private void GraphView_ScrollDoneEvent(ZedGraphControl sender, ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            ScrollEventDone?.Invoke(this, new ScrollEventArgs(sender.GraphPane.XAxis.Scale.Min));
        }

        private bool GraphView_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (_dragBox != null)
                {
                    gp.GraphObjList.Remove(_dragBox);
                    _dragBox = null;
                    Invalidate();

                    _isDragging = false;

                    SelectionRangeChanged?.Invoke(this, null);
                    preventContextMenuStrip = true;

                    return true;
                }
            }

            using (Graphics g = CreateGraphics())
            {
                if (MasterPane.FindNearestPaneObject(e.Location, g, out GraphPane graphPane, out object nearestObj, out int index))
                {

                }
            }

            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _startDragPoint = new PointF(e.X, e.Y);

                return true;
            }

            return false;
        }

        private bool GraphView_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                _endDragPoint = new PointF(e.X, e.Y);

                if (_dragBox != null)
                {
                    gp.GraphObjList.Remove(_dragBox);
                }

                double sx, sy, ex, ey;
                gp.ReverseTransform(_startDragPoint, out sx, out sy);
                gp.ReverseTransform(_endDragPoint, out ex, out ey);

                double xMin = Math.Max(gp.XAxis.Scale.Min, Math.Min(sx, ex));
                double xMax = Math.Min(gp.XAxis.Scale.Max, Math.Max(sx, ex));

                _dragBox = new BoxObj(xMin, 0, xMax - xMin, 1, Color.Black, Color.FromArgb(50, Color.Orange));
                _dragBox.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
                _dragBox.IsClippedToChartRect = true;
                _dragBox.ZOrder = ZOrder.A_InFront;
                _dragBox.Border.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                _dragBox.Border.Width = 1.0f;

                gp.GraphObjList.Add(_dragBox);
                Invalidate();

                return true;
            }

            return false;
        }

        private bool GraphView_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                if (_dragBox != null)
                {
                    int x1Index = GetIndexOf(_dragBox.Location.X1);
                    int x2Index = GetIndexOf(_dragBox.Location.X2);

                    int count = x2Index - x1Index;
                    int missingCount = 0;

                    if (count != 0)
                    {
                        double sum = 0;
                        for (int i = x1Index; i < x2Index; i++)
                        {
                            if (ppl[i].Y == Constant.Missing)
                            {
                                missingCount++;
                                continue;
                            }
                            sum += ppl[i].Y;
                        }

                        SelectionRangeChanged?.Invoke(this, new SelectionRangeChangedEventArgs(ppl[x2Index].X - ppl[x1Index].X, count, sum / (count - missingCount)));
                    }
                }

                _isDragging = false;
                Invalidate();

                return true;
            }

            return false;
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

    public class ScrollEventArgs : EventArgs
    {
        public double Val { get; }

        public ScrollEventArgs(double val)
        {
            Val = val;
        }
    }

    public class SelectionRangeChangedEventArgs : EventArgs
    {
        public double Time { get; }

        public long Samples { get; }

        public double AverageCurrent { get; }

        public SelectionRangeChangedEventArgs(double time, long samples, double averageCurrent)
        {
            Time = time;
            Samples = samples;
            AverageCurrent = averageCurrent;
        }
    }
}
