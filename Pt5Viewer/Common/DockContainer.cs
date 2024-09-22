using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Pt5Viewer.Common
{
    public partial class DockContainer : DockForm
    {

        public DockPanel InnerDockPanel { get; private set; }

        public DockContainer()
        {
            InitializeComponent();

            dockPanel.Theme = new VS2015DarkTheme();
            InnerDockPanel = dockPanel;
        }
    }
}
