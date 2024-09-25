using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Configuration.Preferences;

namespace Pt5Viewer.Common
{
    static class Log
    {
        static readonly bool isPdb;

        static Log()
        {
            isPdb = System.IO.File.Exists(System.Reflection.Assembly.GetEntryAssembly().GetName().Name + ".pdb");
        }

        public static void Error(string message)
        {
            if (PreferencesControl.IsLoggingEnabled == false) return;

            if (isPdb == true)
            {
                StackFrame sf = new StackFrame(1, true);
                if (sf != null)
                {
                    string filename = sf.GetFileName();
                    int line = sf.GetFileLineNumber();
                    message += " " + filename + " " + line;
                }
            }

            Trace.TraceError(DateTime.Now.ToString("MM-dd.HH:mm:ss.ffffff ") + message);
        }

        public static void Warn(string message)
        {
            if (PreferencesControl.IsLoggingEnabled == false) return;

            if (isPdb == true)
            {
                StackFrame sf = new StackFrame(1, true);
                if (sf != null)
                {
                    string filename = sf.GetFileName();
                    int line = sf.GetFileLineNumber();
                    message += " " + filename + " " + line;
                }
            }

            Trace.TraceWarning(DateTime.Now.ToString("MM-dd.HH:mm:ss.ffffff ") + message);
        }

        public static void Info(string message)
        {
            if (PreferencesControl.IsLoggingEnabled == false) return;

            if (isPdb == true)
            {
                StackFrame sf = new StackFrame(1, true);
                if (sf != null)
                {
                    string filename = sf.GetFileName();
                    int line = sf.GetFileLineNumber();
                    message += " " + filename + " " + line;
                }
            }

            Trace.TraceInformation(DateTime.Now.ToString("MM-dd.HH:mm:ss.ffffff ") + message);
        }
    }
}
