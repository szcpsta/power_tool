
namespace Pt5Viewer.Views
{
    partial class ScaleView
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
            this.textBoxCurrentOffset = new System.Windows.Forms.TextBox();
            this.comboBoxCurrentNumberOfTicks = new System.Windows.Forms.ComboBox();
            this.comboBoxCurrentUnitsPerTick = new System.Windows.Forms.ComboBox();
            this.comboBoxCurrentUnit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTimeOffset = new System.Windows.Forms.TextBox();
            this.comboBoxTimeNumberOfTicks = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeUnitsPerTick = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCurrentOffset);
            this.groupBox1.Controls.Add(this.comboBoxCurrentNumberOfTicks);
            this.groupBox1.Controls.Add(this.comboBoxCurrentUnitsPerTick);
            this.groupBox1.Controls.Add(this.comboBoxCurrentUnit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxTimeOffset);
            this.groupBox1.Controls.Add(this.comboBoxTimeNumberOfTicks);
            this.groupBox1.Controls.Add(this.comboBoxTimeUnitsPerTick);
            this.groupBox1.Controls.Add(this.comboBoxTimeUnit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GRAPH SCALE";
            // 
            // textBoxCurrentOffset
            // 
            this.textBoxCurrentOffset.Location = new System.Drawing.Point(290, 69);
            this.textBoxCurrentOffset.Name = "textBoxCurrentOffset";
            this.textBoxCurrentOffset.Size = new System.Drawing.Size(80, 21);
            this.textBoxCurrentOffset.TabIndex = 13;
            this.textBoxCurrentOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCurrentOffset_KeyDown);
            // 
            // comboBoxCurrentNumberOfTicks
            // 
            this.comboBoxCurrentNumberOfTicks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrentNumberOfTicks.FormattingEnabled = true;
            this.comboBoxCurrentNumberOfTicks.Location = new System.Drawing.Point(220, 70);
            this.comboBoxCurrentNumberOfTicks.Name = "comboBoxCurrentNumberOfTicks";
            this.comboBoxCurrentNumberOfTicks.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCurrentNumberOfTicks.TabIndex = 12;
            this.comboBoxCurrentNumberOfTicks.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrentNumberOfTicks_SelectedIndexChanged);
            // 
            // comboBoxCurrentUnitsPerTick
            // 
            this.comboBoxCurrentUnitsPerTick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrentUnitsPerTick.FormattingEnabled = true;
            this.comboBoxCurrentUnitsPerTick.Location = new System.Drawing.Point(150, 70);
            this.comboBoxCurrentUnitsPerTick.Name = "comboBoxCurrentUnitsPerTick";
            this.comboBoxCurrentUnitsPerTick.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCurrentUnitsPerTick.TabIndex = 11;
            this.comboBoxCurrentUnitsPerTick.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrentUnitsPerTick_SelectedIndexChanged);
            // 
            // comboBoxCurrentUnit
            // 
            this.comboBoxCurrentUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrentUnit.FormattingEnabled = true;
            this.comboBoxCurrentUnit.Location = new System.Drawing.Point(80, 70);
            this.comboBoxCurrentUnit.Name = "comboBoxCurrentUnit";
            this.comboBoxCurrentUnit.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCurrentUnit.TabIndex = 10;
            this.comboBoxCurrentUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrentUnit_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(290, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Offset";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(220, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "#Ticks";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(150, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Units/tick";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Unit";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTimeOffset
            // 
            this.textBoxTimeOffset.Location = new System.Drawing.Point(290, 39);
            this.textBoxTimeOffset.Name = "textBoxTimeOffset";
            this.textBoxTimeOffset.Size = new System.Drawing.Size(80, 21);
            this.textBoxTimeOffset.TabIndex = 5;
            this.textBoxTimeOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTimeOffset_KeyDown);
            // 
            // comboBoxTimeNumberOfTicks
            // 
            this.comboBoxTimeNumberOfTicks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeNumberOfTicks.FormattingEnabled = true;
            this.comboBoxTimeNumberOfTicks.Location = new System.Drawing.Point(220, 40);
            this.comboBoxTimeNumberOfTicks.Name = "comboBoxTimeNumberOfTicks";
            this.comboBoxTimeNumberOfTicks.Size = new System.Drawing.Size(60, 20);
            this.comboBoxTimeNumberOfTicks.TabIndex = 4;
            this.comboBoxTimeNumberOfTicks.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeNumberOfTicks_SelectedIndexChanged);
            // 
            // comboBoxTimeUnitsPerTick
            // 
            this.comboBoxTimeUnitsPerTick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeUnitsPerTick.FormattingEnabled = true;
            this.comboBoxTimeUnitsPerTick.Location = new System.Drawing.Point(150, 40);
            this.comboBoxTimeUnitsPerTick.Name = "comboBoxTimeUnitsPerTick";
            this.comboBoxTimeUnitsPerTick.Size = new System.Drawing.Size(60, 20);
            this.comboBoxTimeUnitsPerTick.TabIndex = 3;
            this.comboBoxTimeUnitsPerTick.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeUnitsPerTick_SelectedIndexChanged);
            // 
            // comboBoxTimeUnit
            // 
            this.comboBoxTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimeUnit.FormattingEnabled = true;
            this.comboBoxTimeUnit.Location = new System.Drawing.Point(80, 40);
            this.comboBoxTimeUnit.Name = "comboBoxTimeUnit";
            this.comboBoxTimeUnit.Size = new System.Drawing.Size(60, 20);
            this.comboBoxTimeUnit.TabIndex = 2;
            this.comboBoxTimeUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxTimeUnit_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(10, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ScaleView";
            this.Size = new System.Drawing.Size(380, 100);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTimeOffset;
        private System.Windows.Forms.ComboBox comboBoxTimeNumberOfTicks;
        private System.Windows.Forms.ComboBox comboBoxTimeUnitsPerTick;
        private System.Windows.Forms.ComboBox comboBoxTimeUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCurrentOffset;
        private System.Windows.Forms.ComboBox comboBoxCurrentNumberOfTicks;
        private System.Windows.Forms.ComboBox comboBoxCurrentUnitsPerTick;
        private System.Windows.Forms.ComboBox comboBoxCurrentUnit;
    }
}
