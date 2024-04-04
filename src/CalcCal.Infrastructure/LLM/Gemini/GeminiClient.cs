using System.Text;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Responses.DB;

namespace CalcCal.Infrastructure.LLM.Gemini;

internal sealed class GeminiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GeminiClient> _logger;
    private readonly JsonSerializerSettings _serializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };

    public GeminiClient(HttpClient httpClient, ILogger<GeminiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<string>> GenerateContentAsync(string prompt, CancellationToken cancellationToken)
    {
        var requestBody = GeminiRequestFactory.CreateRequest(prompt);
        var content = new StringContent(JsonConvert.SerializeObject(requestBody, Formatting.None, _serializerSettings), Encoding.UTF8, "application/json");
            
        var response = await _httpClient.PostAsync("", content, cancellationToken);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(responseBody);

            return Result.Failure<string>(
                responseBody.Contains("User location is not supported for the API use.") ?
                    Error.TaskFailed("Feature is not available in your region") :
                    Error.TaskFailed("Something went wrong while processing your prompt"));
        }

        var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseBody);

        var geminiResponseText = geminiResponse?.Candidates[0].Content.Parts[0].Text;

        return geminiResponseText;
    }
}