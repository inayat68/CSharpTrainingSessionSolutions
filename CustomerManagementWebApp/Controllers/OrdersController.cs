using CustomerManagementWebApp.Data;
using CustomerManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SQLite;

namespace CustomerOrderMvc.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DbHelper _db;

        public OrdersController(DbHelper db)
        {
            _db = db;
        }

        // GET: /Orders
        public IActionResult Index(int? customerId)
        {
            string sql = """
                SELECT
                    o.OrderId,
                    o.CustomerId,
                    c.Name AS CustomerName,
                    o.OrderDate,
                    o.Product,
                    o.Quantity,
                    o.Amount
                FROM Orders o
                INNER JOIN Customers c
                    ON c.CustomerId = o.CustomerId
                """;

            List<SQLiteParameter> parameters = new();

            if (customerId.HasValue)
            {
                sql += """
                     WHERE o.CustomerId = @CustomerId
                    """;

                parameters.Add(new SQLiteParameter("@CustomerId", customerId.Value));
            }

            sql += """
                 ORDER BY o.OrderId DESC
                """;

            DataTable table = _db.ExecuteDataTable(sql, parameters.ToArray());

            List<Order> orders = new();

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Order
                {
                    OrderId = Convert.ToInt32(row["OrderId"]),

                    CustomerId = Convert.ToInt32(row["CustomerId"]),

                    CustomerName = row["CustomerName"].ToString()!,

                    OrderDate = DateTime.Parse(row["OrderDate"].ToString()!),

                    Product = row["Product"].ToString()!,

                    Quantity = Convert.ToInt32(row["Quantity"]),

                    Amount = Convert.ToDecimal(row["Amount"])
                });
            }

            return View(orders);
        }

        // GET: /Orders/Create
        public IActionResult Create()
        {
            LoadCustomers();

            return View(new Order
            {
                OrderDate = DateTime.Today,
                Quantity = 1
            });
        }

        // POST: /Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                LoadCustomers();
                return View(order);
            }

            string sql = """
                INSERT INTO Orders
                (
                    CustomerId,
                    OrderDate,
                    Product,
                    Quantity,
                    Amount
                )
                VALUES
                (
                    @CustomerId,
                    @OrderDate,
                    @Product,
                    @Quantity,
                    @Amount
                )
                """;

            _db.ExecuteNonQuery(sql,

                new SQLiteParameter("@CustomerId", order.CustomerId),

                new SQLiteParameter("@OrderDate", order.OrderDate.ToString("yyyy-MM-dd")),

                new SQLiteParameter("@Product", order.Product),

                new SQLiteParameter("@Quantity", order.Quantity),

                new SQLiteParameter("@Amount", order.Amount));

            return RedirectToAction(nameof(Index));
        }

        // GET: /Orders/Edit/5
        public IActionResult Edit(int id)
        {
            Order? order = GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            LoadCustomers();

            return View(order);
        }

        // POST: /Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                LoadCustomers();
                return View(order);
            }

            string sql = """
                UPDATE Orders
                SET
                    CustomerId = @CustomerId,
                    OrderDate = @OrderDate,
                    Product = @Product,
                    Quantity = @Quantity,
                    Amount = @Amount
                WHERE OrderId = @OrderId
                """;

            _db.ExecuteNonQuery(
                sql,

                new SQLiteParameter("@CustomerId", order.CustomerId),

                new SQLiteParameter("@OrderDate", order.OrderDate.ToString("yyyy-MM-dd")),

                new SQLiteParameter("@Product", order.Product),

                new SQLiteParameter("@Quantity", order.Quantity),

                new SQLiteParameter("@Amount", order.Amount),

                new SQLiteParameter("@OrderId", order.OrderId));

            return RedirectToAction(nameof(Index));
        }

        // GET: /Orders/Delete/5
        public IActionResult Delete(int id)
        {
            Order? order = GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: /Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            string sql = """
                DELETE FROM Orders
                WHERE OrderId = @OrderId
                """;

            _db.ExecuteNonQuery(sql, new SQLiteParameter("@OrderId", id));

            return RedirectToAction(nameof(Index));
        }

        private Order? GetOrder(int id)
        {
            string sql = """
                SELECT
                    o.OrderId,
                    o.CustomerId,
                    c.Name AS CustomerName,
                    o.OrderDate,
                    o.Product,
                    o.Quantity,
                    o.Amount
                FROM Orders o
                INNER JOIN Customers c
                    ON c.CustomerId = o.CustomerId
                WHERE o.OrderId = @OrderId
                """;

            DataTable table =
                _db.ExecuteDataTable(sql, new SQLiteParameter("@OrderId", id));

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];

            return new Order
            {
                OrderId = Convert.ToInt32(row["OrderId"]),

                CustomerId = Convert.ToInt32(row["CustomerId"]),

                CustomerName = row["CustomerName"].ToString()!,

                OrderDate = DateTime.Parse(row["OrderDate"].ToString()!),

                Product = row["Product"].ToString()!,

                Quantity = Convert.ToInt32(row["Quantity"]),

                Amount = Convert.ToDecimal(row["Amount"])
            };
        }

        private void LoadCustomers()
        {
            string sql = """
                SELECT
                    CustomerId,
                    Name
                FROM Customers
                ORDER BY Name
                """;

            DataTable table = _db.ExecuteDataTable(sql);

            List<SelectListItem> customers = new();

            foreach (DataRow row in table.Rows)
            {
                customers.Add(new SelectListItem
                {
                    Value = row["CustomerId"].ToString(),

                    Text = row["Name"].ToString()
                });
            }

            ViewBag.Customers = customers;
        }
    }
}