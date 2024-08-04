using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Common
{
    static class Constant
    {
        public static readonly string DriveName;
        public static readonly string TempDirectory;
        public static readonly string SettingDirectory;
        public static readonly string LogDirectory;
        public static readonly string ProfileDirectory;

        public static readonly List<string> TimeUnitList;
        public static readonly List<string> TimeUnitsPerTickList;
        public static readonly List<string> TimeNumberOfTicksList;

        public static readonly List<string> CurrentUnitList;
        public static readonly List<string> CurrentUnitsPerTickList;
        public static readonly List<string> CurrentNumberOfTicksList;

        static Constant()
        {
            TimeUnitList = new List<string> { "s", "ms", "us", "min", "hr" };
            TimeUnitsPerTickList = new List<string> { "1", "2", "5", "10", "20", "40", "50", "100" };
            TimeNumberOfTicksList = new List<string> { "2", "4", "5", "10" };

            CurrentUnitList = new List<string> { "A", "mA", "uA" };
            CurrentUnitsPerTickList = new List<string> { "1", "2", "5", "10", "20", "50", "100", "200", "500" };
            CurrentNumberOfTicksList = new List<string> { "2", "4", "5", "10", "20" };

            //TempDirectory = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.Ticks.ToString());
            //SettingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "setting");
            //LogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "log");
        }
    }
}
