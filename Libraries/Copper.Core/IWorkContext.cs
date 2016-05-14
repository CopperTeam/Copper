using Copper.Core.Domain.Customers;
using Copper.Core.Domain.Localization;

namespace Copper.Core
{
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        Customer CurrentCustomer { get; set; }
        /// <summary>
        /// Get or set current user working language
        /// </summary>
        Language WorkingLanguage { get; set; }
      
    }
}
