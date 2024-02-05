using Guncharted.PackageUpdater;
using McMaster.Extensions.CommandLineUtils;

var app = new CommandLineApplication();

app.HelpOption();
var packageNameArg = app.Option<string>("-p|--package-name <PACKAGE_NAME>", "Package name to update", CommandOptionType.SingleValue);
var targetVersionArg = app.Option<string>("-v|--target-version <VERSION>", "Target version of package", CommandOptionType.SingleValue);

app.OnExecuteAsync(async cancellationToken =>
{
    var packageName = packageNameArg.Value() ?? string.Empty;
    var targetVersion = targetVersionArg.Value() ?? string.Empty;

    var executor = new UpdateExecutor();
    await executor.Run(targetVersion, packageName);
});

return app.Execute(args);