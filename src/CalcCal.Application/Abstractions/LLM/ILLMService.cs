using Responses.DB;
// ReSharper disable InconsistentNaming

namespace CalcCal.Application.Abstractions.LLM
{
    public interface ILLMService
    {
        Task<Result<string>> SendPromptAsync(string prompt);
    }
}
