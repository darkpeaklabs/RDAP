using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapSerializerExtensions
    {
        public static IReadOnlyList<RdapJsonProperty> GetJsonProperties(this Type type)
        {
            return type.GetProperties()
                .Select(x => new RdapJsonProperty(x))
                .Where(x => x.IsJsonProperty)
                .ToList();
        }
    }
}
