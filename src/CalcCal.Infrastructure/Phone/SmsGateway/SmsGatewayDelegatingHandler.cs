using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;

namespace CalcCal.Infrastructure.Phone.SmsGateway
{
    internal sealed class SmsGatewayDelegatingHandler : DelegatingHandler
    {
        private readonly SmsGatewayOptions _options;

        public SmsGatewayDelegatingHandler(IOptions<SmsGatewayOptions> options)
        {
            _options = options.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var (username, password) = GetCredentialsFromUrl(_options.Url);

            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            request.Headers.Add("Accepts", "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            return base.SendAsync(request, cancellationToken);
        }

        private static (string Username, string Password) GetCredentialsFromUrl(string url)
        {
            var credentials = url.Substring(
                url.IndexOf("//", StringComparison.Ordinal) + 2,
                url.LastIndexOf('@') - url.IndexOf("//", StringComparison.Ordinal) - 2
            ).Split(':');

            return (credentials[0], credentials[1]);
        }
    }
}
