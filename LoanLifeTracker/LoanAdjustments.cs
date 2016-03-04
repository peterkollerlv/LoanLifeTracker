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
        protected LoanReportMain loanReportMainObj;
        protected LoanReportData loanReportDataObj;
        //Loan activeLoan;
        //List<Payment> paymentList;
        //DataTable LoanDataTable;
        private DataTable paymentsDataTable;
        
        
        
        private decimal paymentAmount;
        private decimal paymentPrincipalAmount;
        private decimal paymentInterestAmount;
        private decimal trackBarTick;

        // fields need to be implemented:
        private decimal paymentMaxTotal;
        private decimal paymentMaxInterest;
        private decimal paymentMaxPrinciple;

        public DateTime SelectedDate
        {
            get
            {
               
                return inputPaymentDate.Value.Date;
            }
            set
            {
                inputPaymentDate.Value = value;
            }
        }

        private Payment selectedPayment;
        public Payment SelectedPayment
        {
            get
            {
                return selectedPayment;
            }

            set
            {
                if (value != null)
                {
                    paymentBindingSource.DataSource = value;
                    paymentBindingSource.ResetBindings(false);
                }
                selectedPayment = value;
   
            }
        }


        public LoanAdjustments(LoanReportMain loanReportMainObj, LoanReportData loanReportDataObj)
        {
            InitializeComponent();
            this.loanReportMainObj = loanReportMainObj;
            this.loanReportDataObj = loanReportDataObj;
                
            SelectedDate = loanReportDataObj.ActiveLoan.LoanStartDate;
            inputPaymentDate.MinDate = loanReportDataObj.ActiveLoan.LoanStartDate;

            inputPaymentAmount.DataBindings.Add("Text", paymentBindingSource, "TotalPaymentAmount");
            inputPaymentInterestAmount.DataBindings.Add("Text", paymentBindingSource, "InterestPaymentAmount");
            inputPaymentPrincipalAmount.DataBindings.Add("Text", paymentBindingSource, "PrincipalPaymentAmount");

            panelPaymentAllocation.Visible = false;
            gridPaymentList.Visible = false;
            buttonCloseAddPayment.Visible = false;
            inputPaymentAllocationTrack.Visible = false;


            
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            textExistingPaymentSummary.Text = "";


            createPaymentTable();
            addPaymentsToGrid();
            updateTextBoxWithExistingPaymentData();
            
        }

       private void setSelectedPayment(DateTime selectedDate)
        {
            if (loanReportDataObj.ActiveLoan.PaymentsList.Count > 0)
            {

                foreach (Payment payment in loanReportDataObj.ActiveLoan.PaymentsList)
                {
                    if (payment.PaymentDate == SelectedDate)
                    {
                        SelectedPayment = payment;
                        buttonRemovePayment.Visible = true;
                        break;

                    }
                    else if (payment.PaymentDate != SelectedDate) // && loanReportDataObj.ActiveLoan.PaymentsList.Contains(payment)
                    {
                        selectedPayment = null;
                    }
                }
            }
            else
            {
                buttonRemovePayment.Visible = false;
                selectedPayment = null;
            }
            paymentBindingSource.ResetBindings(false);
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
            foreach (Payment payment in loanReportDataObj.ActiveLoan.PaymentsList)
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
            gridPaymentList.Columns["paymentDate"].HeaderText = "Payment Date \n(" + loanReportDataObj.ActiveLoan.LoanCurrency + ")";
            gridPaymentList.Columns["totalPayment"].HeaderText = "Total Payment \n(" + loanReportDataObj.ActiveLoan.LoanCurrency + ")";
            gridPaymentList.Columns["interestPayment"].HeaderText = "Interest Payment \n(" + loanReportDataObj.ActiveLoan.LoanCurrency + ")";
            gridPaymentList.Columns["principalPayment"].HeaderText = "Principal Payment \n(" + loanReportDataObj.ActiveLoan.LoanCurrency + ")";
        }

        private void updateAllocationPercent()
        {
            if (FormatDigitInput.FormatToDecimal(paymentInterestAmount) > 0 && FormatDigitInput.FormatToDecimal(paymentPrincipalAmount) > 0)
            {
                decimal onePercentOfPeyment = paymentAmount / 100;
                decimal interestPercent = FormatDigitInput.FormatToDecimal(Math.Floor((paymentInterestAmount / onePercentOfPeyment)));
                labelInterestPercent.Text = interestPercent.ToString() + "%";
                labelPrincipalPercent.Text = (100 - interestPercent).ToString() + "%";
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
            setSelectedPayment(SelectedDate);

            if (!loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            //if (SelectedPayment == null)
                {
                Payment payment = new Payment();
                payment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
                payment.PaymentDate = SelectedDate;
                payment.TotalPaymentAmount = paymentAmount;
                payment.InterestPaymentAmount = paymentInterestAmount;
                payment.PrincipalPaymentAmount = paymentPrincipalAmount;

                loanReportDataObj.ActiveLoan.PaymentsList.Add(payment);

                paymentsDataTable.Rows.Add(payment.PaymentDate, payment.TotalPaymentAmount, payment.InterestPaymentAmount, payment.PrincipalPaymentAmount);

               
                loanReportMainObj.textPaymentList.Text = "";
                textExistingPaymentSummary.Text += "Existing payment: " + payment.TotalPaymentAmount;
            }
            else if (loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            {
                setSelectedPayment(SelectedDate);
                paymentsDataTable.Rows.Find(SelectedDate)[1] = paymentAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[2] = paymentInterestAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[3] = paymentPrincipalAmount;
            }
   
            loanReportMainObj.textPaymentList.Text = "Existing Payments: \r\n";
            foreach (Payment individualPayment in loanReportDataObj.ActiveLoan.PaymentsList)
            {
                loanReportMainObj.textPaymentList.Text += individualPayment.PaymentDate.ToShortDateString() + " " + individualPayment.TotalPaymentAmount + " " + individualPayment.InterestPaymentAmount + " " + individualPayment.PrincipalPaymentAmount + "\r\n";
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
            loanReportDataObj.CalculateLoan();
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
            setSelectedPayment(SelectedDate);
            updateTextBoxWithExistingPaymentData();
        }



        //temporarly used to display payment data

        private void updateTextBoxWithExistingPaymentData()
        {
            textExistingPaymentSummary.Text = "";
            DataRow dateRow = loanReportDataObj.LoanDataTable.Rows.Find(SelectedDate);
            if (dateRow != null)
            {
                textExistingPaymentSummary.Text = "Balance on date: \r\n" +
                    "Principal Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency  + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
             "Interest Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency  + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + "\r\n" +
             "Current Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency  + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";

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
            loanReportDataObj.ActiveLoan.PaymentsList.Remove(selectedPayment);
            paymentsDataTable.Rows.Find(SelectedDate).Delete();
            paymentsDataTable.AcceptChanges();
            addPaymentsToGrid();
            loanReportDataObj.CalculateLoan();
        }
    }
}
