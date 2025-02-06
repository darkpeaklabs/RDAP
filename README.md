# RDAP Client

RdapClient is a .Net client for [Registration Data Access Protocol (RDAP)](https://www.arin.net/resources/registry/whois/rdap/).

## Live Demo
https://rdap.darkpeaklabs.com/

## Usage

```
using RdapClient service = new RdapClient();
var result = await service.DomainLookupAsync(new Uri("https://rdap.verisign.com/com/v1/"), "example.com");
```

## Bootstrap

The RDAP bootstrap classes provide programmatic way to obtain RDAP lookup service URLs for various RDAP objects.

```
var bootstrap = new RdapDnsBootstrap();
var serviceUri = await bootstrap.FindServiceUrlAsync("example.com");

using RdapClient service = new RdapClient();
var result = await service.DomainLookupAsync(serviceUri, "example.com");
```

## References

- [RFC 9082: Registration Data Access Protocol (RDAP) Query Format](https://datatracker.ietf.org/doc/html/rfc9082)
- [RFC 9083: JSON Responses for the Registration Data Access Protocol (RDAP)](https://datatracker.ietf.org/doc/html/rfc9083)
