using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Hosting;
using Copper.Core.Data;
using Copper.Core.Infrastructure;
using Dapper;

namespace Copper.Data
{
    public class SqlServerDataProvider : IDataProvider
    {
        #region Utilities

        protected virtual string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException($"Specified file doesn't exist - {filePath}");

                return new string[0];
            }

            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                string statement;
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }

        protected virtual string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();

                    return null;
                }
                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;
                sb.Append(lineOfText + Environment.NewLine);
            }
            return sb.ToString();
        }

        #endregion

        #region Methods


        /// <summary>
        /// Initialize database
        /// </summary>
        public virtual void InitDatabase()
        {
            var customCommands = new List<string>();
            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            customCommands.AddRange(ParseCommands(HostingEnvironment.MapPath("~/App_Data/Install/SqlServer.Tables.sql"), false));

            var copperDbConnection = EngineContext.Current.Resolve<ICopperDbConnection>();
            using (var connection = copperDbConnection.GetOpenedDbConenction())
            {
                foreach (var customCommand in customCommands)
                {
                    connection.Execute(customCommand);
                }
            }
        }


        /// <summary>
        /// A value indicating whether this data provider supports stored procedures
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a support database parameter object (used by stored procedures)
        /// </summary>
        /// <returns>Parameter</returns>
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        #endregion
    }
}
