using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanLifeTracker
{
    public partial class DatabaseLookup : Form
    {
        private LoanReportData loanReportDataObj;
        private LoanReportMain loanReportMain;
        private DataTable existingLoans;

        public DatabaseLookup()
        {
            
          //  populateDataGrid();
            InitializeComponent();
            DatabaseConnection dataBaseConnection = new DatabaseConnection();
            DataTable existingLoans = dataBaseConnection.ExistingLoans;
            gridExistingLoans.DataSource = existingLoans;
        }



        //public DatabaseLookup(DataTable existingLoans)
        //{
        //    //this.loanReportMain = loanReportMain;
        //    //this.loanReportDataObj = loanReportDataObj;
        //    this.existingLoans = existingLoans;
           
        //}

        //private void populateDataGrid()
        //{
           

        //}

    }
}
