using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanLifeTrackerV00
{
    public partial class LoanReportDataView : Form
    {
        public LoanReportDataView()
        {
            InitializeComponent();
        }
      

         public string loanReportDataLabelValue
        {
            get
            {
                return loanReportDataLabel.Text;
            }
            set
            {
                loanReportDataLabel.Text = value;
            }
        }
    }
}
