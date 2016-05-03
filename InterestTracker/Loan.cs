using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InterestTracker
{
    public class Loan //: INotifyPropertyChanged
    {
        //all loan specific data stored in this class


        //constructor for new loans

        public Loan(bool newLoan)
        {
            LoanGuid = generateNewGuid();
            LoanStartDate = DateTime.Now.Date;
            LoanInterestPenaltyDate = DateTime.Now.Date;
            LoanTitle = "Please add a title...";
            //LoanInterestRate = 5;
            loanInterestPenaltyRate = 10;
            LoanCurrency = "USD";
            LoanPaymentsList = new ObservableCollection<Payment>();
        }

        //constructor for existing loans

        public Loan(Guid existingGuid)
        {
            LoanGuid = existingGuid;
            LoanPaymentsList = new ObservableCollection<Payment>();
        }

        // required for databinding
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void Notify(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}


        //fields & properies

        private Guid loanGuid;
        public Guid LoanGuid
        {
            get
            {
                return loanGuid;
            }
            set
            {
                if (value != loanGuid)
                {
                    loanGuid = value;
                  //  Notify("LoanGuid");
                }

            }
        }

        private string loanTitle;
        public string LoanTitle
        {
            get
            {
                return loanTitle;
            }
            set
            {
                if (value != loanTitle)
                {
                    loanTitle = value;
                   // Notify("LoanTitle");
                }

            }
        }

        private string loanCompanyInfo;
        public string LoanCompanyInfo
        {
            get
            {
                return loanCompanyInfo;
            }
            set
            {
                if (value != loanCompanyInfo)
                {
                    loanCompanyInfo = value;
                  //  Notify("LoanCompanyInfo");
                }
            }
        }

        private string loanLender;
        public string LoanLender
        {
            get
            {
                return loanLender;
            }
            set
            {
                if (value != loanLender)
                {
                    loanLender = value;
                  //  Notify("LoanLender");
                }

            }
        }

        private string loanBeneficiary;
        public string LoanBeneficiary
        {
            get
            {
                return loanBeneficiary;
            }
            set
            {
                if (value != loanBeneficiary)
                {
                    loanBeneficiary = value;
                    //Notify("LoanBeneficiary");
                }
            }
        }

        private string loanCollectionAccount;
        public string LoanCollectionAccount
        {
            get
            {
                return loanCollectionAccount;
            }
            set
            {
                if (value != loanCollectionAccount)
                {
                    loanCollectionAccount = value;
                    //Notify("LoanCollectionAccount");
                }

            }
        }

        private DateTime loanStartDate;
        public DateTime LoanStartDate
        {
            get
            {
                return loanStartDate.Date;
            }
            set
            {
                if (value != loanStartDate)
                {
                    loanStartDate = value.Date;
                    //Notify("LoanStartDate");
                }
            }
        }

        private DateTime loanPaidDate;
        public DateTime LoanPaidDate
        {
            get
            {
                return loanPaidDate.Date;
            }
            set
            {
                if (value != loanPaidDate)
                {
                loanPaidDate = value.Date;
                    //Notify("LoanPaidDate");
                }
            }
        }

        private decimal loanInitialLoanAmount;
        public decimal LoanInitialLoanAmount
        {
            get
            {
                return loanInitialLoanAmount;
            }
            set
            {
                if (value != loanInitialLoanAmount)
                {
                    loanInitialLoanAmount = value;
                    //Notify("LoanInitialLoanAmount");
                }
            }
        }

        private decimal loanInterestRate;
        public decimal LoanInterestRate
        {
            get
            {
                return loanInterestRate;
            }
            set
            {
                if (value != loanInterestRate)
                {
                    loanInterestRate = value;
                    //Notify("LoanInterestRate");
                }
            }
        }

        private string loanCurrency;
        public string LoanCurrency
        {
            get
            {
                return loanCurrency;
            }
            set
            {
                if (value != loanCurrency)
                {
                    loanCurrency = value;
                    //Notify("LoanCurrency");
                }
              
            }
        }

        private bool loanHasIntersetPenalty;
        public bool LoanHasInterestPenalty
        {
            get
            {
                return loanHasIntersetPenalty;
            }

            set
            {
                if (value != loanHasIntersetPenalty)
                {
                    loanHasIntersetPenalty = value;
                    //Notify("LoanHasInterestPenalty");
                }
            }
        }


        private DateTime loanInterestPenaltyDate;
        public DateTime LoanInterestPenaltyDate
        {
            get
            {
                return loanInterestPenaltyDate;
            }
            set
            {
                if (value != loanInterestPenaltyDate)
                {
                    loanInterestPenaltyDate = value;
                    //Notify("LoanInterestPenaltyDate");
                }
            }
        }


        private decimal loanInterestPenaltyRate;
        public decimal LoanInterestPenaltyRate
        {
            get
            {
                return loanInterestPenaltyRate;
            }
            set
            {
                if (value != loanInterestPenaltyRate)
                {
                    loanInterestPenaltyRate = value;
                    //Notify("LoanInterestPenaltyRate");
                }
            }
        }

        private string loanInterestStructure;
        public string LoanInterestStructure
        {
            get
            {
                return loanInterestStructure;
            }
            set
            {
                if (value != loanInterestStructure)
                {
                    loanInterestStructure = value;
                    //Notify("LoanInterestStructure");
                }
            }
        }

        private ObservableCollection<Payment> loanpaymentsList;
        public ObservableCollection<Payment> LoanPaymentsList
        {
            get
            {
                return loanpaymentsList;
            }
            set
            {
                if (value != loanpaymentsList)
                {
                    loanpaymentsList = value;
                    //Notify("LoanPaymentsList");
                }
            }
        }



        //need to implement theses properties in LoanReportData:

        private bool loanPaid;
        public bool LoanPaid
        {
            get
            {

                return loanPaid;
            }

            set
            {
                if (value != loanPaid)
                {
                    loanPaid = value;
                    //Notify("LoanPaid");
                }
            }
        }

        private bool loanSavedToDb;



        public bool LoanSavedToDb
        {
            get
            {
                return loanSavedToDb;
            }
            set
            {
                if (value != loanPaid)
                {
                    loanSavedToDb = value;
                    //Notify("LoanSavedToDb");
                }
            }
        }

        //loan methods


        public Guid generateNewGuid()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid;
        }
    }
}