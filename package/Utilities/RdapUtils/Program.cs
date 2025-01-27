using System.CommandLine;

namespace DarkPeakLabs.Rdap.Utilities;

internal sealed class Program
{
    static void Main(string[] args)
    {
        var rootCommand = new RootCommand("RDAP utility");

        var pathOption = new Option<FileInfo>(
            name: "--path",
            description: "Path to sources root");
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

        var generateCodeCommand = new Command("gen-code", "Generate Code")
        {
            pathOption
        };
        generateCodeCommand.SetHandler((fileInfo) =>
        {
            GenerateCode(fileInfo.FullName);
        },
        pathOption);

        rootCommand.AddCommand(generateCodeCommand);
        rootCommand.Invoke(args);
    }

    private static void GenerateCode(string solutionRoot)
    {
        var code = RdapEnumCodeGenerator.GenerateLinkRelationEnumAsync().GetAwaiter().GetResult();
        code.SaveAs(Path.Combine(solutionRoot, @"Rdap\Values\Json\RdapLinkRelationType.cs"));

        code = RdapEnumCodeGenerator.GenerateStatusEnumAsync().GetAwaiter().GetResult();
        code.SaveAs(Path.Combine(solutionRoot, @"Rdap\Values\Json\RdapStatus.cs"));

        code = RdapEnumCodeGenerator.GenerateEventActionEnumAsync().GetAwaiter().GetResult();
        code.SaveAs(Path.Combine(solutionRoot, @"Rdap\Values\Json\RdapEventAction.cs"));
        code = RdapEnumCodeGenerator.GenerateEntityRoleEnumAsync().GetAwaiter().GetResult();
        code.SaveAs(Path.Combine(solutionRoot, @"Rdap\Values\Json\RdapEntityRole.cs"));
        code = RdapEnumCodeGenerator.GenerateNoticeAndRemarkTypeEnumAsync().GetAwaiter().GetResult();
        code.SaveAs(Path.Combine(solutionRoot, @"Rdap\Values\Json\RdapNoticeAndRemarkType.cs"));
    }
}
