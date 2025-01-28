using System.Text.Json.Nodes;
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
        }

        [TestMethod]
        public void Test2()
        {
            RdapSecureDns rdapSecureDns = RdapSerializer.Deserialize<RdapSecureDns>(ReadJsonFile("rdap-secure-dns-2.json"));
        }

        [TestMethod]
        public void Test3()
        {
            JsonNode node = RdapSerializer.Deserialize<JsonNode>(ReadJsonFile("rdap-secure-dns-2.json"));
        }
    }
}
