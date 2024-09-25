using System;
using System.Collections.Generic;
using System.Drawing;
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
            Font,
        }

        public static bool IsLoggingEnabled = true;

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

        static public Font Font
        {
            get
            {
                if (ConfigurationControl.ContainsKey(Field.Font.ToString()) == true)
                {
                    return (Font)ConfigurationControl.GetValue(Field.Font.ToString());
                }
                else
                {
                    try
                    {
                        return new Font("맑은 고딕", 9F);
                    }
                    catch
                    {
                        return SystemFonts.DefaultFont;
                    }
                }
            }
            set
            {
                ConfigurationControl.SetValue(Field.Font.ToString(), value);
            }
        }
    }

    public enum ShortcutKeysTag
    {
        Close,
    }
}
