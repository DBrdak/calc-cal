using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Chat.Models.ChatCompletionResponse
{
    public class ChatCompletionResponse
    {
        public string Id { get; set; }
        public List<Choice> Choices { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public string Object { get; set; }
        public object SystemFingerprint { get; set; }
        public Usage Usage { get; set; }
    }
}
