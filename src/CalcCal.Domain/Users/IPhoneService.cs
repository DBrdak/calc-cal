using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Domain.Users;

public interface IPhoneService
{
    Task<Result<string>> SendVerificationCodeAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
}