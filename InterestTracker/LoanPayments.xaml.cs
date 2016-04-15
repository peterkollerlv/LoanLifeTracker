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
    public partial class LoanPayments : Page
    {
        protected LoanReportData loanReportDataObj;
        Payment checkForPayment;
        CollectionViewSource itemCollectionViewSource;

        public LoanPayments(LoanReportData loanReportDataObj)
        {

            this.loanReportDataObj = loanReportDataObj;

            InitializeComponent();


            SelectedPayment = new Payment(loanReportDataObj.LoanGuid);
            SelectedPayment.PaymentDate = this.loanReportDataObj.StartDate;

            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
           itemCollectionViewSource.Source = loanReportDataObj.PaymentList;

            this.DataContext = new
            {
                payment = this.SelectedPayment,
                loanObject = this.loanReportDataObj
            };



            // SelectedPayment.TotalPaymentAmount.Bi.Add("Text", paymentBindingSource, "TotalPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");
            //   inputPaymentInterestAmount.DataBindings.Add("Text", paymentBindingSource, "InterestPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");
            // inputPaymentPrincipalAmount.DataBindings.Add("Text", paymentBindingSource, "PrincipalPaymentAmount", true, DataSourceUpdateMode.Never, 0, "N2");

            //   panelInterestAmount.Visible = false;
            //panelPrincipalAmount.Visible = false;
            //inputPaymentAllocationTrack.Visible = false;
            //labelInterestPercent.Text = "";
            //labelPrincipalPercent.Text = "";

            //  gridPaymentList.Visibility = Visibility.Hidden;

            // createPaymentTable();
            //  addPaymentsToGrid();

            //if(SelectedPayment != null)
            displayedControlsCheck();
        }



        // protected LoanReportMain loanReportMainObj;
        //   protected LoanReportData loanReportDataObj;
        // private DataTable paymentsDataTable;

        private decimal trackBarTick;

        //public DateTime SelectedDate
        //{
        //    get;
        //    //{
        //    //    return inputPaymentDate.SelectedDate.Value.Date;
        //    //}
        //    set;
        //    //{
        //    //    inputPaymentDate.SelectedDate = value;
        //    //}
        //}

        private Payment selectedPayment;

        public Payment SelectedPayment
        {
            get
            {
                return selectedPayment;
            }
            set
            {
                //if (value == null)
                //{
                //    selectedPayment = new Payment(loanReportDataObj.LoanGuid);
                //    this.DataContext = this.SelectedPayment;
                //    //  selectedPayment.PaymentDate = SelectedDate;
                //}
                //else
                //{
                    selectedPayment = value;
                    //this.DataContext = value;
               // }
                
                
            }
        }

        //public decimal SelectedPayment.TotalPaymentAmount
        //{
        //    get
        //    {
        //        return FormatDigitInput.FormatToDecimal(SelectedPayment.TotalPaymentAmount.Text);
        //    }
        //    set
        //    {
        //        //foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
        //        //{
        //        //    if (binding.Control.Name == "SelectedPayment.TotalPaymentAmount.Text")
        //        //    {
        //        //        binding.WriteValue();
        //        //    }
        //        //}
        //        SelectedPayment.TotalPaymentAmount.Text = value.ToString();
        //    }
        //}

        //public decimal SelectedPayment.InterestPaymentAmount
        //{
        //    get
        //    {
        //        return FormatDigitInput.FormatToDecimal(inputPaymentInterestAmount.Text);
        //    }
        //    set
        //    {
        //        //foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
        //        //{
        //        //    if (binding.Control.Name == "inputPaymentInterestAmount.Text")
        //        //    {
        //        //        binding.WriteValue();
        //        //    }
        //        //}
        //        inputPaymentInterestAmount.Text = value.ToString();
        //    }
        //}

        //public decimal SelectedPayment.PrincipalPaymentAmount
        //{
        //    get
        //    {
        //        return FormatDigitInput.FormatToDecimal(inputPaymentPrincipalAmount.Text);
        //    }
        //    set
        //    {
        //        //foreach (Binding binding in paymentBindingSource.CurrencyManager.Bindings)
        //        //{
        //        //    if (binding.Control.Name == "inputPaymentPrincipalAmount.Text")
        //        //    {
        //        //        binding.WriteValue();
        //        //    }
        //        //}
        //        inputPaymentPrincipalAmount.Text = value.ToString();
        //    }
        //}



        private void displayedControlsCheck()
        {

            //if (SelectedPayment != null && SelectedPayment.TotalPaymentAmount > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) > 0)
            //{
            //    buttonAddPayment.Visibility = Visibility.Visible;
            //    if (SelectedPayment.TotalPaymentAmount > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) >= 400)
            //    {
            //        inputPaymentAllocationTrack.Visibility = Visibility.Visible;
            //        panelInterestAmount.Visibility = Visibility.Visible;
            //        panelPrincipalAmount.Visibility = Visibility.Visible;
            //        trackBarTick = SelectedPayment.TotalPaymentAmount / 400;
            //        updateAllocationPercent();
            //    }
            //    else if (SelectedPayment != null && SelectedPayment.TotalPaymentAmount > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) <= 400)
            //    {
            //        panelInterestAmount.Visibility = Visibility.Visible;
            //        panelPrincipalAmount.Visibility = Visibility.Visible;
            //        inputPaymentAllocationTrack.Visibility = Visibility.Hidden;
            //    }
            //}
            //else
            //{
            //    buttonAddPayment.Visibility = Visibility.Hidden;
            //}
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

        private void setSelectedPayment(DateTime selectedDate)
        {
            //if (loanReportDataObj.PaymentList.Count > 0)
            //{
            //    foreach (Payment payment in loanReportDataObj.PaymentList)
            //    {
            //        if (payment.PaymentDate == selectedDate)
            //        {
            //            SelectedPayment = payment;
            //            buttonRemovePayment.Visibility = Visibility.Visible;
            //            break;
            //        }
            //        else if (payment.PaymentDate != selectedDate) // && loanReportDataObj.ActiveLoan.PaymentsList.Contains(payment)
            //        {
            //            SelectedPayment = null;
            //            SelectedPayment = new Payment();
            //            SelectedPayment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
            //            SelectedPayment.PaymentDate = SelectedDate;
            //            buttonRemovePayment.Visibility = Visibility.Hidden;
            //            SelectedPayment.TotalPaymentAmount = 0;
            //            // inputPaymentInterestAmount.Text = "";
            //            //    inputPaymentPrincipalAmount.Text = "";
            //        }
            //    }
            //}
            //else
            //{
            //    buttonRemovePayment.Visibility = Visibility.Hidden;
            //    SelectedPayment = null;
            //    SelectedPayment = new Payment();

            //    SelectedPayment.LoanGUID = loanReportDataObj.ActiveLoan.LoanGuid;
            //    SelectedPayment.PaymentDate = SelectedDate;
             
            //}
            //this.gridPaymentList.ItemsSource = loanReportDataObj.PaymentList;
        }

        private void createPaymentTable()
        {
            //paymentsDataTable = null;
            //paymentsDataTable = new DataTable();
            //paymentsDataTable.Columns.Add("paymentDate", typeof(DateTime));
            //paymentsDataTable.Columns.Add("totalPayment", typeof(decimal));
            //paymentsDataTable.Columns.Add("interestPayment", typeof(decimal));
            //paymentsDataTable.Columns.Add("principalPayment", typeof(decimal));
            //paymentsDataTable.PrimaryKey = new DataColumn[] { paymentsDataTable.Columns["paymentDate"] };
        }

        private void addPaymentsToGrid()
        {
            //  this.gridPaymentList.ItemsSource = loanReportDataObj.PaymentList;
            //paymentsDataTable.Rows.Clear();
            //foreach (Payment payment in loanReportDataObj.PaymentList)
            //{
            //    DataRow paymentRow = paymentsDataTable.NewRow();
            //    paymentRow[0] = payment.PaymentDate;
            //    paymentRow[1] = payment.TotalPaymentAmount;
            //    paymentRow[2] = payment.InterestPaymentAmount;
            //    paymentRow[3] = payment.PrincipalPaymentAmount;
            //    paymentsDataTable.Rows.Add(paymentRow);
            //}
            //paymentsDataTable.AcceptChanges();
            //gridPaymentList.ItemsSource = paymentsDataTable.AsDataView();
            if (loanReportDataObj.PaymentList.Count > 0)
            {
                gridPaymentList.Visibility = Visibility.Visible;
              //  formatPaymentColumnHeaders();
            }

        }

        private void formatPaymentColumnHeaders()
        {
           // this.gridPaymentList.ItemsSource = loanReportDataObj.PaymentList;
            //if (gridPaymentList.Items.Count <= 1)
            //{

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
        }

        private void adjustAllocation()
        {
            //if (SelectedPayment.PrincipalPaymentAmount > 0 && SelectedPayment.PrincipalPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            //{
            //    inputPaymentInterestAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount).ToString();
            //    SelectedPayment.InterestPaymentAmount = SelectedPayment.TotalPaymentAmount - SelectedPayment.PrincipalPaymentAmount;
            //    if (inputPaymentAllocationTrack.IsVisible)
            //    {
            //        decimal setTrackValue;
            //        setTrackValue = SelectedPayment.InterestPaymentAmount / trackBarTick;
            //        inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
            //    }
            //    updateAllocationPercent();
            //}
            //else if (SelectedPayment.PrincipalPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            //{
            //    MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
            //}

            //else if (SelectedPayment.InterestPaymentAmount > 0 && SelectedPayment.InterestPaymentAmount <= SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            //{
            //    inputPaymentPrincipalAmount.Text = (SelectedPayment.TotalPaymentAmount - SelectedPayment.InterestPaymentAmount).ToString();
            //    if (inputPaymentAllocationTrack.IsVisible)
            //    {
            //        decimal setTrackValue;
            //        setTrackValue = SelectedPayment.InterestPaymentAmount / trackBarTick;
            //        inputPaymentAllocationTrack.Value = Convert.ToInt32(setTrackValue);
            //    }
            //    updateAllocationPercent();
            //}

            //else if (SelectedPayment.InterestPaymentAmount > SelectedPayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            //{
            //    MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
            //}
        }

        private void updateTextBoxWithExistingPaymentData()
        {
            if (loanReportDataObj.LoanDataTable != null)
            {
                DataRow dateRow = loanReportDataObj.LoanDataTable.Rows.Find(SelectedPayment.PaymentDate);
                if (dateRow != null)
                {
                    labelSelectedDayInfo.Content = "Balance details of the selected day: \r\n" +
                        "Principal Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
                        "Interest Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[4]).ToString() + "\r\n" +
             "Current Balance: " + loanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";
                }
            }
        }

        //private void inputPaymentAllocationTrack_ValueChanged(object sender, EventArgs e)
        //{

        //}

        private void inputPaymentAllocationTrack_DragOver(object sender, DragEventArgs e)
        {
            if (inputPaymentAllocationTrack.IsFocused)
            {
                decimal calculatedPrinciple;
                decimal calculatedInterest;
                calculatedInterest = FormatDigitInput.FormatToDecimal(trackBarTick * (decimal)inputPaymentAllocationTrack.Value);
                SelectedPayment.InterestPaymentAmount = calculatedInterest;
                inputPaymentInterestAmount.Text = calculatedInterest.ToString();
                calculatedPrinciple = FormatDigitInput.FormatToDecimal(SelectedPayment.TotalPaymentAmount - calculatedInterest);
                SelectedPayment.PrincipalPaymentAmount = calculatedPrinciple;
                inputPaymentPrincipalAmount.Text = calculatedPrinciple.ToString();
                updateAllocationPercent();
            }
        }


        private void inputPaymentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            checkForPayment = loanReportDataObj.PaymentList.Find(delegate (Payment p)
            {
                if (p.PaymentDate == ((DatePicker)sender).SelectedDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            );

            if (checkForPayment == null || SelectedPayment.PaymentDate != checkForPayment.PaymentDate)
            {
                SelectedPayment = new Payment(loanReportDataObj.LoanGuid);               
                SelectedPayment.PaymentDate = ((DatePicker)sender).SelectedDate.Value;
                this.DataContext = new
                {
                    payment = this.SelectedPayment,
                    loanObject = this.loanReportDataObj
                };
            }

            else
            {
                SelectedPayment = checkForPayment;
                this.DataContext = new
                {
                    payment = this.SelectedPayment,
                    loanObject = this.loanReportDataObj
                };
            }
 
            //setSelectedPayment(SelectedDate);
            // DataContext
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
           // ((TextBox)sender).GetBindingExpression(TextBox.BindingGroupProperty).UpdateTarget();
            //if (SelectedPayment.TotalPaymentAmount > 0 && Convert.ToDecimal(SelectedPayment.TotalPaymentAmount) > 0 && inputPaymentAmount.IsFocused == true)
            //{
            // //   inputPaymentInterestAmount.Text = "";
            //   // inputPaymentPrincipalAmount.Text = "";
            //    inputPaymentAllocationTrack.Value = 0;
            //    displayedControlsCheck();
            //}
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
            //SelectedPayment.TotalPaymentAmount = SelectedPayment.TotalPaymentAmount;
            //SelectedPayment.InterestPaymentAmount = SelectedPayment.InterestPaymentAmount;
            //SelectedPayment.PrincipalPaymentAmount = SelectedPayment.PrincipalPaymentAmount;
        }


        private void buttonAddPayment_Click(object sender, RoutedEventArgs e)
        {
            checkForPayment = loanReportDataObj.PaymentList.Find(delegate (Payment p)
            {
                if (p.PaymentDate == SelectedPayment.PaymentDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
  );
            if (checkForPayment == null) // || checkForPayment.PaymentDate != SelectedPayment.PaymentDate && SelectedPayment != null)
            {

                loanReportDataObj.PaymentList.Add(SelectedPayment);
                SelectedPayment = new Payment(loanReportDataObj.LoanGuid);
                SelectedPayment.PaymentDate = ((DatePicker)inputPaymentDate).SelectedDate.Value;
                SelectedPayment.TotalPaymentAmount = 0;
                SelectedPayment.InterestPaymentAmount = 0;
                SelectedPayment.PrincipalPaymentAmount = 0;
               // SelectedPayment = null;
                
                //   addPaymentsToGrid();
                // loanReportMainObj.textPaymentList.Text = "";
            }
            else if (checkForPayment.PaymentDate == SelectedPayment.PaymentDate)
            {
                SelectedPayment = checkForPayment;
                //paymentsDataTable.Rows.Find(SelectedDate)[1] = SelectedPayment.TotalPaymentAmount;
                //paymentsDataTable.Rows.Find(SelectedDate)[2] = SelectedPayment.InterestPaymentAmount;
                //paymentsDataTable.Rows.Find(SelectedDate)[3] = SelectedPayment.PrincipalPaymentAmount;
            }

            // loanReportMainObj.textPaymentList.Text = "Existing Payments: \r\n";
            //foreach (Payment individualPayment in loanReportDataObj.ActiveLoan.PaymentsList)
            //{
            //    loanReportMainObj.textPaymentList.Text += individualPayment.PaymentDate.ToShortDateString() + " " + individualPayment.TotalPaymentAmount + " " + individualPayment.InterestPaymentAmount + " " + individualPayment.PrincipalPaymentAmount + "\r\n";
            //}
            //inputPaymentAllocationTrack.Value = 0;
            //labelInterestPercent.Content = "";
            //labelPrincipalPercent.Content = "";
          //  updateAllocationPercent();
          formatPaymentColumnHeaders();
            gridPaymentList.Visibility = Visibility.Visible;
          //  setSelectedPayment(SelectedDate);
            loanReportDataObj.CalculateLoan();

        }



        private void buttonRemovePayment_Click(object sender, RoutedEventArgs e)
        {
            //if (paymentsDataTable.Rows.Count > 0 && Convert.ToDateTime(paymentsDataTable.Rows.Find(SelectedDate)[0]) != null)
            //{
            //    loanReportDataObj.PaymentList.Remove(SelectedPayment);
            //    paymentsDataTable.Rows.Find(SelectedDate).Delete();
            //    paymentsDataTable.AcceptChanges();
            //    addPaymentsToGrid();
            //    loanReportDataObj.CalculateLoan();
            //    setSelectedPayment(SelectedDate);

            if (loanReportDataObj.PaymentList.Contains(SelectedPayment))
            {
                loanReportDataObj.PaymentList.Remove(SelectedPayment);
            }


            else
            {
                MessageBox.Show("Please pick a date with a payment.");
            }
        }

        //private void buttonClosePrincipleAdjust_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        //private void buttonCloseAddPayment_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        private void gridPaymentList_Click(object sender, EventArgs e)
        {
            //    int paymentRows = gridPaymentList.Rows.GetRowCount(DataGridViewElementStates.Selected);
            //    if (paymentRows == 1)
            //    {
            //        inputPaymentDate.Value = Convert.ToDateTime(gridPaymentList.SelectedRows[paymentRows - 1].Cells[0].Value);
            //        setSelectedPayment(SelectedDate);
            //    }
        }


    }
}


