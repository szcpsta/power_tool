﻿
namespace Pt5Viewer
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.graphView = new Pt5Viewer.Views.GraphView();
            this.scaleView = new Pt5Viewer.Views.ScaleView();
            this.statisticsView = new Pt5Viewer.Views.StatisticsView();
            this.bookmarkView = new Pt5Viewer.Views.BookmarkView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer1
            //
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.bookmarkView);
            this.splitContainer1.Panel2.Controls.Add(this.statisticsView);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 729);
            this.splitContainer1.SplitterDistance = 766;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            //
            // splitContainer2
            //
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer2.Panel1
            //
            this.splitContainer2.Panel1.Controls.Add(this.graphView);
            //
            // splitContainer2.Panel2
            //
            this.splitContainer2.Panel2.Controls.Add(this.scaleView);
            this.splitContainer2.Size = new System.Drawing.Size(766, 729);
            this.splitContainer2.SplitterDistance = 541;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            //
            // graphView
            //
            this.graphView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphView.IsEnableHPan = false;
            this.graphView.IsEnableHZoom = false;
            this.graphView.IsEnableVPan = false;
            this.graphView.IsEnableVZoom = false;
            this.graphView.IsShowHScrollBar = true;
            this.graphView.Location = new System.Drawing.Point(0, 0);
            this.graphView.Name = "graphView";
            this.graphView.ScrollGrace = 0D;
            this.graphView.ScrollMaxX = 60000D;
            this.graphView.ScrollMaxY = 0D;
            this.graphView.ScrollMaxY2 = 0D;
            this.graphView.ScrollMinX = 0D;
            this.graphView.ScrollMinY = 0D;
            this.graphView.ScrollMinY2 = 0D;
            this.graphView.Size = new System.Drawing.Size(762, 537);
            this.graphView.TabIndex = 0;
            this.graphView.XAxisFormattedLabel = null;
            //
            // scaleView
            //
            this.scaleView.CurrentNumberOfTicks = null;
            this.scaleView.CurrentOffset = "";
            this.scaleView.CurrentUnit = null;
            this.scaleView.CurrentUnitsPerTick = null;
            this.scaleView.Location = new System.Drawing.Point(3, 3);
            this.scaleView.Name = "scaleView";
            this.scaleView.Size = new System.Drawing.Size(380, 100);
            this.scaleView.TabIndex = 0;
            this.scaleView.TimeOffset = "";
            //
            // statisticsView
            //
            this.statisticsView.AverageCurrentValue = "";
            this.statisticsView.CurrentUnit = "mA";
            this.statisticsView.Location = new System.Drawing.Point(4, 10);
            this.statisticsView.Name = "statisticsView";
            this.statisticsView.SamplesValue = "";
            this.statisticsView.Size = new System.Drawing.Size(230, 150);
            this.statisticsView.TabIndex = 0;
            this.statisticsView.TimeUnit = "ms";
            this.statisticsView.TimeValue = "";
            this.statisticsView.Title = "CAPTURE STATS";
            //
            // bookmarkView
            //
            this.bookmarkView.Location = new System.Drawing.Point(3, 166);
            this.bookmarkView.Name = "bookmarkView";
            this.bookmarkView.Size = new System.Drawing.Size(230, 150);
            this.bookmarkView.TabIndex = 1;
            //
            // MainForm
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Views.ScaleView scaleView;
        private Views.GraphView graphView;
        private Views.StatisticsView statisticsView;
        private Views.BookmarkView bookmarkView;
    }
}

