namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal class Candidate
{
    public Content Content { get; set; }
    public string FinishReason { get; set; }
    public int Index { get; set; }
    public SafetyRating[] SafetyRatings { get; set; }
}