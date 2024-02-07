namespace SymbolicLinkerInstaller;
using System.Windows.Forms;
internal static class Program {
    #region Version Information
    /// <summary>
    /// The current version of the program.
    /// </summary>
    public const decimal CurrentVersion = 1.0m;
    /// <summary>
    /// Whether the program is a beta version.
    /// </summary>
    public const bool IsBetaVersion = false;
    /// <summary>
    /// The version of the current beta program.
    /// </summary>
    public const string BetaVersion = "1.0-pre0";
    /// <summary>
    /// The string to the Github page.
    /// </summary>
    public const string GithubPage = "https://github.com/murrty/SymbolicLinker";
    #endregion

    #region Runtime Fields
    /// <summary>
    /// Gets or sets the exit code of the application.
    /// </summary>
    public static int ExitCode { get; set; } = 0;
    #endregion

    [STAThread]
    static int Main(string[] args) {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
        using OpenFileDialog ofd = new();
        ofd.Filter = "All Files (*.*)|*.*";
        ofd.Multiselect = false;
        ofd.Title = "Select file to compress...";

        if (ofd.ShowDialog() == DialogResult.OK) {
            FileCompress.CompressToFile(ofd.FileName, System.IO.Path.GetDirectoryName(ofd.FileName) + "\\payload.bin");
        }
#else
        Application.Run(new frmMain());
#endif
        return ExitCode;
    }
}
