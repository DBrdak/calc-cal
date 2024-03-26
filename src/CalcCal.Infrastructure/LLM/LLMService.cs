using CalcCal.Application.Abstractions.LLM;
using CalcCal.Application.Food.AddFood;
using CalcCal.Infrastructure.LLM.Gemini;
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

        public async Task<Result<string>> SendPromptAsync(Prompt prompt, CancellationToken cancellationToken)
        {
            var response = await _geminiService.GenerateContentAsync(prompt.Value, cancellationToken);

            return response ?? 
                   Result.Failure<string>(Error.TaskFailed("Problem while reading response from Gemini"));
        }
    }
}
