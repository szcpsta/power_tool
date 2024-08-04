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

namespace Pt5Viewer.Views
{
    public partial class GraphView : ZedGraphControl, IGraphView
    {
        GraphPane gp;
        TextObj y2AxisTitleObj;

        public event MouseEventHandler TimeOffsetChanged;

        public GraphView()
        {
            InitializeComponent();

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
            gp.XAxis.Scale.FontSpec.Size = 11;

            gp.XAxis.MajorGrid.IsVisible = false;
            gp.XAxis.MinorGrid.IsVisible = false;

            gp.XAxis.MajorTic.IsOpposite = false;
            gp.XAxis.MinorTic.IsOpposite = false;

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

            // Update
            UpdateGraph();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            TimeOffsetChanged?.Invoke(this, e);
        }

        public void SetXAxisTitle(string title)
        {
            gp.XAxis.Title.Text = title;
        }

        public void SetXAxisScale(int unitsPerTick, int numberOfTicks)
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

        public void SetYAxisScale(int unitsPerTick, int numberOfTicks)
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

        public void UpdateGraph()
        {
            AxisChange();
            Invalidate();
        }
    }
}
