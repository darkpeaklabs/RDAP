using DarkPeakLabs.Rdap.Values.Json;
using System.Collections.Generic;

namespace DarkPeakLabs.Rdap
{
    public class RdapContactPhoneNumber
    {
        public IReadOnlyCollection<RdapPhoneNumberType> Types { get; set; }
        public string Value { get; set; }
    }
}