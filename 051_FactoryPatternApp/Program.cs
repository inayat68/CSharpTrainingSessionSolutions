//using System;

namespace FactoryPatternDemo_51;

// ============================================================
// 1. INTERFACE
// ============================================================

public interface INotification
{
    void Send(string message);
}

// ------------------------------------------------------------
// JAVA EQUIVALENT
// ------------------------------------------------------------
//
// public interface INotification
// {
//     void send(String message);
// }


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

// ------------------------------------------------------------
// JAVA EQUIVALENT
// ------------------------------------------------------------
//
// public class EmailNotification implements INotification {
//
//     @Override
//     public void send(String message) {
//         System.out.println("Email: " + message);
//     }
// }


public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}

// ------------------------------------------------------------
// JAVA EQUIVALENT
// ------------------------------------------------------------
//
// public class SmsNotification implements INotification {
//
//     @Override
//     public void send(String message) {
//         System.out.println("SMS: " + message);
//     }
// }


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
            _ => throw new ArgumentException(
                "Invalid notification type")
        };
    }
}

// ------------------------------------------------------------
// JAVA EQUIVALENT
// ------------------------------------------------------------
//
// public class NotificationFactory {
//
//     public static INotification create(String type) {
//
//         switch (type.toLowerCase()) {
//
//             case "email":
//                 return new EmailNotification();
//
//             case "sms":
//                 return new SmsNotification();
//
//             default:
//                 throw new IllegalArgumentException(
//                     "Invalid notification type");
//         }
//     }
// }


// ============================================================
// 4. MAIN PROGRAM
// ============================================================

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Factory Pattern ===");

        // --------------------------------------------------------
        // Factory creates the required object.
        // --------------------------------------------------------

        INotification notification =
            NotificationFactory.Create("email");

        notification.Send("Welcome Ali!");

        // OUTPUT:
        // Email: Welcome Ali!


        notification =
            NotificationFactory.Create("sms");

        notification.Send("Your OTP is 1234");

        // OUTPUT:
        // SMS: Your OTP is 1234
    }
}

// ------------------------------------------------------------
// JAVA EQUIVALENT
// ------------------------------------------------------------
//
// public class Program {
//
//     public static void main(String[] args) {
//
//         System.out.println("=== Factory Pattern ===");
//
//         // Factory creates the required object.
//
//         INotification notification =
//             NotificationFactory.create("email");
//
//         notification.send("Welcome Ali!");
//
//         // OUTPUT:
//         // Email: Welcome Ali!
//
//
//         notification =
//             NotificationFactory.create("sms");
//
//         notification.send("Your OTP is 1234");
//
//         // OUTPUT:
//         // SMS: Your OTP is 1234
//     }
// }


// ============================================================
// C# → JAVA QUICK COMPARISON
// ============================================================
//
// C#                                  Java
// --------------------------------------------------------------
// interface                           interface
// : INotification                    implements INotification
// Console.WriteLine()                System.out.println()
// string                              String
// void Send()                         void send()
// public static                       public static
// ArgumentException                  IllegalArgumentException
// switch expression =>               switch / case
// new EmailNotification()            new EmailNotification()
// ToLower()                          toLowerCase()
// Main()                              main()
//


// ============================================================
// FACTORY PATTERN CONCEPT
// ============================================================
//
// Without Factory:
//
// INotification notification;
//
// if (type == "email")
//     notification = new EmailNotification();
//
// else if (type == "sms")
//     notification = new SmsNotification();
//
//
// With Factory:
//
// INotification notification =
//     NotificationFactory.Create(type);
//
// The calling code does not need to know which concrete
// class needs to be instantiated.
//
// ============================================================
//
// Client
//   │
//   │ Create("email")
//   ▼
// NotificationFactory
//   │
//   │ new EmailNotification()
//   ▼
// INotification
//   ▲
//   │
// EmailNotification
//
// ============================================================
//
// BENEFIT:
//
// Client code depends on:
//
//     INotification
//
// instead of:
//
//     EmailNotification
//     SmsNotification
//
// This makes it easier to add new notification types:
//
//     PushNotification
//     WhatsAppNotification
//     TeamsNotification
//     SlackNotification
//
// without changing the client code.
// ============================================================