using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener($"{Application.ProductName}.log"));
            Trace.AutoFlush = true;

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Application.Run(new MainForm());
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Trace.Flush();
            foreach (TraceListener listener in Trace.Listeners)
            {
                listener.Close();
            }

            Trace.Listeners.Clear();
        }
    }
}
