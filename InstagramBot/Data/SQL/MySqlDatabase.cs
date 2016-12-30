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
        public override bool GetLicense(byte[] hardwareKey, out int uid)
        {
            var args = new[]
            {
                new SqlParameter("HardwareID", SqlDbType.VarBinary, 100) {Value = hardwareKey},
                new SqlParameter("Index", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };

            var result = CallFunction("GetLicense", args);
            if (args[1].Value == DBNull.Value)
            {
                uid = -1;
                return false;
            }
            uid = (int) args[1].Value;
            return result;
        }

        public override bool GetLicenseIDOnSkype(string skype, out int pid)
        {
            var args = new[]
            {
                new SqlParameter("Skype", SqlDbType.VarChar, 50) {Value = skype},
                new SqlParameter("PID", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };

            var result = CallFunction("GetLicenseIDOnSkype", args);
            if (args[1].Value == DBNull.Value)
            {
                pid = -1;
                return false;
            }

            pid = (int) args[1].Value;
            return result;
        }

        public override bool GetMD5Software(int uid, int sid, out byte[] MD5Hash)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = uid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("MD5Hash", SqlDbType.VarBinary, 32) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetMD5Software", args);
            if (args[2].Value == DBNull.Value)
            {
                MD5Hash = new byte[] {};
                return false;
            }
            MD5Hash = (byte[]) args[2].Value;
            return result;
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
            state = (States)args[2].Value;
            return result;
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

        public override bool GetLicenseTime(int uid, int sid, out UnixTime time)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = uid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("Time", SqlDbType.DateTime) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetLicenseTime", args);
            time = new UnixTime((DateTime)args[2].Value);
            return result;
        }

        public override bool GetSoftwareID(string SoftName, out int sid)
        {
            var args = new[]
            {
                new SqlParameter("SoftName", SqlDbType.VarChar, 20) {Value = SoftName},
                new SqlParameter("SoftID", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetSoftwareID", args);

            if (args[1].Value == DBNull.Value)
            {
                sid = -1;
                return false;
            }

            sid = (int) args[1].Value;
            return result;
        }

        public override bool GetSkype(int uid, out string skype)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) { Value = uid },
                new SqlParameter("Skype", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output }
            };
            var result = CallFunction("GetSkype", args);
            skype = (string)args[1].Value;
            return result;
        }

        public override bool GetSoftwareSerialize(int sid, out string serializeClass, out string callMethod)
        {
            var args = new[]
            {
                new SqlParameter("SID", SqlDbType.Int) { Value = sid },
                new SqlParameter("SerializeClass", SqlDbType.VarChar, 1000) { Direction = ParameterDirection.Output },
                new SqlParameter("CallMethod", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output }
            };
            var result = CallFunction("GetSoftwareSerialize", args);
    serializeClass = (string)args[1].Value;
            callMethod = (string)args[2].Value;
            return result;
        }

        public override bool GetSoftwareConfig(int sid,
          out ushort widthForm, out ushort heightForm,
          out ushort widthComponent, out ushort heightComponent,
           out ushort leftComponent, out ushort topComponent, out string version)
        {
            var args = new[]
            {
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("WidthForm", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("HeightForm", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("WidthComponent", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("HeightComponent", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("LeftComponent", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("TopComponent", SqlDbType.SmallInt) {Direction = ParameterDirection.Output},
                new SqlParameter("Version", SqlDbType.VarChar, 20) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetSoftwareConfig", args);
            widthForm = (ushort) args[1].Value;
            heightForm = (ushort)args[2].Value;
            widthComponent = (ushort)args[3].Value;
            heightComponent = (ushort)args[4].Value;
            leftComponent = (ushort)args[5].Value;
            topComponent = (ushort)args[6].Value;
            version = (string)args[7].Value;
            return result;
        }

        public override bool GetOffsets(string BaseAddress,
        out string GameAdress, out string Struct, out string SendPacket, out string PlayerName, out string PlayerHP, out string PlayerMaxHP,
        out string PlayerMP, out string PlayerMaxMP, out string PlayerMagDef, out string PlayerFizDef, out string PlayerUklon, out string PlayerMetca,
        out string PlayerMinMagAttack, out string PlayerMaxMagAttack, out string PlayerMinFizAttack, out string PlayerMaxFizAttack, out string PlayerPA,
        out string PlayerPZ, out string PlayerWID, out string PlayerLocX, out string PlayerLocY, out string PlayerLocZ, out string InventoryArray,
        out string Yacheyka, out string ItemID, out string ItemCount, out string MaxCountYackeyka, out string MoneyCount, out string TargetWID,
        out string Walk1, out string Walk2, out string Walk3, out string WalkOffset, out string OriginalBytes)
        {
            var args = new[]
            {
                new SqlParameter("BaseAddress", SqlDbType.VarChar, 10) {Value = BaseAddress},
                new SqlParameter("GameAdress", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("Struct", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("SendPacket", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerName", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerHP", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMaxHP", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMP", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMaxMP", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMagDef", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerFizDef", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerUklon", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMetca", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMinMagAttack", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMaxMagAttack", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMinFizAttack", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerMaxFizAttack", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerPA", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerPZ", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerWID", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerLocX", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerLocY", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("PlayerLocZ", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("InventoryArray", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("Yacheyka", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("ItemID", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("ItemCount", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("MaxCountYackeyka", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("MoneyCount", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("TargetWID", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("Walk1", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("Walk2", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("Walk3", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("WalkOffset", SqlDbType.VarChar, 10) {Direction = ParameterDirection.Output},
                new SqlParameter("OriginalBytes", SqlDbType.VarChar, 200) {Direction = ParameterDirection.Output}
            };
            var result = CallFunction("GetOffsets", args);
            if (args[1].Value != DBNull.Value)
            {
                GameAdress =    (string) args[1].Value;
                Struct =        (string) args[2].Value;
                SendPacket =    (string) args[3].Value;
                PlayerName =    (string) args[4].Value;
                PlayerHP =      (string) args[5].Value;
                PlayerMaxHP =   (string) args[6].Value;
                PlayerMP =      (string) args[7].Value;
                PlayerMaxMP =   (string) args[8].Value;
                PlayerMagDef =  (string) args[9].Value;
                PlayerFizDef =  (string) args[10].Value;
                PlayerUklon =   (string) args[11].Value;
                PlayerMetca =   (string) args[12].Value;
                PlayerMinMagAttack = (string) args[13].Value;
                PlayerMaxMagAttack = (string) args[14].Value;
                PlayerMinFizAttack = (string) args[15].Value;
                PlayerMaxFizAttack = (string) args[16].Value;
                PlayerPA =      (string) args[17].Value;
                PlayerPZ =      (string) args[18].Value;
                PlayerWID =     (string) args[19].Value;
                PlayerLocX =    (string) args[20].Value;
                PlayerLocY =    (string) args[21].Value;
                PlayerLocZ =    (string) args[22].Value;
                InventoryArray = (string) args[23].Value;
                Yacheyka =      (string) args[24].Value;
                ItemID =        (string) args[25].Value;
                ItemCount =     (string) args[26].Value;
                MaxCountYackeyka = (string) args[27].Value;
                MoneyCount =    (string) args[28].Value;
                TargetWID =     (string) args[29].Value;
                Walk1 =         (string) args[30].Value;
                Walk2 =         (string) args[31].Value;
                Walk3 =         (string) args[32].Value;
                WalkOffset =    (string) args[33].Value;
                OriginalBytes = (string) args[34].Value;
            } else
            {
                GameAdress = "0";
                Struct = "0";
                SendPacket = "0";
                PlayerName = "0";
                PlayerHP = "0";
                PlayerMaxHP = "0";
                PlayerMP = "0";
                PlayerMaxMP = "0";
                PlayerMagDef = "0";
                PlayerFizDef = "0";
                PlayerUklon = "0";
                PlayerMetca = "0";
                PlayerMinMagAttack = "0";
                PlayerMaxMagAttack = "0";
                PlayerMinFizAttack = "0";
                PlayerMaxFizAttack = "0";
                PlayerPA = "0";
                PlayerPZ = "0";
                PlayerWID = "0";
                PlayerLocX = "0";
                PlayerLocY = "0";
                PlayerLocZ = "0";
                InventoryArray = "0";
                Yacheyka = "0";
                ItemID = "0";
                ItemCount = "0";
                MaxCountYackeyka = "0";
                MoneyCount = "0";
                TargetWID = "0";
                Walk1 = "0";
                Walk2 = "0";
                Walk3 = "0";
                WalkOffset = "0";
                OriginalBytes = "0";
            }

            return result;
        }


        public override bool InsertToLicense(byte[] hardwareKey)
        {
            var args = new[]
            {
                new SqlParameter("LicenseKey", SqlDbType.VarBinary, 100) {Value = hardwareKey}
            };
            var result = CallFunction("InsertToLicense", args);
            return result;
        }

        public override bool InsertToLicenseState(int pid, int sid, bool complete)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("Complete", SqlDbType.Bit) {Value = complete}
            };
            var result = CallFunction("InsertToLicenseState", args);
            return result;
        }

        public override bool InsertToLicenseTime(int pid, int sid, UnixTime time)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("SoftwareDate", SqlDbType.DateTime) {Value = (DateTime)time}
            };
            var result = CallFunction("InsertToLicenseTime", args);
            return result;
        }

        public override bool InsertToLink(int pid, string skype)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("Skype", SqlDbType.VarChar, 50) {Value = skype}
            };
            var result = CallFunction("InsertToLink", args);
            return result;
        }

        public override bool InsertToMD5(int pid, int sid, byte[] md5)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("SoftwareMD5", SqlDbType.VarBinary, 32) {Value = md5}
            };
            var result = CallFunction("InsertToMD5", args);
            return result;
        }

        public override bool InsertToOnlineState(int pid, bool online)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("Online", SqlDbType.Bit) {Value = online}
            };
            var result = CallFunction("InsertToOnlineState", args);
            return result;
        }

        public override bool UpdateLicense(int pid, byte[] licenseKey)
        {
            var args = new[]
           {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("LicenseKey", SqlDbType.VarBinary, 100) {Value = licenseKey}
            };
            var result = CallFunction("UpdateLicense", args);
            return result;
        }
        public override bool UpdateLicenseState(int pid, int sid, bool state)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("State", SqlDbType.Bit) {Value = state}
            };
            var result = CallFunction("UpdateLicenseState", args);
            return result;
        }
        public override bool UpdateLicenseTime(int pid, int sid, UnixTime time)
        {
            var args = new[]
           {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("SoftwareDate", SqlDbType.DateTime) {Value = (DateTime)time}
            };
            var result = CallFunction("UpdateLicenseTime", args);
            return result;
        }
        public override bool UpdateLink(int pid, string skype)
        {
            var args = new[]
          {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("Skype", SqlDbType.VarChar, 50) {Value = skype}
            };
            var result = CallFunction("UpdateLink", args);
            return result;
        }
        public override bool UpdateMD5(int pid, int sid, byte[] softwareMd5)
        {
            var args = new[]
           {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("SID", SqlDbType.Int) {Value = sid},
                new SqlParameter("SoftwareMD5", SqlDbType.VarBinary, 32) {Value = softwareMd5}
            };
            var result = CallFunction("UpdateMD5", args);
            return result;
        }

        public override bool UpdateOnlineState(int pid, bool state)
        {
            var args = new[]
            {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid},
                new SqlParameter("Online", SqlDbType.Bit) {Value = state}
            };
            var result = CallFunction("UpdateOnlineState", args);
            return result;
        }

        public override bool DeleteLicense(int pid)
        {
            var args = new[]
           {
                new SqlParameter("PID", SqlDbType.Int) {Value = pid}
            };
            var result = CallFunction("DeleteLicense", args);
            return result;
        }

        public Dictionary<int,string> GetLicenses()
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
           Dictionary<int, string> Licenses = new Dictionary<int, string>();
            using (var command = new SqlCommand("select Link.PID, Link.Skype from Link inner join License on Link.PID = License.PID", MySqlConnection))
            {
                command.CommandType = CommandType.Text;
                var reader =  command.ExecuteReader();
               while(reader.Read())
               {
                   Licenses.Add((int) reader["PID"], (string)reader["Skype"]);
               }
                reader.Close();
            }
           return Licenses;
        }

       /* public List<AccountInfo> GetStatesLicenses(int pid)
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                Connect();
            }
            List<AccountInfo> licenses = new List<AccountInfo>();
            using (var command = new SqlCommand("select Software.Name, LicenseState.Complete, LicenseTime.SoftwareDate from Software inner join LicenseState  on Software.SID = LicenseState.SID inner join LicenseTime on LicenseTime.SID = LicenseState.SID where LicenseState.PID = LicenseTime.PID and LicenseState.PID = " + pid, MySqlConnection))
            {
                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var info = new AccountInfo
                    {
                        LicenseID = pid,
                        SoftwareName = (string) reader["Name"],
                        State = (bool) reader["Complete"],
                        SoftTime = (DateTime) reader["SoftwareDate"]
                    };
                    licenses.Add(info);
                }
                reader.Close();
            }
            return licenses;
        }*/

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
