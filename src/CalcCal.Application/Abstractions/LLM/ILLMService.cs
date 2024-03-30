using CalcCal.Application.Food.AddFood;
using Responses.DB;
// ReSharper disable InconsistentNaming

namespace CalcCal.Application.Abstractions.LLM;

public interface ILLMService
{
    Task<Result<string>> SendPromptAsync(Prompt prompt, CancellationToken cancellationToken);
}