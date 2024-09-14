using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Common;

namespace Pt5Viewer.Views
{
    public partial class BookmarkView : AltUserControl, IBookmarkView
    {
        public int VirtualListSize { get => listView.VirtualListSize; set => listView.VirtualListSize = value; }

        public event EventHandler Add;
        public event EventHandler<IEnumerable<int>> Remove;
        public event EventHandler<int> ItemDoubleClicked;

        public event EventHandler<RetrieveVirtualItemEventArgs> RetrieveVirtualItem;
        public event EventHandler<CacheVirtualItemsEventArgs> CacheVirtualItems;

        public BookmarkView()
        {
            InitializeComponent();

            listView.FullRowSelect = true;
            listView.MultiSelect = true;
            listView.OwnerDraw = true;

            listView.BeginUpdate();

            listView.Columns.Add(string.Empty, 20);
            listView.Columns.Add("Timestamp", this.Width - 50);

            listView.EndUpdate();
        }

        public void RefreshView()
        {
            listView.Refresh();
        }

        #region Virtual Mode

        private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            RetrieveVirtualItem?.Invoke(sender, e);
        }

        private void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            CacheVirtualItems?.Invoke(sender, e);
        }

        #endregion Virtual Mode

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            ItemDoubleClicked?.Invoke(sender, listView.SelectedIndices[0]);
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            Add?.Invoke(sender, e);
        }

        private void toolStripButtonRemove_Click(object sender, EventArgs e)
        {
            Remove?.Invoke(sender, listView.SelectedIndices.Cast<int>());
        }

        public void EnableView()
        {
            toolStripButtonAdd.Enabled = true;
            toolStripButtonRemove.Enabled = true;
            listView.Enabled = true;
        }

        public void DisableView()
        {
            toolStripButtonAdd.Enabled = false;
            toolStripButtonRemove.Enabled = false;
            listView.Enabled = false;
        }

        public void SelectItem(int index)
        {
            listView.SelectedIndices.Clear();

            listView.Items[index].Selected = true;
            listView.Items[index].Focused = true;

            listView.EnsureVisible(index);
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Brush brush;
            Color foreColor;

            if (e.Item.Selected == true)
            {
                brush = new SolidBrush(Constant.DefaultBackColor);
                foreColor = Constant.DefaultForeColor;
            }
            else
            {
                brush = Brushes.White;
                foreColor = Color.Black;
            }

            e.Graphics.FillRectangle(brush, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height - 3);
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, Font, e.Bounds, foreColor, TextFormatFlags.Default);

            if (e.ColumnIndex == 0)
            {
                //if (bookmarkList[e.ItemIndex].Equals(targetItem))
                //{
                //    e.Graphics.DrawImage(Properties.Resources.origin_trace, e.Bounds.X, e.Bounds.Y);
                //}
            }
        }
    }
}
