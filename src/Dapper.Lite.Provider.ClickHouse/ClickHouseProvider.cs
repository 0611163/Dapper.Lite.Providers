using ClickHouse.Client.ADO;
using ClickHouse.Client.ADO.Parameters;
using Dapper.Lite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Lite.Provider
{
    public class ClickHouseProvider : IProvider
    {
        #region Quote
        public string OpenQuote
        {
            get
            {
                return "\"";
            }
        }

        public string CloseQuote
        {
            get
            {
                return "\"";
            }
        }
        #endregion

        #region 创建Db对象
        public DbConnection CreateConnection(string connectionString)
        {
            return new ClickHouseConnection(connectionString);
        }

        public DbCommand GetCommand(DbConnection conn)
        {
            DbCommand command = conn.CreateCommand();
            return command;
        }

        public DbCommand GetCommand(string sql, DbConnection conn)
        {
            DbCommand command = conn.CreateCommand();
            command.CommandText = sql;
            return command;
        }

        public DbParameter GetDbParameter(string name, object value)
        {
            DbParameter parameter = new ClickHouseDbParameter();
            parameter.ParameterName = name.Trim(new char[] { '{', '}' }).Split(':')[0];
            parameter.Value = value;
            DbType dbType = ColumnTypeUtil.GetDBType(value);
            parameter.DbType = dbType;
            return parameter;
        }
        #endregion

        #region Create SQL
        public string CreateGetMaxIdSql(string tableName, string key)
        {
            return string.Format("SELECT Max({0}) FROM {1}", key, tableName);
        }

        public string CreatePageSql(string sql, string orderby, int pageSize, int currentPage)
        {
            StringBuilder sb = new StringBuilder();
            int startRow = 0;
            int endRow = 0;

            #region 分页查询语句
            startRow = pageSize * (currentPage - 1);

            sb.Append("select * from (");
            sb.Append(sql);
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                sb.Append(" ");
                sb.Append(orderby);
            }
            sb.AppendFormat(" ) row_limit limit {0},{1}", startRow, pageSize);
            #endregion

            return sb.ToString();
        }
        #endregion

        #region 删除SQL语句模板
        /// <summary>
        /// 删除SQL语句模板 两个值分别对应 “delete from [表名] where [查询条件]”中的“delete from”和“where”
        /// </summary>
        public Tuple<string, string> CreateDeleteSqlTempldate()
        {
            return new Tuple<string, string>("alter table", "delete where");
        }
        #endregion

        #region 更新SQL语句模板
        /// <summary>
        /// 更新SQL语句模板 三个值分别对应 “update [表名] set [赋值语句] where [查询条件]”中的“update”、“set”和“where”
        /// </summary>
        public Tuple<string, string, string> CreateUpdateSqlTempldate()
        {
            return new Tuple<string, string, string>("alter table", "update", "where");
        }
        #endregion

        #region GetParameterName
        public string GetParameterName(string parameterName, Type parameterType)
        {
            return "{" + parameterName + ":" + ColumnTypeUtil.GetDBTypeName(parameterType) + "}";
        }
        #endregion

        #region For Lambda

        public SqlValue ForList(IList list)
        {
            string type = null;
            if (list.Count > 0)
            {
                object val = list[0];
                if (val != null)
                {
                    if (val.GetType() == typeof(string))
                    {
                        type = "String";
                    }
                    if (val.GetType() == typeof(int))
                    {
                        type = "Int32";
                    }
                    if (val.GetType() == typeof(int?))
                    {
                        type = "Int32";
                    }
                    if (val.GetType() == typeof(long))
                    {
                        type = "Int64";
                    }
                    if (val.GetType() == typeof(long?))
                    {
                        type = "Int64";
                    }
                }
                else
                {
                    throw new Exception("ForList集合项不能为null");
                }
            }
            else
            {
                throw new Exception("ForList集合不能为空");
            }

            List<string> argList = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                argList.Add("{inParam" + i + ":" + type + "}");
            }
            string args = string.Join(",", argList);

            return new SqlValue("(" + args + ")", list);
        }

        #endregion

    }
}
