using CustomerManagementWebApp.Models;

namespace CustomerManagementWebApp.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
    }
}
