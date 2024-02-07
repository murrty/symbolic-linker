namespace SymbolicLinker;

partial class frmMain {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent() {
            this.lbSeparator = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.clCreateFolderJunctionAdmin = new SymbolicLinker.CommandLink();
            this.clCreateFolderJunction = new SymbolicLinker.CommandLink();
            this.clCreateFolderSymbolicLinkAdmin = new SymbolicLinker.CommandLink();
            this.clCreateFolderSymbolicLink = new SymbolicLinker.CommandLink();
            this.clCreateFileHardLinkAdmin = new SymbolicLinker.CommandLink();
            this.clCreateFileSymbolicLinkAdmin = new SymbolicLinker.CommandLink();
            this.clCreateFileHardLink = new SymbolicLinker.CommandLink();
            this.clCreateFileSymbolicLink = new SymbolicLinker.CommandLink();
            this.lbFooter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbSeparator
            // 
            this.lbSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparator.Location = new System.Drawing.Point(12, 145);
            this.lbSeparator.Name = "lbSeparator";
            this.lbSeparator.Size = new System.Drawing.Size(537, 2);
            this.lbSeparator.TabIndex = 4;
            this.lbSeparator.Text = "goodbye, world";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(487, 316);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(406, 316);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 9;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // clCreateFolderJunctionAdmin
            // 
            this.clCreateFolderJunctionAdmin.ButtonIcon = SymbolicLinker.ButtonIcon.Shield;
            this.clCreateFolderJunctionAdmin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFolderJunctionAdmin.Location = new System.Drawing.Point(250, 227);
            this.clCreateFolderJunctionAdmin.Name = "clCreateFolderJunctionAdmin";
            this.clCreateFolderJunctionAdmin.Note = "Creates a symbolic link to a folder as administrator";
            this.clCreateFolderJunctionAdmin.Size = new System.Drawing.Size(312, 60);
            this.clCreateFolderJunctionAdmin.TabIndex = 8;
            this.clCreateFolderJunctionAdmin.Text = "Create folder junction (admin)";
            this.clCreateFolderJunctionAdmin.UseVisualStyleBackColor = true;
            this.clCreateFolderJunctionAdmin.Click += new System.EventHandler(this.clCreateFolderJunctionAdmin_Click);
            // 
            // clCreateFolderJunction
            // 
            this.clCreateFolderJunction.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFolderJunction.Location = new System.Drawing.Point(12, 227);
            this.clCreateFolderJunction.Name = "clCreateFolderJunction";
            this.clCreateFolderJunction.Note = "Creates a symbolic link to a folder";
            this.clCreateFolderJunction.Size = new System.Drawing.Size(222, 60);
            this.clCreateFolderJunction.TabIndex = 7;
            this.clCreateFolderJunction.Text = "Create folder junction";
            this.clCreateFolderJunction.UseVisualStyleBackColor = true;
            this.clCreateFolderJunction.Click += new System.EventHandler(this.clCreateFolderJunction_Click);
            // 
            // clCreateFolderSymbolicLinkAdmin
            // 
            this.clCreateFolderSymbolicLinkAdmin.ButtonIcon = SymbolicLinker.ButtonIcon.Shield;
            this.clCreateFolderSymbolicLinkAdmin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFolderSymbolicLinkAdmin.Location = new System.Drawing.Point(251, 161);
            this.clCreateFolderSymbolicLinkAdmin.Name = "clCreateFolderSymbolicLinkAdmin";
            this.clCreateFolderSymbolicLinkAdmin.Note = "Creates a symbolic link to a folder as administrator";
            this.clCreateFolderSymbolicLinkAdmin.Size = new System.Drawing.Size(312, 60);
            this.clCreateFolderSymbolicLinkAdmin.TabIndex = 6;
            this.clCreateFolderSymbolicLinkAdmin.Text = "Create folder symbolic link (admin)";
            this.clCreateFolderSymbolicLinkAdmin.UseVisualStyleBackColor = true;
            this.clCreateFolderSymbolicLinkAdmin.Click += new System.EventHandler(this.clCreateFolderSymbolicLinkAdmin_Click);
            // 
            // clCreateFolderSymbolicLink
            // 
            this.clCreateFolderSymbolicLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFolderSymbolicLink.Location = new System.Drawing.Point(12, 161);
            this.clCreateFolderSymbolicLink.Name = "clCreateFolderSymbolicLink";
            this.clCreateFolderSymbolicLink.Note = "Creates a symbolic link to a folder";
            this.clCreateFolderSymbolicLink.Size = new System.Drawing.Size(233, 60);
            this.clCreateFolderSymbolicLink.TabIndex = 5;
            this.clCreateFolderSymbolicLink.Text = "Create folder symbolic link";
            this.clCreateFolderSymbolicLink.UseVisualStyleBackColor = true;
            this.clCreateFolderSymbolicLink.Click += new System.EventHandler(this.clCreateFolderSymbolicLink_Click);
            // 
            // clCreateFileHardLinkAdmin
            // 
            this.clCreateFileHardLinkAdmin.ButtonIcon = SymbolicLinker.ButtonIcon.Shield;
            this.clCreateFileHardLinkAdmin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFileHardLinkAdmin.Location = new System.Drawing.Point(251, 78);
            this.clCreateFileHardLinkAdmin.Name = "clCreateFileHardLinkAdmin";
            this.clCreateFileHardLinkAdmin.Note = "Creates a hard link to a file as administrator";
            this.clCreateFileHardLinkAdmin.Size = new System.Drawing.Size(274, 60);
            this.clCreateFileHardLinkAdmin.TabIndex = 3;
            this.clCreateFileHardLinkAdmin.Text = "Create file hard link (admin)";
            this.clCreateFileHardLinkAdmin.UseVisualStyleBackColor = true;
            this.clCreateFileHardLinkAdmin.Click += new System.EventHandler(this.clCreateFileHardLinkAdmin_Click);
            // 
            // clCreateFileSymbolicLinkAdmin
            // 
            this.clCreateFileSymbolicLinkAdmin.ButtonIcon = SymbolicLinker.ButtonIcon.Shield;
            this.clCreateFileSymbolicLinkAdmin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFileSymbolicLinkAdmin.Location = new System.Drawing.Point(251, 12);
            this.clCreateFileSymbolicLinkAdmin.Name = "clCreateFileSymbolicLinkAdmin";
            this.clCreateFileSymbolicLinkAdmin.Note = "Creates a symbolic link to a file as administrator";
            this.clCreateFileSymbolicLinkAdmin.Size = new System.Drawing.Size(297, 60);
            this.clCreateFileSymbolicLinkAdmin.TabIndex = 1;
            this.clCreateFileSymbolicLinkAdmin.Text = "Create file symbolic link (admin)";
            this.clCreateFileSymbolicLinkAdmin.UseVisualStyleBackColor = true;
            this.clCreateFileSymbolicLinkAdmin.Click += new System.EventHandler(this.clCreateFileSymbolicLinkAdmin_Click);
            // 
            // clCreateFileHardLink
            // 
            this.clCreateFileHardLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFileHardLink.Location = new System.Drawing.Point(12, 78);
            this.clCreateFileHardLink.Name = "clCreateFileHardLink";
            this.clCreateFileHardLink.Note = "Creates a hard link to a file";
            this.clCreateFileHardLink.Size = new System.Drawing.Size(184, 60);
            this.clCreateFileHardLink.TabIndex = 2;
            this.clCreateFileHardLink.Text = "Create file hard link";
            this.clCreateFileHardLink.UseVisualStyleBackColor = true;
            this.clCreateFileHardLink.Click += new System.EventHandler(this.clCreateFileHardLink_Click);
            // 
            // clCreateFileSymbolicLink
            // 
            this.clCreateFileSymbolicLink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clCreateFileSymbolicLink.Location = new System.Drawing.Point(12, 12);
            this.clCreateFileSymbolicLink.Name = "clCreateFileSymbolicLink";
            this.clCreateFileSymbolicLink.Note = "Creates a symbolic link to a file";
            this.clCreateFileSymbolicLink.Size = new System.Drawing.Size(213, 60);
            this.clCreateFileSymbolicLink.TabIndex = 0;
            this.clCreateFileSymbolicLink.Text = "Create file symbolic link";
            this.clCreateFileSymbolicLink.UseVisualStyleBackColor = true;
            this.clCreateFileSymbolicLink.Click += new System.EventHandler(this.clCreateFileSymbolicLink_Click);
            // 
            // lbFooter
            // 
            this.lbFooter.AutoSize = true;
            this.lbFooter.Enabled = false;
            this.lbFooter.Location = new System.Drawing.Point(12, 326);
            this.lbFooter.Name = "lbFooter";
            this.lbFooter.Size = new System.Drawing.Size(106, 13);
            this.lbFooter.TabIndex = 11;
            this.lbFooter.Text = "SymbolicLinker v1.0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 351);
            this.Controls.Add(this.lbFooter);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.clCreateFolderJunctionAdmin);
            this.Controls.Add(this.clCreateFolderJunction);
            this.Controls.Add(this.clCreateFolderSymbolicLinkAdmin);
            this.Controls.Add(this.clCreateFolderSymbolicLink);
            this.Controls.Add(this.lbSeparator);
            this.Controls.Add(this.clCreateFileHardLinkAdmin);
            this.Controls.Add(this.clCreateFileSymbolicLinkAdmin);
            this.Controls.Add(this.clCreateFileHardLink);
            this.Controls.Add(this.clCreateFileSymbolicLink);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::SymbolicLinker.Properties.Resources.ProgramIcon;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SymbolicLinker";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private CommandLink clCreateFileSymbolicLink;
    private CommandLink clCreateFileHardLink;
    private CommandLink clCreateFileSymbolicLinkAdmin;
    private CommandLink clCreateFileHardLinkAdmin;
    private System.Windows.Forms.Label lbSeparator;
    private CommandLink clCreateFolderSymbolicLink;
    private CommandLink clCreateFolderSymbolicLinkAdmin;
    private CommandLink clCreateFolderJunctionAdmin;
    private CommandLink clCreateFolderJunction;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.Label lbFooter;
}

