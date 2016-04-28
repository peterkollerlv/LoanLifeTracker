using System;
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
using System.ComponentModel;
using Microsoft.Win32;

namespace InterestTracker
{
    /// <summary>
    /// Interaction logic for InterestTrackerMain.xaml
    /// </summary>
    public partial class InterestTrackerMain : Window
    {


        //loan properties
        LoanReportData LoanReportDataObj;


        private readonly BackgroundWorker bgWorker = new BackgroundWorker();
        private readonly BackgroundWorker bgExcelWorker = new BackgroundWorker();

        public InterestTrackerMain()
        {

            LoanReportDataObj = new LoanReportData();

            LoanReportDataObj.createNewLoan();
            InitializeComponent();
            LoanDetailsPages.CreatePages(LoanReportDataObj);
            //loanCalculation = new LoanCalculation(LoanReportDataObj);
            //loanPayments = new LoanPayments(LoanReportDataObj);
            //loanDrawDown = new LoanDrawDown();
            groupMainWindow.DataContext = LoanReportDataObj;
            inputLoanStartDate.SelectedDate = DateTime.Now.Date;
            inputInterestPenaltyStart.SelectedDate = DateTime.Now.Date;
            loanDetails.Content = LoanDetailsPages.LoanCalculation;
            processIndicationText.Content = "";
            //inputCurrencySelection.SelectedItem = "USD";
        }
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
            if (LoanReportDataObj.ActiveLoan != null)
            {
                // LoanDetailsPages.LoanCalculation.LoanReportObj = this.LoanReportDataObj;
                loanDetails.Content = LoanDetailsPages.LoanCalculation;

                LoanReportDataObj.CalculateLoan();
                navMainTabControl.SelectedItem = navMainLoanDetails;
            }
        }

        private void inputCalculateLoan_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                // LoanDetailsPages.LoanCalculation.LoanReportObj = this.LoanReportDataObj;
                if (loanDetails.Content != LoanDetailsPages.LoanCalculation)
                {
                    loanDetails.Content = LoanDetailsPages.LoanCalculation;
                }

                LoanReportDataObj.CalculateLoan();
                LoanDetailsPages.LoanCalculation.FormatGrid();
                // groupMainLoanDetails.UpdateLayout();
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
        }

        private void inputInterestPenaltyStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.InterestPenaltyDate = inputInterestPenaltyStart.SelectedDate.Value.Date;
        }

        private void inputLoanStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoanReportDataObj.ActiveLoan != null)
            {
                inputReportStartDate.SelectedDate = inputLoanStartDate.SelectedDate;
                inputReportStartDate.DisplayDateStart = inputLoanStartDate.SelectedDate;
                inputReportEndDate.SelectedDate = inputLoanStartDate.SelectedDate.Value.AddYears(Int32.Parse(inputLoanDuration.Text));
            }
            labelLoanStartDate.Content = "Loan Start Date: " + inputLoanStartDate.SelectedDate.Value.ToLongDateString();
        }

        private void inputCurrencySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoanReportDataObj.LoanReportDataGrid != null)
            {
                LoanReportDataObj.CalculateLoan();
            }
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
            if (e.Text == " ")
            {
                e.Handled = true;
            }

            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
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
                labelInitialAmount.Content = "Initial Loan Amount: " + LoanReportDataObj.Currency + " " + inputInitialLoanAmount.Text;
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
                //     LoanReportDataObj.InterestRate = Decimal.Parse(inputInterestRate.Text);
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
            if (LoanReportDataObj.LoanReportDataGrid != null && inputReportStartDate.SelectedDate != null && inputReportEndDate.SelectedDate != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
                LoanDetailsPages.LoanCalculation.FormatGrid();
            }
        }

        private void inputReportEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanReportDataObj.ReportEndDate = inputReportEndDate.SelectedDate.Value.Date;
            if (LoanReportDataObj.LoanReportDataGrid != null && inputReportStartDate.SelectedDate != null && inputReportEndDate.SelectedDate != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
                LoanDetailsPages.LoanCalculation.FormatGrid();
            }
        }

        private void reportSpan_Check(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.ReportSpan = Int32.Parse(((RadioButton)sender).Tag.ToString());
            if (LoanReportDataObj.LoanReportDataGrid != null && inputReportStartDate.SelectedDate != null && inputReportEndDate.SelectedDate != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
                LoanDetailsPages.LoanCalculation.FormatGrid();
            }
        }

        private void inputDisplayPaymentsChk_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.LoanReportDataGrid != null && inputReportStartDate.SelectedDate != null && inputReportEndDate.SelectedDate != null)
            {
                LoanReportDataObj.SortDataGridToReport(inputReportStartDate.SelectedDate.Value, inputReportEndDate.SelectedDate.Value, LoanReportDataObj.ReportSpan);
                LoanDetailsPages.LoanCalculation.FormatGrid();
            }
        }

        private void buttonOpenAddPayment_Click(object sender, RoutedEventArgs e)
        {
            loanDetails.Content = LoanDetailsPages.LoanPayments;
            LoanDetailsPages.LoanPayments.LoanReportDataObj = this.LoanReportDataObj;

        }

        private void buttonOpenCalculation_Click(object sender, RoutedEventArgs e)
        {
            LoanDetailsPages.LoanCalculation.LoanReportObj = this.LoanReportDataObj;
            loanDetails.Content = LoanDetailsPages.LoanCalculation;
            LoanReportDataObj.CalculateLoan();
        }

        private void buttonOpenDrawDown_Click(object sender, RoutedEventArgs e)
        {
            loanDetails.Content = LoanDetailsPages.LoanDrawDown;
        }


        private void inputRefreshLoans_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.dbConnection.isConncectedToDb)
            {
                processIndicationText.Content = "Loading loans... Please Wait";
                statusProgressBar.IsIndeterminate = true;
                bgWorker.DoWork += BgWorker_DoWork;
                bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
                bgWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Unable to connecto to the data base. Please contact your system administrator", "Error connecting to the database");
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (LoanReportDataObj.dbConnection.isConncectedToDb)
            {

                if (null == LoanReportDataObj.ExistingLoans || LoanReportDataObj.ExistingLoans.Count < 1)
                {
                    LoanReportDataObj.dbConnection.GetExistingLoans();
                    DataGridTextColumn title = new DataGridTextColumn();
                    DataGridTextColumn lender = new DataGridTextColumn();
                    DataGridTextColumn beneficiary = new DataGridTextColumn();
                    DataGridTextColumn collectionAccount = new DataGridTextColumn();
                    DataGridTextColumn startDate = new DataGridTextColumn();
                    DataGridTextColumn loanGuid = new DataGridTextColumn();
                    openLoansDataGrid.Columns.Add(title);
                    openLoansDataGrid.Columns.Add(lender);
                    openLoansDataGrid.Columns.Add(beneficiary);
                    openLoansDataGrid.Columns.Add(collectionAccount);
                    openLoansDataGrid.Columns.Add(startDate);
                    openLoansDataGrid.Columns.Add(loanGuid);
                }
                processIndicationText.Content = "";
                statusProgressBar.IsIndeterminate = false;
                //openLoansDataGrid.ItemsSource = null;
                //openLoansDataGrid.ItemsSource = LoanReportDataObj.ExistingLoans;




                openLoansDataGrid.Columns[0].Header = "Title";
                ((DataGridTextColumn)openLoansDataGrid.Columns[0]).Binding = new Binding("LoanTitle");

                openLoansDataGrid.Columns[1].Header = "Lender";
                ((DataGridTextColumn)openLoansDataGrid.Columns[1]).Binding = new Binding("LoanLender");

                openLoansDataGrid.Columns[2].Header = "Beneficifiary";
                ((DataGridTextColumn)openLoansDataGrid.Columns[2]).Binding = new Binding("LoanBeneficiary");

                openLoansDataGrid.Columns[3].Header = "Collection Account";
                ((DataGridTextColumn)openLoansDataGrid.Columns[3]).Binding = new Binding("LoanCollectionAccount");

                openLoansDataGrid.Columns[4].Header = "Start Date";
                ((DataGridTextColumn)openLoansDataGrid.Columns[4]).Binding = new Binding("LoanStartDate");
                //  ((DataGridTextColumn)openLoansDataGrid.Columns[4]).Binding.StringFormat = "d";

                //  ((DataGridTextColumn)openLoansDataGrid.Columns[5]).Binding = new Binding("LoanGuid");

                // openLoansDataGrid.Columns[5].Visibility = Visibility.Collapsed;
                openLoansDataGrid.Items.Refresh();
            }
            else
            {
                processIndicationText.Content = LoanReportDataObj.dbConnection.DbConnectionError;
            }
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoanReportDataObj.dbConnection.OpenConnection();
        }

        private void inputSaveLoan_Click(object sender, RoutedEventArgs e)
        {
            LoanReportDataObj.addLoanToExistingLoans();
            LoanReportDataObj.NotifyUI();
            LoanReportDataObj.dbConnection.UpdateLoansToDb();
        }

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
            LoanReportDataObj.dbConnection.dBConnInfo.MySqlUserName = inputUserName.Text;
            LoanReportDataObj.dbConnection.dBConnInfo.MySqlPassword = inputPassword.Password;
            processIndicationText.Content = "Login is being processed... Please wait";
            statusProgressBar.IsIndeterminate = true;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
            loginExpander.IsExpanded = false;
        }

        private void inputLoanTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "Please add a title...")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void InterestTrackerMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            statusProgressBar.IsIndeterminate = false;
        }

        private void openLoansDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (openLoansDataGrid.SelectedItems.Count > 0)
            {
                LoanReportDataObj.ActiveLoan = ((Loan)openLoansDataGrid.SelectedItems[0]);
            }
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savePdf = new SaveFileDialog();
            savePdf.Filter = "PDF File (*.pdf)|*.pdf";
            savePdf.ValidateNames = true;
            savePdf.Title = "Save Interest Tracker Report...";
            savePdf.FileName = LoanReportDataObj.Title + " " + LoanReportDataObj.ReportStartDate.Month + LoanReportDataObj.ReportStartDate.Day + LoanReportDataObj.ReportStartDate.Year +
               " - " + LoanReportDataObj.ReportEndDate.Month + LoanReportDataObj.ReportEndDate.Day + LoanReportDataObj.ReportEndDate.Year + ".pdf";
            if (savePdf.ShowDialog() == true)
            {
                GeneratePdf toPdf = new GeneratePdf(LoanReportDataObj);
                toPdf.PdfSavePath = savePdf.FileName;
                
                LoanDetailsPages.LoanCalculation.FormatGrid();
                toPdf.BuildPDF();
            }
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            generateExcelBG();
        }

        GenerateExcel toExcel;
        private void generateExcelBG()
        {
            SaveFileDialog saveExcel = new SaveFileDialog();
            saveExcel.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveExcel.ValidateNames = true;
            saveExcel.Title = "Save Interest Tracker Report...";
            saveExcel.FileName = LoanReportDataObj.Title + " " + LoanReportDataObj.ReportStartDate.Month + LoanReportDataObj.ReportStartDate.Day + LoanReportDataObj.ReportStartDate.Year +
               " - " + LoanReportDataObj.ReportEndDate.Month + LoanReportDataObj.ReportEndDate.Day + LoanReportDataObj.ReportEndDate.Year + ".xlsx";
            if (saveExcel.ShowDialog() == true)
            {
                processIndicationText.Content = "Generating Excel document... Please wait";
                statusProgressBar.IsIndeterminate = true;
                toExcel = new GenerateExcel(LoanReportDataObj);
                toExcel.PdfSavePath = saveExcel.FileName;
                bgExcelWorker.DoWork += bgExcelWorker_DoWork;
                bgExcelWorker.RunWorkerCompleted += bgExcelWorker_RunWorkerCompleted;
                bgExcelWorker.RunWorkerAsync();
            }
        }

        private void bgExcelWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            toExcel.BuildExcel();
        }

        private void bgExcelWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            processIndicationText.Content = "";
            statusProgressBar.IsIndeterminate = false;
        }

        private void inputRemoveLoan_Click(object sender, RoutedEventArgs e)
        {
            if (openLoansDataGrid.SelectedItems[0] != null)
            {
                LoanReportDataObj.ExistingLoans.Remove((Loan)openLoansDataGrid.SelectedItems[0]);
            }

        }
    }

    static class LoanDetailsPages
    {
        static private LoanCalculation loanCalculation;
        static private LoanPayments loanPayments;
        static private LoanDrawDown loanDrawDown;
        public static void CreatePages(LoanReportData loanReportDataObj)
        {

            loanCalculation = new LoanCalculation(loanReportDataObj);
            loanPayments = new LoanPayments(loanReportDataObj);
            loanDrawDown = new LoanDrawDown();
        }




        public static LoanCalculation LoanCalculation
        {
            get
            {
                return loanCalculation;
            }

            set
            {

                loanCalculation = value;
            }
        }

        public static LoanPayments LoanPayments
        {
            get
            {
                return loanPayments;
            }

            set
            {
                loanPayments = value;
            }
        }

        public static LoanDrawDown LoanDrawDown
        {
            get
            {
                return loanDrawDown;
            }

            set
            {
                loanDrawDown = value;
            }
        }
    }
}

