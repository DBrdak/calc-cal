using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiRequest;
using CalcCal.Infrastructure.LLM.Gemini.Models.GeminiResponse;

namespace CalcCal.Infrastructure.LLM.Gemini
{
    internal sealed class GeminiRequestFactory
    {
        public static GeminiRequest CreateRequest(string prompt)
        {
            return new GeminiRequest
            {
                Contents = new []
                {
                    new GeminiContents
                    {
                        Role = "user",
                        Parts = new []
                        {
                            new GeminiPart
                            {
                                Text = prompt
                            }
                        }
                    }
                }
            };
        }
    }
}
