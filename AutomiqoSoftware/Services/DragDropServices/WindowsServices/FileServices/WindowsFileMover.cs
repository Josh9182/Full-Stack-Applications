using AutomiqoSoftware.Interfaces.DragDropInterfaces.FileInterfaces;

namespace AutomiqoSoftware.DragDropServices.WindowsServices;

public class WindowsFileMover : IFilePlatformMethods {
    public void FileMover(string sourcePath, string targetPath) {
        File.Move(sourcePath, targetPath); 
    }
}