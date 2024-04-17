using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Users;
using CalcCal.Infrastructure.Phone.SmsGateway;
using Microsoft.Extensions.Options;
using Responses.DB;
using PhoneNumber = CalcCal.Domain.Users.PhoneNumber;

namespace CalcCal.Infrastructure.Phone
{
    internal sealed class PhoneService : IPhoneService
    {
        private readonly SmsGatewayClient _smsGatewayClient;
        private const int verificationCodeLength = 6;

        public PhoneService(SmsGatewayClient client)
        {
            _smsGatewayClient = client;
        }

        public async Task<Result<string>> SendVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            var verificationCode = GenerateVerificationCode();
            var message = GetVerificationCodeMessage(verificationCode);
            var to = phoneNumber.ToString();

            var sendResult = await _smsGatewayClient.SendSmsAsync(to, message, cancellationToken);

            return sendResult.IsSuccess 
                ? Result.Success(verificationCode) 
                : Result.Failure<string>(sendResult.Error);
        }

        private string GetVerificationCodeMessage(string code) => 
            @$"Your CalcCal verification code: {code}";

        private static string GenerateVerificationCode()
        {
            var rng = new Random();
            var verificationCode = string.Empty;

            for (var i = 0; i < verificationCodeLength; i++)
            {
                verificationCode += rng.Next(0,10).ToString();
            }

            return verificationCode;
        }
    }
}
