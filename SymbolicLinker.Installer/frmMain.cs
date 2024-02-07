namespace SymbolicLinkerInstaller;
using System.Threading.Tasks;
using System.Windows.Forms;
public partial class frmMain : Form {
    readonly bool AlreadyInstalled;

    public frmMain() {
        InitializeComponent();
        btnRepair.Enabled = btnRepair.Visible = AlreadyInstalled = System.IO.File.Exists(Installation.DestinationFile);
        if (AlreadyInstalled) {
            btnInstall.Text = "Uninstall";
        }
    }

    private void btnRepair_Click(object sender, EventArgs e) {
        btnInstall.Enabled = false;
        btnRepair.Enabled = false;
        _ = Installation.RunRepair(FinishedRepair);
    }
    private void btnInstall_Click(object sender, EventArgs e) {
        btnInstall.Enabled = false;
        btnRepair.Enabled = false;
        Task RunningTask;
        if (AlreadyInstalled) {
            RunningTask = Installation.RunUninstallation(UninstallationFinished);
            return;
        }

        RunningTask = Installation.RunInstallation(
            () => chkFile.Checked = true,
            (x) => chkRegistry.CheckState = (x ? CheckState.Checked : CheckState.Unchecked),
            FinishedInstallation);
    }
    private void btnCancel_Click(object sender, EventArgs e) {
        this.Close();
    }
    private void FinishedInstallation() {
        lbStatus.Text = "Finished installation.";
        btnCancel.Text = "Close";
    }
    private void FinishedRepair() {
        lbStatus.Text = "Finished repairs.";
        btnCancel.Text = "Close";
    }
    private void UninstallationFinished() {
        lbStatus.Text = "Uninstallation finished.";
        btnCancel.Text = "Close";
    }
}
