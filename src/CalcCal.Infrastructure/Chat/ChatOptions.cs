using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Chat
{
    public sealed class ChatOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string ChatUrl { get; set; } = string.Empty;
        public string ChatModel { get; set; } = string.Empty;
    }
}
