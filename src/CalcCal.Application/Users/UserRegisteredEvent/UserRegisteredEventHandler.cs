using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Users;
using CalcCal.Domain.Users.DomainEvents;
using MediatR;

namespace CalcCal.Application.Users.UserRegisteredEvent
{
    internal sealed class UserRegisteredEventHandler : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IPhoneService _phoneService;

        public UserRegisteredEventHandler(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            await _phoneService.SendVerificationCodeAsync(notification.phoneNumber, cancellationToken);
        }
    }
}
