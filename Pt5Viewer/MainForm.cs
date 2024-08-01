using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SetTitle();
        }

        private void SetTitle()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            
            string title = $"[{Id}] {Application.ProductName} v{version.Major}.{version.Minor}.{version.Build}";
            if (version.Revision != 0)
            {
                title = $"{title}.{version.Revision}";
            }

            Text = title;
        }

        public static int Id
        {
            get
            {
                string processName = Process.GetCurrentProcess().ProcessName;

                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 1)
                {
                    return Process.GetCurrentProcess().Id;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
