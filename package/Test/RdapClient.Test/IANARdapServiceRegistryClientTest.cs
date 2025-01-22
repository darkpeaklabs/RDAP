using DarkPeakLabs.Rdap.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class IANARdapServiceRegistryClientTest
    {
        [TestMethod]
        public void TestGetRegistry()
        {
            using IANARdapServiceRegistryClient client = new IANARdapServiceRegistryClient();
            var registry = client.GetDnsRegistry();
            Assert.IsNotNull(registry?.Services);
            Assert.IsTrue(registry.Services.Count > 0);

            registry = client.GetAsnRegistry();
            Assert.IsNotNull(registry?.Services);
            Assert.IsTrue(registry.Services.Count > 0);

            var objectTagsRegistry = client.GetObjectTagsRegistry();
            Assert.IsNotNull(objectTagsRegistry?.Services);
            Assert.IsTrue(objectTagsRegistry.Services.Any());

            var ipv4registry = client.GetIPv4Registry();
            var ipv6registry = client.GetIPv6Registry();
        }
    }
}
