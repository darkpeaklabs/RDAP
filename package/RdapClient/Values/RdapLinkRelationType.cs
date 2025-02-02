// Generated file from IANA registry (02/02/2025 10:42:36)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values
{
	public enum RdapLinkRelationType
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// Refers to a resource that is the subject of the link's context.
		/// </summary>
		[Display(Name = "About", Description = "Refers to a resource that is the subject of the link's context.")]
		About,

		/// <summary>
		/// Asserts that the link target provides an access control resource for the link context.
		/// </summary>
		[Display(Name = "Acl", Description = "Asserts that the link target provides an access control resource for the link context.")]
		Acl,

		/// <summary>
		/// Refers to a substitute for this context
		/// </summary>
		[Display(Name = "Alternate", Description = "Refers to a substitute for this context")]
		Alternate,

		/// <summary>
		/// Used to reference alternative content that uses the AMP profile of the HTML format.
		/// </summary>
		[Display(Name = "Amphtml", Description = "Used to reference alternative content that uses the AMP profile of the HTML format.")]
		Amphtml,

		/// <summary>
		/// Refers to a list of APIs available from the publisher of the link context.
		/// </summary>
		[Display(Name = "Api-Catalog", Description = "Refers to a list of APIs available from the publisher of the link context.")]
		ApiCatalog,

		/// <summary>
		/// Refers to an appendix.
		/// </summary>
		[Display(Name = "Appendix", Description = "Refers to an appendix.")]
		Appendix,

		/// <summary>
		/// Refers to an icon for the context. Synonym for icon.
		/// </summary>
		[Display(Name = "Apple-Touch-Icon", Description = "Refers to an icon for the context. Synonym for icon.")]
		AppleTouchIcon,

		/// <summary>
		/// Refers to a launch screen for the context.
		/// </summary>
		[Display(Name = "Apple-Touch-Startup-Image", Description = "Refers to a launch screen for the context.")]
		AppleTouchStartupImage,

		/// <summary>
		/// Refers to a collection of records, documents, or other materials of historical interest.
		/// </summary>
		[Display(Name = "Archives", Description = "Refers to a collection of records, documents, or other materials of historical interest.")]
		Archives,

		/// <summary>
		/// Refers to the context's author.
		/// </summary>
		[Display(Name = "Author", Description = "Refers to the context's author.")]
		Author,

		/// <summary>
		/// Identifies the entity that blocks access to a resource following receipt of a legal demand.
		/// </summary>
		[Display(Name = "Blocked-By", Description = "Identifies the entity that blocks access to a resource following receipt of a legal demand.")]
		BlockedBy,

		/// <summary>
		/// Gives a permanent link to use for bookmarking purposes.
		/// </summary>
		[Display(Name = "Bookmark", Description = "Gives a permanent link to use for bookmarking purposes.")]
		Bookmark,

		/// <summary>
		/// This link relation identifies the C2PA Manifest associated with the link context
		/// </summary>
		[Display(Name = "C2pa-Manifest", Description = "This link relation identifies the C2PA Manifest associated with the link context")]
		C2paManifest,

		/// <summary>
		/// Designates the preferred version of a resource (the IRI and its contents).
		/// </summary>
		[Display(Name = "Canonical", Description = "Designates the preferred version of a resource (the IRI and its contents).")]
		Canonical,

		/// <summary>
		/// Refers to a chapter in a collection of resources.
		/// </summary>
		[Display(Name = "Chapter", Description = "Refers to a chapter in a collection of resources.")]
		Chapter,

		/// <summary>
		/// Indicates that the link target is preferred over the link context for the purpose of permanent citation.
		/// </summary>
		[Display(Name = "Cite-As", Description = "Indicates that the link target is preferred over the link context for the purpose of permanent citation.")]
		CiteAs,

		/// <summary>
		/// The target IRI points to a resource which represents the collection resource for the context IRI.
		/// </summary>
		[Display(Name = "Collection", Description = "The target IRI points to a resource which represents the collection resource for the context IRI.")]
		Collection,

		/// <summary>
		/// Refers to a compression dictionary used for content encoding.
		/// </summary>
		[Display(Name = "Compression-Dictionary", Description = "Refers to a compression dictionary used for content encoding.")]
		CompressionDictionary,

		/// <summary>
		/// Refers to a table of contents.
		/// </summary>
		[Display(Name = "Contents", Description = "Refers to a table of contents.")]
		Contents,

		/// <summary>
		/// The document linked to was later converted to the document that contains this link relation. For example, an RFC can have a link to the Internet-Draft that became the RFC; in that case, the link relation would be \"convertedFrom\".
		/// </summary>
		[Display(Name = "Convertedfrom", Description = "The document linked to was later converted to the document that contains this link relation. For example, an RFC can have a link to the Internet-Draft that became the RFC; in that case, the link relation would be \"convertedFrom\".")]
		Convertedfrom,

		/// <summary>
		/// Refers to a copyright statement that applies to the link's context.
		/// </summary>
		[Display(Name = "Copyright", Description = "Refers to a copyright statement that applies to the link's context.")]
		Copyright,

		/// <summary>
		/// The target IRI points to a resource where a submission form can be obtained.
		/// </summary>
		[Display(Name = "Create-Form", Description = "The target IRI points to a resource where a submission form can be obtained.")]
		CreateForm,

		/// <summary>
		/// Refers to a resource containing the most recent item(s) in a collection of resources.
		/// </summary>
		[Display(Name = "Current", Description = "Refers to a resource containing the most recent item(s) in a collection of resources.")]
		Current,

		/// <summary>
		/// Refers to a resource that is documentation (intended for human consumption) about the deprecation of the link's context.
		/// </summary>
		[Display(Name = "Deprecation", Description = "Refers to a resource that is documentation (intended for human consumption) about the deprecation of the link's context.")]
		Deprecation,

		/// <summary>
		/// Refers to a resource providing information about the link's context.
		/// </summary>
		[Display(Name = "Describedby", Description = "Refers to a resource providing information about the link's context.")]
		Describedby,

		/// <summary>
		/// The relationship A 'describes' B asserts that resource A provides a description of resource B. There are no constraints on the format or representation of either A or B, neither are there any further constraints on either resource.
		/// </summary>
		[Display(Name = "Describes", Description = "The relationship A 'describes' B asserts that resource A provides a description of resource B. There are no constraints on the format or representation of either A or B, neither are there any further constraints on either resource.")]
		Describes,

		/// <summary>
		/// Refers to a list of patent disclosures made with respect to material for which 'disclosure' relation is specified.
		/// </summary>
		[Display(Name = "Disclosure", Description = "Refers to a list of patent disclosures made with respect to material for which 'disclosure' relation is specified.")]
		Disclosure,

		/// <summary>
		/// Used to indicate an origin that will be used to fetch required resources for the link context, and that the user agent ought to resolve as early as possible.
		/// </summary>
		[Display(Name = "Dns-Prefetch", Description = "Used to indicate an origin that will be used to fetch required resources for the link context, and that the user agent ought to resolve as early as possible.")]
		DnsPrefetch,

		/// <summary>
		/// Refers to a resource whose available representations are byte-for-byte identical with the corresponding representations of the context IRI.
		/// </summary>
		[Display(Name = "Duplicate", Description = "Refers to a resource whose available representations are byte-for-byte identical with the corresponding representations of the context IRI.")]
		Duplicate,

		/// <summary>
		/// Refers to a resource that can be used to edit the link's context.
		/// </summary>
		[Display(Name = "Edit", Description = "Refers to a resource that can be used to edit the link's context.")]
		Edit,

		/// <summary>
		/// The target IRI points to a resource where a submission form for editing associated resource can be obtained.
		/// </summary>
		[Display(Name = "Edit-Form", Description = "The target IRI points to a resource where a submission form for editing associated resource can be obtained.")]
		EditForm,

		/// <summary>
		/// Refers to a resource that can be used to edit media associated with the link's context.
		/// </summary>
		[Display(Name = "Edit-Media", Description = "Refers to a resource that can be used to edit media associated with the link's context.")]
		EditMedia,

		/// <summary>
		/// Identifies a related resource that is potentially large and might require special handling.
		/// </summary>
		[Display(Name = "Enclosure", Description = "Identifies a related resource that is potentially large and might require special handling.")]
		Enclosure,

		/// <summary>
		/// Refers to a resource that is not part of the same site as the current context.
		/// </summary>
		[Display(Name = "External", Description = "Refers to a resource that is not part of the same site as the current context.")]
		External,

		/// <summary>
		/// An IRI that refers to the furthest preceding resource in a series of resources.
		/// </summary>
		[Display(Name = "First", Description = "An IRI that refers to the furthest preceding resource in a series of resources.")]
		First,

		/// <summary>
		/// Refers to a glossary of terms.
		/// </summary>
		[Display(Name = "Glossary", Description = "Refers to a glossary of terms.")]
		Glossary,

		/// <summary>
		/// Refers to context-sensitive help.
		/// </summary>
		[Display(Name = "Help", Description = "Refers to context-sensitive help.")]
		Help,

		/// <summary>
		/// Refers to a resource hosted by the server indicated by the link context.
		/// </summary>
		[Display(Name = "Hosts", Description = "Refers to a resource hosted by the server indicated by the link context.")]
		Hosts,

		/// <summary>
		/// Refers to a hub that enables registration for notification of updates to the context.
		/// </summary>
		[Display(Name = "Hub", Description = "Refers to a hub that enables registration for notification of updates to the context.")]
		Hub,

		/// <summary>
		/// Conveys the STUN and TURN servers that can be used by an ICE Agent to establish a connection with a peer.
		/// </summary>
		[Display(Name = "Ice-Server", Description = "Conveys the STUN and TURN servers that can be used by an ICE Agent to establish a connection with a peer.")]
		IceServer,

		/// <summary>
		/// Refers to an icon representing the link's context.
		/// </summary>
		[Display(Name = "Icon", Description = "Refers to an icon representing the link's context.")]
		Icon,

		/// <summary>
		/// Refers to an index.
		/// </summary>
		[Display(Name = "Index", Description = "Refers to an index.")]
		Index,

		/// <summary>
		/// refers to a resource associated with a time interval that ends before the beginning of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalafter", Description = "refers to a resource associated with a time interval that ends before the beginning of the time interval associated with the context resource")]
		Intervalafter,

		/// <summary>
		/// refers to a resource associated with a time interval that begins after the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalbefore", Description = "refers to a resource associated with a time interval that begins after the end of the time interval associated with the context resource")]
		Intervalbefore,

		/// <summary>
		/// refers to a resource associated with a time interval that begins after the beginning of the time interval associated with the context resource, and ends before the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalcontains", Description = "refers to a resource associated with a time interval that begins after the beginning of the time interval associated with the context resource, and ends before the end of the time interval associated with the context resource")]
		Intervalcontains,

		/// <summary>
		/// refers to a resource associated with a time interval that begins after the end of the time interval associated with the context resource, or ends before the beginning of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervaldisjoint", Description = "refers to a resource associated with a time interval that begins after the end of the time interval associated with the context resource, or ends before the beginning of the time interval associated with the context resource")]
		Intervaldisjoint,

		/// <summary>
		/// refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalduring", Description = "refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource")]
		Intervalduring,

		/// <summary>
		/// refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalequals", Description = "refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource")]
		Intervalequals,

		/// <summary>
		/// refers to a resource associated with a time interval that begins after the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalfinishedby", Description = "refers to a resource associated with a time interval that begins after the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource")]
		Intervalfinishedby,

		/// <summary>
		/// refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalfinishes", Description = "refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and whose end coincides with the end of the time interval associated with the context resource")]
		Intervalfinishes,

		/// <summary>
		/// refers to a resource associated with a time interval that begins before or is coincident with the beginning of the time interval associated with the context resource, and ends after or is coincident with the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalin", Description = "refers to a resource associated with a time interval that begins before or is coincident with the beginning of the time interval associated with the context resource, and ends after or is coincident with the end of the time interval associated with the context resource")]
		Intervalin,

		/// <summary>
		/// refers to a resource associated with a time interval whose beginning coincides with the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalmeets", Description = "refers to a resource associated with a time interval whose beginning coincides with the end of the time interval associated with the context resource")]
		Intervalmeets,

		/// <summary>
		/// refers to a resource associated with a time interval whose end coincides with the beginning of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalmetby", Description = "refers to a resource associated with a time interval whose end coincides with the beginning of the time interval associated with the context resource")]
		Intervalmetby,

		/// <summary>
		/// refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and ends after the beginning of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervaloverlappedby", Description = "refers to a resource associated with a time interval that begins before the beginning of the time interval associated with the context resource, and ends after the beginning of the time interval associated with the context resource")]
		Intervaloverlappedby,

		/// <summary>
		/// refers to a resource associated with a time interval that begins before the end of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervaloverlaps", Description = "refers to a resource associated with a time interval that begins before the end of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource")]
		Intervaloverlaps,

		/// <summary>
		/// refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and ends before the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalstartedby", Description = "refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and ends before the end of the time interval associated with the context resource")]
		Intervalstartedby,

		/// <summary>
		/// refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource
		/// </summary>
		[Display(Name = "Intervalstarts", Description = "refers to a resource associated with a time interval whose beginning coincides with the beginning of the time interval associated with the context resource, and ends after the end of the time interval associated with the context resource")]
		Intervalstarts,

		/// <summary>
		/// The target IRI points to a resource that is a member of the collection represented by the context IRI.
		/// </summary>
		[Display(Name = "Item", Description = "The target IRI points to a resource that is a member of the collection represented by the context IRI.")]
		Item,

		/// <summary>
		/// An IRI that refers to the furthest following resource in a series of resources.
		/// </summary>
		[Display(Name = "Last", Description = "An IRI that refers to the furthest following resource in a series of resources.")]
		Last,

		/// <summary>
		/// Points to a resource containing the latest (e.g., current) version of the context.
		/// </summary>
		[Display(Name = "Latest-Version", Description = "Points to a resource containing the latest (e.g., current) version of the context.")]
		LatestVersion,

		/// <summary>
		/// Refers to a license associated with this context.
		/// </summary>
		[Display(Name = "License", Description = "Refers to a license associated with this context.")]
		License,

		/// <summary>
		/// The link target of a link with the \"linkset\" relation type provides a set of links, including links in which the link context of the link participates.
		/// </summary>
		[Display(Name = "Linkset", Description = "The link target of a link with the \"linkset\" relation type provides a set of links, including links in which the link context of the link participates.")]
		Linkset,

		/// <summary>
		/// Refers to further information about the link's context, expressed as a LRDD (\"Link-based Resource Descriptor Document\") resource. See [RFC6415] for information about processing this relation type in host-meta documents. When used elsewhere, it refers to additional links and other metadata. Multiple instances indicate additional LRDD resources. LRDD resources MUST have an \"application/xrd+xml\" representation, and MAY have others.
		/// </summary>
		[Display(Name = "Lrdd", Description = "Refers to further information about the link's context, expressed as a LRDD (\"Link-based Resource Descriptor Document\") resource. See [RFC6415] for information about processing this relation type in host-meta documents. When used elsewhere, it refers to additional links and other metadata. Multiple instances indicate additional LRDD resources. LRDD resources MUST have an \"application/xrd+xml\" representation, and MAY have others.")]
		Lrdd,

		/// <summary>
		/// Links to a manifest file for the context.
		/// </summary>
		[Display(Name = "Manifest", Description = "Links to a manifest file for the context.")]
		Manifest,

		/// <summary>
		/// Refers to a mask that can be applied to the icon for the context.
		/// </summary>
		[Display(Name = "Mask-Icon", Description = "Refers to a mask that can be applied to the icon for the context.")]
		MaskIcon,

		/// <summary>
		/// Links to a resource about the author of the link's context.
		/// </summary>
		[Display(Name = "Me", Description = "Links to a resource about the author of the link's context.")]
		Me,

		/// <summary>
		/// Refers to a feed of personalised media recommendations relevant to the link context.
		/// </summary>
		[Display(Name = "Media-Feed", Description = "Refers to a feed of personalised media recommendations relevant to the link context.")]
		MediaFeed,

		/// <summary>
		/// The Target IRI points to a Memento, a fixed resource that will not change state anymore.
		/// </summary>
		[Display(Name = "Memento", Description = "The Target IRI points to a Memento, a fixed resource that will not change state anymore.")]
		Memento,

		/// <summary>
		/// Links to the context's Micropub endpoint.
		/// </summary>
		[Display(Name = "Micropub", Description = "Links to the context's Micropub endpoint.")]
		Micropub,

		/// <summary>
		/// Refers to a module that the user agent is to preemptively fetch and store for use in the current context.
		/// </summary>
		[Display(Name = "Modulepreload", Description = "Refers to a module that the user agent is to preemptively fetch and store for use in the current context.")]
		Modulepreload,

		/// <summary>
		/// Refers to a resource that can be used to monitor changes in an HTTP resource.
		/// </summary>
		[Display(Name = "Monitor", Description = "Refers to a resource that can be used to monitor changes in an HTTP resource.")]
		Monitor,

		/// <summary>
		/// Refers to a resource that can be used to monitor changes in a specified group of HTTP resources.
		/// </summary>
		[Display(Name = "Monitor-Group", Description = "Refers to a resource that can be used to monitor changes in a specified group of HTTP resources.")]
		MonitorGroup,

		/// <summary>
		/// Indicates that the link's context is a part of a series, and that the next in the series is the link target.
		/// </summary>
		[Display(Name = "Next", Description = "Indicates that the link's context is a part of a series, and that the next in the series is the link target.")]
		Next,

		/// <summary>
		/// Refers to the immediately following archive resource.
		/// </summary>
		[Display(Name = "Next-Archive", Description = "Refers to the immediately following archive resource.")]
		NextArchive,

		/// <summary>
		/// Indicates that the context’s original author or publisher does not endorse the link target.
		/// </summary>
		[Display(Name = "Nofollow", Description = "Indicates that the context’s original author or publisher does not endorse the link target.")]
		Nofollow,

		/// <summary>
		/// Indicates that any newly created top-level browsing context which results from following the link will not be an auxiliary browsing context.
		/// </summary>
		[Display(Name = "Noopener", Description = "Indicates that any newly created top-level browsing context which results from following the link will not be an auxiliary browsing context.")]
		Noopener,

		/// <summary>
		/// Indicates that no referrer information is to be leaked when following the link.
		/// </summary>
		[Display(Name = "Noreferrer", Description = "Indicates that no referrer information is to be leaked when following the link.")]
		Noreferrer,

		/// <summary>
		/// Indicates that any newly created top-level browsing context which results from following the link will be an auxiliary browsing context.
		/// </summary>
		[Display(Name = "Opener", Description = "Indicates that any newly created top-level browsing context which results from following the link will be an auxiliary browsing context.")]
		Opener,

		/// <summary>
		/// Refers to an OpenID Authentication server on which the context relies for an assertion that the end user controls an Identifier.
		/// </summary>
		[Display(Name = "Openid2.Local_Id", Description = "Refers to an OpenID Authentication server on which the context relies for an assertion that the end user controls an Identifier.")]
		Openid2LocalId,

		/// <summary>
		/// Refers to a resource which accepts OpenID Authentication protocol messages for the context.
		/// </summary>
		[Display(Name = "Openid2.Provider", Description = "Refers to a resource which accepts OpenID Authentication protocol messages for the context.")]
		Openid2Provider,

		/// <summary>
		/// The Target IRI points to an Original Resource.
		/// </summary>
		[Display(Name = "Original", Description = "The Target IRI points to an Original Resource.")]
		Original,

		/// <summary>
		/// Refers to a P3P privacy policy for the context.
		/// </summary>
		[Display(Name = "P3pv1", Description = "Refers to a P3P privacy policy for the context.")]
		P3pv1,

		/// <summary>
		/// Indicates a resource where payment is accepted.
		/// </summary>
		[Display(Name = "Payment", Description = "Indicates a resource where payment is accepted.")]
		Payment,

		/// <summary>
		/// Gives the address of the pingback resource for the link context.
		/// </summary>
		[Display(Name = "Pingback", Description = "Gives the address of the pingback resource for the link context.")]
		Pingback,

		/// <summary>
		/// Used to indicate an origin that will be used to fetch required resources for the link context. Initiating an early connection, which includes the DNS lookup, TCP handshake, and optional TLS negotiation, allows the user agent to mask the high latency costs of establishing a connection.
		/// </summary>
		[Display(Name = "Preconnect", Description = "Used to indicate an origin that will be used to fetch required resources for the link context. Initiating an early connection, which includes the DNS lookup, TCP handshake, and optional TLS negotiation, allows the user agent to mask the high latency costs of establishing a connection.")]
		Preconnect,

		/// <summary>
		/// Points to a resource containing the predecessor version in the version history.
		/// </summary>
		[Display(Name = "Predecessor-Version", Description = "Points to a resource containing the predecessor version in the version history.")]
		PredecessorVersion,

		/// <summary>
		/// The prefetch link relation type is used to identify a resource that might be required by the next navigation from the link context, and that the user agent ought to fetch, such that the user agent can deliver a faster response once the resource is requested in the future.
		/// </summary>
		[Display(Name = "Prefetch", Description = "The prefetch link relation type is used to identify a resource that might be required by the next navigation from the link context, and that the user agent ought to fetch, such that the user agent can deliver a faster response once the resource is requested in the future.")]
		Prefetch,

		/// <summary>
		/// Refers to a resource that should be loaded early in the processing of the link's context, without blocking rendering.
		/// </summary>
		[Display(Name = "Preload", Description = "Refers to a resource that should be loaded early in the processing of the link's context, without blocking rendering.")]
		Preload,

		/// <summary>
		/// Used to identify a resource that might be required by the next navigation from the link context, and that the user agent ought to fetch and execute, such that the user agent can deliver a faster response once the resource is requested in the future.
		/// </summary>
		[Display(Name = "Prerender", Description = "Used to identify a resource that might be required by the next navigation from the link context, and that the user agent ought to fetch and execute, such that the user agent can deliver a faster response once the resource is requested in the future.")]
		Prerender,

		/// <summary>
		/// Indicates that the link's context is a part of a series, and that the previous in the series is the link target.
		/// </summary>
		[Display(Name = "Prev", Description = "Indicates that the link's context is a part of a series, and that the previous in the series is the link target.")]
		Prev,

		/// <summary>
		/// Refers to a resource that provides a preview of the link's context.
		/// </summary>
		[Display(Name = "Preview", Description = "Refers to a resource that provides a preview of the link's context.")]
		Preview,

		/// <summary>
		/// Refers to the previous resource in an ordered series of resources. Synonym for \"prev\".
		/// </summary>
		[Display(Name = "Previous", Description = "Refers to the previous resource in an ordered series of resources. Synonym for \"prev\".")]
		Previous,

		/// <summary>
		/// Refers to the immediately preceding archive resource.
		/// </summary>
		[Display(Name = "Prev-Archive", Description = "Refers to the immediately preceding archive resource.")]
		PrevArchive,

		/// <summary>
		/// Refers to a privacy policy associated with the link's context.
		/// </summary>
		[Display(Name = "Privacy-Policy", Description = "Refers to a privacy policy associated with the link's context.")]
		PrivacyPolicy,

		/// <summary>
		/// Identifying that a resource representation conforms to a certain profile, without affecting the non-profile semantics of the resource representation.
		/// </summary>
		[Display(Name = "Profile", Description = "Identifying that a resource representation conforms to a certain profile, without affecting the non-profile semantics of the resource representation.")]
		Profile,

		/// <summary>
		/// Links to a publication manifest. A manifest represents structured information about a publication, such as informative metadata, a list of resources, and a default reading order.
		/// </summary>
		[Display(Name = "Publication", Description = "Links to a publication manifest. A manifest represents structured information about a publication, such as informative metadata, a list of resources, and a default reading order.")]
		Publication,

		/// <summary>
		/// Identifies a related resource.
		/// </summary>
		[Display(Name = "Related", Description = "Identifies a related resource.")]
		Related,

		/// <summary>
		/// Identifies the root of RESTCONF API as configured on this HTTP server. The \"restconf\" relation defines the root of the API defined in RFC8040. Subsequent revisions of RESTCONF will use alternate relation values to support protocol versioning.
		/// </summary>
		[Display(Name = "Restconf", Description = "Identifies the root of RESTCONF API as configured on this HTTP server. The \"restconf\" relation defines the root of the API defined in RFC8040. Subsequent revisions of RESTCONF will use alternate relation values to support protocol versioning.")]
		Restconf,

		/// <summary>
		/// Identifies a resource that is a reply to the context of the link.
		/// </summary>
		[Display(Name = "Replies", Description = "Identifies a resource that is a reply to the context of the link.")]
		Replies,

		/// <summary>
		/// The resource identified by the link target provides an input value to an instance of a rule, where the resource which represents the rule instance is identified by the link context.
		/// </summary>
		[Display(Name = "Ruleinput", Description = "The resource identified by the link target provides an input value to an instance of a rule, where the resource which represents the rule instance is identified by the link context.")]
		Ruleinput,

		/// <summary>
		/// Refers to a resource that can be used to search through the link's context and related resources.
		/// </summary>
		[Display(Name = "Search", Description = "Refers to a resource that can be used to search through the link's context and related resources.")]
		Search,

		/// <summary>
		/// Refers to a section in a collection of resources.
		/// </summary>
		[Display(Name = "Section", Description = "Refers to a section in a collection of resources.")]
		Section,

		/// <summary>
		/// Conveys an identifier for the link's context.
		/// </summary>
		[Display(Name = "Self", Description = "Conveys an identifier for the link's context.")]
		Self,

		/// <summary>
		/// Indicates a URI that can be used to retrieve a service document.
		/// </summary>
		[Display(Name = "Service", Description = "Indicates a URI that can be used to retrieve a service document.")]
		Service,

		/// <summary>
		/// Identifies service description for the context that is primarily intended for consumption by machines.
		/// </summary>
		[Display(Name = "Service-Desc", Description = "Identifies service description for the context that is primarily intended for consumption by machines.")]
		ServiceDesc,

		/// <summary>
		/// Identifies service documentation for the context that is primarily intended for human consumption.
		/// </summary>
		[Display(Name = "Service-Doc", Description = "Identifies service documentation for the context that is primarily intended for human consumption.")]
		ServiceDoc,

		/// <summary>
		/// Identifies general metadata for the context that is primarily intended for consumption by machines.
		/// </summary>
		[Display(Name = "Service-Meta", Description = "Identifies general metadata for the context that is primarily intended for consumption by machines.")]
		ServiceMeta,

		/// <summary>
		/// Refers to a capability set document that defines parameters or configuration requirements for automated peering and communication-channel negotiation of the Session Initiation Protocol (SIP).
		/// </summary>
		[Display(Name = "Sip-Trunking-Capability", Description = "Refers to a capability set document that defines parameters or configuration requirements for automated peering and communication-channel negotiation of the Session Initiation Protocol (SIP).")]
		SipTrunkingCapability,

		/// <summary>
		/// Refers to a resource that is within a context that is sponsored (such as advertising or another compensation agreement).
		/// </summary>
		[Display(Name = "Sponsored", Description = "Refers to a resource that is within a context that is sponsored (such as advertising or another compensation agreement).")]
		Sponsored,

		/// <summary>
		/// Refers to the first resource in a collection of resources.
		/// </summary>
		[Display(Name = "Start", Description = "Refers to the first resource in a collection of resources.")]
		Start,

		/// <summary>
		/// Identifies a resource that represents the context's status.
		/// </summary>
		[Display(Name = "Status", Description = "Identifies a resource that represents the context's status.")]
		Status,

		/// <summary>
		/// Refers to a stylesheet.
		/// </summary>
		[Display(Name = "Stylesheet", Description = "Refers to a stylesheet.")]
		Stylesheet,

		/// <summary>
		/// Refers to a resource serving as a subsection in a collection of resources.
		/// </summary>
		[Display(Name = "Subsection", Description = "Refers to a resource serving as a subsection in a collection of resources.")]
		Subsection,

		/// <summary>
		/// Points to a resource containing the successor version in the version history.
		/// </summary>
		[Display(Name = "Successor-Version", Description = "Points to a resource containing the successor version in the version history.")]
		SuccessorVersion,

		/// <summary>
		/// Identifies a resource that provides information about the context's retirement policy.
		/// </summary>
		[Display(Name = "Sunset", Description = "Identifies a resource that provides information about the context's retirement policy.")]
		Sunset,

		/// <summary>
		/// Gives a tag (identified by the given address) that applies to the current document.
		/// </summary>
		[Display(Name = "Tag", Description = "Gives a tag (identified by the given address) that applies to the current document.")]
		Tag,

		/// <summary>
		/// Refers to the terms of service associated with the link's context.
		/// </summary>
		[Display(Name = "Terms-Of-Service", Description = "Refers to the terms of service associated with the link's context.")]
		TermsOfService,

		/// <summary>
		/// The Target IRI points to a TimeGate for an Original Resource.
		/// </summary>
		[Display(Name = "Timegate", Description = "The Target IRI points to a TimeGate for an Original Resource.")]
		Timegate,

		/// <summary>
		/// The Target IRI points to a TimeMap for an Original Resource.
		/// </summary>
		[Display(Name = "Timemap", Description = "The Target IRI points to a TimeMap for an Original Resource.")]
		Timemap,

		/// <summary>
		/// Refers to a resource identifying the abstract semantic type of which the link's context is considered to be an instance.
		/// </summary>
		[Display(Name = "Type", Description = "Refers to a resource identifying the abstract semantic type of which the link's context is considered to be an instance.")]
		Type,

		/// <summary>
		/// Refers to a resource that is within a context that is User Generated Content.
		/// </summary>
		[Display(Name = "Ugc", Description = "Refers to a resource that is within a context that is User Generated Content.")]
		Ugc,

		/// <summary>
		/// Refers to a parent document in a hierarchy of documents.
		/// </summary>
		[Display(Name = "Up", Description = "Refers to a parent document in a hierarchy of documents.")]
		Up,

		/// <summary>
		/// Points to a resource containing the version history for the context.
		/// </summary>
		[Display(Name = "Version-History", Description = "Points to a resource containing the version history for the context.")]
		VersionHistory,

		/// <summary>
		/// Identifies a resource that is the source of the information in the link's context.
		/// </summary>
		[Display(Name = "Via", Description = "Identifies a resource that is the source of the information in the link's context.")]
		Via,

		/// <summary>
		/// Identifies a target URI that supports the Webmention protocol. This allows clients that mention a resource in some form of publishing process to contact that endpoint and inform it that this resource has been mentioned.
		/// </summary>
		[Display(Name = "Webmention", Description = "Identifies a target URI that supports the Webmention protocol. This allows clients that mention a resource in some form of publishing process to contact that endpoint and inform it that this resource has been mentioned.")]
		Webmention,

		/// <summary>
		/// Points to a working copy for this resource.
		/// </summary>
		[Display(Name = "Working-Copy", Description = "Points to a working copy for this resource.")]
		WorkingCopy,

		/// <summary>
		/// Points to the versioned resource from which this working copy was obtained.
		/// </summary>
		[Display(Name = "Working-Copy-Of", Description = "Points to the versioned resource from which this working copy was obtained.")]
		WorkingCopyOf,
	}
}
