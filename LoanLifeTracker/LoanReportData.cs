using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace LoanLifeTracker
{
    public class LoanReportData
    {
        private List<Payment> paymentsList;
        LoanReportMain LoanReportMainObj;
        public LoanReportData(LoanReportMain loanReportMain)
        {
            LoanReportMainObj = loanReportMain;
            ReportType = 0;
            LoanReportDataGrid = LoanReportMainObj.loanReportDataGrid;
            LoanReportDataGrid.DataSource = null;
            //LoanReportMain.LoanCreated = false;
            principalBalance = 0;
            paymentsList = new List<Payment>();
        }
        //private bool firstRowSet = false;

        //build the LoanDetails objects

        public string LoanTitle { get; set; }
        public string CompanyInfo { get; set; }
        public string Lender { get; set; }
        public string Beneficiary { get; set; }
        public string CollectionAccount { get; set; }

        public string LoanCurrency
        {
            get { return LoanReportMainObj.inputCurrencySelection.SelectedItem.ToString(); }
            set { value = LoanReportMainObj.inputCurrencySelection.SelectedItem.ToString(); }
        }
        public DateTime LoanStartDate
        {
            get { return LoanReportMainObj.inputLoanStartDate.Value.Date; }
        }
        public decimal InitialLoanAmount
        {
            get
            {
                if (LoanReportMainObj.inputInitialLoanAmount.Text.Trim() != "")
                {
                    decimal convertedAmount = Convert.ToDecimal(LoanReportMainObj.inputInitialLoanAmount.Text);
                    return convertedAmount;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int LoanDuration
        {
            get { return (int)LoanReportMainObj.inputLoanDuration.Value; }
        }
        public DateTime ReportStartDate
        {
            get { return LoanReportMainObj.inputReportStartDate.Value.Date; }
        }
        public DateTime ReportEndDate
        {
            get { return LoanReportMainObj.inputReportEndDate.Value.Date; }
        }
        public TimeSpan ReportTimeSpan
        {
            get { return ReportEndDate.Subtract(ReportStartDate); }
        }
        public int ReportType
        {
            get { return LoanReportMainObj.inputReportType.SelectedIndex; }
            set { LoanReportMainObj.inputReportType.SelectedIndex = value; }
        }
        public int InterestStructureSelection
        {
            get { return LoanReportMainObj.inputInterestStructureSelection.SelectedIndex; }
        }
        public DateTime InterestPenaltyDate
        {
            get { return LoanReportMainObj.inputInterestPenaltyStart.Value.Date; }
        }
        public bool InterestPenaltyChk
        {
            get { return LoanReportMainObj.inputInterestPenaltyChk.Checked; }
        }
        public bool DisplayPaymentsChk
        {
            get { return LoanReportMainObj.inputDisplayPaymentsChk.Checked; }
        }
        public TimeSpan LoanTimeSpan
        {
            get { return LoanEndDate.Subtract(LoanStartDate); }
        }
        public DateTime LoanEndDate
        {
            get { return LoanStartDate.AddYears(LoanDuration); }
            set { }
        }
        public decimal RecuringPaymentAmount { get; set; }
        public DataGridView LoanReportDataGrid { get; set; }

        private decimal principalBalance;
        public decimal PrincipalBalance
        {
            get
            {
                return principalBalance;
            } set
            {
                principalBalance = value;
            }
        }

        public List<Payment> PaymentsList
        {
            get
            {
                return paymentsList;
            }
            set
            {
                paymentsList = value;
            }
        }

        private decimal interestBalance;
        public decimal InterestBalance
        {
            get
            {
                return interestBalance;
            }
            private set { interestBalance = value; }
        }

        private decimal cumulativeInterestBalance;
        public decimal CumulativeInterestBalance
        {
            get
            {
                return cumulativeInterestBalance;
            }
            private set
            {
                cumulativeInterestBalance = value;
            }
        }

        private decimal currentBalance;
        public decimal CurrentBalance { get
            {
                return currentBalance;
            }
            private set { currentBalance = value; }
        }

        public string SelectedTabControl;
        public DataTable LoanDataTable;
        public bool RegenerateLoanTable;
        public bool LoanGenerated;


        //Generate the loan objects, and populate the data grid colums

        public void CreateLoanObjects()
        {
            LoanDataTable = new DataTable();
            //firstRowSet = false;
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
        }

        // interest structure and rate calculation

        private decimal calculateInterest(decimal interestRate, decimal principal, DateTime date) //geting date to find out the lenght of the year
        {
            decimal interest = 0;
            int numberOfDaysInDateYear = getLastDayOfYear(date);

            switch (InterestStructureSelection)
            {
                case 0:
                    interest = (principal * interestRate) / numberOfDaysInDateYear;
                    goto default;
                case 1:
                    interest = (principal * interestRate) / 360;
                    goto default;
                case 2:
                    interest = (principal * interestRate) / numberOfDaysInDateYear;
                    goto default;
                default:
                    return FormatDigitInput.FormatToDecimal(interest);
            }
        }

        private decimal InterestRate(DateTime interestOnGivenDay)
        {
            decimal returnInteresRate;
            if (InterestPenaltyChk)
            {
                if (interestOnGivenDay < InterestPenaltyDate)
                {
                    returnInteresRate = LoanReportMainObj.inputInterestRate.Value / 100;
                    return returnInteresRate;
                }
                else
                {
                    returnInteresRate = LoanReportMainObj.inputInterestPenaltyRate.Value / 100;
                    return returnInteresRate;
                }
            }
            else
            {
                returnInteresRate = LoanReportMainObj.inputInterestRate.Value / 100;
                return returnInteresRate;
            }
        }

        // loan calculation

        public void CalculateLoan()
        {
            try
            {
                //if (LoanReportMain.LoanCreated)

                DateTime indexingDate = LoanStartDate;
                DateTime indexEndDate = LoanEndDate;
                LoanReportMainObj.statusProgressBar.Maximum = LoanTimeSpan.Days;
                LoanReportMainObj.statusProgressBar.Value = 0;
                PrincipalBalance = 0;
                InterestBalance = 0;
                CumulativeInterestBalance = 0;


                //testing nulling the data source
                LoanGenerated = false;
                if (RegenerateLoanTable)
                {

                    for (DateTime currentDate = LoanStartDate; currentDate < LoanEndDate; currentDate = currentDate.AddDays(1), LoanReportMainObj.statusProgressBar.Value += 1)
                    {
                        bool rowExists = LoanDataTable.Rows.Contains(currentDate);
                        if (!rowExists)
                        {
                            if (addDateRow(currentDate) != null)
                            {
                                LoanDataTable.Rows.Add(addDateRow(currentDate));
                            }
                        }
                        else if (rowExists)
                        {
                            LoanDataTable.Rows.Find(currentDate).Delete();
                            if (addDateRow(currentDate) != null)
                            {
                                LoanDataTable.Rows.Add(addDateRow(currentDate));
                            }
                        }
                    }
                    LoanReportMainObj.statusProgressBar.Value = 0;
                    LoanDataTable.AcceptChanges();
                    //LoanReportMainObj.loanReportDataGrid.DataSource = LoanDataTable;
                    // SetColumnHeaders();
                    LoanGenerated = true;
                    SortDataGridToReport(LoanStartDate, LoanEndDate, 2);
                }
                else if (!RegenerateLoanTable)
                {
                    MessageBox.Show("Loan is not regenerated with new start date");
                }

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
            decimal dailyInterest = calculateInterest(InterestRate(currentDate), PrincipalBalance, currentDate.Date);
            dateRow[0] = currentDate;
            if (currentDate == LoanStartDate)
            {
                PrincipalBalance = FormatDigitInput.FormatToDecimal(InitialLoanAmount);

            }
            
    

            if (paymentExists(currentDate))
            {
                PrincipalBalance = PrincipalBalance - getPaymentDetails(currentDate).PrincipalPaymentAmount;
            }
            
            dateRow[2] = InterestRate(currentDate);
            dateRow[3] = FormatDigitInput.FormatToDecimal(dailyInterest);
            if (paymentExists(currentDate))
            {
                InterestBalance += calculateInterest(InterestRate(currentDate), PrincipalBalance, currentDate.Date) - getPaymentDetails(currentDate).InterestPaymentAmount;
            }
            else
            {
                InterestBalance = InterestBalance + calculateInterest(InterestRate(currentDate), PrincipalBalance, currentDate.Date);
            }
            dateRow[4] = FormatDigitInput.FormatToDecimal(InterestBalance);
            CumulativeInterestBalance = CumulativeInterestBalance + calculateInterest(InterestRate(currentDate), PrincipalBalance, currentDate.Date);
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

            if (InterestStructureSelection == 2)
            {
                if (currentDate.Day == 1)
                {
                    InterestBalance = 0;
                }
                else if (getLastDayOfMonth(currentDate) == currentDate.Day)
                {
                    PrincipalBalance = PrincipalBalance + InterestBalance;
                    CurrentBalance = PrincipalBalance;
                }
                else
                {
                    CurrentBalance = PrincipalBalance + InterestBalance;
                }
            }
            else
            {
                CurrentBalance = PrincipalBalance + InterestBalance;
            }


            dateRow[9] = FormatDigitInput.FormatToDecimal(CurrentBalance);
            dateRow[1] = FormatDigitInput.FormatToDecimal(PrincipalBalance);

            if (CurrentBalance > 0)
            {
                return dateRow;
            }
            else
            {
                return null;
            }
        }

        private Payment getPaymentDetails(DateTime currentDate)
        {
            foreach (var paymentDetail in paymentsList)
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
            foreach (var paymentDetail in paymentsList)
            {
                if(paymentDetail.PaymentDate == currentDate)
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
                    // getLastDayOfMonth(LoanStartDate);
                    switch (reportDisplayRange)
                    {
                        case 0:
                            EnumerableRowCollection<DataRow> reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                                                           where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                                                           select displayDate;
                            DataView viewReport = reportScope.AsDataView();
                            LoanReportDataGrid.DataSource = viewReport;
                            SetColumnHeaders();
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
                                viewReport = reportScope.AsDataView();
                                LoanReportDataGrid.DataSource = viewReport;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfWeek == DayOfWeek.Monday
                                              select displayDate;
                                viewReport = reportScope.AsDataView();
                                LoanReportDataGrid.DataSource = viewReport;
                            }
                            SetColumnHeaders();
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
                                viewReport = reportScope.AsDataView();
                                LoanReportDataGrid.DataSource = viewReport;
                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").Month == displayDate.Field<DateTime>("loanDayDate").Month
                                              where displayDate.Field<DateTime>("loanDayDate").Day == getLastDayOfMonth(displayDate.Field<DateTime>("loanDayDate"))
                                              select displayDate;
                                viewReport = reportScope.AsDataView();
                                LoanReportDataGrid.DataSource = viewReport;
                            }
                            SetColumnHeaders();
                            goto default;
                        case 3:
                            if (DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate")) || displayDate.Field<decimal>("loanDayTotalPayment") != 0 ||
                                              displayDate.Field<decimal>("loanDayInterestPayment") != 0 || displayDate.Field<decimal>("loanDayPrincipalPayment") != 0
                                              select displayDate;

                                viewReport = reportScope.AsDataView();
                                LoanReportDataGrid.DataSource = viewReport;

                            }
                            else if (!DisplayPaymentsChk)
                            {
                                reportScope = from displayDate in LoanDataTable.AsEnumerable()
                                              where displayDate.Field<DateTime>("loanDayDate") <= reportEnd && displayDate.Field<DateTime>("loanDayDate") >= reportStart
                                              where displayDate.Field<DateTime>("loanDayDate").Year == displayDate.Field<DateTime>("loanDayDate").Year &&
                                              displayDate.Field<DateTime>("loanDayDate").DayOfYear == getLastDayOfYear(displayDate.Field<DateTime>("loanDayDate"))
                                              select displayDate;
                            }
                            SetColumnHeaders();
                            goto default;
                        default:
                            if (LoanReportMainObj.inputLoanPanelSelection.SelectedItem.ToString() == "Report")
                            {
                                DataTable reportTable = new DataTable();
                                foreach (DataGridViewColumn gridColumns in LoanReportDataGrid.Columns)
                                {
                                    reportTable.Columns.Add(gridColumns.Name);
                                }
                                foreach (DataGridViewRow gridRows in LoanReportDataGrid.Rows)
                                {
                                    DataRow reportTableRow = reportTable.NewRow();
                                    foreach (DataColumn gridCol in reportTable.Columns)
                                    {
                                        reportTableRow[gridCol.ColumnName] = gridRows.Cells[gridCol.ColumnName].FormattedValue.ToString();
                                    }
                                    reportTable.Rows.Add(reportTableRow);
                                }
                                LoanReportDataGrid.DataSource = reportTable;

                                //LoanReportDataGrid.ReadOnly = true;
                            }
                            else
                            {
                               // LoanReportDataGrid.ReadOnly = false;
                                LoanReportDataGrid.Refresh();
                            }
                            break;
                    }
                }
            }
            else
            { } // MessageBox.Show("Please generate the loan first."); }
        }

        public void SetColumnHeaders()
        {
            if(LoanReportDataGrid.DataSource != null)
            {
                LoanReportDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                LoanReportDataGrid.Columns["loanDayDate"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                LoanReportDataGrid.Columns["loanDayDate"].DefaultCellStyle.Format = "MMMM dd, yyyy";
                LoanReportDataGrid.Columns["loanDayInterestRate"].DefaultCellStyle.Format = "p2";   // #0.00 %
                LoanReportDataGrid.Columns["loanDayPrincipal"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayInterest"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanInterestBalance"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayCuIntrestBal"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayTotalPayment"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayInterestPayment"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayPrincipalPayment"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayCurrentBalance"].DefaultCellStyle.Format = "N";
                LoanReportDataGrid.Columns["loanDayDate"].HeaderText = "Date"; // Index 0
                LoanReportDataGrid.Columns["loanDayPrincipal"].HeaderText = "Principal \n(" + LoanCurrency + ")"; // Index 1
                LoanReportDataGrid.Columns["loanDayInterestRate"].HeaderText = "Interest Rate"; // Index 2
                LoanReportDataGrid.Columns["loanDayInterest"].HeaderText = "Daily Interest \n(" + LoanCurrency + ")"; // Index 3
                LoanReportDataGrid.Columns["loanInterestBalance"].HeaderText = "Interest Balance\n(" + LoanCurrency + ")"; // Index 4
                LoanReportDataGrid.Columns["loanDayCuIntrestBal"].HeaderText = "Cumulative Interest Balance \n(" + LoanCurrency + ")"; // Index 5
                LoanReportDataGrid.Columns["loanDayTotalPayment"].HeaderText = "Total Payment \n(" + LoanCurrency + ")"; // Index 6
                LoanReportDataGrid.Columns["loanDayInterestPayment"].HeaderText = "Interest Payment \n(" + LoanCurrency + ")"; // Index 7
                LoanReportDataGrid.Columns["loanDayPrincipalPayment"].HeaderText = "Principal Payment \n(" + LoanCurrency + ")"; // Index 8
                LoanReportDataGrid.Columns["loanDayCurrentBalance"].HeaderText = "Current Balance \n(" + LoanCurrency + ")"; // Index 9
                LoanReportDataGrid.Columns["loanDayComments"].HeaderText = "Comments"; // Index 10
            }
        }
    }
}
