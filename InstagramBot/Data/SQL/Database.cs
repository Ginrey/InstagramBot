using System;
using System.Collections.Generic;

namespace InstagramBot.Data.SQL
{
    public abstract class Database
    {
        public virtual bool GetLicenseState(long uid, out States complete)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetCountFollows(long uid, out int count)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetReferal(long uid, out string referal)
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

        public virtual bool InsertNewAccount(long pid, string referal, long fromReferal, States state)
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
    }
}
