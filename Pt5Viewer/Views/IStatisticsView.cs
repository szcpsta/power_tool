using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Views
{
    public interface IStatisticsView
    {
        string TimeUnit { get; set; }

        string CurrentUnit { get; set; }
    }
}
