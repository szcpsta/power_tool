using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Common;
using Pt5Viewer.Enums;

namespace Pt5Viewer.Views
{
    public partial class PowerToolScaleView : DockForm
    {
        public PowerToolScaleView()
        {
            InitializeComponent();

            comboBoxTimeUnit.DataSource = Enum.GetValues(typeof(TimeUnitEnum))
                                                .Cast<TimeUnitEnum>()
                                                .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                .ToList();
            comboBoxTimeUnit.DisplayMember = "Description";
            comboBoxTimeUnit.ValueMember = "Value";

            comboBoxTimeUnitsPerTick.DataSource = Enum.GetValues(typeof(TimeUnitsPerTickEnum))
                                                        .Cast<TimeUnitsPerTickEnum>()
                                                        .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                        .ToList();
            comboBoxTimeUnitsPerTick.DisplayMember = "Description";
            comboBoxTimeUnitsPerTick.ValueMember = "Value";

            comboBoxTimeNumberOfTicks.DataSource = Enum.GetValues(typeof(TimeNumberOfTicksEnum))
                                                        .Cast<TimeNumberOfTicksEnum>()
                                                        .Select(e => new { Value = e, Description = Util.GetEnumDescription(e) })
                                                        .ToList();
            comboBoxTimeNumberOfTicks.DisplayMember = "Description";
            comboBoxTimeNumberOfTicks.ValueMember = "Value";

            comboBoxCurrentUnit.Items.AddRange(Constant.CurrentUnitList.ToArray());
            comboBoxCurrentUnitsPerTick.Items.AddRange(Constant.CurrentUnitsPerTickList.ToArray());
            comboBoxCurrentNumberOfTicks.Items.AddRange(Constant.CurrentNumberOfTicksList.ToArray());
        }

        public TimeUnitEnum TimeUnit
        {
            get => (TimeUnitEnum)comboBoxTimeUnit.SelectedValue;
            set => comboBoxTimeUnit.SelectedValue = value;
        }

        public TimeUnitsPerTickEnum TimeUnitsPerTick
        {
            get => (TimeUnitsPerTickEnum)comboBoxTimeUnitsPerTick.SelectedValue;
            set => comboBoxTimeUnitsPerTick.SelectedValue = value;
        }

        public TimeNumberOfTicksEnum TimeNumberOfTicks
        {
            get => (TimeNumberOfTicksEnum)comboBoxTimeNumberOfTicks.SelectedValue;
            set => comboBoxTimeNumberOfTicks.SelectedValue = value;
        }

        public string TimeOffset
        {
            get => textBoxTimeOffset.Text;
            set => textBoxTimeOffset.Text = value;
        }

        public string CurrentUnit
        {
            get => comboBoxCurrentUnit.SelectedItem?.ToString();
            set => comboBoxCurrentUnit.SelectedItem = value;
        }

        public string CurrentUnitsPerTick
        {
            get => comboBoxCurrentUnitsPerTick.SelectedItem?.ToString();
            set => comboBoxCurrentUnitsPerTick.SelectedItem = value;
        }

        public string CurrentNumberOfTicks
        {
            get => comboBoxCurrentNumberOfTicks.SelectedItem?.ToString();
            set => comboBoxCurrentNumberOfTicks.SelectedItem = value;
        }

        public string CurrentOffset
        {
            get => textBoxCurrentOffset.Text;
            set => textBoxCurrentOffset.Text = value;
        }

        public event EventHandler TimeScaleChanged;
        public event EventHandler TimeOffsetChanged;

        public event EventHandler CurrentScaleChanged;
        public event EventHandler CurrentOffsetChanged;

        private void comboBoxTimeScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxCurrentScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentScaleChanged?.Invoke(sender, e);
        }

        private void textBoxTimeOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimeOffsetChanged?.Invoke(sender, e);
            }
        }

        private void textBoxCurrentOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentOffsetChanged?.Invoke(sender, e);
            }
        }
    }
}
