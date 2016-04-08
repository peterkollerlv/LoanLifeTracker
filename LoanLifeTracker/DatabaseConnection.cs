using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using LoanLifeTracker.cyberHostBoxDataSetTableAdapters;


namespace LoanLifeTracker
{

      class DatabaseConnection
    {
         private DataTable existingLoans;
         private cyberHostBoxDataSet.LoanLifeTrackerDataTable loanLifeTrackerDbTable;
         private LoanLifeTrackerTableAdapter dbAdapter;
         private DataSet loanDataSet;

        public DatabaseConnection()
        {
            loanLifeTrackerDbTable = new cyberHostBoxDataSet.LoanLifeTrackerDataTable();
            dbAdapter = new LoanLifeTrackerTableAdapter();

            existingLoans = new DataTable();
            loanDataSet = new DataSet();
            dbAdapter.Connection.Open();
            loanDataSet.Tables.Add(dbAdapter.GetData());
            getExistingLoans();
        }
         public DataTable ExistingLoans
        {
            get
            {
                return existingLoans;
            }
            private set
            {
                existingLoans = value;
            }
        }
         public string AddLoan(Loan activeLoan)
        {

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
                loanDataSet.AcceptChanges();
                return "Loan \"" + activeLoan.LoanTitle + "\" has been saved.";
            }
            catch (Exception ex)
            {
                return "Failed due to: " + ex.Message;
            }
        }
         public void getExistingLoans()
        {
            dbAdapter.GetData();
         
            // foreach (DataRow dataRow in loanLifeTrackerDbTable.Rows)
            DataView dataView = new DataView(loanDataSet.Tables[0]);

            existingLoans.Columns.Add("loanTitle", typeof(string));
            existingLoans.Columns.Add("loanStartDate", typeof(DateTime));
            existingLoans.Columns.Add("loanGuid", typeof(string));

            foreach (DataRow dataRow in dataView.Table.Rows)
            {
                DataRow addRow;
 

                addRow = existingLoans.NewRow();
                addRow[0] = dataRow.Field<string>("loanTitle");
                addRow[1] = dataRow.Field<DateTime>("loanStartDate");
                addRow[2] = dataRow.Field<string>("loanGuid");
                existingLoans.Rows.Add(addRow);
             
            }
        }

    }
}
