﻿using System.Text.Json.Serialization;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using CalcCal.Domain.Users.DomainEvents;
using Responses.DB;

namespace CalcCal.Domain.Users;

public sealed class User : Entity<UserId>
{
    public PhoneNumber PhoneNumber { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Username Username { get; private set; }
    public IReadOnlyCollection<EatenFood> EatenFood => _eatenFood.AsReadOnly();
    private readonly List<EatenFood> _eatenFood;
    public string PasswordHash { get; private set; }
    public bool IsPhoneNumberVerified { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastLoggedInAt { get; private set; }

    private User(
        PhoneNumber phoneNumber,
        FirstName firstName,
        LastName lastName,
        Username username,
        string passwordHash)
    {
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PasswordHash = passwordHash;
        IsPhoneNumberVerified = false; // TODO temporary solution
        CreatedAt = DateTime.UtcNow;
        LastLoggedInAt = DateTime.UtcNow;
        _eatenFood = new List<EatenFood>();
    }

    [JsonConstructor]
    private User()
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

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public void LogIn() => LastLoggedInAt = DateTime.UtcNow;

    public void VerifyPhoneNumber() => IsPhoneNumberVerified = true;

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
}