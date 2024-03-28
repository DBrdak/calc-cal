using System.Net;
using Microsoft.Extensions.Options;

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

            return base.SendAsync(request, cancellationToken);
        }
    }
}
