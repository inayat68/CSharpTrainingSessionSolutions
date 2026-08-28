using CustomerManagementWebApp.Data;
using System.Data.SQLite;

namespace CustomerManagementWebApp.Database;

public static class DatabaseInitializer
{

    public static void CreateTables(DatabaseSettings databaseSettings)
    {
        string connectionString = databaseSettings.ConnectionString;

        using SQLiteConnection connection = new SQLiteConnection(connectionString);

        try
        {
            connection.Open();

            // Force exception for testing
            //throw new Exception("TEST: Database connection exception.");

            using SQLiteCommand command = connection.CreateCommand();

            command.CommandText = """
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Customers
                (
                    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name       TEXT NOT NULL,
                    Email      TEXT NOT NULL,
                    Phone      TEXT
                );

                CREATE TABLE IF NOT EXISTS Orders
                (
                    OrderId    INTEGER PRIMARY KEY AUTOINCREMENT,
                    CustomerId INTEGER NOT NULL,
                    OrderDate  TEXT NOT NULL,
                    Product    TEXT NOT NULL,
                    Quantity   INTEGER NOT NULL,
                    Amount     NUMERIC NOT NULL,

                    CONSTRAINT FK_Orders_Customers
                        FOREIGN KEY (CustomerId)
                        REFERENCES Customers(CustomerId)
                        ON DELETE CASCADE
                );
                """;

            command.ExecuteNonQuery();
        }
        catch (SQLiteException ex)
        {
            // Handle SQLite-specific error
            Console.WriteLine($"SQLite database error: {ex.Message}");

            throw;
        }
        catch (Exception ex)
        {
            // Handle any other error
            Console.WriteLine($"Database initialization error: {ex.Message}");

            throw;
        }

        //////////////////////////////////////////////

        using SQLiteCommand commandSeed = connection.CreateCommand();


        // Check whether sample customers already exist
        commandSeed.CommandText = "SELECT COUNT(*) FROM Customers";

        long customerCount = Convert.ToInt64(commandSeed.ExecuteScalar());

        if (customerCount > 0)
        {
            // Data already exists.
            return;
        }


        // -------------------------------------------------
        // Insert Customers
        // -------------------------------------------------

        commandSeed.CommandText = """
                INSERT INTO Customers (Name, Email, Phone)
                VALUES
                    ('Ali Khan', 'ali@example.com', '0300-1111111'),
                    ('Ahmed Raza', 'ahmed@example.com', '0300-2222222'),
                    ('Sara Ahmed', 'sara@example.com', '0300-3333333');
                """;

        commandSeed.ExecuteNonQuery();


        // -------------------------------------------------
        // Insert Orders
        // -------------------------------------------------

        commandSeed.CommandText = """
                INSERT INTO Orders
                    (CustomerId, OrderDate, Product, Quantity, Amount)
                VALUES

                    -- Ali Khan - 2 orders
                    (1, '2026-08-15', 'Laptop', 1, 125000),
                    (1, '2026-08-16', 'Wireless Mouse', 2, 5000),

                    -- Ahmed Raza - 1 order
                    (2, '2026-08-17', 'Keyboard', 1, 8500),

                    -- Sara Ahmed - 2 orders
                    (3, '2026-08-18', 'Monitor', 1, 45000),
                    (3, '2026-08-19', 'USB-C Cable', 3, 4500);
                """;

        commandSeed.ExecuteNonQuery();
    }

}
