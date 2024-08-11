using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pt5Viewer.Enums;

namespace Pt5Viewer.Views
{
    public partial class ScaleView : UserControl, IScaleView
    {
        public ScaleView()
        {
            InitializeComponent();
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

        public void SetTimeUnitComboBoxItems(IEnumerable<object> items)
        {
            comboBoxTimeUnit.DataSource = items;
            comboBoxTimeUnit.DisplayMember = "Description";
            comboBoxTimeUnit.ValueMember = "Value";
        }

        public void SetTimeUnitsPerTickComboBoxItems(IEnumerable<object> items)
        {
            comboBoxTimeUnitsPerTick.DataSource = items;
            comboBoxTimeUnitsPerTick.DisplayMember = "Description";
            comboBoxTimeUnitsPerTick.ValueMember = "Value";
        }

        public void SetTimeNumberOfTicksComboBoxItems(IEnumerable<object> items)
        {
            comboBoxTimeNumberOfTicks.DataSource = items;
            comboBoxTimeNumberOfTicks.DisplayMember = "Description";
            comboBoxTimeNumberOfTicks.ValueMember = "Value";
        }

        public void SetCurrentUnitComboBoxItems(object[] items)
        {
            comboBoxCurrentUnit.Items.AddRange(items);
        }

        public void SetCurrentUnitsPerTickComboBoxItems(object[] items)
        {
            comboBoxCurrentUnitsPerTick.Items.AddRange(items);
        }

        public void SetCurrentNumberOfTicksComboBoxItems(object[] items)
        {
            comboBoxCurrentNumberOfTicks.Items.AddRange(items);
        }

        private void comboBoxTimeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxTimeUnitsPerTick_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxTimeNumberOfTicks_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxCurrentUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxCurrentUnitsPerTick_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentScaleChanged?.Invoke(sender, e);
        }

        private void comboBoxCurrentNumberOfTicks_SelectedIndexChanged(object sender, EventArgs e)
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
