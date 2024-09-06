using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Models;
using Pt5Viewer.Views;

namespace Pt5Viewer.Presenters
{
    public class BookmarkPresenter : Presenter, IScaleSync
    {
        private IBookmarkView view;
        private Pt5Model model;

        private object cacheLock = new object();
        private int startOffset = -1;
        private List<ListViewItem> cacheList = new List<ListViewItem>();

        public BookmarkPresenter(IBookmarkView bookmarkView)
        {
            view = bookmarkView;
            view.Add += (s, e) =>
            {
                model.bookmarkList.Add(PresenterManager.TimeOffset);
                view.VirtualListSize = model.bookmarkList.Count();
            };

            view.Remove += (s, e) =>
            {
                foreach (var index in e.OrderByDescending(i => i))
                {
                    model.bookmarkList.RemoveAt(index);
                }

                Clear();
                view.VirtualListSize = model.bookmarkList.Count();
            };

            view.ItemDoubleClicked += (s, e) =>
            {
                PresenterManager.TimeOffsetChanged(model.bookmarkList[e]);
            };

            view.RetrieveVirtualItem += (s, e) =>
            {
                if (startOffset < 0 || e.ItemIndex < startOffset || e.ItemIndex >= (startOffset + cacheList.Count))
                {
                    e.Item = SetListViewItem(e.ItemIndex);
                }
                else
                {
                    e.Item = cacheList[e.ItemIndex - startOffset];
                }
            };

            view.CacheVirtualItems += (s, e) =>
            {
                lock (cacheLock)
                {
                    int startIndex;
                    int endIndex;

                    if (cacheList.Count > 0)
                    {
                        startIndex = startOffset;
                        endIndex = startOffset + cacheList.Count() - 1;
                    }
                    else
                    {
                        startIndex = endIndex = -1;
                    }

                    if (e.EndIndex <= endIndex && e.StartIndex >= startIndex) return;
                    else if (e.EndIndex < startIndex || e.StartIndex > endIndex)
                    {
                        cacheList.Clear();
                        startIndex = endIndex = -1;
                    }

                    // 앞쪽 확인
                    if (startIndex >= 0)
                    {
                        if (e.StartIndex < startIndex)
                        {
                            for (int i = startIndex - 1; i >= e.StartIndex; i--)
                            {
                                cacheList.Insert(0, SetListViewItem(i));
                            }
                        }
                        else if (e.StartIndex > startIndex)
                        {
                            cacheList.RemoveRange(0, e.StartIndex - startIndex);
                        }
                    }

                    // 시작위치 변경
                    startOffset = e.StartIndex;
                    if (startOffset > endIndex + 1)
                    {
                        endIndex = startOffset - 1;
                    }

                    // 뒤쪽 확인
                    if (e.EndIndex > endIndex)
                    {
                        for (int i = endIndex + 1; i <= e.EndIndex; i++)
                        {
                            cacheList.Add(SetListViewItem(i));
                        }
                    }
                    else if (e.EndIndex < endIndex)
                    {
                        int removeCount = endIndex - e.EndIndex;
                        cacheList.RemoveRange(cacheList.Count - removeCount, removeCount);
                    }
                }
            };
        }

        private ListViewItem SetListViewItem(int index)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add((model.bookmarkList[index] * PresenterManager.TimeConversionFactor).ToString("F2"));

            return lvi;
        }

        public override void Clear()
        {
            startOffset = -1;
            cacheList.Clear();
        }

        public override void Restart()
        {
            view.RefreshView();
        }

        public override void ModelClosing()
        {
            model = null;
        }

        public override void ModelCreated(Pt5Model pt5Model)
        {
            model = pt5Model;
        }

        public override void ModelStarted()
        {

        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            Clear();
            view.RefreshView();
        }

        public void UpdateTimeOffset(double offset)
        {
            //throw new NotImplementedException();
        }

        public void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks)
        {
            //throw new NotImplementedException();
        }

        public void UpdateCurrentOffset(double offset)
        {
            //throw new NotImplementedException();
        }

        public void UpdateDisplayFormat(bool isDisplayInTimeFormat)
        {
            //throw new NotImplementedException();
        }
    }
}
