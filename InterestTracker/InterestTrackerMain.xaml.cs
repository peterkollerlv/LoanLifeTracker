using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for InterestTrackerMain.xaml
    /// </summary>
    public partial class InterestTrackerMain : Window
    {

        //loan properties
        LoanReportData LoanReportDataObj;

        public InterestTrackerMain()
        {
            
            LoanReportDataObj = new LoanReportData();
            LoanReportDataObj.createNewLoan();
            InitializeComponent();
            LoanReportDataObj.LoanReportDataGrid = gridLoanCalculation;
            
        }
        public string LoanTitle { get { return LoanReportDataObj.Title; } set { inputLoanTitle.Text = value; LoanReportDataObj.Title = value; } }

        //ui events

        private void navLoanData_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanData;
        }

        private void navMainLoanData_GotFocus(object sender, RoutedEventArgs e)
        {
            navTabControl.SelectedItem = navLoanData;
        }

  


        public static void FilterKeypressToDigits(object sender, TextCompositionEventArgs e)
        {
            //if (!char.IsControl(e.Key) && ((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Back) // (char)KeyInterop.VirtualKeyFromKey(e.Key) != '.') //&& (sender as System.Windows.Controls.TextBox)sender.Text.IndexOf('.') > -1
            //{


            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
            //  }
        }

        private void inputLoanDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputLoanDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoanReportDataObj.LoanDuration = Int32.Parse(inputLoanDuration.Text);
        }

        private void inputLoanTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelTitle.Content = inputLoanTitle.Text;
            LoanReportDataObj.Title = inputLoanTitle.Text;
        }

        private void inputCalculateLoan_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                LoanReportDataObj.CalculateLoan();
            }
        }

        private void inputNewLoan_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.createNewLoan();
        }

        private void inputLoanStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                LoanReportDataObj.StartDate = inputLoanStartDate.DisplayDate.Date;
            }
        }

        private void navLoanConfiguration_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanCalculation;
        }

        private void navLoanReports_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanCalculation;
        }

        private void inputInitialLoanAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputInitialLoanAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoanReportDataObj.InitialLoanAmount = Int32.Parse(inputInitialLoanAmount.Text);
        }

        private void inputInterestRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputInterestRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoanReportDataObj.InterestRate = Decimal.Parse(inputInterestRate.Text);

        }

        private void inputInterestPenaltyChk_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.HasInterestPenalty = (bool)inputInterestPenaltyChk.IsChecked;
        }
    }
}
