namespace SymbolicLinker;
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
    /// Gets whether the program is running in debug mode.
    /// </summary>
    public static bool DebugMode { get; } = false;
    /// <summary>
    /// Gets or sets the exit code of the application.
    /// </summary>
    public static int ExitCode { get; set; } = 0;
    #endregion

    static Program() {
#if DEBUG
        DebugMode = true;
#endif
    }

    [STAThread]
    static int Main(string[] args) {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Tests.RunTests();

        if (!Arguments.HandleArguments(args)) {
            Application.Run(new frmMain());
        }

        return ExitCode;
    }
}
