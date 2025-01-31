using DarkPeakLabs.Rdap.Values;
using System.Collections.Generic;

namespace DarkPeakLabs.Rdap
{
    public class RdapContactPhoneNumber
    {
        public IReadOnlyList<RdapPhoneNumberType> Types { get; set; }
        public string Value { get; set; }
    }
}
