using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;
using Newtonsoft.Json;
using Responses.DB;

namespace CalcCal.Infrastructure.LLM.Gemini
{
    internal sealed class GeminiService
    {
        private readonly HttpClient _httpClient;

        public GeminiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GenerateContentAsync(string prompt, CancellationToken cancellationToken)
        {
            var requestBody = GeminiRequestFactory.CreateRequest(prompt);
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("", content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(responseBody);

            var geminiResponseText = geminiResponse?.Candidates[0].Content.Parts[0].Text;

            return geminiResponseText;
        }
    }
}
