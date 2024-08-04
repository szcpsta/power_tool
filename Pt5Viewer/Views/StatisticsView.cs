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

        public StatisticsView()
        {
            InitializeComponent();

            labelTimeValue.Text = "-.--";
            labelSamplesValue.Text = "-.--";
            labelAverageCurrentValue.Text = "-.--";
        }
    }
}
