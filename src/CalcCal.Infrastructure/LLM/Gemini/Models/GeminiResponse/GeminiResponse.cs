namespace CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

internal sealed class GeminiResponse
{
    public Candidate[] Candidates { get; set; }
    public PromptFeedback PromptFeedback { get; set; }
}