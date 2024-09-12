using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer.Common
{
    public class AltVirtualListView : ListView
    {
        public AltVirtualListView()
        {
            DoubleBuffered = true;
            VirtualMode = true;

            View = View.Details;

            Font = new System.Drawing.Font("맑은 고딕", 9);
        }
    }
}
