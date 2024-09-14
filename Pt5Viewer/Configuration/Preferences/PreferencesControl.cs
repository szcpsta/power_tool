using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Configuration.Preferences
{
    static class PreferencesControl
    {
        enum Field
        {
            ShortcutKeys,
        }

        public readonly static ShortcutKeyCollection DefaultShortcutKeys = new ShortcutKeyCollection();

        static private ShortcutKeyCollection shortcutKeys = new ShortcutKeyCollection(Field.ShortcutKeys.ToString());

        static public ShortcutKeyCollection ShortcutKeys
        {
            get
            {
                if (ConfigurationControl.ContainsKey(Field.ShortcutKeys.ToString()))
                {
                    var existingShortcutkeys = (ShortcutKeyCollection)ConfigurationControl.GetValue(Field.ShortcutKeys.ToString());
                    existingShortcutkeys.Invalidation();

                    return existingShortcutkeys;
                }
                else
                    return DefaultShortcutKeys;
            }
            set
            {
                ConfigurationControl.SetValue(Field.ShortcutKeys.ToString(), value);
            }
        }
    }

    public enum ShortcutKeysTag
    {
        Close,
    }
}
