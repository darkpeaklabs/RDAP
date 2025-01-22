using DarkPeakLabs.Rdap.Conformance;
using DarkPeakLabs.Rdap.Serialization;
using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class UnitTestEntity
    {
        private static string ReadJsonFile(string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", filename);
            using StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd();
        }

        [TestMethod]
        public void TestJCardParser()
        {
            RdapEntity entity = RdapSerializer.Deserialize<RdapEntity>(ReadJsonFile("jcard.json"));
            Assert.IsNotNull(entity.Contact);
            Assert.AreEqual(entity.Contact.FullName, "Firstname Lastname");

            Assert.IsNotNull(entity.Contact.Address);
            Assert.IsTrue(entity.Contact.Address.Count() == 10);
            Assert.IsTrue(entity.Contact.Address.ToList()[7] == "Overijssel");

            Assert.IsNotNull(entity.Contact.PhoneNumbers);

            Assert.IsTrue(entity.Contact.PhoneNumbers.Count() == 2);

            var phoneNumbers = entity.Contact.PhoneNumbers.ToList();
            Assert.AreEqual(phoneNumbers[0].Value, "+31.681429692");
            Assert.IsNotNull(phoneNumbers[0].Types);
            Assert.IsTrue(phoneNumbers[0].Types.Any());
            Assert.AreEqual(phoneNumbers[0].Types.ToList()[0], RdapPhoneNumberType.Voice);
            Assert.AreEqual(phoneNumbers[1].Value, "+31.681429888");
            Assert.IsNotNull(phoneNumbers[1].Types);
            Assert.IsTrue(phoneNumbers[1].Types.Count() == 2);
            Assert.AreEqual(phoneNumbers[1].Types.ToList()[0], RdapPhoneNumberType.Fax);
            Assert.AreEqual(phoneNumbers[1].Types.ToList()[1], RdapPhoneNumberType.Cell);

            Assert.IsNotNull(entity.Contact.Emails);

            Assert.IsTrue(entity.Contact.Emails.Any());
            Assert.AreEqual(entity.Contact.Emails.First(), "behave@rtr-dev.com");
            Assert.IsNotNull(entity.Contact.Nicknames);
            Assert.IsTrue(entity.Contact.Nicknames.Any());
            Assert.IsNotNull(entity.Contact.Names);
            Assert.IsTrue(entity.Contact.Names.Any());
        }

        [DataTestMethod]
        [DataRow("jcard_invalid_1.json")]
        public void TestInvalidJCard(string filename)
        {
            RdapEntity entity = RdapSerializer.Deserialize<RdapEntity>(ReadJsonFile(filename), out RdapConformance conformance);
            foreach (RdapEntity item in entity.Entities)
            {
                Assert.IsTrue(item.Contact == null || item.Contact.FullName == null);
            }
            Assert.IsTrue(conformance.Violations != null && conformance.Violations.Count > 0);
        }

        [DataTestMethod]
        [DataRow("jcard_invalid_2.json")]
        public void TestInvalidJCard2(string filename)
        {
            RdapEntity entity = RdapSerializer.Deserialize<RdapEntity>(ReadJsonFile(filename), out RdapConformance conformance);
            Assert.IsNull(entity.Contact.FullName);
            Assert.IsTrue(conformance.Violations != null && conformance.Violations.Count > 0);
        }

        [DataTestMethod]
        [DataRow("jcard_invalid_3.json")]
        public void TestInvalidJCard3(string filename)
        {
            RdapEntity entity = RdapSerializer.Deserialize<RdapEntity>(ReadJsonFile(filename), out RdapConformance conformance);
            foreach (RdapEntity item in entity.Entities)
            {
                Assert.IsNotNull(item.Contact);
                Assert.IsNotNull(item.Contact.FullName);
            }
            Assert.AreEqual(entity.Entities.ToList()[1].Contact.Kind, RdapContactKind.Organization);
            Assert.IsTrue(conformance.Violations != null && conformance.Violations.Count > 0);
        }
    }
}
