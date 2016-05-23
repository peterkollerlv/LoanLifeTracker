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

        private List<string[]> reportText;
        private List<string[]> reportRows = new List<string[]>();
        private string[] displayingPayments;
        public string PdfSavePath { get; internal set; }
        public string ExError;

        internal void BuildPDF()
        {
            string[] gridRow = new string[loanReportDataObj.LoanReportDataGrid.Columns.Count];
            for (int i = 0; i < loanReportDataObj.LoanReportDataGrid.Columns.Count; ++i)
            {
                gridRow[i] = loanReportDataObj.LoanReportDataGrid.Columns[i].Header.ToString();
            }
            reportRows.Add(gridRow);
            var rows = GetDataGridRows(loanReportDataObj.LoanReportDataGrid);
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

            if (loanReportDataObj.DisplayPaymentsChk)
            {
                displayingPayments = new string[] { "Displaying Payments:", "Yes" };
            }
            else if (!loanReportDataObj.DisplayPaymentsChk)
            {
                displayingPayments = new string[] { "Displaying Payments:", "No" };
            }

            System.Drawing.Image freewayLogoFromRescources = System.Drawing.Image.FromHbitmap(Properties.Resources.FreewayLogoPlain.GetHbitmap());
            Font reportFont = FontFactory.GetFont("Arial", 7f);
            Font headerFont = FontFactory.GetFont("Arial", 8f, 1);

            Document exportPdfDocument = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {
                reportText = new List<string[]>();
                string[] newLine = new string[] { "Title:", loanReportDataObj.Title };
                reportText.Add(newLine);
                newLine = new string[] { "Company Info:", loanReportDataObj.CompanyInfo };
                reportText.Add(newLine);
                newLine = new string[] { "Lender:", loanReportDataObj.Lender };
                reportText.Add(newLine);
                newLine = new string[] { "Beneficiary:", loanReportDataObj.Beneficiary };
                reportText.Add(newLine);
                newLine = new string[] { "Collection Account:", loanReportDataObj.CollectionAccount };
                reportText.Add(newLine);
                newLine = new string[] { "Initial Loan Amount:", loanReportDataObj.Currency + " " + loanReportDataObj.InitialLoanAmount.ToString() };
                reportText.Add(newLine);
                newLine = new string[] { "Loan Start Date:", loanReportDataObj.StartDate.ToShortDateString() };
                reportText.Add(newLine);
                newLine = new string[] { "Interest Structure:", loanReportDataObj.InterestStructureSelection };
                reportText.Add(newLine);
                newLine = new string[] { "Report Range:", loanReportDataObj.ReportStartDate.ToShortDateString() + " - " + loanReportDataObj.ReportEndDate.ToShortDateString() };
                reportText.Add(newLine);
                reportText.Add(displayingPayments);

                PdfPTable titleInfoTable = new PdfPTable(2);
                float[] cellWidth = new float[] { 40f, 100f };
                foreach (string[] titeInfoItem in reportText)
                {
                    PdfPCell titleInfoCell;
                    PdfPCell[] titleInfoCells = new PdfPCell[2];
                    titleInfoCell = new PdfPCell(new Phrase(titeInfoItem[0], reportFont));
                    titleInfoCells[0] = titleInfoCell;
                    titleInfoCell.BorderWidth = 0f;
                    titleInfoCell.Padding = 0f;
                    titleInfoCell = new PdfPCell(new Phrase(titeInfoItem[1], reportFont));
                    titleInfoCells[1] = titleInfoCell;
                    titleInfoCell.BorderWidth = 0;
                    titleInfoCell.Padding = 0f;
                    PdfPRow titleInfoRow = new PdfPRow(titleInfoCells);
                    titleInfoTable.Rows.Add(titleInfoRow);
                }
                titleInfoTable.SetWidths(cellWidth);
                Paragraph reportTextBlock = new Paragraph();
                Paragraph reportBody = new Paragraph();
                iTextSharp.text.Image freewayLogo = iTextSharp.text.Image.GetInstance(freewayLogoFromRescources, System.Drawing.Imaging.ImageFormat.Png);
                freewayLogo.Alignment = iTextSharp.text.Image.UNDERLYING | Element.ALIGN_TOP | Element.ALIGN_CENTER;
                freewayLogo.ScalePercent(15f);
                PdfWriter.GetInstance(exportPdfDocument, new FileStream(PdfSavePath, FileMode.Create));
                PdfPTable pdfTable = new PdfPTable(loanReportDataObj.LoanReportDataGrid.Columns.Count);
                pdfTable.WidthPercentage = 100;
                titleInfoTable.HorizontalAlignment = Element.ALIGN_LEFT;
                reportTextBlock.Add(titleInfoTable);

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
                reportTextBlock.SpacingBefore = 50f;
                reportBody.Add(pdfTable);
                reportBody.SpacingBefore = 1f;

                exportPdfDocument.Open();
                exportPdfDocument.Add(freewayLogo);
                exportPdfDocument.Add(reportTextBlock);
                exportPdfDocument.Add(reportBody);
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
