using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copper.Data
{
    public class CopperDbConnection : ICopperDbConnection
    {
        #region Fields
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _dbConnectionString;

        /// <summary>
        /// 数据库提供工厂
        /// </summary>
        private readonly DbProviderFactory _dbProvider;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CopperDbConnection"/> class.
        /// </summary>
        /// <param name="providerString">The provider string.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CopperDbConnection(string providerString, string connectionString)
        {
            if (string.IsNullOrEmpty(providerString))
            {
                throw new ArgumentNullException(nameof(providerString));
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _dbProvider = DbProviderFactories.GetFactory(providerString);
            _dbConnectionString = connectionString;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods
        public DbConnection GetOpenedDbConenction()
        {
            var dbConnection = _dbProvider.CreateConnection();
            if (dbConnection == null) return null;
            dbConnection.ConnectionString = _dbConnectionString;
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            return dbConnection;
        }
        #endregion

    }
}
