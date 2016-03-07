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
        private List<Control> bindedControls;
        public LoanReportData LoanReportDataObj;
        private LoanAdjustments loanAdjustments;


        public LoanReportMain()
        {
            InitializeComponent();
            LoanReportDataObj = new LoanReportData(this);
            inputCurrencySelection.SelectedIndex = 0;
            inputLoanPanelSelection.SelectedIndex = 0;
            bindedControls = new List<Control>();
            applicationInitialState();
            bindControls();
        }

        public void applicationInitialState()
        {
            bindedControls.Add(inputLoanTitle);
            bindedControls.Add(inputBeneficiary);
            bindedControls.Add(inputCollectionAccount);
            bindedControls.Add(inputCompanyInfo);
            bindedControls.Add(inputCurrencySelection);
            bindedControls.Add(inputInterestPenaltyChk);
            bindedControls.Add(inputInitialLoanAmount);
            bindedControls.Add(inputInterestPenaltyStart);
            bindedControls.Add(inputInterestPenaltyRate);
            bindedControls.Add(inputInterestRate);
            bindedControls.Add(inputInterestStructureSelection);
            bindedControls.Add(inputLender);
            bindedControls.Add(inputLoanStartDate);
            bindedControls.Add(labelLoanTitleInfo);
            bindedControls.Add(labelBeneficiaryInfo);
            bindedControls.Add(labelCollectionAccountInfo);
            bindedControls.Add(labelCompanyInfo);
            bindedControls.Add(labelLenderInfo);
            bindedControls.Add(labelLoanStartDateInfo);
            bindedControls.Add(labelLoanTitleFront); inputLoanDuration.Value = 5;
            labelInitialLoanAmountInfo.Text = "";
            textPaymentList.Text = "";
            labelTodayDateInfo.Text = DateTime.Now.Date.ToLongDateString();
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
                // labelInitialLoanAmountInfo.Text = inputInitialLoanAmount.Text + " " + LoanReportDataObj.ActiveLoan.LoanCurrency;
                if (loanReportDataGrid != null)
                {
                    //LoanReportDataObj.SetColumnHeaders();
                }
                if (loanAdjustments != null)
                {
                    //loanAdjustments.labelPaymentCurrency.Text = LoanReportDataObj.ActiveLoan.LoanCurrency;
                    //loanAdjustments.labelInterestPaymentCurrency.Text = LoanReportDataObj.ActiveLoan.LoanCurrency;
                    //loanAdjustments.labelPrincipalPaymentCurrency.Text = LoanReportDataObj.ActiveLoan.LoanCurrency;

                }
            }
        }

        public void recalculateLoan()
        {
            statusProgressBar.Value = 0;
            //inputLoanStartDate.Enabled = false;
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

        //               if (LoanReportDataObj.ActiveLoan != null)
        //        {
        //            LoanReportDataObj.SetColumnHeaders();
        //            //LoanReportDataObj.ActiveLoan.LoanCurrency = inputCurrencySelection.SelectedItem.ToString();
        //            setCurrencyLabels();
        //}

        private void inputCurrencySelection_SelectedValueChanged(object sender, EventArgs e)
        {
            LoanReportDataObj.SetColumnHeaders();
            loanReportDataGrid.Refresh();
        }

        private void inputCurrencySelection_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                LoanReportDataObj.setActiveLoan(true);
                statusIndicationText.Text = "Active Loan GUID: " + LoanReportDataObj.ActiveLoan.LoanGuid.ToString();


                loanBindingSource.DataSource = LoanReportDataObj.ActiveLoan;

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


                foreach (Control control in bindedControls)
                {
                    control.DataBindings.Clear();
                }
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



        private void bindControls()
        {
            inputLoanTitle.DataBindings.Add("Text", loanBindingSource, "LoanTitle", true, DataSourceUpdateMode.OnPropertyChanged);
            inputBeneficiary.DataBindings.Add("Text", loanBindingSource, "LoanBeneficiary", true, DataSourceUpdateMode.OnPropertyChanged);
            inputCollectionAccount.DataBindings.Add("Text", loanBindingSource, "LoanCollectionAccount", true, DataSourceUpdateMode.OnPropertyChanged);
            inputCompanyInfo.DataBindings.Add("SelectedItem", loanBindingSource, "LoanCompanyInfo", true, DataSourceUpdateMode.OnPropertyChanged);
            inputCurrencySelection.DataBindings.Add("SelectedItem", loanBindingSource, "LoanCurrency", true, DataSourceUpdateMode.OnPropertyChanged);
            inputInterestPenaltyChk.DataBindings.Add("Checked", loanBindingSource, "LoanHasPenalty", true, DataSourceUpdateMode.OnPropertyChanged);
            inputInitialLoanAmount.DataBindings.Add("Text", loanBindingSource, "LoanInitialLoanAmount", true, DataSourceUpdateMode.OnPropertyChanged, null, "N2");
            inputInterestPenaltyStart.DataBindings.Add("Value", loanBindingSource, "LoanInterestPenaltyDate", true, DataSourceUpdateMode.OnPropertyChanged);
            inputInterestPenaltyRate.DataBindings.Add("Value", loanBindingSource, "LoanInterestPenaltyRate", true, DataSourceUpdateMode.OnPropertyChanged);
            inputInterestRate.DataBindings.Add("Value", loanBindingSource, "LoanInterestRate", true, DataSourceUpdateMode.OnPropertyChanged);
            inputInterestStructureSelection.DataBindings.Add("SelectedIndex", loanBindingSource, "LoanInterestStructure", true, DataSourceUpdateMode.OnPropertyChanged);
            inputLender.DataBindings.Add("Text", loanBindingSource, "LoanLender", true, DataSourceUpdateMode.OnPropertyChanged);
            inputLoanStartDate.DataBindings.Add("Value", loanBindingSource, "LoanStartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            labelLoanTitleInfo.DataBindings.Add("Text", loanBindingSource, "LoanTitle", true, DataSourceUpdateMode.OnPropertyChanged);
            labelBeneficiaryInfo.DataBindings.Add("Text", loanBindingSource, "LoanBeneficiary", true, DataSourceUpdateMode.OnPropertyChanged);
            labelCollectionAccountInfo.DataBindings.Add("Text", loanBindingSource, "LoanCollectionAccount", true, DataSourceUpdateMode.OnPropertyChanged);
            labelCompanyInfo.DataBindings.Add("Text", loanBindingSource, "LoanCompanyInfo", true, DataSourceUpdateMode.OnPropertyChanged);
            labelLenderInfo.DataBindings.Add("Text", loanBindingSource, "LoanLender", true, DataSourceUpdateMode.OnPropertyChanged);
            labelLoanStartDateInfo.DataBindings.Add("Text", loanBindingSource, "LoanStartDate", true, DataSourceUpdateMode.OnPropertyChanged, null, "D");
            labelLoanTitleFront.DataBindings.Add("Text", loanBindingSource, "LoanTitle", true, DataSourceUpdateMode.OnPropertyChanged, null, "D");
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
            System.Drawing.Image freewayLogoFromRescources = System.Drawing.Image.FromHbitmap(Properties.Resources.FreewayLogoWhiteBackGround.GetHbitmap());

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
            //inputReportStartDate.Value = inputLoanStartDate.Value.Date;
            //inputReportEndDate.Value = inputLoanStartDate.Value.Date.AddYears((int)inputLoanDuration.Value);
            // labelLoanStartDateInfo.Text = inputLoanStartDate.Value.ToLongDateString();
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
            loanAdjustments = new LoanAdjustments(this, LoanReportDataObj);
            //loanAdjustments.labelPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            //loanAdjustments.labelInterestPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            //loanAdjustments.labelPrincipalPaymentCurrency.Text = inputCurrencySelection.SelectedItem.ToString();
            loanAdjustments.Text = "Payments";
            loanAdjustments.panelPrincipleAdjust.Visible = false;
            loanAdjustments.panelAddPayment.Visible = true;
            loanAdjustments.Show();
        }

        private void buttonOpenPrincipalAdjust_Click(object sender, EventArgs e)
        {
            LoanAdjustments loanAdjustments = new LoanAdjustments(this, LoanReportDataObj);
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
            labelInitialLoanAmountInfo.Text = LoanReportDataObj.ActiveLoan.LoanCurrency + " " + LoanReportDataObj.ActiveLoan.LoanInitialLoanAmount;
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


    }
}
