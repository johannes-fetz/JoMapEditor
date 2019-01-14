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

using System.Drawing;
using System.Windows.Forms;

namespace JoMapEditor
{
    public partial class MapViewer
    {
        private void DisplayExplodeTileForm()
        {
            int? tileWidth;
            int? tileHeight;
            bool addRightMarginForSegaSaturn;
            using (TileSizeForm frm = new TileSizeForm())
            {
                frm.ShowDialog();
                tileWidth = frm.TileWidth;
                tileHeight = frm.TileHeight;
                addRightMarginForSegaSaturn = frm.AddRightMarginForSegaSaturn;
            }
            if (!tileWidth.HasValue || !tileHeight.HasValue)
                return;
            foreach (Tile tile in this._selectedTiles)
            {
                if (tile.Width < tileWidth.Value || tile.Height < tileHeight.Value)
                    continue;
                this.ExplodeTile(tile, tileWidth.Value, tileHeight.Value, addRightMarginForSegaSaturn);
            }
            this.PushForUndoRedo();
        }

        private void ExplodeTile(Tile src, int tileWidth, int tileHeight, bool addRightMarginForSegaSaturn)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.panelGrid.SuspendLayout();
            int newTileWidth = tileWidth;
            if (addRightMarginForSegaSaturn)
                newTileWidth = ((tileWidth / 8) + 1) * 8;
            try
            {
                using (Bitmap bmp = new Bitmap((int)(src.Width / this._zoom), (int)(src.Height / this._zoom)))
                {
                    this.DrawControl(src, bmp, (int)(src.Left / this._zoom), (int)(src.Top / this._zoom));
                    Color defaultColor = bmp.ContainsColor(Color.Transparent) ? Color.Transparent : Editor.TransparentColor;
                    this.panelGrid.Controls.Remove(src);
                    int newX = 0;
                    for (int x = 0; x + tileWidth <= bmp.Width; x += tileWidth, newX += newTileWidth)
                    {
                        for (int y = 0; y + tileHeight <= bmp.Height; y += tileHeight)
                        {
                            Bitmap img = null;
                            if (addRightMarginForSegaSaturn)
                            {
                                img = new Bitmap(newTileWidth, tileHeight, bmp.PixelFormat);
                                using (var graphics = Graphics.FromImage(img))
                                {
                                    graphics.Clear(defaultColor);
                                    graphics.DrawImage(bmp, new Rectangle(0, 0, tileWidth, tileHeight), new Rectangle(x, y, tileWidth, tileHeight), GraphicsUnit.Pixel);
                                }
                            }
                            else
                                img = JoMapEditorTools.CropBitmap(bmp, new Rectangle(x, y, tileWidth, tileHeight));
                            if (!img.IsTransparent())
                            {
                                Sprite sprite = new Sprite
                                {
                                    Img = img,
                                    Path = null
                                };
                                this.AddTile(sprite, new Point(src.Left + (int)(newX * this._zoom), src.Top + (int)(y * this._zoom)), src.Attribute);
                            }
                        }
                    }
                }
            }
            finally
            {
                this.ResumeLayout();
                Cursor.Current = Cursors.Default;
            }
        }

    }
}