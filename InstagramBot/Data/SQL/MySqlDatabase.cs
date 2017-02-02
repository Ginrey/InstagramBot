using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

namespace InstagramBot.Data.SQL
{
    public class MySqlDatabase : Database
    {
        public SqlConnection MySqlConnection { get; set; }

        SqlDataReader reader;
        Random random = new Random();

        public MySqlDatabase(string connectionString)
        {
            if (MySqlConnection == null)
                MySqlConnection = new SqlConnection(connectionString);
        }

        public override bool GetCountInstagrams(out int count)
        {
            var args = new[]
            {
                new SqlParameter("Count", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetCountInstagrams", args);
            if (args[0].Value == DBNull.Value || args[0].Value == null)
            {
                count = 0;
                return false;
            }
            count = (int) args[0].Value;
            return true;
        }

        public override bool GetCountRedList(long id, out int count)
        {
            var args = new[]
            {
                new SqlParameter("Id", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Count", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetCountRedList", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                count = 0;
                return false;
            }
            count = (int) args[1].Value;
            return true;
        }

        public override bool GetListFromMyURL(long id, out List<MiniInfo> list)
        {
            list = new List<MiniInfo>();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id}
            };
            var dataReader = CallFunctionReader("GetListFromMyURL", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    list.Add(new MiniInfo((long) dataReader["Id"], (string) dataReader["URL"]));
                }
            return true;
        }

        public override bool GetListNonActiveFromMyURL(long id, out List<MiniInfo> list)
        {
            list = new List<MiniInfo>();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id}
            };
            var dataReader = CallFunctionReader("GetListNonActiveFromMyURL", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    list.Add(new MiniInfo((long) dataReader["Id"], (string) dataReader["URL"]));
                }
            return true;
        }

        public override bool GetFromTo(long id, out long fromId, out long toId)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("FromId", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                new SqlParameter("ToId", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetFromTo", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                fromId = toId = -1;
                return false;
            }
            fromId = (long) args[1].Value;
            toId = (long) args[2].Value;
            return result;
        }

        public override bool GetIdByTelegramId(long telegramId, out List<MiniInfo> list)
        {
            list = new List<MiniInfo>();
            var args = new[]
            {
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Value = telegramId}
            };
            var dataReader = CallFunctionReader("GetIdByTelegramId", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    list.Add(new MiniInfo((long) dataReader["Id"], (string) dataReader["URL"]));
                }
            return true;
        }

        public override bool GetLanguage(long telegramId, out Language language)
        {
            var args = new[]
            {
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Language", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetLanguage", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                language = 0;
                return false;
            }
            language = (Language) args[1].Value;
            return result;
        }

        public override bool GetBaseInstagram(long id, out MiniInfo info)
        {
            info = new MiniInfo();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("NextId", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                new SqlParameter("NextURL", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetBaseInstagram", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                return false;
            }
            info.ID = (long) args[1].Value;
            info.URL = (string) args[2].Value;
            return true;
        }

        public override bool GetBlockList(out List<long> list)
        {
            list = new List<long>();
            SqlParameter[] args = {};
            var dataReader = CallFunctionReader("GetBlockList", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    list.Add((long) dataReader["Id"]);
                }
            return true;
        }

        public override bool GetPriority(int level, out List<MiniInfo> list)
        {
            list = new List<MiniInfo>();
            var args = new[]
            {
                new SqlParameter("Level", SqlDbType.BigInt) {Value = level}
            };
            var dataReader = CallFunctionReader("GetPriority", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    var info = new MiniInfo((long) dataReader["Id"], (string) dataReader["URL"]);
                    int j = random.Next(list.Count + 1);
                    if (j == list.Count)
                    {
                        list.Add(info);
                    }
                    else
                    {
                        list.Add(list[j]);
                        list[j] = info;
                    }
                }
            return true;
        }

        public override bool GetRedList(long id, out List<MiniInfo> list)
        {
            list = new List<MiniInfo>();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id}
            };
            var dataReader = CallFunctionReader("GetRedList", args);
            if (dataReader == null) return false;
            lock (dataReader)
                while (dataReader.Read())
                {
                    list.Add(new MiniInfo((long) dataReader["Id"], (string) dataReader["URL"]));
                }
            return true;
        }

        public override bool GetReferal(long id, out MiniInfo info)
        {
            info = new MiniInfo();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("URL", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetReferal", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                return false;
            }
            info.ID = id;
            info.URL = (string) args[1].Value;
            return result;
        }

        public override bool GetStructure(long id, out StructureLine structure)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("CountFromMyURL", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("CountFromFriends", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("CountUlimited", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("CountMinimum", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("CountBlock", SqlDbType.Int) {Direction = ParameterDirection.Output},
                new SqlParameter("CountNull", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            bool result = CallFunction("GetStructure", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                structure = null;
                return false;
            }
            structure = new StructureLine
            {
                ID = id,
                CountFromMyURL = (int) args[1].Value,
                CountFromFriends = (int) args[2].Value,
                CountUlimited = (int) args[3].Value,
                CountMinimum = (int) args[4].Value,
                CountBlock = (int) args[5].Value,
                CountNull = (int) args[6].Value
            };
            return result;
        }

        public override bool GetTelegramId(long id, out long telegramId)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("TelegramID", SqlDbType.BigInt) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetLanguage", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                telegramId = 0;
                return false;
            }
            telegramId = (long) args[1].Value;
            return result;
        }

        public override bool GetTreeInstagram(long id, out MiniInfo info)
        {
            info = new MiniInfo();
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("NextId", SqlDbType.BigInt) {Direction = ParameterDirection.Output},
                new SqlParameter("NextURL", SqlDbType.VarChar, 50) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetTreeInstagram", args);
            if (args[1].Value == DBNull.Value || args[1].Value == null)
            {
                return false;
            }
            info.ID = (long) args[1].Value;
            info.URL = (string) args[2].Value;
            return true;
        }

        public override bool InsertInstagram(long id, string url)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("URL", SqlDbType.VarChar, 50) {Value = url}
            };
            bool result = CallFunction("InsertInstagram", args);
            return result;
        }

        public override bool InsertLanguage(long telegramId, Language language)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("Language", SqlDbType.Int) {Value = (int)language}
            };
            bool result = CallFunction("InsertLanguage", args);
            return result;
        }

        public override bool InsertRedList(long id, params long[] redIds)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("Id", SqlDbType.BigInt) {Value = id}
            };
            parameters.AddRange(
                redIds.Select((t, i) => new SqlParameter("RedID" + (i + 1), SqlDbType.BigInt) {Value = t}));
            var result = CallFunction("InsertRedList", parameters.ToArray());
            return result;
        }

        public override bool InsertTelegram(long telegramId, long instagramId)
        {
            var args = new[]
            {
                new SqlParameter("TelegramId", SqlDbType.BigInt) {Value = telegramId},
                new SqlParameter("InstagramId", SqlDbType.Int) {Value = instagramId}
            };
            bool result = CallFunction("InsertTelegram", args);
            return result;
        }

        public override bool InsertTree(long id, long fromId, long toId)
        {
            var args = new[]
            {
                new SqlParameter("Id", SqlDbType.BigInt) {Value = id},
                new SqlParameter("FromId", SqlDbType.Int) {Value = fromId},
                new SqlParameter("ToId", SqlDbType.Int) {Value = toId}
            };
            bool result = CallFunction("InsertTree", args);
            return result;
        }

        public override bool IsBlocked(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Block", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsBlocked", args);
            return args[1].Value != DBNull.Value && (bool) args[1].Value;
        }

        public override bool IsPresentInstagram(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentInstagram", args);
            return args[1].Value != DBNull.Value && (bool) args[1].Value;
        }
        public override bool IsPresentTelegram(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentTelegram", args);
            return args[1].Value != DBNull.Value && (bool)args[1].Value;
        }
        public override bool IsPresentURL(string url)
        {
            var args = new[]
            {
                new SqlParameter("URL", SqlDbType.VarChar, 50) {Value = url},
                new SqlParameter("Present", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsPresentURL", args);
            return args[1].Value != DBNull.Value && (bool) args[1].Value;
        }

        public override bool IsStart(long id)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Start", SqlDbType.Bit) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("IsStart", args);
            return args[1].Value != DBNull.Value && (bool) args[1].Value;
        }

        public override bool UpdateBlock(long id, bool block)
        {
            var args = new[]
            {
                new SqlParameter("Id", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Block", SqlDbType.Int) {Value = block}
            };
            bool result = CallFunction("UpdateBlock", args);
            return result;
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

        public override bool UpdateLanguage(long id, Language language)
        {
            var args = new[]
            {
                new SqlParameter("ID", SqlDbType.BigInt) {Value = id},
                new SqlParameter("Language", SqlDbType.Int) {Value = (int) language}
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
            catch (Exception ex)
            {
                Console.WriteLine($"Reconnect:{functionName} [{ex.Message}]");
                MySqlConnection.Close();
                MySqlConnection = new SqlConnection(MySqlConnection.ConnectionString);
                Connect();
                return false;
            }
        }

        public SqlDataReader CallFunctionReader(string functionName, params SqlParameter[] parameters)
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
                        return command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reconnect:{functionName} [{ex.Message}]");
                MySqlConnection.Close();
                MySqlConnection = new SqlConnection(MySqlConnection.ConnectionString);
                Connect();
                return null;
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
