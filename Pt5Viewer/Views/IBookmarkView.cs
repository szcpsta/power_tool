using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer.Views
{
    public interface IBookmarkView
    {
        event EventHandler Add;

        event EventHandler<IEnumerable<int>> Remove;

        event EventHandler<int> ItemDoubleClicked;

        #region Virtual Mode
        int VirtualListSize { get; set; }

        event EventHandler<RetrieveVirtualItemEventArgs> RetrieveVirtualItem;

        event EventHandler<CacheVirtualItemsEventArgs> CacheVirtualItems;
        #endregion Virtual Mode

        void EnableView();

        void DisableView();

        void SelectItem(int index);

        void RefreshView();
    }
}
