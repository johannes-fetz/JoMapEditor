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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Paloma;

namespace JoMapEditor
{
    public partial class MapViewer
    {
        public bool OpenFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show(String.Format("File: {0} not found.", path), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.MapPath = path;
                using (StreamReader fs = new StreamReader(path))
                {
                    this.panelGrid.Controls.Clear();
                    while (!fs.EndOfStream)
                    {
                        string line = fs.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        string[] tab = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        Sprite sprite = new Sprite();
                        if (!Sprite.Filename2path.ContainsKey(tab[0]))
                        {
                            this.panelGrid.Controls.Clear();
                            this.UpdateStatusbar();
                            MessageBox.Show(String.Format("Sprite {0} is not found in available sprites", tab[0]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return (false);
                        }
                        sprite.Path = Sprite.Filename2path[tab[0]];
                        if (sprite.Path.ToLower().EndsWith("tga"))
                            sprite.Img = TargaImage.LoadTargaImage(sprite.Path);
                        else
                            sprite.Img = new Bitmap(sprite.Path);
                        sprite.Img.ReplaceColor(Editor.TransparentColor, Color.Transparent);

                        this.AddTile(sprite, new Point(Convert.ToInt32(tab[1]), Convert.ToInt32(tab[2])), tab.Length > 3 ? (int?)Convert.ToInt32(tab[3]) : null);
                    }
                    this.PushForUndoRedo();
                    this.UpdateStatusbar();

                    /*this.panelGrid.Controls.Add(new Panel
                    {
                        Width = 1,
                        Height = 1,
                        BackColor = Color.Transparent,
                        Left = 5000,
                        Top = 5000,
                    });*/


                    return (true);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public bool OpenFile()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = false;
                dlg.Filter = "MAP|*.MAP";
                dlg.CheckFileExists = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                    return this.OpenFile(dlg.FileName);
            }
            return (false);
        }

        private bool SaveMemorySpriteOnDisk()
        {
            List<Sprite> toSave = new List<Sprite>();
            string folder = null;
            foreach (Control tile in this.panelGrid.Controls)
            {
                if (tile is Tile)
                {
                    if (String.IsNullOrEmpty(((Tile)tile).Sprite.Path))
                        toSave.Add(((Tile)tile).Sprite);
                    else if (folder == null)
                        folder = Path.GetDirectoryName(((Tile)tile).Sprite.Path);
                }
            }
            if (toSave.Count <= 0)
                return true;
            if (MessageBox.Show("Some tiles image are not saved. Would you like to save them (Mandatory to save MAP) ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (folder != null)
                    dialog.SelectedPath = folder;
                dialog.ShowNewFolderButton = true;
                dialog.Description = "Please select the forder where tiles will be saved";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return false;
                int id = 0;
                bool overrideImage = false;
                foreach (Sprite sprite in toSave)
                {
                    string path = Path.Combine(dialog.SelectedPath, String.Format("T{0}.TGA", id));
                    if (File.Exists(path))
                    {
                        if (!overrideImage)
                        {
                            if (MessageBox.Show("Some image already exists. Do you want to override them ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return false;
                            overrideImage = true;
                        }
                        File.Delete(path);
                    }
                    using (Bitmap bitmap = new Bitmap(sprite.Img))
                    {
                        TgaWriter.WriteTga24Bits(bitmap, path);
                    }
                    ++id;
                    sprite.Path = path;
                }
            }
            return true;
        }

        public bool SaveAsFile()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(this.MapPath) || !File.Exists(this.MapPath))
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        Regex rgx = new Regex("[^a-zA-Z0-9]");
                        dlg.FileName = String.Format("{0}.MAP", rgx.Replace(Path.GetFileNameWithoutExtension(this.Text), String.Empty));
                        dlg.DefaultExt = ".MAP";
                        dlg.Filter = "MAP|*.MAP";
                        if (dlg.ShowDialog() != DialogResult.OK)
                            return false;
                        this.MapPath = dlg.FileName;
                        this.Text = dlg.FileName;
                    }
                }
                if (!this.SaveMemorySpriteOnDisk())
                    return false;
                StringBuilder sb = new StringBuilder();
                foreach (Tile tile in this.panelGrid.Controls.Cast<Tile>().OrderBy(item => item.Left).ThenBy(item => item.Top))
                {
                    int x = this.panelGrid.HorizontalScroll.Value + (int)(tile.Left * this._zoomFactor);
                    int y = this.panelGrid.VerticalScroll.Value + (int)(tile.Top * this._zoomFactor);

                    if (tile.Attribute.HasValue)
                        sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}", Path.GetFileName(tile.Sprite.Path), x, y, tile.Attribute.Value));
                    else
                        sb.AppendLine(String.Format("{0}\t{1}\t{2}", Path.GetFileName(tile.Sprite.Path), x, y));
                }
                File.WriteAllText(this.MapPath, sb.ToString());
                this.MarkFileAsNormal();
                return (true);
            }
            catch (Exception ex)
            {
                Program.Logger.Error(ex.Message, ex);
                return (false);
            }
        }
    }
}