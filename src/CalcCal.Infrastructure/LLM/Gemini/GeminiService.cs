using System.Text;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CalcCal.Infrastructure.LLM.Gemini
{
    internal sealed class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiService> _logger;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public GeminiService(HttpClient httpClient, ILogger<GeminiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string?> GenerateContentAsync(string prompt, CancellationToken cancellationToken)
        {
            var requestBody = GeminiRequestFactory.CreateRequest(prompt);
            var content = new StringContent(JsonConvert.SerializeObject(requestBody, Formatting.None, _serializerSettings), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("", content, cancellationToken);

            var responseBody = await response.Content.ReadAsStringAsync();

            var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseBody);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(responseBody);
                return null;
            }

            var geminiResponseText = geminiResponse?.Candidates[0].Content.Parts[0].Text;

            return geminiResponseText;
        }
    }
}
