using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DarkPeakLabs.Rdap.Test
{
    [TestClass]
    public class JsonTest
    {
        private ILoggerFactory _loggerFactory;
        ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerFactory = LoggerFactory.Create(config =>
            {
                config.SetMinimumLevel(LogLevel.Debug);
                config.AddDebug();
            });

            _logger = _loggerFactory.CreateLogger<JsonTest>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _loggerFactory.Dispose();
        }
    }
}
