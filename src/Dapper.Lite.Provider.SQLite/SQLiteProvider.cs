using System.Data.Common;
using System.Data.SQLite;

namespace Dapper.Lite.Provider
{
    public class SQLiteProvider : SQLiteProviderBase, IDbProvider
    {
        #region 创建 DbConnection
        public override DbConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
        #endregion

        #region 生成 DbParameter
        public override DbParameter GetDbParameter(string name, object value)
        {
            return new SQLiteParameter(name, value);
        }
        #endregion

    }
}
