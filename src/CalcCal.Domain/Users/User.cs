﻿using System.Text.Json.Serialization;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using CalcCal.Domain.Users.DomainEvents;
using MongoDB.Bson.Serialization.Attributes;
using Responses.DB;

namespace CalcCal.Domain.Users;

public sealed class User : Entity<UserId>
{
    public PhoneNumber PhoneNumber { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Username Username { get; private set; }
    public IReadOnlyCollection<EatenFood> EatenFood => _eatenFood;
    [BsonElement("EatenFood")]
    // ReSharper disable once FieldCanBeMadeReadOnly.Local (Bson needs)
    private List<EatenFood> _eatenFood;
    public VerificationCode? VerificationCode { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsPhoneNumberVerified { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastLoggedInAt { get; private set; }

    private User(
        PhoneNumber phoneNumber,
        FirstName firstName,
        LastName lastName,
        Username username,
        string passwordHash) : base(new UserId())
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PasswordHash = passwordHash;
        IsPhoneNumberVerified = false;
        CreatedAt = DateTime.UtcNow;
        LastLoggedInAt = DateTime.UtcNow;
        VerificationCode = null;
        _eatenFood = new List<EatenFood>();
    }

    [BsonConstructor]
    [JsonConstructor]
    private User() : base()
    {

    }


    public static Result<User> Create(string countryCode, string phoneNumber, string firstName, string lastName, string username, string passwordHash)
    {
        var phoneNumberResult = PhoneNumber.Create(countryCode, phoneNumber);

        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure<User>(phoneNumberResult.Error);
        }

        var firstNameResult = FirstName.Create(firstName);

        if (firstNameResult.IsFailure)
        {
            return Result.Failure<User>(firstNameResult.Error);
        }

        var lastNameResult = LastName.Create(lastName);

        if (lastNameResult.IsFailure)
        {
            return Result.Failure<User>(lastNameResult.Error);
        }

        var usernameResult = Username.Create(username);

        if (usernameResult.IsFailure)
        {
            return Result.Failure<User>(usernameResult.Error);
        }

        var user = new User(phoneNumberResult.Value, firstNameResult.Value, lastNameResult.Value, usernameResult.Value, passwordHash);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.PhoneNumber));

        return user;
    }

    public void LogIn() => LastLoggedInAt = DateTime.UtcNow;

    public void VerifyPhoneNumber() => IsPhoneNumberVerified = true;

    public Result ChangePassword(string newPasswordHash)
    {
        if (VerificationCode is not null)
        {
            return Result.Failure(UserErrors.VerificationCodeNotVerified);
        }

        PasswordHash = newPasswordHash;

        return Result.Success();
    }

    public Result Eat(Food food, decimal gramsQuantity)
    {
        var eatenFoodResult = Users.EatenFood.Create(food, gramsQuantity);

        if (eatenFoodResult.IsFailure)
        {
            Result.Failure(UserErrors.InvalidFood);
        }

        _eatenFood.Add(eatenFoodResult.Value);

        return Result.Success();
    }

    public Result SetVerificationCode(string? code)
    {
        if (code is null)
        {
            VerificationCode = null;
            return Result.Success();
        }

        var verificationCodeCreateResult = VerificationCode.Create(code);

        if (verificationCodeCreateResult.IsFailure)
        {
            return Result.Failure(verificationCodeCreateResult.Error);
        }

        VerificationCode = verificationCodeCreateResult.Value;

        return Result.Success();
    }

    public Result VerifyCode(string code)
    {
        if (VerificationCode is null || !VerificationCode.IsValid())
        {
            return Result.Failure(UserErrors.VerificationCodeExpired);
        }

        var isValid = VerificationCode.Verify(code);

        if (!isValid)
        {
            return Result.Failure(UserErrors.VerificationCodeIncorrect);
        }

        SetVerificationCode(null);
        return Result.Success();

    }
}