using System.Text;
using CalcCal.Application.Abstractions.LLM;
using CalcCal.Infrastructure.LLM.Gemini;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Responses.DB;
// ReSharper disable InconsistentNaming

namespace CalcCal.Infrastructure.LLM
{
    internal sealed class LLMService : ILLMService
    {
        private readonly GeminiService _geminiService;

        public LLMService(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        public async Task<Result<string>> SendPromptAsync(string prompt)
        {
            var response = await _geminiService.GenerateContent(prompt);

            return response ?? 
                   Result.Failure<string>(Error.TaskFailed("Problem while reading response from Gemini"));
        }
    }
}
