using AutomiqoSoftware.Interfaces.DragDropInterfaces.FolderInterfaces;

namespace AutomiqoSoftware.DragDropServices.WindowsServices;

public class WindowsFolderMover : IFolderPlatformMethods {
    public void DirectoryMover(string sourcePath, string targetPath) {
        Directory.Move(sourcePath, targetPath);
    }
}