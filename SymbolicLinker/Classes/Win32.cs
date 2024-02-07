#nullable enable
namespace SymbolicLinker;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
internal static class Win32 {
    public static bool IsAdmin {
        get {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
    public static bool IsWindowsVistaOrLater {
        get {
            return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(6, 0, 6000);
        }
    }
    public static bool CanRunAsAdmin {
        get {
            return !IsWindowsVistaOrLater || IsAdmin;
        }
    }

    public static bool DirectoryNamesEqual(string a, string b) {
        string DirA = Path.GetFileName(a);
        string DirB = Path.GetFileName(b);
        return DirA.Equals(DirB, StringComparison.InvariantCultureIgnoreCase);
    }
    public static bool DestinationExists(string Destination) {
        return File.Exists(Destination) || Directory.Exists(Destination);
    }
    public static int MoveItem(string Source, string Destination, bool Elevated) {
        return Win32.RunCommand($"move \"{Source}\" \"{Destination}\"", Elevated);
    }
    public static int RunCommand(string Command, bool Elevated) {
        using Process CMD = new() {
            StartInfo = new() {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + Command,
                CreateNoWindow = true,
                Verb = Elevated && CanRunAsAdmin ? "runas" : string.Empty,
#if DEBUG
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
#endif
            },
#if DEBUG
            EnableRaisingEvents = true,
#endif
        };

#if DEBUG
        CMD.OutputDataReceived += (_, e) => {
            if (!e.Data.IsNullEmptyWhitespace()) {
                Debug.Print("stdout: " + e.Data);
            }
        };
        CMD.ErrorDataReceived += (_, e) => {
            if (!e.Data.IsNullEmptyWhitespace()) {
                Debug.Print("stderr: " + e.Data);
            }
        };
#endif

        Debug.Print("Starting command...");
        if (CMD.Start()) {
#if DEBUG
            CMD.BeginOutputReadLine();
            CMD.BeginErrorReadLine();
#endif
            CMD.WaitForExit();
            return CMD.ExitCode;
        }

        throw new Exception("Could not start the command prompt to run the command.");
    }

    public static void ErrorMessageBox(string message) {
        System.Windows.Forms.MessageBox.Show(message,
            Const.DialogProgramName,
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Error);
    }
}
