using Microsoft.Data.SqlClient;
using System.Data;
//dotnet add package microsoft.data.sqlclient --source https://api.nuget.org/v3/index.json

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
            "Server=localhost,1433;" +              //Data Source / Address / Addr / Network Address
            "Database=BookCatalog;" +               //Initial Catalog
            "User Id=sa;Password=Strong@12345;" +   //UID,User,UserId   /   pwd     : pwd='Strong;123';
                                                    //"Integrated Security=True;" +        //Trusted_Connection
            "TrustServerCertificate=True;";         //-

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = "localhost,1433",
            InitialCatalog = "BookCatalog",
            UserID = "sa",
            Password = "Strong@12;345",
            TrustServerCertificate = true
        };

        string connectionStringTest = builder.ConnectionString;

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);

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

            using (SqlCommand command = new SqlCommand(createTableSql, connection))
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
            int rows = 0;
            using (SqlCommand command = new SqlCommand(insertSql, connection))
            {
                command.Parameters.AddWithValue("@EmployeeName", "Sam");
                command.Parameters.AddWithValue("@Department", "IT");
                command.Parameters.AddWithValue("@Salary $", 15000);

                rows = command.ExecuteNonQuery();

                Console.WriteLine($"Row-1 inserted: {rows}");

                //Immediate Window: Debug > Window > Immediate (Ctrl+Alt+I)
                //command.Parameters["@EmployeeName"].Value = "Ahmed";

                command.Parameters.AddWithValue("@EmployeeName", "Peter");
                command.Parameters.AddWithValue("@Department", "Finance");
                command.Parameters.AddWithValue("@Salary $", 11000);

                rows = command.ExecuteNonQuery();

                Console.WriteLine($"Row-2 inserted: {rows}");

            }

            // --------------------------------------------------
            // SELECT Rows 1 by 1
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

            using (SqlCommand command = new SqlCommand(selectSql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string department = reader.GetString(2);
                        decimal salary = reader.GetDecimal(3);

                        Console.WriteLine($"{id} | {name} | " + $"{department} | {salary:N2}");
                    }
                }
            }

            //------------------------------------------------------------
            // SELECT ALL Rows
            //------------------------------------------------------------

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = selectSql;
                command.CommandType = CommandType.Text;
                //command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                // Create DataAdapter using the existing SqlCommand
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    // Create DataTable
                    DataTable table = new DataTable();

                    // Fill DataTable from database
                    adapter.Fill(table);

                    // ----------------------------------------------------
                    // Iterate through DataTable
                    // ----------------------------------------------------
                    foreach (DataRow row in table.Rows)
                    {
                        int id = Convert.ToInt32(row["EmployeeId"]);
                        string? name = row["EmployeeName"].ToString();
                        string? department = row["Department"].ToString();
                        decimal salary = Convert.ToDecimal(row["Salary"]);

                        Console.WriteLine($"{id} | {name} | {department} | {salary:N2}");
                    }
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