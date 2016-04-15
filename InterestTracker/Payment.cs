using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace InterestTracker
{
    public class Payment : INotifyPropertyChanged
    {
        public Payment(Guid loanGuid)
        {
            this.LoanGUID = loanGuid;
        }
        private DateTime paymentDate;
        private decimal totalPaymentAmount = 0;
        private decimal interestPaymentAmount = 0;
        private decimal principalPaymentAmount = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Guid LoanGUID { get; set; }
        public DateTime PaymentDate
        {
            get { return this.paymentDate; }

            set
            {
                if (value != this.paymentDate)
                {
                    this.paymentDate = value;
                    NotifyPropertyChanged("PaymentDate");
                }

            }
        }
        public decimal TotalPaymentAmount
        {
            get { return this.totalPaymentAmount; }
            set
            {
                if (value != this.totalPaymentAmount)
                {
                    this.totalPaymentAmount = value;
                    NotifyPropertyChanged("TotalPaymentAmount");
                }
            }
        }
        public decimal InterestPaymentAmount
        {
            get { return this.interestPaymentAmount; }
            set
            {
                if (value != this.interestPaymentAmount)
                {
                    this.interestPaymentAmount = value;
                    NotifyPropertyChanged("InterestPaymentAmount");
                }
            }
        }
        public decimal PrincipalPaymentAmount
        {
            get { return this.principalPaymentAmount; }
            set
            {
                if (value != this.principalPaymentAmount)
                {
                    this.principalPaymentAmount = value;
                    NotifyPropertyChanged("PrincipalPaymentAmount");
                }
            }
        }
    }
}
