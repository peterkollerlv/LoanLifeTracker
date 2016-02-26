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

namespace LoanLifeTracker
{
    public partial class LoanReportMain : Form
    {
        private int reportSelectedDuration;
        private string reportText;
        private string displayingPayments;
        public static bool LoanCreated;
        public LoanReportData LoanReportDataObj;
        private LoanAdjustments loanAdjustments;

        public LoanReportMain()
        {
            InitializeComponent();
            LoanReportData LoanReportDataObj = new LoanReportData(this);
            inputCurrencySelection.SelectedIndex = 0;
            inputLoanPanelSelection.SelectedIndex = 0;
            applicationInitialState();
        }

        public void applicationInitialState()
        {
            inputInterestRate.Value = Convert.ToDecimal(5.00);
            inputInterestPenaltyRate.Value = Convert.ToDecimal(10.00);
            inputLoanDuration.Value = 5;
            inputLoanStartDate.Value = DateTime.Now.Date;
            labelTodayDateInfo.Text = DateTime.Now.Date.ToLongDateString();
            inputLender.Text = "";
            inputBeneficiary.Text = "";
            inputCollectionAccount.Text = "";
            inputInitialLoanAmount.Text = "";
            inputLoanTitle.Text = "";
            labelInitialLoanAmountInfo.Text = "";
            labelInterestStructureInfo.Text = "";
            labelLoanStartDateInfo.Text = "";
            labelLoanTitleInfo.Text = "";
            labelCompanyInfo.Text = "";
            labelLenderInfo.Text = inputLender.Text;
            labelBeneficiaryInfo.Text = inputBeneficiary.Text;
            labelCollectionAccountInfo.Text = inputCollectionAccount.Text;
            inputInterestPenaltyChk.Checked = true;
            inputRateToWholeDurationChk.Checked = true;
            inputCalculateLoan.Enabled = false;
            groupReportActions.Enabled = false;
            groupLoanInput.Enabled = false;
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

        public void setCurrencyLabels()
        {
            if (LoanReportDataObj != null)
            {
                labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + LoanReportDataObj.LoanCurrency;
                if (loanReportDataGrid != null)
                {
                    LoanReportDataObj.SetColumnHeaders();
                }
                if (loanAdjustments != null)
                {
                    loanAdjustments.labelPaymentCurrency.Text = LoanReportDataObj.LoanCurrency;
                    loanAdjustments.labelInterestPaymentCurrency.Text = LoanReportDataObj.LoanCurrency;
                    loanAdjustments.labelPrincipalPaymentCurrency.Text = LoanReportDataObj.LoanCurrency;
                }
            }
        }

        public void recalculateLoan()
        {
            statusProgressBar.Value = 0;
            inputLoanStartDate.Enabled = false;
            inputLoanDuration.Enabled = true;
            LoanReportDataObj.RegenerateLoanTable = true;
            groupReportActions.Enabled = true;
            groupReportControls.Enabled = true;
            buttonOpenAddPayment.Visible = true;
            buttonOpenPrincipalAdjust.Visible = true;
            LoanReportDataObj.CalculateLoan();
        }

        private void getReportDuration(int selectedDuration)
        {
            if (groupReportControls.Enabled)
            {
                if (LoanReportDataObj != null)
                {
                    LoanReportDataObj.SortDataGridToReport(inputReportStartDate.Value.Date, inputReportEndDate.Value.Date, selectedDuration);
                }
            }
        }

        // loan details change events
        
        private void inputLoanTitle_KeyUp(object sender, KeyEventArgs e)
        {
            LoanReportDataObj.LoanTitle = inputLoanTitle.Text;
            labelLoanTitleInfo.Text = LoanReportDataObj.LoanTitle;
        }

        private void inputCompanyInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoanCreated)
            {
                LoanReportDataObj.CompanyInfo = inputCompanyInfo.SelectedItem.ToString();
                labelCompanyInfo.Text = LoanReportDataObj.CompanyInfo;
            }
        }

        private void inputLender_KeyUp(object sender, KeyEventArgs e)
        {
            LoanReportDataObj.Lender = inputLender.Text;
            labelLenderInfo.Text = LoanReportDataObj.Lender;
        }

        private void inputBeneficiary_KeyUp(object sender, KeyEventArgs e)
        {
            LoanReportDataObj.Beneficiary = inputBeneficiary.Text;
            labelBeneficiaryInfo.Text = LoanReportDataObj.Beneficiary;
        }

        private void inputCollectionAccount_KeyUp(object sender, KeyEventArgs e)
        {
            LoanReportDataObj.CollectionAccount = inputCollectionAccount.Text;
            labelCollectionAccountInfo.Text = LoanReportDataObj.CollectionAccount;
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
            if (LoanReportDataObj != null)
            {
                LoanReportDataObj.LoanCurrency = inputCurrencySelection.SelectedItem.ToString();
                setCurrencyLabels();
            }
        }

        private void inputReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoanReportDataObj != null)
            {
                LoanReportDataObj.ReportType = inputReportType.SelectedIndex;
            }
        }

        private void inputCalculateLoan_Click(object sender, EventArgs e)
        {
            recalculateLoan();
        }

        private void inputNewLoan_Click(object sender, EventArgs e)
        {
            if (!LoanCreated)
            {
                LoanReportDataObj = null;
                LoanReportDataObj = new LoanReportData(this);
                inputLoanStartDate.Enabled = true;
                inputLoanDuration.Enabled = true;
                LoanReportDataObj.RegenerateLoanTable = true;
                inputCalculateLoan.Enabled = true;
                LoanReportDataObj.LoanGenerated = true;
                groupLoanInput.Visible = true;
                groupLoanInput.Enabled = true;
                groupFinancialData.Enabled = true;
                groupInterestConfig.Enabled = true;
                inputInterestStructureSelection.SelectedIndex = 0;
                labelInterestStructureInfo.Text = inputInterestStructureSelection.SelectedItem.ToString();
                inputCompanyInfo.SelectedIndex = -1;
                labelLoanStartDateInfo.Text = inputLoanStartDate.Value.ToLongDateString();
                inputLoanPanelSelection.Visible = true;
                LoanReportDataObj.CreateLoanObjects();
                inputNewLoan.Text = "Clear Loan";
                navRight.Visible = true;
                navLeft.Visible = true;
                inputSaveLoan.Visible = true;
                panelLoanDetails.Visible = true;
                LoanCreated = true;
                setCurrencyLabels();
            }
            else if (LoanCreated)
            {
                LoanReportDataObj = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                inputNewLoan.Text = "New Loan";
                inputLoanPanelSelection.Visible = false;
                panelLoanDetails.Visible = false;
                groupLoanInput.Visible = false;
                navRight.Visible = false;
                navLeft.Visible = false;
                LoanCreated = false;
                inputCompanyInfo.SelectedIndex = -1;
                applicationInitialState();

            }
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
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                exportPdfDocument.Close();
                Process.Start(pdfName);
            }
        }

        private void buttonImportFromExcel_Click(object sender, EventArgs e)
        {
            // non implemented yet
        }

        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            // non implemented yet
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

        private void inputLoanStartDate_ValueChanged(object sender, EventArgs e)
        {
            inputReportStartDate.Value = inputLoanStartDate.Value.Date;
            inputReportEndDate.Value = inputLoanStartDate.Value.Date.AddYears((int)inputLoanDuration.Value);
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

        public void buttonOpenAddPayment_Click(object sender, EventArgs e)
        {
            loanAdjustments = new LoanAdjustments(this, LoanReportDataObj.LoanDataTable);
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
            LoanAdjustments loanAdjustments = new LoanAdjustments(this, LoanReportDataObj.LoanDataTable);
            loanAdjustments.Text = "Principal";
            loanAdjustments.panelAddPayment.Visible = false;
            loanAdjustments.panelPrincipleAdjust.Visible = true;
            loanAdjustments.Show();
        }

        private void inputLoanPanelSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (inputLoanPanelSelection.SelectedIndex)
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
                        getReportDuration(2);
                        break;
                    }
                case 2:
                    {
                        panelLoandDataGrid.Visible = true;
                        panelLoanDataDisplay.Visible = false;
                        panelLoanData.Visible = false;
                        panelLoanConfig.Visible = false;
                        panelReport.Visible = true;
                        inputReportSortMonths.Checked = true;
                        inputReportType.SelectedIndex = 0;
                        getReportDuration(reportSelectedDuration);

                        break;
                    }
            }

        }

        private void inputInitialLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputInitialLoanAmount_TextChanged(object sender, EventArgs e)
        {
            labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + inputCurrencySelection.SelectedItem.ToString();
        }

        private void navRight_Click(object sender, EventArgs e)
        {
            if (inputLoanPanelSelection.SelectedIndex <= 1)
                inputLoanPanelSelection.SelectedIndex = inputLoanPanelSelection.SelectedIndex + 1;
            else
                inputLoanPanelSelection.SelectedIndex = 0;
        }

        private void navLeft_Click(object sender, EventArgs e)
        {
            if (inputLoanPanelSelection.SelectedIndex >= 1)
                inputLoanPanelSelection.SelectedIndex = inputLoanPanelSelection.SelectedIndex - 1;
            else
                inputLoanPanelSelection.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inputLoanPanelSelection.SelectedIndex <= 1)
                inputLoanPanelSelection.SelectedIndex = inputLoanPanelSelection.SelectedIndex + 1;
            else
                inputLoanPanelSelection.SelectedIndex = 0;
        }
    }
}
