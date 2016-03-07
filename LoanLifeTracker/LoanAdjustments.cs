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
            inputPaymentAmount.DataBindings.Add("Text", paymentBindingSource, "TotalPaymentAmount", true, DataSourceUpdateMode.OnPropertyChanged, null, "N2");
            inputPaymentInterestAmount.DataBindings.Add("Text", paymentBindingSource, "InterestPaymentAmount", true, DataSourceUpdateMode.OnPropertyChanged, null, "N2");
            inputPaymentPrincipalAmount.DataBindings.Add("Text", paymentBindingSource, "PrincipalPaymentAmount", true, DataSourceUpdateMode.OnPropertyChanged, null, "N2");
            panelPaymentAllocation.Visible = false;
            gridPaymentList.Visible = false;
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
                panelPaymentAllocation.Visible = true;
                buttonAddPayment.Visible = true;
                if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0 && Convert.ToDecimal(inputPaymentAmount.Text) >= 400)
                {
                    inputPaymentAllocationTrack.Visible = true;
                    trackBarTick = SelectedPayment.TotalPaymentAmount / 400;
                    updateAllocationPercent();

                }
                else if (inputPaymentAmount.Text != "" && Convert.ToDecimal(inputPaymentAmount.Text) > 0 && Convert.ToDecimal(inputPaymentAmount.Text) <= 400)

                {
                    inputPaymentAllocationTrack.Visible = false;

                }
            }
            else
            {
                buttonAddPayment.Visible = false;
                panelPaymentAllocation.Visible = false;
            }
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
                        paymentBindingSource.ResetBindings(false);

                        break;

                    }
                    else if (payment.PaymentDate != SelectedDate) // && loanReportDataObj.ActiveLoan.PaymentsList.Contains(payment)
                    {
                        //selectedPayment = null;

                        SelectedPayment = new Payment();
                        SelectedPayment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
                        SelectedPayment.PaymentDate = SelectedDate;

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

        private void updateAllocationPercent()
        {
            if (SelectedPayment.InterestPaymentAmount > 0 && SelectedPayment.PrincipalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount >= 100)
            {
                decimal onePercentOfPeyment = SelectedPayment.TotalPaymentAmount / 100;
                decimal interestPercent = Math.Floor((SelectedPayment.InterestPaymentAmount / onePercentOfPeyment));
                labelInterestPercent.Text = interestPercent.ToString() + "%";
                labelPrincipalPercent.Text = (100 - interestPercent).ToString() + "%";
            }
            else
            {
                labelInterestPercent.Text = "";
                labelPrincipalPercent.Text = "";
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

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            if (!loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            {
                loanReportDataObj.ActiveLoan.PaymentsList.Add(SelectedPayment);
                addPaymentsToGrid();
                loanReportMainObj.textPaymentList.Text = "";
                //  textExistingPaymentSummary.Text += "Existing payment: " + SelectedPayment.TotalPaymentAmount;
            }
            else if (loanReportDataObj.ActiveLoan.PaymentsList.Contains(SelectedPayment))
            {
                paymentsDataTable.Rows.Find(SelectedDate)[1] = SelectedPayment.TotalPaymentAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[2] = SelectedPayment.InterestPaymentAmount;
                paymentsDataTable.Rows.Find(SelectedDate)[3] = SelectedPayment.PrincipalPaymentAmount;
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
   
            loanReportDataObj.CalculateLoan();
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        {
            if (inputPaymentAllocationTrack.Focused)
            {
                decimal calculatedPrinciple;
                decimal calculatedInterest;
                calculatedInterest = FormatDigitInput.FormatToDecimal(trackBarTick * inputPaymentAllocationTrack.Value);
                SelectedPayment.InterestPaymentAmount = calculatedInterest;
                inputPaymentInterestAmount.Text = calculatedInterest.ToString();
                calculatedPrinciple = FormatDigitInput.FormatToDecimal(SelectedPayment.TotalPaymentAmount - calculatedInterest);
                SelectedPayment.PrincipalPaymentAmount = calculatedPrinciple;
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



        //temporarly used to display payment data

        private void updateTextBoxWithExistingPaymentData()
        {
            // textExistingPaymentSummary.Text = "";
            DataRow dateRow = loanReportDataObj.LoanDataTable.Rows.Find(SelectedDate);
            if (dateRow != null)
            {
                labelSelectedDayInfo.Text = "Balance details of the selected day: \r\n" +
                    "Principal Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
                    "Interest Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + "\r\n" +
         "Current Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";

            }
        }



        private void inputPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);


        }


        private void inputPaymentAmount_KeyUp(object sender, KeyEventArgs e)
        {
            displayedControlsCheck();
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

        private void inputPaymentInterestAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }


        // experimental to fix the issue with SelectionStart when entering numbers

        private void inputPaymentInterestAmount_TextChanged(object sender, EventArgs e)
        {
            adjustAllocation();
            //if (inputPaymentInterestAmount.Focused)
            //{
            //    inputPaymentInterestAmount.Focus();
            //  //  inputPaymentInterestAmount.SelectionStart = inputPaymentInterestAmount.Text.IndexOf(".");
            //}
        }

        private void inputPaymentPrincipalAmount_TextChanged(object sender, EventArgs e)
        {
            //adjustAllocation();
            //if (inputPaymentPrincipalAmount.Focused)
            //{
            //    inputPaymentPrincipalAmount.SelectionStart = inputPaymentPrincipalAmount.Text.IndexOf(".");
            //}
        }

        private void adjustAllocation()
        {

            if (SelectedPayment.PrincipalPaymentAmount > 0 && SelectedPayment.PrincipalPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.Focused == true)
            {
                inputPaymentInterestAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount).ToString();
                SelectedPayment.InterestPaymentAmount = SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount;
                if (inputPaymentAllocationTrack.Visible)
                {
                    decimal setTrackValue;
                    setTrackValue = SelectedPayment.InterestPaymentAmount / trackBarTick;
                    inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                }
                updateAllocationPercent();
            }
            else if (SelectedPayment.PrincipalPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.Focused == true)
            {
                MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
            }

            else if (SelectedPayment.InterestPaymentAmount > 0 && SelectedPayment.InterestPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.Focused == true)
            {
                inputPaymentPrincipalAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.InterestPaymentAmount).ToString();
                if (inputPaymentAllocationTrack.Visible)
                {
                    decimal setTrackValue;
                    setTrackValue = SelectedPayment.InterestPaymentAmount / trackBarTick;
                    inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
                }
                updateAllocationPercent();
            }

            else if (SelectedPayment.InterestPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.Focused == true)
            {
                MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
            }
        }

        private void buttonRemovePayment_Click(object sender, EventArgs e)
        {
            loanReportDataObj.ActiveLoan.PaymentsList.Remove(SelectedPayment);
            paymentsDataTable.Rows.Find(SelectedDate).Delete();
            paymentsDataTable.AcceptChanges();
            addPaymentsToGrid();
            loanReportDataObj.CalculateLoan();
        }
    }
}
