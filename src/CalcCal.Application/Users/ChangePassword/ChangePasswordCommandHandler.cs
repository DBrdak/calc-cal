using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Application.Users.UserRegisteredEvent;
using CalcCal.Domain.Users;
using CalcCal.Domain.Users.DomainEvents;
using Responses.DB;

namespace CalcCal.Application.Users.ChangePassword
{
    internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, UserDetailedModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private const string passwordPattern = @"^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<Result<UserDetailedModel>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!Regex.IsMatch(request.NewPassword, passwordPattern))
            {
                return Result.Failure<UserDetailedModel>(Error.InvalidRequest("Password is too weak"));
            }

            var phoneNumberCreateResult = PhoneNumber.Create(request.CountryCode ?? "", request.PhoneNumber ?? "");

            var userGetResult = await _userRepository.GetUserByPhoneNumber(phoneNumberCreateResult.Value, cancellationToken);

            if (userGetResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(userGetResult.Error);
            }

            var user = userGetResult.Value;

            var verificationResult = user.VerifyCode(request.VerificationCode);

            if (verificationResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(verificationResult.Error);
            }

            var passwordHash = _passwordService.HashPassword(request.NewPassword);
            var changePasswordResult = user.ChangePassword(passwordHash);

            if (changePasswordResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(changePasswordResult.Error);
            }

            var userUpdateResult = await _userRepository.Update(user, cancellationToken);

            return userUpdateResult.IsFailure ?
                    Result.Failure<UserDetailedModel>(userUpdateResult.Error) :
                    Result.Success(UserDetailedModel.FromDomainObject(userUpdateResult.Value));
        }
    }
}
