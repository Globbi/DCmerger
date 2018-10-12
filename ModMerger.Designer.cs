namespace DC_Mod_Merger
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.listPassive = new System.Windows.Forms.ListBox();
            this.listActive = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolMenuMods = new System.Windows.Forms.ToolStripMenuItem();
            this.updateModListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerFixoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeModsPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pAKToolPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureWarning = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // listPassive
            // 
            this.listPassive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPassive.ItemHeight = 16;
            this.listPassive.Location = new System.Drawing.Point(0, 26);
            this.listPassive.Name = "listPassive";
            this.listPassive.Size = new System.Drawing.Size(200, 260);
            this.listPassive.TabIndex = 0;
            this.toolTip.SetToolTip(this.listPassive, "Available Mods");
            // 
            // listActive
            // 
            this.listActive.AllowDrop = true;
            this.listActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listActive.ItemHeight = 16;
            this.listActive.Location = new System.Drawing.Point(313, 26);
            this.listActive.Name = "listActive";
            this.listActive.Size = new System.Drawing.Size(200, 260);
            this.listActive.TabIndex = 1;
            this.toolTip.SetToolTip(this.listActive, "Mods to be merged\r\n(Drag and drop to change merge order)");
            this.listActive.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListUsed_DragDrop);
            this.listActive.DragOver += new System.Windows.Forms.DragEventHandler(this.ListUsed_DragOver);
            this.listActive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListUsed_MouseDown);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(206, 53);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(101, 24);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = ">>>";
            this.toolTip.SetToolTip(this.buttonAdd, "Add to collection");
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(206, 82);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(101, 24);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "<<<";
            this.toolTip.SetToolTip(this.buttonRemove, "Remove from collection");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuMods,
            this.settingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(513, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolMenuMods
            // 
            this.toolMenuMods.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateModListToolStripMenuItem});
            this.toolMenuMods.Name = "toolMenuMods";
            this.toolMenuMods.Size = new System.Drawing.Size(49, 20);
            this.toolMenuMods.Text = "Mods";
            // 
            // updateModListToolStripMenuItem
            // 
            this.updateModListToolStripMenuItem.Name = "updateModListToolStripMenuItem";
            this.updateModListToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.updateModListToolStripMenuItem.Text = "Refresh Mod Info";
            this.updateModListToolStripMenuItem.ToolTipText = "Manually refresh mod info";
            this.updateModListToolStripMenuItem.Click += new System.EventHandler(this.RefreshModListToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.headerFixoldToolStripMenuItem,
            this.changeModsPathToolStripMenuItem,
            this.pAKToolPathToolStripMenuItem,
            this.creditsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // headerFixoldToolStripMenuItem
            // 
            this.headerFixoldToolStripMenuItem.Name = "headerFixoldToolStripMenuItem";
            this.headerFixoldToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.headerFixoldToolStripMenuItem.Text = "Header Fix (old)";
            this.headerFixoldToolStripMenuItem.Click += new System.EventHandler(this.HeaderFixoldToolStripMenuItem_Click);
            // 
            // changeModsPathToolStripMenuItem
            // 
            this.changeModsPathToolStripMenuItem.Name = "changeModsPathToolStripMenuItem";
            this.changeModsPathToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.changeModsPathToolStripMenuItem.Text = "Change Library Path";
            this.changeModsPathToolStripMenuItem.Click += new System.EventHandler(this.LibraryPathToolStripMenuItem_Click);
            // 
            // pAKToolPathToolStripMenuItem
            // 
            this.pAKToolPathToolStripMenuItem.Name = "pAKToolPathToolStripMenuItem";
            this.pAKToolPathToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.pAKToolPathToolStripMenuItem.Text = "Change PAKTool Path";
            this.pAKToolPathToolStripMenuItem.Click += new System.EventHandler(this.PAKToolPathToolStripMenuItem_Click);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBuild.Location = new System.Drawing.Point(206, 242);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(101, 24);
            this.buttonBuild.TabIndex = 8;
            this.buttonBuild.Text = "Build";
            this.toolTip.SetToolTip(this.buttonBuild, "Build Merge Mod");
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 5000;
            this.toolTip.AutoPopDelay = 50000;
            this.toolTip.InitialDelay = 1000;
            this.toolTip.ReshowDelay = 100;
            // 
            // pictureWarning
            // 
            this.pictureWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureWarning.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureWarning.BackgroundImage")));
            this.pictureWarning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureWarning.Location = new System.Drawing.Point(206, 128);
            this.pictureWarning.Name = "pictureWarning";
            this.pictureWarning.Size = new System.Drawing.Size(101, 59);
            this.pictureWarning.TabIndex = 10;
            this.pictureWarning.TabStop = false;
            this.toolTip.SetToolTip(this.pictureWarning, resources.GetString("pictureWarning.ToolTip"));
            this.pictureWarning.Visible = false;
            this.pictureWarning.Click += new System.EventHandler(this.LabelWarning_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(206, 212);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(101, 24);
            this.buttonReset.TabIndex = 8;
            this.buttonReset.Text = "Reset";
            this.toolTip.SetToolTip(this.buttonReset, "Remove all from collection");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // MainWindow
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(513, 286);
            this.Controls.Add(this.pictureWarning);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listActive);
            this.Controls.Add(this.listPassive);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "DC Mod Merger (v1.0.4)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listPassive;
        private System.Windows.Forms.ListBox listActive;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolMenuMods;
        private System.Windows.Forms.ToolStripMenuItem updateModListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pAKToolPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeModsPathToolStripMenuItem;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureWarning;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem headerFixoldToolStripMenuItem;
    }
}

