namespace Application.Helpers;

public static class PathHelper
{
    public static DirectoryInfo GetSolutionPath(string currentPath = null!)
    {
        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory!;
    }
}
