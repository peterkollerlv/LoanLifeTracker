using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Collections;
using System.Windows.Controls;

namespace InterestTracker
{
    public class GeneratePdf
    {
        private LoanReportData loanReportDataObj;
        public DataGrid GridLoanCalclation;
        public GeneratePdf(LoanReportData loanReportDataObj)
        {
            this.loanReportDataObj = loanReportDataObj;
        }

        private string displayingPayments;
        private string reportText;
        public string ExError;


        private List<string[]> reportRows = new List<string[]>();

        public string PdfSavePath { get; internal set; }

        internal void BuildPDF()
        {
            string[] gridRow = new string[loanReportDataObj.LoanReportDataGrid.Columns.Count];
            for (int i = 0; i < loanReportDataObj.LoanReportDataGrid.Columns.Count; ++i)
            {
                gridRow[i] = loanReportDataObj.LoanReportDataGrid.Columns[i].Header.ToString();
            }
            reportRows.Add(gridRow);
            var rows = GetDataGridRows(loanReportDataObj.LoanReportDataGrid);
            //var rows = GetDataGridRows(loanReportDataObj.LoanReportDataGrid);
            foreach (System.Windows.Controls.DataGridRow row in rows)
            {
                gridRow = null;
                gridRow = new string[loanReportDataObj.LoanReportDataGrid.Columns.Count];
                DataRowView rowView = (DataRowView)row.Item;
                for (int i = 0; i < loanReportDataObj.LoanReportDataGrid.Columns.Count; i++)
                {
                   System.Windows.Controls.TextBlock textBlock = loanReportDataObj.LoanReportDataGrid.Columns[i].GetCellContent(row) as System.Windows.Controls.TextBlock;
                    gridRow[i] = textBlock.Text;
                }
                reportRows.Add(gridRow);
            }
        //    buildPdf();
        //}



        //private void buildPdf()
        //{

            if (loanReportDataObj.DisplayPaymentsChk)
            {
                displayingPayments = "Displaying Payments: Yes";
            }
            else if (!loanReportDataObj.DisplayPaymentsChk)
            {
                displayingPayments = "Displaying Payments: No";
            }

            //string pdfName = loanReportDataObj.Title + " " + loanReportDataObj.ReportStartDate.Month + loanReportDataObj.ReportStartDate.Day + loanReportDataObj.ReportStartDate.Year +
            //    " - " + loanReportDataObj.ReportEndDate.Month + loanReportDataObj.ReportEndDate.Day + loanReportDataObj.ReportEndDate.Year + ".pdf";


            System.Drawing.Image freewayLogoFromRescources = System.Drawing.Image.FromHbitmap(Properties.Resources.FreewayLogoPlain.GetHbitmap());
            
            Document exportPdfDocument = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {

                reportText = "\n\n\n\n\n\n\n" + "Title: " + loanReportDataObj.Title + "\n" +
    "Company Info: " + " " + loanReportDataObj.CompanyInfo + "\n" +
    "Lender: " + " " + loanReportDataObj.Lender + "\n" +
    "Beneficiary: " + " " + loanReportDataObj.Beneficiary + "\n" +
    "Collection Account: " + " " + loanReportDataObj.CollectionAccount + "\n" +
    "Initial Loan Amount: " + loanReportDataObj.Currency + " " + loanReportDataObj.InitialLoanAmount.ToString() + "\n" +
    "Loan Start Date: " + " " + loanReportDataObj.StartDate.ToShortDateString() + "\n" +
    "Interest Structure: " + " " + loanReportDataObj.InterestStructureSelection + "\n" +
    "Report Range: " + loanReportDataObj.ReportStartDate.ToShortDateString() + " - " + loanReportDataObj.ReportEndDate.ToShortDateString() + "\n" +
    displayingPayments + "\n\n";

                iTextSharp.text.Image freewayLogo = iTextSharp.text.Image.GetInstance(freewayLogoFromRescources, System.Drawing.Imaging.ImageFormat.Png);
                freewayLogo.Alignment = iTextSharp.text.Image.UNDERLYING | Element.ALIGN_TOP | Element.ALIGN_CENTER;
                freewayLogo.ScalePercent(15f);
                PdfWriter.GetInstance(exportPdfDocument, new FileStream(PdfSavePath, FileMode.Create));
                PdfPTable pdfTable = new PdfPTable(loanReportDataObj.LoanReportDataGrid.Columns.Count);
                pdfTable.WidthPercentage = 100;
                Font reportFont = FontFactory.GetFont("Arial", 7f);
                Font headerFont = FontFactory.GetFont("Arial", 8f, 1);
                Paragraph reportTextBlock = new Paragraph(reportText, reportFont);
                reportTextBlock.SetLeading(9f, 0f);

                foreach (string headerCell in reportRows[0])
                {
                    PdfPCell headerPdfCell = new PdfPCell(new Phrase(headerCell, headerFont)); //headerFont
                    pdfTable.AddCell(headerPdfCell);
                }
                foreach (string[] reportRow in reportRows)
                {
                    if (!reportRow[0].Contains("Start Date"))
                    {
                        foreach (string reportCell in reportRow)
                        {
                            PdfPCell pdfCell = new PdfPCell(new Phrase(reportCell, reportFont));
                            pdfCell.FixedHeight = 20f;
                            pdfTable.AddCell(pdfCell);
                        }
                    }
                }
                exportPdfDocument.Open();
                exportPdfDocument.Add(freewayLogo);
                exportPdfDocument.Add(reportTextBlock);

                exportPdfDocument.Add(pdfTable);
            }

            catch (Exception ex)
            {
                ExError = ex.Message;
            }
            finally
            {
                exportPdfDocument.Close();
                Process.Start(PdfSavePath);
            }
        }


        public IEnumerable<System.Windows.Controls.DataGridRow> GetDataGridRows(System.Windows.Controls.DataGrid grid)
        {
            var itemsource = grid.ItemsSource as IEnumerable;
            if (null == itemsource) yield return null;
            foreach (var item in itemsource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as System.Windows.Controls.DataGridRow;
                if (null != row) yield return row;
            }

        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numberOfVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numberOfVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }

            }
            return child;
        }
    }
}
