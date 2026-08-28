using System;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait_18;

//dotnet add package Newtonsoft.Json
/*
  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="13.0.4" />
  </ItemGroup>
*/
public class Program
{
    //Sample Post Data API - Learning Url
    //https://dummyjson.com/docs/products
    public static async Task Main(string[] args)
    {
        Console.WriteLine("=== AsyncAwait 18 ===");
        Console.WriteLine("async and await");
        Console.WriteLine();


        // ============================================================
        // ASYNC / AWAIT
        // ============================================================
        // C# uses async/await for asynchronous operations.
        // Java has a similar concept using CompletableFuture, but the syntax is different.

        await RunAsync();

        static async Task RunAsync()
        {
            Console.WriteLine("Starting...");

            // Simulates an asynchronous operation.
            await Task.Delay(1000);

            //Get Data Call
            string retJson = await GetRemoteData();

            Console.WriteLine(retJson);
            Console.WriteLine();

            //Post Data Call
            string resJson = await PostRemoteData();

            Console.WriteLine(resJson);
            Console.WriteLine();


            Console.WriteLine("Completed.");
        }

        // OUTPUT:
        // Starting...
        // Completed.


        // Java equivalent:
        //
        // static CompletableFuture<Void> runAsync() {
        //     System.out.println("Starting...");
        //
        //     return CompletableFuture.runAsync(() -> {
        //         try {
        //             Thread.sleep(500);
        //         } catch (InterruptedException e) {
        //             Thread.currentThread().interrupt();
        //         }
        //
        //         System.out.println("Completed.");
        //     });
        // }


        // ============================================================
        // C# → JAVA
        // ============================================================
        //
        // C#                         Java
        // ------------------------------------------------------------
        // async                       CompletableFuture
        // await                       thenApply() / thenCompose()
        // Task                        CompletableFuture
        // Task.Delay()                delayed async operation
        //
        // C# async/await syntax is generally simpler.


        Console.WriteLine();

        Console.WriteLine("Done.");
    }

    public static async Task<string> GetRemoteData()
    {
        string url = "https://jsonplaceholder.typicode.com/posts/1";

        using HttpClient client = new HttpClient();

        // GET request
        string json = await client.GetStringAsync(url);

        return json;
    }

    //https://dummyjson.com/docs/products
    public static async Task<string> PostRemoteData()
    {
        string url = "https://dummyjson.com/products/add";
        string json = """{"title":"BMW Pencil"}""";

        using HttpClient client = new();
        using StringContent content = new(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);
        string result = await response.Content.ReadAsStringAsync();

        return result;
    }

}