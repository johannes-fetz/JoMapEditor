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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JoMapEditor
{
    public partial class MapViewer
    {
        private void SetTileSelected(Tile tile, bool focus)
        {
            if (focus)
                tile.Focus();
            tile.BorderStyle = BorderStyle.FixedSingle;
            if (!this._selectedTiles.Contains(tile))
                this._selectedTiles.Add(tile);
            if (Editor.Instance._tilePropertiesToolBox != null)
            {
                Editor.Instance._tilePropertiesToolBox.CurrentMapViewer = this;
                Editor.Instance._tilePropertiesToolBox.CurrentEditorControl = this.panelGrid;
                if (this._selectedTiles.Count != 1)
                    Editor.Instance._tilePropertiesToolBox.CurrrentTile = null;
                else
                    Editor.Instance._tilePropertiesToolBox.CurrrentTile = this._selectedTiles.First();
            }
        }

        private void SetTileUnselected(Tile tile)
        {
            tile.BorderStyle = BorderStyle.None;
            if (this._selectedTiles.Contains(tile))
                this._selectedTiles.Remove(tile);
        }

        private bool HasMouseMovedWhilePicking;

        private void PickingCameraHandling()
        {
            Point tmp = this.panelGrid.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            if (tmp.X < 0)
            {
                int abs = Math.Abs(tmp.X);
                if (this.panelGrid.HorizontalScroll.Value >= abs)
                    this.panelGrid.HorizontalScroll.Value -= abs;
                this.panelGrid.Refresh();
            }
            if (tmp.X > this.panelGrid.Width)
            {
                int abs = tmp.X - this.panelGrid.Width;
                this.panelGrid.HorizontalScroll.Value += abs;
                this.panelGrid.Refresh();
            }
            if (tmp.Y < 0)
            {
                int abs = Math.Abs(tmp.Y);
                if (this.panelGrid.VerticalScroll.Value >= abs)
                    this.panelGrid.VerticalScroll.Value -= abs;
                this.panelGrid.Refresh();
            }
            if (tmp.Y > this.panelGrid.Height)
            {
                int abs = tmp.Y - this.panelGrid.Height;
                this.panelGrid.VerticalScroll.Value += abs;
                this.panelGrid.Refresh();
            }
        }

        private void panelGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._isPicking)
            {
                this.PickingCameraHandling();
                this.HasMouseMovedWhilePicking = true;
                for (int i = 0; i < this._selectedTiles.Count; ++i)
                {
                    Point p = this.panelGrid.PointToClient(new Point(Cursor.Position.X - this._pickingTiles[i].X, Cursor.Position.Y - this._pickingTiles[i].Y));
                    if (p.X < 0)
                        p.X = 0;
                    if (p.Y < 0)
                        p.Y = 0;
                    if (this._alignmentX != 1)
                        p.X = p.X / this._alignmentX * this._alignmentX;
                    if (this._alignmentY != 1)
                        p.Y = p.Y / this._alignmentY * this._alignmentY;

                    p.X -= this.panelGrid.HorizontalScroll.Value % this._alignmentX;
                    p.Y -= this.panelGrid.VerticalScroll.Value % this._alignmentY;

                    this._selectedTiles[i].Location = p;
                    if (Editor.Instance._tilePropertiesToolBox != null && Editor.Instance._tilePropertiesToolBox.CurrrentTile != null)
                        Editor.Instance._tilePropertiesToolBox.UpdateGUI();
                    this.MarkFileAsEdited();
                }
            }
            this.UpdateStatusbar();
        }

        private void panelGrid_Click(object sender, EventArgs e)
        {
            if (!this.panelGrid.Focused)
                this.panelGrid.Focus();
            while (this._selectedTiles.Count > 0)
                this.SetTileUnselected(this._selectedTiles.First());
            this.UpdateStatusbar();
        }

        private void tile_MouseUp(object sender, MouseEventArgs e)
        {
            if (this._isPicking && this.HasMouseMovedWhilePicking)
            {
                this.panelGrid.Refresh();
                this.PushForUndoRedo();
            }
            this.HasMouseMovedWhilePicking = false;
            this._isPicking = false;
            this._pickingTiles.Clear();
            Cursor.Current = Cursors.Cross;
        }

        private void tile_MouseDown(object sender, MouseEventArgs e)
        {
            this.HasMouseMovedWhilePicking = false;
            if (!this._selectedTiles.Contains(sender as Panel))
                return;
            foreach (Panel tile in this._selectedTiles)
                this._pickingTiles.Add(tile.PointToClient(Cursor.Position));
            this._isPicking = true;
            Cursor.Current = Cursors.Hand;
        }

        private void tile_Click(object sender, EventArgs e)
        {
            Tile tile = sender as Tile;
            if (!this.panelGrid.Focused)
                this.panelGrid.Focus();
            if (Control.ModifierKeys != Keys.Control)
            {
                while (this._selectedTiles.Count > 0)
                    this.SetTileUnselected(this._selectedTiles.First());
            }
            this.SetTileSelected(tile, false);
            this.UpdateStatusbar();
        }
        private void PasteSprites()
        {
            if (MapViewer._clipboard != null && MapViewer._clipboard.Count > 0)
            {
                this.SuspendLayout();
                Point mousePos = this.panelGrid.PointToClient(Cursor.Position);
                Point delta = new Point(mousePos.X - MapViewer._clipboard[0].Location.X, mousePos.Y - MapViewer._clipboard[0].Location.Y);
                this.AddTile(MapViewer._clipboard[0].Sprite, mousePos, MapViewer._clipboard[0].Attribute);
                for (int i = 1; i < MapViewer._clipboard.Count; ++i)
                {
                    Point p = MapViewer._clipboard[i].Location;
                    p.X += delta.X;
                    p.Y += delta.Y;
                    this.AddTile(MapViewer._clipboard[i].Sprite, p, MapViewer._clipboard[i].Attribute);
                }
                this.ResumeLayout();
                this.PushForUndoRedo();
            }
        }

        private void CutSelectedSprites()
        {
            if (this._selectedTiles.Count < 1)
                return;
            if (Editor.Instance._tilePropertiesToolBox != null)
                Editor.Instance._tilePropertiesToolBox.CurrrentTile = null;
            MapViewer._clipboard = new List<Tile>(this._selectedTiles);
            this.SuspendLayout();
            foreach (Panel tile in this._selectedTiles)
                this.panelGrid.Controls.Remove(tile);
            this.ResumeLayout();
        }

        private void CopySelectedSprites()
        {
            if (this._selectedTiles.Count < 1)
                return;
            MapViewer._clipboard = new List<Tile>(this._selectedTiles);
        }

        private void DeleteSelectedSprites()
        {
            if (this._selectedTiles.Count < 1)
                return;
            if (Editor.Instance._tilePropertiesToolBox != null)
                Editor.Instance._tilePropertiesToolBox.CurrrentTile = null;
            foreach (Panel tile in this._selectedTiles)
                this.panelGrid.Controls.Remove(tile);
            this._selectedTiles.Clear();
            this.MarkFileAsEdited();
            this.UpdateStatusbar();
            this.PushForUndoRedo();
        }

        private void SelectAllSprites()
        {
            if (!this.panelGrid.Focused)
                this.panelGrid.Focus();
            this.SuspendLayout();
            while (this._selectedTiles.Count > 0)
                this.SetTileUnselected(this._selectedTiles.First());
            foreach (Control tile in this.panelGrid.Controls)
            {
                if (tile is Tile)
                    this.SetTileSelected((Tile)tile, false);
            }
            this.ResumeLayout();
        }

        private void MapViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (this._selectedTiles.Count < 1)
                    return;
                if (this._selectedTiles.TrueForAll(item => item.Left >= this._alignmentX))
                {
                    foreach (Panel tile in this._selectedTiles)
                        tile.Left -= this._alignmentX;
                    if (Editor.Instance._tilePropertiesToolBox != null && Editor.Instance._tilePropertiesToolBox.CurrrentTile != null)
                        Editor.Instance._tilePropertiesToolBox.UpdateGUI();
                    this.MarkFileAsEdited();
                    e.Handled = true;
                    this.UpdateStatusbar();
                    this.PushForUndoRedo();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (this._selectedTiles.Count < 1)
                    return;
                foreach (Panel tile in this._selectedTiles)
                    tile.Left += this._alignmentX;
                if (Editor.Instance._tilePropertiesToolBox != null && Editor.Instance._tilePropertiesToolBox.CurrrentTile != null)
                    Editor.Instance._tilePropertiesToolBox.UpdateGUI();
                this.MarkFileAsEdited();
                e.Handled = true;
                this.UpdateStatusbar();
                this.PushForUndoRedo();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (this._selectedTiles.Count < 1)
                    return;
                if (this._selectedTiles.TrueForAll(item => item.Top >= this._alignmentY))
                {
                    foreach (Panel tile in this._selectedTiles)
                        tile.Top -= this._alignmentY;
                    if (Editor.Instance._tilePropertiesToolBox != null && Editor.Instance._tilePropertiesToolBox.CurrrentTile != null)
                        Editor.Instance._tilePropertiesToolBox.UpdateGUI();
                    this.MarkFileAsEdited();
                    e.Handled = true;
                    this.UpdateStatusbar();
                    this.PushForUndoRedo();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (this._selectedTiles.Count < 1)
                    return;
                foreach (Panel tile in this._selectedTiles)
                    tile.Top += this._alignmentY;
                if (Editor.Instance._tilePropertiesToolBox != null && Editor.Instance._tilePropertiesToolBox.CurrrentTile != null)
                    Editor.Instance._tilePropertiesToolBox.UpdateGUI();
                this.MarkFileAsEdited();
                e.Handled = true;
                this.UpdateStatusbar();
                this.PushForUndoRedo();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (this._selectedTiles.Count < 1)
                    return;
                this.DeleteSelectedSprites();
                e.Handled = true;
                this.UpdateStatusbar();
            }
        }

        private bool _isPicking;
        private static List<Tile> _clipboard = new List<Tile>();
        private List<Tile> _selectedTiles = new List<Tile>();
        private List<Point> _pickingTiles = new List<Point>();
    }
}