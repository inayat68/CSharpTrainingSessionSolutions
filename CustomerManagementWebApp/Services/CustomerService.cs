using CustomerManagementWebApp.Data;
using CustomerManagementWebApp.Models;

namespace CustomerManagementWebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DbHelper _dbHelper;

        public CustomerService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                string sql = "SELECT Id, Name, Email FROM Customers";

                using (var command = _dbHelper.CreateCommand(connection, sql))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            Name = reader["Name"].ToString() ?? "",
                            Email = reader["Email"].ToString() ?? ""
                        });
                    }
                }
            }

            return customers;
        }
    }
}
