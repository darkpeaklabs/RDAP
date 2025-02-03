using System;
using System.Linq;
using DarkPeakLabs.Rdap.Serialization;
using DarkPeakLabs.Rdap.Values;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class RdapSerializerTest: JsonTestBase
    {
        private ILoggerFactory _loggerFactory;
        ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerFactory = LoggerFactory.Create(config =>
            {
                config.SetMinimumLevel(LogLevel.Debug);
                config.AddDebug();
            });

            _logger = _loggerFactory.CreateLogger<RdapSerializerTest>();
        }


        [TestMethod]
        public void Test1()
        {
            RdapDomainLookupResponse response = RdapSerializer.Deserialize<RdapDomainLookupResponse>(ReadJsonFile("domain-1.json"), logger: _logger);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.ObjectClass, RdapObjectClass.Domain);
            Assert.IsNotNull(response.Handle);
            Assert.AreEqual(response.Handle.Value, "2336799_DOMAIN_COM-VRSN");
            Assert.AreEqual(response.Handle.Tag, "VRSN");
            Assert.AreEqual(response.LDHName, "EXAMPLE.COM");

            Assert.IsNotNull(response.Links);
            Assert.AreEqual(response.Links.Count, 1);
            var link = response.Links.FirstOrDefault();
            Assert.IsNotNull(link);
            Assert.AreEqual(link.Value, "https://rdap.verisign.com/com/v1/domain/EXAMPLE.COM");
            Assert.AreEqual(link.Href, "https://rdap.verisign.com/com/v1/domain/EXAMPLE.COM");
            Assert.AreEqual(link.Relation, RdapLinkRelationType.Self);
            Assert.AreEqual(link.Type, "application/rdap+json");

            Assert.IsNotNull(response.Status);
            Assert.AreEqual(response.Status.Count, 3);
            Assert.IsTrue(response.Status.Contains(RdapStatus.ClientDeleteProhibited));
            Assert.IsTrue(response.Status.Contains(RdapStatus.ClientTransferProhibited));
            Assert.IsTrue(response.Status.Contains(RdapStatus.ClientUpdateProhibited));

            Assert.IsNotNull(response.Entities);
            Assert.AreEqual(response.Entities.Count, 1);
            var entity = response.Entities.FirstOrDefault();
            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.ObjectClass, RdapObjectClass.Entity);
            Assert.AreEqual(entity.Handle.Value, "376");
            Assert.IsNotNull(entity.Roles);
            Assert.AreEqual(entity.Roles.Count, 1);
            var entityRole = entity.Roles.FirstOrDefault();
            Assert.IsNotNull(entityRole);
            Assert.AreEqual(entityRole, RdapEntityRole.Registrar);
            Assert.IsNotNull(entity.PublicIds);
            Assert.AreEqual(entity.PublicIds.Count, 1);
            var entityPublicId = entity.PublicIds.FirstOrDefault();
            Assert.IsNotNull(entityPublicId);
            Assert.AreEqual(entityPublicId.Type, "IANA Registrar ID");
            Assert.AreEqual(entityPublicId.Identifier, "376");

            Assert.IsNotNull(entity.Entities);
            Assert.AreEqual(entity.Entities.Count, 1);
            var entityEntity = entity.Entities.FirstOrDefault();
            Assert.IsNotNull(entityEntity);
            Assert.AreEqual(entityEntity.ObjectClass, RdapObjectClass.Entity);
            Assert.IsNotNull(entityEntity.Roles);
            Assert.AreEqual(entityEntity.Roles.Count, 1);
            var entityEntityRole = entityEntity.Roles.FirstOrDefault();
            Assert.IsNotNull(entityEntityRole);
            Assert.AreEqual(entityEntityRole, RdapEntityRole.Abuse);

            Assert.IsNotNull(response.Events);
            Assert.AreEqual(response.Events.Count, 4);
            for (int i = 0; i < response.Events.Count; i++)
            {
                Assert.IsNotNull(response.Events[i]);
            }
            Assert.AreEqual(response.Events[0].EventAction, RdapEventAction.Registration);
            Assert.AreEqual(response.Events[0].EventDate, DateTimeOffset.Parse("1995-08-14T04:00:00Z"));
            Assert.AreEqual(response.Events[1].EventAction, RdapEventAction.Expiration);
            Assert.AreEqual(response.Events[1].EventDate, DateTimeOffset.Parse("2025-08-13T04:00:00Z"));
            Assert.AreEqual(response.Events[2].EventAction, RdapEventAction.LastChanged);
            Assert.AreEqual(response.Events[2].EventDate, DateTimeOffset.Parse("2024-08-14T07:01:34Z"));
            Assert.AreEqual(response.Events[3].EventAction, RdapEventAction.LastUpdateOfRDAPDatabase);
            Assert.AreEqual(response.Events[3].EventDate, DateTimeOffset.Parse("2025-01-28T17:58:39Z"));

            Assert.IsNotNull(response.SecureDNS);
            Assert.AreEqual(response.SecureDNS.DelegationSigned, true);
            Assert.IsNotNull(response.SecureDNS.DnsDsRecords);
            Assert.AreEqual(response.SecureDNS.DnsDsRecords.Count, 1);
            var dnsDsRecords = response.SecureDNS.DnsDsRecords.FirstOrDefault();
            Assert.IsNotNull(dnsDsRecords);
            Assert.AreEqual(dnsDsRecords.KeyTag, 370);
            Assert.AreEqual(dnsDsRecords.Algorithm, DnsSecAlgorithmType.ECDSACurveP256WithSHA256);
            Assert.AreEqual(dnsDsRecords.DigestType, DnsSecDigestType.SHA256);
            Assert.AreEqual(dnsDsRecords.Digest, "BE74359954660069D5C63D200C39F5603827D7DD02B56F120EE9F3A86764247C");

            Assert.IsNotNull(response.NameServers);
            Assert.AreEqual(response.NameServers.Count, 2);
            Assert.IsNotNull(response.NameServers[0]);
            Assert.AreEqual(response.NameServers[0].ObjectClass, RdapObjectClass.NameServer);
            Assert.AreEqual(response.NameServers[0].LDHName, "A.IANA-SERVERS.NET");
            Assert.AreEqual(response.NameServers[1].ObjectClass, RdapObjectClass.NameServer);
            Assert.AreEqual(response.NameServers[1].LDHName, "B.IANA-SERVERS.NET");

            Assert.IsNotNull(response.Conformance);
            Assert.AreEqual(response.Conformance.Count, 3);
            Assert.AreEqual(response.Conformance[0], "rdap_level_0");
            Assert.AreEqual(response.Conformance[1], "icann_rdap_technical_implementation_guide_0");
            Assert.AreEqual(response.Conformance[2], "icann_rdap_response_profile_0");

            Assert.IsNotNull(response.Notices);
            Assert.AreEqual(response.Notices.Count, 3);
            for (int i = 0; i < response.Notices.Count; i++)
            {
                Assert.IsNotNull(response.Notices[i]);
                Assert.IsNotNull(response.Notices[i].Title);
                Assert.IsNotNull(response.Notices[i].Description);
                Assert.AreEqual(response.Notices[i].Description.Count, 1);
                Assert.IsNotNull(response.Notices[i].Description[0]);
                Assert.IsNotNull(response.Notices[i].Links);
                Assert.AreEqual(response.Notices[i].Links.Count, 1);
                Assert.IsNotNull(response.Notices[i].Links[0]);
                Assert.AreEqual(response.Notices[i].Links[0].Type, "text/html");
            }
            Assert.AreEqual(response.Notices[0].Title, "Terms of Use");
            Assert.AreEqual(response.Notices[0].Description[0], "Service subject to Terms of Use.");
            Assert.AreEqual(response.Notices[0].Links[0].Href, "https://www.verisign.com/domain-names/registration-data-access-protocol/terms-service/index.xhtml");
            Assert.AreEqual(response.Notices[1].Title, "Status Codes");
            Assert.AreEqual(response.Notices[1].Description[0], "For more information on domain status codes, please visit https://icann.org/epp");
            Assert.AreEqual(response.Notices[1].Links[0].Href, "https://icann.org/epp");
            Assert.AreEqual(response.Notices[2].Title, "RDDS Inaccuracy Complaint Form");
            Assert.AreEqual(response.Notices[2].Description[0], "URL of the ICANN RDDS Inaccuracy Complaint Form: https://icann.org/wicf");
            Assert.AreEqual(response.Notices[2].Links[0].Href, "https://icann.org/wicf");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _loggerFactory.Dispose();
        }
    }
}
