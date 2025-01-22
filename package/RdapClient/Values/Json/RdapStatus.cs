// Generated file from IANA registry (2/20/2022 10:53:44 AM)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values.Json
{
	public enum RdapStatus
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// Signifies that the data of the object instance has been found to be accurate. This type of status is usually found on entity object instances to note the validity of identifying contact information.
		/// </summary>
		[Display(Name = "Validated", Description = "Signifies that the data of the object instance has been found to be accurate. This type of status is usually found on entity object instances to note the validity of identifying contact information.")]
		Validated,

		/// <summary>
		/// Renewal or reregistration of the object instance is forbidden.
		/// </summary>
		[Display(Name = "Renew Prohibited", Description = "Renewal or reregistration of the object instance is forbidden.")]
		RenewProhibited,

		/// <summary>
		/// Updates to the object instance are forbidden.
		/// </summary>
		[Display(Name = "Update Prohibited", Description = "Updates to the object instance are forbidden.")]
		UpdateProhibited,

		/// <summary>
		/// Transfers of the registration from one registrar to another are forbidden. This type of status normally applies to DNR domain names.
		/// </summary>
		[Display(Name = "Transfer Prohibited", Description = "Transfers of the registration from one registrar to another are forbidden. This type of status normally applies to DNR domain names.")]
		TransferProhibited,

		/// <summary>
		/// Deletion of the registration of the object instance is forbidden. This type of status normally applies to DNR domain names.
		/// </summary>
		[Display(Name = "Delete Prohibited", Description = "Deletion of the registration of the object instance is forbidden. This type of status normally applies to DNR domain names.")]
		DeleteProhibited,

		/// <summary>
		/// The registration of the object instance has been performed by a third party. This is most commonly applied to entities.
		/// </summary>
		[Display(Name = "Proxy", Description = "The registration of the object instance has been performed by a third party. This is most commonly applied to entities.")]
		Proxy,

		/// <summary>
		/// The information of the object instance is not designated for public consumption. This is most commonly applied to entities.
		/// </summary>
		[Display(Name = "Private", Description = "The information of the object instance is not designated for public consumption. This is most commonly applied to entities.")]
		Private,

		/// <summary>
		/// Some of the information of the object instance has not been made available and has been removed. This is most commonly applied to entities.
		/// </summary>
		[Display(Name = "Removed", Description = "Some of the information of the object instance has not been made available and has been removed. This is most commonly applied to entities.")]
		Removed,

		/// <summary>
		/// Some of the information of the object instance has been altered for the purposes of not readily revealing the actual information of the object instance. This is most commonly applied to entities.
		/// </summary>
		[Display(Name = "Obscured", Description = "Some of the information of the object instance has been altered for the purposes of not readily revealing the actual information of the object instance. This is most commonly applied to entities.")]
		Obscured,

		/// <summary>
		/// The object instance is associated with other object instances in the registry. This is most commonly used to signify that a nameserver is associated with a domain or that an entity is associated with a network resource or domain.
		/// </summary>
		[Display(Name = "Associated", Description = "The object instance is associated with other object instances in the registry. This is most commonly used to signify that a nameserver is associated with a domain or that an entity is associated with a network resource or domain.")]
		Associated,

		/// <summary>
		/// The object instance is in use. For domain names, it signifies that the domain name is published in DNS. For network and autnum registrations, it signifies that they are allocated or assigned for use in operational networks. This maps to the \"OK\" status of the Extensible Provisioning Protocol (EPP) [RFC5730].
		/// </summary>
		[Display(Name = "Active", Description = "The object instance is in use. For domain names, it signifies that the domain name is published in DNS. For network and autnum registrations, it signifies that they are allocated or assigned for use in operational networks. This maps to the \"OK\" status of the Extensible Provisioning Protocol (EPP) [RFC5730].")]
		Active,

		/// <summary>
		/// The object instance is not in use. See \"active\".
		/// </summary>
		[Display(Name = "Inactive", Description = "The object instance is not in use. See \"active\".")]
		Inactive,

		/// <summary>
		/// Changes to the object instance cannot be made, including the association of other object instances.
		/// </summary>
		[Display(Name = "Locked", Description = "Changes to the object instance cannot be made, including the association of other object instances.")]
		Locked,

		/// <summary>
		/// A request has been received for the creation of the object instance, but this action is not yet complete.
		/// </summary>
		[Display(Name = "Pending Create", Description = "A request has been received for the creation of the object instance, but this action is not yet complete.")]
		PendingCreate,

		/// <summary>
		/// A request has been received for the renewal of the object instance, but this action is not yet complete.
		/// </summary>
		[Display(Name = "Pending Renew", Description = "A request has been received for the renewal of the object instance, but this action is not yet complete.")]
		PendingRenew,

		/// <summary>
		/// A request has been received for the transfer of the object instance, but this action is not yet complete.
		/// </summary>
		[Display(Name = "Pending Transfer", Description = "A request has been received for the transfer of the object instance, but this action is not yet complete.")]
		PendingTransfer,

		/// <summary>
		/// A request has been received for the update or modification of the object instance, but this action is not yet complete.
		/// </summary>
		[Display(Name = "Pending Update", Description = "A request has been received for the update or modification of the object instance, but this action is not yet complete.")]
		PendingUpdate,

		/// <summary>
		/// A request has been received for the deletion or removal of the object instance, but this action is not yet complete. For domains, this might mean that the name is no longer published in DNS but has not yet been purged from the registry database.
		/// </summary>
		[Display(Name = "Pending Delete", Description = "A request has been received for the deletion or removal of the object instance, but this action is not yet complete. For domains, this might mean that the name is no longer published in DNS but has not yet been purged from the registry database.")]
		PendingDelete,

		/// <summary>
		/// This grace period is provided after the initial registration of the object. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the registration. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'addPeriod' status.
		/// </summary>
		[Display(Name = "Add Period", Description = "This grace period is provided after the initial registration of the object. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the registration. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'addPeriod' status.")]
		AddPeriod,

		/// <summary>
		/// This grace period is provided after an object registration period expires and is extended (renewed) automatically by the server. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the auto renewal. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'autoRenewPeriod' status.
		/// </summary>
		[Display(Name = "Auto Renew Period", Description = "This grace period is provided after an object registration period expires and is extended (renewed) automatically by the server. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the auto renewal. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'autoRenewPeriod' status.")]
		AutoRenewPeriod,

		/// <summary>
		/// The client requested that requests to delete the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientDeleteProhibited' status.
		/// </summary>
		[Display(Name = "Client Delete Prohibited", Description = "The client requested that requests to delete the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientDeleteProhibited' status.")]
		ClientDeleteProhibited,

		/// <summary>
		/// The client requested that the DNS delegation information MUST NOT be published for the object. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'clientHold' status.
		/// </summary>
		[Display(Name = "Client Hold", Description = "The client requested that the DNS delegation information MUST NOT be published for the object. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'clientHold' status.")]
		ClientHold,

		/// <summary>
		/// The client requested that requests to renew the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'clientRenewProhibited' status.
		/// </summary>
		[Display(Name = "Client Renew Prohibited", Description = "The client requested that requests to renew the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'clientRenewProhibited' status.")]
		ClientRenewProhibited,

		/// <summary>
		/// The client requested that requests to transfer the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientTransferProhibited' status.
		/// </summary>
		[Display(Name = "Client Transfer Prohibited", Description = "The client requested that requests to transfer the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientTransferProhibited' status.")]
		ClientTransferProhibited,

		/// <summary>
		/// The client requested that requests to update the object (other than to remove this status) MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientUpdateProhibited' status.
		/// </summary>
		[Display(Name = "Client Update Prohibited", Description = "The client requested that requests to update the object (other than to remove this status) MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'clientUpdateProhibited' status.")]
		ClientUpdateProhibited,

		/// <summary>
		/// An object is in the process of being restored after being in the redemption period state. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'pendingRestore' status.
		/// </summary>
		[Display(Name = "Pending Restore", Description = "An object is in the process of being restored after being in the redemption period state. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'pendingRestore' status.")]
		PendingRestore,

		/// <summary>
		/// A delete has been received, but the object has not yet been purged because an opportunity exists to restore the object and abort the deletion process. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'redemptionPeriod' status.
		/// </summary>
		[Display(Name = "Redemption Period", Description = "A delete has been received, but the object has not yet been purged because an opportunity exists to restore the object and abort the deletion process. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'redemptionPeriod' status.")]
		RedemptionPeriod,

		/// <summary>
		/// This grace period is provided after an object registration period is explicitly extended (renewed) by the client. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the renewal. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'renewPeriod' status.
		/// </summary>
		[Display(Name = "Renew Period", Description = "This grace period is provided after an object registration period is explicitly extended (renewed) by the client. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the renewal. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'renewPeriod' status.")]
		RenewPeriod,

		/// <summary>
		/// The server set the status so that requests to delete the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverDeleteProhibited' status.
		/// </summary>
		[Display(Name = "Server Delete Prohibited", Description = "The server set the status so that requests to delete the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverDeleteProhibited' status.")]
		ServerDeleteProhibited,

		/// <summary>
		/// The server set the status so that requests to renew the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'serverRenewProhibited' status.
		/// </summary>
		[Display(Name = "Server Renew Prohibited", Description = "The server set the status so that requests to renew the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'serverRenewProhibited' status.")]
		ServerRenewProhibited,

		/// <summary>
		/// The server set the status so that requests to transfer the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverTransferProhibited' status.
		/// </summary>
		[Display(Name = "Server Transfer Prohibited", Description = "The server set the status so that requests to transfer the object MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverTransferProhibited' status.")]
		ServerTransferProhibited,

		/// <summary>
		/// The server set the status so that requests to update the object (other than to remove this status) MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverUpdateProhibited' status.
		/// </summary>
		[Display(Name = "Server Update Prohibited", Description = "The server set the status so that requests to update the object (other than to remove this status) MUST be rejected. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731], Extensible Provisioning Protocol (EPP) Host Mapping [RFC5732], and Extensible Provisioning Protocol (EPP) Contact Mapping [RFC5733] 'serverUpdateProhibited' status.")]
		ServerUpdateProhibited,

		/// <summary>
		/// The server set the status so that DNS delegation information MUST NOT be published for the object. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'serverHold' status.
		/// </summary>
		[Display(Name = "Server Hold", Description = "The server set the status so that DNS delegation information MUST NOT be published for the object. This maps to the Extensible Provisioning Protocol (EPP) Domain Name Mapping [RFC5731] 'serverHold' status.")]
		ServerHold,

		/// <summary>
		/// This grace period is provided after the successful transfer of object registration sponsorship from one client to another client. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the transfer. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'transferPeriod' status.
		/// </summary>
		[Display(Name = "Transfer Period", Description = "This grace period is provided after the successful transfer of object registration sponsorship from one client to another client. If the object is deleted by the client during this period, the server provides a credit to the client for the cost of the transfer. This maps to the Domain Registry Grace Period Mapping for the Extensible Provisioning Protocol (EPP) [RFC3915] 'transferPeriod' status.")]
		TransferPeriod,

		/// <summary>
		/// The object instance has been allocated administratively (i.e., not for use by the recipient in their own right in operational networks).
		/// </summary>
		[Display(Name = "Administrative", Description = "The object instance has been allocated administratively (i.e., not for use by the recipient in their own right in operational networks).")]
		Administrative,

		/// <summary>
		/// The object instance has been allocated to an IANA special-purpose address registry.
		/// </summary>
		[Display(Name = "Reserved", Description = "The object instance has been allocated to an IANA special-purpose address registry.")]
#pragma warning disable CA1700 // Do not name enum values 'Reserved'
		Reserved,
#pragma warning restore CA1700 // Do not name enum values 'Reserved'
	}
}
