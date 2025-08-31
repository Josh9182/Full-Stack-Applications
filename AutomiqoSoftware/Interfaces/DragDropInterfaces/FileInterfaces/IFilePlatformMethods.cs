namespace AutomiqoSoftware.Interfaces.DragDropInterfaces.FileInterfaces;

public interface IFilePlatformMethods { /* Cross platform interface to be instanced by each OS interface, 
                                            housing all automation methods.
                                         */
    void FileMover(string sourcePath, string targetPath); // Move a file from one directory to another.

}