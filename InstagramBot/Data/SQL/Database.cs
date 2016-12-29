using System;
using System.Collections.Generic;

namespace InstagramBot.Data.SQL
{
    public abstract class Database
    {
        public virtual bool GetLicense(byte[] hardwareKey, out int uid)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetLicenseIDOnSkype(string skype, out int pid)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetMD5Software(int uid, int sid, out byte[] MD5Hash)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetSoftwareID(string softwarename, out int sid)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetSkype(int uid, out string skype)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetSoftwareSerialize(int sid, out string serializeClass, out string callMethod)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetLicenseState(int uid, out States complete)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertNewAccount(int pid, string referal, int fromReferal, States state)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetLicenseTime(int uid, int sid, out UnixTime time)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetSoftwareConfig(int sid,
          out ushort widthForm, out ushort heightForm,
          out ushort widthComponent, out ushort heightComponent,
           out ushort leftComponent, out ushort topComponent, out string version)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetOffsets(string BaseAddress,
         out string GameAdress, out string Struct, out string SendPacket, out string PlayerName, out string PlayerHP, out string PlayerMaxHP,
         out string PlayerMP, out string PlayerMaxMP, out string PlayerMagDef, out string PlayerFizDef, out string PlayerUklon, out string PlayerMetca,
         out string PlayerMinMagAttack, out string PlayerMaxMagAttack, out string PlayerMinFizAttack, out string PlayerMaxFizAttack, out string PlayerPA, 
         out string PlayerPZ, out string PlayerWID, out string PlayerLocX, out string PlayerLocY, out string PlayerLocZ, out string InventoryArray,
         out string Yacheyka, out string ItemID, out string ItemCount, out string MaxCountYackeyka, out string MoneyCount, out string TargetWID,
         out string Walk1, out string Walk2, out string Walk3, out string WalkOffset, out string OriginalBytes)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToLicense(byte[] hardwareKey)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToLicenseState(int pid, int sid, bool complete)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToLicenseTime(int pid, int sid, UnixTime time)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToLink(int pid, string skype)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToMD5(int pid, int sid, byte[] md5)
        {
            throw new NotImplementedException();
        }

        public virtual bool InsertToOnlineState(int pid, bool online)
        {
            throw new NotImplementedException();
        }

        public virtual bool UpdateLicense(int pid, byte[] licenseKey)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateLicenseState(int pid,int sid, bool state)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateLicenseTime(int pid, int sid, UnixTime time)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateLink(int pid, string skype)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateMD5(int pid, int sid, byte[] softwareMd5)
        {
            throw new NotImplementedException();
        }

        public virtual bool UpdateOnlineState(int pid, bool state)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<dynamic> Query(string name, params object[] args)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteLicense(int pid)
        {
            throw new NotImplementedException();
        }
    }
}
