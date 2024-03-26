namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;

internal sealed class SafetySetting
{
    public string Category { get; set; }
    public string Threshold { get; set; }
}