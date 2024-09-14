using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Configuration.Preferences;

namespace Pt5Viewer.Common
{
    public partial class AltUserControl : UserControl
    {
        public AltUserControl()
        {
            InitializeComponent();

            Font = PreferencesControl.Font;
        }
    }
}
