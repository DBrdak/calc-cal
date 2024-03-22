﻿using CommonAbstractions.DB.Messaging;

namespace CalcCal.Application.Users.LogIn;

public sealed record LogInCommand(string Username, string Password) : ICommand<AccessToken>
{
}