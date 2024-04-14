using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Users;
using Microsoft.Extensions.Options;
using Responses.DB;
using Telnyx;
using PhoneNumber = CalcCal.Domain.Users.PhoneNumber;

namespace CalcCal.Infrastructure.Phone
{
    internal sealed class PhoneService : IPhoneService
    {
        private readonly SmsGatewayOptions _options;

        public PhoneService(IOptions<SmsGatewayOptions> options)
        {
            _options = options.Value;
        }

        public async Task<Result<string>> SendVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            //TODO implement
            TelnyxConfiguration.SetApiKey(_options.ApiKey);

            var service = new MessagingSenderIdService();

            var options = new NewMessagingSenderId
            {
                From = _options.SenderPhoneNumber,
                To = phoneNumber.ToString(),
                Text = "Hello, World!"
            };

            var messageResponse = await service.CreateAsync(options);

            return Result.Success("");
        }
    }
}
