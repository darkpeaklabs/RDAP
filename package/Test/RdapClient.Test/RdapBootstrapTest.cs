using System;
using DarkPeakLabs.Rdap.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class RdapBootstrapTest
    {
        private readonly RdapDnsBootstrap dnsBootstrap = new RdapDnsBootstrap();
        private readonly RdapIpBootstrap ipBootstrap = new RdapIpBootstrap();

        public RdapBootstrapTest()
        {
        }

        [TestMethod]
        public void TestBootstrapDomains()
        {
            Uri url = dnsBootstrap.FindServiceUrlAsync("microsoft.com").GetAwaiter().GetResult();
            Assert.AreEqual(new Uri("https://rdap.verisign.com/com/v1/"), url);
        }

        [TestMethod]
        public void TestBootstrapTld()
        {
            Uri url = dnsBootstrap.FindServiceUrlAsync("com").GetAwaiter().GetResult();
            Assert.AreEqual(new Uri("https://rdap.iana.org"), url);
        }

        [TestMethod]
        public void TestBootstrapIP()
        {
            Uri url = ipBootstrap.FindServiceUrlAsync("100.64.255.46").GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestBootstrapDomainsException()
        {
            Assert.ThrowsException<RdapBootstrapException>(() => dnsBootstrap.FindServiceUrlAsync("microsoft.qwerty").GetAwaiter().GetResult());
        }

        [TestMethod]
        public void TestBootstrapDomainsIdn()
        {
            string name = "webwhois.nic.嘉里";
            dnsBootstrap.FindServiceUrlAsync(name).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void TestBootstrapObjectTags()
        {
            //RdapObjectTagsServiceRegistryCache registry = new RdapObjectTagsServiceRegistryCache(new RdapLocalStorage());
            //registry.FindServiceUrl("ARIN");
        }
    }
}
