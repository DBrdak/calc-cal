using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Phone
{
    public sealed class SmsGatewayOptionsSetup : IConfigureOptions<SmsGatewayOptions>
    {
        private const string sectionName = "SmsGateway";
        private readonly IConfiguration _configuration;

        public SmsGatewayOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SmsGatewayOptions options)
        {
            _configuration.GetSection(sectionName).Bind(options);
        }
    }
}
