using CustomerManagement.Core;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace CustomerManagement.Web.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerService _customerService;
    private readonly ConnectionStringSetup _connectionSettings;

    public CustomerController(ConnectionStringSetup connectionSettings)
    {
        _customerService = new CustomerService();
        _connectionSettings = connectionSettings;
    }

    public IActionResult Index()
    {
        ViewBag.ConnectionString =
            _connectionSettings.ConnectionString;

        var customers = _customerService.GetCustomers();

        return View(customers);
    }
}
