
using DarkPeakLabs.Rdap.Serialization;

namespace DarkPeakLabs.Rdap.Utilities
{
    internal sealed class RdapJsonParser
    {
        private readonly string _path;

        public RdapJsonParser(string path)
        {
            _path = path;
        }

        internal void Run()
        {
            foreach(var file in Directory.EnumerateFiles(_path))
            {
                try
                {
                    using StreamReader reader = new StreamReader(file);
                    var json = reader.ReadToEnd();
                    var domains = RdapSerializer.Deserialize<RdapDomainLookupResponse>(json);
                }
                catch(RdapJsonException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
