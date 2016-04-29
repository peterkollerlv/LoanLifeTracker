using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterestTracker
{
    /// <summary>
    /// Interaction logic for LoanPayments.xaml
    /// </summary>
    partial class LoanPayments : Page
    {
        private LoanReportData loanReportDataObj;
        Payment checkForPayment;
        CollectionViewSource itemCollectionViewSource;

        public LoanPayments(LoanReportData loanReportDataObj)
        //public LoanPayments()

        {
            this.LoanReportDataObj = loanReportDataObj;
            InitializeComponent();
            SelectedPayment = new Payment(loanReportDataObj.LoanGuid);
            SelectedDate = this.LoanReportDataObj.StartDate;
            SelectedPayment.PaymentDate = SelectedDate;
            panelPaymentAllocation.DataContext = SelectedPayment;

            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = null;
            itemCollectionViewSource.Source = loanReportDataObj.PaymentList;
            formatPaymentColumnHeaders();
            displayedControlsCheck();
        }

        private Payment selectedPayment;
        public Payment SelectedPayment
        {
            get { return selectedPayment; }
            set
            {

                selectedPayment = value;
            }
        }

        public DateTime SelectedDate
        {
            get { return inputPaymentDate.SelectedDate.Value; }
            set { inputPaymentDate.SelectedDate = value; }
        }

        internal LoanReportData LoanReportDataObj
        {
            get
            {
                return loanReportDataObj;
            }

            set
            {
                loanReportDataObj = value;
            }
        }

        private void displayedControlsCheck()
        {
            // ((DatePicker)inputPaymentDate).DisplayDateStart = loanReportDataObj.StartDate;
            panelInterestAmount.Visibility = Visibility.Hidden;
            panelPrincipalAmount.Visibility = Visibility.Hidden;
            if (SelectedPayment != null && SelectedPayment.TotalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount > 0)
            {
                buttonAddPayment.Visibility = Visibility.Visible;
                if (SelectedPayment.TotalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount >= 100)
                {
                    inputPaymentAllocationTrack.Visibility = Visibility.Visible;
                    panelInterestAmount.Visibility = Visibility.Visible;
                    panelPrincipalAmount.Visibility = Visibility.Visible;
                    updateAllocationPercent();
                }
                else if (SelectedPayment != null && SelectedPayment.TotalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount <= 100)
                {
                    panelInterestAmount.Visibility = Visibility.Visible;
                    panelPrincipalAmount.Visibility = Visibility.Visible;
                    inputPaymentAllocationTrack.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                buttonAddPayment.Visibility = Visibility.Hidden;
            }
        }

        private void updateAllocationPercent()
        {
            if (SelectedPayment.InterestPaymentAmount > 0 && SelectedPayment.PrincipalPaymentAmount > 0 && SelectedPayment.TotalPaymentAmount >= 100)
            {
                decimal onePercentOfPayment = SelectedPayment.TotalPaymentAmount / 100;
                decimal interestPercent = Math.Floor((SelectedPayment.InterestPaymentAmount / onePercentOfPayment));
                labelInterestPercent.Content = interestPercent.ToString() + "%";
                labelPrincipalPercent.Content = (100 - interestPercent).ToString() + "%";
            }
            else
            {
                labelInterestPercent.Content = "";
                labelPrincipalPercent.Content = "";
            }
        }

        private void formatPaymentColumnHeaders()
        {
            //itemCollectionViewSource.Source = null;
            //itemCollectionViewSource.Source = loanReportDataObj.PaymentList;
            //if (loanReportDataObj.PaymentList.Count >= 1 && gridPaymentList.Columns.Count != 0)
            //{
            //    gridPaymentList.Visibility = Visibility.Visible;
            //    gridPaymentList.Columns[0].Visibility = Visibility.Collapsed;
            //    gridPaymentList.Columns[1].Header = "Payment Date";
            //    ((DataGridTextColumn)gridPaymentList.Columns[1]).Binding.StringFormat = "MM/dd/yyyy";

            //    gridPaymentList.Columns[2].Header = "Total Payment \n(" + loanReportDataObj.Currency + ")";
            //    ((DataGridTextColumn)gridPaymentList.Columns[2]).Binding.StringFormat = "N";

            //    gridPaymentList.Columns[3].Header = "Interest Payment \n(" + loanReportDataObj.Currency + ")";
            //    ((DataGridTextColumn)gridPaymentList.Columns[3]).Binding.StringFormat = "N";

            //    gridPaymentList.Columns[4].Header = "Principal Payment \n(" + loanReportDataObj.Currency + ")";
            //    ((DataGridTextColumn)gridPaymentList.Columns[4]).Binding.StringFormat = "N";
            //}
            //else
            //{
            //    gridPaymentList.Visibility = Visibility.Hidden;
            //}
        }

        private void adjustAllocation()
        {
            if (SelectedPayment.PrincipalPaymentAmount > 0 && SelectedPayment.PrincipalPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            {
                inputPaymentInterestAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount).ToString();
                SelectedPayment.InterestPaymentAmount = SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount;
                if (inputPaymentAllocationTrack.IsVisible)
                {
                    inputPaymentAllocationTrack.Maximum = (double)SelectedPayment.TotalPaymentAmount;
                    inputPaymentAllocationTrack.SmallChange = (double)(SelectedPayment.TotalPaymentAmount / 100);
                    inputPaymentAllocationTrack.LargeChange = (double)(SelectedPayment.TotalPaymentAmount / 10);
                }
                updateAllocationPercent();
            }
            else if (SelectedPayment.PrincipalPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            {
                MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
            }

            else if (SelectedPayment.InterestPaymentAmount > 0 && SelectedPayment.InterestPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            {
                inputPaymentPrincipalAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.InterestPaymentAmount).ToString();
                if (inputPaymentAllocationTrack.IsVisible)
                {
                    inputPaymentAllocationTrack.Maximum = (double)SelectedPayment.TotalPaymentAmount;
                    inputPaymentAllocationTrack.SmallChange = (double)(SelectedPayment.TotalPaymentAmount / 100);
                    inputPaymentAllocationTrack.LargeChange = (double)(SelectedPayment.TotalPaymentAmount / 10);
                }
                updateAllocationPercent();
                displayedControlsCheck();
            }

            else if (SelectedPayment.InterestPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            {
                MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
            }
        }

        private void updateTextBoxWithExistingPaymentData()
        {
            if (LoanReportDataObj.LoanDataTable != null)
            {
                DataRow dateRow = LoanReportDataObj.LoanDataTable.Rows.Find(SelectedPayment.PaymentDate);
                if (dateRow != null)
                {
                    labelSelectedDayInfo.Content = "Balance details of the selected day: \r\n" +
                        "Principal Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
                        "Interest Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + "\r\n" +
             "Current Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";
                }
            }
        }

        private void inputPaymentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (LoanReportDataObj.PaymentList.Any(date => date.PaymentDate == SelectedDate))
            {
                SelectedPayment = (Payment)LoanReportDataObj.PaymentList.Select(p => p.PaymentDate == SelectedDate);
            }
            //{
            //    if (p.PaymentDate == SelectedDate)
            //    {
            //        checkForPayment = p;
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //);

            if (checkForPayment == null || SelectedDate != checkForPayment.PaymentDate)
            {
                SelectedPayment.PaymentDate = SelectedDate;
                SelectedPayment.TotalPaymentAmount = 0;
                SelectedPayment.InterestPaymentAmount = 0;
                SelectedPayment.PrincipalPaymentAmount = 0;
                checkForPayment = null;
            }

            else
            {
                SelectedPayment.PaymentDate = checkForPayment.PaymentDate;
                SelectedPayment.TotalPaymentAmount = checkForPayment.TotalPaymentAmount;
                SelectedPayment.InterestPaymentAmount = checkForPayment.InterestPaymentAmount;
                SelectedPayment.PrincipalPaymentAmount = checkForPayment.PrincipalPaymentAmount;
            }
            displayedControlsCheck();
            updateTextBoxWithExistingPaymentData();
        }

        private void inputPaymentAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == " ")
            {
                e.Handled = true;
            }
            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
            displayedControlsCheck();
        }

        private void inputPaymentInterestAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == " ")
            {
                e.Handled = true;
            }
            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
        }

        private void inputPaymentPrincipalAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == " ")
            {
                e.Handled = true;
            }
            e.Handled = !new Regex(@"^(?:\d*)?(?:\.{1})?(?:\d+)?$").IsMatch(e.Text);
        }


        private void inputPaymentAmount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inputPaymentInterestAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            adjustAllocation();
        }

        private void inputPaymentPrincipalAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            adjustAllocation();
        }

        private void panelPaymentAllocation_LostFocus(object sender, RoutedEventArgs e)
        {

        }


        private void buttonAddPayment_Click(object sender, RoutedEventArgs e)
        {
            //checkForPayment = LoanReportDataObj.PaymentList.Find(delegate (Payment p)
            //{
            //    if (p.PaymentDate == SelectedDate)
            //    {
            //        checkForPayment = p;
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
 // );
            if (checkForPayment == null)
            {
                Payment addPayment = new Payment(LoanReportDataObj.LoanGuid);
                addPayment.PaymentDate = SelectedDate;
                addPayment.TotalPaymentAmount = SelectedPayment.TotalPaymentAmount;
                addPayment.InterestPaymentAmount = SelectedPayment.InterestPaymentAmount;
                addPayment.PrincipalPaymentAmount = SelectedPayment.PrincipalPaymentAmount;
                LoanReportDataObj.PaymentList.Add(addPayment);
                addPayment = null;
                itemCollectionViewSource.Source = null;
                itemCollectionViewSource.Source = LoanReportDataObj.PaymentList;
                formatPaymentColumnHeaders();
                SelectedPayment.PaymentDate = SelectedDate;
            }
            else if (checkForPayment.PaymentDate == SelectedDate)
            {
                Payment updatePayment = new Payment(LoanReportDataObj.LoanGuid);
                updatePayment.PaymentDate = SelectedDate;
                updatePayment.TotalPaymentAmount = SelectedPayment.TotalPaymentAmount;
                updatePayment.InterestPaymentAmount = SelectedPayment.InterestPaymentAmount;
                updatePayment.PrincipalPaymentAmount = SelectedPayment.PrincipalPaymentAmount;
                LoanReportDataObj.PaymentList.Remove(checkForPayment);
                LoanReportDataObj.PaymentList.Add(updatePayment);
                SelectedPayment = checkForPayment;
                itemCollectionViewSource.Source = null;
                itemCollectionViewSource.Source = LoanReportDataObj.PaymentList;
                formatPaymentColumnHeaders();
            }

            gridPaymentList.Visibility = Visibility.Visible;
            LoanReportDataObj.CalculateLoan();
            displayedControlsCheck();
            formatColumns();
        }



        private void buttonRemovePayment_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.PaymentList.Contains(SelectedPayment))
            {
                LoanReportDataObj.PaymentList.Remove(SelectedPayment);
                itemCollectionViewSource.Source = null;
                itemCollectionViewSource.Source = LoanReportDataObj.PaymentList;
                formatPaymentColumnHeaders();
                displayedControlsCheck();
                formatColumns();
            }
            else
            {
                MessageBox.Show("Please pick a date with a payment.");
            }
        }

        private void gridPaymentList_Click(object sender, EventArgs e)
        {
            //    int paymentRows = gridPaymentList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            //    if (paymentRows == 1)
            //    {
            //        inputPaymentDate.Value = Convert.ToDateTime(gridPaymentList.SelectedRows[paymentRows - 1].Cells[0].Value);
            //        setSelectedPayment(SelectedDate);
            //    }
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inputPaymentAllocationTrack.IsFocused)
            {

                decimal calculatedPrinciple;
                decimal calculatedInterest;
                calculatedInterest = (decimal)inputPaymentAllocationTrack.Value;
                SelectedPayment.InterestPaymentAmount = calculatedInterest;
                calculatedPrinciple = (SelectedPayment.TotalPaymentAmount - calculatedInterest);
                SelectedPayment.PrincipalPaymentAmount = calculatedPrinciple;
                updateAllocationPercent();
            }
        }

        private void gridPaymentList_Loaded(object sender, RoutedEventArgs e)
        {
            formatColumns();
        }


        private void formatColumns()
        {
            if (gridPaymentList.ItemsSource != null)
            {

                itemCollectionViewSource.Source = null;
                itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
                itemCollectionViewSource.Source = LoanReportDataObj.PaymentList;
                if (LoanReportDataObj.PaymentList.Count >= 1 && gridPaymentList.Columns.Count != 0)
                {
                    gridPaymentList.Visibility = Visibility.Visible;
                    gridPaymentList.Columns[0].Visibility = Visibility.Collapsed;
                    gridPaymentList.Columns[1].Header = "Payment Date";
                    ((DataGridTextColumn)gridPaymentList.Columns[1]).Binding.StringFormat = "MM/dd/yyyy";

                    gridPaymentList.Columns[2].Header = "Total Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[2]).Binding.StringFormat = "N";

                    gridPaymentList.Columns[3].Header = "Interest Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[3]).Binding.StringFormat = "N";

                    gridPaymentList.Columns[4].Header = "Principal Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[4]).Binding.StringFormat = "N";

                    ((DataGridTextColumn)gridPaymentList.Columns[5]).Visibility = Visibility.Collapsed;
                }
                else
                {
                    gridPaymentList.Visibility = Visibility.Hidden;
                }
            }
        }

        private void gridPaymentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPaymentList.SelectedItems.Count > 0)
            {
                SelectedPayment = (Payment)gridPaymentList.SelectedItems[0];
            }
        }
    }
}