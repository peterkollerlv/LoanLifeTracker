using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using Renci.SshNet;



namespace InterestTracker
{

    class DatabaseConnection
    {
        private List<Loan> existingLoans;
        //   private cyberHostBoxDataSet.LoanLifeTrackerDataTable loanLifeTrackerDbTable;
        // private LoanLifeTrackerTableAdapter dbAdapter;
        MySqlCommand dbCommand;
        MySqlDataAdapter dataAdapter;

        private DataSet loanDataSet;
        private DataTable openLoansTable;
        private DataTable openLoanPayments;
        internal DBConnectInfo dBConnInfo;
        internal MySqlConnection dbConn;
        public string DbConnectionError;

        public DatabaseConnection()
        {
            dBConnInfo = new DBConnectInfo();
            loanDataSet = new DataSet();
            openLoansTable = new DataTable();
            openLoanPayments = new DataTable();
        }

        public bool OpenConnection()
        {
            try
            {

                //using (var client = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword))
                //{
                //    client.Connect();
                //    var tunnel = new ForwardedPortLocal("127.0.0.1", 8001, "127.0.0.1", 3306);
                //    client.AddForwardedPort(tunnel);
                //    tunnel.Start();                    
                //}

                dbConn = new MySqlConnection(dBConnInfo.MySqlConnectionString);
                var dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "SHOW TABLES;";
                MySqlDataReader dbReader;
                dbConn.Open();
                string connState = dbConn.State.ToString();
                if (dbConn.State == ConnectionState.Open)
                {

                    dbReader = dbCommand.ExecuteReader();
                    TableNames = new List<string>();
                    while (dbReader.Read())
                    {
                        
                      //  for (int i = 0; i < dbReader.FieldCount; i++)
                       // {
                            TableNames.Add(dbReader.GetValue(0).ToString());
                       // }
                    }
                    string[] tableNamesArray = TableNames.ToArray<string>();
                    dbReader.Close();
                    string selectTable = "SELECT * FROM LoanLifeTracker";
                    dataAdapter = new MySqlDataAdapter(selectTable, dbConn);
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.Fill(openLoansTable);

                    selectTable = "SELECT * FROM LoanLifeTrackerPayments";
                    dataAdapter = new MySqlDataAdapter(selectTable, dbConn);
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.Fill(openLoanPayments);
                    openLoansTable.TableName = "LoanLifeTracker";
                    openLoanPayments.TableName = "LoanLifeTrackerPayments";
                    if (LoanDataSet.Tables["LoanLifeTracker"] == null && LoanDataSet.Tables["LoanLifeTrackerPayments"] == null)
                    { 
                    LoanDataSet.Tables.Add(openLoansTable);
                    LoanDataSet.Tables.Add(openLoanPayments);
                    }
                    else
                    {

                    }

                    //loanDataSet.Load(dbReader, LoadOption.OverwriteChanges, tableNamesArray);
                    //loanDataSet.Tables.Add(openLoansTable);
                    //loanDataSet.Tables.Add(openLoanPayments);
                    //loanDataSet.Tables[0].
                    return true;
                }
                else
                {
                    return false;

                }


            }
            catch (Exception ex)
            {
                DbConnectionError = ex.Message;
                return false;
            }



        }

        public void CloseConnection()
        {
            dbConn.Close();
        }
        public List<string> TableNames;


        public DataSet LoanDataSet
        {
            get
            {
                return loanDataSet;
            }

            set
            {
                loanDataSet = value;
            }
        }
        public List<Loan> ExistingLoans
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
                if (activeLoan.LoanHasInterestPenalty)
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
                //dbAdapter.Insert(
                // activeLoan.LoanGuid.ToString(),
                // activeLoan.LoanTitle,
                // activeLoan.LoanBeneficiary,
                // activeLoan.LoanCollectionAccount,
                // activeLoan.LoanCompanyInfo,
                // activeLoan.LoanCurrency,
                // loanPenaltyConvert,
                // activeLoan.LoanInitialLoanAmount,
                // activeLoan.LoanInterestPenaltyDate,
                // activeLoan.LoanInterestPenaltyRate,
                // activeLoan.LoanInterestRate,
                // activeLoan.LoanInterestStructure,
                // activeLoan.LoanLender,
                // loanPaidConvert,
                // loanSavedToDB,
                // activeLoan.LoanStartDate
                // );
                //  dbAdapter.Update(loanLifeTrackerDbTable);
                LoanDataSet.AcceptChanges();
                return "Loan \"" + activeLoan.LoanTitle + "\" has been saved.";
            }
            catch (Exception ex)
            {
                return "Failed due to: " + ex.Message;
            }
        }
        public void getExistingLoans()
        {


            //   dbAdapter.GetData();

            // foreach (DataRow dataRow in loanLifeTrackerDbTable.Rows)
            DataView dataView = new DataView(LoanDataSet.Tables[0]);

            //  existingLoans.Columns.Add("loanTitle", typeof(string));
            //  existingLoans.Columns.Add("loanStartDate", typeof(DateTime));
            //   existingLoans.Columns.Add("loanGuid", typeof(string));

            foreach (DataRow dataRow in dataView.Table.Rows)
            {
                DataRow addRow;


                //       addRow = existingLoans.NewRow();
                //        addRow[0] = dataRow.Field<string>("loanTitle");
                //        addRow[1] = dataRow.Field<DateTime>("loanStartDate");
                //         addRow[2] = dataRow.Field<string>("loanGuid");
                //        existingLoans.Rows.Add(addRow);

            }
        }

    }
}
