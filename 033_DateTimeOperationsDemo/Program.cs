namespace _33_DateTimeOperationsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 32_DateTimeDemo ===");
            Console.WriteLine("Java vs C# Date & Time");
            Console.WriteLine();


            // =================================================================
            // JAVA → C# DATE & TIME QUICK GRID
            // =================================================================

            // Java                         → C#
            // ---------------------------------------------------------------
            // LocalDate                    → DateTime.Today
            // LocalDateTime                → DateTime.Now
            // LocalTime                    → DateTime.Now.TimeOfDay
            // ZonedDateTime                → DateTimeOffset + TimeZoneInfo
            // Instant                      → DateTimeOffset.UtcNow
            // Duration                     → TimeSpan
            // Period                       → AddDays/AddMonths/AddYears
            // ZoneId                       → TimeZoneInfo
            // ZoneOffset                   → TimeSpan
            //
            // Main difference:
            // Java provides separate date/time classes.
            // C# commonly uses DateTime, DateTimeOffset and TimeSpan.


            // =================================================================
            // 1. CURRENT DATE / TIME
            // =================================================================

            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;

            Console.WriteLine("Current DateTime: " + now);
            Console.WriteLine("Current Date: " +
                              today.ToString("yyyy-MM-dd"));

            // Java:
            // LocalDateTime now = LocalDateTime.now();
            // LocalDate today = LocalDate.now();


            // UTC
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;

            Console.WriteLine("UTC: " + utcNow);

            // Java:
            // Instant utcNow = Instant.now();


            // Local time only
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            Console.WriteLine("Current Time: " + currentTime);

            // Java:
            // LocalTime currentTime = LocalTime.now();


            // =================================================================
            // 2. CREATE DATE / DATE-TIME
            // =================================================================

            DateTime date = new DateTime(2026, 8, 12);

            DateTime dateTime = new DateTime(2026, 8, 12, 15, 30, 0);

            Console.WriteLine("Date: " +
                              date.ToString("yyyy-MM-dd"));

            Console.WriteLine("DateTime: " +
                              dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            // Java:
            // LocalDate date =
            //     LocalDate.of(2026, 8, 12);
            //
            // LocalDateTime dateTime =
            //     LocalDateTime.of(2026, 8, 12, 15, 30);


            // =================================================================
            // 3. GET DATE COMPONENTS
            // =================================================================

            Console.WriteLine("Year       : " + date.Year);
            Console.WriteLine("Month      : " + date.Month);
            Console.WriteLine("Day        : " + date.Day);
            Console.WriteLine("DayOfWeek  : " + date.DayOfWeek);
            Console.WriteLine("DayOfYear  : " + date.DayOfYear);

            Console.WriteLine("Hour       : " + dateTime.Hour);
            Console.WriteLine("Minute     : " + dateTime.Minute);
            Console.WriteLine("Second     : " + dateTime.Second);

            // Java:
            // date.getYear();
            // date.getMonthValue();
            // date.getDayOfMonth();
            // date.getDayOfWeek();
            // date.getDayOfYear();
            //
            // dateTime.getHour();
            // dateTime.getMinute();
            // dateTime.getSecond();
            //
            // Difference:
            // Java uses methods.
            // C# uses properties.


            // =================================================================
            // 4. ADD / SUBTRACT DAYS
            // =================================================================

            DateTime plus10Days = today.AddDays(10);
            DateTime minus10Days = today.AddDays(-10);

            Console.WriteLine("+10 Days: " +
                              plus10Days.ToString("yyyy-MM-dd"));

            Console.WriteLine("-10 Days: " +
                              minus10Days.ToString("yyyy-MM-dd"));

            // Java:
            // today.plusDays(10);
            // today.minusDays(10);
            //
            // C# uses a negative value for subtraction.


            // =================================================================
            // 5. ADD / SUBTRACT MONTHS
            // =================================================================

            DateTime plus2Months = today.AddMonths(2);
            DateTime minus2Months = today.AddMonths(-2);

            Console.WriteLine("+2 Months: " +
                              plus2Months.ToString("yyyy-MM-dd"));

            Console.WriteLine("-2 Months: " +
                              minus2Months.ToString("yyyy-MM-dd"));

            // Java:
            // today.plusMonths(2);
            // today.minusMonths(2);


            // =================================================================
            // 6. ADD / SUBTRACT YEARS
            // =================================================================

            DateTime plus1Year = today.AddYears(1);
            DateTime minus1Year = today.AddYears(-1);

            Console.WriteLine("+1 Year: " +
                              plus1Year.ToString("yyyy-MM-dd"));

            Console.WriteLine("-1 Year: " +
                              minus1Year.ToString("yyyy-MM-dd"));

            // Java:
            // today.plusYears(1);
            // today.minusYears(1);


            // =================================================================
            // 7. DATE COMPARISON
            // =================================================================

            DateTime d1 = new DateTime(2026, 4, 22);
            DateTime d2 = new DateTime(2026, 6, 10);

            Console.WriteLine("d1 < d2 : " + (d1 < d2));
            Console.WriteLine("d1 > d2 : " + (d1 > d2));
            Console.WriteLine("d1 == d2: " + (d1 == d2));

            // OUTPUT:
            // d1 < d2 : True
            // d1 > d2 : False
            // d1 == d2: False

            // Java:
            // d1.isBefore(d2);
            // d1.isAfter(d2);
            // d1.isEqual(d2);
            //
            // Difference:
            // Java uses methods.
            // C# supports comparison operators.


            // =================================================================
            // 8. DAYS IN MONTH / LEAP YEAR
            // =================================================================

            int daysInMonth =
                DateTime.DaysInMonth(
                    today.Year,
                    today.Month);

            bool leapYear =
                DateTime.IsLeapYear(today.Year);

            Console.WriteLine("Days in Month: " + daysInMonth);
            Console.WriteLine("Leap Year: " + leapYear);

            // Java:
            // date.lengthOfMonth();
            // date.isLeapYear();


            // =================================================================
            // 9. DATE FORMATTING
            // =================================================================

            Console.WriteLine(
                "ISO Date: " +
                today.ToString("yyyy-MM-dd"));

            Console.WriteLine(
                "Full Date: " +
                today.ToString("ddd, d MMMM yyyy"));

            Console.WriteLine(
                "DateTime: " +
                now.ToString("yyyy-MM-dd HH:mm:ss"));

            // OUTPUT example:
            // ISO Date: 2026-08-16
            // Full Date: Sun, 16 August 2026
            // DateTime: 2026-08-16 01:30:00

            // Java:
            // date.format(
            //     DateTimeFormatter.ofPattern("yyyy-MM-dd"));
            //
            // dateTime.format(
            //     DateTimeFormatter.ofPattern(
            //         "yyyy-MM-dd HH:mm:ss"));


            // =================================================================
            // 10. PARSING
            // =================================================================

            DateTime parsedDate =
                DateTime.Parse("2026-08-12");

            DateTime parsedExact =
                DateTime.ParseExact(
                    "2026-08-12",
                    "yyyy-MM-dd",
                    null);

            Console.WriteLine(
                "Parsed: " +
                parsedDate.ToString("yyyy-MM-dd"));

            Console.WriteLine(
                "Parsed Exact: " +
                parsedExact.ToString("yyyy-MM-dd"));

            // Java:
            // LocalDate.parse("2026-08-12");
            //
            // LocalDate.parse(
            //     "2026-08-12",
            //     DateTimeFormatter.ofPattern("yyyy-MM-dd"));


            // =================================================================
            // 11. DATE DIFFERENCE
            // =================================================================

            TimeSpan difference = d2 - d1;

            Console.WriteLine(
                "Difference Days: " +
                difference.TotalDays);

            // OUTPUT:
            // Difference Days: 49

            // Java:
            // long days =
            //     ChronoUnit.DAYS.between(d1, d2);


            // =================================================================
            // 12. DURATION / TIMESPAN
            // =================================================================

            DateTime start = DateTime.Now;

            // Simulate some elapsed time.
            DateTime end = start.AddHours(2)
                                .AddMinutes(30);

            TimeSpan duration = end - start;

            Console.WriteLine(
                "Hours: " + duration.TotalHours);

            Console.WriteLine(
                "Minutes: " + duration.TotalMinutes);

            Console.WriteLine(
                "Seconds: " + duration.TotalSeconds);

            // Java:
            // Duration duration =
            //     Duration.between(start, end);
            //
            // duration.toHours();
            // duration.toMinutes();
            // duration.getSeconds();
            //
            // C#:
            // TimeSpan duration = end - start;


            // =================================================================
            // 13. COMBINE DATE + TIME
            // =================================================================

            DateTime startOfDay = today.Date;

            DateTime afternoon =
                today.Date
                    .AddHours(15)
                    .AddMinutes(30);

            Console.WriteLine(
                "Start of Day: " +
                startOfDay.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine(
                "3:30 PM: " +
                afternoon.ToString("yyyy-MM-dd HH:mm:ss"));

            // Java:
            // date.atStartOfDay();
            // date.atTime(15, 30);


            // =================================================================
            // 14. DATE → TIME
            // =================================================================

            TimeSpan timeOnly = now.TimeOfDay;

            Console.WriteLine(
                "Time Only: " + timeOnly);

            // Java:
            // LocalTime timeOnly = LocalTime.now();


            // =================================================================
            // 15. START / END OF DAY
            // =================================================================

            DateTime dayStart = today.Date;

            DateTime dayEnd =
                today.Date
                    .AddDays(1)
                    .AddTicks(-1);

            Console.WriteLine(
                "Day Start: " +
                dayStart.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine(
                "Day End: " +
                dayEnd.ToString("yyyy-MM-dd HH:mm:ss"));

            // OUTPUT:
            // Day Start: 2026-08-16 00:00:00
            // Day End:   2026-08-16 23:59:59


            // =================================================================
            // 16. TIME ZONES
            // =================================================================

            DateTime utc = DateTime.UtcNow;

            Console.WriteLine("UTC: " + utc);

            TimeZoneInfo localZone =
                TimeZoneInfo.Local;

            Console.WriteLine(
                "Local Zone: " +
                localZone.Id);

            // Java:
            // ZoneId zone = ZoneId.of("UTC");
            // ZonedDateTime.now(zone);
            //
            // C#:
            // TimeZoneInfo


            // =================================================================
            // 17. DATETIMEOFFSET
            // =================================================================
            // Useful when the UTC offset must be preserved.

            DateTimeOffset offsetNow =
                DateTimeOffset.Now;

            Console.WriteLine(
                "DateTimeOffset: " +
                offsetNow);

            Console.WriteLine(
                "Offset: " +
                offsetNow.Offset);

            // Java:
            // OffsetDateTime.now();


            // =================================================================
            // 18. TICKS
            // =================================================================
            // C# DateTime uses ticks.
            // 1 tick = 100 nanoseconds.

            Console.WriteLine(
                "Ticks: " + now.Ticks);

            // Java:
            // LocalDateTime supports nanoseconds.
            //
            // C#:
            // DateTime → Ticks
            // 1 tick = 100 ns


            // =================================================================
            // 19. MODIFY DATE
            // =================================================================

            DateTime firstDayOfMonth =
                new DateTime(
                    today.Year,
                    today.Month,
                    1);

            DateTime december =
                new DateTime(
                    today.Year,
                    12,
                    today.Day);

            Console.WriteLine(
                "First Day: " +
                firstDayOfMonth.ToString("yyyy-MM-dd"));

            Console.WriteLine(
                "December: " +
                december.ToString("yyyy-MM-dd"));

            // Java:
            // date.withDayOfMonth(1);
            // date.withMonth(12);


            // =================================================================
            // 20. IMPORTANT JAVA → C# DIFFERENCES
            // =================================================================

            // Java                              C#
            // -----------------------------------------------------------------
            // LocalDate                         DateTime
            // LocalDateTime                     DateTime
            // LocalTime                         TimeSpan / TimeOfDay
            // ZonedDateTime                     DateTimeOffset + TimeZoneInfo
            // Instant                           DateTimeOffset.UtcNow
            // Duration                          TimeSpan
            // Period                            AddDays/AddMonths/AddYears
            // date.getYear()                    date.Year
            // date.getMonthValue()              date.Month
            // date.getDayOfMonth()              date.Day
            // date.plusDays(5)                  date.AddDays(5)
            // date.minusDays(5)                 date.AddDays(-5)
            // date.plusMonths(2)                date.AddMonths(2)
            // date.plusYears(1)                 date.AddYears(1)
            // date.isBefore(d2)                 date < d2
            // date.isAfter(d2)                  date > d2
            // date.isEqual(d2)                  date == d2
            // date.format(...)                  date.ToString(...)
            // LocalDate.parse(...)              DateTime.Parse(...)
            // Duration.between(...)             end - start


            // =================================================================
            // 21. QUICK JAVA → C# GRID
            // =================================================================

            // JAVA                              C#
            // -----------------------------------------------------------------
            // LocalDate.now()                  DateTime.Today
            // LocalDateTime.now()              DateTime.Now
            // Instant.now()                    DateTimeOffset.UtcNow
            // LocalTime.now()                  DateTime.Now.TimeOfDay
            //
            // LocalDate.of(...)                new DateTime(...)
            // LocalDateTime.of(...)            new DateTime(...)
            //
            // getYear()                        Year
            // getMonthValue()                  Month
            // getDayOfMonth()                  Day
            // getDayOfWeek()                   DayOfWeek
            // getDayOfYear()                   DayOfYear
            //
            // plusDays()                       AddDays()
            // plusMonths()                     AddMonths()
            // plusYears()                      AddYears()
            //
            // isBefore()                       <
            // isAfter()                        >
            // isEqual()                        ==
            //
            // DateTimeFormatter                Format string
            // Duration                         TimeSpan
            // ZoneId                           TimeZoneInfo


            Console.WriteLine();
            Console.WriteLine("Done.");
        }
    }
}
