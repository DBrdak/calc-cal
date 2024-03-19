using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CalcCal.Domain.Foods;
using CalcCal.Domain.Shared;
using Responses.DB;

namespace CalcCal.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        public PhoneNumber PhoneNumber { get; private set; }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public IReadOnlyCollection<Food> EatenFood => _eatenFood.AsReadOnly();
        private readonly List<Food> _eatenFood;
        public string PasswordHash { get; private set; }
        public bool IsEmailVerified { get; private set; }
        public DateTime CreatedAt { get; init; }
        public DateTime LastLoggedInAt { get; private set; }

        private User(
            PhoneNumber phoneNumber,
            FirstName firstName,
            LastName lastName,
            string passwordHash)
        {
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            IsEmailVerified = true; // TODO temporary solution
            CreatedAt = DateTime.UtcNow;
            LastLoggedInAt = DateTime.UtcNow;
            _eatenFood = new List<Food>();
        }

        [JsonConstructor]
        private User()
        {

        }


        public static Result<User> Create(string phoneNumber, string firstName, string lastName, string passwordHash)
        {
            var phoneNumberResult = PhoneNumber.Create(phoneNumber);

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

            return new User(phoneNumberResult.Value, firstNameResult.Value, lastNameResult.Value, passwordHash);
        }

        public void LogIn() => LastLoggedInAt = DateTime.UtcNow;

        public void VerifyEmail() => IsEmailVerified = true;
    }
}
