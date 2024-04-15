using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SecurityToken.Internal;
using CalcCal.Infrastructure.Phone.Blowerio.Models.BlowerioRequest;
using Responses.DB;

namespace CalcCal.Infrastructure.Phone.Blowerio
{
    internal sealed class BlowerioClient
    {
        private readonly HttpClient _httpClient;
        private const string smsSendPath = "/messages";

        public BlowerioClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result> SendSmsAsync(string to, string message, CancellationToken cancellationToken)
        {
            var request = new FormUrlEncodedContent(new BlowerioRequest(to, message).AsForm());

            var response = await _httpClient.PostAsync(smsSendPath, request, cancellationToken);

            return Result.Success();
        }
    }
}
