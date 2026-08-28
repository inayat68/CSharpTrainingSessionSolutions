------------------------------------ OLLAMA ------------------------------------------

# LLAMA is the local runtime/platform that downloads, manages, loads and runs models on your computer. 

                		   Ollama
                  		  	 │
                  ┌──────────┼──────────┐

            DeepSeek-R1   	llama      Qwen
            		  	     │
                  ┌──────────┼──────────┐
    			
                    AI inference locally

-------------------------------------------------------------------------------------

# Download and install OLLAMA platform

OLLAMA Link to Download:
https://ollama.com/download/windows

    OR

PS> irm https://ollama.com/install.ps1 | iex

PS> ollama list

NAME                               ID              SIZE      MODIFIED
qwen2.5-coder:7b                   dae161e27b0e    4.7 GB    29 minutes ago
distil-qwen3-4b-text2sql:latest    601038027535    2.5 GB    21 hours ago
llama3.1:8b                        46e0c10c039e    4.9 GB    23 hours ago
deepseek-r1:7b                     755ced02ce7b    4.7 GB    2 days ago

------------------------------ FOUNDATION AI MODELS ----------------------------------

# Install and Run Model: LLAMA - 8b parameters

PS> ollama pull llama3.1:8b

PS> ollama run llama3.1:8b

------------------------------------------------------------------------------------

# Install and Run MODEL: Qwen Coder - 7b parameters

PS> ollama pull qwen2.5-coder:7b

PS> ollama run qwen2.5-coder:7b

------------------------------------------------------------------------------------

# Install and Run MODEL: Qwen Distil - 4b parameters

PS> ollama pull distil-qwen3-4b-text2sql

PS> ollama run distil-qwen3-4b-text2sql


-------------------------- OR -----------------------------------------------------

# 1. Install the huggingface-hub tool
PS> pip install huggingface-hub

# 2. Download only the model files (this is often faster and more stable)
PS> huggingface-cli download distil-labs/distil-qwen3-4b-text2sql-gguf-4bit --local-dir distil-model

# 3. Navigate into the downloaded directory
PS> cd distil-model

# 4. Create the Ollama model
PS> ollama create distil-qwen3-4b-text2sql -f Modelfile

# 5. Run it
PS> ollama run distil-qwen3-4b-text2sql

-------------------------------------------------------------------------------------

# Install and Run Model: Deepseek - 7b parameters

PS> ollama pull deepseek-r1:7b

PS> ollama pull deepseek-r1:7b

------------------------------------------------------------------------------------

----------------------- SAMPLE Transalated Queries using MODELS --------------------

------------------------------------------------------------------------------------

# xxxxxxxxxxxxxxxxxx Query QD1: Qwen-4b-Dist xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:00:51
========================================
Status : 200 OK
========================================

Completed in: 00:00:52.621

========== AI Response ==========
1. Source Database Name
Microsoft SQL Server

2. Target Database Name
PostgreSQL

3. Source SQL Query
SELECT TOP 10 * FROM Employees Order By EmployeeId

4. Provided Target SQL Query
SELECT * FROM Employees ORDER BY EmployeeId LIMIT 10;

5. AI Translated Query
SELECT * FROM Employees ORDER BY EmployeeId OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

6. Incorrect Differences List
None. The provided target query is functionally equivalent to the source query in terms of syntax, behavior, and database-specific semantics when executed on PostgreSQL. There are no errors or incorrect differences in the provided target SQL query. It correctly translates the TOP 10 with ORDER BY EmployeeId into PostgreSQL's LIMIT clause. No semantic deviation occurs under standard assumptions about column types or data distribution. The translation is valid for all cases where EmployeeId is a numeric or string identifier that can be used as a sort key. Therefore, the provided target query has no incorrect differences and is functionally equivalent to the source SQL statement.

Final determination: The Provided Target SQL Query is functionally equivalent to the Source SQL Query. No errors exist in syntax, semantics, or database compatibility.


# xxxxxxxxxxxxxxxxxx Query LL1: Llama-3.2b xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:02:36
========================================
Status : 200 OK
========================================

Completed in: 00:02:36.654

========== AI Response ==========
Here is the response in the exact format requested:


1. Source Database Name
Microsoft SQL Server


2. Target Database Name
PostgreSQL


3. Source SQL Query
SELECT TOP 10 * FROM Employees Order By EmployeeId


4. Provided Target SQL Query
SELECT * FROM Employees ORDER BY EmployeeId LIMIT 10;


5. AI Translated Query
SELECT * FROM Employees ORDER BY EmployeeId LIMIT 10


6. Incorrect Differences List
None, as the provided target query is functionally equivalent to the source query.


Explanation:

The provided target query and the AI-translated query are identical. This suggests that the original translator or someone familiar with both databases hasgenerated a correct translation.

However, upon closer inspection, we can identify a subtle difference in behavior between the two queries:


* The `SELECT TOP 10 * FROM Employees` clause in the source query is not directly equivalent to the `LIMIT 10` clause used in the target query. In Microsoft SQL Server, `TOP` is used to limit the number of rows returned, whereas in PostgreSQL, `LIMIT` serves a similar purpose.


Since this difference does not affect functional equivalence and depends on the underlying database schema (column data type), we can conclude that the provided target query is indeed functionally equivalent to the source query.

Correctness verification:


The AI-translated query accurately reflects the functionality of the original source query. No errors or syntax issues are present in either the source or target queries.


Final assessment:


Functionally equivalent: Yes


Syntax and semantic behavior consistent across both databases: Yes


# xxxxxxxxxxxxxxxxxx Query QC1: Qwen-2.5b-Coder xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:01:34
========================================
Status : 200 OK
========================================

Completed in: 00:01:35.860

========== AI Response ==========
1. Source Database Name
Microsoft SQL Server

2. Target Database Name
PostgreSQL

3. Source SQL Query
```sql
SELECT TOP 10 * FROM Employees ORDER BY EmployeeId
```

4. Provided Target SQL Query
```sql
SELECT * FROM Employees ORDER BY EmployeeId LIMIT 10;
```

5. AI Translated Query
```sql
SELECT * FROM Employees ORDER BY EmployeeId LIMIT 10;
```

6. Incorrect Differences List
None


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
------------------------------------------------------------------------------------


# xxxxxxxxxxxxxxxxxx Query QD2: Qwen-4b-Dist xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:03:51
========================================
Status : 200 OK
========================================

Completed in: 00:03:52.644

========== AI Response ==========
1. Source Database Name
Microsoft SQL Server

2. Target Database Name
PostgreSQL

3. Source SQL Query
SELECT TOP 10
        e.EmployeeId,
        e.FirstName + ' ' + e.LastName AS EmployeeName,
        ISNULL(e.Salary, 0) AS Salary,
        GETDATE() AS CurrentDate,
        YEAR(e.HireDate) AS HireYear
FROM Employees e
WHERE e.IsActive = 1
ORDER BY e.Salary DESC

4. Provided Target SQL Query
SELECT
        e.EmployeeId,
        e.FirstName || ' ' || e.LastName AS EmployeeName,
        ISNULL(e.Salary, 0) AS Salary,
        CURRENT_TIMESTAMP AS CurrentDate,
        EXTRACT(YEAR FROM e.HireDate) AS HireYear
FROM Employees e
WHERE e.IsActive = 1
ORDER BY e.Salary DESC
LIMIT 10;

5. AI Translated Query
SELECT
    e.EmployeeId,
    e.FirstName || ' ' || e.LastName AS EmployeeName,
    COALESCE(e.Salary, 0) AS Salary,
    NOW() AS CurrentDate,
    EXTRACT(YEAR FROM e.HireDate) AS HireYear
FROM Employees e
WHERE e.IsActive = TRUE
ORDER BY e.Salary DESC
LIMIT 10;

6. Incorrect Differences List
- The provided target query uses ISNULL(e.Salary, 0), but PostgreSQL does not have a function named ISNULL that works on this schema. It should use COALESCE instead, which is the correct and standard equivalent in PostgreSQL.
- The source query uses GETDATE(), which returns the current timestamp; however, PostgreSQL's NOW() or CURRENT_TIMESTAMP are more accurate. While both can be used, NOW() is functionally equivalent to GETDATE(). However, the provided targetSQL uses CURRENT_TIMESTAMP, which is valid but not strictly identical-it returns a timestamp with timezone-awareness ifenabled. The source's GETDATE() does not have such implications; thus, a direct translation using NOW() or CURRENT_TIMESTAMP would be acceptable in context. This difference is minor and acceptable for functional equivalence unless strict precision is required.
- The provided target SQL uses LIMIT 10 instead of TOP 10, which is valid since PostgreSQL uses LIMIT to achieve the same behavior as TOP n. However, this is a syntax-level equivalent and not an error per se. But crucially: in the source, there is no explicit indication that the row count is for "top by salary" with paging-TOP 10 implies at most 10 rows. InPostgreSQL, LIMIT 10 does so correctly.
- The WHERE clause uses e.IsActive = 1; however, PostgreSQL boolean values are not defaulting to TRUE or FALSE like SQLServer, and the boolean operator in this context must be explicitly checked. While PostgreSQL supports boolean comparisons (e.g., IsActive = true), the provided target uses an implicit cast from integer 1 to boolean. This is valid-but onlyif the column is actually of type BOOLEAN or INT/SMALLINT. If the column is defined as BIT, TINYINT, or similar, the comparison may behave differently. Since the schema dependency is not specified, and SQL Server allows numeric comparisonson boolean columns (e.g., 1=TRUE), this behavior must be confirmed in the target schema.
- The provided query does not explicitly declare any data types for expressions such as e.FirstName || ' ' || e.LastName-this is valid since PostgreSQL handles string concatenation correctly for text types, but assumes column types are compatible. If any of these columns are NULL or of a different type (e.g., VARCHAR vs TEXT), the expression may fail silently. This dependency on schema exists and is not addressed in the target query.
- The provided query uses EXTRACT(YEAR FROM e.HireDate) which is correct, but in PostgreSQL, this function returns an integer year value-correctly matching the source YEAR() output.

Conclusion: The provided target SQL query contains one error that affects correctness: **ISNULL(e.Salary, 0)** ␦ **PostgreSQL does not recognize ISNULL as a valid function**; it must be replaced with COALESCE. All other expressions and clauses are either correct or valid in PostgreSQL context.

Final determination:
? The provided target SQL query is NOT functionally equivalent due to the use of ISNULL(e.Salary, 0), which is invalid syntax in PostgreSQL.

Corrected version required: Use COALESCE instead.

Therefore, the incorrect differences list below focuses only on syntactic and semantic errors:

6. Incorrect Differences List
- The function ISNULL is not valid in PostgreSQL. It must be replaced with COALESCE to achieve the same behavior of returning a default value if NULL. This results in a syntax error or non-equivalent behavior unless corrected.

The provided target SQL query contains one syntactic and semantic error: the use of ISNULL(e.Salary, 0), which is invalid PostgreSQL syntax. The correct equivalent function must be COALESCE. Any version using ISNULL will not execute or produce expected results in PostgreSQL. This is a critical error affecting correctness.

Therefore, the provided target SQL query is NOT functionally equivalent to the source SQL query due to this error.

# xxxxxxxxxxxxxxxxxx Query LL2: Llama-3.2b xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:03:03
========================================
Status : 200 OK
========================================

Completed in: 00:03:03.247

========== AI Response ==========
Here is the response in the exact format requested:


1. Source Database Name
Microsoft SQL Server


2. Target Database Name
PostgreSQL


3. Source SQL Query
SELECT TOP 10
                                e.EmployeeId,
                                e.FirstName + ' ' + e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                GETDATE() AS CurrentDate,
                                YEAR(e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC


4. Provided Target SQL Query
SELECT
                                e.EmployeeId,
                                e.FirstName || ' ' || e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                CURRENT_TIMESTAMP AS CurrentDate,
                                EXTRACT(YEAR FROM e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC
                            LIMIT 10;


5. AI Translated Query
SELECT
    e.EmployeeId,
    e.FirstName || ' ' || e.LastName AS EmployeeName,
    COALESCE(e.Salary, 0) AS Salary,
    CURRENT_TIMESTAMP AS CurrentDate,
    EXTRACT(YEAR FROM e.HireDate) AS HireYear
FROM Employees e
WHERE e.IsActive = 1
ORDER BY e.Salary DESC
LIMIT 10;

6. Incorrect Differences List

* The Source SQL Query uses `ISNULL` function, while the AI Translated Query uses `COALESCE` which is equivalent in PostgreSQL.
* In the Source SQL Query, `GETDATE()` returns the date and time, whereas in the AI Translated Query, `CURRENT_TIMESTAMP` returns the current timestamp. This difference depends on the underlying database schema or column data type.

Note that I have generated my own AI Translated Query based solely on the Source SQL Query, without using or relying on the Provided Target SQL Query. The differences between the AI Translated Query and the Provided Target SQL Query are identified in the "Incorrect Differences List" section above.

# xxxxxxxxxxxxxxxxxx Query QC2: Qwen-2.5b-Coder xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:02:33
========================================
Status : 200 OK
========================================

Completed in: 00:02:34.382

========== AI Response ==========
1. Source Database Name
Microsoft SQL Server

2. Target Database Name
PostgreSQL

3. Source SQL Query
```sql
SELECT TOP 10
                                e.EmployeeId,
                                e.FirstName + ' ' + e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                GETDATE() AS CurrentDate,
                                YEAR(e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC
```

4. Provided Target SQL Query
```sql
SELECT
                                e.EmployeeId,
                                e.FirstName || ' ' || e.LastName AS EmployeeName,
                                ISNULL(e.Salary, 0) AS Salary,
                                CURRENT_TIMESTAMP AS CurrentDate,
                                EXTRACT(YEAR FROM e.HireDate) AS HireYear
                            FROM Employees e
                            WHERE e.IsActive = 1
                            ORDER BY e.Salary DESC
                            LIMIT 10;
```

5. AI Translated Query
```sql
SELECT
    e.EmployeeId,
    CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
    COALESCE(e.Salary, 0) AS Salary,
    CURRENT_TIMESTAMP AS CurrentDate,
    EXTRACT(YEAR FROM e.HireDate) AS HireYear
FROM Employees e
WHERE e.IsActive = 1
ORDER BY e.Salary DESC
LIMIT 10;
```

6. Incorrect Differences List
- The `CONCAT` function is used instead of the `||` operator for string concatenation in PostgreSQL.
- The `COALESCE` function is used instead of `ISNULL` to handle NULL values, as PostgreSQL uses `COALESCE`.
- The `LIMIT 10` clause is correctly placed at the end of the query, which is the correct way to limit the result set in PostgreSQL.


------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
------------------------------------------------------------------------------------

# xxxxxxxxxxxxxxxxxx Query QD3: Qwen-4b-Dist xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:05:34
========================================
Status : 200 OK
========================================

Completed in: 00:05:34.852

========== AI Response ==========
1. Source Database Name
Microsoft SQL Server

2. Target Database Name
PostgreSQL

3. Source SQL Query
[As provided]

4. Provided Target SQL Query
[As provided]

5. AI Translated Query
WITH EmployeeSales AS (
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
    INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
    LEFT JOIN Orders o ON e.EmployeeID = o.EmployeeID
    LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
    WHERE o.OrderDate >= DATEADD(YEAR, -1, GETDATE())
    GROUP BY e.EmployeeID, e.FirstName, e.LastName, e.DepartmentID, d.DepartmentName
    HAVING SUM(od.Quantity * od.UnitPrice) > 5000
)
SELECT
    ROW_NUMBER() OVER (ORDER BY es.TotalSales DESC) AS RowNo,
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
    EXTRACT(DAY FROM CURRENT_DATE - es.LastOrderDate) + (EXTRACT(MONTH FROM CURRENT_DATE) * 30) + (EXTRACT(YEAR FROM CURRENT_DATE) * 365) AS DaysSinceLastOrder,
    CONCAT(m.FirstName, ' ', m.LastName) AS ManagerName
FROM EmployeeSales es
INNER JOIN Employees e ON es.EmployeeID = e.EmployeeID
LEFT JOIN Employees m ON e.ManagerID = m.EmployeeID
WHERE es.DepartmentName LIKE 'S%'
AND EXISTS (SELECT 1 FROM Bonuses b WHERE b.EmployeeID = es.EmployeeID AND b.BonusYear = EXTRACT(YEAR FROM CURRENT_DATE))
ORDER BY TotalSales DESC, EmployeeName
LIMIT 20;

6. Incorrect Differences List
- The expression for DaysSinceLastOrder in the AI Translated Query is functionally incorrect and non-standard. It attempts to calculate days by manually adding months and years, which does not equate to date arithmetic. PostgreSQL has a built-in DATE_DIFF or simply subtracting dates with appropriate casting.
- The AI Translated Query uses an incorrect formula for DaysSinceLastOrder that depends on flawed assumptions about month lengths (e.g., 30 days per month). This may produce inaccurate results under any real-world date scenario, especially across years or months with varying lengths.
- The AI query does not use the correct operator for subtracting dates in PostgreSQL; it uses an invalid arithmetic expression instead of proper DATE subtraction. In PostgreSQL, you should use `CURRENT_DATE - es.LastOrderDate` to get a date interval, and then extract days from that interval using `EXTRACT(DAY FROM ...)` or simply cast to integer if the result is expected as days.
- The provided Target SQL Query already correctly computes DaysSinceLastOrder with `CURRENT_DATE - es.LastOrderDate::DATE`, which gives an INTEGER (days) when cast properly. This is valid and standard.
- The AI Translated Query fails to use a valid date arithmetic expression for day counting, violating PostgreSQL syntax and resulting in incorrect behavior.

Conclusion: The Provided Target SQL Query IS functionally equivalent to the Source SQL Query, with correct PostgreSQL syntax and accurate date calculations. The AI Translated Query contains a critical error in its DaysSinceLastOrder calculation that makes it non-equivalent. The provided target query is valid and correctly implements the intended logic.


# xxxxxxxxxxxxxxxxxx Query LL3: Llama-3.2b xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:13:28
========================================
Status : 200 OK
========================================

Completed in: 00:13:28.998

========== AI Response ==========
**1. Source Database Name**
Microsoft SQL Server

**2. Target Database Name**
PostgreSQL

**3. Source SQL Query**

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

**4. Provided Target SQL Query**

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

**5. AI Translated Query**

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

**6. Incorrect Differences List**

*   The `DATEADD` function is not supported in PostgreSQL, so it was replaced with `CURRENT_TIMESTAMP - INTERVAL '1 year'`.
*   The `TOP` clause is not used in the AI Translated Query because PostgreSQL uses `LIMIT` instead.
*   The `ISNULL` function is not necessary in the AI Translated Query since `COALESCE` can be used instead to handle null values.
*   The `DATEDIFF` function was replaced with `CURRENT_DATE - es.LastOrderDate::DATE` to calculate the days since the last order date.

Note that the AI Translated Query uses PostgreSQL-specific functions and syntax, such as `CONCAT`, `COALESCE`, and `EXTRACT`. These differences are due to the fact that PostgreSQL has its own set of built-in functions and operators.

# xxxxxxxxxxxxxxxxxx Query QC3: Qwen-2.5b-Coder xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
Sending request to Ollama...
Elapsed Time: 00:10:32
========================================
Status : 200 OK
========================================

Completed in: 00:10:33.653

========== AI Response ==========
1. Source Database Name:
Microsoft SQL Server

2. Target Database Name:
PostgreSQL

3. Source SQL Query:

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

4. Provided Target SQL Query:

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

5. AI Translated Query:

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
        o.OrderDate >= CURRENT_DATE - INTERVAL '1 year'

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

    es.FirstName || ' ' || es.LastName AS EmployeeName,

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
                YEAR FROM CURRENT_DATE
            )
    )

ORDER BY
    TotalSales DESC,
    EmployeeName;

6. Incorrect Differences List:
- The `TOP` keyword in SQL Server is not directly supported in PostgreSQL, so it should be replaced with the `LIMIT` keyword.
- The string concatenation operator `+` is not used for string concatenation in PostgreSQL; instead, the `||` operator is used.
- No other differences were identified.


--------------------------------------------------------------------------------------

--------------------------------------- Claude Code ----------------------------------

--------------------------------------------------------------------------------------

Claude Code Install on Windows Powershell
irm https://claude.ai/install.ps1 | iex
Setting up Claude Code...

√ Claude Code successfully installed!

  Version: 2.1.223

  Location: C:\Users\Inayat.Rehman\.local\bin\claude.exe


  Next: Run claude --help to get started

‼ Setup notes:
  ● Native installation exists but C:\Users\Inayat.Rehman\.local\bin is not in your PATH. Add it by opening: System Properties → Environment Variables →
    Edit User PATH → New → Add the path above. Then restart your terminal.


✅ Installation complete!

-----------------------------------------------------------

[Environment]::SetEnvironmentVariable(
    "Path",
    [Environment]::GetEnvironmentVariable("Path", "User") + ";C:\Users\Inayat.Rehman\.local\bin",
    "User"
)

-----------------------------------------------------------

https://code.claude.com/docs/en/quickstart

------------------------------------------------------------

/ goal

----------------------------------------------------------

