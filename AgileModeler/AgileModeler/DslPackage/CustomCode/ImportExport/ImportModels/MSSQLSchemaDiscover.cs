using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery
{
    public class MSSQLSchemaDiscover : DbSchemaDiscoverer
    {
        ConnectionStringSettings connSettings = null;
        public MSSQLSchemaDiscover(ConnectionStringSettings connSettings)
            : base(connSettings)
        {
            this.connSettings = connSettings;
        }

        public List<Association> DiscoverAssociations()
        {
            var cmdText = @"IF @@version like '%2005%'
                SELECT	fkr.name as Association, o1.name as ChildTable, c1.name as ChildMember, o2.name as ParentTable, c2.name as ParentMember
                FROM	sys.objects o1 JOIN sys.columns c1 ON o1.object_id = c1.object_id
			        JOIN sys.foreign_key_columns fkc ON fkc.parent_object_id = c1.object_id AND fkc.parent_column_id = c1.column_id
    			    JOIN sys.columns c2 ON c2.object_id = fkc.referenced_object_id AND c2.column_id = fkc.referenced_column_id
	    		    JOIN sys.objects o2 ON o2.object_id = c2.object_id
		    	    JOIN sys.foreign_keys fkr ON  fkr.parent_object_id = fkc.parent_object_id
					    and fkr.referenced_object_id = fkc.referenced_object_id
					    and fkc.constraint_object_id = fkr.object_id
            ELSE
	            SELECT	o1.name as [Association], o2.name [ChildTable], c1.name as [ChildMember], o3.name as [ParentTable], c2.name as [ParentMember]
	            FROM	sysforeignkeys fk join sysobjects o1 on fk.constid = o1.id 
			        join sysobjects o2 on fk.fkeyid = o2.id 
			        join sysobjects o3 on fk.rkeyid = o3.id 
			        join syscolumns c1 on fk.fkey = c1.colid and fk.fkeyid = c1.id 
			        join syscolumns c2 on fk.rkey = c2.colid and fk.rkeyid = c2.id";

            var associations = new List<Association>();
            using (var conn = new SqlConnection(connSettings.ConnectionString))
            {
                conn.Open();
                using (var reader = new SqlCommand(cmdText, conn).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        associations.Add(new Association
                        {
                            Name = reader["Association"] as string,
                            ChildTable = reader["ChildTable"] as string,
                            ChildMember = reader["ChildMember"] as string,
                            ParentTable = reader["ParentTable"] as string,
                            ParentMember = reader["ParentMember"] as string
                        });
                    }
                }
            }

            return associations;
        }
    }
}
