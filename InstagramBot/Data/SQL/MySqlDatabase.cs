using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace InstagramBot.Data.SQL
{
    public class MySqlDatabase : Database
    {
        public static SqlConnection MySqlConnection { get; set; }

        public MySqlDatabase(string connectionString)
        {
            if(MySqlConnection == null)
            MySqlConnection = new SqlConnection(connectionString);
        }
        public override bool GetLicenseState(long uid, out States state)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("State", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetLicenseState", args);
            if (args[1].Value == DBNull.Value)
            {
                state = States.Registering;
                return false;
            }
            state = (States)args[1].Value;
            return result;
        }
        public override bool GetCountFollows(long uid, out int count)
        {
            var args = new[]
           {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Count", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetCountFollows", args);
            if (args[1].Value == DBNull.Value)
            {
                count = 0;
                return false;
            }
            count = (int)args[1].Value;
            return result;
        }
        public override bool IsLicenseStart(long uid)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Status", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            CallFunction("IsLicenseStart", args);
            if (args[1].Value == DBNull.Value)
            {
                return false;
            }
            return (bool)args[1].Value;
        }
        public override bool InsertNewAccount(long pid, string referal, long fromReferal, States state)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = pid},
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Value = referal},
                new SqlParameter("FromReferal", SqlDbType.BigInt) {Value = fromReferal},
                new SqlParameter("State", SqlDbType.Int) {Value = (int)state}
            };
            var result = CallFunction("InsertNewAccount", args);
            return result;
        }

        public override bool IsPresentLicense(long uid)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("State", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentLicense", args);
            return args[1].Value != DBNull.Value && result;
        }

        public bool CallFunction(string functionName, params SqlParameter[] parameters)
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
            using (var command = new SqlCommand(functionName, MySqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery() != 0;
            }
        }
        public void Connect()
        {
            lock (MySqlConnection)
            {
                while (MySqlConnection.State == ConnectionState.Connecting)
                {
                    Thread.Sleep(10);
                }
                if (MySqlConnection.State == ConnectionState.Open)
                {
                    return;
                }
                MySqlConnection.Open();
            }
        }
    }
}
