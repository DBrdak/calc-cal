using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace CalcCal.Infrastructure.LLM.Gemini
{
    internal sealed class GeminiDelegatingHandler : DelegatingHandler
    {
        private readonly GeminiOptions _chatOptions;

        public GeminiDelegatingHandler(IOptions<GeminiOptions> chatOptions)
        {
            _chatOptions = chatOptions.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-goog-api-key", $"{_chatOptions.ApiKey}");
            request.Headers.Add("Content-Type", $"application/json");

            return base.SendAsync(request, cancellationToken);
        }
    }
}
