using Microsoft.Office.Interop.Excel;
using System;
using System.Data;


namespace InterestTracker
{
    internal class GenerateExcel
    {
        private LoanReportData loanReportDataObj;
        private Application excel;
        private Workbook workBook;

        public string PdfSavePath { get; internal set; }

        public GenerateExcel(LoanReportData loanReportDataObj)
        {
            this.loanReportDataObj = loanReportDataObj;

        }

        internal void BuildExcel()
        {
            excel = new Application();
            workBook = excel.Workbooks.Add();
            Worksheet reportView = workBook.ActiveSheet;
            Worksheet loanCalculation = workBook.Worksheets.Add();
            Worksheet loanInfo = workBook.Worksheets.Add();
            loanInfo.Name = "Loan Info";
            loanCalculation.Name = "Calculation";
            reportView.Name = "Report View";


            loanInfo.Cells[1, 1] = "Title:";
            loanInfo.Cells[2, 1] = "Comany Info:";
            loanInfo.Cells[3, 1] = "Lender:";
            loanInfo.Cells[4, 1] = "Beneficiary:";
            loanInfo.Cells[5, 1] = "Collection Account:";
            loanInfo.Cells[6, 1] = "Initial Loan Amount:";
            loanInfo.Cells[7, 1] = "Loan Start Date:";
            loanInfo.Cells[8, 1] = "Interest Structure:";
            loanInfo.Cells[9, 1] = "Report Range:";
            loanInfo.Cells[10, 1] = "Displaying Payments:";
            loanInfo.Cells[11, 1] = "Unique Loan ID:";

            loanInfo.Cells[1, 2] = loanReportDataObj.Title;
            loanInfo.Cells[2, 2] = loanReportDataObj.CompanyInfo;
            loanInfo.Cells[3, 2] = loanReportDataObj.Lender;
            loanInfo.Cells[4, 2] = loanReportDataObj.Beneficiary;
            loanInfo.Cells[5, 2] = loanReportDataObj.CollectionAccount;
            loanInfo.Cells[6, 2] = loanReportDataObj.Currency + " " + loanReportDataObj.InitialLoanAmount.ToString();
            loanInfo.Cells[7, 2] = loanReportDataObj.StartDate.ToShortDateString();
            loanInfo.Cells[8, 2] = loanReportDataObj.InterestStructureSelection;
            loanInfo.Cells[9, 2] = loanReportDataObj.ReportStartDate.ToShortDateString() + " - " + loanReportDataObj.ReportEndDate.ToShortDateString();


            switch (loanReportDataObj.DisplayPaymentsChk)
            {
                case true:
                    loanInfo.Cells[10, 2] = "Yes";
                    break;
                case false:
                    loanInfo.Cells[10, 2] = "No";
                    break;
            }

            loanInfo.Cells[11, 2] = loanReportDataObj.LoanGuid.ToString();

            for (int i = 0; i < loanReportDataObj.LoanDataTable.Columns.Count; i++)
            {
                loanCalculation.Cells[1, i + 1] = loanReportDataObj.LoanDataTable.Columns[i].ColumnName;
                reportView.Cells[1, i + 1] = loanReportDataObj.LoanDataTable.Columns[i].ColumnName;
            }

            for (int i = 0; i < loanReportDataObj.LoanDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < loanReportDataObj.LoanDataTable.Columns.Count; j++)
                {
                    if (j > 0 && j < 10)
                    {
                        loanCalculation.Cells[i + 2, j + 1] = String.Format("{0:#,##0.##}", loanReportDataObj.LoanDataTable.Rows[i][j]);
                    }
                    else
                    {
                        loanCalculation.Cells[i + 2, j + 1] = loanReportDataObj.LoanDataTable.Rows[i][j];
                    }
                }
            }

            for (int i = 0; i < loanReportDataObj.ReportViewTable.Rows.Count; i++)
            {
                for (int j = 0; j < loanReportDataObj.ReportViewTable.Columns.Count; j++)
                {
                    if (j > 0 && j < 10)
                    {
                        reportView.Cells[i + 2, j + 1] = String.Format("{0:#,##0.##}", loanReportDataObj.ReportViewTable.Rows[i][j]);
                    }
                    else
                    {
                        reportView.Cells[i + 2, j + 1] = loanReportDataObj.ReportViewTable.Rows[i][j];
                    }
                }
            }
            loanInfo.Columns.AutoFit();
            loanCalculation.Columns.AutoFit();
            reportView.Columns.AutoFit();

            try
            {
                //string savePath = @"C:\test\"+loanReportDataObj.Title + "_" + loanReportDataObj.StartDate.Day + loanReportDataObj.StartDate.Month + loanReportDataObj.StartDate.Year + ".xlsx";
                excel.DisplayAlerts = false;
                workBook.SaveAs(PdfSavePath);
                excel.Visible = true;
                // excel.Quit();
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
        }

    }
}