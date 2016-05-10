using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Collections.ObjectModel;
using System.Windows;

namespace InterestTracker
{

    internal class DatabaseConnection
    {
        // private List<Loan> existingLoans;
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
        internal bool isConncectedToDb;
        private LoanReportData loanReportDataObj;
        SshClient sshClient;
        ForwardedPortLocal sshTunnel;
        // public string ConnectionAlive;

        public DatabaseConnection(LoanReportData loanReportDataObj)
        {
            this.loanReportDataObj = loanReportDataObj;
            dBConnInfo = new DBConnectInfo();
            loanDataSet = new DataSet();
            openLoansTable = new DataTable();
            openLoanPayments = new DataTable();
            OpenTunnel();

        }


        public void OpenTunnel()
        {

        }

        // MGY 2016.05.05
        // Description: saves loan data
        public void saveLoanLifeTracker(Loan activeLoan)
        {
            //Guid loanGuid ac 
            //    string LoanTitle,
            //    string LoanBeneficiary,
            //    string LoanCollectionAccount,

            //                            string LoanCompanyInfo,
            //    string LoanCurrency,
            //    bool loanHasPenalty,
            //    decimal LoanInitialLoanAmount,

            //                            DateTime LoanPenaltyDate, 
            //decimal LoanPenaltyRate,
            //    decimal LoanInterestRate,
            //    string LoanInterestStructure,
            //                            string LoanLender,
            //    bool LoanPaid,
            //    DateTime LoanStartDate
            try
            {
                if (sshClient == null || !sshClient.IsConnected || !sshTunnel.IsStarted)
                {

                    sshClient = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword);
                    sshClient.Connect();
                    sshTunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    sshClient.AddForwardedPort(sshTunnel);
                    sshTunnel.Start();
                }

                dbConn = new MySqlConnection(dBConnInfo.MySqlConnectionString.ToString());
                dbConn.Open();

                bool recordExists = false;
                string sql = string.Format("select count(*) from frw_loanlifetracker.LoanLifeTracker where LoanGuid = '{0}'", activeLoan.LoanGuid);
                MySqlCommand cmdCheckGuid = new MySqlCommand(sql, dbConn);
                cmdCheckGuid.Prepare();
                object result = cmdCheckGuid.ExecuteScalar();
                if (result != null)
                {
                    recordExists = Convert.ToInt16(result) > 0;
                }

                if (recordExists)
                {
                    string sqlUpd = @"UPDATE frw_loanlifetracker.LoanLifeTracker 
                                    set LoanTitle = @LoanTitle, 
                                        LoanBeneficiary = @LoanBeneficiary, 
                                        LoanCollectionAccount = @LoanCollectionAccount, 
                                        LoanCompanyInfo = @LoanCompanyInfo, 
                                        LoanCurrency = @LoanCurrency, 
                                        LoanHasPenalty = @LoanHasPenalty, 
                                        LoanInitialLoanAmount = @LoanInitialLoanAmount, 
                                        LoanPenaltyDate = @LoanPenaltyDate, 
                                        LoanPenaltyRate = @LoanPenaltyRate, 
                                        LoanInterestRate = @LoanInterestRate, 
                                        LoanInterestStructure = @LoanInterestStructure, 
                                        LoanLender = @LoanLender, 
                                        LoanPaid = @LoanPaid, 
                                        LoanStartDate = @LoanStartDate 
                                        where LoanGuid = @LoanGuid";

                    MySqlCommand cmdUpdateLoanToDataBase = new MySqlCommand(sqlUpd, dbConn);
                    cmdUpdateLoanToDataBase.Prepare();

                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanGuid", activeLoan.LoanGuid);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanTitle", activeLoan.LoanTitle);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanBeneficiary", activeLoan.LoanBeneficiary);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanCollectionAccount", activeLoan.LoanCollectionAccount);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanCompanyInfo", activeLoan.LoanCompanyInfo);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanCurrency", activeLoan.LoanCurrency);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@loanHasPenalty", activeLoan.LoanHasInterestPenalty);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanInitialLoanAmount", activeLoan.LoanInitialLoanAmount);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanPenaltyDate", activeLoan.LoanInterestPenaltyDate);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanPenaltyRate", activeLoan.LoanInterestPenaltyRate);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanInterestRate", activeLoan.LoanInterestRate);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanInterestStructure", activeLoan.LoanInterestStructure);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanLender", activeLoan.LoanLender);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanPaid", activeLoan.LoanPaid);
                    cmdUpdateLoanToDataBase.Parameters.AddWithValue("@LoanStartDate", activeLoan.LoanStartDate);

                    MySqlDataReader reader;
                    reader = cmdUpdateLoanToDataBase.ExecuteReader(); //execute the mysql command

                    if (reader.RecordsAffected == 1)
                        MessageBox.Show("Database has been updated!");

                    dbConn.Close();
                    return;
                }

                string sqlStmt = @"INSERT INTO frw_loanlifetracker.LoanLifeTracker(LoanGuid, LoanTitle, LoanBeneficiary, LoanCollectionAccount, 
                                        LoanCompanyInfo, LoanCurrency, loanHasPenalty, LoanInitialLoanAmount, LoanPenaltyDate, LoanPenaltyRate, 
                                        LoanInterestRate, LoanInterestStructure, LoanLender, LoanPaid, LoanStartDate)
                                    VALUES (@LoanGuid, @LoanTitle, @LoanBeneficiary, @LoanCollectionAccount, @LoanCompanyInfo,
                                        @LoanCurrency, @LoanHasPenalty, @LoanInitialLoanAmount, @LoanPenaltyDate, @LoanPenaltyRate, 
                                        @LoanInterestRate, @LoanInterestStructure, @LoanLender, @LoanPaid, @LoanStartDate)";

                MySqlCommand cmdInsertLoanToDataBase = new MySqlCommand(sqlStmt, dbConn);
                cmdInsertLoanToDataBase.Prepare();

                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanGuid", activeLoan.LoanGuid);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanTitle", activeLoan.LoanTitle);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanBeneficiary", activeLoan.LoanBeneficiary);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanCollectionAccount", activeLoan.LoanCollectionAccount);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanCompanyInfo", activeLoan.LoanCompanyInfo);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanCurrency", activeLoan.LoanCurrency);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@loanHasPenalty", activeLoan.LoanHasInterestPenalty);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanInitialLoanAmount", activeLoan.LoanInitialLoanAmount);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanPenaltyDate", activeLoan.LoanInterestPenaltyDate);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanPenaltyRate", activeLoan.LoanInterestPenaltyRate);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanInterestRate", activeLoan.LoanInterestRate);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanInterestStructure", activeLoan.LoanInterestStructure);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanLender", activeLoan.LoanLender);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanPaid", activeLoan.LoanPaid);
                cmdInsertLoanToDataBase.Parameters.AddWithValue("@LoanStartDate", activeLoan.LoanStartDate);

                cmdInsertLoanToDataBase.ExecuteNonQuery(); //execute the mysql command

                dbConn.Close();

            }
            catch (Exception ex)
            {
                DbConnectionError = ex.Message;
                isConncectedToDb = false;
                MessageBox.Show(ex.Message);
            }
        }

        // MGY 2016.05.05
        // Description: saves loan data
        public void saveLoanLifeTrackerPayment(Payment payment)
        {
            try
            {
                if (sshClient == null || !sshClient.IsConnected || !sshTunnel.IsStarted)
                {

                    sshClient = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword);
                    sshClient.Connect();
                    sshTunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    sshClient.AddForwardedPort(sshTunnel);
                    sshTunnel.Start();
                }

                dbConn = new MySqlConnection(dBConnInfo.MySqlConnectionString.ToString());
                dbConn.Open();

                bool recordExists = false;
                string sql = string.Format("select count(*) from frw_loanlifetracker.LoanLifeTrackerPayments where PaymentGuid = '{0}'", payment.PaymentGuid);
                MySqlCommand cmdCheckGuid = new MySqlCommand(sql, dbConn);
                cmdCheckGuid.Prepare();
                object result = cmdCheckGuid.ExecuteScalar();
                if (result != null)
                {
                    recordExists = Convert.ToInt16(result) > 0;
                }

                if (recordExists)
                {
                    string sqlUpd = @"UPDATE frw_loanlifetracker.LoanLifeTrackerPayments 
                                    set loanGuid = @LoanGuid, 
                                        paymentDate = @PaymentDate, 
                                        paymentInterestAmount = @PaymentInterestAmount, 
                                        paymentPrincipalAmount = @PaymentPrincipalAmount, 
                                        paymentTotalAmount = @PaymentTotalAmount
                                        where paymentGuid = @PaymentGuid";

                    MySqlCommand cmdUpd = new MySqlCommand(sqlUpd, dbConn);
                    cmdUpd.Prepare();

                    cmdUpd.Parameters.AddWithValue("@LoanGuid", payment.LoanGuid);
                    cmdUpd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmdUpd.Parameters.AddWithValue("@PaymentInterestAmount", payment.InterestPaymentAmount);
                    cmdUpd.Parameters.AddWithValue("@PaymentPrincipalAmount", payment.PrincipalPaymentAmount);
                    cmdUpd.Parameters.AddWithValue("@PaymentTotalAmount", payment.TotalPaymentAmount);
                    cmdUpd.Parameters.AddWithValue("@PaymentGuid", payment.PaymentGuid);

                    MySqlDataReader reader;
                    reader = cmdUpd.ExecuteReader(); //execute the mysql command

                    if (reader.RecordsAffected == 1)
                        MessageBox.Show("Database has been updated!");

                    dbConn.Close();
                    return;
                }

                string sqlStmt = @"INSERT INTO frw_loanlifetracker.LoanLifeTrackerPayments(paymentGuid, loanGuid, paymentDate, paymentInterestAmount, 
                                        paymentPrincipalAmount, paymentTotalAmount)
                                    VALUES (@PaymentGuid, @LoanGuid, @PaymentDate, @PaymentInterestAmount, @PaymentPrincipalAmount, @PaymentTotalAmount)";

                MySqlCommand cmd = new MySqlCommand(sqlStmt, dbConn);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@PaymentGuid", payment.PaymentGuid);
                cmd.Parameters.AddWithValue("@LoanGuid", payment.LoanGuid);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@PaymentInterestAmount", payment.InterestPaymentAmount);
                cmd.Parameters.AddWithValue("@PaymentPrincipalAmount", payment.PrincipalPaymentAmount);
                cmd.Parameters.AddWithValue("@PaymentTotalAmount", payment.TotalPaymentAmount);

                cmd.ExecuteNonQuery(); //execute the mysql command
                dbConn.Close();
            }
            catch (Exception ex)
            {
                DbConnectionError = ex.Message;
                isConncectedToDb = false;
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenConnection()
        {
            try
            {
                //using (sshClient = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword))
                //{
                //    sshClient.Connect();
                //    sshTunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                //    sshClient.AddForwardedPort(sshTunnel);
                //    sshTunnel.Start();

                //}

                if (sshClient == null || !sshClient.IsConnected || !sshTunnel.IsStarted)
                {

                    sshClient = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword);
                    sshClient.Connect();
                    sshTunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    sshClient.AddForwardedPort(sshTunnel);
                    sshTunnel.Start();
                }


                //using (SshClient client = new SshClient(dBConnInfo.SshHost, dBConnInfo.SshUserName, dBConnInfo.SshPassword))
                //{


                dbConn = new MySqlConnection(dBConnInfo.MySqlConnectionString.ToString());

                dbConn.Open();

                if (dbConn.State == ConnectionState.Open)
                {
                    openLoansTable.Rows.Clear();
                    openLoanPayments.Rows.Clear();
                    string selectTable = "SELECT * FROM LoanLifeTracker";
                    dataAdapter = new MySqlDataAdapter(selectTable, dbConn);
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    dataAdapter.Fill(openLoansTable);


                    selectTable = "SELECT * FROM LoanLifeTrackerPayments";
                    dataAdapter = new MySqlDataAdapter(selectTable, dbConn);
                    dataAdapter.SelectCommand.CommandType = CommandType.Text;
                    //  ConnectionAlive  = (string)dataAdapter.InitializeLifetimeService();
                    dataAdapter.Fill(openLoanPayments);


                    openLoansTable.TableName = "LoanLifeTracker";
                    openLoanPayments.TableName = "LoanLifeTrackerPayments";
                    if (LoanDataSet.Tables["LoanLifeTracker"] == null && LoanDataSet.Tables["LoanLifeTrackerPayments"] == null)
                    {
                        LoanDataSet.Tables.Add(openLoansTable);
                        LoanDataSet.Tables.Add(openLoanPayments);
                        openLoansTable.PrimaryKey = new DataColumn[] { openLoansTable.Columns["loanGuid"] };
                        openLoanPayments.PrimaryKey = new DataColumn[] { openLoanPayments.Columns["paymentGuid"] };
                    }
                    //GetExistingLoans();
                    isConncectedToDb = true;
                    // sshTunnel.Dispose();
                }
                else
                {
                    isConncectedToDb = false;

                }
                //}
            }
            catch (Exception ex)
            {
                DbConnectionError = ex.Message;
                isConncectedToDb = false;
            }
        }

        public void CloseConnection()
        {
            dbConn.Close();
        }



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
        //public List<Loan> ExistingLoans
        //{
        //    get
        //    {
        //        return existingLoans;
        //    }
        //    private set
        //    {
        //        existingLoans = value;
        //    }
        //}



        public string UpdateLoansToDb()
        {

            try
            {
                if (dbConn.State == ConnectionState.Open)
                {
                    // openLoansTable.Rows.Clear();
                    foreach (Loan loan in loanReportDataObj.ExistingLoans)
                    {
                        DataRow fillRow;
                        //  DataRow fillRow = openLoansTable.NewRow();
                        if (openLoansTable.Rows.Find(loan.LoanGuid) != null)
                        {
                            fillRow = openLoansTable.Rows.Find(loan.LoanGuid);
                            fillRow["loanGuid"] = loan.LoanGuid;
                            fillRow["loanTitle"] = loan.LoanTitle;
                            fillRow["loanBeneficiary"] = loan.LoanBeneficiary;
                            fillRow["loanCollectionAccount"] = loan.LoanCollectionAccount;
                            fillRow["loanCompanyInfo"] = loan.LoanCompanyInfo;
                            fillRow["loanCurrency"] = loan.LoanCurrency;
                            fillRow["loanHasPenalty"] = loan.LoanHasInterestPenalty;
                            fillRow["loanInitialLoanAmount"] = loan.LoanInitialLoanAmount;
                            fillRow["loanPenaltyDate"] = loan.LoanInterestPenaltyDate;
                            fillRow["loanPenaltyRate"] = loan.LoanInterestPenaltyRate;
                            fillRow["loanInterestRate"] = loan.LoanInterestRate;
                            fillRow["loanInterestStructure"] = loan.LoanInterestStructure;
                            fillRow["loanLender"] = loan.LoanLender;
                            fillRow["loanPaid"] = loan.LoanPaid;
                            fillRow["loanSavedToDB"] = loan.LoanSavedToDb;
                            fillRow["loanStartDate"] = loan.LoanStartDate;

                        }
                        else
                        {
                            fillRow = openLoansTable.NewRow();
                            fillRow["loanGuid"] = loan.LoanGuid;
                            fillRow["loanTitle"] = loan.LoanTitle;
                            fillRow["loanBeneficiary"] = loan.LoanBeneficiary;
                            fillRow["loanCollectionAccount"] = loan.LoanCollectionAccount;
                            fillRow["loanCompanyInfo"] = loan.LoanCompanyInfo;
                            fillRow["loanCurrency"] = loan.LoanCurrency;
                            fillRow["loanHasPenalty"] = loan.LoanHasInterestPenalty;
                            fillRow["loanInitialLoanAmount"] = loan.LoanInitialLoanAmount;
                            fillRow["loanPenaltyDate"] = loan.LoanInterestPenaltyDate;
                            fillRow["loanPenaltyRate"] = loan.LoanInterestPenaltyRate;
                            fillRow["loanInterestRate"] = loan.LoanInterestRate;
                            fillRow["loanInterestStructure"] = loan.LoanInterestStructure;
                            fillRow["loanLender"] = loan.LoanLender;
                            fillRow["loanPaid"] = loan.LoanPaid;
                            fillRow["loanSavedToDB"] = loan.LoanSavedToDb;
                            fillRow["loanStartDate"] = loan.LoanStartDate;
                            openLoansTable.Rows.Add(fillRow);
                        }

                    }
                    string selectTable = "SELECT * FROM LoanLifeTracker";
                    dataAdapter = new MySqlDataAdapter(selectTable, dbConn);
                    dataAdapter.UpdateCommand = new MySqlCommandBuilder(dataAdapter).GetDeleteCommand();
                    dataAdapter.Update(openLoansTable);
                    byte loanPenaltyConvert;
                    byte loanPaidConvert;
                    byte loanSavedToDB = 1;
                    if (loanReportDataObj.HasInterestPenalty)
                    {
                        loanPenaltyConvert = 1;
                    }
                    else
                    {
                        loanPenaltyConvert = 0;
                    }
                    if (loanReportDataObj.Paid)
                    {
                        loanPaidConvert = 1;
                    }
                    else
                    {
                        loanPaidConvert = 0;
                    }



                    
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
                    // LoanDataSet.AcceptChanges();
                    return "Loans has been saved.";
                }
                else
                {
                    return "Error saving loan";
                }
            }
            catch (Exception ex)
            {
                return "Failed due to: " + ex.Message;
            }
        }
        public void GetExistingLoans()
        {

            if (loanReportDataObj.ExistingLoans != null)
            {
                loanReportDataObj.ExistingLoans.Clear();
                // loanReportDataObj.ExistingLoans = new ObservableCollection<Loan>();
                foreach (DataRow loans in openLoansTable.Rows)
                {
                    Guid guid = new Guid(loans["loanGuid"].ToString());
                    Loan newLoan = new Loan(guid);
                    newLoan.LoanTitle = loans["loanTitle"].ToString();
                    newLoan.LoanLender = loans["loanLender"].ToString();
                    newLoan.LoanCompanyInfo = loans["loanCompanyInfo"].ToString();
                    newLoan.LoanBeneficiary = loans["loanBeneficiary"].ToString();
                    newLoan.LoanInterestStructure = loans["loanInterestStructure"].ToString();
                    newLoan.LoanCollectionAccount = loans["loanCollectionAccount"].ToString();
                    newLoan.LoanStartDate = (DateTime)loans["loanStartDate"];
                    //newLoan.LoanStartDate = DateTime.Parse(loans["loanStartDate"].ToString());
                    newLoan.LoanCurrency = loans["loanCurrency"].ToString();
                    newLoan.LoanInitialLoanAmount = Decimal.Parse(loans["loanInitialLoanAmount"].ToString());
                    newLoan.LoanInterestRate = Decimal.Parse(loans["loanInterestRate"].ToString());
                    switch (loans["loanHasPenalty"].ToString())
                    {
                        case "0":
                            newLoan.LoanHasInterestPenalty = false;
                            break;
                        case "1":
                            newLoan.LoanHasInterestPenalty = true;
                            break;
                    }
                    newLoan.LoanInterestPenaltyDate = (DateTime)loans["loanPenaltyDate"];
                    newLoan.LoanInterestPenaltyRate = Decimal.Parse(loans["loanPenaltyRate"].ToString());
                    switch (loans["loanPaid"].ToString())
                    {
                        case "0":
                            newLoan.LoanPaid = false;
                            break;
                        case "1":
                            newLoan.LoanPaid = true;
                            break;
                    }

                    foreach (DataRow paymentRow in openLoanPayments.Rows)
                    {
                        if (paymentRow["loanGuid"].ToString().Contains(newLoan.LoanGuid.ToString()))
                        {
                            Payment newPayment = new Payment(newLoan.LoanGuid);
                            newPayment.PaymentGuid = Guid.Parse(paymentRow["paymentGuid"].ToString());
                            newPayment.PaymentDate = (DateTime)paymentRow["paymentDate"];
                            newPayment.TotalPaymentAmount = Decimal.Parse(paymentRow["paymentTotalAmount"].ToString());
                            newPayment.InterestPaymentAmount = Decimal.Parse(paymentRow["paymentInterestAmount"].ToString());
                            newPayment.PrincipalPaymentAmount = Decimal.Parse(paymentRow["paymentPrincipalAmount"].ToString());

                            newLoan.LoanPaymentsList.Add(newPayment);
                            newPayment = null;
                        }
                    }

                    loanReportDataObj.ExistingLoans.Add(newLoan);
                }
            }

        }
    }

}
