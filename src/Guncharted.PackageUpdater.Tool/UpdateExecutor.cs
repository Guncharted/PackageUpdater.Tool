namespace Guncharted.PackageUpdater;

internal class UpdateExecutor
{
    public async Task Run(string targetVersion, string packageName)
    {
        if (string.IsNullOrWhiteSpace(targetVersion) || string.IsNullOrWhiteSpace(packageName))
        {
            LogConsole("Unable to update versions - incorrect arguments passed");
            return;
        }

        await Console.Out.WriteLineAsync($"WORKING DIRECTORY - {Environment.CurrentDirectory}\n");

        var directoryFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.csproj", SearchOption.AllDirectories);

        foreach (var directoryFile in directoryFiles)
        {
            var text = (await File.ReadAllLinesAsync(directoryFile)).ToList();

            var shortFileName = directoryFile.Split('\\').LastOrDefault();

            var targetLine = text.Where((x, i) => x.Contains(packageName) && x.Contains("PackageReference")).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(targetLine))
                continue;

            LogConsole($"---\nSetting version of {packageName}-{targetVersion} for {shortFileName}");

            var index = text.IndexOf(targetLine);
            text[index] = GenerateNewPackageReferenceNode(packageName, targetVersion, targetLine);

            await File.WriteAllLinesAsync(directoryFile, text);

            LogConsole($"Updated package references for {shortFileName}\n---\n");
        }
    }

    private string GenerateNewPackageReferenceNode(string packageName, string targetVersion, string currentRecord)
    {
        var newRecord = $"<PackageReference Include=\"{packageName}\" Version=\"{targetVersion}\" />";
        return currentRecord.Replace(currentRecord.Trim(), newRecord);
    }

    private void LogConsole(string text) => Console.WriteLine(text);
}
