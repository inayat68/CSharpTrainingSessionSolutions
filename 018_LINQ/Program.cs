using System;
using System.Linq;
using System.Collections.Generic;

namespace LINQ_16;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("================================================");
        Console.WriteLine("              16 - LINQ DEMO");
        Console.WriteLine("================================================");

        // ============================================================
        // LINQ
        // ============================================================
        // LINQ = Language Integrated Query
        //
        // LINQ is used to:
        //   - Filter
        //   - Sort
        //   - Transform
        //   - Group
        //   - Join
        //   - Aggregate
        //   - Search
        //   - Paginate
        //
        // Java equivalent:
        //   LINQ      -> Java Stream API
        //
        // LINQ works with arrays, List<T>, collections,
        // databases through Entity Framework, XML, etc.


        // ============================================================
        // EMPLOYEE DATA
        // ============================================================

        List<Employee> employees =
        [
            new Employee(101, "Ali",   "IT",        150000, 5),
            new Employee(102, "Saad",  "HR",        110000, 3),
            new Employee(103, "Ahmed", "IT",        175000, 7),
            new Employee(104, "Sara",  "Finance",   135000, 4),
            new Employee(105, "Ayesha","HR",        125000, 6),
            new Employee(106, "Usman", "IT",        160000, 4),
            new Employee(107, "Hina",  "Finance",   145000, 8),
            new Employee(108, "Bilal", "Sales",     100000, 2),
            new Employee(109, "Zara",  "Sales",     120000, 5),
            new Employee(110, "Hamza", "IT",        190000, 10)
        ];


        // ============================================================
        // 1. WHERE - FILTER
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("1. WHERE - Employees with salary >= 150000");
        Console.WriteLine("--------------------------------------------");

        var highPaidEmployees = employees.Where(e => e.Salary >= 150000);

        foreach (var employee in highPaidEmployees)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .filter(e -> e.getSalary() >= 150000)
        //     .forEach(e ->
        //         System.out.println(
        //             e.getName() + " - " + e.getSalary()));


        // ============================================================
        // 2. SELECT - TRANSFORM / PROJECT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("2. SELECT - Get employee names");
        Console.WriteLine("--------------------------------");

        var employeeNames = employees.Select(e => e.Name);

        Console.WriteLine(string.Join(", ", employeeNames));

        // Java:
        //
        // List<String> employeeNames = employees.stream()
        //     .map(Employee::getName)
        //     .collect(Collectors.toList());


        // ============================================================
        // 3. SELECT - CREATE ANONYMOUS OBJECT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("3. SELECT - Project selected properties");
        Console.WriteLine("----------------------------------------");

        var employeeSummary = employees
            .Select(e => new
            {
                e.Name,
                e.Department,
                e.Salary
            });

        foreach (var employee in employeeSummary)
        {
            Console.WriteLine(
                $"{employee.Name,-10} " +
                $"{employee.Department,-10} " +
                $"{employee.Salary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .map(e -> new EmployeeSummary(
        //         e.getName(),
        //         e.getDepartment(),
        //         e.getSalary()))
        //     .collect(Collectors.toList());


        // ============================================================
        // 4. ORDERBY - ASCENDING SORT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("4. ORDERBY - Salary ascending");
        Console.WriteLine("--------------------------------");

        var salaryAscending = employees.OrderBy(e => e.Salary);

        foreach (var employee in salaryAscending)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .sorted(Comparator.comparing(Employee::getSalary))
        //     .forEach(...);


        // ============================================================
        // 5. ORDERBYDESCENDING
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("5. ORDERBYDESCENDING - Salary descending");
        Console.WriteLine("-----------------------------------------");

        var salaryDescending = employees.OrderByDescending(e => e.Salary);

        foreach (var employee in salaryDescending)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .sorted(
        //         Comparator.comparing(Employee::getSalary)
        //                   .reversed())
        //     .forEach(...);


        // ============================================================
        // 6. THENBY - SECONDARY SORT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("6. THENBY - Department then Name");
        Console.WriteLine("--------------------------------");

        var departmentAndName = employees.OrderBy(e => e.Department).ThenBy(e => e.Name);

        foreach (var employee in departmentAndName)
        {
            Console.WriteLine($"{employee.Department,-10} {employee.Name}");
        }

        // Java:
        //
        // employees.stream()
        //     .sorted(
        //         Comparator.comparing(Employee::getDepartment)
        //             .thenComparing(Employee::getName))
        //     .forEach(...);


        // ============================================================
        // 7. FIRST
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("7. FIRST - First employee");
        Console.WriteLine("--------------------------");

        var firstEmployee = employees.First();

        Console.WriteLine($"{firstEmployee.Id} - {firstEmployee.Name}");

        // Java:
        //
        // Employee firstEmployee = employees.stream()
        //     .findFirst()
        //     .orElseThrow();


        // ============================================================
        // 8. FIRSTORDEFAULT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("8. FIRSTORDEFAULT");
        Console.WriteLine("------------------");

        var employee200 = employees.FirstOrDefault(e => e.Id == 200);

        Console.WriteLine(
            employee200 == null
                ? "Employee not found"
                : employee200.Name);

        // Java:
        //
        // Optional<Employee> employee200 =
        //     employees.stream()
        //         .filter(e -> e.getId() == 200)
        //         .findFirst();


        // ============================================================
        // 9. SINGLE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("9. SINGLE - Find employee by unique ID");
        Console.WriteLine("---------------------------------------");

        var employee101 = employees
            .Single(e => e.Id == 101);

        Console.WriteLine(employee101.Name);

        // Java:
        //
        // Employee employee101 = employees.stream()
        //     .filter(e -> e.getId() == 101)
        //     .findFirst()
        //     .orElseThrow();


        // ============================================================
        // 10. ANY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("10. ANY - Is there an employee earning > 180000?");
        Console.WriteLine("-----------------------------------------------");

        bool existsHighSalary = employees
            .Any(e => e.Salary > 180000);

        Console.WriteLine(existsHighSalary);

        // Java:
        //
        // boolean existsHighSalary = employees.stream()
        //     .anyMatch(e -> e.getSalary() > 180000);


        // ============================================================
        // 11. ALL
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("11. ALL - Do all employees earn >= 100000?");
        Console.WriteLine("------------------------------------------");

        bool allAbove100K = employees
            .All(e => e.Salary >= 100000);

        Console.WriteLine(allAbove100K);

        // Java:
        //
        // boolean allAbove100K = employees.stream()
        //     .allMatch(e -> e.getSalary() >= 100000);


        // ============================================================
        // 12. COUNT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("12. COUNT - Number of employees");
        Console.WriteLine("--------------------------------");

        int employeeCount = employees.Count();

        Console.WriteLine(employeeCount);

        // Java:
        //
        // long employeeCount = employees.stream()
        //     .count();


        // ============================================================
        // 13. COUNT WITH CONDITION
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("13. COUNT - Number of IT employees");
        Console.WriteLine("-----------------------------------");

        int itCount = employees
            .Count(e => e.Department == "IT");

        Console.WriteLine(itCount);

        // Java:
        //
        // long itCount = employees.stream()
        //     .filter(e -> e.getDepartment().equals("IT"))
        //     .count();


        // ============================================================
        // 14. SUM
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("14. SUM - Total salary");
        Console.WriteLine("-----------------------");

        decimal totalSalary = employees
            .Sum(e => e.Salary);

        Console.WriteLine($"{totalSalary:N0}");

        // Java:
        //
        // int totalSalary = employees.stream()
        //     .mapToInt(Employee::getSalary)
        //     .sum();


        // ============================================================
        // 15. AVERAGE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("15. AVERAGE - Average salary");
        Console.WriteLine("-----------------------------");

        decimal averageSalary = employees
            .Average(e => e.Salary);

        Console.WriteLine($"{averageSalary:N2}");

        // Java:
        //
        // double averageSalary = employees.stream()
        //     .mapToInt(Employee::getSalary)
        //     .average()
        //     .orElse(0);


        // ============================================================
        // 16. MIN / MAX
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("16. MIN / MAX salary");
        Console.WriteLine("---------------------");

        decimal minimumSalary = employees.Min(e => e.Salary);
        decimal maximumSalary = employees.Max(e => e.Salary);

        Console.WriteLine($"Minimum: {minimumSalary:N0}");
        Console.WriteLine($"Maximum: {maximumSalary:N0}");

        // Java:
        //
        // int minimumSalary = employees.stream()
        //     .mapToInt(Employee::getSalary)
        //     .min()
        //     .orElse(0);
        //
        // int maximumSalary = employees.stream()
        //     .mapToInt(Employee::getSalary)
        //     .max()
        //     .orElse(0);


        // ============================================================
        // 17. GROUPBY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("17. GROUPBY - Employees by Department");
        Console.WriteLine("---------------------------------------");

        var employeesByDepartment = employees
            .GroupBy(e => e.Department);

        foreach (var group in employeesByDepartment)
        {
            Console.WriteLine();
            Console.WriteLine($"Department: {group.Key}");

            foreach (var employee in group)
            {
                Console.WriteLine(
                    $"  {employee.Name} - {employee.Salary:N0}");
            }
        }

        // Java:
        //
        // Map<String, List<Employee>> employeesByDepartment =
        //     employees.stream()
        //         .collect(Collectors.groupingBy(
        //             Employee::getDepartment));


        // ============================================================
        // 18. GROUPBY + COUNT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("18. GROUPBY + COUNT");
        Console.WriteLine("-------------------");

        var departmentCounts = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                Count = g.Count()
            });

        foreach (var item in departmentCounts)
        {
            Console.WriteLine(
                $"{item.Department}: {item.Count}");
        }

        // Java:
        //
        // Map<String, Long> departmentCounts =
        //     employees.stream()
        //         .collect(Collectors.groupingBy(
        //             Employee::getDepartment,
        //             Collectors.counting()));


        // ============================================================
        // 19. GROUPBY + SUM
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("19. GROUPBY + SUM - Salary by Department");
        Console.WriteLine("-----------------------------------------");

        var salaryByDepartment = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                TotalSalary = g.Sum(e => e.Salary)
            });

        foreach (var item in salaryByDepartment)
        {
            Console.WriteLine(
                $"{item.Department,-10} " +
                $"{item.TotalSalary:N0}");
        }

        // Java:
        //
        // Map<String, Integer> salaryByDepartment =
        //     employees.stream()
        //         .collect(Collectors.groupingBy(
        //             Employee::getDepartment,
        //             Collectors.summingInt(
        //                 Employee::getSalary)));


        // ============================================================
        // 20. GROUPBY + AVERAGE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("20. GROUPBY + AVERAGE");
        Console.WriteLine("---------------------");

        var averageSalaryByDepartment = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AverageSalary = g.Average(e => e.Salary)
            });

        foreach (var item in averageSalaryByDepartment)
        {
            Console.WriteLine(
                $"{item.Department,-10} " +
                $"{item.AverageSalary:N2}");
        }

        // Java:
        //
        // Map<String, Double> averageSalaryByDepartment =
        //     employees.stream()
        //         .collect(Collectors.groupingBy(
        //             Employee::getDepartment,
        //             Collectors.averagingInt(
        //                 Employee::getSalary)));


        // ============================================================
        // 21. SKIP
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("21. SKIP - Skip first 3 employees");
        Console.WriteLine("---------------------------------");

        var skipEmployees = employees
            .Skip(3);

        foreach (var employee in skipEmployees)
        {
            Console.WriteLine(employee.Name);
        }

        // Java:
        //
        // employees.stream()
        //     .skip(3)
        //     .forEach(e -> System.out.println(e.getName()));


        // ============================================================
        // 22. TAKE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("22. TAKE - Take first 3 employees");
        Console.WriteLine("---------------------------------");

        var firstThree = employees
            .Take(3);

        foreach (var employee in firstThree)
        {
            Console.WriteLine(employee.Name);
        }

        // Java:
        //
        // employees.stream()
        //     .limit(3)
        //     .forEach(e -> System.out.println(e.getName()));


        // ============================================================
        // 23. DISTINCT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("23. DISTINCT - Unique departments");
        Console.WriteLine("----------------------------------");

        var departments = employees
            .Select(e => e.Department)
            .Distinct();

        Console.WriteLine(
            string.Join(", ", departments));

        // Java:
        //
        // List<String> departments = employees.stream()
        //     .map(Employee::getDepartment)
        //     .distinct()
        //     .collect(Collectors.toList());


        // ============================================================
        // 24. CONTAINS
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("24. CONTAINS");
        Console.WriteLine("-------------");

        bool hasIT = departments.Contains("IT");

        Console.WriteLine($"Contains IT: {hasIT}");

        // Java:
        //
        // boolean hasIT = departments.contains("IT");


        // ============================================================
        // 25. DISTINCT + ORDERBY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("25. DISTINCT + ORDERBY");
        Console.WriteLine("----------------------");

        var sortedDepartments = employees
            .Select(e => e.Department)
            .Distinct()
            .OrderBy(d => d);

        Console.WriteLine(
            string.Join(", ", sortedDepartments));

        // Java:
        //
        // employees.stream()
        //     .map(Employee::getDepartment)
        //     .distinct()
        //     .sorted()
        //     .forEach(...);


        // ============================================================
        // 26. SELECTMANY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("26. SELECTMANY - Flatten nested collections");
        Console.WriteLine("-------------------------------------------");

        var employeeSkills = employees
            .SelectMany(e => e.Skills);

        Console.WriteLine(
            string.Join(", ", employeeSkills));

        // Java:
        //
        // List<String> employeeSkills =
        //     employees.stream()
        //         .flatMap(e -> e.getSkills().stream())
        //         .collect(Collectors.toList());


        // ============================================================
        // 27. SELECTMANY + DISTINCT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("27. SELECTMANY + DISTINCT");
        Console.WriteLine("-------------------------");

        var allSkills = employees
            .SelectMany(e => e.Skills)
            .Distinct()
            .OrderBy(s => s);

        Console.WriteLine(
            string.Join(", ", allSkills));

        // Java:
        //
        // employees.stream()
        //     .flatMap(e -> e.getSkills().stream())
        //     .distinct()
        //     .sorted()
        //     .forEach(...);


        // ============================================================
        // 28. COMPLEX WHERE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("28. COMPLEX WHERE");
        Console.WriteLine("------------------");

        var complexSearch = employees
            .Where(e =>
                e.Department == "IT" &&
                e.Salary >= 150000 &&
                e.Experience >= 5);

        foreach (var employee in complexSearch)
        {
            Console.WriteLine(employee.Name);
        }

        // Java:
        //
        // employees.stream()
        //     .filter(e ->
        //         e.getDepartment().equals("IT") &&
        //         e.getSalary() >= 150000 &&
        //         e.getExperience() >= 5)
        //     .forEach(...);


        // ============================================================
        // 29. WHERE + ORDERBY + SELECT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("29. WHERE + ORDERBY + SELECT");
        Console.WriteLine("-----------------------------");

        var result = employees
            .Where(e => e.Salary >= 120000)
            .OrderByDescending(e => e.Salary)
            .Select(e => e.Name);

        Console.WriteLine(
            string.Join(", ", result));

        // Java:
        //
        // employees.stream()
        //     .filter(e -> e.getSalary() >= 120000)
        //     .sorted(
        //         Comparator.comparing(Employee::getSalary)
        //                   .reversed())
        //     .map(Employee::getName)
        //     .forEach(...);


        // ============================================================
        // 30. TOP 3 HIGHEST PAID
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("30. TOP 3 HIGHEST PAID EMPLOYEES");
        Console.WriteLine("--------------------------------");

        var topThree = employees
            .OrderByDescending(e => e.Salary)
            .Take(3);

        foreach (var employee in topThree)
        {
            Console.WriteLine(
                $"{employee.Name} - {employee.Salary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .sorted(
        //         Comparator.comparing(Employee::getSalary)
        //                   .reversed())
        //     .limit(3)
        //     .forEach(...);


        // ============================================================
        // 31. PAGINATION
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("31. PAGINATION - Page 2, Page Size 3");
        Console.WriteLine("-------------------------------------");

        int pageNumber = 2;
        int pageSize = 3;

        var page = employees
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        foreach (var employee in page)
        {
            Console.WriteLine(
                $"{employee.Id} - {employee.Name}");
        }

        // Java:
        //
        // int pageNumber = 2;
        // int pageSize = 3;
        //
        // employees.stream()
        //     .skip((pageNumber - 1) * pageSize)
        //     .limit(pageSize)
        //     .forEach(...);


        // ============================================================
        // 32. TOLOOKUP
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("32. TOLOOKUP - Quick lookup by Department");
        Console.WriteLine("-------------------------------------------");

        var employeeLookup =
            employees.ToLookup(e => e.Department);

        foreach (var employee in employeeLookup["IT"])
        {
            Console.WriteLine(employee.Name);
        }

        // Java:
        //
        // Map<String, List<Employee>> employeeLookup =
        //     employees.stream()
        //         .collect(Collectors.groupingBy(
        //             Employee::getDepartment));


        // ============================================================
        // 33. TODICTIONARY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("33. TODICTIONARY - Employee by ID");
        Console.WriteLine("----------------------------------");

        var employeeDictionary =
            employees.ToDictionary(e => e.Id);

        Console.WriteLine(
            employeeDictionary[101].Name);

        // Java:
        //
        // Map<Integer, Employee> employeeDictionary =
        //     employees.stream()
        //         .collect(Collectors.toMap(
        //             Employee::getId,
        //             e -> e));


        // ============================================================
        // 34. MAXBY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("34. MAXBY - Employee with highest salary");
        Console.WriteLine("-----------------------------------------");

        var highestPaid = employees
            .MaxBy(e => e.Salary);

        Console.WriteLine(
            $"{highestPaid?.Name} - {highestPaid?.Salary:N0}");

        // Java:
        //
        // Employee highestPaid = employees.stream()
        //     .max(Comparator.comparing(Employee::getSalary))
        //     .orElseThrow();


        // ============================================================
        // 35. MINBY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("35. MINBY - Employee with lowest salary");
        Console.WriteLine("---------------------------------------");

        var lowestPaid = employees
            .MinBy(e => e.Salary);

        Console.WriteLine(
            $"{lowestPaid?.Name} - {lowestPaid?.Salary:N0}");

        // Java:
        //
        // Employee lowestPaid = employees.stream()
        //     .min(Comparator.comparing(Employee::getSalary))
        //     .orElseThrow();


        // ============================================================
        // 36. AGGREGATE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("36. AGGREGATE - Custom calculation");
        Console.WriteLine("-----------------------------------");

        int totalExperience = employees
            .Select(e => e.Experience)
            .Aggregate(0, (total, experience) =>
                total + experience);

        Console.WriteLine(
            $"Total Experience: {totalExperience}");

        // Java:
        //
        // int totalExperience = employees.stream()
        //     .mapToInt(Employee::getExperience)
        //     .reduce(0, (total, experience) ->
        //         total + experience);


        // ============================================================
        // 37. CONCAT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("37. CONCAT");
        Console.WriteLine("-----------");

        var firstGroup = employees.Take(3);
        var secondGroup = employees.Skip(3).Take(3);

        var combined = firstGroup.Concat(secondGroup);

        Console.WriteLine(
            string.Join(", ", combined.Select(e => e.Name)));

        // Java:
        //
        // Stream.concat(
        //     firstGroup.stream(),
        //     secondGroup.stream())
        // .forEach(...);


        // ============================================================
        // 38. UNION
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("38. UNION - Unique values from two collections");
        Console.WriteLine("----------------------------------------------");

        var departments1 =
            new[] { "IT", "HR", "Finance" };

        var departments2 =
            new[] { "IT", "Sales", "HR" };

        var union = departments1
            .Union(departments2);

        Console.WriteLine(
            string.Join(", ", union));

        // Java:
        //
        // Stream.concat(
        //     Arrays.stream(departments1),
        //     Arrays.stream(departments2))
        // .distinct()
        // .forEach(...);


        // ============================================================
        // 39. INTERSECT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("39. INTERSECT");
        Console.WriteLine("--------------");

        var commonDepartments = departments1
            .Intersect(departments2);

        Console.WriteLine(
            string.Join(", ", commonDepartments));

        // Java:
        //
        // Arrays.stream(departments1)
        //     .filter(d ->
        //         Arrays.asList(departments2).contains(d))
        //     .distinct()
        //     .forEach(...);


        // ============================================================
        // 40. EXCEPT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("40. EXCEPT");
        Console.WriteLine("-----------");

        var onlyFirst = departments1
            .Except(departments2);

        Console.WriteLine(
            string.Join(", ", onlyFirst));

        // Java:
        //
        // Arrays.stream(departments1)
        //     .filter(d ->
        //         !Arrays.asList(departments2).contains(d))
        //     .forEach(...);


        // ============================================================
        // 41. REVERSE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("41. REVERSE");
        Console.WriteLine("------------");

        var reversedEmployees =
            employees.Select(e => e.Name).Reverse();

        Console.WriteLine(
            string.Join(", ", reversedEmployees));

        // Java:
        //
        // Java Stream has no direct reverse().
        // Usually collect into a List and use:
        //
        // Collections.reverse(list);


        // ============================================================
        // 42. JOIN - EMPLOYEE + DEPARTMENT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("42. JOIN - Employee with Department details");
        Console.WriteLine("--------------------------------------------");

        List<Department> departments5 =
        [
            new Department(1, "IT", "Information Technology"),
            new Department(2, "HR", "Human Resources"),
            new Department(3, "Finance", "Finance Department"),
            new Department(4, "Sales", "Sales Department")
        ];

        var employeeDepartments =
            employees.Join(
                departments5,
                employee => employee.Department,
                department => department.Name,
                (employee, department) => new
                {
                    EmployeeName = employee.Name,
                    Department = department.Name,
                    Description = department.Description,
                    Salary = employee.Salary
                });

        foreach (var item in employeeDepartments)
        {
            Console.WriteLine(
                $"{item.EmployeeName,-10} " +
                $"{item.Department,-10} " +
                $"{item.Description}");
        }

        // Java:
        //
        // Java Streams don't have a direct join()
        // equivalent. Usually create a Map first:
        //
        // Map<String, Department> departmentMap =
        //     departments.stream()
        //         .collect(Collectors.toMap(
        //             Department::getName,
        //             d -> d));
        //
        // employees.stream()
        //     .map(e -> new EmployeeDepartment(
        //         e,
        //         departmentMap.get(e.getDepartment())))
        //     .forEach(...);


        // ============================================================
        // 43. COMPLEX LINQ QUERY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("43. COMPLEX LINQ QUERY");
        Console.WriteLine("----------------------");

        var complexResult = employees
            .Where(e =>
                e.Salary >= 120000 &&
                e.Experience >= 4)
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                EmployeeCount = g.Count(),
                TotalSalary = g.Sum(e => e.Salary),
                AverageSalary = g.Average(e => e.Salary),
                HighestSalary = g.Max(e => e.Salary)
            })
            .OrderByDescending(x => x.AverageSalary);

        foreach (var item in complexResult)
        {
            Console.WriteLine(
                $"{item.Department,-10} " +
                $"Employees: {item.EmployeeCount,-2} " +
                $"Total: {item.TotalSalary:N0} " +
                $"Average: {item.AverageSalary:N0} " +
                $"Highest: {item.HighestSalary:N0}");
        }

        // Java:
        //
        // employees.stream()
        //     .filter(e ->
        //         e.getSalary() >= 120000 &&
        //         e.getExperience() >= 4)
        //     .collect(Collectors.groupingBy(
        //         Employee::getDepartment))
        //     .entrySet()
        //     .stream()
        //     .map(entry -> new DepartmentSummary(
        //         entry.getKey(),
        //         entry.getValue().size(),
        //         entry.getValue().stream()
        //             .mapToInt(Employee::getSalary)
        //             .sum(),
        //         entry.getValue().stream()
        //             .mapToInt(Employee::getSalary)
        //             .average()
        //             .orElse(0),
        //         entry.getValue().stream()
        //             .mapToInt(Employee::getSalary)
        //             .max()
        //             .orElse(0)))
        //     .sorted(
        //         Comparator.comparing(
        //             DepartmentSummary::getAverageSalary)
        //             .reversed())
        //     .forEach(...);


        // ============================================================
        // 44. QUERY SYNTAX
        // ============================================================
        // LINQ also supports SQL-like query syntax.
        //
        // This is especially useful for developers coming from
        // SQL backgrounds.

        Console.WriteLine();
        Console.WriteLine("44. LINQ QUERY SYNTAX");
        Console.WriteLine("---------------------");

        var querySyntax =
            from employee in employees
            where employee.Salary >= 150000
            orderby employee.Name
            select employee;

        foreach (var employee in querySyntax)
        {
            Console.WriteLine(
                $"{employee.Name} - {employee.Salary:N0}");
        }

        // Java:
        //
        // Java does not have LINQ query syntax.
        // Java uses Stream API method syntax.


        // ============================================================
        // COMMON LINQ → JAVA STREAM REFERENCE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("================================================");
        Console.WriteLine("       C# LINQ → Java Stream Reference");
        Console.WriteLine("================================================");

        /*
        C# LINQ                         Java Stream
        ------------------------------------------------------------
        Where()                        filter()
        Select()                       map()
        SelectMany()                   flatMap()
        OrderBy()                      sorted()
        OrderByDescending()            sorted(reversed)
        ThenBy()                       thenComparing()
        First()                        findFirst()
        FirstOrDefault()               findFirst() + Optional
        Single()                       No exact equivalent
        Any()                          anyMatch()
        All()                          allMatch()
        Count()                        count()
        Sum()                          sum()
        Average()                      average()
        Min()                          min()
        Max()                          max()
        MinBy()                        min(Comparator)
        MaxBy()                        max(Comparator)
        GroupBy()                      groupingBy()
        Distinct()                     distinct()
        Contains()                     contains()
        Skip()                         skip()
        Take()                         limit()
        Aggregate()                    reduce()
        Concat()                       Stream.concat()
        Union()                        concat + distinct
        Intersect()                    filter + contains
        Except()                       filter + NOT contains
        ToList()                       collect(toList())
        ToDictionary()                collect(toMap())
        ToLookup()                    groupingBy()
        Reverse()                      Collections.reverse()
        Join()                         No direct equivalent
        Query Syntax                   No direct equivalent
        */


        Console.WriteLine();
        Console.WriteLine("================================================");
        Console.WriteLine("                    Done.");
        Console.WriteLine("================================================");
    }
}


// ================================================================
// EMPLOYEE CLASS
// ================================================================

public class Employee
{
    public int Id { get; }

    public string Name { get; }

    public string Department { get; }

    public decimal Salary { get; }

    public int Experience { get; }

    public List<string> Skills { get; }

    public Employee(int id, string name, string department, decimal salary, int experience)
    {
        Id = id;
        Name = name;
        Department = department;
        Salary = salary;
        Experience = experience;

        // Sample skills based on department.
        Skills = department switch
        {
            "IT" =>
            [
                "C#",
                ".NET",
                "SQL"
            ],

            "HR" =>
            [
                "Recruitment",
                "Communication"
            ],

            "Finance" =>
            [
                "Excel",
                "Accounting"
            ],

            "Sales" =>
            [
                "CRM",
                "Communication"
            ],

            _ => []
        };
    }
}


// ================================================================
// DEPARTMENT CLASS
// ================================================================

public class Department
{
    public int Id { get; }

    public string Name { get; }

    public string Description { get; }

    public Department(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}