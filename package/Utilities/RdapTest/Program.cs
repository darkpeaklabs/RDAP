using System.CommandLine;

namespace DarkPeakLabs.Rdap.Utilities;

internal sealed class Program
{
    static void Main(string[] args)
    {
        var rootCommand = new RootCommand("RDAP test utility");

        var pathOption = new Option<FileInfo>(
            name: "--path",
            description: "Path to results");
        pathOption.AddAlias("-p");
        pathOption.IsRequired = true;
        pathOption.AddValidator(validationResult =>
        {
            var fileInfo = validationResult.GetValueForOption(pathOption);
            if (fileInfo != null)
            {
                if (!Directory.Exists(fileInfo.FullName))
                {
                    validationResult.ErrorMessage = $"Path '{fileInfo.FullName}' does not exist or is not a directory";
                }                    
            }
            else
            {
                validationResult.ErrorMessage = $"Invalid path option";
            }
        });

        var testCommand = new Command("test", "Run test")
        {
            pathOption
        };
        testCommand.SetHandler(async (fileInfo) =>
        {
            string path = fileInfo.FullName;
            if (Directory.Exists(path))
            {

            }
            var rdapTest = new RdapClientTest(path);
            await rdapTest.RunTestAsync().ConfigureAwait(false);
        },
        pathOption);

        rootCommand.AddCommand(testCommand);
        rootCommand.Invoke(args);
    }
}
