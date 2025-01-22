namespace Microsoft.CorporateDomains.Rdap.Test
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public class MajesticRecord : IDomainListRecord
    {
        public int GlobalRank { get; set; }
        public int TldRank { get; set; }
        public string Domain { get; set; }
        public string TLD { get; set; }

        public int RefSubNets { get; set; }
        public int RefIPs { get; set; }
        
        public string IDN_Domain { get; set; }
        public string IDN_TLD { get; set; }

        public int PrevGlobalRank { get; set; }
        public int PrevTldRank { get; set; }

        public int PrevRefSubNets { get; set; }
        public int PrevRefIPs { get; set; }
    }
}
