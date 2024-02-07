#nullable enable
namespace SymbolicLinker;
using System.Windows.Forms;
public partial class frmMain : Form {
    public frmMain() {
        InitializeComponent();
    }
    private void frmMain_Shown(object? sender, EventArgs e) {
        lbSeparator.Focus();
    }

    private void clCreateFileSymbolicLink_Click(object? sender, EventArgs e) {
        MakeLink.CreateFileSymbolicLink(false);
    }
    private void clCreateFileSymbolicLinkAdmin_Click(object? sender, EventArgs e) {
        MakeLink.CreateFileSymbolicLink(true);
    }

    private void clCreateFileHardLink_Click(object? sender, EventArgs e) {
        MakeLink.CreateFileHardLink(true);
    }
    private void clCreateFileHardLinkAdmin_Click(object? sender, EventArgs e) {
        MakeLink.CreateFileHardLink(true);
    }

    private void clCreateFolderSymbolicLink_Click(object? sender, EventArgs e) {
        MakeLink.CreateDirectorySymbolicLink(false);
    }
    private void clCreateFolderSymbolicLinkAdmin_Click(object? sender, EventArgs e) {
        MakeLink.CreateDirectorySymbolicLink(true);
    }

    private void clCreateFolderJunction_Click(object? sender, EventArgs e) {
        MakeLink.CreateDirectoryJunction(false);
    }
    private void clCreateFolderJunctionAdmin_Click(object? sender, EventArgs e) {
        MakeLink.CreateDirectoryJunction(true);
    }

    private void btnHelp_Click(object? sender, EventArgs e) {
        using frmHelp Help = new();
        Help.ShowDialog(this);
    }
    private void btnClose_Click(object? sender, EventArgs e) {
        this.Close();
    }
}
