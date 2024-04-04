using CalcCal.Application.Abstractions.LLM;
using CalcCal.Application.Food.AddFood;
using CalcCal.Infrastructure.LLM.Gemini;
using Responses.DB;
// ReSharper disable InconsistentNaming

namespace CalcCal.Infrastructure.LLM;

internal sealed class LLMService : ILLMService
{
    private readonly GeminiClient _geminiClient;

    public LLMService(GeminiClient geminiClient)
    {
        _geminiClient = geminiClient;
    }

    public async Task<Result<string>> SendPromptAsync(Prompt prompt, CancellationToken cancellationToken)
    {
        var response = await _geminiClient.GenerateContentAsync(prompt.Value, cancellationToken);

        return response;
    }
}