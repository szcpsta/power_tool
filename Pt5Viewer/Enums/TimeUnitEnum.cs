using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Enums
{
    public enum TimeUnitEnum
    {
        [Description("s")]
        Second,

        [Description("ms")]
        Millisecond,

        [Description("us")]
        Microsecond,

        [Description("min")]
        Minute,

        [Description("hr")]
        Hour,
    }
}
