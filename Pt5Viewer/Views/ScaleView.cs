using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pt5Viewer.Views
{
    public partial class ScaleView : UserControl, IScaleView
    {
        public ScaleView()
        {
            InitializeComponent();
        }

        public string TimeUnit
        {
            get => comboBoxTimeUnit.SelectedItem?.ToString();
            set => comboBoxTimeUnit.SelectedItem = value;
        }

        public string TimeUnitsPerTick
        {
            get => comboBoxTimeUnitsPerTick.SelectedItem?.ToString();
            set => comboBoxTimeUnitsPerTick.SelectedItem = value;
        }

        public string TimeNumberOfTicks
        {
            get => comboBoxTimeNumberOfTicks.SelectedItem?.ToString();
            set => comboBoxTimeNumberOfTicks.SelectedItem = value;
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

        public void SetTimeUnitComboBoxItems(object[] items)
        {
            comboBoxTimeUnit.Items.AddRange(items);
        }

        public void SetTimeUnitsPerTickComboBoxItems(object[] items)
        {
            comboBoxTimeUnitsPerTick.Items.AddRange(items);
        }

        public void SetTimeNumberOfTicksComboBoxItems(object[] items)
        {
            comboBoxTimeNumberOfTicks.Items.AddRange(items);
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
