using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;
using Pt5Viewer.Models;
using Pt5Viewer.Views;

namespace Pt5Viewer.Presenters
{
    public class GraphPresenter : Presenter, IScaleSync
    {
        private IGraphView view;
        private Pt5Model model;

        private double XScaleMin;
        private double XScaleMax;

        public GraphPresenter(IGraphView graphView)
        {
            view = graphView;

            view.TimeOffsetChanged += (s, e) =>
            {
                double nextOffset = PresenterManager.TimeOffset + (e.Delta / 120 * -(int)PresenterManager.TimeUnitsPerTick / PresenterManager.TimeConversionFactor);
                PresenterManager.TimeOffsetChanged(nextOffset);
            };

            view.ScaleFormatEventTriggered += (s, e) =>
            {
                if (PresenterManager.IsDisplayInTimeFormat)
                {
                    view.XAxisFormattedLabel = $"{PresenterManager.GetDateTime(e.Val).ToString("HH:mm:ss.ffff")}";
                }
                else
                {
                    view.XAxisFormattedLabel = $"{e.Val * PresenterManager.TimeConversionFactor}";
                }
            };

            view.DisplayFormatChanged += (s, e) =>
            {
                PresenterManager.DisplayFormatChanged(e.IsDisplayInTimeFormat);
            };

            view.ScrollEventDone += (s, e) =>
            {
                PresenterManager.TimeOffsetChanged(e.Val);
            };
        }

        public void UpdateTimeScale(TimeUnitEnum unit, TimeUnitsPerTickEnum unitsPerTick, TimeNumberOfTicksEnum numberOfTicks)
        {
            XScaleMax = XScaleMin + (int)unitsPerTick / PresenterManager.TimeConversionFactor * (int)numberOfTicks;

            if (model.IsStarted)
            {
                view.ClearLineItem();

                long firstIndex = model.GetIndexFromTimestamp(XScaleMin);
                long lastIndex = model.GetIndexFromTimestamp(XScaleMax);
                for (long i = firstIndex; i <= lastIndex; i++)
                {
                    view.AddPoint(model.GetX(i), model.GetY(i));
                }
            }

            view.SetXAxisTitle($"Time({Util.GetEnumDescription(unit)})");
            view.SetXAxisScale((int)unitsPerTick / PresenterManager.TimeConversionFactor, (int)numberOfTicks);

            view.UpdateGraph();
        }

        public void UpdateTimeOffset(double offset)
        {
            double old_min = XScaleMin;
            double old_max = XScaleMax;

            double xSpan = XScaleMax - XScaleMin;

            XScaleMin = offset;
            XScaleMax = XScaleMin + xSpan;

            double new_min = XScaleMin;
            double new_max = XScaleMax;

            double delta = Math.Abs(old_min - new_min);

            if (model.IsStarted)
            {
                if (xSpan <= delta)
                {
                    view.ClearLineItem();

                    long firstIndex = model.GetIndexFromTimestamp(new_min);
                    long lastIndex = model.GetIndexFromTimestamp(new_max);
                    for (long i = firstIndex; i <= lastIndex; i++)
                    {
                        view.AddPoint(model.GetX(i), model.GetY(i));
                    }
                }
                else if (old_min < new_min)
                {
                    long firstIndex = model.GetIndexFromTimestamp(new_min);
                    long lastIndex = model.GetIndexFromTimestamp(new_max);

                    long old_firstIndex = model.GetIndexFromTimestamp(old_min);
                    long old_lastIndex = model.GetIndexFromTimestamp(old_max);

                    view.RemoveRange(0, (int)(firstIndex - old_firstIndex));
                    for (long i = old_lastIndex + 1; i <= lastIndex; i++)
                    {
                        view.AddPoint(model.GetX(i), model.GetY(i));
                    }
                }
                else
                {
                    long firstIndex = model.GetIndexFromTimestamp(new_min);
                    long lastIndex = model.GetIndexFromTimestamp(new_max);

                    long old_firstIndex = model.GetIndexFromTimestamp(old_min);
                    long old_lastIndex = model.GetIndexFromTimestamp(old_max);

                    for (long i = old_firstIndex - 1; i >= firstIndex; i--)
                    {
                        view.InsertPoint(0, model.GetX(i), model.GetY(i));
                    }

                    view.RemoveRange((int)(lastIndex - firstIndex + 1), (int)(old_lastIndex - lastIndex));
                }
            }

            view.SetXAxisOffset(offset);

            view.UpdateGraph();
        }

        public void UpdateCurrentScale(string unit, double unitsPerTick, int numberOfTicks)
        {
            view.SetYAxisTitle($"{unit}");
            view.SetYAxisScale(unitsPerTick, numberOfTicks);

            view.UpdateGraph();
        }

        public void UpdateCurrentOffset(double offset)
        {
            view.SetYAxisOffset(offset);

            view.UpdateGraph();
        }

        public void UpdateDisplayFormat(bool isDisplayInTimeFormat)
        {
            view.UpdateGraph();
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override void Restart()
        {
            base.Restart();
        }
        public override void ModelClosing()
        {
            view.Clear();
            model = null;
        }

        public override void ModelCreated(Pt5Model pt5Model)
        {
            model = pt5Model;
        }

        public override void ModelStarted()
        {
            view.LoadLineItem(model.TimeScaleMax);

            long firstIndex = model.GetIndexFromTimestamp(XScaleMin);
            long lastIndex = model.GetIndexFromTimestamp(XScaleMax);
            for (long i = firstIndex; i<=lastIndex; i++)
            {
                view.AddPoint(model.GetX(i), model.GetY(i));
            }

            base.ModelStarted();
        }
    }
}
