using System.Text;
using System.Text.Json;

namespace OllamaDemo.Model
{
    public class OllamaClient
    {
        private readonly HttpClient _http;

        public OllamaClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:11434/"),
                Timeout = TimeSpan.FromMinutes(20)
            };
        }

        public async Task<OllamaResponse> AskAsync(string model, string prompt)
        {
            var request = new OllamaRequest
            {
                Model = model,
                Prompt = prompt,
                Stream = false,
                KeepAlive = "30m",
                Options = new OllamaOptions
                {
                    Temperature = 0.1,
                    TopP = 0.9,
                    NumPredict = 1024
                }
            };

            string json = JsonSerializer.Serialize(request);

            using var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response =
                await _http.PostAsync("api/generate", content);

            string body = await response.Content.ReadAsStringAsync();

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine($"Status : {(int)response.StatusCode} {response.StatusCode}");
            Console.WriteLine("========================================");
            //Console.WriteLine(body);
            //Console.WriteLine("========================================");
            Console.WriteLine();

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<OllamaResponse>(
                body,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
                throw new InvalidOperationException("Unable to deserialize Ollama response.");

            return result;
        }

        public async Task<string> AskTextAsync(string model, string prompt)
        {
            var result = await AskAsync(model, prompt);

            if (!string.IsNullOrWhiteSpace(result.Response))
                return result.Response;

            if (!string.IsNullOrWhiteSpace(result.Thinking))
                return result.Thinking;

            return string.Empty;
        }
    }
}