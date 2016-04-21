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
        {
            InitializeComponent();
            this.loanReportObj = loanReportObj;
            loanReportObj.LoanReportDataGrid = gridLoanCalculation;
            loanReportObj.LoanReportDataGrid.ItemsSource = null;

            
        }

        private void gridLoanCalculation_Loaded(object sender, RoutedEventArgs e)
        {
         

            if (gridLoanCalculation.ItemsSource != null)
            {
                DataView viewReport = loanReportObj.ReportScope.AsDataView();
                gridLoanCalculation.ItemsSource = null;
                gridLoanCalculation.ItemsSource = viewReport;
                gridLoanCalculation.AutoGenerateColumns = true;

                gridLoanCalculation.Columns[0].Header = "Start Date";
                ((DataGridTextColumn)gridLoanCalculation.Columns[0]).Binding.StringFormat = "MM/dd/yyyy";
                gridLoanCalculation.Columns[1].Header = "Principal \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[1]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[2].Header = "Interest Rate";
                ((DataGridTextColumn)gridLoanCalculation.Columns[2]).Binding.StringFormat = "p2";
                gridLoanCalculation.Columns[3].Header = "Daily Interest \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[3]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[4].Header = "Interest Balance\n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[4]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[5].Header = "Cumulative Interest \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[5]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[6].Header = "Total Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[6]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[7].Header = "Interest Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[7]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[8].Header = "Principal Payment \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[8]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[9].Header = "Current Balance \n(" + loanReportObj.ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)gridLoanCalculation.Columns[9]).Binding.StringFormat = "N";
                gridLoanCalculation.Columns[10].Header = "Comments";

                gridLoanCalculation.Visibility = Visibility.Visible;
            }
        }
    }
}
