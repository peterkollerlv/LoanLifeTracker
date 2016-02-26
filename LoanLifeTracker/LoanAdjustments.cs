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
        DataTable paymentsDataTable;
        DataTable LoanDataTable;
        private decimal paymentAmount;
        private decimal paymentPrincipalAmount;
        private decimal paymentInterestAmount;
        private decimal trackBarTick;
        private List<Payment> paymentList;


        public LoanAdjustments(LoanReportMain loanReportMainPassed, DataTable loanDataTable)
        {
            InitializeComponent();
            loanReportMain = loanReportMainPassed;
            LoanDataTable = loanDataTable;
            paymentList = loanReportMainPassed.LoanReportDataObj.PaymentsList;
            panelPaymentAllocation.Visible = false;
            gridPaymentList.Visible = false;
            buttonCloseAddPayment.Visible = false;
            inputPaymentAllocationTrack.Visible = false;
            labelInterestPercent.Text = "";
            labelPrincipalPercent.Text = "";
            createPaymentTable();
            addPaymentsToGrid();
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
            }
            paymentsDataTable.AcceptChanges();
            
            gridPaymentList.DataSource = paymentsDataTable;
            gridPaymentList.Visible = true;
            
            formatPaymentColumnHeaders();
        }

        //private void getPaymentsToGrid()
        //{
        //    EnumerableRowCollection<DataRow> allDaysWithPayments = from DataRow daysWithPayment in LoanDataTable.AsEnumerable()
        //                                                           where daysWithPayment.Field<decimal>("loanDayTotalPayment") != 0
        //                                                           select daysWithPayment;
        //    foreach (DataRow paymentRow in allDaysWithPayments)
        //    {
        //        paymentsDataTable.Rows.Add(new DateTime(paymentRow.Field<DateTime>("loanDayDate").Year, paymentRow.Field<DateTime>("loanDayDate").Month, paymentRow.Field<DateTime>("loanDayDate").Day),
        //            paymentRow.Field<decimal>("loanDayTotalPayment"), paymentRow.Field<decimal>("loanDayInterestPayment"), paymentRow.Field<decimal>("loanDayPrincipalPayment"));
        //    }
        //    if(paymentsDataTable.Rows.Count > 0)
        //    {
        //        gridPaymentList.Visible = true;
        //        gridPaymentList.DataSource = paymentsDataTable;
        //    addPaymentColumnHeaders();
        //     }
        //}

        private void formatPaymentColumnHeaders()
        {
            gridPaymentList.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            gridPaymentList.Columns["paymentDate"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridPaymentList.Columns["paymentDate"].DefaultCellStyle.Format = "MMMM dd, yyyy";
            gridPaymentList.Columns["totalPayment"].DefaultCellStyle.Format = "c2";
            gridPaymentList.Columns["principalPayment"].DefaultCellStyle.Format = "c2";
            gridPaymentList.Columns["interestPayment"].DefaultCellStyle.Format = "c2";
            gridPaymentList.Columns["paymentDate"].HeaderText = "Payment Date";
            gridPaymentList.Columns["totalPayment"].HeaderText = "Total Payment";
            gridPaymentList.Columns["interestPayment"].HeaderText = "Interest Payment";
            gridPaymentList.Columns["principalPayment"].HeaderText = "Principal Payment";
        }

        private void updateAllocationPercent()
        {
            if (FormatDigitInput.FormatToDecimal(paymentInterestAmount) > 0 && FormatDigitInput.FormatToDecimal(paymentPrincipalAmount) > 0)
                {
                decimal onePercentOfPeyment = paymentAmount / 100;
                labelInterestPercent.Text = FormatDigitInput.FormatToDecimal((paymentInterestAmount / onePercentOfPeyment)).ToString() + "%";
                labelPrincipalPercent.Text = FormatDigitInput.FormatToDecimal((paymentPrincipalAmount / onePercentOfPeyment)).ToString() + "%";
            }
        }

        private void buttonClosePrincipleAdjust_Click(object sender, EventArgs e)
        {
            //this.Hide();
        }

        private void buttonCloseAddPayment_Click(object sender, EventArgs e)
        {

            loanReportMain.LoanReportDataObj.CalculateLoan();
            Close();
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            bool paymentExists = paymentsDataTable.Rows.Contains(inputPaymentDate.Value.Date);

            if (!paymentExists)
            {
                paymentsDataTable.Rows.Add(inputPaymentDate.Value.Date, paymentAmount, paymentInterestAmount, paymentPrincipalAmount);

                Payment payment = new Payment();
                payment.PaymentID = 1;
                payment.PaymentDate = inputPaymentDate.Value.Date;
                payment.TotalPaymentAmount = paymentAmount;
                payment.InterestPaymentAmount = paymentInterestAmount;
                payment.PrincipalPaymentAmount = paymentPrincipalAmount;
                paymentList.Add(payment);
                loanReportMain.textPaymentList.Text = "";


              foreach (Payment individualPayment in paymentList)
                {
                 
                        loanReportMain.textPaymentList.Text += individualPayment.PaymentDate.ToShortDateString() + " " + individualPayment.TotalPaymentAmount + " " + individualPayment.InterestPaymentAmount + " " + individualPayment.PrincipalPaymentAmount + "\r\n";
                }
            }
            else if (paymentExists)
            {
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[1] = paymentAmount;
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[2] = paymentInterestAmount;
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[3] = paymentPrincipalAmount;
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

        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        {
            if (inputPaymentAllocationTrack.Focused)
            {
                decimal calculatedPrinciple;
                decimal calculatedInterest;
                
                calculatedInterest = (trackBarTick * inputPaymentAllocationTrack.Value);
                calculatedInterest = decimal.Round(calculatedInterest, 2, MidpointRounding.AwayFromZero);
                paymentInterestAmount = calculatedInterest;

                inputPaymentInterestAmount.Text = calculatedInterest.ToString();
                calculatedPrinciple = (paymentAmount - calculatedInterest);
                calculatedPrinciple = decimal.Round(calculatedPrinciple, 2, MidpointRounding.AwayFromZero);
                paymentPrincipalAmount = calculatedPrinciple;
                inputPaymentPrincipalAmount.Text = calculatedPrinciple.ToString();
                updateAllocationPercent();
            }
        }
        private void inputPaymentDate_ValueChanged(object sender, EventArgs e)
        {
           //not implemented yet
        }

        private void inputPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormatDigitInput.FilterKeypressToDigits(sender, e);
        }

        private void inputPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            if(inputPaymentAmount.Text != "")
            {
                inputPaymentAmount.Text = inputPaymentAmount.Text.Trim();
                paymentAmount = FormatDigitInput.FormatToDecimal(paymentAmount);
                paymentAmount = decimal.Round(Convert.ToDecimal(inputPaymentAmount.Text),2, MidpointRounding.AwayFromZero);
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
            if(inputPaymentInterestAmount.Text != "")
            {
                inputPaymentInterestAmount.Text = inputPaymentInterestAmount.Text.Trim();
                paymentInterestAmount = decimal.Round(Convert.ToDecimal(inputPaymentInterestAmount.Text), 2,MidpointRounding.AwayFromZero);
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
                paymentPrincipalAmount = decimal.Round(Convert.ToDecimal(inputPaymentPrincipalAmount.Text), 2, MidpointRounding.AwayFromZero);
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
    }
}
