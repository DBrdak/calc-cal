using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CalcCal.Infrastructure.Chat
{
    internal sealed class ChatCompletionDelegatingHandler : DelegatingHandler
    {
        private readonly ChatOptions _chatOptions;

        public ChatCompletionDelegatingHandler(ChatOptions chatOptions)
        {
            _chatOptions = chatOptions;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {_chatOptions.ApiKey}");
            request.Headers.Add("Content-Type", $"application/json");

            return base.SendAsync(request, cancellationToken);
        }
    }
}
