using DarkPeakLabs.Rdap.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class RdapSecureDnsTest : JsonTestBase
    {
        [TestMethod]
        public void Test1()
        {
            RdapSecureDns rdapSecureDns = RdapSerializer.Deserialize<RdapSecureDns>(ReadJsonFile("rdap-secure-dns-1.json"));
            Assert.IsNull(rdapSecureDns.DnsDsRecords);
        }

        [TestMethod]
        public void Test2()
        {
            RdapSecureDns rdapSecureDns = RdapSerializer.Deserialize<RdapSecureDns>(ReadJsonFile("rdap-secure-dns-2.json"));
            Assert.IsNotNull(rdapSecureDns.DnsDsRecords);
        }
    }
}
