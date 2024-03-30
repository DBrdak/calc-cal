namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal sealed class PromptFeedback
{
    public SafetyRating[] SafetyRatings { get; set; }
}