using DarkPeakLabs.Rdap.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test;

[TestClass]
public class DomainListTest
{
    [DataTestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TestCiscoUmbrella(bool useCache)
    {
        CiscoUmbrellaDomainList domainList = new();
        var domains = domainList.GetDomainsAsync(useCache).GetAwaiter().GetResult();
        Assert.IsNotNull(domains);
        Assert.AreEqual(1000000, domains.Count);
    }

    [DataTestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TestMajestic(bool useCache)
    {
        MajesticDomainList domainList = new();
        var domains = domainList.GetDomainsAsync(useCache).GetAwaiter().GetResult();
        Assert.IsNotNull(domains);
        Assert.AreEqual(1000000, domains.Count);
    }

    [DataTestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TestBuiltWith(bool useCache)
    {
        BuiltWithDomainList domainList = new();
        var domains = domainList.GetDomainsAsync(useCache).GetAwaiter().GetResult();
        Assert.IsNotNull(domains);
        Assert.IsTrue(domains.Count > 900000);
    }

    [DataTestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TestDomCop(bool useCache)
    {
        DomCopDomainList domainList = new();
        var domains = domainList.GetDomainsAsync(useCache).GetAwaiter().GetResult();
        Assert.IsNotNull(domains);
        Assert.IsTrue(domains.Count > 9900000);
    }

    [DataTestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public void TestTranco(bool useCache)
    {
        TrancoDomainList domainList = new();
        var domains = domainList.GetDomainsAsync(useCache).GetAwaiter().GetResult();
        Assert.IsNotNull(domains);
        Assert.AreEqual(1000000, domains.Count);
    }
}
