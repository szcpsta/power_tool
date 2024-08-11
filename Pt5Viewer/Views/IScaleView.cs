using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Enums;

namespace Pt5Viewer.Views
{
    public interface IScaleView
    {
        void SetTimeUnitComboBoxItems(IEnumerable<object> items);

        void SetTimeUnitsPerTickComboBoxItems(IEnumerable<object> items);

        void SetTimeNumberOfTicksComboBoxItems(IEnumerable<object> items);

        void SetCurrentUnitComboBoxItems(object[] items);

        void SetCurrentUnitsPerTickComboBoxItems(object[] items);

        void SetCurrentNumberOfTicksComboBoxItems(object[] items);

        TimeUnitEnum TimeUnit { get; set; }

        TimeUnitsPerTickEnum TimeUnitsPerTick { get; set; }

        TimeNumberOfTicksEnum TimeNumberOfTicks { get; set; }

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
