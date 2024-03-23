namespace CalcCal.Infrastructure.Chat.Models.ChatCompletionResponse;

public class Message
{
    public string Content { get; set; }
    public string Role { get; set; }
    public object FunctionCall { get; set; }
    public object ToolCalls { get; set; }
}