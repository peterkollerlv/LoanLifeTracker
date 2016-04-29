using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestTracker
{
    public class LoanDetailsPages
    {
         private LoanCalculation loanCalculation;
         private LoanPayments loanPayments;
         private LoanDrawDown loanDrawDown;

       public LoanDetailsPages(LoanReportData loanReportDataObj)
        {
            CreatePages(loanReportDataObj);
        }

        public void CreatePages(LoanReportData loanReportDataObj)
        {

            loanCalculation = new LoanCalculation(loanReportDataObj);
            loanPayments = new LoanPayments(loanReportDataObj);
            loanDrawDown = new LoanDrawDown();
        }




        public  LoanCalculation LoanCalculation
        {
            get
            {
                return loanCalculation;
            }

            set
            {

                loanCalculation = value;
            }
        }

        public LoanPayments LoanPayments
        {
            get
            {
                return loanPayments;
            }

            set
            {
                loanPayments = value;
            }
        }

        public  LoanDrawDown LoanDrawDown
        {
            get
            {
                return loanDrawDown;
            }

            set
            {
                loanDrawDown = value;
            }
        }
    }
}
