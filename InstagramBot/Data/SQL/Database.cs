using System;
using System.Collections.Generic;
using InstagramBot.Data.Accounts;

namespace InstagramBot.Data.SQL
{
    public abstract class Database
    {
        public virtual bool GetCountInstagrams(out int count)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetFromTo(long id, out long fromId, out long toId)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetIdByTelegramId(long telegramId, out List<MiniInfo> list)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetLanguage(long telegramId, out int language)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetBaseInstagram(long id, out MiniInfo info)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetBlockList(out List<long> list)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetPriority(int level, out List<MiniInfo> list)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetRedList(long id, out List<MiniInfo> list)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetReferal(long id, out MiniInfo info)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetStructure(long id, out StructureLine structure)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetTelegramId(long id, out long telegramId)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetTreeInstagram(long id, out MiniInfo info)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertInstagram(long id, string url)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertLanguage(long telegramId, int language)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertRedList(long id, params long[] redIds)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertTelegram(long telegramId, long instagramId)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertTree(long id, long fromId, long toId)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsBlocked(long id)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsPresentInstagram(long id)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsPresentURL(string url)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsStart(long id)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateBlock(long id, bool block)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateLanguage(long id, int language)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateStatus(long id, bool status)
        {
            throw new NotImplementedException();
        }
        ////////////////////////////////
        ////////////////
      /*  public virtual bool GetLicenseState(long uid, out States complete)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetCountFollows(long uid, out int count)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetRedList(long uid, out List<string> list)
        {
            throw new NotImplementedException();
        }
     

        public virtual bool GetReferalByTelegramId(long tid, out string referal)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetNeedReferalForFollow(long uid, out long referalId, out string referal)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetFromReferalId(long uid, out long referalId)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetTelegramId(long uid, out long telegramId)
        {
            throw new NotImplementedException();
        }
        public virtual List<string> GetPriorityList(int priority)
        {
            throw new NotImplementedException();
        }
        public virtual List<StructInfo> GetStructInfo(long fromId)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertNewAccount(long pid, string referal,long telegramId, long fromReferal, States state, DateTime date)
        {
            throw new NotImplementedException();
        }
        public virtual bool InsertRedList(long pid, params string[] reds)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsLicenseStart(long uid)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsPresentLicense(long uid)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsPresentReferal(string referal)
        {
            throw new NotImplementedException();
        }

     
        public virtual bool UpdateCountFollows(long uid, int count)
        {
            throw new NotImplementedException();
        }*/
    }
}
