using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LoanLifeTracker
{

    static class DatabaseConnection
    {
        static public string AddLoan(Loan activeLoan)
        {
            cyberHostBoxDataSet.LoanLifeTrackerDataTable loanLifeTrackerDbTable = new cyberHostBoxDataSet.LoanLifeTrackerDataTable();
            cyberHostBoxDataSetTableAdapters.LoanLifeTrackerTableAdapter dbAdapter = new cyberHostBoxDataSetTableAdapters.LoanLifeTrackerTableAdapter();
            try
            {
                byte loanPenaltyConvert;
                byte loanPaidConvert;
                byte loanSavedToDB = 1;
                if (activeLoan.LoanHasPenalty)
                {
                    loanPenaltyConvert = 1;
                }
                else
                {
                    loanPenaltyConvert = 0;
                }
                if (activeLoan.LoanPaid)
                {
                    loanPaidConvert = 1;
                }
                else
                {
                    loanPaidConvert = 0;
                }
                dbAdapter.Insert(
                activeLoan.LoanGuid.ToString(),
                activeLoan.LoanTitle,
                activeLoan.LoanBeneficiary,
                activeLoan.LoanCollectionAccount,
                activeLoan.LoanCompanyInfo,
                activeLoan.LoanCurrency,
                loanPenaltyConvert,
                activeLoan.LoanInitialLoanAmount,
                activeLoan.LoanInterestPenaltyDate,
                activeLoan.LoanInterestPenaltyRate,
                activeLoan.LoanInterestRate,
                activeLoan.LoanInterestStructure,
                activeLoan.LoanLender,
                loanPaidConvert,
                loanSavedToDB,
                activeLoan.LoanStartDate
                );
                dbAdapter.Update(loanLifeTrackerDbTable);
                return "Loan \"" + activeLoan.LoanTitle + "\" has been saved.";
            }
            catch (Exception ex)
            {
                return "Failed due to: " + ex.Message;
            }
        }
        static public DataTable getExistingLoans()
        {
            DataTable existingLoans = new DataTable();
            DataRow addRow;
            existingLoans.Columns.Add("loanTitle", typeof(string));
            existingLoans.Columns.Add("loanStartDate", typeof(DateTime));
            existingLoans.Columns.Add("loanGuid", typeof(string));
            addRow = existingLoans.NewRow();
            addRow[0] = "test";
            addRow[1] = DateTime.Now.Date;
            addRow[2] = "testguid";
            existingLoans.Rows.Add(addRow);
            return existingLoans;
        }

    }
}
