using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.ReSendVerificationCode
{
    internal sealed class ReSendVerificationCodeCommandHandler : ICommandHandler<ReSendVerificationCodeCommand>
    {
        private readonly IPhoneService _phoneService;

        public ReSendVerificationCodeCommandHandler(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        public async Task<Result> Handle(ReSendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var phoneNumberCreateResult = PhoneNumber.Create(request.CountryCode, request.PhoneNumber);

            if (phoneNumberCreateResult.IsFailure)
            {
                return Result.Failure(phoneNumberCreateResult.Error);
            }

            var phoneNumber = phoneNumberCreateResult.Value;

            return await _phoneService.SendVerificationCodeAsync(phoneNumber, cancellationToken);
        }
    }
}
