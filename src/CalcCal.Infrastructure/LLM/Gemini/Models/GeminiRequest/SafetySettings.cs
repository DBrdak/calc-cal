namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;

internal sealed class SafetySettings
{
    public string Category { get; set; }
    public string Threshold { get; set; }
}