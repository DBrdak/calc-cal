using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

namespace CalcCal.Infrastructure.LLM.Gemini;

internal sealed class GeminiRequestFactory
{
    public static GeminiRequest CreateRequest(string prompt)
    {
        return new GeminiRequest
        {
            Contents = new GeminiContent[]
            {
                new ()
                {
                    Role = "user",
                    Parts = new GeminiPart[]
                    {
                        new()
                        {
                            Text = prompt
                        }
                    }
                }
            },
            GenerationConfig = new GenerationConfig
            {
                Temperature = 0,
                TopK = 1,
                TopP = 1,
                MaxOutputTokens = 2048,
                StopSequences = new List<object>()
            },
            SafetySettings = new SafetySettings[]
            {
                new()
                {
                    Category = "HARM_CATEGORY_HARASSMENT",
                    Threshold = "BLOCK_ONLY_HIGH"
                },
                new()
                {
                    Category = "HARM_CATEGORY_HATE_SPEECH",
                    Threshold = "BLOCK_ONLY_HIGH"
                },
                new()
                {
                    Category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                    Threshold = "BLOCK_ONLY_HIGH"
                },
                new()
                {
                    Category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                    Threshold = "BLOCK_ONLY_HIGH"
                }
            }
        };
    }
}