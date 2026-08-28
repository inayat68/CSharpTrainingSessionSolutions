using System;

namespace FactoryPatternDemo_51;

// ============================================================
// 1. INTERFACE
// ============================================================

public interface INotification
{
    void Send(string message);
}

// ============================================================
// 2. CONCRETE CLASSES
// ============================================================

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

// ============================================================
// 3. FACTORY
// ============================================================

public static class NotificationFactory
{
    public static INotification Create(string type)
    {
        return type.ToLower() switch
        {
            "email" => new EmailNotification(),
            "sms" => new SmsNotification(),
            _ => throw new ArgumentException("Invalid notification type")
        };
    }
}

// ============================================================
// 4. MAIN PROGRAM
// ============================================================

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Factory Pattern ===");

        // Factory creates the required object.
        INotification notification =
            NotificationFactory.Create("email");

        notification.Send("Welcome Ali!");
        // OUTPUT: Email: Welcome Ali!

        notification =
            NotificationFactory.Create("sms");

        notification.Send("Your OTP is 1234");
        // OUTPUT: SMS: Your OTP is 1234
    }
}