using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Users
{
    public interface IPhoneService
    {
        Task<Result> SendVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);

        Task<Result> VerifyCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
