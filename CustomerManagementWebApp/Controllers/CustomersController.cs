
using CustomerManagementWebApp.Data;
using CustomerManagementWebApp.Models;
using CustomerManagementWebApp.Data;
using CustomerManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using CustomerManagementWebApp.Services;

namespace CustomerManagementWebApp.Controllers;

public class CustomersController : Controller
{
    private readonly DbHelper _db;
    private readonly ICustomerService _customerService;

    public CustomersController(DbHelper db, ICustomerService customerService, IConfiguration conf)
    {
        _db = db;
        _customerService = customerService;
    }

    public IActionResult Index2()
    {
        var customers = _customerService.GetCustomers();

        return View(customers);
    }

    // GET: /Customers
    public IActionResult Index()
    {
        string sql = """
                SELECT
                    CustomerId,
                    Name,
                    Email,
                    Phone
                FROM Customers
                ORDER BY Name
                """;

        DataTable table = _db.ExecuteDataTable(sql);

        List<Customer> customers = new();

        foreach (DataRow row in table.Rows)
        {
            customers.Add(new Customer
            {
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                Name = row["Name"].ToString()!,
                Email = row["Email"].ToString()!,
                Phone = row["Phone"]?.ToString() ?? ""
            });
        }

        return View(customers);
    }

    // GET: /Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        string sql = """
                INSERT INTO Customers
                    (Name, Email, Phone)
                VALUES
                    (@Name, @Email, @Phone)
                """;

        _db.ExecuteNonQuery(
            sql,
            new SQLiteParameter("@Name", customer.Name),
            new SQLiteParameter("@Email", customer.Email),
            new SQLiteParameter("@Phone", customer.Phone));

        return RedirectToAction(nameof(Index));
    }

    // GET: /Customers/Edit/5
    public IActionResult Edit(int id)
    {
        Customer? customer = GetCustomer(id);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: /Customers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        int id,
        Customer customer)
    {
        if (id != customer.CustomerId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        string sql = """
                UPDATE Customers
                SET
                    Name = @Name,
                    Email = @Email,
                    Phone = @Phone
                WHERE CustomerId = @CustomerId
                """;

        _db.ExecuteNonQuery(
            sql,
            new SQLiteParameter("@Name", customer.Name),
            new SQLiteParameter("@Email", customer.Email),
            new SQLiteParameter("@Phone", customer.Phone),
            new SQLiteParameter("@CustomerId", customer.CustomerId));

        return RedirectToAction(nameof(Index));
    }

    // GET: /Customers/Delete/5
    public IActionResult Delete(int id)
    {
        Customer? customer = GetCustomer(id);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: /Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        string sql = """
                DELETE FROM Customers
                WHERE CustomerId = @CustomerId
                """;

        _db.ExecuteNonQuery(sql, new SQLiteParameter("@CustomerId", id));

        return RedirectToAction(nameof(Index));
    }

    private Customer? GetCustomer(int id)
    {
        string sql = """
                SELECT
                    CustomerId,
                    Name,
                    Email,
                    Phone
                FROM Customers
                WHERE CustomerId = @CustomerId
                """;

        DataTable table = _db.ExecuteDataTable(sql, new SQLiteParameter("@CustomerId", id));

        if (table.Rows.Count == 0)
        {
            return null;
        }

        DataRow row = table.Rows[0];

        return new Customer
        {
            CustomerId = Convert.ToInt32(row["CustomerId"]),

            Name = row["Name"].ToString()!,

            Email = row["Email"].ToString()!,

            Phone = row["Phone"]?.ToString() ?? ""
        };
    }
}