using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.Base;
using Microsoft.Practices.RepositoryFactory.SchemaDiscovery.ObjectModel.SqlServer;
using System.Globalization;

namespace Microsoft.Practices.RepositoryFactory.SchemaDiscovery.Translators.SqlServer.Helpers
{
    /// <summary/>
    internal static class SqlServerTranslatorHelper
    {
        #region Public Implementation
        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <param name="dbValue">The db value.</param>
        /// <returns></returns>
        public static ParameterDirection GetDirection(string dbValue)
        {
            ParameterDirection direction = ParameterDirection.Input;

            switch(dbValue.ToLowerInvariant())
            {
                case "in":
                    direction = ParameterDirection.Input;
                    break;
                case "out":
                    direction = ParameterDirection.Output;
                    break;
                case "inout":
                    direction = ParameterDirection.InputOutput;
                    break;
                default:
                    break;
            }

            return direction;
        }

        /// <summary>
        /// Determines whether the specified db value is nullable.
        /// </summary>
        /// <param name="dbValue">The db value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified db value is nullable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullable(string dbValue)
        {
            return string.Compare(dbValue, "yes", StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Gets the precision.
        /// </summary>
        /// <param name="rowValue">The row value.</param>
        /// <returns></returns>
        public static Byte GetPrecision(object rowValue)
        {
            Byte response;

            if(!Byte.TryParse(rowValue.ToString(), out response))
            {
                response = 0;
            }

            return response;
        }

        /// <summary>
        /// Gets the scale.
        /// </summary>
        /// <param name="rowValue">The row value.</param>
        /// <returns></returns>
        public static Int16 GetScale(object rowValue)
        {
            Int16 response;

            if(!Int16.TryParse(rowValue.ToString(), out response))
            {
                response = 0;
            }

            return response;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <param name="rowValue">The row value.</param>
        /// <returns></returns>
        public static int GetSize(object rowValue)
        {
            int response;

            if(!Int32.TryParse(rowValue.ToString(), out response))
            {
                response = 0;
            }

            return response;
        }

        /// <summary>
        /// Gets the size of the parameter.
        /// </summary>
        /// <param name="providerType">Type of the provider.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public static int GetParameterSize(DatabaseType providerType, DataRow parameter)
        {
            switch(providerType)
            {
                case DatabaseType.Binary:
                    return 0;
                case DatabaseType.Blob:
                    return 0;
                case DatabaseType.Boolean:
                    return 1;
                case DatabaseType.Byte:
                    return 1;
                case DatabaseType.Char:
                    return (int)parameter["CHARACTER_MAXIMUM_LENGTH"];
                case DatabaseType.Currency:
                    return 8;
                case DatabaseType.Date:
                    return 8;
                case DatabaseType.Decimal:
                    return 8;
                case DatabaseType.Float:
                    return 8;
                case DatabaseType.Int64:
                    return 8;
                case DatabaseType.Int32:
                    return 4;
                case DatabaseType.Int16:
                    return 2;
                case DatabaseType.NChar:
                    return (int)parameter["CHARACTER_MAXIMUM_LENGTH"];
                case DatabaseType.NText:
                    return 0;
                case DatabaseType.NVarChar:
                    return (int)parameter["CHARACTER_MAXIMUM_LENGTH"];
                case DatabaseType.Object:
                    return 0;
                case DatabaseType.Real:
                    return 8;
                case DatabaseType.SmallDate:
                    return 8;
                case DatabaseType.SmallCurrency:
                    return 0;
                case DatabaseType.Text:
                    return 0;
                case DatabaseType.Timestamp:
                    return 0;
                case DatabaseType.Guid:
                    return 0;
                case DatabaseType.VarBinary:
                    return 0;
                case DatabaseType.VarChar:
                    return (int)parameter["CHARACTER_MAXIMUM_LENGTH"];
                case DatabaseType.NotSupported:
                    return 0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets the default parameter value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetDefaultParameterValue(Type type)
        {
            if(((type == typeof(short)) || (type == typeof(int))) || ((type == typeof(long)) || (type == typeof(byte))))
            {
                return 0;
            }
            if(type == typeof(string))
            {
                return string.Empty;
            }
            if(type == typeof(bool))
            {
                return true;
            }
            if(((type == typeof(decimal)) || (type == typeof(float))) || (type == typeof(double)))
            {
                return 0;
            }
            if(type == typeof(DateTime))
            {
                return DateTime.Today;
            }
            return DBNull.Value;
        }

        /// <summary>
        /// Gets the name of the qualified.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetQualifiedName(string name)
        {
            return ('[' + name + ']');
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <param name="dbSchema">The db schema.</param>
        /// <param name="dbObject">The db object.</param>
        /// <returns></returns>
        public static string GetObjectName(string dbSchema, string dbObject)
        {
            return string.Format(CultureInfo.CurrentCulture, Properties.Resources.EscapeObjectNameWithSchema, dbSchema, dbObject);
        }

        #endregion
    }
}
