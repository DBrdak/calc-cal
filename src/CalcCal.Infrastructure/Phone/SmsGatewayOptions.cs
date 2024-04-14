using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Phone
{
    public sealed class SmsGatewayOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string SenderPhoneNumber { get; set; } = string.Empty;
    }
}
