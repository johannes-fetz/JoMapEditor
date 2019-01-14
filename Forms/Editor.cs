/*
** Jo Sega Saturn Engine
** Copyright (c) 2012-2019, Johannes Fetz (johannesfetz@gmail.com)
** All rights reserved.
**
** Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are met:
**     * Redistributions of source code must retain the above copyright
**       notice, this list of conditions and the following disclaimer.
**     * Redistributions in binary form must reproduce the above copyright
**       notice, this list of conditions and the following disclaimer in the
**       documentation and/or other materials provided with the distribution.
**     * Neither the name of the Johannes Fetz nor the
**       names of its contributors may be used to endorse or promote products
**       derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
** ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
** WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
** DISCLAIMED. IN NO EVENT SHALL Johannes Fetz BE LIABLE FOR ANY
** DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
** (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
** LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
** ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JoMapEditor.Properties;
using Paloma;
using Silver.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace JoMapEditor
{
    public partial class Editor : Form
    {
        public static Editor Instance { get; private set; }

        public Editor()
        {
            this.InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            Editor.Instance = this;
            try
            {
                Editor.Config = new IniConfigFile(Path.Combine(Environment.CurrentDirectory, "JoMapEditor.ini"));
                string transparentColor = Editor.Config.Read("user", "transparent_color");
                if (!String.IsNullOrWhiteSpace(transparentColor))
                {
                    int argb;
                    if (int.TryParse(transparentColor, out argb))
                        Editor.TransparentColor = Color.FromArgb(argb);
                }
                this.SetColorIcon();
                string lastmap = Editor.Config.Read("user", "last_map");
                if (!String.IsNullOrWhiteSpace(lastmap))
                {
                    ToolStripMenuItem ts = new ToolStripMenuItem();
                    ts.Text = Path.GetFileName(lastmap);
                    ts.Image = Resources.fav;
                    ts.Click += (sender, e) =>
                    {
                        MapViewer viewer = new MapViewer();
                        if (!viewer.OpenFile(lastmap))
                            return;
                        this.ApplyConfigToMapViewer(viewer);
                    };
                    fileToolStripMenuItem.DropDownItems.Insert(0, ts);
                }
                this.PopulateToolBox();
                this.OpenSpriteAttributeToolBox();
                this.InfoToolStripStatusLabel.Text = String.Empty;
            }
            catch (Exception ex)
            {
                Program.Logger.Fatal(@"Ctor()", ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetColorIcon()
        {
            if (this.changeTransparentColorToolStripMenuItem.Image != null)
                this.changeTransparentColorToolStripMenuItem.Image.Dispose();
            Bitmap colorIcon = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(colorIcon))
            {
                g.Clear(Editor.TransparentColor);
            }
            this.changeTransparentColorToolStripMenuItem.Image = colorIcon;
        }

        private void RefreshAvailableSprites()
        {
            if (this._spriteToolBox == null)
                return;
            this._spriteToolBox.Close();
            this.PopulateToolBox();
        }

        private void refreshAvailableSpritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshAvailableSprites();
        }

        private void _spriteToolBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._spriteToolBox.Dispose();
            this._spriteToolBox = null;
            this.availableSpritesToolStripMenuItem.Enabled = true;
            this.refreshAvailableSpritesToolStripMenuItem.Enabled = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
                if (String.IsNullOrWhiteSpace(viewer.MapPath) || MessageBox.Show("Do you wants to save change on this map ?", viewer.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    viewer.SaveAsFile();
        }

        private void ApplyConfigToMapViewer(MapViewer viewer)
        {
            viewer.Text = Path.GetFileName(viewer.MapPath);
            viewer.TabText = viewer.Text;
            viewer.ChangeScale(this._zoomFactor);
            viewer.ChangeTileAlignment(this._alignmentX, this._alignmentY);
            viewer.FormClosed += this.viewer_FormClosed;
            viewer.MarkFileAsNormal();
            this._documents.Add(viewer);
            viewer.Show(this.dockPanel1);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapViewer viewer = new MapViewer();
            if (!viewer.OpenFile())
                return;
            this.ApplyConfigToMapViewer(viewer);
            Editor.Config.Write("user", "last_map", viewer.MapPath);
        }

        private void viewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._documents.Remove(sender as MapViewer);
        }

        private MapViewer CreateNewMapViewer()
        {
            MapViewer viewer = new MapViewer();
            viewer.ChangeScale(this._zoomFactor);
            viewer.ChangeTileAlignment(this._alignmentX, this._alignmentY);
            viewer.FormClosed += this.viewer_FormClosed;
            viewer.Text = String.Format("New map {0}", this._doc_count);
            viewer.TabText = viewer.Text;
            this._documents.Add(viewer);
            viewer.Show(this.dockPanel1);
            ++this._doc_count;
            return viewer;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateNewMapViewer();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changeSpritesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Please select sprites folder";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Editor.Config.Write("directory", "sprites", dialog.SelectedPath);
                    if (this._spriteToolBox != null)
                        this._spriteToolBox.Close();
                    this.PopulateToolBox();
                }
            }
        }

        private void OpenSpriteAttributeToolBox()
        {
            if (this._tilePropertiesToolBox != null)
                return;
            this._tilePropertiesToolBox = new TilePropertiesToolBox();
            this._tilePropertiesToolBox.DockAreas = DockAreas.DockRight;
            this._tilePropertiesToolBox.FormClosed += this._tilePropertiesToolBox_FormClosed;
            this._tilePropertiesToolBox.Show(this.dockPanel1);
            this.tilePropertiesToolStripMenuItem.Enabled = false;
        }

        private void _tilePropertiesToolBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._tilePropertiesToolBox.Dispose();
            this._tilePropertiesToolBox = null;
            this.tilePropertiesToolStripMenuItem.Enabled = true;
        }

        private void PopulateToolBox()
        {
            string spritePath = Editor.Config.Read("directory", "sprites");
            if (String.IsNullOrWhiteSpace(spritePath) || !Directory.Exists(spritePath))
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Please select sprites folder";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        spritePath = dialog.SelectedPath;
                        Editor.Config.Write("directory", "sprites", spritePath);
                    }
                    if (String.IsNullOrWhiteSpace(spritePath))
                    {
                        this.availableSpritesToolStripMenuItem.Enabled = true;
                        return;
                    }
                }
            }
            this._spriteToolBox = new SpriteToolBox();
            this._spriteToolBox.DockAreas = DockAreas.DockLeft;
            if (this._spriteToolBox.Populate(spritePath))
            {
                this._spriteToolBox.FormClosed += this._spriteToolBox_FormClosed;
                this._spriteToolBox.Show(this.dockPanel1);
                this.availableSpritesToolStripMenuItem.Enabled = false;
                this.refreshAvailableSpritesToolStripMenuItem.Enabled = true;
            }
        }

        private void exportMapAsTGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAsTga();
            }
        }

        private void exportMapAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAsPng();
            }
        }

        private void exportMapAsCSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAsC();
            }
        }

        private void exportMapAsBINImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAsBin();
            }
        }

        private void exportMapAsRaw15BitsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAs15BitsRawImage();
            }
        }

        private void exportMapAsJoEngineTilesetStructToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MapViewer viewer in this._documents)
            {
                if (!viewer.IsActivated || viewer.Empty)
                    continue;
                viewer.ExportAsJoImageTileset();
            }
        }

        private void tilePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenSpriteAttributeToolBox();
        }

        private void availableSpritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PopulateToolBox();
        }

        private void joMapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Jo Map Editor
Copyright (c) 2014-2018, Johannes Fetz (johannesfetz@gmail.com)
All rights reserved.

This program is a part of the Jo Sega Saturn Engine under the MIT licence.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            this.UpdateMenus();
        }

        private void UpdateMenus()
        {
            if (this.dockPanel1.ActiveDocument is MapViewer)
                this.InfoToolStripStatusLabel.Text = (this.dockPanel1.ActiveDocument as MapViewer).MapPath;
            else
                this.InfoToolStripStatusLabel.Text = String.Empty;
            this.exportMapAsPNGToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
            this.exportMapAsCSourceToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
            this.exportMapAsTGAToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
            this.exportMapAsBINImageToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
            this.exportMapAsRaw15BitsImageToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
            this.exportMapAsJoEngineTilesetStructToolStripMenuItem.Enabled = this._documents.Any(item => item.IsActivated && !item.Empty);
        }

        private void InfoToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            string args = string.Format("/Select, {0}", this.InfoToolStripStatusLabel.Text);
            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
            Process.Start(pfi);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"On map:

- Use arrow keys to move selected sprites
- You can multi-select by pressing control and left-click
- You can access to extra features on context-menu (right click)
- When you load an existing MAP you can click on the linklabel at the bottom to open the folder
", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Alignment

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = true;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 1;
            this._alignmentY = 1;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(1, 1));
        }

        private void x48ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = true;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 48;
            this._alignmentY = 48;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(48, 48));
        }

        private void x24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = true;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 24;
            this._alignmentY = 24;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(24, 24));
        }

        private void x16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = true;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 16;
            this._alignmentY = 16;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(16, 16));
        }

        private void x32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = true;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 32;
            this._alignmentY = 32;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(32, 32));
        }

        private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = true;
            this.x8ToolStripMenuItem.Checked = false;
            this._alignmentX = 160;
            this._alignmentY = 120;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(160, 120));
        }

        private void x8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.noneToolStripMenuItem.Checked = false;
            this.x48ToolStripMenuItem.Checked = false;
            this.x24ToolStripMenuItem.Checked = false;
            this.x16ToolStripMenuItem.Checked = false;
            this.x32ToolStripMenuItem.Checked = false;
            this.x120ToolStripMenuItem.Checked = false;
            this.x8ToolStripMenuItem.Checked = true;
            this._alignmentX = 8;
            this._alignmentY = 8;
            this._documents.ForEach(doc => doc.ChangeTileAlignment(8, 8));
        }

        #endregion

        #region Zoom

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.x2ToolStripMenuItem.Checked = true;
            this.x4ToolStripMenuItem1.Checked = false;
            this.x0_5ToolStripMenuItem2.Checked = false;
            this.defaultToolStripMenuItem.Checked = false;
            this._zoomFactor = 2.0M;
            this._documents.ForEach(doc => doc.ChangeScale(2.0M));
        }

        private void x4ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.x2ToolStripMenuItem.Checked = false;
            this.x4ToolStripMenuItem1.Checked = true;
            this.x0_5ToolStripMenuItem2.Checked = false;
            this.defaultToolStripMenuItem.Checked = false;
            this._zoomFactor = 4.0M;
            this._documents.ForEach(doc => doc.ChangeScale(4.0M));
        }

        private void x0_5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.x2ToolStripMenuItem.Checked = false;
            this.x4ToolStripMenuItem1.Checked = false;
            this.x0_5ToolStripMenuItem2.Checked = true;
            this.defaultToolStripMenuItem.Checked = false;
            this._zoomFactor = 0.5M;
            this._documents.ForEach(doc => doc.ChangeScale(0.5M));
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.x2ToolStripMenuItem.Checked = false;
            this.x4ToolStripMenuItem1.Checked = false;
            this.x0_5ToolStripMenuItem2.Checked = false;
            this.defaultToolStripMenuItem.Checked = true;
            this._zoomFactor = 1.0M;
            this._documents.ForEach(doc => doc.ChangeScale(1.0M));
        }

        #endregion

        #region Fields

        internal static IniConfigFile Config;
        internal List<MapViewer> _documents = new List<MapViewer>();
        internal SpriteToolBox _spriteToolBox;
        internal TilePropertiesToolBox _tilePropertiesToolBox;
        private decimal _zoomFactor = 1.0M;
        private int _alignmentX = 8;
        private int _alignmentY = 8;
        private int _doc_count = 1;
        internal static Color TransparentColor = Color.FromArgb(0, 255, 0);

        #endregion

        private void dockPanel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ToolBoxItem)))
            {
                MapViewer viewer = this.CreateNewMapViewer();
                viewer.DropToolBoxItem(e.Data.GetData(typeof(ToolBoxItem)) as ToolBoxItem, new Point(e.X, e.Y));
            }
            else if (e.Data.GetDataPresent("FileNameW"))
            {
                MapViewer viewer = this.CreateNewMapViewer();
                string[] paths = (string[])e.Data.GetData("FileNameW");
                foreach (string path in paths)
                {
                    Bitmap img = null;
                    if (path.ToLower().EndsWith("tga"))
                        img = TargaImage.LoadTargaImage(path);
                    else
                        img = new Bitmap(path);
                    img.ReplaceColor(Editor.TransparentColor, Color.Transparent);
                    Sprite sprite = new Sprite
                    {
                        Img = img,
                        Path = null
                    };
                    viewer.AddTile(sprite, this.PointToClient(new Point(e.X, e.Y)));
                }
            }
        }

        private void dockPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ToolBoxItem)) || e.Data.GetDataPresent("FileNameW"))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void changeTransparentColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = Editor.TransparentColor;
                dialog.AllowFullOpen = true;
                dialog.FullOpen = true;
                dialog.SolidColorOnly = true;
                dialog.CustomColors = new[]
                {
                    0xFF0000,
                    0x00FF00,
                    0x0000FF,
                    0xFF00FF
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Editor.TransparentColor = dialog.Color;
                    this.SetColorIcon();
                    Editor.Config.Write("user", "transparent_color", dialog.Color.ToArgb().ToString());
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        this.RefreshAvailableSprites();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void colladaToJoEngine3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColladaImportForm frm = new ColladaImportForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void wavefrontobjToJoEngineSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ObjImportForm frm = new ObjImportForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.UpdateMenus();
        }
    }
}
