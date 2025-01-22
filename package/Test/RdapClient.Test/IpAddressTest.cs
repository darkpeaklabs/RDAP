using System;
using DarkPeakLabs.Rdap.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class IpAddressTest
    {
        private readonly RdapIpBootstrap ipBootstrap = new RdapIpBootstrap();

        [TestMethod]
        public void TestIpAddressLookup()
        {
            string address = "100.64.255.46";
            Uri serviceUri = ipBootstrap.FindServiceUrlAsync(address).GetAwaiter().GetResult();
            using RdapClient client = new RdapClient();
            var response = client.IpLookupAsync(serviceUri, address).GetAwaiter().GetResult();
        }
    }
}
