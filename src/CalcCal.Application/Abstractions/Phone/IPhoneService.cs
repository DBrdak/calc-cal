using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace CalcCal.Application.Abstractions.Phone;

public interface IPhoneService
{
    Task<Result> SendVerificationMessage(string message);
}