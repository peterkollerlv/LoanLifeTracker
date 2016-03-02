using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanLifeTracker
{
    public partial class LoanAdjustments : Form
    {
        LoanReportMain loanReportMain;
        Loan activeLoan;
        List<Payment> paymentList;
        DataTable paymentsDataTable;
        DataTable LoanDataTable;
        private Payment selectedPayment;
        private DateTime selectedPaymentDate;
        private decimal paymentAmount;
        private decimal paymentPrincipalAmount;
        private decimal paymentInterestAmount;
        private decimal trackBarTick;

        // fields need to be implemented:
        private decimal paymentMaxTotal;
        private decimal paymentMaxInterest;
        private decimal paymentMaxPrinciple;

        public LoanAdjustments(LoanReportMain loanReportMainPassed, DataTable loanDataTable, Loan activeLoanP)
        {
            InitializeComponent();
            loanReportMain = loanReportMainPassed;
            LoanDataTable = loanDataTable;
            activeLoan = activeLoanP;
            paymentList = activeLoan.PaymentsList;
            panelPaymentAllocation.Visible = false;
            gridPaymentList.Visible = false;
            buttonCloseAddPayment.Visible = false;
            inputPaymentAllocationTrack.Visible = false;
            selectedPaymentDate = inputPaymentDate.Value.Date;
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            textExistingPaymentSummary.Text = "";
            getSelectedDatesPayment(selectedPaymentDate);
            createPaymentTable();
            addPaymentsToGrid();
            updateTextBoxWithExistingPaymentData();
        }

        private void createPaymentTable()
        {
            paymentsDataTable = null;
            paymentsDataTable = new DataTable();
            paymentsDataTable.Columns.Add("paymentDate", typeof(DateTime));
            paymentsDataTable.Columns.Add("totalPayment", typeof(decimal));
            paymentsDataTable.Columns.Add("interestPayment", typeof(decimal));
            paymentsDataTable.Columns.Add("principalPayment", typeof(decimal));
            paymentsDataTable.PrimaryKey = new DataColumn[] { paymentsDataTable.Columns["paymentDate"] };
        }

        private void addPaymentsToGrid()
        {
            foreach (Payment payment in paymentList)
            {
                DataRow paymentRow = paymentsDataTable.NewRow();
                paymentRow[0] = payment.PaymentDate;
                paymentRow[1] = payment.TotalPaymentAmount;
                paymentRow[2] = payment.InterestPaymentAmount;
                paymentRow[3] = payment.PrincipalPaymentAmount;
                paymentsDataTable.Rows.Add(paymentRow);
            }
            paymentsDataTable.AcceptChanges();
            gridPaymentList.DataSource = paymentsDataTable;
            gridPaymentList.Visible = true;
            formatPaymentColumnHeaders();
        }

        private void formatPaymentColumnHeaders()
        {
            gridPaymentList.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            gridPaymentList.Columns["paymentDate"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridPaymentList.Columns["paymentDate"].DefaultCellStyle.Format = "MMMM dd, yyyy";
            gridPaymentList.Columns["totalPayment"].DefaultCellStyle.Format = "N";
            gridPaymentList.Columns["principalPayment"].DefaultCellStyle.Format = "N";
            gridPaymentList.Columns["interestPayment"].DefaultCellStyle.Format = "N";
            gridPaymentList.Columns["paymentDate"].HeaderText = "Payment Date \n(" + activeLoan.LoanCurrency + ")";
            gridPaymentList.Columns["totalPayment"].HeaderText = "Total Payment \n(" + activeLoan.LoanCurrency + ")";
            gridPaymentList.Columns["interestPayment"].HeaderText = "Interest Payment \n(" + activeLoan.LoanCurrency + ")";
            gridPaymentList.Columns["principalPayment"].HeaderText = "Principal Payment \n(" + activeLoan.LoanCurrency + ")";
        }

        private void updateAllocationPercent()
        {
            if (FormatDigitInput.FormatToDecimal(paymentInterestAmount) > 0 && FormatDigitInput.FormatToDecimal(paymentPrincipalAmount) > 0)
            {
                decimal onePercentOfPeyment = paymentAmount / 100;
                labelInterestPercent.Text = FormatDigitInput.FormatToDecimal(Math.Floor((paymentInterestAmount / onePercentOfPeyment))).ToString() + "%";
                labelPrincipalPercent.Text = FormatDigitInput.FormatToDecimal(Math.Floor((paymentPrincipalAmount / onePercentOfPeyment))).ToString() + "%";
            }
        }

        private void buttonClosePrincipleAdjust_Click(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void buttonCloseAddPayment_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            bool paymentExists = paymentsDataTable.Rows.Contains(selectedPaymentDate);

            if (!paymentExists)
            {
                paymentsDataTable.Rows.Add(selectedPaymentDate, paymentAmount, paymentInterestAmount, paymentPrincipalAmount);
                Payment payment = new Payment();
                payment.PaymentID = 1;
                payment.PaymentDate = selectedPaymentDate;
                payment.TotalPaymentAmount = paymentAmount;
                payment.InterestPaymentAmount = paymentInterestAmount;
                payment.PrincipalPaymentAmount = paymentPrincipalAmount;
                paymentList.Add(payment);
                inputPaymentDate.Value = selectedPaymentDate;
                loanReportMain.textPaymentList.Text = "";

                foreach (Payment individualPayment in paymentList)
                {
                    loanReportMain.textPaymentList.Text += individualPayment.PaymentDate.ToShortDateString() + " " + individualPayment.TotalPaymentAmount + " " + individualPayment.InterestPaymentAmount + " " + individualPayment.PrincipalPaymentAmount + "\r\n";
                }
            }
            else if (paymentExists)
            {
                paymentsDataTable.Rows.Find(selectedPaymentDate)[1] = paymentAmount;
                paymentsDataTable.Rows.Find(selectedPaymentDate)[2] = paymentInterestAmount;
                paymentsDataTable.Rows.Find(selectedPaymentDate)[3] = paymentPrincipalAmount;
            }
            inputPaymentAllocationTrack.Value = 0;
            inputPaymentAmount.Text = "";
            inputPaymentInterestAmount.Text = "";
            inputPaymentPrincipalAmount.Text = "";
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            gridPaymentList.DataSource = paymentsDataTable;
            formatPaymentColumnHeaders();
            gridPaymentList.Visible = true;
            buttonCloseAddPayment.Visible = true;
            loanReportMain.LoanReportDataObj.CalculateLoan();
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        {
            if (inputPaymentAllocationTrack.Focused)
            {
                decimal calculatedPrinciple;
                decimal calculatedInterest;
                calculatedInterest = FormatDigitInput.FormatToDecimal(trackBarTick * inputPaymentAllocationTrack.Value);
                paymentInterestAmount = calculatedInterest;
                inputPaymentInterestAmount.Text = calculatedInterest.ToString();
                calculatedPrinciple = FormatDigitInput.FormatToDecimal(paymentAmount - calculatedInterest);
                paymentPrincipalAmount = calculatedPrinciple;
                inputPaymentPrincipalAmount.Text = calculatedPrinciple.ToString();
                updateAllocationPercent();
            }
        }
        private void inputPaymentDate_ValueChanged(object sender, EventArgs e)
        {
            selectedPaymentDate = inputPaymentDate.Value.Date;
            updateTextBoxWithExistingPaymentData();
            getSelectedDatesPayment(selectedPaymentDate);
        }

        private void getSelectedDatesPayment(DateTime selectedDate)
        {
            if (loanReportMain.LoanReportDataObj.getPaymentDetails(selectedPaymentDate) != null)
            {
                buttonRemovePayment.Visible = true;
                selectedPayment = loanReportMain.LoanReportDataObj.getPaymentDetails(selectedPaymentDate);
                textExistingPaymentSummary.Text += "Existing payment: " + selectedPayment.TotalPaymentAmount;
            }

        }

        private void updateTextBoxWithExistingPaymentData()
        {
            textExistingPaymentSummary.Text = "";
            DataRow dateRow = loanReportMain.LoanReportDataObj.LoanDataTable.Rows.Find(selectedPaymentDate);
            if (dateRow != null)
            {
                textExistingPaymentSummary.Text = "Balance on date: \r\n" +
                    "Principal Balance: " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + activeLoan.LoanCurrency + "\r\n" +
             "Interest Balance: " + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + activeLoan.LoanCurrency + "\r\n" +
             "Current Balance: " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + activeLoan.LoanCurrency + "\r\n";

            }
        }



        private void inputPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            if (inputPaymentAmount.Text != "")
            {
                inputPaymentAmount.Text = inputPaymentAmount.Text.Trim();
                paymentAmount = FormatDigitInput.FormatToDecimal(inputPaymentAmount.Text);
                panelPaymentAllocation.Visible = true;
                if (inputPaymentAmount.Focused)
                {
                    inputPaymentInterestAmount.Text = "";
                    inputPaymentPrincipalAmount.Text = "";
                    paymentInterestAmount = 0;
                    paymentPrincipalAmount = 0;
                    inputPaymentAllocationTrack.Value = 0;

                }
                if (paymentAmount >= 400)
                {
                    inputPaymentAllocationTrack.Visible = true;
                    trackBarTick = paymentAmount / 400;

                }
                else if (paymentAmount <= 400)
                {
                    inputPaymentAllocationTrack.Visible = false;
                }
            }
        }

        private void inputPaymentInterestAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void LoanAdjustments_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.KeyChar.ToString());
        }

        private void inputPaymentInterestAmount_TextChanged(object sender, EventArgs e)
        {
            if (inputPaymentInterestAmount.Text != "")
            {
                inputPaymentInterestAmount.Text = inputPaymentInterestAmount.Text.Trim();
                paymentInterestAmount = FormatDigitInput.FormatToDecimal(inputPaymentInterestAmount.Text);
                updateAllocationPercent();
                if (paymentInterestAmount <= paymentAmount && inputPaymentInterestAmount.Focused == true)
                {
                    inputPaymentPrincipalAmount.Text = (paymentAmount - paymentInterestAmount).ToString();
                    if (inputPaymentAllocationTrack.Visible)
                    {
                        decimal setTrackValue;
                        setTrackValue = paymentInterestAmount / trackBarTick;
                        inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                    }
                }
                else if (paymentInterestAmount > paymentAmount && inputPaymentInterestAmount.Focused == true)
                {
                    MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
                }
            }

            else
            {
                paymentInterestAmount = 0;
            }
        }

        private void inputPaymentPrincipalAmount_TextChanged(object sender, EventArgs e)
        {
            if (inputPaymentPrincipalAmount.Text != "")
            {
                inputPaymentPrincipalAmount.Text = inputPaymentPrincipalAmount.Text.Trim();
                paymentPrincipalAmount = FormatDigitInput.FormatToDecimal(inputPaymentPrincipalAmount.Text);
                updateAllocationPercent();

                if (paymentPrincipalAmount <= paymentAmount && inputPaymentPrincipalAmount.Focused == true)
                {
                    inputPaymentInterestAmount.Text = (paymentAmount - paymentPrincipalAmount).ToString();
                    paymentInterestAmount = paymentAmount - paymentPrincipalAmount;
                    if (inputPaymentAllocationTrack.Visible)
                    {
                        decimal setTrackValue;
                        setTrackValue = paymentInterestAmount / trackBarTick;
                        inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                    }
                }
                else if (paymentPrincipalAmount > paymentAmount && inputPaymentPrincipalAmount.Focused == true)
                {
                    MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
                }
            }
            else
            {
                paymentInterestAmount = 0;
            }
        }

        private void inputPaymentAmount_Leave(object sender, EventArgs e)
        {
            inputPaymentAmount.Text = FormatDigitInput.FormatToDecimal(paymentAmount).ToString();
        }

        private void buttonRemovePayment_Click(object sender, EventArgs e)
        {
            paymentList.Remove(selectedPayment);
            paymentsDataTable.Rows.Find(selectedPaymentDate).Delete();
            paymentsDataTable.AcceptChanges();
            addPaymentsToGrid();
            loanReportMain.LoanReportDataObj.CalculateLoan();
        }
    }
}
