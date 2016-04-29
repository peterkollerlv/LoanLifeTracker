using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterestTracker
{
    /// <summary>
    /// Interaction logic for LoanCalculation.xaml
    /// </summary>
    public partial class LoanCalculation : Page
    {
        LoanReportData loanReportObj;

  

        public LoanCalculation(LoanReportData loanReportObj)
        //public LoanCalculation()
        {
            InitializeComponent();
            this.loanReportObj = loanReportObj;
            loanReportObj.LoanReportDataGrid = GridLoanCalclation;
            
            // loanReportObj.LoanReportDataGrid.ItemsSource = null;


        }

        public DataGrid GridLoanCalclation
        {
            get
            {
                return gridLoanCalculation;
            }
            
        }

        public LoanReportData LoanReportObj
        {
            get
            {
                return loanReportObj;
            }

            set
            {
                loanReportObj = value;
            }
        }

        private void gridLoanCalculation_Loaded(object sender, RoutedEventArgs e)
        {
           FormatGrid();
        }

        internal void FormatGrid()
        {
            gridLoanCalculation.DataContext = LoanReportObj;

              GridLoanCalclation.Columns.Clear();
            //  loanReportObj.LoanReportDataGrid = GridLoanCalclation;
            DataView viewReport;
            if (loanReportObj.ReportScope != null)
            {
                viewReport = loanReportObj.ReportScope.AsDataView();
                gridLoanCalculation.ItemsSource = null;
                gridLoanCalculation.ItemsSource = viewReport;
            }

            //gridLoanCalculation.ItemsSource = viewReport;


            if (gridLoanCalculation.ItemsSource != null)
            {
               // gridLoanCalculation.AutoGenerateColumns = true;
                //  if (((DataGridTextColumn)gridLoanCalculation.Columns[0]).Binding.StringFormat != "MM/dd/yyyy")
                //  {
              DataGridTextColumn startDate = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(startDate);
                gridLoanCalculation.Columns[0].Header = "Start Date";
                ((DataGridTextColumn)gridLoanCalculation.Columns[0]).Binding = new Binding("loanDayDate");
                ((DataGridTextColumn)gridLoanCalculation.Columns[0]).Binding.StringFormat = "MM/dd/yyyy";

                DataGridTextColumn principal = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(principal);
                gridLoanCalculation.Columns[1].Header = "Principal \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[1]).Binding = new Binding("loanDayPrincipal");
                ((DataGridTextColumn)gridLoanCalculation.Columns[1]).Binding.StringFormat = "N";

                DataGridTextColumn interestRate = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(interestRate);
                gridLoanCalculation.Columns[2].Header = "Interest Rate";
                ((DataGridTextColumn)gridLoanCalculation.Columns[2]).Binding = new Binding("loanDayInterestRate");
                ((DataGridTextColumn)gridLoanCalculation.Columns[2]).Binding.StringFormat = "p2";

                DataGridTextColumn dailyInterest = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(dailyInterest);
                gridLoanCalculation.Columns[3].Header = "Daily Interest \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[3]).Binding = new Binding("loanDayInterest");
                ((DataGridTextColumn)gridLoanCalculation.Columns[3]).Binding.StringFormat = "N";

                DataGridTextColumn interestBalance = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(interestBalance);
                gridLoanCalculation.Columns[4].Header = "Interest Balance\n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[4]).Binding = new Binding("loanInterestBalance");
                ((DataGridTextColumn)gridLoanCalculation.Columns[4]).Binding.StringFormat = "N";

                DataGridTextColumn cumultativeInterest = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(cumultativeInterest);
                gridLoanCalculation.Columns[5].Header = "Cumulative Interest \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[5]).Binding = new Binding("loanDayCuIntrestBal");
                ((DataGridTextColumn)gridLoanCalculation.Columns[5]).Binding.StringFormat = "N";

                DataGridTextColumn totalPayment = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(totalPayment);
                gridLoanCalculation.Columns[6].Header = "Total Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[6]).Binding = new Binding("loanDayTotalPayment");
                ((DataGridTextColumn)gridLoanCalculation.Columns[6]).Binding.StringFormat = "N";

                DataGridTextColumn interestPayment = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(interestPayment);
                gridLoanCalculation.Columns[7].Header = "Interest Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[7]).Binding = new Binding("loanDayInterestPayment");
                ((DataGridTextColumn)gridLoanCalculation.Columns[7]).Binding.StringFormat = "N";

                DataGridTextColumn principalPayment = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(principalPayment);
                gridLoanCalculation.Columns[8].Header = "Principal Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[8]).Binding = new Binding("loanDayPrincipalPayment");
                ((DataGridTextColumn)gridLoanCalculation.Columns[8]).Binding.StringFormat = "N";

                    DataGridTextColumn currentBalance = new DataGridTextColumn();
                gridLoanCalculation.Columns.Add(currentBalance);
                gridLoanCalculation.Columns[9].Header = "Current Balance \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[9]).Binding = new Binding("loanDayCurrentBalance");
                ((DataGridTextColumn)gridLoanCalculation.Columns[9]).Binding.StringFormat = "N";

    

              //  gridLoanCalculation.Columns[10].Header = "Comments";

                gridLoanCalculation.Visibility = Visibility.Visible;
                //   }
            }
        }

        private void gridLoanCalculation_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
