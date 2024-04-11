namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal sealed class SafetyRating
{
    public string Category { get; set; }
    public string Probability { get; set; }
}