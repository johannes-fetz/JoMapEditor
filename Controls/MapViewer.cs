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
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JoMapEditor.Properties;
using Paloma;
using Silver.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace JoMapEditor
{
    public partial class MapViewer : DockContent
    {
        internal static readonly Cursor SegaSaturnScreenCursor = JoMapEditorTools.CursorFromBitmap(Resources.saturn_screen2);
        internal static readonly Cursor SegaSaturnScreenCursor05x = JoMapEditorTools.CursorFromBitmap(JoMapEditorTools.ResizeImage(Resources.saturn_screen2, Resources.saturn_screen2.Width / 2, Resources.saturn_screen2.Height / 2));
        internal static readonly Cursor SegaSaturnScreenCursor2x = JoMapEditorTools.CursorFromBitmap(JoMapEditorTools.ResizeImage(Resources.saturn_screen2, Resources.saturn_screen2.Width * 2, Resources.saturn_screen2.Height * 2));
        internal static readonly Cursor SegaSaturnScreenCursor4x = JoMapEditorTools.CursorFromBitmap(JoMapEditorTools.ResizeImage(Resources.saturn_screen2, Resources.saturn_screen2.Width * 4, Resources.saturn_screen2.Height * 4));

        public MapViewer()
        {
            this.InitializeComponent();
        }

        public class EditorControl : Panel
        {
            bool displayScreenCursor;

            internal void ToggleScreenCursor()
            {
                displayScreenCursor ^= true;
                if (displayScreenCursor)
                {
                    MapViewer parent = (MapViewer)this.Parent;
                    if (parent._zoom == 0.5M)
                        this.Cursor = MapViewer.SegaSaturnScreenCursor05x;
                    else if (parent._zoom == 2.0M)
                        this.Cursor = MapViewer.SegaSaturnScreenCursor2x;
                    else if (parent._zoom == 4.0M)
                        this.Cursor = MapViewer.SegaSaturnScreenCursor4x;
                    else
                        this.Cursor = MapViewer.SegaSaturnScreenCursor;

                }
                else
                    this.Cursor = Cursors.Default;
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                if (e.KeyCode == Keys.F11)
                    this.ToggleScreenCursor();                
                base.OnKeyDown(e);
            }

            protected override bool IsInputKey(Keys keyData)
            {
                MapViewer parent = (MapViewer)this.Parent;
                bool hasSelectedTiles = parent._selectedTiles.Count > 0;
                if (keyData == Keys.Up)
                {
                    if (!hasSelectedTiles)
                    {
                        int val = this.VerticalScroll.Value - (int)(20 * parent._zoom);
                        if (val < this.VerticalScroll.Minimum)
                            val = this.VerticalScroll.Minimum;
                        this.VerticalScroll.Value = val;
                        this.Invalidate();
                    }
                    return true;
                }
                if (keyData == Keys.Down)
                {
                    if (!hasSelectedTiles)
                    {
                        int val = this.VerticalScroll.Value + (int)(20 * parent._zoom);
                        if (val > this.VerticalScroll.Maximum)
                            val = this.VerticalScroll.Maximum;
                        this.VerticalScroll.Value = val;
                        this.Invalidate();
                    }
                    return true;
                }
                if (keyData == Keys.Left)
                {
                    if (!hasSelectedTiles)
                    {
                        int val = this.HorizontalScroll.Value - (int)(20 * parent._zoom);
                        if (val < this.HorizontalScroll.Minimum)
                            val = this.HorizontalScroll.Minimum;
                        this.HorizontalScroll.Value = val;
                        this.Invalidate();
                    }
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    if (!hasSelectedTiles)
                    {
                        int val = this.HorizontalScroll.Value + (int)(20 * parent._zoom);
                        if (val > this.HorizontalScroll.Maximum)
                            val = this.HorizontalScroll.Maximum;
                        this.HorizontalScroll.Value = val;
                        this.Invalidate();
                    }
                    return true;
                }
                return base.IsInputKey(keyData);
            }

            public EditorControl()
            {
                this.DoubleBuffered = true;
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                base.OnPaintBackground(e);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                //base.OnPaint(e);
            }
        }

        public void DropToolBoxItem(ToolBoxItem item, Point where)
        {
            Tile tile = this.AddTile(item.Object as Sprite, this.panelGrid.PointToClient(where));
            this.SetTileSelected(tile, true);
            this.PushForUndoRedo();
        }

        private void panelGrid_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ToolBoxItem)))
                this.DropToolBoxItem(e.Data.GetData(typeof(ToolBoxItem)) as ToolBoxItem, new Point(e.X, e.Y));
            else if (e.Data.GetDataPresent("FileNameW"))
            {
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
                    this.AddTile(sprite, this.PointToClient(new Point(e.X, e.Y)));
                }
            }
        }

        private void panelGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ToolBoxItem)) || e.Data.GetDataPresent("FileNameW"))
                e.Effect = DragDropEffects.Copy;
            else if (e.Data.GetDataPresent(typeof(Panel)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void UpdateStatusbar()
        {
            Point p = this.panelGrid.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            StringBuilder sb = new StringBuilder();
            int count = this.panelGrid.Controls.Cast<Control>().Count(item => item is Tile);
            sb.AppendFormat("x:{0,-5} y:{1,-5} ", p.X * this._zoomFactor, p.Y * this._zoomFactor);
            if (this._selectedTiles.Count > 0)
            {
                if (this._selectedTiles.Count == count)
                    sb.Append("All sprites selected");
                else
                    sb.AppendFormat("{0} of {1} sprites selected", this._selectedTiles.Count, count);
            }
            else if (count > 0)
                sb.AppendFormat("{0} sprites", count);
            this.positionToolStripStatusLabel.Text = sb.ToString();
        }

        private void MapViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.panelGrid.Controls.Cast<Control>().Count(item => item is Tile) > 0 && this._hasChange)
            {
                if (MessageBox.Show("Do you wants to save change on this map ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!this.SaveAsFile())
                        e.Cancel = true;
                }
            }
        }

        public Tile AddTile(Sprite sprite, Point location, int? attribute = null)
        {
            Tile tile = new Tile();
            tile.Click += this.tile_Click;
            tile.MouseDown += this.tile_MouseDown;
            tile.MouseUp += this.tile_MouseUp;
            tile.BackgroundImageLayout = ImageLayout.Stretch;
            tile.Sprite = sprite;
            tile.Attribute = attribute;
            tile.MouseMove += panelGrid_MouseMove;
            tile.Width = (int)(sprite.Img.Width * (1.0M / this._zoomFactor));
            tile.Height = (int)(sprite.Img.Height * (1.0M / this._zoomFactor));
            tile.BackgroundImage = sprite.Img;
            tile.Location = location;
            if (this._alignmentX != 1)
                tile.Left = (tile.Left / this._alignmentX) * this._alignmentX;
            if (this._alignmentY != 1)
                tile.Top = (tile.Top / this._alignmentY) * this._alignmentY;
            this.MarkFileAsEdited();
            tile.Parent = this.panelGrid;
            return tile;
        }

        public void ChangeTileAlignment(int x, int y)
        {
            this._alignmentX = x;
            this._alignmentY = y;
            this.CheckTilesAlignment();
            this.MarkFileAsEdited();
        }

        public void MarkFileAsEdited()
        {
            this._hasChange = true;
            if (!this.Text.EndsWith("*"))
            {
                this.Text += "*";
                this.TabText = this.Text;
            }
        }

        public void MarkFileAsNormal()
        {
            this._hasChange = false;
            if (this.Text.EndsWith("*"))
            {
                this.Text = this.Text.TrimEnd('*');
                this.TabText = this.Text;
            }
        }

        public void CheckTilesAlignment()
        {
            foreach (Control tile in this.panelGrid.Controls)
            {
                if (!(tile is Tile))
                    continue;
                if (this._alignmentX != 1)
                    tile.Left = (tile.Left / this._alignmentX) * this._alignmentX;
                if (this._alignmentY != 1)
                    tile.Top = (tile.Top / this._alignmentY) * this._alignmentY;
            }
            this.MarkFileAsEdited();
        }

        public void ChangeScale(decimal wantedZoom)
        {
            this.panelGrid.Visible = false;
            this.panelGrid.SuspendLayout();
            this.panelGrid.HorizontalScroll.Value = 0;
            this.panelGrid.VerticalScroll.Value = 0;
            foreach (Control tile in this.panelGrid.Controls)
            {
                if (!(tile is Tile))
                    continue;
                if (this._zoom != 1)
                {
                    tile.Width = (int)(tile.Width * this._zoomFactor);
                    tile.Height = (int)(tile.Height * this._zoomFactor);
                    tile.Left = (int)(tile.Left * this._zoomFactor);
                    tile.Top = (int)(tile.Top * this._zoomFactor);
                }
                if (wantedZoom != 1.0M)
                {
                    tile.Width = (int)(tile.Width * wantedZoom);
                    tile.Height = (int)(tile.Height * wantedZoom);
                    tile.Left = (int)(tile.Left * wantedZoom);
                    tile.Top = (int)(tile.Top * wantedZoom);
                }
            }
            this._zoom = wantedZoom;
            this._zoomFactor = 1.0M / wantedZoom;
            this.panelGrid.ResumeLayout();
            this.panelGrid.Visible = true;
        }

        public void DrawControl(Tile control, Bitmap bitmap, int left, int top)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(control.Sprite.Img, (int)(control.Bounds.Left / this._zoom) - left, (int)(control.Bounds.Top / this._zoom) - top, (int)(control.Width / this._zoom), (int)(control.Height / this._zoom));
            }
            foreach (Control childControl in control.Controls)
            {
                if (childControl is Tile)
                    this.DrawControl((Tile)childControl, bitmap, left, top);
            }
        }

        public Bitmap GetBitmap(bool onlySelected = false)
        {
            int left = int.MaxValue;
            int top = int.MaxValue;
            int right = 0;
            int bottom = 0;

            #region Get Tiles

            List<Tile> list;
            if (onlySelected && this._selectedTiles.Count > 0)
                list = this._selectedTiles;
            else
            {
                list = new List<Tile>();
                foreach (Control control in this.panelGrid.Controls)
                {
                    if (control is Tile)
                        list.Add((Tile)control);
                }
            }

            #endregion

            #region Crop

            foreach (Panel control in list)
            {
                if (control.Left < left)
                    left = control.Left;
                if (control.Top < top)
                    top = control.Top;
                if ((control.Left + control.Width) > right)
                    right = control.Left + control.Width;
                if ((control.Top + control.Height) > bottom)
                    bottom = control.Top + control.Height;
            }

            #endregion

            left = (int)(left / this._zoom);
            right = (int)(right / this._zoom);
            bottom = (int)(bottom / this._zoom);
            top = (int)(top / this._zoom);

            Bitmap bitmap = new Bitmap(right - left, bottom - top, PixelFormat.Format32bppArgb);
            foreach (Tile control in list)
                this.DrawControl(control, bitmap, left, top);
            return bitmap;
        }

        private void exportAsRaw15BitsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAs15BitsRawImage(true);
        }

        private void exportAsCSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAsC(true);
        }

        private void exportAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAsPng(true);
        }

        private void exportAsBINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAsBin(true);
        }

        private void exportAsTGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAsTga(true);
        }

        private void exportAsJoEngineTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportAsJoImageTileset(true);
        }

        private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteSelectedSprites();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopySelectedSprites();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PasteSprites();
        }

        private void cutSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CutSelectedSprites();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Redo();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.explodeToolStripMenuItem.Enabled = this._selectedTiles.Count > 0;
            this.exportAsCSourceFileToolStripMenuItem.Enabled = !this.Empty;
            this.exportAsPNGToolStripMenuItem.Enabled = !this.Empty;
            this.exportAsTGAToolStripMenuItem.Enabled = !this.Empty;
            this.exportAsBINToolStripMenuItem.Enabled = !this.Empty;
            this.exportAsRaw15BitsImageToolStripMenuItem.Enabled = !this.Empty;
            this.selectAllToolStripMenuItem.Enabled = !this.Empty;
            this.deleteSelectionToolStripMenuItem.Enabled = this._selectedTiles.Count > 0;
            this.cutSelectionToolStripMenuItem.Enabled = this._selectedTiles.Count > 0;
            this.copyToolStripMenuItem.Enabled = this._selectedTiles.Count > 0;
            this.pasteToolStripMenuItem.Enabled = MapViewer._clipboard != null && MapViewer._clipboard.Count > 0;
            this.undoToolStripMenuItem.Enabled = this.UndoRedoCursor > 0;
            this.redoToolStripMenuItem.Enabled = (this.UndoRedoCursor + 1) < this.UndoRedoFrames.Count;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SelectAllSprites();
        }

        private void explodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DisplayExplodeTileForm();
        }

        private void panelGrid_Scroll(object sender, ScrollEventArgs e)
        {
            this.panelGrid.Refresh();
        }

        private void panelGrid_Resize(object sender, EventArgs e)
        {
            this.panelGrid.Refresh();
        }

        #region Properties

        public bool Empty
        {
            get
            {
                return this.panelGrid.Controls.Cast<Control>().Count(item => item is Tile) <= 0;
            }
        }

        public string MapPath { get; private set; }

        #endregion

        #region Fields

        internal decimal _zoom = 1.0M;
        internal decimal _zoomFactor = 1.0M;
        internal int _alignmentX = 8;
        internal int _alignmentY = 8;
        internal bool _hasChange;

        #endregion
    }
}
