using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    partial class LoanPayments : Page, INotifyPropertyChanged
    {
        private LoanReportData loanReportDataObj;
        public LoanPayments(LoanReportData loanReportDataObj)
        {
            this.LoanReportDataObj = loanReportDataObj;
            InitializeComponent();
            this.DataContext = this;
            //inputPaymentDate.DataContext = this;
            //inputPaymentAmount.DataContext = this;
            //inputPaymentPrincipalAmount.DataContext = this;
            //inputPaymentInterestAmount.DataContext = this;
            SelectedDate = this.LoanReportDataObj.StartDate;
            displayedControlsCheck();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Payment selectedPayment;
        public Payment ActivePayment
        {
            get { return selectedPayment; }
            set
            {
                if (value != ActivePayment)
                {
                    selectedPayment = value;
                    Notify("ActivePayment");
                }
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                if (ActivePayment != null)
                {
                    return ActivePayment.PaymentDate;
                }
                else
                {
                    return LoanReportDataObj.StartDate;
                }
            }
            set
            {
                if (null == ActivePayment)
                {
                    ActivePayment = new Payment(LoanReportDataObj.LoanGuid);
                    ActivePayment.PaymentDate = value;
                    TotalPaymentAmount = 0;
                    InterestPaymentAmount = 0;
                    PrincipalPaymentAmount = 0;
                    Notify("PrincipalPaymentAmount");
                    Notify("InterestPaymentAmount");
                    Notify("TotalPaymentAmount");
                }
                // inputPaymentDate.SelectedDate = value;

                else if (LoanReportDataObj.PaymentList.Any(date => date.PaymentDate == value))
                {
                    // checkForPayment = LoanReportDataObj.PaymentList.Contains(ActivePayment) ? ActivePayment  : null;
                    Payment locatedPayment = LoanReportDataObj.PaymentList.Where(p => p.PaymentDate == value).FirstOrDefault();
                    PaymentGuid = locatedPayment.PaymentGuid;
                    ActivePayment.PaymentDate = value;
                    TotalPaymentAmount = locatedPayment.TotalPaymentAmount;
                    InterestPaymentAmount = locatedPayment.InterestPaymentAmount;
                    PrincipalPaymentAmount = locatedPayment.PrincipalPaymentAmount;



                }

                else if (ActivePayment.PaymentDate != value)
                {
                    ActivePayment = new Payment(LoanReportDataObj.LoanGuid);

                    ActivePayment.PaymentDate = value;
                    TotalPaymentAmount = 0;
                    InterestPaymentAmount = 0;
                    PrincipalPaymentAmount = 0;
                    Notify("PrincipalPaymentAmount");
                    Notify("InterestPaymentAmount");
                    Notify("TotalPaymentAmount");
                }

                else
                {
                    //ActivePayment.PaymentDate = value;
                    //TotalPaymentAmount = 0;
                    //InterestPaymentAmount = 0;
                    //PrincipalPaymentAmount = 0;
                    // ActivePayment = checkForPayment;
                    //ActivePayment.PaymentDate = checkForPayment.PaymentDate;
                    //ActivePayment.TotalPaymentAmount = checkForPayment.TotalPaymentAmount;
                    //ActivePayment.InterestPaymentAmount = checkForPayment.InterestPaymentAmount;
                    //ActivePayment.PrincipalPaymentAmount = checkForPayment.PrincipalPaymentAmount;
                }
                formatColumns();
                displayedControlsCheck();
                updateTextBoxWithExistingPaymentData();

                Notify("SelectedDate");
                Notify("ActivePayment");
            }
        }

        public decimal TotalPaymentAmount
        {
            get
            {
                if (ActivePayment != null)
                {
                    return ActivePayment.TotalPaymentAmount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (TotalPaymentAmount != value)
                {
                    ActivePayment.TotalPaymentAmount = value;
                    Notify("TotalPaymentAmount");
                }
            }
        }
        public decimal InterestPaymentAmount
        {
            get
            {
                if (ActivePayment != null)
                {
                    return ActivePayment.InterestPaymentAmount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (InterestPaymentAmount != value)
                {
                    ActivePayment.InterestPaymentAmount = value;
                    Notify("InterestPaymentAmount");
                }
            }
        }
        public decimal PrincipalPaymentAmount
        {
            get
            {
                if (ActivePayment != null)
                {
                    return ActivePayment.PrincipalPaymentAmount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (PrincipalPaymentAmount != value)
                {
                    ActivePayment.PrincipalPaymentAmount = value;
                    Notify("PrincipalPaymentAmount");
                }
            }
        }

        public Guid PaymentGuid
        {
            get
            {
                return ActivePayment.PaymentGuid;
            }
            set
            {
                ActivePayment.PaymentGuid = value;
            }
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

        public DateTime PaymentDate
        {
            get
            {
                if (ActivePayment != null)
                {
                    return SelectedDate;
                }
                else
                    return LoanReportDataObj.StartDate;
            }

            set
            {
                if (SelectedDate != value)
                {
                    SelectedDate = value;
                    Notify("SelectedDate");
                }
            }
        }

        public int LargeSliderTick
        {
            get
            {
               
                return Decimal.ToInt32(TotalPaymentAmount / 100);

            }

            set
            {
                LargeSliderTick = value;
                Notify("LargeSliderTick");
            }
        }

        public DateTime StartDate
        {
            get
            {
                return LoanReportDataObj.StartDate;
            }
            set
            {
                if (StartDate != value)
                {
                    StartDate = value;
                    Notify("StartDate");
                }
            }
        }

        private void displayedControlsCheck()
        {
            // ((DatePicker)inputPaymentDate).DisplayDateStart = loanReportDataObj.StartDate;
            // panelInterestAmount.Visibility = Visibility.Hidden;
            // panelPrincipalAmount.Visibility = Visibility.Hidden;
            updateAllocationPercent();
            if (ActivePayment != null && TotalPaymentAmount > 0 && TotalPaymentAmount > 0)
            {
                buttonAddPayment.Visibility = Visibility.Visible;
                if (TotalPaymentAmount > 0 && TotalPaymentAmount > 0 && TotalPaymentAmount >= 100)
                {
                    inputPaymentAllocationTrack.Visibility = Visibility.Visible;
                    panelInterestAmount.Visibility = Visibility.Visible;
                    panelPrincipalAmount.Visibility = Visibility.Visible;
                    updateAllocationPercent();
                }
                else if (ActivePayment != null && TotalPaymentAmount > 0 && TotalPaymentAmount > 0 && TotalPaymentAmount <= 100)
                {
                    panelInterestAmount.Visibility = Visibility.Visible;
                    panelPrincipalAmount.Visibility = Visibility.Visible;
                    //        inputPaymentAllocationTrack.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                buttonAddPayment.Visibility = Visibility.Hidden;
            }
        }

        private void updateAllocationPercent()
        {
            labelInterestPercent.Content = "";
            labelPrincipalPercent.Content = "";
            if (ActivePayment.InterestPaymentAmount > 0 && ActivePayment.PrincipalPaymentAmount > 0 && ActivePayment.TotalPaymentAmount >= 1)
            {
                labelInterestPercent.Content = (InterestPaymentAmount / TotalPaymentAmount).ToString("0.00%");
                labelPrincipalPercent.Content = (PrincipalPaymentAmount / TotalPaymentAmount).ToString("0.00%");

            }
            else
            {
                labelInterestPercent.Content = "";
                labelPrincipalPercent.Content = "";
            }


        }

        private void adjustAllocation()
        {
            if (ActivePayment.PrincipalPaymentAmount > 0 && ActivePayment.PrincipalPaymentAmount <= ActivePayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            {
                inputPaymentInterestAmount.Text = (ActivePayment.TotalPaymentAmount - ActivePayment.PrincipalPaymentAmount).ToString();
                ActivePayment.InterestPaymentAmount = ActivePayment.TotalPaymentAmount - ActivePayment.PrincipalPaymentAmount;

                updateAllocationPercent();
            }
            else if (ActivePayment.PrincipalPaymentAmount > ActivePayment.TotalPaymentAmount && inputPaymentPrincipalAmount.IsFocused == true)
            {
                MessageBox.Show("Principal exceeded the payment amount, please adjust the value.", "Payments");
            }

            else if (ActivePayment.InterestPaymentAmount > 0 && ActivePayment.InterestPaymentAmount <= ActivePayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            {
                inputPaymentPrincipalAmount.Text = (ActivePayment.TotalPaymentAmount - ActivePayment.InterestPaymentAmount).ToString();

                updateAllocationPercent();
                displayedControlsCheck();
            }

            else if (ActivePayment.InterestPaymentAmount > ActivePayment.TotalPaymentAmount && inputPaymentInterestAmount.IsFocused == true)
            {
                MessageBox.Show("Interest exceeded the payment amount, please adjust the value.", "Payments");
            }
        }

        private void updateTextBoxWithExistingPaymentData()
        {
            if (ActivePayment != null && LoanReportDataObj.LoanDataTable != null)
            {
                DataRow dateRow = LoanReportDataObj.LoanDataTable.Rows.Find(PaymentDate);
                if (dateRow != null)
                {
                    labelSelectedDayInfo.Content = "Balance details of the selected day: \r\n" +
                        "Principal Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[1]).ToString() + "\r\n" +
                        "Interest Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[5]).ToString() + "\r\n" +
             "Current Balance: " + LoanReportDataObj.ActiveLoan.LoanCurrency + " " + FormatDigitInput.FormatToDecimal(dateRow[9]).ToString() + "\r\n";
                }
            }
        }

        private void inputPaymentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

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

        int indexOfPayment;
        private void buttonAddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.PaymentList.Count > 0)
            {
                foreach (Payment p in LoanReportDataObj.PaymentList.ToList())
                {
                    if (p.PaymentGuid == ActivePayment.PaymentGuid)
                    {
                        indexOfPayment = LoanReportDataObj.PaymentList.IndexOf(p);
                    }
                    else
                    {
                        indexOfPayment = -1;
                    }
                }
            }
            else
            {
                indexOfPayment = -1;
            }
            if (indexOfPayment > -1)
            {
                Payment p = LoanReportDataObj.PaymentList.ElementAt(indexOfPayment);
                p.PaymentGuid = ActivePayment.PaymentGuid;
                p.PaymentDate = ActivePayment.PaymentDate;
                p.TotalPaymentAmount = ActivePayment.TotalPaymentAmount;
                p.InterestPaymentAmount = ActivePayment.InterestPaymentAmount;
                p.PrincipalPaymentAmount = ActivePayment.PrincipalPaymentAmount;
            }
            else
            {
                addNewPayment();
            }
            gridPaymentList.Visibility = Visibility.Visible;
            LoanReportDataObj.CalculateLoan();
            displayedControlsCheck();
            formatColumns();
        }

        private void addNewPayment()
        {
            Payment addPayment = new Payment(LoanReportDataObj.LoanGuid);
            addPayment.PaymentGuid = ActivePayment.PaymentGuid;
            addPayment.PaymentDate = SelectedDate;
            addPayment.TotalPaymentAmount = ActivePayment.TotalPaymentAmount;
            addPayment.InterestPaymentAmount = ActivePayment.InterestPaymentAmount;
            addPayment.PrincipalPaymentAmount = ActivePayment.PrincipalPaymentAmount;
            LoanReportDataObj.PaymentList.Add(addPayment);
            addPayment = null;
        }

        private void buttonRemovePayment_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReportDataObj.PaymentList.Count > 0)
            {
                if (LoanReportDataObj.PaymentList.Any(p => PaymentDate == ActivePayment.PaymentDate))
                {
                    Payment paymentRemove = (Payment)LoanReportDataObj.PaymentList.Where(p => p.PaymentGuid == ActivePayment.PaymentGuid).FirstOrDefault();
                    LoanReportDataObj.PaymentList.Remove(paymentRemove);
                    paymentRemove = null;
                    displayedControlsCheck();
                    formatColumns();
                }
            }
            else
            {
                MessageBox.Show("No payment found to remove...", "Payment Removal");
            }
        }

        private void inputPaymentAllocationTrack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inputPaymentAllocationTrack.IsFocused)
            {
                InterestPaymentAmount = FormatDigitInput.FormatToDecimal((decimal)inputPaymentAllocationTrack.Value);
                PrincipalPaymentAmount = FormatDigitInput.FormatToDecimal(inputPaymentAllocationTrack.Maximum - inputPaymentAllocationTrack.Value);

                updateAllocationPercent();
            }
        }

        private void gridPaymentList_Loaded(object sender, RoutedEventArgs e)
        {
            formatColumns();
        }


        private void formatColumns()
        {
            gridPaymentList.Columns.Clear();
            gridPaymentList.ItemsSource = null;
            gridPaymentList.ItemsSource = LoanReportDataObj.PaymentList;

            if (gridPaymentList.ItemsSource != null)
            {

                if (LoanReportDataObj.PaymentList.Count >= 1)
                {
                    gridPaymentList.Visibility = Visibility.Visible;

                    DataGridTextColumn gPaymentDate = new DataGridTextColumn();
                    gridPaymentList.Columns.Add(gPaymentDate);
                    gridPaymentList.Columns[0].Header = "Payment Date";
                    ((DataGridTextColumn)gridPaymentList.Columns[0]).Binding = new Binding("PaymentDate");
                    ((DataGridTextColumn)gridPaymentList.Columns[0]).Binding.StringFormat = "MM/dd/yyyy";

                    DataGridTextColumn jTotalPayment = new DataGridTextColumn();
                    gridPaymentList.Columns.Add(jTotalPayment);
                    gridPaymentList.Columns[1].Header = "Total Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[1]).Binding = new Binding("TotalPaymentAmount");
                    ((DataGridTextColumn)gridPaymentList.Columns[1]).Binding.StringFormat = "N";

                    DataGridTextColumn gInterestPayment = new DataGridTextColumn();
                    gridPaymentList.Columns.Add(gInterestPayment);
                    gridPaymentList.Columns[2].Header = "Interest Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[2]).Binding = new Binding("InterestPaymentAmount");
                    ((DataGridTextColumn)gridPaymentList.Columns[2]).Binding.StringFormat = "N";

                    DataGridTextColumn gPrincipalPayment = new DataGridTextColumn();
                    gridPaymentList.Columns.Add(gPrincipalPayment);
                    gridPaymentList.Columns[3].Header = "Principal Payment \n(" + LoanReportDataObj.Currency + ")";
                    ((DataGridTextColumn)gridPaymentList.Columns[3]).Binding = new Binding("PrincipalPaymentAmount");
                    ((DataGridTextColumn)gridPaymentList.Columns[3]).Binding.StringFormat = "N";


                }
                else
                {

                }
            }
        }

        private void gridPaymentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridPaymentList.SelectedItems.Count > 0)
            {
                Payment p = (Payment)gridPaymentList.SelectedItems[0];
                ActivePayment.PaymentGuid = p.PaymentGuid;
                PaymentDate = p.PaymentDate;
                ActivePayment.TotalPaymentAmount = p.TotalPaymentAmount;
                ActivePayment.InterestPaymentAmount = p.InterestPaymentAmount;
                ActivePayment.PrincipalPaymentAmount = p.PrincipalPaymentAmount;

            }
        }
    }
}