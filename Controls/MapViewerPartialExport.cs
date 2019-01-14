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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using JoMapEditor.SegaSaturn;

namespace JoMapEditor
{
    public partial class MapViewer
    {
        public void ExportAs15BitsRawImage(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty).ToUpper();
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.RAW", spriteName);
                dlg.Filter = "15 bits raw image|*.RAW";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bitmap = this.GetBitmap(onlySelected))
                    {
                        using (FileStream stream = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            SegaSaturnConverter.ToBinFile(new SegaSaturnTexture
                            {
                                Image = bitmap
                            }, stream, false);
                        }
                    }
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ExportAsBin(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty).ToUpper();
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.BIN", spriteName);
                dlg.Filter = "Jo Engine binary file|*.BIN";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bitmap = this.GetBitmap(onlySelected))
                    {
                        using (FileStream stream = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            SegaSaturnConverter.ToBinFile(new SegaSaturnTexture
                            {
                                Image = bitmap
                            }, stream, true);
                        }
                    }
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ExportAsJoImageTileset(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string tilesetName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("Tileset{0}.h", tilesetName);
                dlg.Filter = "C header file|*.h";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    List<Tile> tiles = onlySelected ? this._selectedTiles : this.panelGrid.Controls.Cast<Control>().Where(item => item is Tile).Cast<Tile>().ToList();
                    List<Rectangle> toSave = new List<Rectangle>();
                    int xOrigin = tiles.Min(tile => (int)(tile.Left * this._zoomFactor));
                    int yOrigin = tiles.Min(tile => (int)(tile.Top * this._zoomFactor));
                    foreach (Tile tile in tiles.OrderBy(item => item.Left).ThenBy(item => item.Top))
                    {
                        int x = this.panelGrid.HorizontalScroll.Value + (int)(tile.Left * this._zoomFactor) - xOrigin;
                        int y = this.panelGrid.VerticalScroll.Value + (int)(tile.Top * this._zoomFactor) - yOrigin;
                        int width = (int)(tile.Width * this._zoomFactor);
                        int height = (int)(tile.Height * this._zoomFactor);
                        toSave.Add(new Rectangle(x, y, width, height));
                    }
                    File.WriteAllText(dlg.FileName, SegaSaturnConverter.ToSourceFile(toSave, tilesetName, true));
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ExportAsC(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("Sprite{0}.h", spriteName);
                dlg.Filter = "C header file|*.h";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bitmap = this.GetBitmap(onlySelected))
                    {
                        File.WriteAllText(dlg.FileName, SegaSaturnConverter.ToSourceFile(new SegaSaturnTexture
                        {
                            Image = bitmap,
                            Name = spriteName
                        }, true));
                    }
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ExportAsPng(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.png", spriteName);
                dlg.DefaultExt = ".png";
                dlg.Filter = "PNG|*.png";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (Bitmap bitmap = this.GetBitmap(onlySelected))
                {
                    bitmap.Save(dlg.FileName, ImageFormat.Png);
                }
                MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ExportAsTga(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.TGA", spriteName);
                dlg.DefaultExt = ".TGA";
                dlg.Filter = "TGA|*.TGA";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (Bitmap bitmap = this.GetBitmap(onlySelected))
                {
                    TgaWriter.WriteTga24Bits(bitmap, dlg.FileName);
                }
                MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ExportAsSegaSaturnGraphic(bool onlySelected = false)
        {
            if (this.Empty)
                return;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("SegaSaturnGraphic{0}.h", spriteName);
                dlg.Filter = "C header file|*.h";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bitmap = this.GetBitmap(onlySelected))
                    {
                        File.WriteAllText(dlg.FileName, SegaSaturnConverter.ToSourceFile(new SegaSaturnGraphic
                        {
                            Image = bitmap,
                            Name = spriteName
                        }, true));
                    }
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}