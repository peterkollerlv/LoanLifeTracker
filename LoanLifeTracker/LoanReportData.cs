﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace LoanLifeTracker
{
    public class LoanReportData
    {
        LoanReportMain LoanReportMainObj;
        public LoanReportData(LoanReportMain loanReportMain)
        {
            LoanReportMainObj = loanReportMain;
        }
        private bool firstRowSet = false;
        
        public string LoanCurrency
        {
            get { return LoanReportMainObj.inputCurrencySelection.SelectedText; }
            set { }
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
            set { }
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
        }
        public decimal RecuringPaymentAmount { get; set; }
        public DataGridView LoanReportDataGrid { get; set; }
        //public string LoanTitle { get; set; }
        public string SelectedTabControl;
        public DataTable LoanDataTable;
        public bool RegenerateLoanTable;
        public bool LoanGenerated;

        //Generate the loan objects, and populate the data grid colums

        public void CreateLoanObjects()
        {
            LoanDataTable = new DataTable();
            firstRowSet = false;
            LoanDataTable.Columns.Add("loanDayDate", typeof(DateTime));
            LoanDataTable.Columns.Add("loanDayPrincipal", typeof(decimal));
           // LoanDataTable.Columns.Add("principalCurrency",typeof(string));
            LoanDataTable.Columns.Add("loanDayInterestRate", typeof(decimal));
            LoanDataTable.Columns.Add("loanDayInterest", typeof(decimal));
            LoanDataTable.Columns.Add("loanDayCuIntrestBal", typeof(decimal));
            LoanDataTable.Columns.Add("loanDayTotalPayment", typeof(decimal)); ;
            LoanDataTable.Columns.Add("loanDayInterestPayment", typeof(decimal));
            LoanDataTable.Columns.Add("loanDayPrincipalPayment", typeof(decimal)); ;
            LoanDataTable.Columns.Add("loanDayCurrentBalance", typeof(decimal));
            LoanDataTable.Columns.Add("loanDayComments", typeof(string));
            LoanDataTable.PrimaryKey = new DataColumn[] { LoanDataTable.Columns["loanDayDate"] };
        }


        // interest structure and rate calculation

        private decimal calculateInterest(decimal interestRate, decimal principal, int structureSelected, DateTime date) //geting date to find out the lenght of the year
        {
            decimal interest = 0;
            int numberOfDaysInDateYear = getLastDayOfYear(date);

            switch (structureSelected)
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
                    return interest;
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
                if (LoanGenerated)
                {
                    DateTime indexingDate = LoanStartDate;
                    DateTime indexEndDate = LoanEndDate;
                    LoanReportMainObj.statusProgressBar.Maximum = LoanTimeSpan.Days;
                    LoanReportMainObj.statusProgressBar.Value = 0;
                    LoanReportMainObj.loanReportDataGrid.DataSource = null; //testing nulling the data source
                    LoanGenerated = false;
                    if (RegenerateLoanTable)
                    {
                        for (DateTime dates = LoanStartDate; dates < LoanEndDate; dates = dates.AddDays(1), LoanReportMainObj.statusProgressBar.Value += 1)
                        {
                            bool rowExists = LoanDataTable.Rows.Contains(dates);
                            var calculatedPrinciple = 0M;

                            switch (rowExists)
                            {
                                //this section adds the rows
                                case false:
                                    if (!firstRowSet)
                                    {
                                        LoanDataTable.Rows.Add(dates.Date,
                                            InitialLoanAmount,
                                            //LoanCurrency,
                                            InterestRate(dates.Date),
                                            decimal.Round(calculateInterest(InterestRate(dates.Date), InitialLoanAmount, InterestStructureSelection, dates), 2, MidpointRounding.AwayFromZero),
                                             decimal.Round(calculateInterest(InterestRate(dates.Date), InitialLoanAmount, InterestStructureSelection, dates), 2, MidpointRounding.AwayFromZero),
                                            0, 0, 0, InitialLoanAmount, "");
                                        firstRowSet = true;
                                    }
                                    else
                                    {
                                        dates.AddDays(1);
                                        calculatedPrinciple = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[1]), 2, MidpointRounding.AwayFromZero) - decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[7]), 2, MidpointRounding.AwayFromZero);
                                        LoanDataTable.Rows.Add(dates.Date,
                                            calculatedPrinciple,
                                            //LoanCurrency,
                                            InterestRate(dates.Date),
                                            decimal.Round(calculateInterest(InterestRate(dates.Date), InitialLoanAmount, InterestStructureSelection, dates), 2, MidpointRounding.AwayFromZero),
                                            0, 0, 0, 0, 0, "");
                                        LoanDataTable.Rows.Find(dates)[4] = Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[3]) + Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[4]) - Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[6]);
                                        if (InterestStructureSelection == 2)
                                        {
                                            if (dates.Day == getLastDayOfMonth(dates))
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[4]), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero);
                                                LoanDataTable.Rows.Find(dates)[1] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[8]), 2, MidpointRounding.AwayFromZero);
                                            }
                                            else
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = LoanDataTable.Rows.Find(dates.AddDays(-1))[8];
                                            }
                                        }
                                        else
                                        {
                                            if (dates.Day == getLastDayOfMonth(dates))
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[4]), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero);
                                            }
                                            else
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = LoanDataTable.Rows.Find(dates.AddDays(-1))[8];
                                            }
                                        }
                                        if (dates == LoanEndDate.AddDays(-1))
                                        { LoanReportMainObj.statusProgressBar.Value = 0; }
                                    }
                                    goto default;

                                //this section changes the rows if they exists
                                case true:
                                    if (dates == LoanStartDate)
                                    {
                                        LoanDataTable.Rows.Find(dates)[1] = decimal.Round(InitialLoanAmount, 2, MidpointRounding.AwayFromZero);
                                        LoanDataTable.Rows.Find(dates)[2] = InterestRate(dates.Date);
                                        LoanDataTable.Rows.Find(dates)[3] = decimal.Round(calculateInterest(InterestRate(dates.Date),
                                            decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero), InterestStructureSelection, dates), 2, MidpointRounding.AwayFromZero);
                                        LoanDataTable.Rows.Find(dates)[4] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[4]), 2, MidpointRounding.AwayFromZero) - decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[6]), 2, MidpointRounding.AwayFromZero);
                                        LoanDataTable.Rows.Find(dates)[8] = decimal.Round(InitialLoanAmount, 2, MidpointRounding.AwayFromZero);
                                    }
                                    else if (dates != LoanStartDate)
                                    {
                                        LoanDataTable.Rows.Find(dates)[1] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[1]), 2, MidpointRounding.AwayFromZero); // calculatePrinciple;
                                        LoanDataTable.Rows.Find(dates)[1] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero) - decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[7]));
                                        LoanDataTable.Rows.Find(dates)[2] = InterestRate(dates.Date);
                                        LoanDataTable.Rows.Find(dates)[3] = decimal.Round(calculateInterest(InterestRate(dates.Date), decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero), InterestStructureSelection, dates), 2, MidpointRounding.AwayFromZero);
                                        LoanDataTable.Rows.Find(dates)[4] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[3]), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[4]), 2, MidpointRounding.AwayFromZero) - decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[6]), 2, MidpointRounding.AwayFromZero);
                                        if (InterestStructureSelection == 2)
                                        {
                                            if (dates.Day == getLastDayOfMonth(dates))
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[4]), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero);
                                                LoanDataTable.Rows.Find(dates)[1] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[8]), 2, MidpointRounding.AwayFromZero);
                                            }
                                            else
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[8]), 2, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                        else
                                        {
                                            if (dates.Day == getLastDayOfMonth(dates))
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[4]), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates)[1]), 2, MidpointRounding.AwayFromZero);
                                            }
                                            else
                                            {
                                                LoanDataTable.Rows.Find(dates)[8] = decimal.Round(Convert.ToDecimal(LoanDataTable.Rows.Find(dates.AddDays(-1))[8]), 2, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                        if (dates == LoanEndDate.AddDays(-1))
                                        { LoanReportMainObj.statusProgressBar.Value = 0; }
                                    }
                                    goto default;
                                default:
                                    if (dates == LoanEndDate.AddDays(-1))
                                    {
                                        LoanGenerated = true;
                                        SortDataGridToReport(LoanStartDate, LoanEndDate, 2);

                                    }
                                    break;
                            }
                        }
                        LoanDataTable.AcceptChanges();
                    }
                    else if (!RegenerateLoanTable)
                    {
                        MessageBox.Show("Loan is not regenerated with new start date");
                    }
                }
            }
            catch (OverflowException e)
            {
                MessageBox.Show(e.ToString());
                return;
            }
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
                            addColumnHeaders();
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
                            addColumnHeaders();
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
                            addColumnHeaders();
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
                            addColumnHeaders();
                            goto default;
                        default:
                            if (SelectedTabControl == "Report")
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

                                LoanReportDataGrid.ReadOnly = true;
                            }
                            else
                            {
                                LoanReportDataGrid.ReadOnly = false;
                                LoanReportDataGrid.Refresh();
                            }
                            break;
                    }
                }
            }
            else
            { } // MessageBox.Show("Please generate the loan first."); }
        }

        private void addColumnHeaders()
        {
            
            LoanReportDataGrid.Columns["loanDayDate"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            LoanReportDataGrid.Columns["loanDayDate"].DefaultCellStyle.Format = "MMMM dd, yyyy";
            LoanReportDataGrid.Columns["loanDayInterestRate"].DefaultCellStyle.Format = "p2";   // #0.00 %
            LoanReportDataGrid.Columns["loanDayPrincipal"].DefaultCellStyle.Format = "c0";
            LoanReportDataGrid.Columns["loanDayInterest"].DefaultCellStyle.Format = "c2";
            LoanReportDataGrid.Columns["loanDayCuIntrestBal"].DefaultCellStyle.Format = "c0";
            LoanReportDataGrid.Columns["loanDayTotalPayment"].DefaultCellStyle.Format = "c0";
            LoanReportDataGrid.Columns["loanDayInterestPayment"].DefaultCellStyle.Format = "c0";
            LoanReportDataGrid.Columns["loanDayPrincipalPayment"].DefaultCellStyle.Format = "c0";
            LoanReportDataGrid.Columns["loanDayCurrentBalance"].DefaultCellStyle.Format = "c0";

            LoanReportDataGrid.Columns["loanDayDate"].HeaderText = "Date"; // Index 0
            LoanReportDataGrid.Columns["loanDayPrincipal"].HeaderText = "Principal"; // Index 1
            //LoanReportDataGrid.Columns["principalCurrency"].HeaderText = "";


            LoanReportDataGrid.Columns["loanDayInterestRate"].HeaderText = "Interest Rate"; // Index 2
            LoanReportDataGrid.Columns["loanDayInterest"].HeaderText = "Daily Interest"; // Index 3
            LoanReportDataGrid.Columns["loanDayCuIntrestBal"].HeaderText = "Cumulative Interes Balance"; // Index 4
            LoanReportDataGrid.Columns["loanDayTotalPayment"].HeaderText = "Total Payment"; // Index 5
            LoanReportDataGrid.Columns["loanDayInterestPayment"].HeaderText = "Interest Payment"; // Index 6
            LoanReportDataGrid.Columns["loanDayPrincipalPayment"].HeaderText = "Principal Payment"; // Index 7
            LoanReportDataGrid.Columns["loanDayCurrentBalance"].HeaderText = "Current Balance"; // Index 8
            LoanReportDataGrid.Columns["loanDayComments"].HeaderText = "Comments"; // Index 9

        }



    }
}
