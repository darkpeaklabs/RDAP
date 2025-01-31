using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal record VCardProperty
    {
        internal string Name;
        internal string Type;
        internal Dictionary<string, List<string>> Parameters;
        internal List<string> Values;
    }
}
