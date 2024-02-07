#nullable enable
namespace SymbolicLinkerInstaller;
using System.Threading.Tasks;
using Microsoft.Win32;
using SymbolicLinker.Shared;
internal static class SystemRegistry {
    #region Key name & values
    // Files (Source)
    static readonly KeyValuePair<string, string> CreateSymbolicFileLink =
        new("symboliclinker.symbolicfile", "Create a symbolic link to this file");
    static readonly KeyValuePair<string, string> CreateHardFileLink =
        new("symboliclinker.hardfile", "Create a hard link to this file");
    static readonly KeyValuePair<string, string> CreateSymbolicFileLinkElevated =
        new("symboliclinker.symbolicfile.admin", "Create a symbolic link to this file (as admin)");
    static readonly KeyValuePair<string, string> CreateHardFileLinkElevated =
        new("symboliclinker.hardfile.admin", "Create a hard link to this file (as admin)");

    // Files (Destination)
    static readonly KeyValuePair<string, string> CreateSymbolicFileLinkHere =
        new("symboliclinker.symbolicfile.here", "Create a symbolic file link here");
    static readonly KeyValuePair<string, string> CreateHardFileLinkHere =
        new("symboliclinker.hardfile.here", "Create a hard file link here");
    static readonly KeyValuePair<string, string> CreateSymbolicFileLinkHereElevated =
        new("symboliclinker.symbolicfile.here.admin", "Create a symbolic file link here (as admin)");
    static readonly KeyValuePair<string, string> CreateHardFileLinkHereElevated =
        new("symboliclinker.hardfile.here.admin", "Create a hard file link here (as admin)");

    // Directories (Source)
    static readonly KeyValuePair<string, string> CreateSymbolicDirectoryLink =
        new("symboliclinker.symbolicfolder", "Create a symbolic link to this directory");
    static readonly KeyValuePair<string, string> CreateDirectoryJunction =
        new("symboliclinker.folderjunction", "Create a junction to this directory");
    static readonly KeyValuePair<string, string> CreateSymbolicDirectoryLinkElevated =
        new("symboliclinker.symbolicfolder.admin", "Create a symbolic link to this directory (as admin)");
    static readonly KeyValuePair<string, string> CreateDirectoryJunctionElevated =
        new("symboliclinker.folderjunction.admin", "Create a junction to this directory (as admin)");

    // Directories (Destination)
    static readonly KeyValuePair<string, string> CreateSymbolicDirectoryLinkHere =
        new("symboliclinker.symbolicfolder.here", "Create a symbolic directory link here");
    static readonly KeyValuePair<string, string> CreateDirectoryJunctionHere =
        new("symboliclinker.folderjunction.here", "Create a directory junction here");
    static readonly KeyValuePair<string, string> CreateSymbolicDirectoryLinkHereElevated =
        new("symboliclinker.symbolicfolder.here.admin", "Create a symbolic directory link here (as admin)");
    static readonly KeyValuePair<string, string> CreateDirectoryJunctionHereElevated =
        new("symboliclinker.folderjunction.here.admin", "Create a directory junction here (as admin)");

    // General
    static readonly KeyValuePair<string, string> OpenSymbolicLinker =
        new("symboliclinker.open", "Run Symbolic Linker");

    // The string that appears when right-clicking on a file, folder, or an empty directory space.
    const string SubMenuName = "&Symbolic linker";
    const string FileIcon = "imageres.dll,-2";
    const string FolderIcon = "imageres.dll,-3";
    const string FileCommand1 = "\"%1\"";
    const string FileCommandV = "\"%V\"";
    #endregion

    public static async Task<bool> SetRegistryKeysAsync() {
        return await Task.Run(() => {
            try {
                using RegistryKey Base = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                    Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

                InstallFileKeys(Base);
                InstallDirectoryKeys(Base);
                AddCommandKey(Base, OpenSymbolicLinker, $"\"{Installation.DestinationFile}\",0", string.Empty, string.Empty, true);
                return true;
            }
            catch {
                return false;
            }
        });
    }
    public static async Task<bool> RemoveRegistryKeysAsync() {
        return await Task.Run(() => {
            try {
                Registry.ClassesRoot.DeleteSubKey("*\\shell\\symbolic-linker");
                Registry.ClassesRoot.DeleteSubKey("Directory\\shell\\symbolic-linker");
                Registry.ClassesRoot.DeleteSubKey("Drive\\shell\\symbolic-linker");
                Registry.ClassesRoot.DeleteSubKey("Directory\\Background\\shell\\symbolic-linker");
                Registry.ClassesRoot.DeleteSubKey("Drive\\Background\\shell\\symbolic-linker");
#if INSTALL_LIBRARY_FOLDER
                Registry.ClassesRoot.DeleteSubKey("LibraryFolder\\Background\\shell\\symbolic-linker");
#endif

                using RegistryKey Base = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                    Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

                RemoveCommandKey(Base, CreateSymbolicFileLink);
                RemoveCommandKey(Base, CreateSymbolicFileLinkElevated);
                RemoveCommandKey(Base, CreateSymbolicFileLinkHere);
                RemoveCommandKey(Base, CreateSymbolicFileLinkHereElevated);
                RemoveCommandKey(Base, CreateHardFileLink);
                RemoveCommandKey(Base, CreateHardFileLinkElevated);
                RemoveCommandKey(Base, CreateHardFileLinkHere);
                RemoveCommandKey(Base, CreateHardFileLinkHereElevated);
                RemoveCommandKey(Base, CreateSymbolicDirectoryLink);
                RemoveCommandKey(Base, CreateSymbolicDirectoryLinkElevated);
                RemoveCommandKey(Base, CreateSymbolicDirectoryLinkHere);
                RemoveCommandKey(Base, CreateSymbolicDirectoryLinkHereElevated);
                RemoveCommandKey(Base, CreateDirectoryJunction);
                RemoveCommandKey(Base, CreateDirectoryJunctionElevated);
                RemoveCommandKey(Base, CreateDirectoryJunctionHere);
                RemoveCommandKey(Base, CreateDirectoryJunctionHereElevated);

                return true;
            }
            catch {
                return false;
            }
        });
    }

    private static void InstallFileKeys(RegistryKey BaseHive) {
        string JoinedCommands = JoinKeys(
            CreateSymbolicFileLink, CreateHardFileLink, CreateSymbolicFileLinkElevated, CreateHardFileLinkElevated, OpenSymbolicLinker);

        RegistryKey CurrentKey = Registry.ClassesRoot.CreateSubKey("*\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();

        AddCommandKey(BaseHive, CreateSymbolicFileLink, FileIcon, ArgumentCommands.CreateSymbolicFileLink, FileCommand1);
        AddCommandKey(BaseHive, CreateSymbolicFileLinkElevated, FileIcon, ArgumentCommands.CreateSymbolicFileLinkElevated, FileCommand1);
        AddCommandKey(BaseHive, CreateSymbolicFileLinkHere, FileIcon, ArgumentCommands.CreateSymbolicFileLinkHere, FileCommandV);
        AddCommandKey(BaseHive, CreateSymbolicFileLinkHereElevated, FileIcon, ArgumentCommands.CreateSymbolicFileLinkHereElevated, FileCommandV);
        AddCommandKey(BaseHive, CreateHardFileLink, FileIcon, ArgumentCommands.CreateHardFileLink, FileCommand1);
        AddCommandKey(BaseHive, CreateHardFileLinkElevated, FileIcon, ArgumentCommands.CreateHardFileLinkElevated, FileCommand1);
        AddCommandKey(BaseHive, CreateHardFileLinkHere, FileIcon, ArgumentCommands.CreateHardFileLinkHere, FileCommandV);
        AddCommandKey(BaseHive, CreateHardFileLinkHereElevated, FileIcon, ArgumentCommands.CreateHardFileLinkHereElevated, FileCommandV);
    }
    private static void InstallDirectoryKeys(RegistryKey BaseHive) {
        string JoinedCommands = JoinKeys(
            CreateSymbolicDirectoryLink, CreateDirectoryJunction, CreateSymbolicDirectoryLinkElevated, CreateDirectoryJunctionElevated, OpenSymbolicLinker);

        RegistryKey CurrentKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();

        CurrentKey = Registry.ClassesRoot.CreateSubKey("Drive\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();

        AddCommandKey(BaseHive, CreateSymbolicDirectoryLink, FolderIcon, ArgumentCommands.CreateSymbolicDirectoryLink, FileCommand1);
        AddCommandKey(BaseHive, CreateSymbolicDirectoryLinkElevated, FolderIcon, ArgumentCommands.CreateSymbolicDirectoryLinkElevated, FileCommand1);
        AddCommandKey(BaseHive, CreateSymbolicDirectoryLinkHere, FolderIcon, ArgumentCommands.CreateSymbolicDirectoryLinkHere, FileCommandV, true);
        AddCommandKey(BaseHive, CreateSymbolicDirectoryLinkHereElevated, FolderIcon, ArgumentCommands.CreateSymbolicDirectoryLinkHereElevated, FileCommandV);
        AddCommandKey(BaseHive, CreateDirectoryJunction, FolderIcon, ArgumentCommands.CreateDirectoryJunction, FileCommand1);
        AddCommandKey(BaseHive, CreateDirectoryJunctionElevated, FolderIcon, ArgumentCommands.CreateDirectoryJunctionElevated, FileCommand1);
        AddCommandKey(BaseHive, CreateDirectoryJunctionHere, FolderIcon, ArgumentCommands.CreateDirectoryJunctionHere, FileCommandV);
        AddCommandKey(BaseHive, CreateDirectoryJunctionHereElevated, FolderIcon, ArgumentCommands.CreateDirectoryJunctionHereElevated, FileCommandV);

        JoinedCommands = JoinKeys(
            CreateSymbolicFileLinkHere, CreateHardFileLinkHere, CreateSymbolicFileLinkHereElevated, CreateHardFileLinkHereElevated,
            CreateSymbolicDirectoryLinkHere, CreateDirectoryJunctionHere, CreateSymbolicDirectoryLinkHereElevated, CreateDirectoryJunctionHereElevated,
            OpenSymbolicLinker);

        CurrentKey = Registry.ClassesRoot.CreateSubKey("Directory\\Background\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        //CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();

        CurrentKey = Registry.ClassesRoot.CreateSubKey("Drive\\Background\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        //CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();

#if INSTALL_LIBRARY_FOLDER
        CurrentKey = Registry.ClassesRoot.CreateSubKey("LibraryFolder\\Background\\shell\\symbolic-linker");
        CurrentKey.SetValue("MUIVerb", SubMenuName);
        CurrentKey.SetValue("SubCommands", JoinedCommands);
        //CurrentKey.SetValue("SeparatorBefore", string.Empty);
        CurrentKey.FinalizeKey();
#endif
    }

    private static void AddCommandKey(RegistryKey BaseHive, KeyValuePair<string, string> KeyValue, string Icon, string ArgumentCommand, string FileCommand, bool SeparatorAbove = false) {
        var CurrentKey = BaseHive.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\" + KeyValue.Key);
        CurrentKey.SetValue("", KeyValue.Value);
        CurrentKey.SetValue("MUIVerb", KeyValue.Value);
        CurrentKey.SetValue("Icon", Icon);

        if (SeparatorAbove) {
            CurrentKey.SetValue("CommandFlags", 0x00000020);
        }

        var SubKey = CurrentKey.CreateSubKey("command");
        SubKey.SetValue("", $"\"{Installation.DestinationFile}\" {ArgumentCommand} {FileCommand}".TrimEnd());

        SubKey.FinalizeKey();
        CurrentKey.FinalizeKey();
    }
    private static void RemoveCommandKey(RegistryKey BaseHive, KeyValuePair<string, string> KeyValue) {
        BaseHive.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\" + KeyValue.Key, false);
    }
    private static void FinalizeKey(this RegistryKey key) {
        key.Flush();
        key.Close();
    }
    private static string JoinKeys(params KeyValuePair<string, string>[] keys) {
        return string.Join(";", keys.Select(x => x.Key));
    }
}
