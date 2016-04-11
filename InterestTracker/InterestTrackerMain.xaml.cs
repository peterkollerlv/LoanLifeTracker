using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for InterestTrackerMain.xaml
    /// </summary>
    public partial class InterestTrackerMain : Window
    {

        //loan properties
        Loan activeLoan = new Loan(true);


        public string LoanTitle { get { return activeLoan.LoanTitle; } set { inputLoanTitle.Text = value; activeLoan.LoanTitle = value; } }


        public InterestTrackerMain()
        {
           
            this.KeyDown += new KeyEventHandler(FilterKeypressToDigits);
            InitializeComponent();
            activeLoan.LoanTitle = this.LoanTitle;


        }


        //ui events

        private void navLoanData_GotFocus(object sender, RoutedEventArgs e)
        {
            navMainTabControl.SelectedItem = navMainLoanData;
        }

        private void navMainLoanData_GotFocus(object sender, RoutedEventArgs e)
        {
            navTabControl.SelectedItem = navLoanData;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public void FilterKeypressToDigits(object sender, KeyEventArgs e)
        {
            if (!char.IsControl(e.Key) && ((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Back) // (char)KeyInterop.VirtualKeyFromKey(e.Key) != '.') //&& (sender as System.Windows.Controls.TextBox)sender.Text.IndexOf('.') > -1
            {
                e.Handled = true;
            }
        }
    }
}
