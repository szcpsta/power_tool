
namespace Pt5Viewer.Views
{
    partial class StatisticsView
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelAverageCurrentValue = new System.Windows.Forms.Label();
            this.labelSamplesValue = new System.Windows.Forms.Label();
            this.labelTimeValue = new System.Windows.Forms.Label();
            this.labelCurrentUnit = new System.Windows.Forms.Label();
            this.labelTimeUnit = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelAverageCurrentValue);
            this.groupBox1.Controls.Add(this.labelSamplesValue);
            this.groupBox1.Controls.Add(this.labelTimeValue);
            this.groupBox1.Controls.Add(this.labelCurrentUnit);
            this.groupBox1.Controls.Add(this.labelTimeUnit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAPTURE STATS";
            // 
            // labelAverageCurrentValue
            // 
            this.labelAverageCurrentValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAverageCurrentValue.Location = new System.Drawing.Point(106, 65);
            this.labelAverageCurrentValue.Name = "labelAverageCurrentValue";
            this.labelAverageCurrentValue.Size = new System.Drawing.Size(80, 20);
            this.labelAverageCurrentValue.TabIndex = 7;
            this.labelAverageCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSamplesValue
            // 
            this.labelSamplesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSamplesValue.Location = new System.Drawing.Point(106, 45);
            this.labelSamplesValue.Name = "labelSamplesValue";
            this.labelSamplesValue.Size = new System.Drawing.Size(80, 20);
            this.labelSamplesValue.TabIndex = 6;
            this.labelSamplesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimeValue
            // 
            this.labelTimeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeValue.Location = new System.Drawing.Point(106, 25);
            this.labelTimeValue.Name = "labelTimeValue";
            this.labelTimeValue.Size = new System.Drawing.Size(80, 20);
            this.labelTimeValue.TabIndex = 5;
            this.labelTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCurrentUnit
            // 
            this.labelCurrentUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentUnit.Location = new System.Drawing.Point(190, 65);
            this.labelCurrentUnit.Name = "labelCurrentUnit";
            this.labelCurrentUnit.Size = new System.Drawing.Size(30, 20);
            this.labelCurrentUnit.TabIndex = 4;
            this.labelCurrentUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimeUnit
            // 
            this.labelTimeUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeUnit.Location = new System.Drawing.Point(190, 25);
            this.labelTimeUnit.Name = "labelTimeUnit";
            this.labelTimeUnit.Size = new System.Drawing.Size(30, 20);
            this.labelTimeUnit.TabIndex = 3;
            this.labelTimeUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Average Current";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Samples";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "TIme";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatisticsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "StatisticsView";
            this.Size = new System.Drawing.Size(230, 150);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelAverageCurrentValue;
        private System.Windows.Forms.Label labelSamplesValue;
        private System.Windows.Forms.Label labelTimeValue;
        private System.Windows.Forms.Label labelCurrentUnit;
        private System.Windows.Forms.Label labelTimeUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
