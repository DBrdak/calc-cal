using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Users.SendVerificationCode;
using CalcCal.Domain.Users;
using CalcCal.Domain.Users.DomainEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CalcCal.Application.Users.UserRegisteredEvent;

internal sealed class UserRegisteredEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public UserRegisteredEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var sender = _serviceProvider.GetRequiredService<ISender>();

        await sender.Send(
            new SendVerificationCodeCommand(
                notification.phoneNumber.CountryCode,
                notification.phoneNumber.Value),
            cancellationToken);
    }
}