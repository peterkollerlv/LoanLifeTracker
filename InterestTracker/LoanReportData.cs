using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace InterestTracker
{
    public class LoanReportData : INotifyPropertyChanged
    {
        internal DatabaseConnection dbConnection;
        private Loan activeLoan;
        public LoanReportData()
        {

            ReportType = 0;

            principalBalance = 0;
            dbConnection = new DatabaseConnection(this);
            ExistingLoans = new ObservableCollection<Loan>();
            CurrencyList = new List<string>();
            CurrencyList.Add("USD");
            CurrencyList.Add("EUR");
            CurrencyList.Add("CAD");
            CurrencyList.Add("AUD");

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Loan specific properties properties

        public Loan ActiveLoan
        {
            get
            {
                return activeLoan;
            }
            set
            {
                if (value != activeLoan)
                {
                    activeLoan = value;
                    Notify("ActiveLoan");
                    Notify("Title");
                    Notify("Lender");
                    Notify("Beneficiary");
                    Notify("CollectionAccount");
                    Notify("StartDate");
                    Notify("Currency");
                    Notify("InitialLoanAmount");
                    Notify("InterestRate");
                    Notify("HasInterestPenalty");
                    Notify("InterestPenaltyDate");
                    Notify("InterestPenaltyRate");
                    Notify("ExistingLoans");
                    Notify("ActiveGuid");

                }
            }
        }


        public Guid LoanGuid
        {
            get { return ActiveLoan.LoanGuid; }
            set
            {
                ActiveGuid = value.ToString();
                ActiveLoan.LoanGuid = value;
            }
        }

        public string Title
        {
            get { return ActiveLoan.LoanTitle; }
            set
            {
                if (value != ActiveLoan.LoanTitle)
                {
                    ActiveLoan.LoanTitle = value;
                    Notify("Title");
                }
            }
        }

        public string CompanyInfo
        {
            get { return ActiveLoan.LoanCompanyInfo; }
            set { ActiveLoan.LoanCompanyInfo = value; }
        }

        public string Lender
        {
            get { return ActiveLoan.LoanLender; }
            set
            {
                if (value != ActiveLoan.LoanLender)
                {
                    ActiveLoan.LoanLender = value;
                    Notify("Lender");
                }
            }
        }

        public string Beneficiary
        {
            get { return ActiveLoan.LoanBeneficiary; }
            set
            {
                if (value != ActiveLoan.LoanBeneficiary)
                {
                    ActiveLoan.LoanBeneficiary = value;
                    Notify("Beneficiary");
                }
            }
        }

        public string CollectionAccount
        {
            get { return ActiveLoan.LoanCollectionAccount; }
            set
            {
                if (value != ActiveLoan.LoanCollectionAccount)
                {
                    ActiveLoan.LoanCollectionAccount = value;
                    Notify("CollectionAccount");
                }
            }
        }

        public DateTime StartDate
        {
            get { return ActiveLoan.LoanStartDate; }
            set
            {
                if (value != ActiveLoan.LoanStartDate)
                {
                    ActiveLoan.LoanStartDate = value;
                    Notify("StartDate");
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                if (ActiveLoan.LoanPaid)
                {
                    return ActiveLoan.LoanPaidDate;
                }
                else
                {
                    return StartDate.AddYears(LoanDuration);
                }
            }

            set { ActiveLoan.LoanPaidDate = value; }
        }

        public decimal InitialLoanAmount
        {
            get { return ActiveLoan.LoanInitialLoanAmount; }
            set
            {
                if (value != ActiveLoan.LoanInitialLoanAmount)
                {
                    ActiveLoan.LoanInitialLoanAmount = value;
                    Notify("InitialLoanAmount");
                }
            }
        }

        public decimal InterestRate
        {
            get { return ActiveLoan.LoanInterestRate; }
            set
            {
                if (value != ActiveLoan.LoanInterestRate)
                {
                    ActiveLoan.LoanInterestRate = value;
                    Notify("InterestRate");
                }
            }
        }

        public string Currency
        {
            get { return ActiveLoan.LoanCurrency; }
            set
            {
                if (value != ActiveLoan.LoanCurrency)
                {


                    ActiveLoan.LoanCurrency = value;
                    Notify("Currency");
                }
            }
        }

        public bool HasInterestPenalty
        {
            get { return ActiveLoan.LoanHasInterestPenalty; }
            set
            {
                if (value != ActiveLoan.LoanHasInterestPenalty)
                {
                    ActiveLoan.LoanHasInterestPenalty = value;
                    Notify("HasInterestPenalty");
                }
            }
        }

        public DateTime InterestPenaltyDate
        {
            get { return ActiveLoan.LoanInterestPenaltyDate; }
            set
            {
                if (value != ActiveLoan.LoanInterestPenaltyDate)
                {
                    ActiveLoan.LoanInterestPenaltyDate = value;
                    Notify("InterestPenaltyDate");
                }
            }
        }

        public decimal InterestPenaltyRate
        {
            get { return ActiveLoan.LoanInterestPenaltyRate; }
            set
            {
                if (value != ActiveLoan.LoanInterestPenaltyRate)
                {
                    ActiveLoan.LoanInterestPenaltyRate = value;
                    Notify("InterestPenaltyRate");
                }
            }
        }

        public string InterestStructureSelection
        {
            get { return ActiveLoan.LoanInterestStructure; }
            set { ActiveLoan.LoanInterestStructure = value; }
        }

       // public Payment SelectedPayment { get; set; }
        public ObservableCollection<Payment> PaymentList
        {
            get { return ActiveLoan.LoanPaymentsList; }
            set { ActiveLoan.LoanPaymentsList = value; }
        }


        public bool Paid
        {
            get { return ActiveLoan.LoanPaid; }
            set { ActiveLoan.LoanPaid = value; }
        }

        public bool ActiveLoanSavedToDb
        {
            get { return ActiveLoan.LoanSavedToDb; }
            set { ActiveLoan.LoanSavedToDb = value; }
        }



        //calculation specific properties

        private int loanDuration;
        public int LoanDuration
        {
            get { return loanDuration; }
            set { loanDuration = value; }
        }
        private DateTime reportStartDate;
        public DateTime ReportStartDate
        {
            get { return reportStartDate; }
            set
            {
                if (value != ReportStartDate)
                {
                    reportStartDate = value;
                    Notify("ReportStartDate");
                }
            }
        }
        private DateTime reportEndDate;
        public DateTime ReportEndDate
        {
            get { return reportEndDate; }
            set
            {
                if (value != ReportEndDate)
                {
                    reportEndDate = value;
                    Notify("ReportEndDate");
                }
            }
        }
        private TimeSpan reportTimeSpan;
        public TimeSpan ReportTimeSpan
        {
            get
            {
                reportTimeSpan = ReportEndDate.Subtract(ReportStartDate);
                return reportTimeSpan;
            }
        }
        private int reportType;
        public int ReportType
        {
            get { return reportType; }
            set { reportType = value; }
        }

        public int ReportSpan { get; set; }


        private bool displayPaymentsChk;
        public bool DisplayPaymentsChk
        {
            get { return displayPaymentsChk; }
            set
            {
                if (value != displayPaymentsChk)
                {
                    displayPaymentsChk = value;
                    Notify("DisplayPaymentsChk");
                }
            }
        }
        public TimeSpan loanTimeSpan;
        public TimeSpan LoanTimeSpan
        {
            get
            {
                loanTimeSpan = EndDate.Subtract(ActiveLoan.LoanStartDate);
                return loanTimeSpan;
            }
        }

        public DataGrid LoanReportDataGrid { get; set; }


        private decimal principalBalance;
        public decimal PrincipalBalance
        {
            get { return principalBalance; }
            set { principalBalance = value; }
        }

        private decimal interestBalance;
        public decimal InterestBalance
        {
            get { return interestBalance; }
            set { interestBalance = value; }
        }

        private decimal cumulativeInterestBalance;
        public decimal CumulativeInterestBalance
        {
            get { return cumulativeInterestBalance; }
            set { cumulativeInterestBalance = value; }
        }

        private decimal currentBalance;
        public decimal CurrentBalance
        {
            get { return currentBalance; }
            private set { currentBalance = value; }
        }



        // public string SelectedTabControl;
        public DataTable LoanDataTable;
        public DataTable ReportViewTable;
        public bool RegenerateLoanTable;
        public bool LoanGenerated;
        private string activeGuid;
        private EnumerableRowCollection<DataRow> reportScope;
        public EnumerableRowCollection<DataRow> ReportScope
        {
            get { return reportScope; }
            set { reportScope = value; }
        }

        private ObservableCollection<Loan> existingLoans;
        public ObservableCollection<Loan> ExistingLoans
        {
            get { return existingLoans; }

            set
            {
                if (value != ExistingLoans)
                {
                    existingLoans = value;
                    Notify("ExistingLoans");
                }
            }
        }

        public string ActiveGuid
        {
            get
            {
                activeGuid = LoanGuid.ToString();
                return "Active GUID: " + activeGuid;

            }
            set
            {
                if (value != ActiveGuid)
                {
                    activeGuid = value;
                    Notify("ActiveGuid");
                }
            }
        }

        public List<string> CurrencyList
        { get; set; }


        public void NotifyUI()
        {
            Notify("ActiveLoan");
            Notify("Title");
            Notify("Lender");
            Notify("Beneficiary");
            Notify("CollectionAccount");
            Notify("StartDate");
            Notify("Currency");
            Notify("InitialLoanAmount");
            Notify("InterestRate");
            Notify("HasInterestPenalty");
            Notify("InterestPenaltyDate");
            Notify("InterestPenaltyRate");
            Notify("ExistingLoans");
            Notify("ActiveGuid");
        }







        // not implemented yet

        //private decimal recuringPaymentAmount;
        //public decimal RecuringPaymentAmount
        //{
        //    get { return recuringPaymentAmount; }
        //    set { recuringPaymentAmount = value; }
        //}





        //Generate the loan objects, and populate the data grid colums



        public void createNewLoan()
        {
            ActiveLoan = new Loan(true);

        }

        public void addLoanToExistingLoans()
        {
            if (ExistingLoans != null)
            {
                if (!ExistingLoans.Contains(ActiveLoan))
                {
                    ExistingLoans.Add(ActiveLoan);
                }
                else
                {
                    //Loan existingLoan = ExistingLoans.Select(ActiveLoan)
                }
            }
        }

        public void setExistingLoanActive(Loan selectedLoan)
        {
            ActiveLoan = selectedLoan;
        }

        // interest structure and rate calculation

        private decimal calculateInterest(DateTime date) //geting date to find out the lenght of the year
        {
            decimal interest = 0;
            int numberOfDaysInDateYear = getLastDayOfYear(date);

            switch (InterestStructureSelection)
            {
                case "fixed365":
                    {
                        interest = (PrincipalBalance * calculateInterestRate(date)) / numberOfDaysInDateYear;
                        goto default;
                    }
                case "compDay365":
                case "compMonth365":
                case "compQuarter365":
                case "compYear365":
                    interest = (CurrentBalance * calculateInterestRate(date)) / numberOfDaysInDateYear;
                    goto default;
                case "fixed360":
                    {
                        interest = (PrincipalBalance * calculateInterestRate(date)) / 360;
                        goto default;
                    }
                case "compDay360":
                case "compMonth360":
                case "compQuarter360":
                case "compYear360":
                    interest = (CurrentBalance * calculateInterestRate(date)) / 360;
                    goto default;
                //  case "compDay365":
                //interest = (PrincipalBalance * calculateInterestRate(date)) / numberOfDaysInDateYear;
                //goto default;
                default:
                    return FormatDigitInput.FormatToDecimal(interest);
            }
        }

        private decimal calculateInterestRate(DateTime interestOnGivenDay)
        {
            decimal returnInteresRate;
            if (HasInterestPenalty)
            {
                if (interestOnGivenDay < ActiveLoan.LoanInterestPenaltyDate)
                {
                    returnInteresRate = InterestRate / 100;
                    return returnInteresRate;
                }
                else
                {
                    returnInteresRate = InterestPenaltyRate / 100;
                    return returnInteresRate;
                }
            }
            else
            {
                returnInteresRate = InterestRate / 100;
                return returnInteresRate;
            }
        }

        // loan calculation

        public void CalculateLoan()
        {
            try
            {
                // LoanReportDataGrid.Visibility = Visibility.Hidden;
                LoanDataTable = new DataTable();
                LoanDataTable.Columns.Add("loanDayDate", typeof(DateTime));
                LoanDataTable.Columns.Add("loanDayPrincipal", typeof(decimal));
                LoanDataTable.Columns.Add("loanDayInterestRate", typeof(decimal));
                LoanDataTable.Columns.Add("loanDayInterest", typeof(decimal));
                LoanDataTable.Columns.Add("loanInterestBalance", typeof(decimal));
                LoanDataTable.Columns.Add("loanDayCuIntrestBal", typeof(decimal));
                LoanDataTable.Columns.Add("loanDayTotalPayment", typeof(decimal)); ;
                LoanDataTable.Columns.Add("loanDayInterestPayment", typeof(decimal));
                LoanDataTable.Columns.Add("loanDayPrincipalPayment", typeof(decimal)); ;
                LoanDataTable.Columns.Add("loanDayCurrentBalance", typeof(decimal));
                //  LoanDataTable.Columns.Add("loanDayComments", typeof(string));
                LoanDataTable.PrimaryKey = new DataColumn[] { LoanDataTable.Columns["loanDayDate"] };

                DateTime indexingDate = StartDate;
                DateTime indexEndDate = EndDate;
                //LoanReportMainObj.statusProgressBar.Maximum = LoanTimeSpan.Days;
                //LoanReportMainObj.statusProgressBar.Value = 0;
                PrincipalBalance = 0;
                InterestBalance = 0;
                CumulativeInterestBalance = 0;
                CurrentBalance = 0;


                //testing nulling the data source
                LoanGenerated = false;
                //if (RegenerateLoanTable)
                //{

                for (DateTime currentDate = ActiveLoan.LoanStartDate; currentDate < EndDate; currentDate = currentDate.AddDays(1)) //LoanReportMainObj.statusProgressBar.Value += 1
                {
                    bool rowExists = LoanDataTable.Rows.Contains(currentDate);

                    if (!rowExists)
                    {
                        if (CurrentBalance >= 0)
                        {
                            LoanDataTable.Rows.Add(addDateRow(currentDate));
                        }
                    }
                    else if (rowExists)
                    {
                        LoanDataTable.Rows.Find(currentDate).Delete();
                        if (CurrentBalance >= 0)
                        {
                            LoanDataTable.Rows.Add(addDateRow(currentDate));
                        }
                    }
                }
                // LoanReportMainObj.statusProgressBar.Value = 0;
                LoanDataTable.AcceptChanges();
                //LoanReportMainObj.loanReportDataGrid.DataSource = LoanDataTable;
                // SetColumnHeaders();
                LoanGenerated = true;
                SortDataGridToReport(ActiveLoan.LoanStartDate, EndDate, 2);
                // }
                //else if (!RegenerateLoanTable)
                //{
                //    MessageBox.Show("Loan is not regenerated with new start date");
                //}

            }
            catch (OverflowException e)
            {
                MessageBox.Show(e.Message.ToString());
                return;
            }
        }

        private DataRow addDateRow(DateTime currentDate)
        {
            DataRow dateRow = LoanDataTable.NewRow();

            if (currentDate == ActiveLoan.LoanStartDate) //first day of the loan
            {
                PrincipalBalance = ActiveLoan.LoanInitialLoanAmount;
                CurrentBalance = PrincipalBalance;
            }


            decimal dailyInterest = calculateInterest(currentDate.Date);
            CumulativeInterestBalance += dailyInterest;

            if (paymentExists(currentDate))
            {
                if (getPaymentDetails(currentDate).InterestPaymentAmount > CumulativeInterestBalance)
                {
                    //InterestBalance = InterestBalance - getPaymentDetails(currentDate).InterestPaymentAmount;
                    CumulativeInterestBalance = Math.Abs(CumulativeInterestBalance - getPaymentDetails(currentDate).InterestPaymentAmount + dailyInterest);
                    PrincipalBalance = PrincipalBalance - CumulativeInterestBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount;
                    CumulativeInterestBalance = 0;
                }
                else if (getPaymentDetails(currentDate).PrincipalPaymentAmount > PrincipalBalance)
                {
                    PrincipalBalance = Math.Abs(PrincipalBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount);
                    CumulativeInterestBalance = CumulativeInterestBalance - PrincipalBalance - getPaymentDetails(currentDate).InterestPaymentAmount;
                    PrincipalBalance = 0;
                }

                else
                {
                    PrincipalBalance = PrincipalBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount;
                    CumulativeInterestBalance += dailyInterest - getPaymentDetails(currentDate).InterestPaymentAmount;
                }
                CurrentBalance = PrincipalBalance + CumulativeInterestBalance;
            }
            else
            {
               // CumulativeInterestBalance += dailyInterest;   //calculateInterest(currentDate.Date);
            }

            

            switch (InterestStructureSelection)
            {
                case "fixed360":
                case "fixed365":
                    {
                        CurrentBalance = PrincipalBalance + CumulativeInterestBalance;

                        goto default;
                    }

                case "compDay365":
                case "compDay360":
                    {

                        CumulativeInterestBalance = calculateInterest(currentDate.Date);
                        CurrentBalance = PrincipalBalance + CumulativeInterestBalance;
                       // CurrentBalance = PrincipalBalance;

                        goto default;
                    }

                case "compMonth365":
                case "compMonth360":
                    {
                        if (currentDate.Day == 1)
                        {
                            CurrentBalance = PrincipalBalance + CumulativeInterestBalance;
                            //CurrentBalance = PrincipalBalance;
                           // CumulativeInterestBalance = calculateInterest(currentDate.Date); //resets the interest balance;
                        }
                        if (getLastDayOfMonth(currentDate) == currentDate.Day)
                        {

                        }
                        goto default;
                    }
                case "compQuarter365":
                case "compQuarter360":
                    {
                        goto case "compMonth365"; //non implemented defaulting to monthly
                    }
                case "compYear365":
                case "compYear360":
                    {
                        goto case "compMonth365"; //non implemented defaulting to monthly
                    }

                //if (currentDate.Day == 1)
                //{
                // InterestBalance = calculateInterest(currentDate.Date); //resets the interest balance;
                // }
                //else if (getLastDayOfMonth(currentDate) == currentDate.Day)
                //{

                // }



                //  goto default;

                default:
                    dateRow[0] = currentDate;
                    dateRow[1] = FormatDigitInput.FormatToDecimal(PrincipalBalance);
                    dateRow[2] = calculateInterestRate(currentDate);
                    dateRow[3] = FormatDigitInput.FormatToDecimal(dailyInterest);
                    dateRow[4] = FormatDigitInput.FormatToDecimal(InterestBalance);
                    dateRow[5] = FormatDigitInput.FormatToDecimal(CumulativeInterestBalance);

                    if (paymentExists(currentDate))
                    {
                        dateRow[6] = FormatDigitInput.FormatToDecimal(getPaymentDetails(currentDate).TotalPaymentAmount);
                        dateRow[7] = FormatDigitInput.FormatToDecimal(getPaymentDetails(currentDate).InterestPaymentAmount);
                        dateRow[8] = FormatDigitInput.FormatToDecimal(getPaymentDetails(currentDate).PrincipalPaymentAmount);
                    }
                    else
                    {
                        dateRow[6] = 0;
                        dateRow[7] = 0;
                        dateRow[8] = 0;
                    }
                    dateRow[9] = FormatDigitInput.FormatToDecimal(CurrentBalance);
                    return dateRow;
            }
        }

        public Payment getPaymentDetails(DateTime currentDate)
        {
            foreach (Payment paymentDetail in ActiveLoan.LoanPaymentsList)
            {
                if (paymentDetail.PaymentDate == currentDate)
                {
                    return paymentDetail;
                }
            }
            return null;
        }
        private bool paymentExists(DateTime currentDate)
        {
            foreach (Payment paymentDetail in ActiveLoan.LoanPaymentsList)
            {
                if (paymentDetail.PaymentDate == currentDate)
                {
                    return true;
                }
            }
            return false;
        }




        // report generation

        //get last days methods
        private int getLastDayOfMonth(DateTime lastDay)
        {
            switch (lastDay.Month)
            {
                case 12:
                    return 31;
            }
            var monthToChange = new DateTime(lastDay.Year, lastDay.Month + 1, 1);
            monthToChange = monthToChange.AddDays(-1);
            lastDay = monthToChange;
            return lastDay.Day;
        }

        private int getLastDayOfYear(DateTime lastDay)
        {
            int lastDayOfYear = 0;
            lastDay = new DateTime(lastDay.Year, 12, 31);
            lastDayOfYear = lastDay.DayOfYear;
            return lastDayOfYear;
        }

        //generate gridview contents on LoanReportMain

        public void SortDataGridToReport(DateTime reportStart, DateTime reportEnd, int reportDisplayRange)
        {
            if (LoanGenerated)
            {
                if (ReportType == 0)
                {
                    //EnumerableRowCollection<DataRow> reportScope = from displayDate in LoanDataTable.AsEnumerable()
                    ReportScope = from displayDate in LoanDataTable.AsEnumerable()

                                  where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                  select displayDate;
                    switch (reportDisplayRange)
                    {
                        case 0:
                            goto default;
                        case 1:
                            if (DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfWeek == DayOfWeek.Monday || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") != 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") != 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfWeek == DayOfWeek.Monday
                                              select displayDate;
                            }
                            goto default;
                        case 2:
                            if (DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month
                                              where displayDate.Field<DateTime>("loanDayDate").Day == getLastDayOfMonth(displayDate.Field<DateTime>("loanDayDate")) || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") != 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") != 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month
                                              where displayDate.Field<DateTime>("loanDayDate").Day == getLastDayOfMonth(displayDate.Field<DateTime>("loanDayDate"))
                                              select displayDate;
                            }
                            goto default;
                        case 3:
                            if (DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate")) || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") > 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") > 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                ReportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate"))
                                              select displayDate;
                            }
                            goto default;
                        default:
                            DataView viewReport = ReportScope.AsDataView();
                            ReportViewTable = viewReport.ToTable();
                            //  LoanReportDataGrid.ItemsSource = null;
                            //  LoanReportDataGrid.ItemsSource = viewReport;

                            SetColumnHeaders();


                            break;
                    }
                }
            }
            else
            { } // MessageBox.Show("Please generate the loan first."); }
        }


        public void SetColumnHeaders()
        {
            //if (LoanReportDataGrid.ItemsSource != null)
            //{

            //    LoanReportDataGrid.AutoGenerateColumns = true;

            //    LoanReportDataGrid.Columns[0].Header = "Start Date";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[0]).Binding.StringFormat = "MM/dd/yyyy";
            //    LoanReportDataGrid.Columns[1].Header = "Principal \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[1]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[2].Header = "Interest Rate";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[2]).Binding.StringFormat = "p2";
            //    LoanReportDataGrid.Columns[3].Header = "Daily Interest \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[3]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[4].Header = "Interest Balance\n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[4]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[5].Header = "Cumulative Interest \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[5]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[6].Header = "Total Payment \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[6]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[7].Header = "Interest Payment \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[7]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[8].Header = "Principal Payment \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[8]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[9].Header = "Current Balance \n(" + ActiveLoan.LoanCurrency + ")";
            //    ((DataGridTextColumn)LoanReportDataGrid.Columns[9]).Binding.StringFormat = "N";
            //    LoanReportDataGrid.Columns[10].Header = "Comments";

            //    LoanReportDataGrid.Visibility = Visibility.Visible;

            // }
        }
    }
}
