using System.Text.Json.Serialization;

namespace OllamaDemo.Model
{
    public class OllamaRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("stream")]
        public bool Stream { get; set; }

        [JsonPropertyName("keepAlive")]
        public string KeepAlive { get; set; } = "30m";

        [JsonPropertyName("ollamaOptions")]
        public OllamaOptions Options { get; set; } = new();
    }

    public class OllamaOptions
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 0.1;

        [JsonPropertyName("topP")]
        public double TopP { get; set; } = 0.9;

        [JsonPropertyName("numPredict")]
        public int NumPredict { get; set; } = 1024;
    }
}
