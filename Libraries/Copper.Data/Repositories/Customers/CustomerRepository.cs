using System;
using System.Collections.Generic;
using System.Linq;
using Copper.Core.Domain.Customers;
using Dapper;

namespace Copper.Data.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Fields

        private readonly ICopperDbConnection _copperDbConnection;

        #endregion

        #region Ctor

        public CustomerRepository(ICopperDbConnection copperDbConnection)
        {
            _copperDbConnection = copperDbConnection;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods
        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>customer</returns>
        public Customer GetById(int id)
        {
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                var customer = connection.Query<Customer>(@"SELECT [Id],[CustomerGuid] ,[Username] ,[Email] ,[Password] from [Customer] WHERE Id=@Id", new { Id = id }).First();
                return customer;
            }
        }

        /// <summary>
        /// Insert customer
        /// </summary>
        /// <param name="customer">customer</param>
        public void Insert(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                connection.Execute(@"INSERT INTO [Customer]([CustomerGuid],[Username],[Email],[Password]) VALUES(@CustomerGuid,@Username,@Email,@Password)", new { CustomerGuid = customer.CustomerGuid, Username = customer.Username, Email = customer.Email, Password = customer.Password });
            }
        }

        /// <summary>
        /// Insert customers
        /// </summary>
        /// <param name="customers">customers</param>
        public void Insert(IEnumerable<Customer> customers)
        {
            var customerArray = customers as Customer[] ?? customers.ToArray();
            if (customers == null || !customerArray.Any())
            {
                return;
            }
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                var customerParameterObjects = customerArray.Select(c => new { CustomerGuid = c.CustomerGuid, Username = c.Username, Email = c.Email, Password = c.Password });
                connection.Execute(@"INSERT INTO [Customer]([CustomerGuid],[Username],[Email],[Password]) VALUES(@CustomerGuid,@Username,@Email,@Password)", customerParameterObjects);
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer">customer</param>
        public void Update(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                connection.Execute(@"UPDATE [Customer] set [CustomerGuid]=@CustomerGuid, [Username]=@Username, [Email]=@Email ,[Password]=@Password WHERE Id=@Id", new { CustomerGuid = customer.CustomerGuid, Username = customer.Username, Email = customer.Email, Password = customer.Password, Id = customer.Id });
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void Delete(int id)
        {
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                connection.Execute(@"DELETE FROM  [Customer] WHERE Id=@Id", new { Id = id });
            }
        }


        /// <summary>
        /// Deletes the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public void Delete(int[] ids)
        {
            using (var connection = _copperDbConnection.GetOpenedDbConenction())
            {
                connection.Execute(@"DELETE FROM  [Customer] WHERE Id in @Ids", new { Ids = ids });
            }
        }
        #endregion

    }
}
