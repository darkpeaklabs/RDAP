using System;
using DarkPeakLabs.Rdap.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test;

[TestClass]
public class RdapClientTest
{
    private readonly RdapDnsBootstrap bootstrap = new RdapDnsBootstrap();

    [TestMethod]
    public void TestHelp()
    {
        using RdapClient client = new RdapClient();
        var help = client.HelpAsync(new Uri("https://rdap.verisign.com/com/v1/")).GetAwaiter().GetResult();
    }

    [TestMethod]
    public void TestDomainLookup()
    {
        using RdapClient service = new RdapClient();
        var result = service.DomainLookupAsync(new Uri("https://rdap.verisign.com/com/v1/"), "microsoft.com").GetAwaiter().GetResult();

        result = service.DomainLookupAsync(new Uri("https://rdap.markmonitor.com/rdap/"), "microsoft.com").GetAwaiter().GetResult();
        result = service.DomainLookupAsync(new Uri("https://rdap.markmonitor.com/rdap"), "microsoft.com").GetAwaiter().GetResult();        
    }

    [TestMethod]
    public void TestDomainLookup2()
    {
        using RdapClient service = new RdapClient();
        //squarespace requires user-agent header
        var result = service.DomainLookupAsync(new Uri("https://rdap.squarespace.domains"), "mixpanel.com").GetAwaiter().GetResult();
    }

    [TestMethod]
    public void TestDomainLookup3()
    {
        using RdapClient service = new RdapClient();
        var result = service.DomainLookupAsync(new Uri("https://namerdap.systems"), "lastpass.com").GetAwaiter().GetResult();
    }

    [TestMethod]
    public void TestRdapException()
    {
        using RdapClient service = new RdapClient();

        try
        {
            var result = service.DomainLookupAsync(new Uri("https://rdap.verisign.com/com/v1/"), "microsoft142424246282876.com").Result;
            Assert.Fail("RdapRequestException not thrown");
        }
        catch (AggregateException exception)
        {
            if (exception.InnerException != null && exception.InnerException is RdapRequestException)
            {
                //Pass
            }
            else
            {
                Assert.Fail("Expecting RdapRequestException");
            }
        }
    }

    [TestMethod]
    public void TestIdnDomainLookup()
    {
        string name = "㯙㯜㯙㯟.com";
        Uri serviceUri = bootstrap.FindServiceUrlAsync(name).GetAwaiter().GetResult();

        using RdapClient service = new RdapClient();
        var result = service.DomainLookupAsync(serviceUri, name).GetAwaiter().GetResult();
    }

    /// <summary>
    /// DNSSEC test zones from https://www.internetsociety.org/resources/deploy360/2013/dnssec-test-sites/
    /// </summary>
    /// <param name="value"></param>
    [DataTestMethod]
    [DataRow("internetsociety.org")]
    [DataRow("dnssec-tools.org")]
    [DataRow("dnssec-deployment.org")]
    [DataRow("dnssec-failed.org")]
    [DataRow("rhybar.cz")]
    public void TestDnsSecDomainLookup(string value)
    {
        Uri serviceUri = bootstrap.FindServiceUrlAsync(value).GetAwaiter().GetResult();

        using RdapClient service = new RdapClient();
        var result = service.DomainLookupAsync(serviceUri, value).GetAwaiter().GetResult();
    }
}
