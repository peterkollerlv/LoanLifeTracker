using System;
using System.Collections.Generic;

namespace InterestTracker
{
    public class Loan
    {
        //all loan specific data stored in this class


        //constructor for new loans

        public Loan(bool newLoan)
        {
            LoanGuid = generateNewGuid();
            LoanStartDate = DateTime.Now.Date;
            LoanInterestRate = 5;
            loanInterestPenaltyRate = 10;
            LoanCurrency = "USD";
            PaymentsList = new List<Payment>();
        }

        //constructor for existing loans

        public Loan(Guid existingGuid)
        {
            LoanGuid = existingGuid;
            PaymentsList = new List<Payment>();
        }

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
                loanGuid = value;
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
                loanTitle = value;
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
                loanCompanyInfo = value;
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
                loanLender = value;
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
                loanBeneficiary = value;
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
                loanCollectionAccount = value;
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
                loanStartDate = value.Date;
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
                loanInitialLoanAmount = value;
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
                loanCurrency = value;
            }
        }

        private bool loanHasPenalty;
        public bool LoanHasPenalty
        {
            get
            {
                return loanHasPenalty;
            }

            set
            {
                loanHasPenalty = value;
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
                loanInterestPenaltyDate = value;
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
                loanInterestRate = value;
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
                loanInterestPenaltyRate = value;
            }
        }

        private int loanInterestStructure;
        public int LoanInterestStructure
        {
            get
            {
                return loanInterestStructure;
            }
            set
            {
                loanInterestStructure = value;
            }
        }

        private List<Payment> paymentsList;
        public List<Payment> PaymentsList
        {
            get
            {
                return paymentsList;
            }
            set
            {
                paymentsList = value;
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
                loanPaid = value;
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
                loanSavedToDb = value;
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