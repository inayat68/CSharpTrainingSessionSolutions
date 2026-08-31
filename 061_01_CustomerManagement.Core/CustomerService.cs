namespace CustomerManagement.Core;

public class CustomerService
{
    private readonly List<Customer> _customers =
    [
        new Customer
        {
            Id = 1,
            Name = "Ali Khan",
            Email = "ali@example.com"
        },
        new Customer
        {
            Id = 2,
            Name = "Ahmed Raza",
            Email = "ahmed@example.com"
        },
        new Customer
        {
            Id = 3,
            Name = "Sara Ahmed",
            Email = "sara@example.com"
        }
    ];

    public List<Customer> GetCustomers()
    {
        return _customers;
    }

    public Customer? GetCustomerById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }
}
