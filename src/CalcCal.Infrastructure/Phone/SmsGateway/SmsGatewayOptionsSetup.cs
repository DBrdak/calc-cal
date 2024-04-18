using Amazon.Util.Internal.PlatformServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CalcCal.Infrastructure.Phone.SmsGateway;

internal sealed class SmsGatewayOptionsSetup : IConfigureOptions<SmsGatewayOptions>
{
    private const string sectionName = "Blowerio";
    private const string productionSectionName = "BLOWERIO_URL";
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public SmsGatewayOptionsSetup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void Configure(SmsGatewayOptions options)
    {
        if (_env.IsDevelopment())
        {
            _configuration.GetSection(sectionName).Bind(options);
            return;
        }

        options.Url = _configuration.GetValue<string>(productionSectionName) ?? string.Empty;
    }
}