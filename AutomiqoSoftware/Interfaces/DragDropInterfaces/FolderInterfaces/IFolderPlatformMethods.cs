namespace AutomiqoSoftware.Interfaces.DragDropInterfaces.FolderInterfaces;

public interface IFolderPlatformMethods { /* Cross platform interface to be instanced by each OS interface, 
                                            housing all automation methods.
                                         */
    public void DirectoryMover(string sourcePath, string targetPath);

}