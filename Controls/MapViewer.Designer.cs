using System.ComponentModel;
using System.Windows.Forms;

namespace JoMapEditor
{
    partial class MapViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewer));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.positionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.explodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsTGAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsRaw15BitsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsJoEngineTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsBINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelGrid = new JoMapEditor.MapViewer.EditorControl();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positionToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // positionToolStripStatusLabel
            // 
            this.positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
            this.positionToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.explodeToolStripMenuItem,
            this.exportAsCSourceFileToolStripMenuItem,
            this.exportAsTGAToolStripMenuItem,
            this.exportAsPNGToolStripMenuItem,
            this.exportAsRaw15BitsImageToolStripMenuItem,
            this.exportAsJoEngineTilesetToolStripMenuItem,
            this.exportAsBINToolStripMenuItem,
            this.deleteSelectionToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.cutSelectionToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(269, 334);
            this.contextMenuStrip1.Text = "Export as Jo Engine Bin file";
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // explodeToolStripMenuItem
            // 
            this.explodeToolStripMenuItem.Enabled = false;
            this.explodeToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.explode;
            this.explodeToolStripMenuItem.Name = "explodeToolStripMenuItem";
            this.explodeToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.explodeToolStripMenuItem.Text = "Explode image in tile";
            this.explodeToolStripMenuItem.Click += new System.EventHandler(this.explodeToolStripMenuItem_Click);
            // 
            // exportAsCSourceFileToolStripMenuItem
            // 
            this.exportAsCSourceFileToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.c_lg;
            this.exportAsCSourceFileToolStripMenuItem.Name = "exportAsCSourceFileToolStripMenuItem";
            this.exportAsCSourceFileToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsCSourceFileToolStripMenuItem.Text = "Export as C source file";
            this.exportAsCSourceFileToolStripMenuItem.Click += new System.EventHandler(this.exportAsCSourceFileToolStripMenuItem_Click);
            // 
            // exportAsTGAToolStripMenuItem
            // 
            this.exportAsTGAToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.tga;
            this.exportAsTGAToolStripMenuItem.Name = "exportAsTGAToolStripMenuItem";
            this.exportAsTGAToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsTGAToolStripMenuItem.Text = "Export as 24 bits TGA";
            this.exportAsTGAToolStripMenuItem.Click += new System.EventHandler(this.exportAsTGAToolStripMenuItem_Click);
            // 
            // exportAsPNGToolStripMenuItem
            // 
            this.exportAsPNGToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.gif;
            this.exportAsPNGToolStripMenuItem.Name = "exportAsPNGToolStripMenuItem";
            this.exportAsPNGToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsPNGToolStripMenuItem.Text = "Export as PNG";
            this.exportAsPNGToolStripMenuItem.Click += new System.EventHandler(this.exportAsPNGToolStripMenuItem_Click);
            // 
            // exportAsRaw15BitsImageToolStripMenuItem
            // 
            this.exportAsRaw15BitsImageToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.colorPicker;
            this.exportAsRaw15BitsImageToolStripMenuItem.Name = "exportAsRaw15BitsImageToolStripMenuItem";
            this.exportAsRaw15BitsImageToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsRaw15BitsImageToolStripMenuItem.Text = "Export as raw 15 bits image";
            this.exportAsRaw15BitsImageToolStripMenuItem.Click += new System.EventHandler(this.exportAsRaw15BitsImageToolStripMenuItem_Click);
            // 
            // exportAsJoEngineTilesetToolStripMenuItem
            // 
            this.exportAsJoEngineTilesetToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.tileset;
            this.exportAsJoEngineTilesetToolStripMenuItem.Name = "exportAsJoEngineTilesetToolStripMenuItem";
            this.exportAsJoEngineTilesetToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsJoEngineTilesetToolStripMenuItem.Text = "Export map as Jo Engine tileset struct";
            this.exportAsJoEngineTilesetToolStripMenuItem.Click += new System.EventHandler(this.exportAsJoEngineTilesetToolStripMenuItem_Click);
            // 
            // exportAsBINToolStripMenuItem
            // 
            this.exportAsBINToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.script;
            this.exportAsBINToolStripMenuItem.Name = "exportAsBINToolStripMenuItem";
            this.exportAsBINToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exportAsBINToolStripMenuItem.Text = "Export as Jo Engine Bin file";
            this.exportAsBINToolStripMenuItem.Click += new System.EventHandler(this.exportAsBINToolStripMenuItem_Click);
            // 
            // deleteSelectionToolStripMenuItem
            // 
            this.deleteSelectionToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.delete;
            this.deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
            this.deleteSelectionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteSelectionToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.deleteSelectionToolStripMenuItem.Text = "Delete selection";
            this.deleteSelectionToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectionToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.selectAll;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // cutSelectionToolStripMenuItem
            // 
            this.cutSelectionToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.cut;
            this.cutSelectionToolStripMenuItem.Name = "cutSelectionToolStripMenuItem";
            this.cutSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutSelectionToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.cutSelectionToolStripMenuItem.Text = "Cut selection";
            this.cutSelectionToolStripMenuItem.Click += new System.EventHandler(this.cutSelectionToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.copyToolStripMenuItem.Text = "Copy selection";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.undo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::JoMapEditor.Properties.Resources.redo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.AllowDrop = true;
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.AutoScroll = true;
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.BackgroundImage = global::JoMapEditor.Properties.Resources.background;
            this.panelGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(784, 537);
            this.panelGrid.TabIndex = 0;
            this.panelGrid.TabStop = true;
            this.panelGrid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panelGrid_Scroll);
            this.panelGrid.Click += new System.EventHandler(this.panelGrid_Click);
            this.panelGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelGrid_DragDrop);
            this.panelGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelGrid_DragEnter);
            this.panelGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelGrid_MouseMove);
            this.panelGrid.Resize += new System.EventHandler(this.panelGrid_Resize);
            // 
            // MapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelGrid);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MapViewer";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.TabText = "Map";
            this.Text = "Map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapViewer_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapViewer_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal EditorControl panelGrid;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel positionToolStripStatusLabel;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exportAsCSourceFileToolStripMenuItem;
        private ToolStripMenuItem exportAsPNGToolStripMenuItem;
        private ToolStripMenuItem deleteSelectionToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem cutSelectionToolStripMenuItem;
        private ToolStripMenuItem exportAsTGAToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem explodeToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem exportAsBINToolStripMenuItem;
        private ToolStripMenuItem exportAsRaw15BitsImageToolStripMenuItem;
        private ToolStripMenuItem exportAsJoEngineTilesetToolStripMenuItem;
    }
}