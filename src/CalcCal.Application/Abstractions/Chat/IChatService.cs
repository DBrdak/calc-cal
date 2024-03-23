using Responses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Application.Abstractions.Chat
{
    public interface IChatService
    {
        Task<Result<string>> SendChatCompletionAsync(string message);
    }
}
