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
            if (MySqlConnection == null)
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
                state = States.SelectLanguage;
                return false;
            }
            state = (States) args[1].Value;
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
            count = (int) args[1].Value;
            return result;
        }

        public override bool GetRedList(long uid, out List<string> list)
        {
            var args = new[]
           {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Red1", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output},
                 new SqlParameter("Red2", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output},
                  new SqlParameter("Red3", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetRedList", args);
            list = new List<string>();
            if (args[1].Value == DBNull.Value)
            {
                return false;
            }
            list.Add((string)args[1].Value);
            list.Add((string)args[2].Value);
            list.Add((string)args[3].Value);
            return result;
        }

        public override bool GetReferal(long uid, out string referal)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetReferal", args);
            if (args[1].Value == DBNull.Value)
            {
                referal = "";
                return false;
            }
            referal = (string) args[1].Value;
            return result;
        }

        public override bool GetReferalByTelegramId(long tid, out string referal)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = tid},
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetReferalByTelegramId", args);
            if (args[1].Value == DBNull.Value)
            {
                referal = "";
                return false;
            }
            referal = (string)args[1].Value;
            return result;
        }
        public override bool GetTelegramId(long uid, out long telegramId)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetTelegramID", args);
            if (args[1].Value == DBNull.Value)
            {
                telegramId = 0;
                return false;
            }
            telegramId = (long)args[1].Value;
            return result;
        }
        public override bool GetFromReferalId(long uid, out long referalId)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("ReferalId", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetFromReferalId", args);
            if (args[1].Value == DBNull.Value)
            {
                referalId = 0;
                return false;
            }
            referalId = (long) args[1].Value;
            return result;
        }

        public override List<string> GetPriorityList(int priority)
        {
            lock (MySqlConnection)
            {
                List<string> list = new List<string>();
                using (
                    var command =
                        new SqlCommand(
                            "select PriorityLevels.Referal from PriorityLevels where PriorityLevels.Priority = " +
                            priority, MySqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader["Referal"].ToString());
                    }
                    reader.Close();
                }
                return list;
            }
        }
       
        public override bool GetNeedReferalForFollow(long uid, out long referalId, out string referal)
        {
            if (IsLicenseStart(uid))
                referalId = uid;
            else
                GetFromReferalId(uid, out referalId);
            GetReferal(referalId, out referal);
            return true;
        }
        public override bool InsertNewAccount(long pid, string referal,long telegramiD, long fromReferal, States state)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = pid},
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Value = referal},
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Value = telegramiD},
                new SqlParameter("FromReferal", SqlDbType.BigInt) {Value = fromReferal},
                new SqlParameter("State", SqlDbType.Int) {Value = (int) state}
            };
            var result = CallFunction("InsertNewAccount", args);
            return result;
        }
        public override bool InsertRedList(long pid, string red1, string red2, string red3)
        {
            var args = new[]
          {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = pid},
                new SqlParameter("Red1", SqlDbType.VarChar, 50) {Value = red1},
                new SqlParameter("Red2", SqlDbType.VarChar, 50) {Value = red2},
                new SqlParameter("Red3", SqlDbType.VarChar, 50) {Value = red3}
            };
            var result = CallFunction("InsertRedList", args);
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

        public override bool IsPresentReferal(string referal)
        {
            var args = new[]
            {
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Value = referal},
                new SqlParameter("State", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentReferal", args);
            return args[1].Value != DBNull.Value && result;
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

        public override bool UpdateStatus(long uid, bool status)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Status", SqlDbType.Bit) {Value = status}
            };
            return CallFunction("UpdateStatus", args);
        }

        public override bool UpdateCountFollows(long uid, int count)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Count", SqlDbType.Int) {Value = count}
            };
            return CallFunction("UpdateCountFollows", args);
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
