using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapStatusMapping
    {
        /// <summary>
        /// Maps the statuses defined in the Extensible Provisioning Protocol(EPP) RFCs to the list of statuses registered
        /// for use in the Registration Data Access Protocol(RDAP), in the "RDAP JSON Values" registry.
        /// <see cref="https://tools.ietf.org/html/rfc8056">RFC 8056</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapStatus result, ILogger logger = null)
        {
            switch (value)
            {
                case "addPeriod": result = RdapStatus.AddPeriod; break;
                case "autoRenewPeriod": result = RdapStatus.AutoRenewPeriod; break;
                case "clientDeleteProhibited": result = RdapStatus.ClientDeleteProhibited; break;
                case "clientHold": result = RdapStatus.ClientHold; break;
                case "clientRenewProhibited": result = RdapStatus.ClientRenewProhibited; break;
                case "clientTransferProhibited": result = RdapStatus.ClientTransferProhibited; break;
                case "clientUpdateProhibited": result = RdapStatus.ClientUpdateProhibited; break;
                case "inactive": result = RdapStatus.Inactive; break;
                case "linked": result = RdapStatus.Associated; break;
                case "ok": result = RdapStatus.Active; break;
                case "pendingCreate": result = RdapStatus.PendingCreate; break;
                case "pendingDelete": result = RdapStatus.PendingDelete; break;
                case "pendingRenew": result = RdapStatus.PendingRenew; break;
                case "pendingRestore": result = RdapStatus.PendingRestore; break;
                case "pendingTransfer": result = RdapStatus.PendingTransfer; break;
                case "pendingUpdate": result = RdapStatus.PendingUpdate; break;
                case "redemptionPeriod": result = RdapStatus.RedemptionPeriod; break;
                case "renewPeriod": result = RdapStatus.RenewPeriod; break;
                case "serverDeleteProhibited": result = RdapStatus.ServerDeleteProhibited; break;
                case "serverRenewProhibited": result = RdapStatus.ServerRenewProhibited; break;
                case "serverTransferProhibited": result = RdapStatus.ServerTransferProhibited; break;
                case "serverUpdateProhibited": result = RdapStatus.ServerUpdateProhibited; break;
                case "serverHold": result = RdapStatus.ServerHold; break;
                case "transferPeriod": result = RdapStatus.TransferPeriod; break;
                default: result = RdapStatus.Unknown; break;
            };

            if (result != RdapStatus.Unknown)
            {
                logger?.LogDebug("Status string value {Value} mapped to value {Enum}", value, result);
            }
            else
            {
                logger?.LogWarning("Unable to map Status string value {Value} to RDAP JSON value", value);
            }

            return result != RdapStatus.Unknown;
        }
    }
}