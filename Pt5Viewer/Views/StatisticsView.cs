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
    public partial class StatisticsView : UserControl, IStatisticsView
    {
        public string Title
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public string TimeUnit
        {
            get => labelTimeUnit.Text;
            set => labelTimeUnit.Text = value;
        }

        public string CurrentUnit
        {
            get => labelCurrentUnit.Text;
            set => labelCurrentUnit.Text = value;
        }

        public string TimeValue
        {
            get => labelTimeValue.Text;
            set => labelTimeValue.Text = value;
        }

        public string SamplesValue
        {
            get => labelSamplesValue.Text;
            set => labelSamplesValue.Text = value;
        }

        public string AverageCurrentValue
        {
            get => labelAverageCurrentValue.Text;
            set => labelAverageCurrentValue.Text = value;
        }

        public StatisticsView()
        {
            InitializeComponent();
        }
    }
}
