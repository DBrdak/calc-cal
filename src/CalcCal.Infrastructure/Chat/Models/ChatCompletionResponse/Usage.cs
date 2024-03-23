namespace CalcCal.Infrastructure.Chat.Models.ChatCompletionResponse;

public class Usage
{
    public int CompletionTokens { get; set; }
    public int PromptTokens { get; set; }
    public int TotalTokens { get; set; }
}