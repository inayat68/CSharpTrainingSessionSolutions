using System.Data.SqlClient;

namespace SqlServer;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       4. SQL Server Demonstration");
        Console.WriteLine("========================================");

        // --------------------------------------------------
        // Change this connection string
        // --------------------------------------------------

        string connectionString =
            "Server=localhost;" +
            "Database=TrainingDb;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";

        try
        {
            using SqlConnection connection =
                new SqlConnection(connectionString);

            Console.WriteLine();
            Console.WriteLine("Connecting to SQL Server...");

            connection.Open();

            Console.WriteLine("Connected successfully.");

            // --------------------------------------------------
            // Create table
            // --------------------------------------------------

            string createTableSql = """
                IF OBJECT_ID('Employees', 'U') IS NULL
                BEGIN
                    CREATE TABLE Employees
                    (
                        EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
                        EmployeeName NVARCHAR(100) NOT NULL,
                        Department NVARCHAR(100) NOT NULL,
                        Salary DECIMAL(18,2) NOT NULL
                    )
                END
                """;

            using (SqlCommand command =
                   new SqlCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Employees table is ready.");

            // --------------------------------------------------
            // INSERT
            // --------------------------------------------------

            string insertSql = """
                INSERT INTO Employees
                (
                    EmployeeName,
                    Department,
                    Salary
                )
                VALUES
                (
                    @EmployeeName,
                    @Department,
                    @Salary
                )
                """;

            using (SqlCommand command =
                   new SqlCommand(insertSql, connection))
            {
                command.Parameters.AddWithValue(
                    "@EmployeeName",
                    "Ali");

                command.Parameters.AddWithValue(
                    "@Department",
                    "IT");

                command.Parameters.AddWithValue(
                    "@Salary",
                    150000);

                int rows =
                    command.ExecuteNonQuery();

                Console.WriteLine(
                    $"Rows inserted: {rows}");
            }

            // --------------------------------------------------
            // SELECT
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("Employees");
            Console.WriteLine("--------------------------------");

            string selectSql = """
                SELECT
                    EmployeeId,
                    EmployeeName,
                    Department,
                    Salary
                FROM Employees
                ORDER BY EmployeeId
                """;

            using (SqlCommand command =
                   new SqlCommand(selectSql, connection))
            using (SqlDataReader reader =
                   command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id =
                        reader.GetInt32(0);

                    string name =
                        reader.GetString(1);

                    string department =
                        reader.GetString(2);

                    decimal salary =
                        reader.GetDecimal(3);

                    Console.WriteLine(
                        $"{id} | {name} | " +
                        $"{department} | {salary:N2}");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine();
            Console.WriteLine("SQL Server Error:");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("Error:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}