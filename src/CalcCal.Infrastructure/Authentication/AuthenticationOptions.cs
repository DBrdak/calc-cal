﻿namespace CalcCal.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; } = string.Empty;

    public int ExpireInMinutes { get; init; } = 300;

    public string SecretKey { get; init; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;
}