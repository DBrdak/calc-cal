using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Phone.Blowerio
{
    internal sealed class BlowerioOptionsSetup : IConfigureOptions<BlowerioOptions>
    {
        private const string sectionName = "Blowerio";
        private readonly IConfiguration _configuration;

        public BlowerioOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(BlowerioOptions options)
        {
            _configuration.GetSection(sectionName).Bind(options);
        }
    }
}
