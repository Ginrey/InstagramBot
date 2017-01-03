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

        public virtual bool InsertNewAccount(long pid, string referal, long fromReferal, States state)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsPresentLicense(long uid)
        {
            throw new NotImplementedException();
        }
    }
}
