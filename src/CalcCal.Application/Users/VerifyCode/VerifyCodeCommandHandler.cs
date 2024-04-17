using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.VerifyCode
{
    internal sealed class VerifyCodeCommandHandler : ICommandHandler<VerifyCodeCommand, UserDetailedModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly Dictionary<VerificationType, Action<User>> _verificationTypesActions = new()
        {
            {VerificationType.PhoneNumberVerification, VerifyPhoneNumber}
        };

        public VerifyCodeCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDetailedModel>> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var verificationTypeCreateResult = VerificationType.Create(request.VerificationType);

            if (verificationTypeCreateResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(verificationTypeCreateResult.Error);
            }

            var verificationType = verificationTypeCreateResult.Value;

            var phoneNumberCreateResult = PhoneNumber.Create(request.CountryCode, request.PhoneNumber);

            if (phoneNumberCreateResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(phoneNumberCreateResult.Error);
            }

            var phoneNumber = phoneNumberCreateResult.Value;

            var userGetResult = await _userRepository.GetUserByPhoneNumber(phoneNumber, cancellationToken);

            if (userGetResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(userGetResult.Error);
            }

            var user = userGetResult.Value;

            var verificationResult = user.VerifyCode(request.Code);

            if (verificationResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(verificationResult.Error);
            }

            _verificationTypesActions.GetValueOrDefault(verificationType)?.Invoke(user);

            var updateResult = await _userRepository.Update(user, cancellationToken);

            return updateResult.IsSuccess ?
                Result.Success(UserDetailedModel.FromDomainObject(user)) :
                Result.Failure<UserDetailedModel>(updateResult.Error);
        }

        private static void VerifyPhoneNumber(User user)
        {
            user.VerifyPhoneNumber();
        }
    }
}
