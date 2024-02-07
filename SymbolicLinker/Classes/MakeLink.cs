#nullable enable
namespace SymbolicLinker;
using System.IO;
using System.Windows.Forms;
internal static class MakeLink {
    /// <summary>
    ///     Creates a symbolic file link.
    /// </summary>
    /// <param name="Source">
    ///     The file that the link will be pointing to.
    /// </param>
    /// <param name="DestinationDirectory">
    ///     The directory the link will be created at.
    /// </param>
    /// <param name="DestinationFileName">
    ///     The name of the destination link, not including the path.
    ///     If <see langword="null"/>, it will use the name of the source.
    /// </param>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateFileSymbolicLink(string Source, string DestinationDirectory, string? DestinationFileName, bool Elevated) {
        if (!ValidParameters(Source, DestinationDirectory, false)) {
            return;
        }
        DestinationFileName ??= Path.GetFileName(Source);
        string Destination = DestinationDirectory + Path.DirectorySeparatorChar + DestinationFileName;
        if (Win32.DestinationExists(Destination) && !ExistingLinkShouldContinue(Destination, false, out var NewName)) {
            if (NewName != null) {
                CreateFileSymbolicLink(Source, DestinationDirectory, NewName, Elevated);
            }
            return;
        }
        HandleCommand($"mklink \"{Destination}\" \"{Source}\"", Elevated);
    }
    /// <summary>
    ///     Creates a symbolic file link.
    /// </summary>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateFileSymbolicLink(bool Elevated) {
        using OpenFileDialog ofd = new() {
            Filter = Const.AllFilesFilter,
            Title = "Select file(s) to create symbolic links for...",
            Multiselect = true,
        };

        if (ofd.ShowDialog() != DialogResult.OK || ofd.FileNames.Length < 1) {
            return;
        }

        if (ofd.FileNames.Length > 1) {
            var Result = MessageBox.Show(
                "Would you like to specify individual destinations?",
                Const.DialogProgramName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel) {
                return;
            }

            if (Result == DialogResult.Yes) {
                string InitialDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
                using SaveFileDialog sfd = new() {
                    Filter = Const.AllFilesFilter,
                    Title = "Create symbolic file link at..."
                };

                for (int i = 0; i < ofd.FileNames.Length; i++) {
                    string CurrentFile = ofd.FileNames[i];

                    sfd.FileName = Path.GetFileName(CurrentFile);
                    sfd.InitialDirectory = InitialDirectory;

                    if (sfd.ShowDialog() != DialogResult.OK) {
                        continue;
                    }

                    InitialDirectory = Path.GetDirectoryName(sfd.FileName);
                    MakeLink.CreateFileSymbolicLink(
                        Source: CurrentFile,
                        DestinationDirectory: InitialDirectory,
                        DestinationFileName: Path.GetFileName(sfd.FileName),
                        Elevated: Elevated);
                }

                return;
            }

            using BetterFolderBrowserNS.BetterFolderBrowser fbd = new() {
                Multiselect = false,
                InitialDirectory = Path.GetDirectoryName(ofd.FileNames[0]),
            };

            if (fbd.ShowDialog() != DialogResult.OK) {
                return;
            }

            for (int i = 0; i < ofd.FileNames.Length; i++) {
                string CurrentFile = ofd.FileNames[i];
                MakeLink.CreateFileSymbolicLink(
                    Source: CurrentFile,
                    DestinationDirectory: fbd.SelectedPath,
                    DestinationFileName: Path.GetFileName(CurrentFile),
                    Elevated: Elevated);
            }
        }
        else {
            string CurrentFile = ofd.FileNames[0];
            using SaveFileDialog sfd = new() {
                Filter = Const.AllFilesFilter,
                Title = "Create symbolic file link at...",
                FileName = Path.GetFileName(CurrentFile),
                InitialDirectory = Path.GetDirectoryName(CurrentFile),
            };

            if (sfd.ShowDialog() == DialogResult.OK) {
                MakeLink.CreateFileSymbolicLink(
                    Source: CurrentFile,
                    DestinationDirectory: Path.GetDirectoryName(sfd.FileName),
                    DestinationFileName: Path.GetFileName(sfd.FileName),
                    Elevated: Elevated);
            }
        }
    }

    /// <summary>
    ///     Creates a hard file link.
    /// </summary>
    /// <param name="Source">
    ///     The file that the link will be pointing to.
    /// </param>
    /// <param name="DestinationDirectory">
    ///     The directory the link will be created at.
    /// </param>
    /// <param name="DestinationFileName">
    ///     The name of the destination link, not including the path.
    ///     If <see langword="null"/>, it will use the name of the source.
    /// </param>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateFileHardLink(string Source, string DestinationDirectory, string? DestinationFileName, bool Elevated) {
        if (!ValidParameters(Source, DestinationDirectory, false)) {
            return;
        }
        DestinationFileName ??= Path.GetFileName(Source);
        string Destination = DestinationDirectory + Path.DirectorySeparatorChar + DestinationFileName;
        if (Win32.DestinationExists(Destination) && !ExistingLinkShouldContinue(Destination, false, out var NewName)) {
            if (NewName != null) {
                CreateFileHardLink(Source, DestinationDirectory, NewName, Elevated);
            }
            return;
        }
        HandleCommand($"mklink /H \"{Destination}\" \"{Source}\"", Elevated);
    }
    /// <summary>
    ///     Creates a hard file link.
    /// </summary>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateFileHardLink(bool Elevated) {
        using OpenFileDialog ofd = new() {
            Filter = Const.AllFilesFilter,
            Title = "Select file(s) to create hard links for...",
            Multiselect = true,
        };

        if (ofd.ShowDialog() != DialogResult.OK || ofd.FileNames.Length < 1) {
            return;
        }

        if (ofd.FileNames.Length > 1) {
            var Result = MessageBox.Show(
                "Would you like to specify individual destinations?",
                Const.DialogProgramName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel) {
                return;
            }

            if (Result == DialogResult.Yes) {
                string InitialDirectory = Path.GetDirectoryName(ofd.FileNames[0]);
                using SaveFileDialog sfd = new() {
                    Filter = Const.AllFilesFilter,
                    Title = "Create hard file link at...",
                };

                for (int i = 0; i < ofd.FileNames.Length; i++) {
                    string CurrentFile = ofd.FileNames[i];

                    sfd.FileName = Path.GetFileName(CurrentFile);
                    sfd.InitialDirectory = InitialDirectory;

                    if (sfd.ShowDialog() != DialogResult.OK) {
                        continue;
                    }

                    InitialDirectory = Path.GetDirectoryName(sfd.FileName);
                    MakeLink.CreateFileHardLink(
                        Source: CurrentFile,
                        DestinationDirectory: InitialDirectory,
                        DestinationFileName: Path.GetFileName(sfd.FileName),
                        Elevated: Elevated);
                }

                return;
            }

            using BetterFolderBrowserNS.BetterFolderBrowser fbd = new() {
                Multiselect = false,
                InitialDirectory = Path.GetDirectoryName(ofd.FileNames[0]),
            };

            if (fbd.ShowDialog() != DialogResult.OK) {
                return;
            }

            for (int i = 0; i < ofd.FileNames.Length; i++) {
                string CurrentFile = ofd.FileNames[i];
                MakeLink.CreateFileHardLink(
                    Source: CurrentFile,
                    DestinationDirectory: fbd.SelectedPath,
                    DestinationFileName: Path.GetFileName(CurrentFile),
                    Elevated: Elevated);
            }
        }
        else {
            string CurrentFile = ofd.FileNames[0];
            using SaveFileDialog sfd = new() {
                Filter = Const.AllFilesFilter,
                Title = "Create hard file link at...",
                FileName = Path.GetFileName(CurrentFile),
                InitialDirectory = Path.GetDirectoryName(CurrentFile),
            };

            if (sfd.ShowDialog() == DialogResult.OK) {
                MakeLink.CreateFileHardLink(
                    Source: CurrentFile,
                    DestinationDirectory: Path.GetDirectoryName(sfd.FileName),
                    DestinationFileName: Path.GetFileName(sfd.FileName),
                    Elevated: Elevated);
            }
        }
    }

    /// <summary>
    ///     Creates a symbolic directory link.
    /// </summary>
    /// <param name="Source">
    ///     The directory that the link will be pointing to.
    /// </param>
    /// <param name="DestinationDirectory">
    ///     The directory the link will be created at.
    /// </param>
    /// <param name="DestinationFolderName">
    ///     The name of the destination link, not including the path.
    ///     If <see langword="null"/>, it will use the name of the source.
    /// </param>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateDirectorySymbolicLink(string Source, string DestinationDirectory, string? DestinationFolderName, bool Elevated) {
        if (!ValidParameters(Source, DestinationDirectory, true)) {
            return;
        }
        DestinationFolderName ??= Path.GetFileName(Source);
        string Destination = DestinationDirectory + Path.DirectorySeparatorChar + DestinationFolderName;
        if (Win32.DestinationExists(Destination) && !ExistingLinkShouldContinue(Destination, true, out var NewName)) {
            if (NewName != null) {
                CreateDirectorySymbolicLink(Source, DestinationDirectory, NewName, Elevated);
            }
            return;
        }
        HandleCommand($"mklink /D \"{Destination}\" \"{Source}\"", Elevated);
    }
    /// <summary>
    ///     Creates a symbolic directory link.
    /// </summary>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateDirectorySymbolicLink(bool Elevated) {
        using BetterFolderBrowserNS.BetterFolderBrowser fbd = new() {
            Title = "Select folder(s) to create symbolic links for...",
            Multiselect = true,
        };

        if (fbd.ShowDialog() != DialogResult.OK || fbd.SelectedPaths.Length < 1) {
            return;
        }

        if (fbd.SelectedPaths.Length > 1) {
            var Result = MessageBox.Show(
                "Would you like to specify individual destinations for the folders?",
                Const.DialogProgramName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel) {
                return;
            }

            using BetterFolderBrowserNS.BetterFolderBrowser fbd2 = new() {
                Title = "Create symbolic folder link at...",
                Multiselect = false,
            };

            if (Result == DialogResult.Yes) {
                string InitialDirectory = Path.GetDirectoryName(fbd.SelectedPaths[0]);

                for (int i = 0; i < fbd.SelectedPaths.Length; i++) {
                    string CurrentFolder = fbd.SelectedPaths[i];

                    fbd2.InitialDirectory = InitialDirectory;
                    if (fbd2.ShowDialog() != DialogResult.OK) {
                        continue;
                    }

                    InitialDirectory = fbd2.SelectedPath;
                    MakeLink.CreateDirectorySymbolicLink(
                        Source: CurrentFolder,
                        DestinationDirectory: InitialDirectory,
                        DestinationFolderName: Path.GetFileName(CurrentFolder),
                        Elevated: Elevated);
                }

                return;
            }

            fbd2.InitialDirectory = Path.GetDirectoryName(fbd.SelectedPaths[0]);

            if (fbd2.ShowDialog() != DialogResult.OK) {
                return;
            }

            for (int i = 0; i < fbd.SelectedPaths.Length; i++) {
                string CurrentFolder = fbd.SelectedPaths[i];
                MakeLink.CreateDirectorySymbolicLink(
                    Source: CurrentFolder,
                    DestinationDirectory: fbd2.SelectedPath,
                    DestinationFolderName: Path.GetFileName(CurrentFolder),
                    Elevated: Elevated);
            }
        }
        else {
            string CurrentFolder = fbd.SelectedPaths[0];
            using BetterFolderBrowserNS.BetterFolderBrowser fbd2 = new() {
                Title = "Create symbolic folder link at...",
                InitialDirectory = Path.GetDirectoryName(CurrentFolder),
                Multiselect = false,
            };

            if (fbd2.ShowDialog() == DialogResult.OK) {
                MakeLink.CreateDirectorySymbolicLink(
                    Source: CurrentFolder,
                    DestinationDirectory: fbd2.SelectedPath,
                    DestinationFolderName: Path.GetFileName(CurrentFolder),
                    Elevated: Elevated);
            }
        }
    }

    /// <summary>
    ///     Creates a directory junction.
    /// </summary>
    /// <param name="Source">
    ///     The directory that the junction will be pointing to.
    /// </param>
    /// <param name="DestinationDirectory">
    ///     The directory the link will be created at.
    /// </param>
    /// <param name="DestinationFolderName">
    ///     The name of the destination link, not including the path.
    ///     If <see langword="null"/>, it will use the name of the source.
    /// </param>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateDirectoryJunction(string Source, string DestinationDirectory, string? DestinationFolderName, bool Elevated) {
        if (!ValidParameters(Source, DestinationDirectory, true)) {
            return;
        }
        DestinationFolderName ??= Path.GetFileName(Source);
        string Destination = DestinationDirectory + Path.DirectorySeparatorChar + DestinationFolderName;
        if (Win32.DestinationExists(Destination) && !ExistingLinkShouldContinue(Destination, true, out var NewName)) {
            if (NewName != null) {
                CreateDirectoryJunction(Source, DestinationDirectory, NewName, Elevated);
            }
            return;
        }
        HandleCommand($"mklink /J \"{Destination}\" \"{Source}\"", Elevated);
    }
    /// <summary>
    ///     Creates a directory junction.
    /// </summary>
    /// <param name="Elevated">
    ///     Whether the symbolic link will be created with administrative permissions.
    /// </param>
    public static void CreateDirectoryJunction(bool Elevated) {
        using BetterFolderBrowserNS.BetterFolderBrowser fbd = new() {
            Title = "Select folder(s) to create junctions for...",
            Multiselect = true,
        };

        if (fbd.ShowDialog() != DialogResult.OK || fbd.SelectedPaths.Length < 1) {
            return;
        }

        if (fbd.SelectedPaths.Length > 1) {
            var Result = MessageBox.Show(
                "Would you like to specify individual destinations for the folders?",
                Const.DialogProgramName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel) {
                return;
            }

            using BetterFolderBrowserNS.BetterFolderBrowser fbd2 = new() {
                Title = "Create folder junction at...",
                Multiselect = false,
            };

            if (Result == DialogResult.Yes) {
                string InitialDirectory = Path.GetDirectoryName(fbd.SelectedPaths[0]);

                for (int i = 0; i < fbd.SelectedPaths.Length; i++) {
                    string CurrentFolder = fbd.SelectedPaths[i];

                    fbd2.InitialDirectory = InitialDirectory;
                    if (fbd2.ShowDialog() != DialogResult.OK) {
                        continue;
                    }

                    InitialDirectory = fbd2.SelectedPath;
                    MakeLink.CreateDirectoryJunction(
                        Source: CurrentFolder,
                        DestinationDirectory: InitialDirectory,
                        DestinationFolderName: Path.GetFileName(CurrentFolder),
                        Elevated: Elevated);
                }

                return;
            }

            fbd2.InitialDirectory = Path.GetDirectoryName(fbd.SelectedPaths[0]);

            if (fbd2.ShowDialog() != DialogResult.OK) {
                return;
            }

            for (int i = 0; i < fbd.SelectedPaths.Length; i++) {
                string CurrentFolder = fbd.SelectedPaths[i];
                MakeLink.CreateDirectoryJunction(
                    Source: CurrentFolder,
                    DestinationDirectory: fbd2.SelectedPath,
                    DestinationFolderName: Path.GetFileName(CurrentFolder),
                    Elevated: Elevated);
            }
        }
        else {
            string CurrentFolder = fbd.SelectedPaths[0];
            using BetterFolderBrowserNS.BetterFolderBrowser fbd2 = new() {
                Title = "Create folder junction at...",
                InitialDirectory = Path.GetDirectoryName(CurrentFolder),
                Multiselect = false,
            };

            if (fbd2.ShowDialog() == DialogResult.OK) {
                MakeLink.CreateDirectoryJunction(
                    Source: CurrentFolder,
                    DestinationDirectory: fbd2.SelectedPath,
                    DestinationFolderName: Path.GetFileName(CurrentFolder),
                    Elevated: Elevated);
            }
        }
    }

    private static bool ValidParameters(string Source, string DestinationDirectory, bool IsDirectory) {
        if (Source.IsNullEmptyWhitespace()) {
            Win32.ErrorMessageBox("Cannot make the link, the source for the symbolic link is null, empty, or whitespace.");
            return false;
        }

        if (DestinationDirectory.IsNullEmptyWhitespace()) {
            Win32.ErrorMessageBox("Cannot make the link, the destination directory is null, empty, or whitespace.");
            return false;
        }

        if (!Win32.DestinationExists(Source)) {
            Win32.ErrorMessageBox($"The source {(IsDirectory ? "directory" : "file")} does not exist.");
            return false;
        }

        if (!Win32.DestinationExists(DestinationDirectory)) {
            Win32.ErrorMessageBox("The destination directory does not exist.");
            return false;
        }

        return true;
    }
    private static string? GetBackupName(string Destination) {
        const string BackupName = ".symbck";

        string NewBackupName = Destination + BackupName;
        if (!Win32.DestinationExists(NewBackupName)) {
            return NewBackupName;
        }

        ulong BackupIteration = ulong.MinValue;
        while (true) {
            NewBackupName = Destination + BackupName + BackupIteration++;
            if (!Win32.DestinationExists(NewBackupName)) {
                return NewBackupName;
            }
            if (BackupIteration == ulong.MaxValue) {
                return null;
            }
        }
    }
    private static bool ExistingLinkShouldContinue(string Destination, bool IsDirectory, out string? NewName) {
        NewName = null;

        using frmLinkExists ExistForm = new();
        if (ExistForm.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
            return false;
        }

        switch (ExistForm.Action) {
            case ExistingAction.Backup: {
                const string CouldntMakeBackup_TooManyBackups = "Couldn't make backup, there are too many existing backups.";
                const string CouldntMakeBackup_TooLongName = "Couldn't make backup, the backup name is too long.";

                if (IsDirectory) {
                    string? BackupFileName = GetBackupName(Destination);
                    if (BackupFileName == null) {
                        Win32.ErrorMessageBox(CouldntMakeBackup_TooManyBackups);
                        return false;
                    }

                    try {
                        Directory.Move(Destination, BackupFileName);
                    }
                    catch (DirectoryNotFoundException) { }
                    catch (PathTooLongException) {
                        Win32.ErrorMessageBox(CouldntMakeBackup_TooLongName);
                        return false;
                    }
                    catch (Exception ex) {
                        Win32.ErrorMessageBox("Could not make backup.\n\n" + ex);
                    }
                }
                else {
                    string? BackupFileName = GetBackupName(Destination);
                    if (BackupFileName == null) {
                        Win32.ErrorMessageBox(CouldntMakeBackup_TooManyBackups);
                        return false;
                    }

                    try {
                        File.Move(Destination, BackupFileName);
                    }
                    catch (DirectoryNotFoundException) { }
                    catch (PathTooLongException) {
                        Win32.ErrorMessageBox(CouldntMakeBackup_TooLongName);
                        return false;
                    }
                    catch (Exception ex) {
                        Win32.ErrorMessageBox("Could not make backup.\n\n" + ex);
                    }
                }
            } break;
            case ExistingAction.Overwrite: {
                if (IsDirectory) {
                    try {
                        Directory.Delete(Destination, true);
                    }
                    catch (DirectoryNotFoundException) { }
                    catch (PathTooLongException) { }
                    catch (Exception ex) {
                        Win32.ErrorMessageBox("Could not delete the directory.\n\n" + ex);
                    }
                }
                else {
                    try {
                        File.Delete(Destination);
                    }
                    catch (DirectoryNotFoundException) { }
                    catch (PathTooLongException) { }
                    catch (Exception ex) {
                        Win32.ErrorMessageBox("Could not delete the file.\n\n" + ex);
                    }
                }
            } break;
            case ExistingAction.ChangeExistingName: {
                string BaseDestination = Path.GetDirectoryName(Destination);
                string BaseFileName = Path.GetFileName(Destination);
                string NewDestination;

                try {
                    if (IsDirectory) {
                        do {
                            GetNewName(BaseFileName, out NewName);
                            if (NewName == null) {
                                return false;
                            }
                            NewDestination = Path.GetDirectoryName(Destination) + Path.DirectorySeparatorChar + NewName;
                        } while (Win32.DestinationExists(NewDestination));
                        Directory.Move(Destination, NewDestination);
                    }
                    else {
                        do {
                            GetNewName(BaseFileName, out NewName);
                            if (NewName == null) {
                                return false;
                            }
                            NewDestination = Path.GetDirectoryName(Destination) + Path.DirectorySeparatorChar + NewName;
                        } while (Win32.DestinationExists(NewDestination));
                        File.Move(Destination, NewDestination);
                    }
                }
                catch (DirectoryNotFoundException) { }
                catch (PathTooLongException) {
                    Win32.ErrorMessageBox("The new path is too long.");
                    return false;
                }
                catch (Exception ex) {
                    Win32.ErrorMessageBox("Unable to change the name.\n\n" + ex);
                    return false;
                }
            } break;
            case ExistingAction.NewName: {
                GetNewName(Path.GetFileName(Destination), out NewName);
            } return false;
            default: return false;
        }

        return true;
    }
    private static void GetNewName(string Name, out string? NewName) {
        NewName = Microsoft.VisualBasic.Interaction.InputBox("Enter a name that will be used:", Title: "Name required", DefaultResponse: Name);
        if (NewName.IsEmptyWhitespace()) {
            NewName = null;
        }
    }

    private static void HandleCommand(string Command, bool Elevated) {
        try {
            int ExitCode = Win32.RunCommand(Command, Elevated);
            if (ExitCode != 0) {
                System.Windows.Forms.MessageBox.Show(
                    $"The command exited with code {ExitCode}. You may need to create the link as an administrator.",
                    Const.DialogProgramName,
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        catch {
            System.Windows.Forms.MessageBox.Show(
                    "Unable to start the program \"cmd.exe\". If you are unable to use command prompt, then this application is useless.",
                    Const.DialogProgramName,
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
}
