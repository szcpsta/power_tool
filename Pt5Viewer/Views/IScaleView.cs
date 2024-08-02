using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Views
{
    public interface IScaleView
    {
        string TimeUnit { get; set; }

        string TimeUnitsPerTick { get; set; }

        string TimeNumberOfTicks { get; set; }

        string TimeOffset { get; set; }

        string CurrentUnit { get; set; }

        string CurrentUnitsPerTick { get; set; }

        string CurrentNumberOfTicks { get; set; }

        string CurrentOffset { get; set; }

        event EventHandler TimeScaleChanged;

        event EventHandler TimeOffsetChanged;

        event EventHandler CurrentScaleChanged;

        event EventHandler CurrentOffsetChanged;
    }
}
