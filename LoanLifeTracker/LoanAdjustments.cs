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
        DataGridView paymentGridView;
        DataTable paymentsDataTable;
        DataTable LoanDataTable;
        private decimal paymentAmount;
        private decimal paymentPrincipalAmount;
        private decimal paymentInterestAmount;
        private int trackBarValue;
        private decimal trackBarTick;


        public LoanAdjustments(LoanReportMain loanReportMainPassed, DataTable loanDataTable)
        {
            InitializeComponent();
            loanReportMain = loanReportMainPassed;
            LoanDataTable = loanDataTable;
            panelPaymentAllocation.Visible = false;
            //this.Controls.Add(inputPaymentAmount);
            createPaymentTable();
            getPaymentsToGrid();

        }

        private void createPaymentTable()
        {
            paymentsDataTable = null;
            paymentsDataTable = new DataTable();
            paymentGridView = gridPaymentList;
            paymentsDataTable.Columns.Add("paymentDate", typeof(DateTime));
            paymentsDataTable.Columns.Add("totalPayment", typeof(decimal));
            paymentsDataTable.Columns.Add("interestPayment", typeof(decimal));
            paymentsDataTable.Columns.Add("principalPayment", typeof(decimal));
            paymentsDataTable.PrimaryKey = new DataColumn[] { paymentsDataTable.Columns["paymentDate"] };
        }

        private void getPaymentsToGrid()
        {



            EnumerableRowCollection<DataRow> allDaysWithPayments = from DataRow daysWithPayment in LoanDataTable.AsEnumerable()
                                                                   where daysWithPayment.Field<decimal>("loanDayTotalPayment") != 0
                                                                   select daysWithPayment;
            foreach (DataRow paymentRow in allDaysWithPayments)
            {
                paymentsDataTable.Rows.Add(new DateTime(paymentRow.Field<DateTime>("loanDayDate").Year, paymentRow.Field<DateTime>("loanDayDate").Month, paymentRow.Field<DateTime>("loanDayDate").Day),
                    paymentRow.Field<decimal>("loanDayTotalPayment"), paymentRow.Field<decimal>("loanDayInterestPayment"), paymentRow.Field<decimal>("loanDayPrincipalPayment"));
            }
            gridPaymentList.DataSource = paymentsDataTable;
            addPaymentColumnHeaders();

            //DataTable paymentsTabl = allDaysWithPayments.CopyToDataTable
            //DataView viewPayments = allDaysWithPayments.AsDataView();
        }

        private void addPaymentColumnHeaders()
        {
            paymentGridView.Columns["paymentDate"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            paymentGridView.Columns["paymentDate"].DefaultCellStyle.Format = "MMMM dd, yyyy";
            paymentGridView.Columns["totalPayment"].DefaultCellStyle.Format = "c2";
            paymentGridView.Columns["principalPayment"].DefaultCellStyle.Format = "c2";
            paymentGridView.Columns["interestPayment"].DefaultCellStyle.Format = "c2";
            paymentGridView.Columns["paymentDate"].HeaderText = "Payment Date";
            paymentGridView.Columns["totalPayment"].HeaderText = "Total Payment";
            paymentGridView.Columns["interestPayment"].HeaderText = "Interest Payment";
            paymentGridView.Columns["principalPayment"].HeaderText = "Principal Payment";

        }

        private void buttonClosePrincipleAdjust_Click(object sender, EventArgs e)
        {


            //this.Hide();
        }

        private void buttonCloseAddPayment_Click(object sender, EventArgs e)
        {

            if (loanReportMain.LoanReportData.LoanGenerated)
            {
                foreach (DataRow payments in paymentsDataTable.Rows)
                {
                    var paymentDate = payments.Field<DateTime>("paymentDate");
                    bool rowExists = loanReportMain.LoanReportData.LoanDataTable.Rows.Contains(paymentDate);
                    if (rowExists)
                    { 
                        loanReportMain.LoanReportData.LoanDataTable.Rows.Find(paymentDate)[5] = payments[1];
                        loanReportMain.LoanReportData.LoanDataTable.Rows.Find(paymentDate)[6] = payments[2];
                        loanReportMain.LoanReportData.LoanDataTable.Rows.Find(paymentDate)[7] = payments[3];
                    }
               }
                loanReportMain.recalculateLoan();
            }
            else
            {
                MessageBox.Show("Please generate the loan first.");
            }
            Close();
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            bool paymentExists = paymentsDataTable.Rows.Contains(inputPaymentDate.Value.Date);
            if (!paymentExists)
            {
                paymentsDataTable.Rows.Add(inputPaymentDate.Value.Date, paymentAmount, paymentInterestAmount, paymentPrincipalAmount);
            }
            else if (paymentExists)
            {
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[1] = paymentAmount;
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[2] = paymentInterestAmount;
                paymentsDataTable.Rows.Find(inputPaymentDate.Value.Date)[3] = paymentPrincipalAmount;
            }

            inputPaymentAmount.Text = "";
            inputPaymentInterestAmount.Value = 0;
            inputPaymentPrincipalAmount.Value = 0;
            inputPaymentAllocationTrack.Value = 0;
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        {
            //paymentPrincipalAmount = trackBarValue * trackBarTick;
            //inputPaymentPrincipalAmount.Value = trackBarTick * inputPaymentAllocationTrack.Value;
            //inputPaymentInterestAmount.Value = inputPaymentAmount.Value - inputPaymentPrincipalAmount.Value;
        }

        private void inputPaymentPrincipalAmount_ValueChanged(object sender, EventArgs e)
        {
           // paymentDelegation();
        }

        private void inputPaymentInterestAmount_ValueChanged(object sender, EventArgs e)
        {
            //paymentDelegation();
        }

        private void inputPaymentAmount_ValueChanged(object sender, EventArgs e)
        {

            //MessageBox.Show("Value Changed");
            //if (inputPaymentAmount.Value > 400)
            //{
            //    if (inputPaymentAmount.Value != 0)
            //    {

            //        paymentAmount = inputPaymentAmount.Value;

            //        paymentPrincipalAmount = paymentAmount / 2;
            //        paymentInterestAmount = paymentAmount - paymentPrincipalAmount;
            //        inputPaymentPrincipalAmount.Value = paymentPrincipalAmount;
            //        inputPaymentInterestAmount.Value = paymentInterestAmount;
            //        trackBarValue = 1;
            //        trackBarValue = Convert.ToInt32(paymentAmount / 400);
            //        trackBarTick = paymentAmount / 400;
            //        trackBarValue = (Convert.ToInt32(paymentAmount) / trackBarValue) / 2;
            //        inputPaymentAllocationTrack.Value = trackBarValue;
            //    }
            //}
        }

        private void inputPaymentDate_ValueChanged(object sender, EventArgs e)
        {
           
        }


        private void paymentDelegation()
        {
           // paymentAmount = OLDinputPaymentAmount.Value;
            paymentInterestAmount = inputPaymentInterestAmount.Value;
            paymentPrincipalAmount = inputPaymentPrincipalAmount.Value;
        }

        public void updatePaymentList()
        {

        }

        private void inputPaymentAmount_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void inputPaymentAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void inputPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            panelPaymentAllocation.Visible = true;
            paymentAmount = Convert.ToDecimal(inputPaymentAmount.Text);

            panelPaymentAllocation.Visible = true;
            trackBarTick = paymentAmount / 400;
        }
    }
}
