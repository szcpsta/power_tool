using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Configuration.Preferences;

namespace Pt5Viewer.Common
{
    static class Constant
    {
        public static readonly string DriveName;
        public static readonly string TempDirectory;
        public static readonly string SettingDirectory;
        public static readonly string LogDirectory;
        public static readonly string ProfileDirectory;

        public static readonly List<string> CurrentUnitList;
        public static readonly List<string> CurrentUnitsPerTickList;
        public static readonly List<string> CurrentNumberOfTicksList;

        public static readonly double Missing = Double.MaxValue;

        public const string DATETIME_FORMAT = "HH:mm:ss.ffff";
        public const string NUMBER_FORMAT = "F2";

        public static readonly Color DefaultBackColor = Color.FromArgb(51, 153, 255);
        public static readonly Color DefaultForeColor = Color.FromArgb(255, 255, 255);

        public static readonly Dictionary<ShortcutKeysTag, ShortcutKey> ShortcutKeyInfo = new Dictionary<ShortcutKeysTag, ShortcutKey>()
        {
            { ShortcutKeysTag.Close, new ShortcutKey("Close View", Keys.Control | Keys.X) },
        };

        public class ShortcutKey
        {
            private readonly string description;

            private readonly Keys value;

            public string Description => description;

            public Keys Value => value;

            public  ShortcutKey(string description, Keys value)
            {
                this.description = description;
                this.value = value;
            }
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


        static Constant()
        {
            CurrentUnitList = new List<string> { "A", "mA", "uA" };
            CurrentUnitsPerTickList = new List<string> { "1", "2", "5", "10", "20", "50", "100", "200", "500" };
            CurrentNumberOfTicksList = new List<string> { "2", "4", "5", "10", "20" };

            //TempDirectory = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.Ticks.ToString());
            //SettingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "setting");
            //LogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "log");
        }
    }
}
