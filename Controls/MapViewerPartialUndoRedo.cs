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
        public class UndoRedoItem
        {
            public Sprite SpriteData;
            public int? Attribute;
            public int Left;
            public int Top;
            public IntPtr OriginalTileHandle;
        }

        public class UndoRedoFrame
        {
            public List<UndoRedoItem> Items = new List<UndoRedoItem>();
        }

        private List<UndoRedoFrame> UndoRedoFrames = new List<UndoRedoFrame>();
        private int UndoRedoCursor;

        public void Undo()
        {
            if (this.UndoRedoCursor <= 0)
                return;
            --this.UndoRedoCursor;
            this.RestoreUndoRedo();
        }

        public void Redo()
        {
            if ((this.UndoRedoCursor + 1) >= this.UndoRedoFrames.Count)
                return;
            ++this.UndoRedoCursor;
            this.RestoreUndoRedo();
        }

        public void RestoreUndoRedo()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.panelGrid.Visible = false;
                List<UndoRedoItem> items = this.UndoRedoFrames[this.UndoRedoCursor].Items;
                List<Control> controls = new List<Control>(this.panelGrid.Controls.Cast<Control>().Where(item => item is Tile));
                foreach (Tile tile in controls)
                {
                    UndoRedoItem bak = items.FirstOrDefault(item => item.OriginalTileHandle == tile.Handle);
                    if (bak == null)
                        this.panelGrid.Controls.Remove(tile);
                    else
                    {
                        if (bak.Left != (int)(tile.Left * this._zoomFactor))
                            tile.Left = (int)(bak.Left * this._zoom);
                        if (bak.Top != (int)(tile.Top * this._zoomFactor))
                            tile.Top = (int)(bak.Top * this._zoom);
                    }
                }
                foreach (UndoRedoItem bak in items.Where(item => !controls.Any(p => p.Handle == item.OriginalTileHandle)))
                    this.AddTile(bak.SpriteData, new Point(bak.Left, bak.Top), bak.Attribute);
                this.panelGrid.Visible = true;
                this.panelGrid.Focus();
                this._pickingTiles.Clear();
                this._isPicking = false;
                this.UpdateStatusbar();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void CancelUndoRedo()
        {
            this.UndoRedoFrames.RemoveAt(this.UndoRedoFrames.Count - 1);
            --this.UndoRedoCursor;
        }

        public void PushForUndoRedo()
        {
            UndoRedoFrame frame = new UndoRedoFrame();
            foreach (Tile tile in this.panelGrid.Controls.Cast<Control>().Where(item => item is Tile))
            {
                frame.Items.Add(new UndoRedoItem
                {
                    OriginalTileHandle = tile.Handle,
                    SpriteData = tile.Sprite,
                    Left = (int)(tile.Left * this._zoomFactor),
                    Top = (int)(tile.Top * this._zoomFactor),
                    Attribute = tile.Attribute
                });
            }
            if ((this.UndoRedoCursor + 1) < this.UndoRedoFrames.Count)
                this.UndoRedoFrames.RemoveRange(this.UndoRedoCursor, this.UndoRedoFrames.Count - this.UndoRedoCursor);
            this.UndoRedoFrames.Add(frame);
            this.UndoRedoCursor = this.UndoRedoFrames.Count - 1;
        }
    }
}
