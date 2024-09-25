using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Common;
using Pt5Viewer.Configuration.Preferences;
using Pt5Viewer.Presenters;
using Pt5Viewer.Views;

namespace Pt5Viewer
{
    public partial class MainForm : Form
    {
        PresenterManager presenterManager;

        ScalePresenter scalePresenter;
        GraphPresenter graphPresenter;
        StatisticsPresenter statisticsPresenter;
        BookmarkPresenter bookmarkPresenter;

        public MainForm()
        {
            Log.Info($"Program started. Process ID: {Process.GetCurrentProcess().Id}");

            InitializeComponent();

            SetTitle();

            presenterManager = new PresenterManager();

            scalePresenter = new ScalePresenter(scaleView);
            presenterManager.AddPresenter(scalePresenter);

            graphPresenter = new GraphPresenter(graphView);
            presenterManager.AddPresenter(graphPresenter);

            statisticsPresenter = new StatisticsPresenter(statisticsView);
            presenterManager.AddPresenter(statisticsPresenter);

            bookmarkPresenter = new BookmarkPresenter(bookmarkView);
            presenterManager.AddPresenter(bookmarkPresenter);

            graphPresenter.SelectionRangeChanged += (s, e) =>
            {
                if (e == null)
                {
                    statisticsPresenter.UpdateStats();
                }
                else
                {
                    statisticsPresenter.UpdateStats(e.Time, e.Samples, e.AverageCurrent);
                }
            };
        }

        private void SetTitle()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;

            string title = $"[{Constant.Id}] {Application.ProductName} v{version.Major}.{version.Minor}.{version.Build}";
            if (version.Revision != 0)
            {
                title = $"{title}.{version.Revision}";
            }

            Text = title;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
            presenterManager.Start();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                presenterManager.Start(files[0]);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == PreferencesControl.ShortcutKeys[ShortcutKeysTag.Close])
            {
                Dispose();
                Close();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            ProcessThreadCollection ptc = proc.Threads;

            toolStripStatusLabelThreadInfo.Text = $"#Thread : {ptc.Count}";
        }

        private void powerToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerToolView powerToolView = new PowerToolView();
            powerToolView.Show();
        }
    }
}
