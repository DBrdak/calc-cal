using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Application.Abstractions.Messaging;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Application.Users.VerifyCode
{
    internal sealed class VerifyCodeCommandHandler : ICommandHandler<VerifyCodeCommand>
    {
        private readonly IPhoneService _phoneService;

        public VerifyCodeCommandHandler(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        public async Task<Result> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            return await _phoneService.VerifyCodeAsync(request.Code, cancellationToken);
        }
    }
}
