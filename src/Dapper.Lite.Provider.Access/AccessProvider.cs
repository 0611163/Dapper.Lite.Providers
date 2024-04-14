using System.Data.Common;
using System.Data.OleDb;

namespace Dapper.Lite.Provider
{
    public class AccessProvider : AccessProviderBase, IDbProvider
    {
        #region 创建 DbConnection
        public override DbConnection CreateConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }
        #endregion

        #region 生成 DbParameter
        public override DbParameter GetDbParameter(string name, object value)
        {
            return new OleDbParameter(name, value);
        }
        #endregion

    }
}
