using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcCal.Domain.Users;
using Responses.DB;

namespace CalcCal.Infrastructure.Services
{
    internal sealed class PhoneService : IPhoneService
    {
        public async Task<Result<string>> SendVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
        {
            return Result.Success("");
        }
    }
}
