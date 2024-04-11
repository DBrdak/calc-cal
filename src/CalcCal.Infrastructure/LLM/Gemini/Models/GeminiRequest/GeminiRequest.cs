using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;

internal sealed class GeminiRequest
{
    public GeminiContent[] Contents { get; set; }
    public GenerationConfig GenerationConfig { get; set; }
    public SafetySettings[] SafetySettings { get; set; }
}