#nullable enable
namespace SymbolicLinkerInstaller;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SymbolicLinker.Shared;
internal static class Installation {
    private const string ExecutableHash = "HashGoesHere";
    public static readonly string DestinationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "SymbolicLinker");
    public static readonly string DestinationFile = Path.Combine(DestinationDirectory, "SymbolicLinker.exe");

    public static async Task RunInstallation(Action? SavedFile, Action<bool>? WroteRegistry, Action? Finished) {
        FileStream fs;
        try {
            Directory.CreateDirectory(DestinationDirectory);
            fs = new(DestinationFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await FileCompress.DecompressToStreamAsync(Properties.Resources.payload, fs);
            SavedFile?.Invoke();
        }
        catch {
            MessageBox.Show("Unable to decompress the file to the installation path.");
            return;
        }

        try {
            if (!(await Hashing.CalculateSHA256Async(fs)).Equals(ExecutableHash, StringComparison.OrdinalIgnoreCase)
            && MessageBox.Show("The hash does not match, would you like to continue?", "SymbolicLinker Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No) {
                fs.Close();
                Directory.Delete(DestinationDirectory);
                return;
            }
            fs.Close();
        }
        catch {
            MessageBox.Show("Could not verify the hash.");
            return;
        }

        if (!await SystemRegistry.SetRegistryKeysAsync()){
            MessageBox.Show("Could not install registry keys, the application is basically gimped.", "SymbolicLinker Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WroteRegistry?.Invoke(false);
        }
        else {
            WroteRegistry?.Invoke(true);
        }

        Finished?.Invoke();
    }
    public static async Task RunUninstallation(Action? Finished) {
        await Task.Run(async () => {
            try {
                Directory.Delete(DestinationDirectory, true);
            }
            catch {
                MessageBox.Show("Could not delete the installation folder.", "SymbolicLinker Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!await SystemRegistry.RemoveRegistryKeysAsync()) {
                MessageBox.Show("Could not remove some or all of the registry keys.", "SymbolicLinker Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        });
        Finished?.Invoke();
    }
    public static async Task RunRepair(Action? Finished) {
        if (!Directory.Exists(DestinationDirectory) || !File.Exists(DestinationFile)) {
            await RunInstallation(null, null, null);
            return;
        }

        FileStream fs = new(DestinationFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        if (!(await Hashing.CalculateSHA256Async(fs)).Equals(ExecutableHash, StringComparison.OrdinalIgnoreCase)) {
            fs.Close();
            File.Delete(ExecutableHash);

            fs = new(DestinationFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await FileCompress.DecompressToStreamAsync(Properties.Resources.payload, fs);
            if (!(await Hashing.CalculateSHA256Async(fs)).Equals(ExecutableHash, StringComparison.OrdinalIgnoreCase)
            && MessageBox.Show("The hash does not match, would you like to continue?", "SymbolicLinker Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No) {
                fs.Close();
                Directory.Delete(DestinationDirectory);
                return;
            }
        }
        fs.Close();

        if (!await SystemRegistry.SetRegistryKeysAsync()) {
            MessageBox.Show("Could not install registry keys, the application is basically gimped.", "SymbolicLinker Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Finished?.Invoke();
    }
}
