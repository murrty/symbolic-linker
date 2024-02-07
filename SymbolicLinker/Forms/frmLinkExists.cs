#nullable enable
namespace SymbolicLinker;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
internal partial class frmLinkExists : Form {
    public ExistingAction Action { get; private set; }

    public frmLinkExists() {
        InitializeComponent();
        cbOptions.DataSource = Enum.GetValues(typeof(ExistingAction));
        cbOptions.SelectedIndex = 0;
    }

    private bool HasAction([NotNullWhen(true)] out ExistingAction? existingAction) {
        if (cbOptions.SelectedValue is not ExistingAction act) {
            MessageBox.Show($"The {nameof(cbOptions)} value is not a proper {nameof(ExistingAction)} value. No action will be able to be done.", Const.DialogProgramName);
            this.Close();
            existingAction = null;
            return false;
        }
        existingAction = act;
        return true;
    }

    private void cbOptions_SelectedIndexChanged(object? sender, EventArgs e) {
        if (!HasAction(out var act)) {
            return;
        }

        switch (act.Value) {
            case ExistingAction.Backup: {
                lbHint.Text = "The existing file or folder will be saved as a backup.";
            } break;
            case ExistingAction.Overwrite: {
                lbHint.Text = "The existing file or folder will be overwritten. If it's not a link, it will be deleted.";
            } break;
            case ExistingAction.ChangeExistingName: {
                lbHint.Text = "You can specify a new name for the existing file or folder. Invalid names will make a backup instead.";
            } break;
            case ExistingAction.NewName: {
                lbHint.Text = "You can specify a new name for the link.";
            } break;
            default: {
                lbHint.Text = "No action will be taken, no link will be made.";
            } break;
        }
    }
    private void btnProceed_Click(object? sender, EventArgs e) {
        if (!HasAction(out var act)) {
            this.DialogResult = DialogResult.Cancel;
            return;
        }
        Action = act.Value;
        this.DialogResult = DialogResult.OK;
    }
    private void btnCancel_Click(object? sender, EventArgs e) {
        this.DialogResult = DialogResult.Cancel;
    }
}
