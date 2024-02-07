namespace SymbolicLinker;

partial class frmHelp {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelp));
            this.lbHelpBody = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHelpBody
            // 
            this.lbHelpBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHelpBody.Location = new System.Drawing.Point(12, 9);
            this.lbHelpBody.Name = "lbHelpBody";
            this.lbHelpBody.Size = new System.Drawing.Size(580, 367);
            this.lbHelpBody.TabIndex = 0;
            this.lbHelpBody.Text = resources.GetString("lbHelpBody.Text");
            // 
            // frmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(604, 385);
            this.Controls.Add(this.lbHelpBody);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::SymbolicLinker.Properties.Resources.ProgramIcon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(620, 420);
            this.MinimumSize = new System.Drawing.Size(620, 420);
            this.Name = "frmHelp";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbHelpBody;
}