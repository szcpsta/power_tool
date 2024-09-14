using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Configuration.Preferences
{
    public static class ConfigurationControl
    {
        public static Hashtable ConfigurationTable = new Hashtable();

        public static bool ContainsKey(string key)
        {
            return ConfigurationTable.ContainsKey(key);
        }

        public static object GetValue(string key)
        {
            if (ConfigurationTable.ContainsKey(key))
                return ConfigurationTable[key];
            else
                return null;
        }

        public static void SetValue(string key, object value)
        {
            if (ConfigurationTable.ContainsKey(key))
                ConfigurationTable[key] = value;
            else
                ConfigurationTable.Add(key, value);
        }
    }
}
