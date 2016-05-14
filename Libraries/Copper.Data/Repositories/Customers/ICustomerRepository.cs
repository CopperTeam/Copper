using System.Collections.Generic;
using Copper.Core.Domain.Customers;

namespace Copper.Data.Repositories.Customers
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>customer</returns>
        Customer GetById(int id);

        /// <summary>
        /// Insert customer
        /// </summary>
        /// <param name="customer">customer</param>
        void Insert(Customer customer);

        /// <summary>
        /// Insert customers
        /// </summary>
        /// <param name="customers">customers</param>
        void Insert(IEnumerable<Customer> customers);

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer">customer</param>
        void Update(Customer customer);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        void Delete(int id);

        /// <summary>
        /// Deletes the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        void Delete(int[] ids);
    }
}
