using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Data;

namespace InterestTracker
{
    public class LoanReportData
    {
        
        private Loan activeLoan;
       // InterestTrackerMain LoanReportMainObj;

        //public LoanReportData(InterestTrackerMain loanReportMain)
        public LoanReportData()
        {
            //LoanReportMainObj = loanReportMain;
            ReportType = 0;
           // LoanReportDataGrid = LoanReportMainObj.gridLoanCalculation;
          //  LoanReportDataGrid.ItemsSource = null;
            principalBalance = 0;

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
                activeLoan = value;
            }
        }

       
        public Guid LoanGuid
        {
            get { return ActiveLoan.LoanGuid; }
            set { ActiveLoan.LoanGuid = value; }
        }

        public string Title
        {
            get { return ActiveLoan.LoanTitle; }
            set { ActiveLoan.LoanTitle = value; }
        }

       public string CompanyInfo
        {
            get { return ActiveLoan.LoanCompanyInfo; }
            set { ActiveLoan.LoanCompanyInfo = value; }
        }
       public string Lender
        {
            get { return ActiveLoan.LoanLender; }
            set { ActiveLoan.LoanLender = value; }
        }

        public string Beneficiary
        {
            get { return ActiveLoan.LoanBeneficiary; }
            set { ActiveLoan.LoanBeneficiary = value; }
        }

        public string CollectionAccount
        {
            get { return ActiveLoan.LoanCollectionAccount; }
            set { ActiveLoan.LoanCollectionAccount = value; }
        }

        public DateTime StartDate
        {
            get { return ActiveLoan.LoanStartDate; }
            set { ActiveLoan.LoanStartDate = value; }
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
            set { ActiveLoan.LoanInitialLoanAmount = value; }
        }

        public decimal InterestRate
        {
            get { return ActiveLoan.LoanInterestRate; }
            set { ActiveLoan.LoanInterestRate = value; }
        }

        public string Currency
        {
            get { return ActiveLoan.LoanCurrency; }
            set { ActiveLoan.LoanCurrency = value; }
        }

        public bool HasInterestPenalty
        {
            get { return ActiveLoan.LoanHasInterestPenalty; }
            set { ActiveLoan.LoanHasInterestPenalty = value; }
        }

        public DateTime InterestPenaltyDate
        {
            get { return ActiveLoan.LoanInterestPenaltyDate; }
            set { ActiveLoan.LoanInterestPenaltyDate = value; }
        }

        public decimal InterestPenaltyRate
        {
            get { return ActiveLoan.LoanInterestPenaltyRate; }
            set { ActiveLoan.LoanInterestPenaltyRate = value; }
        }

        public string InterestStructureSelection
        {
            get { return ActiveLoan.LoanInterestStructure; }
            set { ActiveLoan.LoanInterestStructure = value; }
        }

    public List<Payment> PaymentList
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
            set { reportStartDate = value; }
        }
        private DateTime reportEndDate;
        public DateTime ReportEndDate
        {
            get { return reportEndDate; }
            set { reportEndDate = value; }
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
            set { displayPaymentsChk = value; }
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
        public bool RegenerateLoanTable;
        public bool LoanGenerated;


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
                case "365fixed":
                    interest = (PrincipalBalance) * calculateInterestRate(date) / numberOfDaysInDateYear;
                    goto default;
                case "360fixed":
                    interest = (PrincipalBalance * calculateInterestRate(date)) / 360;
                    goto default;
                case "365compDay":
                    interest = (PrincipalBalance * calculateInterestRate(date)) / numberOfDaysInDateYear;
                    goto default;
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
                LoanReportDataGrid.Visibility = Visibility.Hidden;
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
                LoanDataTable.Columns.Add("loanDayComments", typeof(string));
                LoanDataTable.PrimaryKey = new DataColumn[] { LoanDataTable.Columns["loanDayDate"] };

                DateTime indexingDate = StartDate;
                DateTime indexEndDate = EndDate;
                //LoanReportMainObj.statusProgressBar.Maximum = LoanTimeSpan.Days;
                //LoanReportMainObj.statusProgressBar.Value = 0;
                PrincipalBalance = 0;
                InterestBalance = 0;
                CumulativeInterestBalance = 0;


                //testing nulling the data source
                LoanGenerated = false;
                //if (RegenerateLoanTable)
                //{

                    for (DateTime currentDate = ActiveLoan.LoanStartDate; currentDate < EndDate; currentDate = currentDate.AddDays(1) ) //LoanReportMainObj.statusProgressBar.Value += 1
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
                if (getPaymentDetails(currentDate).InterestPaymentAmount > InterestBalance)
                {
                    //InterestBalance = InterestBalance - getPaymentDetails(currentDate).InterestPaymentAmount;
                    InterestBalance = Math.Abs(InterestBalance - getPaymentDetails(currentDate).InterestPaymentAmount + dailyInterest);
                    PrincipalBalance = PrincipalBalance - InterestBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount;
                    InterestBalance = 0;
                }
                else if (getPaymentDetails(currentDate).PrincipalPaymentAmount > PrincipalBalance)
                {
                    PrincipalBalance = Math.Abs(PrincipalBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount);
                    InterestBalance = InterestBalance - PrincipalBalance - getPaymentDetails(currentDate).InterestPaymentAmount;
                    PrincipalBalance = 0;
                }

                else
                {
                    PrincipalBalance = PrincipalBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount;
                    InterestBalance += dailyInterest - getPaymentDetails(currentDate).InterestPaymentAmount;
                }
                CurrentBalance = PrincipalBalance + InterestBalance;
            }
            else
            {
                InterestBalance += dailyInterest;   //calculateInterest(currentDate.Date);
            }

            switch (InterestStructureSelection)
            {
                case "_365fixed":
                    {
                        if (getLastDayOfMonth(currentDate) == currentDate.Day)
                        {
                            CurrentBalance = PrincipalBalance + InterestBalance;
                        }
                        goto default;
                    }
                case "_360fixed":
                    {
                        goto case "_365fixed";
                    }

                case "_365compDay":

                    if (currentDate.Day == 1)
                    {
                        InterestBalance = calculateInterest(currentDate.Date); //resets the interest balance at the begining of the month
                    }
                    else if (getLastDayOfMonth(currentDate) == currentDate.Day)
                    {
                        PrincipalBalance = PrincipalBalance + InterestBalance;
                        CurrentBalance = PrincipalBalance;
                    }



                    goto default;

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
                    EnumerableRowCollection<DataRow> reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                                                   where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                                                   select displayDate;
                    switch (reportDisplayRange)
                    {
                        case 0:
                            goto default;
                        case 1:
                            if (DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfWeek == DayOfWeek.Monday || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") != 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") != 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
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
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month
                                              where displayDate.Field<DateTime>("loanDayDate").Day == getLastDayOfMonth(displayDate.Field<DateTime>("loanDayDate")) || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") != 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") != 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
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
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate")) || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") > 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") > 0
                                              select displayDate;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate"))
                                              select displayDate;
                            }
                            goto default;
                        default:
                            DataView viewReport = reportScope.AsDataView();                            
                            LoanReportDataGrid.ItemsSource = viewReport;
                            
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
            if (LoanReportDataGrid.ItemsSource != null)
            {
                LoanReportDataGrid.Columns[0].Header = "Start Date";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[0]).Binding.StringFormat = "MM/dd/yyyy";
                LoanReportDataGrid.Columns[1].Header = "Principal \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[1]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[2].Header = "Interest Rate";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[2]).Binding.StringFormat = "p2";
                LoanReportDataGrid.Columns[3].Header = "Daily Interest \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[3]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[4].Header = "Interest Balance\n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[4]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[5].Header = "Cumulative Interest \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[5]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[6].Header = "Total Payment \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[6]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[7].Header = "Interest Payment \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[7]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[8].Header = "Principal Payment \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[8]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[9].Header = "Current Balance \n(" + ActiveLoan.LoanCurrency + ")";
                ((DataGridTextColumn)LoanReportDataGrid.Columns[9]).Binding.StringFormat = "N";
                LoanReportDataGrid.Columns[10].Header = "Comments";

                LoanReportDataGrid.Visibility = Visibility.Visible;

            }
        }
    }
}
