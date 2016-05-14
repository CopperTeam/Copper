using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Copper.Core.Data;
using Copper.Core.Infrastructure;
using static System.String;

namespace Copper.Web.Controllers
{
    public class InstallController : Controller
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Utilities
        /// <summary>
        /// SQLs the server database exists.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool SqlServerDatabaseExists(string connectionString)
        {
            try
            {
                //just try to connect
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the SQL server database.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="collation">The collation.</param>
        /// <returns>System.String.</returns>
        private string CreateSqlServerDatabase(string connectionString, string collation)
        {
            try
            {
                //parse database name
                var builder = new SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;
                //now create connection string to 'master' dabatase. It always exists.
                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = $"CREATE DATABASE [{databaseName}]";
                if (!IsNullOrWhiteSpace(collation))
                    query = $"{query} COLLATE {collation}";
                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            const string dataProvider = "System.Data.SqlClient";
            const string connectionString = @"Data Source=.\BOBSQLSERVER;Initial Catalog=Copper;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=123";
            if (!SqlServerDatabaseExists(connectionString))
            {
                //create database
                var errorCreatingDatabase = CreateSqlServerDatabase(connectionString, "");
                if (!IsNullOrEmpty(errorCreatingDatabase))
                    throw new Exception(errorCreatingDatabase);

            }
            var settingsManager = new DataSettingsManager();
            //数据库设置
            var settings = new DataSettings
            {
                DataProvider = dataProvider,
                DataConnectionString = connectionString
            };
            settingsManager.SaveSettings(settings);
            //初始化数据库提供者（SqlServer,Oracle）
            var dataProviderInstance = EngineContext.Current.Resolve<BaseDataProviderManager>().LoadDataProvider();
            dataProviderInstance.InitDatabase();
            //重置缓存
            DataSettingsHelper.ResetCache();
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        #endregion

    }
}