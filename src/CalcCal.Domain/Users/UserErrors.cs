using Responses.DB;

namespace CalcCal.Domain.Users;

public static class UserErrors
{
    public static Error InvalidPhoneNumber =>
        new Error(
            "Users.InvalidPhoneNumber",
            "Invalid phone number");
    public static Error InvalidFirstName =>
        new Error(
            "Users.InvalidFirstName",
            "Invalid first name");
    public static Error InvalidLastName =>
        new Error(
            "Users.InvalidLastName",
            "Invalid last name");
    public static Error InvalidUsername =>
        new Error(
            "Users.InvalidUsername",
            "Invalid username");

    public static Error InvalidFood =>
        new Error(
            "Users.InvalidFood",
            "Invalid eaten food data");

    public static Error InvalidCredentials =>
        new Error(
            "Users.InvalidCredentials",
            "Invalid credentials");

    public static Error InvalidVerificationCode =>
        new(
            "Users. InvalidVerificationCode",
            "Invalid verification code");

    public static Error VerificationCodeExpired =>
        new(
            "Users.VerificationCodeExpired",
            "Verification code has expired");
    public static Error VerificationCodeIncorrect =>
        new(
            "Users.VerificationCodeIncorrect",
            "Verification code incorrect");

    public static Error VerificationCodeNotVerified =>
        new(
            "Users.VerificationCodeNotVerified",
            "Verify the verification code");
}