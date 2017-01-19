using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

namespace InstagramBot.Data.SQL
{
    public class MySqlDatabase : Database
    {
        public SqlConnection MySqlConnection { get; set; }

        SqlDataReader reader;

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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                count = 0;
                return false;
            }
            count = (int) args[1].Value;
            return result;
        }

        public override bool GetRedList(long uid, out List<string> list)
        {
            List<SqlParameter> args = new List<SqlParameter>();
            args.Add(new SqlParameter("ID", SqlDbType.BigInt) { Value = uid });
            for (int i = 0; i < 9; i++)
            {
                args.Add(new SqlParameter("Red" + (i + 1), SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output });
            }
            var result = CallFunction("GetRedList", args.ToArray());

            list = new List<string>();
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                return false;
            }
            for (int i = 1; i <= 9; i++)
                if(args[i].Value != DBNull.Value && args[i].Value != null)
                list.Add((string)args[i].Value);
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                referal = "";
                return false;
            }
            referal = (string) args[1].Value;
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                telegramId = 0;
                return false;
            }
            telegramId = (long) args[1].Value;
            return result;
        }

        public bool GetLanguage(long uid, out Language lang)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Language", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetLanguage", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                lang = 0;
                return false;
            }
            lang = (Language)args[1].Value;
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                referalId = 0;
                return false;
            }
            referalId = (long) args[1].Value;
            return result;
        }

        Random random = new Random();

        public override List<string> GetPriorityList(int priority)
        {
            List<string> list = new List<string>();
            using (
                var command =
                    new SqlCommand(
                        "select PriorityLevels.Referal from PriorityLevels where PriorityLevels.Priority = " +
                        priority, MySqlConnection))
            {
                command.CommandType = CommandType.Text;
                lock (start)
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string referal = reader["Referal"].ToString();
                        int j = random.Next(list.Count + 1);
                        if (j == list.Count)
                        {
                            list.Add(referal);
                        }
                        else
                        {
                            list.Add(list[j]);
                            list[j] = referal;
                        }
                    }
                    reader.Close();
                }
            }
            return list;
        }

        public override List<StructInfo> GetStructInfo(long fromId)
        {
            lock (start)
            {
                List<StructInfo> list = new List<StructInfo>();
                using (
                    var command =
                        new SqlCommand(
                            "select Accounts.Referal, Accounts.State, Accounts.Status, Accounts.CountFollows from Accounts where Accounts.FromReferal = " +
                            fromId, MySqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new StructInfo
                        {
                            Referal = reader["Referal"].ToString(),
                            States = (States) reader["State"],
                            Status = (bool) reader["Status"],
                            CountFollows = (int)reader["CountFollows"]
                        });
                    }
                    reader.Close();
                }
            return list;
        }
    }

        public List<long> GetTelegrams()
        {
            lock (start)
            {
                List<long> list = new List<long>();
                using (
                    var command =
                        new SqlCommand(
                            "select Accounts.TelegramID from Accounts", MySqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add((long) reader["TelegramID"]);
                    }
                    reader.Close();
                }
                return list;
            }
        }

        public List<long> GetBlockList()
        {
            lock (start)
            {
                List<long> list = new List<long>();
                using (
                    var command =
                        new SqlCommand(
                            "select Accounts.ID from Accounts where State = "+ (int)States.Blocked, MySqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add((long)reader["ID"]);
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

        public bool GetTreeFollow(long uid, out long referalId, out string referal)
        {
            GetFromReferalId(uid, out referalId);
            GetReferal(referalId, out referal);
            return true;
        }

        public override bool InsertNewAccount(long pid, string referal, long telegramiD, long fromReferal, States state,
            DateTime date)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = pid},
                new SqlParameter("Referal", SqlDbType.VarChar, 50) {Value = referal},
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Value = telegramiD},
                new SqlParameter("FromReferal", SqlDbType.BigInt) {Value = fromReferal},
                new SqlParameter("State", SqlDbType.Int) {Value = (int) state},
                new SqlParameter("Date", SqlDbType.Date) {Value = date}
            };
            var result = CallFunction("InsertNewAccount", args);
            return result;
        }

        public override bool InsertRedList(long pid, params string[] reds)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("ID", SqlDbType.BigInt) {Value = pid});
            for(int i = 0; i < reds.Length; i++)
            {
                parameters.Add(new SqlParameter("Red" + (i+1), SqlDbType.VarChar, 50) {Value = reds[i]});
            }
           
            var result = CallFunction("InsertRedList", parameters.ToArray());
            return result;
        }
        public bool InsertLanguage(long pid, Language lang)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = pid},
                new SqlParameter("Language", SqlDbType.Int) {Value = (int)lang}
            };

            var result = CallFunction("InsertLanguage", args);
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
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                return false;
            }
            return (bool) args[1].Value;
        }

        public bool BlockLicense(long uid)
        {
            return UpdateState(uid, (int)States.Blocked);
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
        public  bool UpdateState(long uid, int state)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("State", SqlDbType.Int) {Value = state}
            };
            return CallFunction("UpdateState", args);
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
        public bool UpdateLanguage(long uid, Language lang)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = uid},
                new SqlParameter("Language", SqlDbType.Int) {Value = (int)lang}
            };
            return CallFunction("UpdateLanguage", args);
        }

        object start = new object();
        
        public bool CallFunction(string functionName, params SqlParameter[] parameters)
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
            try
            {
                lock (start)
                {
                    using (var command = new SqlCommand(functionName, MySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        return command.ExecuteNonQuery() != 0;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Reconnect");
                MySqlConnection.Close();
                MySqlConnection = new SqlConnection(MySqlConnection.ConnectionString);
                Connect();
                return false;
            }
        }

        public void Connect()
        {
            if (MySqlConnection.State == ConnectionState.Open)
            {
                return;
            }
            MySqlConnection.Open();
        }
    }
}
