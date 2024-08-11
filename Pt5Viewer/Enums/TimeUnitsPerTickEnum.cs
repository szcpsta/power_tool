using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Enums
{
    public enum TimeUnitsPerTickEnum
    {
        [Description("1")]
        One = 1,

        [Description("2")]
        Two = 2,

        [Description("5")]
        Five = 5,

        [Description("10")]
        Ten = 10,

        [Description("20")]
        Twenty = 20,

        [Description("40")]
        Forty = 40,

        [Description("50")]
        Fifty = 50,

        [Description("100")]
        Hundred = 100,
    }
}
