using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AgileFx.ORM;

namespace taskometer.Core.Models
{
    public partial class User
    {
        public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public static User Create(string username, string firstName, string lastName, string role, Tenant tenant)
        {
            if (tenant.EntityContext.CreateQuery<Account>().Any(a => a.Username == username && a.Tenant.Id == tenant.Id))
                throw new Exception("Account with same username already exists.");

            var account = new Account()
            {
                Username = username,
                Status = ACCOUNT_STATUS.ACTIVE,
                DateCreated = DateTime.Now,
                DateLastLogin = DateTime.Now,
                Tenant = tenant
            };
            var adminUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Account = account,
                Tenant = tenant
            };

            adminUser.Account.Roles.Add(tenant.EntityContext.CreateQuery<Role>().Single(r => r.Tenant.Id == tenant.Id && r.Name == ROLES.ADMINISTRATOR));
            tenant.EntityContext.AddObject(adminUser);

            return adminUser;
        }
    }
}
