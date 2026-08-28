using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CustomerManagementWebApp.Data;


public class DbHelper
{
    private readonly string _connectionString;
    private readonly string[] _args;

    public DbHelper(DatabaseSettings databaseSettings, string[] args)
    {
        _connectionString = databaseSettings.ConnectionString;
        _args = args;
    }
    public SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(_connectionString);
    }

    public SQLiteCommand CreateCommand(SQLiteConnection connection, string sql)
    {
        return new SQLiteCommand(sql, connection);
    }

    public SQLiteDataAdapter CreateDataAdapter(SQLiteCommand command)
    {
        return new SQLiteDataAdapter(command);
    }

    public DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
    {
        using SQLiteConnection connection = GetConnection();

        using SQLiteCommand command = CreateCommand(connection, sql);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        using SQLiteDataAdapter adapter = CreateDataAdapter(command);

        DataTable table = new DataTable();

        adapter.Fill(table);

        return table;
    }

    public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
    {
        using SQLiteConnection connection = GetConnection();

        connection.Open();

        using SQLiteCommand command = CreateCommand(connection, sql);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        return command.ExecuteNonQuery();
    }

    public object? ExecuteScalar(string sql, params SQLiteParameter[] parameters)
    {
        using SQLiteConnection connection = GetConnection();

        connection.Open();

        using SQLiteCommand command = CreateCommand(connection, sql);
        command.CommandType = CommandType.Text;
        //command.CommandType = CommandType.StoredProcedure;

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        return command.ExecuteScalar();
    }

    public SQLiteDataReader ExecuteReader(SQLiteConnection connection, string sql, params SQLiteParameter[] parameters)
    {
        SQLiteCommand command = CreateCommand(connection, sql);

        if (parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }
}
