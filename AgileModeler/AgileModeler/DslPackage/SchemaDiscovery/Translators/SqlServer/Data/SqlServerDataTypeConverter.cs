using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Data
{
    internal static class SqlServerDataTypeConverter
    {
        #region Public Implementation
        public static SqlDbType DatabaseType2SqlDbType(DatabaseType dbType)
        {
            switch(dbType)
            {
                case DatabaseType.Binary:
                    return SqlDbType.Binary;
                case DatabaseType.Blob:
                    return SqlDbType.Image;
                case DatabaseType.Boolean:
                    return SqlDbType.Bit;
                case DatabaseType.Byte:
                    return SqlDbType.TinyInt;
                case DatabaseType.Char:
                    return SqlDbType.Char;
                case DatabaseType.Currency:
                    return SqlDbType.Money;
                case DatabaseType.Date:
                    return SqlDbType.DateTime;
                case DatabaseType.Decimal:
                    return SqlDbType.Decimal;
                case DatabaseType.Float:
                    return SqlDbType.Float;
                case DatabaseType.Int64:
                    return SqlDbType.BigInt;
                case DatabaseType.Int32:
                    return SqlDbType.Int;
                case DatabaseType.Int16:
                    return SqlDbType.SmallInt;
                case DatabaseType.NChar:
                    return SqlDbType.NChar;
                case DatabaseType.NText:
                    return SqlDbType.NText;
                case DatabaseType.NVarChar:
                    return SqlDbType.NVarChar;
                case DatabaseType.Object:
                    return SqlDbType.Variant;
                case DatabaseType.Real:
                    return SqlDbType.Real;
                case DatabaseType.SmallDate:
                    return SqlDbType.SmallDateTime;
                case DatabaseType.SmallCurrency:
                    return SqlDbType.SmallMoney;
                case DatabaseType.Text:
                    return SqlDbType.Text;
                case DatabaseType.Timestamp:
                    return SqlDbType.Timestamp;
                case DatabaseType.Guid:
                    return SqlDbType.UniqueIdentifier;
                case DatabaseType.VarBinary:
                    return SqlDbType.VarBinary;
                case DatabaseType.VarChar:
                    return SqlDbType.VarChar;
                default:
                    return SqlDbType.Variant;
            }
        }

        public static DatabaseType SqlDbType2DatabaseType(object enumObj)
        {
            switch(((SqlDbType)enumObj))
            {
                case SqlDbType.BigInt:
                    return DatabaseType.Int64;
                case SqlDbType.Binary:
                    return DatabaseType.Binary;
                case SqlDbType.Bit:
                    return DatabaseType.Boolean;
                case SqlDbType.Char:
                    return DatabaseType.Char;
                case SqlDbType.DateTime:
                    return DatabaseType.Date;
                case SqlDbType.Decimal:
                    return DatabaseType.Decimal;
                case SqlDbType.Float:
                    return DatabaseType.Float;
                case SqlDbType.Image:
                    return DatabaseType.Blob;
                case SqlDbType.Int:
                    return DatabaseType.Int32;
                case SqlDbType.Money:
                    return DatabaseType.Currency;
                case SqlDbType.NChar:
                    return DatabaseType.NChar;
                case SqlDbType.NText:
                    return DatabaseType.NText;
                case SqlDbType.NVarChar:
                    return DatabaseType.NVarChar;
                case SqlDbType.Real:
                    return DatabaseType.Real;
                case SqlDbType.UniqueIdentifier:
                    return DatabaseType.Guid;
                case SqlDbType.SmallDateTime:
                    return DatabaseType.SmallDate;
                case SqlDbType.SmallInt:
                    return DatabaseType.Int16;
                case SqlDbType.SmallMoney:
                    return DatabaseType.SmallCurrency;
                case SqlDbType.Text:
                    return DatabaseType.Text;
                case SqlDbType.Timestamp:
                    return DatabaseType.Timestamp;
                case SqlDbType.TinyInt:
                    return DatabaseType.Byte;
                case SqlDbType.VarBinary:
                    return DatabaseType.VarBinary;
                case SqlDbType.VarChar:
                    return DatabaseType.VarChar;
                case SqlDbType.Variant:
                    return DatabaseType.NotSupported;
                case SqlDbType.Xml:
                    return DatabaseType.NotSupported;
                default:
                    return DatabaseType.NotSupported;
            }
        }

        public static DatabaseType String2DatabaseType(string dataType)
        {
            switch(dataType.ToLowerInvariant())
            {
                case "bigint":
                    return DatabaseType.Int64;
                case "binary":
                    return DatabaseType.Binary;
                case "bit":
                    return DatabaseType.Boolean;
                case "char":
                    return DatabaseType.Char;
                case "datetime":
                    return DatabaseType.Date;
                case "decimal":
                    return DatabaseType.Decimal;
                case "float":
                    return DatabaseType.Float;
                case "image":
                    return DatabaseType.Blob;
                case "int":
                    return DatabaseType.Int32;
                case "money":
                    return DatabaseType.Currency;
                case "nchar":
                    return DatabaseType.NChar;
                case "ntext":
                    return DatabaseType.NText;
                case "numeric":
                    return DatabaseType.Decimal;
                case "nvarchar":
                    return DatabaseType.NVarChar;
                case "real":
                    return DatabaseType.Real;
                case "sql_variant":
                    return DatabaseType.NotSupported;
                case "smalldatetime":
                    return DatabaseType.SmallDate;
                case "smallint":
                    return DatabaseType.Int16;
                case "smallmoney":
                    return DatabaseType.SmallCurrency;
                case "text":
                    return DatabaseType.Text;
                case "timestamp":
                    return DatabaseType.Timestamp;
                case "tinyint":
                    return DatabaseType.Byte;
                case "uniqueidentifier":
                    return DatabaseType.Guid;
                case "varbinary":
                    return DatabaseType.VarBinary;
                case "varchar":
                    return DatabaseType.VarChar;
                case "xml":
                    return DatabaseType.NotSupported;
                default:
                    return DatabaseType.NotSupported;
            }
        }

        public static Type DatabaseType2NetType(DatabaseType dbType)
        {
            switch(dbType)
            {
                case DatabaseType.Binary:
                    return typeof(byte[]);
                case DatabaseType.Blob:
                    return typeof(byte[]);
                case DatabaseType.Boolean:
                    return typeof(Boolean);
                case DatabaseType.Byte:
                    return typeof(Byte);
                case DatabaseType.Char:
                    return typeof(string);
                case DatabaseType.Currency:
                    return typeof(Decimal);
                case DatabaseType.Date:
                    return typeof(DateTime);
                case DatabaseType.Decimal:
                    return typeof(Decimal);
                case DatabaseType.Float:
                    return typeof(float);
                case DatabaseType.Int64:
                    return typeof(Int64);
                case DatabaseType.Int32:
                    return typeof(Int32);
                case DatabaseType.Int16:
                    return typeof(Int16);
                case DatabaseType.NChar:
                    return typeof(string);
                case DatabaseType.NText:
                    return typeof(string);
                case DatabaseType.NVarChar:
                    return typeof(string);
                case DatabaseType.Object:
                    return typeof(object);
                case DatabaseType.Real:
                    return typeof(float);
                case DatabaseType.SmallDate:
                    return typeof(DateTime);
                case DatabaseType.SmallCurrency:
                    return typeof(Decimal);
                case DatabaseType.Text:
                    return typeof(string);
                case DatabaseType.Timestamp:
                    return typeof(byte[]);
                case DatabaseType.Guid:
                    return typeof(Guid);
                case DatabaseType.VarBinary:
                    return typeof(byte[]);
                case DatabaseType.VarChar:
                    return typeof(string);
                default:
                    return typeof(object);
            }
        }
        #endregion
    }
}
