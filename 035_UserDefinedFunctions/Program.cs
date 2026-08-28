using System;

namespace _35_UserDefinedFunctions;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 08_Functions ===");
        Console.WriteLine("C# methods / Java methods comparison");
        Console.WriteLine();

        // ============================================================
        // 1. MULTIPLICATION TABLE
        // ============================================================

        // C#: Local function can be declared inside Main().
        void GenerateTable(int number)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{number} x {i} = {number * i}");
            }
        }

        // Java equivalent:
        /*
        static void generateTable(int number) {
            for (int i = 1; i <= 10; i++) {
                System.out.println(
                    number + " x " + i + " = " + (number * i));
            }
        }
        */

        GenerateTable(5);

        // OUTPUT:
        // 5 x 1 = 5
        // 5 x 2 = 10
        // ...
        // 5 x 10 = 50


        // ============================================================
        // 2. WEATHER ADVICE
        // ============================================================

        string WeatherAdvice(int temp)
        {
            if (temp > 35)
                return "Very Hot";

            if (temp >= 20)
                return "Pleasant";

            return "Cold";
        }

        // Java equivalent:
        /*
        static String weatherAdvice(int temp) {
            if (temp > 35)
                return "Very Hot";

            if (temp >= 20)
                return "Pleasant";

            return "Cold";
        }
        */

        Console.WriteLine(WeatherAdvice(40));
        // OUTPUT: Very Hot


        // ============================================================
        // 3. CIRCLE CALCULATOR
        // ============================================================

        double CircleCalculator(double radius, string type)
        {
            double PI = 3.14159;

            if (type == "area")
                return PI * radius * radius;

            if (type == "circumference")
                return 2 * PI * radius;

            return -1;
        }

        // Java equivalent:
        /*
        static double circleCalculator(double radius, String type) {
            double PI = 3.14159;

            if (type.equals("area"))
                return PI * radius * radius;

            if (type.equals("circumference"))
                return 2 * PI * radius;

            return -1;
        }
        */

        Console.WriteLine(CircleCalculator(5, "area"));
        // OUTPUT: 78.53975


        // ============================================================
        // 4. RECTANGLE CALCULATOR
        // ============================================================

        int RectangleCalculator(int length, int width, string type)
        {
            if (type == "area")
                return length * width;

            if (type == "perimeter")
                return 2 * (length + width);

            return -1;
        }

        // Java equivalent:
        /*
        static int rectangleCalculator(
                int length, int width, String type) {

            if (type.equals("area"))
                return length * width;

            if (type.equals("perimeter"))
                return 2 * (length + width);

            return -1;
        }
        */

        Console.WriteLine(RectangleCalculator(10, 5, "area"));
        // OUTPUT: 50


        // ============================================================
        // 5. TRIANGLE TYPE
        // ============================================================

        string TriangleType(int a, int b, int c)
        {
            if (a == b && b == c)
                return "Equilateral";

            if (a == b || b == c || a == c)
                return "Isosceles";

            return "Scalene";
        }

        // Java equivalent:
        /*
        static String triangleType(int a, int b, int c) {
            if (a == b && b == c)
                return "Equilateral";

            if (a == b || b == c || a == c)
                return "Isosceles";

            return "Scalene";
        }
        */

        Console.WriteLine(TriangleType(5, 5, 3));
        // OUTPUT: Isosceles


        // ============================================================
        // 6. BILL CALCULATOR
        // ============================================================

        double CalculateBill(double amount)
        {
            if (amount >= 1000)
                return amount * 0.8;   // 20% discount

            if (amount >= 500)
                return amount * 0.9;   // 10% discount

            return amount;
        }

        // Java equivalent:
        /*
        static double calculateBill(double amount) {
            if (amount >= 1000)
                return amount * 0.8;

            if (amount >= 500)
                return amount * 0.9;

            return amount;
        }
        */

        Console.WriteLine(CalculateBill(1200));
        // OUTPUT: 960


        // ============================================================
        // 7. SPEED CHECK
        // ============================================================

        string CheckSpeed(int speed)
        {
            if (speed > 100)
                return "Overspeeding";

            if (speed >= 60)
                return "Normal Speed";

            return "Too Slow";
        }

        // Java equivalent:
        /*
        static String checkSpeed(int speed) {
            if (speed > 100)
                return "Overspeeding";

            if (speed >= 60)
                return "Normal Speed";

            return "Too Slow";
        }
        */

        Console.WriteLine(CheckSpeed(120));
        // OUTPUT: Overspeeding


        // ============================================================
        // 8. GRADE CALCULATOR
        // ============================================================

        string GetGrade(int marks)
        {
            if (marks >= 90 && marks <= 100)
                return "A";

            if (marks >= 80)
                return "B";

            if (marks >= 70)
                return "C";

            if (marks >= 60)
                return "D";

            if (marks >= 0)
                return "Fail";

            return "Invalid Marks";
        }

        // Java equivalent:
        /*
        static String getGrade(int marks) {
            if (marks >= 90 && marks <= 100)
                return "A";

            if (marks >= 80)
                return "B";

            if (marks >= 70)
                return "C";

            if (marks >= 60)
                return "D";

            if (marks >= 0)
                return "Fail";

            return "Invalid Marks";
        }
        */

        Console.WriteLine(GetGrade(85));
        // OUTPUT: B


        // ============================================================
        // 9. LOGIN SYSTEM
        // ============================================================

        string Login(string user, string pass)
        {
            if (user == "admin" && pass == "1234")
                return "Admin Login";

            if (user == "user" && pass == "1111")
                return "User Login";

            return "Invalid Credentials";
        }

        // Java equivalent:
        /*
        static String login(String user, String pass) {
            if (user.equals("admin") && pass.equals("1234"))
                return "Admin Login";

            if (user.equals("user") && pass.equals("1111"))
                return "User Login";

            return "Invalid Credentials";
        }
        */

        Console.WriteLine(Login("admin", "1234"));
        // OUTPUT: Admin Login


        // ============================================================
        // JAVA vs C# FUNCTION DIFFERENCE
        // ============================================================

        // Java:
        // static int add(int a, int b) {
        //     return a + b;
        // }
        //
        // C#:
        // static int Add(int a, int b) {
        //     return a + b;
        // }

        // C# also supports LOCAL FUNCTIONS:
        // int Add(int a, int b) => a + b;
        //
        // Java does NOT allow declaring a normal method directly
        // inside another method.

        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}
