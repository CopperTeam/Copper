namespace Copper.Web.Framework.Mvc
{
    public class DataSourceRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DataSourceRequest()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
        }
    }
}
