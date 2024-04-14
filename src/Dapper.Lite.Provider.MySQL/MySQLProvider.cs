using MySqlConnector;
using System.Data.Common;

namespace Dapper.Lite.Provider
{
    public class MySQLProvider : MySQLProviderBase, IDbProvider
    {
        public static readonly string ReturnIdSQL = "select @@IDENTITY";

        #region 创建 DbConnection
        public override DbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
        #endregion

        #region 生成 DbParameter
        public override DbParameter GetDbParameter(string name, object value)
        {
            return new MySqlParameter(name, value);
        }
        #endregion

    }
}
