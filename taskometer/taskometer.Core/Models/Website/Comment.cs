using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core.Models
{
    public partial class Comment
    {
        public static Comment Create(Page page, string text, string addedByName, string addedByEmail, string addedByWebsite, string ipAddress, User addedBy, Tenant tenant)
        {
            return new Comment
            {
                Body = text,
                AddedBy = addedBy,
                AddedByDisplayName = addedByName,
                AddedByEmail = addedByEmail,
                AddedByWebsite = addedByWebsite,
                IPAddress = ipAddress,
                AddedOn = DateTime.Now,
                Page = page,
                Tenant = tenant
            };
        }
    }
}
