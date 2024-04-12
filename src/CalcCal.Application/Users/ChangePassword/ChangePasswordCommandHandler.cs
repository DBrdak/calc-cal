using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDetailedModel>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var usernameCreateResult = Username.Create(request.Username ?? "");

            var phoneNumberCreateResult = PhoneNumber.Create(request.CountryCode ?? "", request.PhoneNumber ?? "");

            var userGetResult = Result.Failure<User>(Error.NotFound("User not found"));

            if (usernameCreateResult.IsSuccess)
            {
                userGetResult = await _userRepository.GetUserByUsername(usernameCreateResult.Value, cancellationToken);
            }
            else if (phoneNumberCreateResult.IsSuccess)
            {
                userGetResult = await _userRepository.GetUserByPhoneNumber(phoneNumberCreateResult.Value, cancellationToken);
            }

            if (userGetResult.IsFailure)
            {
                return Result.Failure<UserDetailedModel>(userGetResult.Error);
            }

            var user = userGetResult.Value;

            user.ChangePassword(request.NewPassword);

            var userUpdateResult = await _userRepository.Update(user, cancellationToken);

            return userUpdateResult.IsFailure ?
                    Result.Failure<UserDetailedModel>(userUpdateResult.Error) :
                    Result.Success(UserDetailedModel.FromDomainObject(userUpdateResult.Value));
        }
    }
}
