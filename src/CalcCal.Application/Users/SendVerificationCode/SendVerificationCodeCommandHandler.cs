using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Application.Models;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.SendVerificationCode;

internal sealed class SendVerificationCodeCommandHandler : ICommandHandler<SendVerificationCodeCommand, UserDetailedModel>
{
    private readonly IPhoneService _phoneService;
    private readonly IUserRepository _userRepository;

    public SendVerificationCodeCommandHandler(IPhoneService phoneService, IUserRepository userRepository)
    {
        _phoneService = phoneService;
        _userRepository = userRepository;
    }

    public async Task<Result<UserDetailedModel>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
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

        var sendResult = await _phoneService.SendVerificationCodeAsync(phoneNumber, cancellationToken);

        if (sendResult.IsFailure)
        {
            return Result.Failure<UserDetailedModel>(sendResult.Error);
        }

        var code = sendResult.Value;

        var setVerificationCodeResult = user.SetVerificationCode(code);

        if (setVerificationCodeResult.IsFailure)
        {
            return Result.Failure<UserDetailedModel>(setVerificationCodeResult.Error);
        }

        var userUpdateResult = await _userRepository.Update(user, cancellationToken);

        return userUpdateResult.IsSuccess ? 
            UserDetailedModel.FromDomainObject(user) :
            Result.Failure<UserDetailedModel>(userUpdateResult.Error);
    }
}