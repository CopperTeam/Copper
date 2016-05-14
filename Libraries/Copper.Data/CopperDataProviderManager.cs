using System;
using Copper.Core;
using Copper.Core.Data;

namespace Copper.Data
{
    public partial class CopperDataProviderManager : BaseDataProviderManager
    {
        public CopperDataProviderManager(DataSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new CopperException("Data Settings doesn't contain a providerName");

            switch (providerName)
            {
                case "System.Data.SqlClient":
                    return new SqlServerDataProvider();
                //case "System.Data.OracleClient":
                //    return new OracleDataProvider();
                //case "MySql.Data.MySqlClient":
                //    return new MySqlDataProvider();
                default:
                    throw new CopperException($"Not supported dataprovider name: {providerName}");
            }
        }
    }
}
