﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Objects;
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
        LoanCalculation loanCalculation;
        LoanPayments loanPayments;
        LoanDrawDown loanDrawDown;
        DatabaseConnection dbConnection;

        CollectionViewSource loanReportDataViewSource;

        private string tableNames;

        public InterestTrackerMain()
        {

            LoanReportDataObj = new LoanReportData();
            LoanReportDataObj.createNewLoan();
            dbConnection = new DatabaseConnection();
            InitializeComponent();

            inputLoanStartDate.SelectedDate = DateTime.Now.Date;
            inputInterestPenaltyStart.SelectedDate = DateTime.Now.Date;

            //loanCalculation = new LoanCalculation(LoanReportDataObj);
            //loanPayments = new LoanPayments(LoanReportDataObj);
            //loanDrawDown = new LoanDrawDown();
            loanDetails.Content = loanCalculation;


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

        private void navLoanConfiguration_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanDetails;
        }

        private void navLoanReports_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanDetails;
            loanDetails.Content = loanCalculation;
        }

        private void inputCalculateLoan_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                loanDetails.Content = new LoanCalculation(LoanReportDataObj);
                LoanReportDataObj.CalculateLoan();
            }
        }

        private void inputNewLoan_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.createNewLoan();
        }

        private void inputInterestPenaltyChk_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.HasInterestPenalty = (bool)inputInterestPenaltyChk.IsChecked;
        }

        private void interestStructureChecked_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            LoanReportDataObj.InterestStructureSelection = radioButton.Name.ToString();
            labelInterestStructure.Content = "Interest Structure: " + radioButton.Tag.ToString();
            // MessageBox.Show(LoanReportDataObj.InterestStructureSelection);
        }

        private void inputInterestPenaltyStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.InterestPenaltyDate = inputInterestPenaltyStart.SelectedDate.Value.Date;
        }

        private void inputLoanStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                LoanReportDataObj.StartDate = inputLoanStartDate.DisplayDate.Date;
                inputReportStartDate.SelectedDate = inputLoanStartDate.SelectedDate;
                inputReportStartDate.DisplayDateStart = inputLoanStartDate.SelectedDate;
                inputReportEndDate.SelectedDate = inputLoanStartDate.SelectedDate.Value.AddYears(Int32.Parse(inputLoanDuration.Text));
            }
            labelLoanStartDate.Content = "Loan Start Date: " + inputLoanStartDate.SelectedDate.Value.ToLongDateString();
        }

        private void inputCurrencySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.Currency = ((ComboBoxItem)inputCurrencySelection.SelectedItem).Content.ToString();
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.CalculateLoan();
            }
            //MessageBox.Show(LoanReportDataObj.Currency);
        }

        private void inputReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.ReportType = inputReportType.SelectedIndex;
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.CalculateLoan();
            }

        }

        public static void FilterKeypressToDigits(object sender, TextCompositionEventArgs e)
        {
            //if (!char.IsControl(e.Key) && ((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Back) // (char)KeyInterop.VirtualKeyFromKey(e.Key) != '.') //&& (sender as System.Windows.Controls.TextBox)sender.Text.IndexOf('.') > -1
            //{
            if (e.Text == " ")
            {
                e.Handled = true;
            }

            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
            //  }
        }

        private void inputLoanDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Contains("."))
            {
                e.Handled = true;
            }
            else
            {
                FilterKeypressToDigits(sender, e);
            }

        }

        private void inputLoanDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputLoanDuration.Text.Length > 0)
            {
                LoanReportDataObj.LoanDuration = Int32.Parse(inputLoanDuration.Text);
            }
        }



        private void inputInitialLoanAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputInitialLoanAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputInitialLoanAmount.Text.Length > 0)
            {
                LoanReportDataObj.InitialLoanAmount = Decimal.Parse(inputInitialLoanAmount.Text);
                labelInitialAmount.Content = "Initial Loan Amount: " + ((ComboBoxItem)inputCurrencySelection.SelectedItem).Content + " " + inputInitialLoanAmount.Text;
            }
        }

        private void inputInterestRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputInterestRate_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (inputInterestRate.Text.Length > 0)
            {
                LoanReportDataObj.InterestRate = Decimal.Parse(inputInterestRate.Text);
            }
        }

        private void inputInterestPenaltyRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            FilterKeypressToDigits(sender, e);
        }

        private void inputInterestPenaltyRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputInterestPenaltyRate.Text.Length > 0)
            {
                LoanReportDataObj.InterestPenaltyRate = Decimal.Parse(inputInterestPenaltyRate.Text);
            }

        }

        private void inputLoanDuration_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void inputInitialLoanAmount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void inputInterestRate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void inputInterestPenaltyRate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void inputReportStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.ReportStartDate = inputReportStartDate.SelectedDate.Value.Date;
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
            }
        }

        private void inputReportEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.ReportEndDate = inputReportEndDate.SelectedDate.Value.Date;
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
            }

        }

        private void reportSpan_Check(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.ReportSpan = Int32.Parse(((RadioButton)sender).Tag.ToString());
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
            }
        }

        private void inputDisplayPaymentsChk_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.DisplayPaymentsChk = (bool)inputDisplayPaymentsChk.IsChecked;
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
            }
        }

        private void buttonOpenAddPayment_Click(object sender, RoutedEventArgs e)
        {
            loanDetails.Content = new LoanPayments(LoanReportDataObj);
        }

        private void buttonOpenCalculation_Click(object sender, RoutedEventArgs e)
        {
            loanDetails.Content = new LoanCalculation(LoanReportDataObj);
            LoanReportDataObj.CalculateLoan();
        }

        private void buttonOpenDrawDown_Click(object sender, RoutedEventArgs e)
        {
            loanDetails.Content = loanDrawDown;
        }

        private void inputOpenLoan_Click(object sender, RoutedEventArgs e)
        {

            bool connected = dbConnection.OpenConnection();

         if(connected)
            {
                // foreach (DataRow row in dbConnection.LoanDataSet.Tables[0].Rows)
                openLoansDataGrid.Items.Clear();
                DataTable openLoansTable = new DataTable();
                openLoansTable.Columns.Add("title");
                openLoansTable.Columns.Add("lender");
                openLoansTable.Columns.Add("beneficiary");
                openLoansTable.Columns.Add("collectionAccount");
                openLoansTable.Columns.Add("startDate");


                foreach (DataRow row in dbConnection.LoanDataSet.Tables["LoanLifeTracker"].Rows)
                    {
                    string date = row.Field<DateTime>("loanStartDate").ToShortDateString();
                    List<string> loanRow = new List<string> { row.Field<string>("loanTitle") , row.Field<string>("loanLender"), row.Field<string>("loanBeneficiary"), row.Field<string>("loanCollectionAccount"), date };
                    DataRow newRow = openLoansTable.NewRow();

                    for (int i = 0; i < loanRow.Count; i++)
                    {
                        newRow[i] = loanRow[i];
                    }
                    openLoansTable.Rows.Add(newRow);
               
                }

                openLoansDataGrid.ItemsSource = openLoansTable.AsDataView();
                openLoansDataGrid.Columns[0].Header = "Title";
                openLoansDataGrid.Columns[1].Header = "Lender";
                openLoansDataGrid.Columns[2].Header = "Beneficifiary";
                openLoansDataGrid.Columns[3].Header = "Collection Account";
                openLoansDataGrid.Columns[4].Header = "Start Date";
                openLoansDataGrid.Items.Refresh();
            }
            //{LoanLifeTracker}
        }

        private void inputSaveLoan_Click(object sender, RoutedEventArgs e)
        {
            // dbConnection = new DatabaseConnection();
            dbConnection.AddLoan(LoanReportDataObj.ActiveLoan);
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    loanReportDataViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("loanReportDataViewSource")));
        //    if(this.LoanReportDataObj != null)
        //    { 
        //    loanReportDataViewSource.Source = LoanReportDataObj;
        //        // Load data by setting the CollectionViewSource.Source property:
        //    }

        //}

        private void inputCompanyInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            labelCompanyInfo.Content = "Company Info: " + ((ComboBoxItem)inputCompanyInfo.SelectedItem).Content;
        }

        private void inputLender_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelLender.Content = "Lender: " + inputLender.Text;
        }

        private void inputLoanTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelTitle.Content = "Title: " + inputLoanTitle.Text;
            LoanReportDataObj.Title = inputLoanTitle.Text;
        }

        private void inputBeneficiary_TextChanged(object sender, TextChangedEventArgs e)
        {
            labeBeneficiary.Content = "Beneficiary: " + inputBeneficiary.Text;
        }

        private void inputCollectionAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelCollectionAccount.Content = "Collection Account: " + inputCollectionAccount.Text;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            dbConnection.dBConnInfo.MySqlUserName = inputUserName.Text;
            dbConnection.dBConnInfo.MySqlPassword = inputPassword.Password;
            bool connectionSucess = dbConnection.OpenConnection();
            MessageBox.Show(connectionSucess.ToString());

            //inputUserName.Text = "";
            //inputPassword.Password = "";
        }
    }
}
