using System;
using System.Collections.Generic;
using InstagramBot.Data.Accounts;
using InstagramBot.IO;

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
        public virtual bool GetLanguage(long telegramId, out Language language)
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
        public virtual bool GetCountRedList(long id, out int count)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetListFromMyURL(long id, out List<MiniInfo> list)
        {
            throw new NotImplementedException();
        }
        public virtual bool GetListNonActiveFromMyURL(long id, out List<MiniInfo> list)
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
        public virtual bool InsertLanguage(long telegramId, Language language)
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
        public virtual bool IsPresentTelegram(long id)
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
        public virtual bool UpdateLanguage(long id, Language language)
        {
            throw new NotImplementedException();
        }
        public virtual bool UpdateStatus(long id, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
