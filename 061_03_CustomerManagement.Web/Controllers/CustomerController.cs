using CustomerManagement.Core;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace CustomerManagement.Web.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerService _customerService;

    private readonly ConnectionStringSettings _connectionSettings;
    //private readonly ConnectionStringSetup __connectionSettings;

    public CustomerController(ConnectionStringSettings connectionSettings)
    {
        _customerService = new CustomerService();
        _connectionSettings = connectionSettings;
    }

    //public CustomerController(ConnectionStringSetup connectionSettings)
    //{
    //    _customerService = new CustomerService();
    //    __connectionSettings = connectionSettings;
    //}



    public IActionResult Index()
    {
        ViewBag.ConnectionString = _connectionSettings.ConnectionString;

        var customers = _customerService.GetCustomers();

        return View(customers);
    }
}
