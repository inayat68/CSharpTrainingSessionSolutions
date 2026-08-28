using OllamaDemo.Model;
using System.Diagnostics;

var client = new OllamaClient();

Console.WriteLine("Sending request to Ollama...");

var stopwatch = Stopwatch.StartNew();

// Display elapsed time while waiting
// Task.Run() starts this timer code as a separate background Task,
// allowing the main program to continue running without waiting.
// The async/await inside the Task allows the timer to wait 1 second
// without blocking the thread.
var timerTask = Task.Run(async () =>
{
    while (stopwatch.IsRunning)
    {
        Console.Write($"\rElapsed Time: {stopwatch.Elapsed:hh\\:mm\\:ss}");
        await Task.Delay(1000);
    }
});

//Source Database Name and Documentation Url
string SourceDatabase = "Microsoft SQL Server";
string SourceDatabaseDocUrl = "https://learn.microsoft.com/en-us/sql/t-sql/queries/queries";

//Target Database Dame and Documentation Url
string TargetDatabase = "PostgreSQL";
string TargetDatabaseDocUrl = "https://www.postgresql.org/docs/18/index.html";

//Source Query
string StarMSourceQuery = "SELECT TOP 10 Name, Email, GetDate() FROM Employees Order By EmployeeId";
string StarMSourceQuery2 = @"SELECT TOP 10
                                e.EmployeeId,
                                e.FirstName + ' ' + e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                GETDATE() AS CurrentDate,
                                YEAR(e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC
                            ";
string StarMSourceQuery3 = @"
WITH EmployeeSales AS
(
    SELECT
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.DepartmentID,
        d.DepartmentName,

        COUNT(o.OrderID) AS TotalOrders,

        SUM(od.Quantity * od.UnitPrice) AS TotalSales,

        AVG(od.UnitPrice) AS AvgUnitPrice,

        MIN(o.OrderDate) AS FirstOrderDate,

        MAX(o.OrderDate) AS LastOrderDate

    FROM Employees e

    INNER JOIN Departments d
        ON e.DepartmentID = d.DepartmentID

    LEFT JOIN Orders o
        ON e.EmployeeID = o.EmployeeID

    LEFT JOIN OrderDetails od
        ON o.OrderID = od.OrderID

    WHERE
        o.OrderDate >= DATEADD(YEAR, -1, GETDATE())

    GROUP BY
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.DepartmentID,
        d.DepartmentName

    HAVING
        SUM(od.Quantity * od.UnitPrice) > 5000
)

SELECT TOP (20)

    ROW_NUMBER() OVER
    (
        ORDER BY es.TotalSales DESC
    ) AS RowNo,

    es.EmployeeID,

    es.FirstName + ' ' + es.LastName AS EmployeeName,

    es.DepartmentName,

    ISNULL(es.TotalOrders, 0) AS TotalOrders,

    CAST(es.TotalSales AS DECIMAL(18,2)) AS TotalSales,

    CASE
        WHEN es.TotalSales >= 50000 THEN 'Platinum'
        WHEN es.TotalSales >= 25000 THEN 'Gold'
        WHEN es.TotalSales >= 10000 THEN 'Silver'
        ELSE 'Bronze'
    END AS SalesCategory,

    DATEDIFF(
        DAY,
        es.LastOrderDate,
        GETDATE()
    ) AS DaysSinceLastOrder,

    m.FirstName + ' ' + m.LastName AS ManagerName

FROM EmployeeSales es

INNER JOIN Employees e
    ON es.EmployeeID = e.EmployeeID

LEFT JOIN Employees m
    ON e.ManagerID = m.EmployeeID

WHERE
    es.DepartmentName LIKE 'S%'

    AND EXISTS
    (
        SELECT 1
        FROM Bonuses b

        WHERE
            b.EmployeeID = es.EmployeeID
            AND b.BonusYear = YEAR(GETDATE())
    )

ORDER BY
    TotalSales DESC,
    EmployeeName;
";

//Target Query
string StarMTargetQuery = "SELECT Name, Email, Now() FROM Employees LIMIT 10;";
string StarMTargetQuery2 = @"SELECT
                                e.EmployeeId,
                                e.FirstName || ' ' || e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                CURRENT_TIMESTAMP AS CurrentDate,
                                EXTRACT(YEAR FROM e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC
                            LIMIT 10;";
string StarMTargetQuery3 = @"
WITH EmployeeSales AS
(
    SELECT
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.DepartmentID,
        d.DepartmentName,

        COUNT(o.OrderID) AS TotalOrders,

        SUM(od.Quantity * od.UnitPrice) AS TotalSales,

        AVG(od.UnitPrice) AS AvgUnitPrice,

        MIN(o.OrderDate) AS FirstOrderDate,

        MAX(o.OrderDate) AS LastOrderDate

    FROM Employees e

    INNER JOIN Departments d
        ON e.DepartmentID = d.DepartmentID

    LEFT JOIN Orders o
        ON e.EmployeeID = o.EmployeeID

    LEFT JOIN OrderDetails od
        ON o.OrderID = od.OrderID

    WHERE
        o.OrderDate >= CURRENT_TIMESTAMP - INTERVAL '1 year'

    GROUP BY
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.DepartmentID,
        d.DepartmentName

    HAVING
        SUM(od.Quantity * od.UnitPrice) > 5000
)

SELECT

    ROW_NUMBER() OVER
    (
        ORDER BY es.TotalSales DESC
    ) AS RowNo,

    es.EmployeeID,

    CONCAT(es.FirstName, ' ', es.LastName) AS EmployeeName,

    es.DepartmentName,

    COALESCE(es.TotalOrders, 0) AS TotalOrders,

    CAST(es.TotalSales AS DECIMAL(18,2)) AS TotalSales,

    CASE
        WHEN es.TotalSales >= 50000 THEN 'Platinum'
        WHEN es.TotalSales >= 25000 THEN 'Gold'
        WHEN es.TotalSales >= 10000 THEN 'Silver'
        ELSE 'Bronze'
    END AS SalesCategory,

    CURRENT_DATE - es.LastOrderDate::DATE
        AS DaysSinceLastOrder,

    CONCAT(m.FirstName, ' ', m.LastName) AS ManagerName

FROM EmployeeSales es

INNER JOIN Employees e
    ON es.EmployeeID = e.EmployeeID

LEFT JOIN Employees m
    ON e.ManagerID = m.EmployeeID

WHERE
    es.DepartmentName LIKE 'S%'

    AND EXISTS
    (
        SELECT 1
        FROM Bonuses b

        WHERE
            b.EmployeeID = es.EmployeeID
            AND b.BonusYear = EXTRACT(
                YEAR FROM CURRENT_TIMESTAMP
            )
    )

ORDER BY
    TotalSales DESC,
    EmployeeName

LIMIT 20;
    ";
//CONCAT(es.FirstName,' ',es.LastName) AS EmployeeName,
string Directions = "Check the conversion.";

//3. Rigorously compare your AI Translated Query against the provided Target Query to identify defects, semantic mismatches, missing logic, unsupported syntax, or incorrect conversions in the Target Query.
//I am going to check AI can identify the incorrect translation of function 'ISNULL(e.Salary, 0) AS Salary,' is reamin same and not converted to 'COALESCE(e.Salary, 0) AS Salary,' in target Database?
string strPrompt = @$"
You are an expert SQL translation verification assistant specializing in database SQL migration and cross-database SQL compatibility.

I will provide the following six inputs:

1. Source Database Name and Documentation Url to follow in Query Parsing: 
+{SourceDatabase}
+{SourceDatabaseDocUrl}

2. Target Database Name and Documentation Url to follow in Query Parsing: 
+{TargetDatabase}
+{TargetDatabaseDocUrl}

3. Source SQL Query:
{StarMSourceQuery}

4. Provided Target SQL Query:
{StarMTargetQuery}

Tasks to Perform:
1.Analyze the Source SQL Query independently.
2.Translate the Source SQL Query into the Target Database syntax.
3.Generate your own AI Translated Query based solely on the Source SQL Query.
4.Do not use, copy, modify, or rely on the Provided Target SQL Query while generating the AI Translated Query.
5.After independently generating the AI Translated Query, compare it with the Provided Target SQL Query.
6.Identify only the incorrect differences in the Provided Target SQL Query.
7.Do not report correct, equivalent, valid alternative, formatting, whitespace, capitalization, or style differences.
8.Focus on syntax, semantic behavior, database-specific functions, operators, data types, clauses, joins, expressions, and overall functional equivalence.
9.If a difference depends on the underlying database schema or column data type, clearly mention that dependency.
10.Determine whether the Provided Target SQL Query is functionally equivalent to the Source SQL Query.
11.If the Provided Target SQL Query contains errors, identify each error and provide the correct PostgreSQL syntax or expression.

Return your response EXACTLY in the following format with all 5 sections:

1. Source Database Name
{SourceDatabase}

2. Target Database Name
{TargetDatabase}

3. Source SQL Query
{StarMSourceQuery}

4. Provided Target SQL Query
{StarMTargetQuery}

5. AI Translated Query
...

6. Incorrect Differences List
...


";

string strPrompt2 = @$"
You are an expert database migration specialist with deep knowledge of SQL dialects, query optimization, and cross-platform compatibility. Your task is to rigorously identify and list down the issues in translaton between source and target queries. You must also generate your own translated query from {SourceDatabase} to {TargetDatabase}, then compare your generated query with the provided target query to identify issues that the given target query failed to handle correctly.

Task:
{Directions}

Source Database:
{SourceDatabase}

Target Database:
{TargetDatabase}

Source Query:
{StarMSourceQuery}

Target Query:
{StarMTargetQuery}

AI Query:
Generate your Translated {SourceDatabase} Query using documentation Url {SourceDatabaseDocUrl} to {TargetDatabaseDocUrl} Query using documentation Url {TargetDatabaseDocUrl}:

Instructions:
1. Compare the given source and target queries intelligently to identify the correctness of translation.
2. Verify that {SourceDatabase} query syntax, keywords, clauses, operators, and overall structure are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
3. Verify that {SourceDatabase} functions, operators, expressions, and built-in functions are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
4. Verify that {SourceDatabase} query schemas, tables, views, columns, aliases, identifiers, and reserved keywords are correctly mapped and correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
5. Verify that {SourceDatabase} query joins and relationships are correctly converted, including INNER JOIN, LEFT JOIN, RIGHT JOIN, FULL JOIN, CROSS JOIN, join conditions, and join behavior are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
6. Verify that {SourceDatabase} query filtering and conditional logic is preserved, including WHERE, HAVING, CASE, AND, OR, NOT, IN, EXISTS, BETWEEN, LIKE, NULL handling, and related conditions are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
7. Verify that {SourceDatabase} query aggregation, grouping, sorting, pagination, and analytical logic is correctly converted, including GROUP BY, HAVING, ORDER BY, TOP, LIMIT, OFFSET, DISTINCT, aggregate functions, and window functions are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
8. Verify that {SourceDatabase} query CTEs, subqueries, correlated subqueries, EXISTS/NOT EXISTS, UNION, UNION ALL, INTERSECT, EXCEPT, and other nested query structures are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
9. Verify that {SourceDatabase} query data types, CAST/CONVERT operations, numeric precision, string handling, date/time operations, Boolean values, NULL behavior, parameters, variables, and implicit conversions are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
10. Verify that {SourceDatabase} query preserves the same result set, filtering, joins, calculations, sorting, aggregation, NULL behavior, date/time behavior, and overall business logic as the source query are correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
11. Verify that {SourceDatabase} query contains no remaining unsupported source-database syntax, functions, keywords, or features, and is fully executable in {TargetDatabase}. Identify, explain, classify, and correct any such issues correctly converted to {TargetDatabase}. List down any issue(s) found for Point-D below.
12. Identify any missing, incorrect, or incompatible conversions.
13. Explain each issue found with sufficient detail to enable correction.
14. If the conversion is entirely correct, explicitly state that no issues were found.

Return your response EXACTLY in the following format with all 5 sections:

**Point-A. Write Original Source Query**:
{StarMSourceQuery}
---------------------

**Point-B. Write Original Target Query**:
{StarMTargetQuery}
---------------------

**Point-C. Generate your Translated {SourceDatabase} Query to {TargetDatabase} Query**:
AI Query
...
---------------------

**Point-D. Overall Status**:
Compare Target and AI Queries. If they are similar and no issue(s) in translation then write 'Correct' otherwise 'Incorrect'.
...
---------------------

**Point-E. Issue(s) Found (if any)**:
Compare Target and AI Queries. If they are similar and no issue(s) in translation then write 'The converted SQL query is accurate' otherwise mention the differences in the form of points to elaborate the differences/issues between them.
...
---------------------
";

string strPrompt3 = @$"
You are an expert database migration specialist.

Task:
{Directions}

Source Database:
{SourceDatabase}

Target Database:
{TargetDatabase}

Source Query:
{StarMSourceQuery}

Converted Query:
{StarMTargetQuery}

Instructions:
1. Compare the given source and target queries intelligently to identify the correctness of translation.
2. Verify that the converted query syntax, keywords, clauses, operators, and overall structure are valid for {TargetDatabase}. Report issue if any.
3. Verify that all source-database-specific functions like ISNULL() to COALESCE() checking, operators, expressions, and built-in functions are correctly converted to their {TargetDatabase} equivalents. Report issue(s) if any.
4. Verify that all databases, schemas, tables, views, columns, aliases, identifiers, and reserved keywords are correctly mapped and valid in {TargetDatabase}. Report issue(s) if any.
5. Verify that all joins and relationships are correctly converted, including INNER JOIN, LEFT JOIN, RIGHT JOIN, FULL JOIN, CROSS JOIN, join conditions, and join behavior. Report issue(s) if any.
6. Verify that filtering and conditional logic is preserved, including WHERE, HAVING, CASE, AND, OR, NOT, IN, EXISTS, BETWEEN, LIKE, NULL handling, and related conditions. Report issue(s) if any.
7. Verify that aggregation, grouping, sorting, pagination, and analytical logic is correctly converted, including GROUP BY, HAVING, ORDER BY, TOP, LIMIT, OFFSET, DISTINCT, aggregate functions, and window functions. Report issue(s) if any.
8. Verify that CTEs, subqueries, correlated subqueries, EXISTS/NOT EXISTS, UNION, UNION ALL, INTERSECT, EXCEPT, and other nested query structures are correctly converted. Report issue(s) if any.
9. Verify that data types, CAST/CONVERT operations, numeric precision, string handling, date/time operations, Boolean values, NULL behavior, parameters, variables, and implicit conversions are compatible with {TargetDatabase}. Report issue(s) if any.
10. Verify that the converted query preserves the same result set, filtering, joins, calculations, sorting, aggregation, NULL behavior, date/time behavior, and overall business logic as the source query. Report issue(s) if any.
11. Verify that the converted query contains no remaining unsupported source-database syntax, functions, keywords, or features, is executable in {TargetDatabase}, and identify, explain, classify, and correct it.Report issue(s) if any.
12. Identify any missing, incorrect, or incompatible conversions.
13. Explain each issue found.
15. If the conversion is correct, explicitly state that no issues were found.

Return your response EXACTLY in following format with all 5 points:

**1. Overall Status**:
<Correct / Incorrect>
after it add a line ""-------------------- - ""

* *2.Issues Found (if any)**:
-...
after it add a line ""---------------------""

**3. Original Source Query**:
...
after it add a line ""---------------------""

**4. Original Translated Query**:
...
after it add a line ""---------------------""

**5. Generate Deepseek Translated Source Query to {TargetDatabase} Query * *:
...
after it add a line ""---------------------""
";

//AI Models List

//OllamaResponse answer = await client.AskAsync("deepseek-r1:7b", strPrompt);
//OllamaResponse answer = await client.AskAsync("qwen2.5-coder:7b", strPrompt);
//OllamaResponse answer = await client.AskAsync("distil-qwen3-4b-text2sql", strPrompt);
OllamaResponse answer = await client.AskAsync("llama3.1:8b", strPrompt);

//+------------------------------------------------------------------+
// Execution is paused here because of await method call above and   |
// and it will continues to the next line below as response receives | 
//+------------------------------------------------------------------+

stopwatch.Stop();

// Wait for timer task to exit
await timerTask;

// Clear the elapsed time line
Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");

Console.WriteLine($"Completed in: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}");
Console.WriteLine();

Console.WriteLine("========== AI Response ==========");
Console.WriteLine(answer.Response);
Console.WriteLine("=================================");

// Copy to clipboard (requires one of the clipboard methods discussed earlier)
// Clipboard.SetText(answer.Response);

Console.WriteLine();
Console.WriteLine("Press ENTER to exit...");
Console.ReadLine();


// ============================================================================
// ASYNC FLOW: Ollama Request + Elapsed-Time Timer
// ============================================================================
//
//                         PROGRAM START
//                              │
//                              ▼
//                     Create OllamaClient
//                              │
//                              ▼
//                   "Sending request..."
//                              │
//                              ▼
//                       Start Stopwatch
//                              │
//                              ▼
//                        Task.Run(...)
//                         /          \
//                        /            \
//                       ▼              ▼
//                  TIMER TASK       MAIN TASK
//                      │                 │
//                      │                 ▼
//                      │           AskAsync(...)
//                      │                 │
//                      ▼                 ▼
//                   Print          Ollama request
//                      │                 │
//                      ▼                 │
//               Delay 1 second           │
//                      │                 │
//                      ▼                 │
//                   Print                │
//                      │                 │
//                      ▼                 │
//               Delay 1 second           │
//                      │                 │
//                      ▼                 │
//                     ...                │
//                      │                 │
//                      │           Ollama responds
//                      │                 │
//                      │                 ▼
//                      │          await completes
//                      │                 │
//                      │                 ▼
//                      │         answer = response
//                      │                 │
//                      │                 ▼
//                      │         stopwatch.Stop()
//                      │                 │
//                      ◄─────────────────┘
//                      │
//              IsRunning == false
//                      │
//                      ▼
//                 Timer exits
//                      │
//                      ▼
//               await timerTask
//                      │
//                      ▼
//                   FINISHED
//
// ============================================================================
//
// IMPORTANT:
//   1. Task.Run() starts the timer work separately.
//   2. AskAsync() sends the request to Ollama.
//   3. await AskAsync() waits asynchronously for the Ollama response.
//   4. While waiting, the TIMER TASK continues printing elapsed time.
//   5. When Ollama responds, await completes and 'answer' gets the response.
//   6. stopwatch.Stop() changes IsRunning to false.
//   7. The timer sees IsRunning == false and exits its loop.
//   8. await timerTask ensures the timer task has completely finished.
//
// ============================================================================