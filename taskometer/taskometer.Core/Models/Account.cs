using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgileFx.ORM;
using AgileFx.MVC;
using AgileFx.Security;

namespace taskometer.Core.Models
{
    public partial class Account
    {
        public static Account VerifyCredentials(long tenantId, string username, string password)
        {
            string hashedPassword = CryptoUtil.HashPassword(password);
            return new Entities().Account.LoadRelated(a => a.Roles).SingleOrDefault
                (a => a.Tenant.Id == tenantId && a.Username.Equals(username) && a.Password.Equals(hashedPassword));
        }

        public void SetPassword(string newPassword)
        {
            this.Password = CryptoUtil.HashPassword(newPassword);
        }
    }
}
