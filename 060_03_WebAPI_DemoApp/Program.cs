using System.Net.Http.Json;
using WebApis.Models;

namespace WebApis;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("        3. Web API Demonstration");
        Console.WriteLine("========================================");

        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

        try
        {
            // --------------------------------------------------
            // GET
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("1. GET Request");
            Console.WriteLine("--------------------------------");

            HttpResponseMessage response = await client.GetAsync("posts/1");

            Console.WriteLine(
                $"Status: {(int)response.StatusCode} " +
                $"{response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);

            // --------------------------------------------------
            // GET as object
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("2. GET as C# object");
            Console.WriteLine("--------------------------------");

            Post? post = await client.GetFromJsonAsync<Post>("posts/1");

            if (post != null)
            {
                Console.WriteLine($"ID: {post.Id}");
                Console.WriteLine($"User ID: {post.UserId}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Body: {post.Body}");
            }

            // --------------------------------------------------
            // POST
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("3. POST Request");
            Console.WriteLine("--------------------------------");

            Post newPost = new Post
            {
                UserId = 10,
                Title = "C# Web API",
                Body = "This record was created from C#."
            };

            HttpResponseMessage postResponse = await client.PostAsJsonAsync("posts", newPost);

            Console.WriteLine(
                $"Status: {(int)postResponse.StatusCode} " +
                $"{postResponse.StatusCode}");

            string postResult = await postResponse.Content.ReadAsStringAsync();

            Console.WriteLine(postResult);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine();
            Console.WriteLine("HTTP Error:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}