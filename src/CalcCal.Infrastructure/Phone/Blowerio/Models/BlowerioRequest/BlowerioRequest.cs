using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcCal.Infrastructure.Phone.Blowerio.Models.BlowerioRequest
{
    internal sealed record BlowerioRequest(string To, string Message)
    {
        public IEnumerable<KeyValuePair<string, string>> AsForm() =>
            new List<KeyValuePair<string, string>>
            {
                new ("to", To),
                new ("message", Message),
            };
    }
}
