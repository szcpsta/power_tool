
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelThreadInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphView = new Pt5Viewer.Views.GraphView();
            this.scaleView = new Pt5Viewer.Views.ScaleView();
            this.bookmarkView = new Pt5Viewer.Views.BookmarkView();
            this.statisticsView = new Pt5Viewer.Views.StatisticsView();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
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
            // menuStrip
            //
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            //
            // toolStrip
            //
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            //
            // statusStrip
            //
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelThreadInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 707);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            //
            // toolStripStatusLabelThreadInfo
            //
            this.toolStripStatusLabelThreadInfo.Name = "toolStripStatusLabelThreadInfo";
            this.toolStripStatusLabelThreadInfo.Size = new System.Drawing.Size(0, 17);
            //
            // splitContainer1
            //
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
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
            this.splitContainer1.Size = new System.Drawing.Size(1008, 658);
            this.splitContainer1.SplitterDistance = 781;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
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
            this.splitContainer2.Size = new System.Drawing.Size(781, 658);
            this.splitContainer2.SplitterDistance = 485;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            //
            // timer
            //
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            //
            // viewToolStripMenuItem
            //
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.powerToolToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.viewToolStripMenuItem.Text = "View";
            //
            // powerToolToolStripMenuItem
            //
            this.powerToolToolStripMenuItem.Name = "powerToolToolStripMenuItem";
            this.powerToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.powerToolToolStripMenuItem.Text = "PowerTool";
            this.powerToolToolStripMenuItem.Click += new System.EventHandler(this.powerToolToolStripMenuItem_Click);
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
            this.graphView.Size = new System.Drawing.Size(777, 481);
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
            // bookmarkView
            //
            this.bookmarkView.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bookmarkView.Location = new System.Drawing.Point(3, 166);
            this.bookmarkView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.bookmarkView.Name = "bookmarkView";
            this.bookmarkView.Size = new System.Drawing.Size(230, 150);
            this.bookmarkView.TabIndex = 1;
            this.bookmarkView.VirtualListSize = 0;
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
            // MainForm
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Views.GraphView graphView;
        private Views.ScaleView scaleView;
        private Views.BookmarkView bookmarkView;
        private Views.StatisticsView statisticsView;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelThreadInfo;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolToolStripMenuItem;
    }
}

