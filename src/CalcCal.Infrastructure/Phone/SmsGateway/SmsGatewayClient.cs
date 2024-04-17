using CalcCal.Infrastructure.Phone.SmsGateway.Models.BlowerioRequest;
using Microsoft.Extensions.Logging;
using Responses.DB;

namespace CalcCal.Infrastructure.Phone.SmsGateway
{
    internal sealed class SmsGatewayClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SmsGatewayClient> _logger;
        private const string smsSendPath = "messages";
        private readonly Error _sendingError = new(
            "Blowerio.SendFailure",
            "Failed to send message");

        public SmsGatewayClient(HttpClient httpClient, ILogger<SmsGatewayClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Result> SendSmsAsync(string to, string message, CancellationToken cancellationToken)
        {
            var request = new FormUrlEncodedContent(new BlowerioRequest(to, message).AsForm());

            var response = await _httpClient.PostAsync(smsSendPath, request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Message send failure, reason: {message}", errorMessage);

            return Result.Failure(_sendingError);
        }
    }
}
