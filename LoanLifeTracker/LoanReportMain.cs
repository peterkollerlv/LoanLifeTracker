using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

//added git
//adding a fork and start with Payment GUI fixing
//revisiting constructor

namespace LoanLifeTracker
{
    
    public partial class LoanReportMain : Form
    {
        private string loanTitle;
        private int reportSelectedDuration;
        private bool loanCreated;
        private string reportText;
        private string displayingPayments;
        private LoanReportData loanReportData;
        public LoanReportData LoanReportData
        {
            get { return loanReportData; }
            set { }

        }
        public LoanReportMain()
        {
            InitializeComponent();
            LoanReportData loanReportData = new LoanReportData(this);           
        }

        private void LoanReportMain_Load(object sender, EventArgs e)
        {
            
            applicationInitialState();
        }

        public void applicationInitialState()
        {
            loanReportData = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            loanReportData = new LoanReportData(this);
            loanReportDataGrid.DataSource = null;
            loanReportData.LoanReportDataGrid = loanReportDataGrid;
            inputLoanPanelSelection.SelectedIndex = 0;
            inputCurrencySelection.SelectedIndex = 0;
            inputReportType.SelectedIndex = 0;
            statusProgressBar.Value = 0;
            inputInterestRate.Value = Convert.ToDecimal(5.00);
            inputInterestPenaltyRate.Value = Convert.ToDecimal(10.00);
            inputLoanPanelSelection.Visible = false;
            inputLoanTitle.Text = "";
            inputCompanyInfo.Text = "";
            inputLender.Text = "";
            inputBeneficiary.Text = "";
            inputCollectionAccount.Text = "";
            inputLoanStartDate.Value = DateTime.Now.Date;
            inputLoanDuration.Value = 5;
            inputInitialLoanAmount.Text = "";
            labelLoanTitleInfo.Text = inputLoanTitle.Text;
            labelCompanyInfo.Text = inputCompanyInfo.Text;
            labelLenderInfo.Text = inputLender.Text;
            labelBeneficiaryInfo.Text = inputBeneficiary.Text;
            labelCollectionAccountInfo.Text = inputCollectionAccount.Text;
            labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + inputCurrencySelection.SelectedItem.ToString();
            labelInterestStructureInfo.Text = "";
            labelLoanStartDateInfo.Text = "";
            labelTodayDateInfo.Text = DateTime.Now.Date.ToLongDateString();
            inputInterestPenaltyChk.Checked = true;
            inputRateToWholeDurationChk.Checked = true;
            inputCalculateLoan.Enabled = false;
            groupReportActions.Enabled = false;
            groupLoanInput.Enabled = false;
            loanCreated = false;
            groupVaryingInterestTerm.Visible = false;
            groupFinancialData.Enabled = false;
            groupInterestConfig.Enabled = false;
            buttonOpenAddPayment.Visible = false;
            buttonOpenPrincipalAdjust.Visible = false;
            groupReportControls.Enabled = false;
            inputReportSortWeeks.Checked = true;
            inputRateToWholeDurationChk.Enabled = false;
            inputSaveLoan.Visible = false;

        }



        public void recalculateLoan()
        {
            
            statusProgressBar.Value = 0;
            
            inputLoanStartDate.Enabled = false;
            inputLoanDuration.Enabled = true;
            loanReportData.RegenerateLoanTable = true;
            groupReportActions.Enabled = true;
            groupReportControls.Enabled = true;
            buttonOpenAddPayment.Visible = true;
            buttonOpenPrincipalAdjust.Visible = true;
            loanReportData.CalculateLoan();
           // inputInitialLoanAmount.Text = "";
        }

        private void getReportDuration(int selectedDuration)
        {
            if (groupReportControls.Enabled)
            {
             
                loanReportData.SortDataGridToReport(inputReportStartDate.Value.Date, inputReportEndDate.Value.Date, selectedDuration);
            }
        }

        private void inputLoanTitle_KeyUp(object sender, KeyEventArgs e)
        {
            loanTitle = inputLoanTitle.Text;
            labelLoanTitleInfo.Text = inputLoanTitle.Text;
        }

        private void inputReportStartDate_ValueChanged(object sender, EventArgs e)
        {
            getReportDuration(reportSelectedDuration);
        }

        private void inputReportEndDate_ValueChanged(object sender, EventArgs e)
        {
            getReportDuration(reportSelectedDuration);
        }




        private void inputReportSortDays_CheckedChanged(object sender, EventArgs e)
        {
            if (inputReportSortDays.Checked)
            {
                reportSelectedDuration = 0;
                getReportDuration(reportSelectedDuration);
            }
        }

        private void inputReportSortWeeks_CheckedChanged(object sender, EventArgs e)
        {
            if (inputReportSortWeeks.Checked)
            {
                reportSelectedDuration = 1;
                getReportDuration(reportSelectedDuration);
            }
        }

        private void inputReportSortMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (inputReportSortMonths.Checked)
            {
                reportSelectedDuration = 2;
                getReportDuration(reportSelectedDuration);
            }
        }

        private void inputReportSortYears_CheckedChanged(object sender, EventArgs e)
        {
            if (inputReportSortYears.Checked)
            {
                reportSelectedDuration = 3;
                getReportDuration(reportSelectedDuration);
            }
        }

        private void inputDisplayPaymentsChk_CheckedChanged(object sender, EventArgs e)
        {
            getReportDuration(reportSelectedDuration);
        }

        private void inputCurrencySelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            loanReportData.LoanCurrency = inputCurrencySelection.SelectedItem.ToString();
            labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + inputCurrencySelection.SelectedItem.ToString();

        }

        private void inputReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loanReportData.ReportType = inputReportType.SelectedIndex;
        }

        private void inputCalculateLoan_Click(object sender, EventArgs e)
        {
            recalculateLoan();
        }

        private void inputNewLoan_Click(object sender, EventArgs e)
        {
            if (!loanCreated)
            {
                inputLoanStartDate.Enabled = true;
                inputLoanDuration.Enabled = true;
                loanReportData.RegenerateLoanTable = true;
                inputCalculateLoan.Enabled = true;
                loanReportData.LoanGenerated = true;
                groupLoanInput.Enabled = true;
                groupFinancialData.Enabled = true;
                groupInterestConfig.Enabled = true;
                inputInterestStructureSelection.SelectedIndex = 0;
                labelInterestStructureInfo.Text = inputInterestStructureSelection.SelectedItem.ToString();
                inputCompanyInfo.SelectedIndex = 0;
                statusProgressBar.Value = 0;
                labelCompanyInfo.Text = inputCompanyInfo.SelectedItem.ToString();
                labelLoanStartDateInfo.Text = inputLoanStartDate.Value.ToLongDateString();
                inputLoanPanelSelection.Visible = true;
                loanReportData.CreateLoanObjects();
                inputNewLoan.Text = "Clear Loan";
                inputSaveLoan.Visible = true;
                loanCreated = true;
            }
            else if (loanCreated)
            {
                inputNewLoan.Text = "New Loan";
                applicationInitialState();

            }
        }


        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void buttonExportToPdf_Click(object sender, EventArgs e)
        {

            if (inputDisplayPaymentsChk.Checked)
            {
                displayingPayments = "Displaying Payments: Yes";
            }
            else if (!inputDisplayPaymentsChk.Checked)
            {
                displayingPayments = "Displaying Payments: No";
            }

            string pdfName = inputLoanTitle.Text + " " + inputReportStartDate.Value.Month + inputReportStartDate.Value.Day + inputReportStartDate.Value.Year +
                " - " + inputReportEndDate.Value.Month + inputReportEndDate.Value.Day + inputReportEndDate.Value.Year + ".pdf";
            System.Drawing.Image freewayLogoFromRescources = System.Drawing.Image.FromHbitmap(LoanLifeTracker.Properties.Resources.FreewayLogoWhiteBackGround.GetHbitmap());

            Document exportPdfDocument = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            try
            {

                reportText = "\n\n\n\n\n\n\n" + labelTitle.Text + " " + labelLoanTitleInfo.Text + "\n" +
    labelCompany.Text + " " + labelCompanyInfo.Text + "\n" +
    labelLander.Text + " " + labelLenderInfo.Text + "\n" +
    labelBeneficiary.Text + " " + labelBeneficiaryInfo.Text + "\n" +
    labelCollection.Text + " " + labelCollectionAccountInfo.Text + "\n" +
    labelInitialLoan.Text + " " + labelInitialLoanAmountInfo.Text + "\n" +
    labelLoanStart.Text + " " + labelLoanStartDateInfo.Text + "\n" +
    labelInterestStruct.Text + " " + labelInterestStructureInfo.Text + "\n" +
    "Report Range: " + inputReportStartDate.Value.ToShortDateString() + " - " + inputReportEndDate.Value.ToShortDateString() + "\n" +
    displayingPayments + "\n\n";

                Image freewayLogo = Image.GetInstance(freewayLogoFromRescources, System.Drawing.Imaging.ImageFormat.Png);
                freewayLogo.Alignment = Image.UNDERLYING | Element.ALIGN_TOP | Element.ALIGN_CENTER;
                freewayLogo.ScalePercent(10f);
                PdfWriter.GetInstance(exportPdfDocument, new FileStream(pdfName, FileMode.Create));
                PdfPTable pdfTable = new PdfPTable(loanReportDataGrid.ColumnCount);
                pdfTable.WidthPercentage = 100;
                Font reportFont = FontFactory.GetFont("Arial", 8f);
                Font headerFont = FontFactory.GetFont("Arial", 9f, 1);
                Paragraph reportTextBlock = new Paragraph(reportText, reportFont);
                reportTextBlock.SetLeading(9f, 0f);

                foreach (DataGridViewColumn headerCell in loanReportDataGrid.Columns)
                {
                    PdfPCell headerPdfCell = new PdfPCell(new Phrase(headerCell.HeaderText, headerFont)); //headerFont
                    pdfTable.AddCell(headerPdfCell);
                }
                foreach (DataGridViewRow gridRow in loanReportDataGrid.Rows)
                {
                    foreach (DataGridViewCell gridCell in gridRow.Cells)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridCell.Value.ToString(), reportFont));
                        pdfCell.FixedHeight = 20f;
                        pdfTable.AddCell(pdfCell);
                    }
                }
                exportPdfDocument.Open();
                exportPdfDocument.Add(freewayLogo);
                exportPdfDocument.Add(reportTextBlock);

                exportPdfDocument.Add(pdfTable);
            }

            catch (Exception ex)
            {

            }
            finally
            {
                exportPdfDocument.Close();
                Process.Start(pdfName);
            }
        }

        private void buttonImportFromExcel_Click(object sender, EventArgs e)
        {

        }

        private void inputLoanPenaltyChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!inputInterestPenaltyChk.Checked)
            {
                groupInterestPenalty.Visible = false;
            }
            else
            {
                groupInterestPenalty.Visible = true;
            }
        }

        private void inputRateToWholeDurationChk_CheckedChanged(object sender, EventArgs e)
        {
            if (inputRateToWholeDurationChk.Checked)
            {
                groupVaryingInterestTerm.Visible = false;
            }
            else
            {
                groupVaryingInterestTerm.Visible = true;
            }
        }

        private void inputCompanyInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelCompanyInfo.Text = inputCompanyInfo.SelectedItem.ToString();
        }

        private void inputLender_KeyUp(object sender, KeyEventArgs e)
        {
            labelLenderInfo.Text = inputLender.Text;
        }

        private void inputBeneficiary_KeyUp(object sender, KeyEventArgs e)
        {
            labelBeneficiaryInfo.Text = inputBeneficiary.Text;
        }

        private void inputCollectionAccount_KeyUp(object sender, KeyEventArgs e)
        {
            labelCollectionAccountInfo.Text = inputCollectionAccount.Text;
        }

        private void inputLoanStartDate_ValueChanged(object sender, EventArgs e)
        {
            inputReportStartDate.Value = inputLoanStartDate.Value.Date;
            inputReportEndDate.Value = inputLoanStartDate.Value.Date.AddYears(1);
            labelLoanStartDateInfo.Text = inputLoanStartDate.Value.ToLongDateString();

        }

        private void inputInterestStructureSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelInterestStructureInfo.Text = inputInterestStructureSelection.SelectedItem.ToString();
        }


        private void controlsFileExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonOpenAddPayment_Click(object sender, EventArgs e)
        {
            LoanAdjustments loanAdjustments = new LoanAdjustments(this, loanReportData.LoanDataTable); 
            loanAdjustments.labelPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            loanAdjustments.labelInterestPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            loanAdjustments.labelPrincipalPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            loanAdjustments.Text = "Payments";
            loanAdjustments.panelPrincipleAdjust.Visible = false;
            loanAdjustments.panelAddPayment.Visible = true;
            loanAdjustments.Show();
        }

        private void buttonOpenPrincipalAdjust_Click(object sender, EventArgs e)
        {
            LoanAdjustments loanAdjustments = new LoanAdjustments(this, loanReportData.LoanDataTable);
            //loanAdjustments.labelPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            //loanAdjustments.labelInterestPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            //loanAdjustments.labelPrincipalPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            loanAdjustments.Text = "Principal";
            loanAdjustments.panelAddPayment.Visible = false;
            loanAdjustments.panelPrincipleAdjust.Visible = true;
            loanAdjustments.Show();
        }

        private void inputLoanPanelSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch(inputLoanPanelSelection.SelectedIndex)
            {
                case 0:
                    {
                        panelLoandDataGrid.Visible = false;
                        panelLoanDataDisplay.Visible = true;
                        panelLoanData.Visible = true;
                        panelLoanConfig.Visible = false;
                        panelReport.Visible = false;
                                               
                        break;
                    }
                case 1:
                    {
                        panelLoandDataGrid.Visible = true;
                        panelLoanDataDisplay.Visible = false;
                        panelLoanData.Visible = false;
                        panelLoanConfig.Visible = true;
                        panelReport.Visible = false;
                        loanReportDataGrid.ReadOnly = false;
                        break;
                    }
                case 2:
                    {
                        panelLoandDataGrid.Visible = true;
                        panelLoanDataDisplay.Visible = false;
                        panelLoanData.Visible = false;
                        panelLoanConfig.Visible = false;
                        panelReport.Visible = true;
                        loanReportDataGrid.ReadOnly = true;

                        inputReportSortMonths.Checked = true;
                        getReportDuration(reportSelectedDuration);

                        break;
                    }
            }
                
        }

        private void inputInitialLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateForDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputInitialLoanAmount_TextChanged(object sender, EventArgs e)
        {
            labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + inputCurrencySelection.SelectedItem.ToString();
        }
    }
}
