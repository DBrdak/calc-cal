namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal class SafetyRating
{
    public string Category { get; set; }
    public string Probability { get; set; }
}