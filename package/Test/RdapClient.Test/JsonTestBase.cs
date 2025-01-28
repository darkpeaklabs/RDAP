using System.IO;
using System.Reflection;

namespace DarkPeakLabs.Rdap.Test
{
    public abstract class JsonTestBase
    {
        protected static string ReadJsonFile(string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", filename);
            using StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd();
        }
    }
}
