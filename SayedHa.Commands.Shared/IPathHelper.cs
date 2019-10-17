namespace SayedHa.Commands.Shared {
    public interface IPathHelper {
        bool ArePathsEqual(string path1, string path2);
        string FindFolderWithNameInOrAbove(string dirnameToFind, string startingDirectory);
        string GetFullPath(string path);
        string GetHomeFolder();
    }
}