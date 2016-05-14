using System.Data.Common;

namespace Copper.Data
{
    public interface ICopperDbConnection
    {
        /// <summary>
        /// Gets the database conenction.
        /// </summary>
        /// <returns>System.String.</returns>
        DbConnection GetOpenedDbConenction();

    }
}
