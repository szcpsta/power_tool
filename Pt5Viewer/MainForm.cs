﻿using System;
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

using Pt5Viewer.Presenters;

namespace Pt5Viewer
{
    public partial class MainForm : Form
    {
        PresenterManager presenterManager;

        ScalePresenter scalePresenter;
        GraphPresenter graphPresenter;
        StatisticsPresenter statisticsPresenter;

        public MainForm()
        {
            InitializeComponent();

            SetTitle();

            presenterManager = new PresenterManager();

            scalePresenter = new ScalePresenter(scaleView);
            presenterManager.AddPresenter(scalePresenter);

            graphPresenter = new GraphPresenter(graphView);
            presenterManager.AddPresenter(graphPresenter);

            statisticsPresenter = new StatisticsPresenter(statisticsView);
            presenterManager.AddPresenter(statisticsPresenter);
        }

        private void SetTitle()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;

            string title = $"[{Id}] {Application.ProductName} v{version.Major}.{version.Minor}.{version.Build}";
            if (version.Revision != 0)
            {
                title = $"{title}.{version.Revision}";
            }

            Text = title;
        }

        public static int Id
        {
            get
            {
                string processName = Process.GetCurrentProcess().ProcessName;

                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 1)
                {
                    return Process.GetCurrentProcess().Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
    }
}
