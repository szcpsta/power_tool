using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Common;

namespace Pt5Viewer.Configuration.Preferences
{
    [Serializable]
    class ShortcutKeyCollection
    {
        private Dictionary<ShortcutKeysTag, Keys> shortcutKeysDic;
        private readonly string shortcutKeyTag;

        public ShortcutKeyCollection()
        {
            shortcutKeysDic = new Dictionary<ShortcutKeysTag, Keys>();

            // Set default shortcut keys
            foreach (ShortcutKeysTag shortcutKey in Enum.GetValues(typeof(ShortcutKeysTag)))
            {
                var value = Constant.ShortcutKeyInfo[shortcutKey].Value;
                shortcutKeysDic.Add(shortcutKey, value);
            }
        }

        public ShortcutKeyCollection(string shortcutKeyTag) : this()
        {
            this.shortcutKeyTag = shortcutKeyTag;

            // 저장된 shortcutKeysDic에 대비 default shortcutKeysDic에 변경 사항이 있다면 반영한다.
            Invalidation();
        }

        public Keys this[ShortcutKeysTag key]
        {
            get
            {
                if (shortcutKeysDic.ContainsKey(key))
                    return shortcutKeysDic[key];
                else
                    throw new KeyNotFoundException(string.Format("Not found {0} key", key.ToString()));
            }
            set
            {
                if (shortcutKeysDic.ContainsKey(key))
                    shortcutKeysDic[key] = value;
                else
                    shortcutKeysDic.Add(key, value);
            }
        }

        public Keys this[int index]
        {
            get
            {
                if (index < shortcutKeysDic.Count)
                    return shortcutKeysDic.ElementAt(index).Value;
                else
                    throw new KeyNotFoundException(string.Format("Not found item {0}index", index.ToString()));
            }
            set
            {
                if (index < shortcutKeysDic.Count)
                {
                    var key = shortcutKeysDic.ElementAt(index).Key;
                    shortcutKeysDic[key] = value;
                }
            }
        }

        public Dictionary<ShortcutKeysTag, Keys>.KeyCollection Keys
        {
            get { return shortcutKeysDic.Keys; }
        }

        public int Count
        {
            get { return shortcutKeysDic.Count; }
        }

        // 새로 추가되는 키가 있는 경우 Collection을 update한다
        public void Invalidation()
        {
            if (shortcutKeysDic.Count != PreferencesControl.DefaultShortcutKeys.Count)
            {
                var newDics = Constant.ShortcutKeyInfo.Where(x => shortcutKeysDic.ContainsKey(x.Key) == false).ToDictionary(t => t.Key, t => t.Value);

                foreach (var newItem in newDics)
                {
                    shortcutKeysDic[newItem.Key] = newItem.Value.Value;
                }
            }
        }
    }
}
