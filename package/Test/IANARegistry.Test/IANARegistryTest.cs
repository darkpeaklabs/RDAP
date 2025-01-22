using DarkPeakLabs.Rdap.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class IANARegistryTest
    {
        [TestMethod]
        public void TestIANARegistrarIds()
        {
            using IANARegistryClient client = new IANARegistryClient();
            var registrarIds = client.GetRegistrarIds();
            Assert.IsNotNull(registrarIds);
            Assert.IsTrue(registrarIds.Any());
        }

        [TestMethod]
        public void TestRdapJsonValues()
        {
            using IANARegistryClient client = new IANARegistryClient();
            var jsonValues = client.GetRdapJsonValues();
            Assert.IsNotNull(jsonValues);
            Assert.IsTrue(jsonValues.Any());
        }

        [TestMethod]
        public void TestLinkRelations()
        {
            using IANARegistryClient client = new IANARegistryClient();
            var linkRelations = client.GetLinkRelations();
            Assert.IsNotNull(linkRelations);
            Assert.IsTrue(linkRelations.Any());
        }
    }
}
