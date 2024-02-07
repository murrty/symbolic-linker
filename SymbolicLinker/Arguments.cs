#nullable enable
namespace SymbolicLinker;
using System.IO;
using System.Windows.Forms;
using SymbolicLinker.Shared;
internal static class Arguments {
    public static bool HandleArguments(string[] args) {
        if (args.Length == 0) {
            return false;
        }

        bool Elevated = false;
        string? Source;
        string? Destination;

        switch (args[0].ToLowerInvariant()) {
            case ArgumentCommands.CreateSymbolicFileLink: {
                if (args.Length < 2 || !File.Exists(Source = args[1])) {
                    MakeLink.CreateFileSymbolicLink(Elevated);
                    return true;
                }

                Destination = (args.Length > 2) ?
                    args[2] : GetFolder(Path.GetDirectoryName(Source), "Select a directory to create the symbolic link at...");

                if (Directory.Exists(Destination)) {
                    Destination += Path.DirectorySeparatorChar + Path.GetFileName(Source);
                }

                string DestinationDirectory = Path.GetDirectoryName(Destination);
                string DestinationName = Path.GetFileName(Destination);

                Directory.CreateDirectory(DestinationDirectory);
                MakeLink.CreateFileSymbolicLink(
                    Source: Source,
                    DestinationDirectory: DestinationDirectory,
                    DestinationFileName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateSymbolicFileLinkElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateSymbolicFileLink;

            case ArgumentCommands.CreateHardFileLink: {
                if (args.Length < 2 || !File.Exists(Source = args[1])) {
                    MakeLink.CreateFileHardLink(Elevated);
                    return true;
                }

                Destination = (args.Length > 2) ?
                    args[2] : GetFolder(Path.GetDirectoryName(Source), "Select a directory to create the hard link at...");

                if (Directory.Exists(Destination)) {
                    Destination += Path.DirectorySeparatorChar + Path.GetFileName(Source);
                }

                string DestinationDirectory = Path.GetDirectoryName(Destination);
                string DestinationName = Path.GetFileName(Destination);

                Directory.CreateDirectory(DestinationDirectory);
                MakeLink.CreateFileHardLink(
                    Source: Source,
                    DestinationDirectory: DestinationDirectory,
                    DestinationFileName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateHardFileLinkElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateHardFileLink;

            case ArgumentCommands.CreateSymbolicFileLinkHere: {
                if (args.Length < 2 || !Directory.Exists(Destination = args[1])) {
                    MakeLink.CreateFileSymbolicLink(Elevated);
                    return true;
                }

                Source = GetFile(Destination, "Select a file to create a symbolic link to...");

                if (!File.Exists(Source)) {
                    return true;
                }

                string DestinationName = Path.GetFileName(Source);

                Directory.CreateDirectory(Destination);
                MakeLink.CreateFileSymbolicLink(
                    Source: Source!,
                    DestinationDirectory: Destination,
                    DestinationFileName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateSymbolicFileLinkHereElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateSymbolicFileLinkHere;

            case ArgumentCommands.CreateHardFileLinkHere: {
                if (args.Length < 2 || !Directory.Exists(Destination = args[1])) {
                    MakeLink.CreateFileHardLink(Elevated);
                    return true;
                }

                Source = GetFile(Destination, "Select a file to create a hard link to...");

                if (!File.Exists(Source)) {
                    return true;
                }

                string DestinationName = Path.GetFileName(Source);

                Directory.CreateDirectory(Destination);
                MakeLink.CreateFileHardLink(
                    Source: Source!,
                    DestinationDirectory: Destination,
                    DestinationFileName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateHardFileLinkHereElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateHardFileLinkHere;

            // ------------- \\

            case ArgumentCommands.CreateSymbolicDirectoryLink: {
                if (args.Length < 2 || !Directory.Exists(Source = args[1])) {
                    MakeLink.CreateDirectorySymbolicLink(Elevated);
                    return true;
                }

                Destination = (args.Length > 2) ?
                    args[2] : GetFolder(Path.GetDirectoryName(Source), "Select a directory to create the symbolic link at...");

                if (Directory.Exists(Destination)) {
                    Destination += Path.DirectorySeparatorChar + Path.GetFileName(Source);
                }

                string DestinationDirectory = Path.GetDirectoryName(Destination);
                string DestinationName = Path.GetFileName(Destination);

                Directory.CreateDirectory(DestinationDirectory);
                MakeLink.CreateDirectorySymbolicLink(
                    Source: Source,
                    DestinationDirectory: DestinationDirectory,
                    DestinationFolderName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateSymbolicDirectoryLinkElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateSymbolicDirectoryLink;

            case ArgumentCommands.CreateDirectoryJunction: {
                if (args.Length < 2 || !Directory.Exists(Source = args[1])) {
                    MakeLink.CreateDirectoryJunction(Elevated);
                    return true;
                }

                Destination = (args.Length > 2) ?
                    args[2] : GetFolder(Path.GetDirectoryName(Source), "Select a directory to create the symbolic link at...");

                if (Directory.Exists(Destination)) {
                    Destination += Path.DirectorySeparatorChar + Path.GetFileName(Source);
                }

                string DestinationDirectory = Path.GetDirectoryName(Destination);
                string DestinationName = Path.GetFileName(Destination);

                Directory.CreateDirectory(DestinationDirectory);
                MakeLink.CreateDirectoryJunction(
                    Source: Source,
                    DestinationDirectory: DestinationDirectory,
                    DestinationFolderName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateDirectoryJunctionElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateDirectoryJunction;

            case ArgumentCommands.CreateSymbolicDirectoryLinkHere: {
                if (args.Length < 2 || !Directory.Exists(Destination = args[1])) {
                    MakeLink.CreateDirectorySymbolicLink(Elevated);
                    return true;
                }

                Source = GetFolder(Destination, "Select a folder to create a symbolic link to...");

                if (!Directory.Exists(Source)) {
                    return true;
                }

                string DestinationName = Path.GetFileName(Source);

                Directory.CreateDirectory(Destination);
                MakeLink.CreateDirectorySymbolicLink(
                    Source: Source!,
                    DestinationDirectory: Destination,
                    DestinationFolderName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateSymbolicDirectoryLinkHereElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateSymbolicDirectoryLinkHere;

            case ArgumentCommands.CreateDirectoryJunctionHere: {
                if (args.Length < 2 || !Directory.Exists(Destination = args[1])) {
                    MakeLink.CreateDirectoryJunction(Elevated);
                    return true;
                }

                Source = GetFolder(Destination, "Select a folder to create a symbolic link to...");

                if (!Directory.Exists(Source)) {
                    return true;
                }

                string DestinationName = Path.GetFileName(Source);

                Directory.CreateDirectory(Destination);
                MakeLink.CreateDirectoryJunction(
                    Source: Source!,
                    DestinationDirectory: Destination,
                    DestinationFolderName: DestinationName,
                    Elevated: Elevated);
            } return true;
            case ArgumentCommands.CreateDirectoryJunctionHereElevated:
                Elevated = true;
                goto case ArgumentCommands.CreateDirectoryJunctionHere;
        }

        return false;
    }

    private static string? GetFile(string? StartingDirectory, string Title) {
        using OpenFileDialog ofd = new() {
            Filter = Const.AllFilesFilter,
            Multiselect = false,
            Title = Title,
        };

        if (Directory.Exists(StartingDirectory)) {
            ofd.InitialDirectory = StartingDirectory;
        }

        if (ofd.ShowDialog() != DialogResult.OK) {
            return null;
        }

        return ofd.FileName;
    }
    private static string? GetFolder(string? StartingDirectory, string Title) {
        using BetterFolderBrowserNS.BetterFolderBrowser fbd = new() {
            Multiselect = false,
            Title = Title,
        };

        if (Directory.Exists(StartingDirectory)) {
            fbd.InitialDirectory = StartingDirectory;
        }

        if (fbd.ShowDialog() != DialogResult.OK) {
            return null;
        }

        return fbd.SelectedPath;
    }
}
