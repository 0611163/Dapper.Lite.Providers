using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Lite.Provider
{
    public class ColumnTypeUtil
    {
        public static DbType GetDBType(object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                {
                    return DbType.DateTime;
                }
                else if (type == typeof(string))
                {
                    return DbType.String;
                }
                else if (type == typeof(float) || type == typeof(float?))
                {
                    return DbType.Double;
                }
                else if (type == typeof(double) || type == typeof(double?))
                {
                    return DbType.Double;
                }
                else if (type == typeof(decimal) || type == typeof(decimal?))
                {
                    return DbType.Decimal;
                }
                else if (type == typeof(short) || type == typeof(short?))
                {
                    return DbType.Int16;
                }
                else if (type == typeof(int) || type == typeof(int?))
                {
                    return DbType.Int32;
                }
                else if (type == typeof(long) || type == typeof(long?))
                {
                    return DbType.Int64;
                }
            }
            return DbType.String;
        }

        public static string GetDBTypeName(Type parameterType)
        {
            if (parameterType == typeof(DateTime) || parameterType == typeof(DateTime?))
            {
                return "DateTime";
            }
            else if (parameterType == typeof(string))
            {
                return "String";
            }
            else if (parameterType == typeof(float) || parameterType == typeof(float?))
            {
                return "Float32";
            }
            else if (parameterType == typeof(double) || parameterType == typeof(double?))
            {
                return "Float64";
            }
            else if (parameterType == typeof(decimal) || parameterType == typeof(decimal?))
            {
                return "Float64";
            }
            else if (parameterType == typeof(short) || parameterType == typeof(short?))
            {
                return "Int16";
            }
            else if (parameterType == typeof(int) || parameterType == typeof(int?))
            {
                return "Int32";
            }
            else if (parameterType == typeof(long) || parameterType == typeof(long?))
            {
                return "Int64";
            }
            return "String";
        }

    }
}
