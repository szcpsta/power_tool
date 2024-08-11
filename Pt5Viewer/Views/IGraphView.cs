using System;
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

        bool IsDisplayInTimeFormat { get; set; }

        string XAxisFormattedLabel { get; set; }

    }
}
