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
        private DataTable paymentsDataTable;
        private decimal trackBarTick;

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
                }
                selectedPayment = value;
            }
        }

        public decimal InputPaymentAmount
        {
            get
            {
                return FormatDigitInput.FormatToDecimal(inputPaymentAmount.Text);
            }
            set
            {
                foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
                {
                    if (binding.Control.Name == "inputPaymentAmount.Text")
                    {
                        binding.WriteValue();
                    }
                }
                inputPaymentAmount.Text = value.ToString();
            }
        }

        public decimal InputInterestAmount
        {
            get
            {
                return FormatDigitInput.FormatToDecimal(inputPaymentInterestAmount.Text);
            }
            set
            {
                foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
                {
                    if (binding.Control.Name == "inputPaymentInterestAmount.Text")
                    {
                        binding.WriteValue();
                    }
                }
                inputPaymentInterestAmount.Text = value.ToString();
            }
        }

        public decimal InputPrincipalAmount
        {
            get
            {
                return FormatDigitInput.FormatToDecimal(inputPaymentPrincipalAmount.Text);
            }
            set
            {
                foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
                {
                    if (binding.Control.Name == "inputPaymentPrincipalAmount.Text")
                    {
                        binding.WriteValue();
                    }
                }
                inputPaymentPrincipalAmount.Text = value.ToString();
            }
        }

        public LoanAdjustments(LoanReportMain loanReportMainObj, LoanReportData loanReportDataObj)
        {
            InitializeComponent();
            this.loanReportMainObj = loanReportMainObj;
            this.loanReportDataObj = loanReportDataObj;
            SelectedDate = loanReportDataObj.ActiveLoan.LoanStartDate;
            inputPaymentDate.MinDate = loanReportDataObj.ActiveLoan.LoanStartDate;
            inputPaymentAmount.DataBindings.Add("Text", paymentBindingSource, "TotalPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");
            inputPaymentInterestAmount.DataBindings.Add("Text", paymentBindingSource, "InterestPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");
            inputPaymentPrincipalAmount.DataBindings.Add("Text", paymentBindingSource, "PrincipalPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");
            gridPaymentList.Visible = false;
            panelInterestAmount.Visible = false;
            panelPrincipalAmount.Visible = false;
            inputPaymentAllocationTrack.Visible = false;
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            createPaymentTable();
            addPaymentsToGrid();
            displayedControlsCheck();
        }

        private void displayedControlsCheck()
        {
            if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0)
            {
                buttonAddPayment.Visible = true;
                if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0 && Convert.ToDecimal(inputPaymentAmount.Text) >= 400)
                {
                    inputPaymentAllocationTrack.Visible = true;
                    panelInterestAmount.Visible = true;
                    panelPrincipalAmount.Visible = true;
                    trackBarTick = InputPaymentAmount / 400;
                    updateAllocationPercent();
                }
                else if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0 && Convert.ToDecimal(inputPaymentAmount.Text) <= 400)
                {
                    panelInterestAmount.Visible = true;
                    panelPrincipalAmount.Visible = true;
                    inputPaymentAllocationTrack.Visible = false;
                }
            }
            else
            {
                buttonAddPayment.Visible = false;
            }
        }

        private void updateAllocationPercent()
        {
            if (InputInterestAmount > 0 && InputPrincipalAmount > 0 && InputPaymentAmount >= 100)
            {
                decimal onePercentOfPayment = InputPaymentAmount / 100;
                decimal interestPercent = Math.Floor((InputInterestAmount / onePercentOfPayment));
                labelInterestPercent.Text = interestPercent.ToString() + "%";
                labelPrincipalPercent.Text = (100 - interestPercent).ToString() + "%";
            }
            else
            {
                labelInterestPercent.Text = "";
                labelPrincipalPercent.Text = "";
            }
        }

        private void setSelectedPayment(DateTime selectedDate)
        {
            if (loanReportDataObj.ActiveLoan.PaymentsList.Count > 0)
            {
                foreach (Payment payment in loanReportDataObj.ActiveLoan.PaymentsList)
                {
                    if (payment.PaymentDate == selectedDate)
                    {
                        SelectedPayment = payment;
                        buttonRemovePayment.Visible = true;
                        break;
                    }
                    else if (payment.PaymentDate != selectedDate) // && loanReportDataObj.ActiveLoan.PaymentsList.Contains(payment)
                    {
                        SelectedPayment = new Payment();
                        SelectedPayment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
                        SelectedPayment.PaymentDate = SelectedDate;
                        buttonRemovePayment.Visible = false;
                        inputPaymentAmount.Text = "";
                        inputPaymentInterestAmount.Text = "";
                        inputPaymentPrincipalAmount.Text = "";
                    }
                }
            }
            else
            {
                buttonRemovePayment.Visible = false;
                SelectedPayment = new Payment();
                SelectedPayment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
                SelectedPayment.PaymentDate = SelectedDate;
            }
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
            paymentsDataTable.Rows.Clear();
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
            if (loanReportDataObj.ActiveLoan.PaymentsList.Count > 0)
            {
                gridPaymentList.Visible = true;
            }
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

        private void adjustAllocation()
        {
            if (InputPrincipalAmount > 0 && InputPrincipalAmount <= InputPaymentAmount && inputPaymentPrincipalAmount.Focused == true)
            {
                inputPaymentInterestAmount.Text = (InputPaymentAmount - InputPrincipalAmount).ToString();
                InputInterestAmount = InputPaymentAmount - InputPrincipalAmount;
                if (inputPaymentAllocationTrack.Visible)
                {
                    decimal setTrackValue;
                    setTrackValue = InputInterestAmount / trackBarTick;
                    inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                }
                updateAllocationPercent();
            }
            else if (InputPrincipalAmount > InputPaymentAmount && inputPaymentPrincipalAmount.Focused == true)
            {
                MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
            }

            else if (InputInterestAmount > 0 && InputInterestAmount <= InputPaymentAmount && inputPaymentInterestAmount.Focused == true)
            {
                inputPaymentPrincipalAmount.Text = (InputPaymentAmount - InputInterestAmount).ToString();
                if (inputPaymentAllocationTrack.Visible)
                {
                    decimal setTrackValue;
                    setTrackValue = InputInterestAmount / trackBarTick;
                    inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                }
                updateAllocationPercent();
            }

            else if (InputInterestAmount > InputPaymentAmount && inputPaymentInterestAmount.Focused == true)
            {
                MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
            }
        }

        private void updateTextBoxWithExistingPaymentData()
        {
            DataRow dateRow = loanReportDataObj.LoanDataTable.Rows.Find(SelectedDate);
            if (dateRow != null)
            {
                labelSelectedDayInfo.Text = "Balance details of the selected day: \r\n" +
                    "Principal Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
                    "Interest Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + "\r\n" +
         "Current Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";
            }
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        {
            if (inputPaymentAllocationTrack.Focused)
            {
                decimal calculatedPrinciple;
                decimal calculatedInterest;
                calculatedInterest = FormatDigitInput.FormatToDecimal(trackBarTick * inputPaymentAllocationTrack.Value);
                InputInterestAmount = calculatedInterest;
                inputPaymentInterestAmount.Text = calculatedInterest.ToString();
                calculatedPrinciple = FormatDigitInput.FormatToDecimal(InputPaymentAmount - calculatedInterest);
                InputPrincipalAmount = calculatedPrinciple;
                inputPaymentPrincipalAmount.Text = calculatedPrinciple.ToString();
                updateAllocationPercent();
            }
        }

        private void inputPaymentDate_ValueChanged(object sender, EventArgs e)
        {
            setSelectedPayment(SelectedDate);
            displayedControlsCheck();
            updateTextBoxWithExistingPaymentData();
        }

        private void inputPaymentAmount_KeyUp(object sender, KeyEventArgs e)
        {
            displayedControlsCheck();
        }

        private void inputPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputPaymentInterestAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputPaymentPrincipalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0 && inputPaymentAmount.Focused == true)
            {
                inputPaymentInterestAmount.Text = "";
                inputPaymentPrincipalAmount.Text = "";
                inputPaymentAllocationTrack.Value = 0;
            }
        }

        private void inputPaymentInterestAmount_TextChanged(object sender, EventArgs e)
        {
            adjustAllocation();
        }

        private void inputPaymentPrincipalAmount_TextChanged(object sender, EventArgs e)
        {
            adjustAllocation();
        }

        private void panelPaymentAllocation_Leave(object sender, EventArgs e)
        {
            SelectedPayment.TotalPaymentAmount = InputPaymentAmount;
            SelectedPayment.InterestPaymentAmount = InputInterestAmount;
            SelectedPayment.PrincipalPaymentAmount = InputPrincipalAmount;
            paymentBindingSource.ResetBindings(false);
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            if (!loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            {
                loanReportDataObj.ActiveLoan.PaymentsList.Add(SelectedPayment);
                addPaymentsToGrid();
                loanReportMainObj.textPaymentList.Text = "";
            }
            else if (loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            {
                paymentsDataTable.Rows.Find(SelectedDate)[1] = InputPaymentAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[2] = InputInterestAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[3] = InputPrincipalAmount;
            }

            loanReportMainObj.textPaymentList.Text = "Existing Payments: \r\n";
            foreach (Payment individualPayment in loanReportDataObj.ActiveLoan.PaymentsList)
            {
                loanReportMainObj.textPaymentList.Text += individualPayment.PaymentDate.ToShortDateString() + " " + individualPayment.TotalPaymentAmount + " " + individualPayment.InterestPaymentAmount + " " + individualPayment.PrincipalPaymentAmount + "\r\n";
            }
            inputPaymentAllocationTrack.Value = 0;
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            updateAllocationPercent();
            formatPaymentColumnHeaders();
            gridPaymentList.Visible = true;
            setSelectedPayment(SelectedDate);
            loanReportDataObj.CalculateLoan();
        }

        private void buttonRemovePayment_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(paymentsDataTable.Rows.Find(SelectedDate)[0]) != null)
            {
                loanReportDataObj.ActiveLoan.PaymentsList.Remove(SelectedPayment);
                paymentsDataTable.Rows.Find(SelectedDate).Delete();
                paymentsDataTable.AcceptChanges();
                addPaymentsToGrid();
                loanReportDataObj.CalculateLoan();
               setSelectedPayment(SelectedDate);
            }
            else
            {
                MessageBox.Show("Please pick a date with a payment.");
            }
        }

        private void buttonClosePrincipleAdjust_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCloseAddPayment_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridPaymentList_Click(object sender, EventArgs e)
        {
            int paymentRows = gridPaymentList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (paymentRows == 1)
            {
               inputPaymentDate.Value = Convert.ToDateTime(gridPaymentList.SelectedRows[paymentRows-1].Cells[0].Value);
                setSelectedPayment(SelectedDate);
            }
        }
    }
}
