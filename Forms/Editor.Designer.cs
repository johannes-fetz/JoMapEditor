using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace JoMapEditor
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin2 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient4 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient8 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient9 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient5 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient10 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient11 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient12 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient6 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient13 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient14 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x0_5ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x48ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x24ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.x120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.availableSpritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSpritesFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAvailableSpritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeTransparentColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportMapAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapAsTGAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapAsCSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapAsBINImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapAsRaw15BitsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colladaToJoEngine3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joMapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.InfoToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.alignmentToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.dToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.file;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.file;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.openFolder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.exitApplication;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x0_5ToolStripMenuItem2,
            this.defaultToolStripMenuItem,
            this.x2ToolStripMenuItem,
            this.x4ToolStripMenuItem1});
            this.zoomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomToolStripMenuItem.Image")));
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // x0_5ToolStripMenuItem2
            // 
            this.x0_5ToolStripMenuItem2.Name = "x0_5ToolStripMenuItem2";
            this.x0_5ToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.x0_5ToolStripMenuItem2.Text = "0.5 x";
            this.x0_5ToolStripMenuItem2.Click += new System.EventHandler(this.x0_5ToolStripMenuItem2_Click);
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Checked = true;
            this.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.defaultToolStripMenuItem.Text = "1 x (Default)";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // x2ToolStripMenuItem
            // 
            this.x2ToolStripMenuItem.Name = "x2ToolStripMenuItem";
            this.x2ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.x2ToolStripMenuItem.Text = "2 x";
            this.x2ToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // x4ToolStripMenuItem1
            // 
            this.x4ToolStripMenuItem1.Name = "x4ToolStripMenuItem1";
            this.x4ToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.x4ToolStripMenuItem1.Text = "4 x";
            this.x4ToolStripMenuItem1.Click += new System.EventHandler(this.x4ToolStripMenuItem1_Click);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.x48ToolStripMenuItem,
            this.x32ToolStripMenuItem,
            this.x24ToolStripMenuItem,
            this.x16ToolStripMenuItem,
            this.x8ToolStripMenuItem,
            this.toolStripSeparator1,
            this.x120ToolStripMenuItem});
            this.alignmentToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.grid;
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // x48ToolStripMenuItem
            // 
            this.x48ToolStripMenuItem.Name = "x48ToolStripMenuItem";
            this.x48ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x48ToolStripMenuItem.Text = "48 x 48";
            this.x48ToolStripMenuItem.Click += new System.EventHandler(this.x48ToolStripMenuItem_Click);
            // 
            // x32ToolStripMenuItem
            // 
            this.x32ToolStripMenuItem.Name = "x32ToolStripMenuItem";
            this.x32ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x32ToolStripMenuItem.Text = "32 x 32";
            this.x32ToolStripMenuItem.Click += new System.EventHandler(this.x32ToolStripMenuItem_Click);
            // 
            // x24ToolStripMenuItem
            // 
            this.x24ToolStripMenuItem.Name = "x24ToolStripMenuItem";
            this.x24ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x24ToolStripMenuItem.Text = "24 x 24";
            this.x24ToolStripMenuItem.Click += new System.EventHandler(this.x24ToolStripMenuItem_Click);
            // 
            // x16ToolStripMenuItem
            // 
            this.x16ToolStripMenuItem.Name = "x16ToolStripMenuItem";
            this.x16ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x16ToolStripMenuItem.Text = "16 x 16";
            this.x16ToolStripMenuItem.Click += new System.EventHandler(this.x16ToolStripMenuItem_Click);
            // 
            // x8ToolStripMenuItem
            // 
            this.x8ToolStripMenuItem.Checked = true;
            this.x8ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x8ToolStripMenuItem.Name = "x8ToolStripMenuItem";
            this.x8ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x8ToolStripMenuItem.Text = "8 x 8";
            this.x8ToolStripMenuItem.Click += new System.EventHandler(this.x8ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
            // 
            // x120ToolStripMenuItem
            // 
            this.x120ToolStripMenuItem.Name = "x120ToolStripMenuItem";
            this.x120ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.x120ToolStripMenuItem.Text = "160 x 120";
            this.x120ToolStripMenuItem.Click += new System.EventHandler(this.x120ToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.availableSpritesToolStripMenuItem,
            this.tilePropertiesToolStripMenuItem});
            this.viewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripMenuItem.Image")));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // availableSpritesToolStripMenuItem
            // 
            this.availableSpritesToolStripMenuItem.Enabled = false;
            this.availableSpritesToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.gif;
            this.availableSpritesToolStripMenuItem.Name = "availableSpritesToolStripMenuItem";
            this.availableSpritesToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.availableSpritesToolStripMenuItem.Text = "Available sprites";
            this.availableSpritesToolStripMenuItem.Click += new System.EventHandler(this.availableSpritesToolStripMenuItem_Click);
            // 
            // tilePropertiesToolStripMenuItem
            // 
            this.tilePropertiesToolStripMenuItem.Enabled = false;
            this.tilePropertiesToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.property;
            this.tilePropertiesToolStripMenuItem.Name = "tilePropertiesToolStripMenuItem";
            this.tilePropertiesToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tilePropertiesToolStripMenuItem.Text = "Tile properties";
            this.tilePropertiesToolStripMenuItem.Click += new System.EventHandler(this.tilePropertiesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSpritesFolderToolStripMenuItem,
            this.refreshAvailableSpritesToolStripMenuItem,
            this.changeTransparentColorToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportMapAsPNGToolStripMenuItem,
            this.exportMapAsTGAToolStripMenuItem,
            this.exportMapAsCSourceToolStripMenuItem,
            this.exportMapAsBINImageToolStripMenuItem,
            this.exportMapAsRaw15BitsImageToolStripMenuItem,
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem,
            this.toolStripSeparator4});
            this.toolsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripMenuItem.Image")));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.toolsToolStripMenuItem_DropDownOpening);
            // 
            // changeSpritesFolderToolStripMenuItem
            // 
            this.changeSpritesFolderToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.imageFolder;
            this.changeSpritesFolderToolStripMenuItem.Name = "changeSpritesFolderToolStripMenuItem";
            this.changeSpritesFolderToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.changeSpritesFolderToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.changeSpritesFolderToolStripMenuItem.Text = "Change sprites folder";
            this.changeSpritesFolderToolStripMenuItem.Click += new System.EventHandler(this.changeSpritesFolderToolStripMenuItem_Click);
            // 
            // refreshAvailableSpritesToolStripMenuItem
            // 
            this.refreshAvailableSpritesToolStripMenuItem.Enabled = false;
            this.refreshAvailableSpritesToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.refresh;
            this.refreshAvailableSpritesToolStripMenuItem.Name = "refreshAvailableSpritesToolStripMenuItem";
            this.refreshAvailableSpritesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshAvailableSpritesToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.refreshAvailableSpritesToolStripMenuItem.Text = "Refresh available sprites";
            this.refreshAvailableSpritesToolStripMenuItem.Click += new System.EventHandler(this.refreshAvailableSpritesToolStripMenuItem_Click);
            // 
            // changeTransparentColorToolStripMenuItem
            // 
            this.changeTransparentColorToolStripMenuItem.Name = "changeTransparentColorToolStripMenuItem";
            this.changeTransparentColorToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.changeTransparentColorToolStripMenuItem.Text = "Change transparent color";
            this.changeTransparentColorToolStripMenuItem.Click += new System.EventHandler(this.changeTransparentColorToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(265, 6);
            // 
            // exportMapAsPNGToolStripMenuItem
            // 
            this.exportMapAsPNGToolStripMenuItem.Enabled = false;
            this.exportMapAsPNGToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.gif;
            this.exportMapAsPNGToolStripMenuItem.Name = "exportMapAsPNGToolStripMenuItem";
            this.exportMapAsPNGToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsPNGToolStripMenuItem.Text = "Export map as PNG";
            this.exportMapAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsPNGToolStripMenuItem_Click);
            // 
            // exportMapAsTGAToolStripMenuItem
            // 
            this.exportMapAsTGAToolStripMenuItem.Enabled = false;
            this.exportMapAsTGAToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.tga;
            this.exportMapAsTGAToolStripMenuItem.Name = "exportMapAsTGAToolStripMenuItem";
            this.exportMapAsTGAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportMapAsTGAToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsTGAToolStripMenuItem.Text = "Export map as 24 bits TGA";
            this.exportMapAsTGAToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsTGAToolStripMenuItem_Click);
            // 
            // exportMapAsCSourceToolStripMenuItem
            // 
            this.exportMapAsCSourceToolStripMenuItem.Enabled = false;
            this.exportMapAsCSourceToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.c_lg;
            this.exportMapAsCSourceToolStripMenuItem.Name = "exportMapAsCSourceToolStripMenuItem";
            this.exportMapAsCSourceToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsCSourceToolStripMenuItem.Text = "Export map as C source file";
            this.exportMapAsCSourceToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsCSourceToolStripMenuItem_Click);
            // 
            // exportMapAsBINImageToolStripMenuItem
            // 
            this.exportMapAsBINImageToolStripMenuItem.Enabled = false;
            this.exportMapAsBINImageToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.script;
            this.exportMapAsBINImageToolStripMenuItem.Name = "exportMapAsBINImageToolStripMenuItem";
            this.exportMapAsBINImageToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsBINImageToolStripMenuItem.Text = "Export map as Jo Engine Bin file";
            this.exportMapAsBINImageToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsBINImageToolStripMenuItem_Click);
            // 
            // exportMapAsRaw15BitsImageToolStripMenuItem
            // 
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Enabled = false;
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.colorPicker;
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Name = "exportMapAsRaw15BitsImageToolStripMenuItem";
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Text = "Export map as raw 15 bits image";
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsRaw15BitsImageToolStripMenuItem_Click);
            // 
            // exportMapAsJoEngineTilesetStructToolStripMenuItem
            // 
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Enabled = false;
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.tileset;
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Name = "exportMapAsJoEngineTilesetStructToolStripMenuItem";
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Text = "Export map as Jo Engine tileset struct";
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Click += new System.EventHandler(this.exportMapAsJoEngineTilesetStructToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(265, 6);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colladaToJoEngine3DToolStripMenuItem,
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem});
            this.dToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dToolStripMenuItem.Image")));
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.dToolStripMenuItem.Text = "3D";
            // 
            // colladaToJoEngine3DToolStripMenuItem
            // 
            this.colladaToJoEngine3DToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.cube_tool_icon;
            this.colladaToJoEngine3DToolStripMenuItem.Name = "colladaToJoEngine3DToolStripMenuItem";
            this.colladaToJoEngine3DToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.colladaToJoEngine3DToolStripMenuItem.Text = "Collada (.dae) to Jo Engine source file";
            this.colladaToJoEngine3DToolStripMenuItem.Click += new System.EventHandler(this.colladaToJoEngine3DToolStripMenuItem_Click);
            // 
            // wavefrontobjToJoEngineSourceFileToolStripMenuItem
            // 
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.cube_tool_icon;
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem.Name = "wavefrontobjToJoEngineSourceFileToolStripMenuItem";
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem.Text = "Wavefront (.obj) to Jo Engine source file";
            this.wavefrontobjToJoEngineSourceFileToolStripMenuItem.Click += new System.EventHandler(this.wavefrontobjToJoEngineSourceFileToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joMapEditorToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.question_blue;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // joMapEditorToolStripMenuItem
            // 
            this.joMapEditorToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.sega_saturn_32;
            this.joMapEditorToolStripMenuItem.Name = "joMapEditorToolStripMenuItem";
            this.joMapEditorToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.joMapEditorToolStripMenuItem.Text = "Jo Map Editor";
            this.joMapEditorToolStripMenuItem.Click += new System.EventHandler(this.joMapEditorToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.question_blue;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // InfoToolStripStatusLabel
            // 
            this.InfoToolStripStatusLabel.IsLink = true;
            this.InfoToolStripStatusLabel.Name = "InfoToolStripStatusLabel";
            this.InfoToolStripStatusLabel.Size = new System.Drawing.Size(52, 17);
            this.InfoToolStripStatusLabel.Text = "File path";
            this.InfoToolStripStatusLabel.Click += new System.EventHandler(this.InfoToolStripStatusLabel_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.AllowDrop = true;
            this.dockPanel1.BackColor = System.Drawing.Color.White;
            this.dockPanel1.BackgroundImage = global::JoMapEditor.Properties.Resources.SegaSaturnLogo;
            this.dockPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.Color.Transparent;
            this.dockPanel1.Location = new System.Drawing.Point(0, 24);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(984, 516);
            dockPanelGradient4.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient4.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin2.DockStripGradient = dockPanelGradient4;
            tabGradient8.EndColor = System.Drawing.SystemColors.Control;
            tabGradient8.StartColor = System.Drawing.SystemColors.Control;
            tabGradient8.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin2.TabGradient = tabGradient8;
            dockPanelSkin2.AutoHideStripSkin = autoHideStripSkin2;
            tabGradient9.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient9.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.ActiveTabGradient = tabGradient9;
            dockPanelGradient5.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient5.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient2.DockStripGradient = dockPanelGradient5;
            tabGradient10.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient10.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient2.InactiveTabGradient = tabGradient10;
            dockPaneStripSkin2.DocumentGradient = dockPaneStripGradient2;
            tabGradient11.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient11.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient11.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient11.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient2.ActiveCaptionGradient = tabGradient11;
            tabGradient12.EndColor = System.Drawing.SystemColors.Control;
            tabGradient12.StartColor = System.Drawing.SystemColors.Control;
            tabGradient12.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient2.ActiveTabGradient = tabGradient12;
            dockPanelGradient6.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient6.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient2.DockStripGradient = dockPanelGradient6;
            tabGradient13.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient13.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient13.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient13.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient2.InactiveCaptionGradient = tabGradient13;
            tabGradient14.EndColor = System.Drawing.Color.Transparent;
            tabGradient14.StartColor = System.Drawing.Color.Transparent;
            tabGradient14.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient2.InactiveTabGradient = tabGradient14;
            dockPaneStripSkin2.ToolWindowGradient = dockPaneStripToolWindowGradient2;
            dockPanelSkin2.DockPaneStripSkin = dockPaneStripSkin2;
            this.dockPanel1.Skin = dockPanelSkin2;
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.ActiveDocumentChanged += new System.EventHandler(this.dockPanel1_ActiveDocumentChanged);
            this.dockPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dockPanel1_DragDrop);
            this.dockPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dockPanel1_DragEnter);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jo Map Editor v6.2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem x2ToolStripMenuItem;
        private ToolStripMenuItem x4ToolStripMenuItem1;
        private ToolStripMenuItem defaultToolStripMenuItem;
        private ToolStripMenuItem x0_5ToolStripMenuItem2;
        private ToolStripMenuItem alignmentToolStripMenuItem;
        private ToolStripMenuItem noneToolStripMenuItem;
        private ToolStripMenuItem x48ToolStripMenuItem;
        private ToolStripMenuItem x24ToolStripMenuItem;
        private ToolStripMenuItem x16ToolStripMenuItem;
        private ToolStripMenuItem x32ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem x120ToolStripMenuItem;
        private ToolStripMenuItem x8ToolStripMenuItem;
        private DockPanel dockPanel1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem availableSpritesToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem joMapEditorToolStripMenuItem;
        private ToolStripStatusLabel InfoToolStripStatusLabel;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem changeSpritesFolderToolStripMenuItem;
        private ToolStripMenuItem exportMapAsPNGToolStripMenuItem;
        private ToolStripMenuItem refreshAvailableSpritesToolStripMenuItem;
        private ToolStripMenuItem exportMapAsCSourceToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem exportMapAsTGAToolStripMenuItem;
        private ToolStripMenuItem exportMapAsBINImageToolStripMenuItem;
        private ToolStripMenuItem exportMapAsRaw15BitsImageToolStripMenuItem;
        private ToolStripMenuItem changeTransparentColorToolStripMenuItem;
        private ToolStripMenuItem tilePropertiesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem exportMapAsJoEngineTilesetStructToolStripMenuItem;
        private ToolStripMenuItem dToolStripMenuItem;
        private ToolStripMenuItem colladaToJoEngine3DToolStripMenuItem;
        private ToolStripMenuItem wavefrontobjToJoEngineSourceFileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
    }
}

