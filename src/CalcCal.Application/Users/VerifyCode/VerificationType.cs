using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Application.Users.VerifyCode
{
    internal sealed record VerificationType
    {
        public string Value { get; init; }
        private static readonly Error createError = new (
            "VerificationType.CreateError",
            "Invalid Verification Type");

        private VerificationType(string value) => Value = value;

        public static VerificationType PhoneNumberVerification => new("phone number verification");
        public static VerificationType Other => new("other");

        public static Result<VerificationType> Create(string value) =>
            All.FirstOrDefault(x => x.Value == value.ToLower()) 
            ?? Result.Failure<VerificationType>(createError);

        public static IReadOnlyCollection<VerificationType> All =>
            new List<VerificationType>
            {
                PhoneNumberVerification,
                Other
            };
    }
}
