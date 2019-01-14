using System.ComponentModel;
using System.Windows.Forms;
using Silver.UI;

namespace JoMapEditor
{
    partial class SpriteToolBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteToolBox));
            this.SpriteContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportAsCSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsJoEngineBinFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsRaw15BitsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsTGAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JoEngineCompatibilityCcheckBox = new System.Windows.Forms.CheckBox();
            this.toolBox1 = new Silver.UI.ToolBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SpriteContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SpriteContextMenuStrip
            // 
            this.SpriteContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsCSourceFileToolStripMenuItem,
            this.exportAsJoEngineBinFileToolStripMenuItem,
            this.exportAsRaw15BitsImageToolStripMenuItem,
            this.exportAsTGAToolStripMenuItem,
            this.detailsToolStripMenuItem,
            this.editToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem});
            this.SpriteContextMenuStrip.Name = "contextMenuStrip1";
            this.SpriteContextMenuStrip.Size = new System.Drawing.Size(217, 158);
            // 
            // exportAsCSourceFileToolStripMenuItem
            // 
            this.exportAsCSourceFileToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.c_lg;
            this.exportAsCSourceFileToolStripMenuItem.Name = "exportAsCSourceFileToolStripMenuItem";
            this.exportAsCSourceFileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exportAsCSourceFileToolStripMenuItem.Text = "Export as C source file";
            this.exportAsCSourceFileToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // exportAsJoEngineBinFileToolStripMenuItem
            // 
            this.exportAsJoEngineBinFileToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.script;
            this.exportAsJoEngineBinFileToolStripMenuItem.Name = "exportAsJoEngineBinFileToolStripMenuItem";
            this.exportAsJoEngineBinFileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exportAsJoEngineBinFileToolStripMenuItem.Text = "Export as Jo Engine Bin file";
            this.exportAsJoEngineBinFileToolStripMenuItem.Click += new System.EventHandler(this.exportAsJoEngineBinFileToolStripMenuItem_Click);
            // 
            // exportAsRaw15BitsImageToolStripMenuItem
            // 
            this.exportAsRaw15BitsImageToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.colorPicker;
            this.exportAsRaw15BitsImageToolStripMenuItem.Name = "exportAsRaw15BitsImageToolStripMenuItem";
            this.exportAsRaw15BitsImageToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exportAsRaw15BitsImageToolStripMenuItem.Text = "Export as raw 15 bits image";
            this.exportAsRaw15BitsImageToolStripMenuItem.Click += new System.EventHandler(this.exportAsRaw15BitsImageToolStripMenuItem_Click);
            // 
            // exportAsTGAToolStripMenuItem
            // 
            this.exportAsTGAToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.tga;
            this.exportAsTGAToolStripMenuItem.Name = "exportAsTGAToolStripMenuItem";
            this.exportAsTGAToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exportAsTGAToolStripMenuItem.Text = "Export as 24 bits TGA";
            this.exportAsTGAToolStripMenuItem.Click += new System.EventHandler(this.exportAsTGAToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.info;
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.openFolder;
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open containing folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // JoEngineCompatibilityCcheckBox
            // 
            this.JoEngineCompatibilityCcheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JoEngineCompatibilityCcheckBox.AutoSize = true;
            this.JoEngineCompatibilityCcheckBox.Location = new System.Drawing.Point(6, 394);
            this.JoEngineCompatibilityCcheckBox.Name = "JoEngineCompatibilityCcheckBox";
            this.JoEngineCompatibilityCcheckBox.Size = new System.Drawing.Size(212, 17);
            this.JoEngineCompatibilityCcheckBox.TabIndex = 3;
            this.JoEngineCompatibilityCcheckBox.Text = "Only show Jo Engine compatible sprites";
            this.JoEngineCompatibilityCcheckBox.UseVisualStyleBackColor = true;
            this.JoEngineCompatibilityCcheckBox.CheckedChanged += new System.EventHandler(this.JoEngineCompatibilityCcheckBox_CheckedChanged);
            // 
            // toolBox1
            // 
            this.toolBox1.AllowDrop = true;
            this.toolBox1.AllowSwappingByDragDrop = true;
            this.toolBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolBox1.ContextMenuStrip = this.SpriteContextMenuStrip;
            this.toolBox1.InitialScrollDelay = 500;
            this.toolBox1.ItemBackgroundColor = System.Drawing.Color.Empty;
            this.toolBox1.ItemBorderColor = System.Drawing.Color.Empty;
            this.toolBox1.ItemHeight = 20;
            this.toolBox1.ItemHoverColor = System.Drawing.Color.PowderBlue;
            this.toolBox1.ItemHoverTextColor = System.Drawing.Color.Black;
            this.toolBox1.ItemNormalColor = System.Drawing.Color.WhiteSmoke;
            this.toolBox1.ItemNormalTextColor = System.Drawing.Color.Black;
            this.toolBox1.ItemSelectedColor = System.Drawing.Color.SteelBlue;
            this.toolBox1.ItemSelectedTextColor = System.Drawing.Color.White;
            this.toolBox1.ItemSpacing = 2;
            this.toolBox1.LargeItemSize = new System.Drawing.Size(64, 64);
            this.toolBox1.LayoutDelay = 10;
            this.toolBox1.Location = new System.Drawing.Point(0, 0);
            this.toolBox1.Name = "toolBox1";
            this.toolBox1.ScrollDelay = 60;
            this.toolBox1.SelectAllTextWhileRenaming = true;
            this.toolBox1.SelectedTabIndex = -1;
            this.toolBox1.ShowOnlyOneItemPerRow = false;
            this.toolBox1.Size = new System.Drawing.Size(284, 390);
            this.toolBox1.SmallItemSize = new System.Drawing.Size(32, 32);
            this.toolBox1.TabHeight = 18;
            this.toolBox1.TabHoverTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabIndex = 0;
            this.toolBox1.TabNormalTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabSelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabSpacing = 1;
            this.toolBox1.UseItemColorInRename = false;
            this.toolBox1.ItemSelectionChanged += new Silver.UI.ItemSelectionChangedHandler(this.toolBox1_ItemSelectionChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Image = global::JoMapEditor.Properties.Resources.info;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(3, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "      Do right-click on sprite to show menu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpriteToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 433);
            this.Controls.Add(this.JoEngineCompatibilityCcheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpriteToolBox";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.TabText = "Available sprites";
            this.Text = "Sprites";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpriteToolBox_FormClosed);
            this.SpriteContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolBox toolBox1;
        private ContextMenuStrip SpriteContextMenuStrip;
        private ToolStripMenuItem exportAsCSourceFileToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem detailsToolStripMenuItem;
        private ToolStripMenuItem exportAsTGAToolStripMenuItem;
        private CheckBox JoEngineCompatibilityCcheckBox;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private ToolStripMenuItem exportAsJoEngineBinFileToolStripMenuItem;
        private ToolStripMenuItem exportAsRaw15BitsImageToolStripMenuItem;
    }
}