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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using JoMapEditor.SegaSaturn;
using Paloma;
using Silver.UI;
using WeifenLuo.WinFormsUI.Docking;

namespace JoMapEditor
{
    public partial class SpriteToolBox : DockContent
    {
        public SpriteToolBox()
        {
            InitializeComponent();
            this.HideOnClose = false;
            this.JoEngineCompatibilityCcheckBox.Checked = Editor.Config.Read("user", "only_show_jo_engine_compatible_sprites") == "1";
        }

        private void PopulateFolder(string dir, IEnumerable<string> images)
        {
            int idx = this.toolBox1.AddTab(new DirectoryInfo(dir).Name);
            this.toolBox1[idx].View = ViewMode.LargeIcons;
            this.toolBox1[idx].ToolTip = dir;
            foreach (string path in images)
            {
                Bitmap img = null;
                if (path.ToLower().EndsWith("tga"))
                    img = TargaImage.LoadTargaImage(path);
                else
                    img = new Bitmap(path);
                img.ReplaceColor(Editor.TransparentColor, Color.Transparent);
                Sprite.Filename2path[Path.GetFileName(path)] = path;
                this.toolBox1.LargeImageList.Images.Add(img);
                this.toolBox1[idx].SelectedItemIndex = 0;
                int item = this.toolBox1[idx].AddItem(Path.GetFileNameWithoutExtension(path), 0, this.toolBox1.LargeImageList.Images.Count - 1);
                this.toolBox1[idx][item].ToolTip = String.Format("{2} {0} x {1}", img.Width, img.Height, Path.GetFileName(path));
                this.toolBox1[idx][item].Object = new Sprite
                {
                    Path = path,
                    Img = img
                };
            }
        }

        private static readonly string[] allExtensions = { ".jpg", ".png", ".tga", ".bmp", "gif" };
        private static readonly string[] JoEngineExtensions = { ".tga" };

        public bool Populate(string dirpath)
        {
            this.toolBox1.SuspendLayout();
            this.toolBox1.DeleteAllTabs();
            this.toolBox1.LargeImageList = new ImageList();
            this.toolBox1.LargeImageList.ImageSize = new Size(64, 64);
            if (Directory.Exists(dirpath))
            {
                string lastDirName = null;
                List<string> dirFiles = new List<string>();
                foreach (string file in Directory.EnumerateFiles(dirpath, "*.*", SearchOption.AllDirectories).Where(s => (this.JoEngineCompatibilityCcheckBox.Checked ? SpriteToolBox.JoEngineExtensions : SpriteToolBox.allExtensions).Any(ext => ext == Path.GetExtension(s).ToLower())))
                {
                    string dirname = Path.GetDirectoryName(file);
                    if (lastDirName == null)
                        lastDirName = dirname;
                    if (lastDirName != dirname)
                    {
                        this.PopulateFolder(lastDirName, dirFiles);
                        lastDirName = dirname;
                        dirFiles = new List<string>();
                    }
                    dirFiles.Add(file);
                }
                if (dirFiles.Count > 0)
                {
                    this.PopulateFolder(lastDirName, dirFiles);
                    return true;
                }
            }
            this.toolBox1.ResumeLayout();
            return false;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                return;
            Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(sprite.Path), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("Sprite{0}.h", spriteName);
                dlg.Filter = "C header file|*.h";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, SegaSaturnConverter.ToSourceFile(new SegaSaturnTexture
                    {
                        Image = sprite.Img,
                        Name = spriteName
                    }, true));
                    MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                return;
            Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;

            FileInfo info = new FileInfo(sprite.Path);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Name: {0}", info.Name));
            sb.AppendLine(String.Format("Extension: {0}", info.Extension));
            sb.AppendLine(String.Format("Directory: {0}", info.DirectoryName));
            sb.AppendLine(String.Format("Width: {0} pixels", sprite.Img.Width));
            sb.AppendLine(String.Format("Height: {0} pixels", sprite.Img.Height));
            sb.AppendLine(String.Format("Size: {0} bytes", info.Length));
            sb.AppendLine(String.Format("Last write: {0}", info.LastWriteTime));
            sb.AppendLine(String.Format("Last access: {0}", info.LastAccessTime));
            sb.AppendLine(String.Format("Is readonly: {0}", info.IsReadOnly));

            MessageBox.Show(sb.ToString(), Path.GetFileName(sprite.Path), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportAsTGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                return;
            Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(sprite.Path), String.Empty);
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.TGA", spriteName);
                dlg.DefaultExt = ".TGA";
                dlg.Filter = "TGA|*.TGA";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (Bitmap bitmap = new Bitmap(sprite.Img))
                {
                    TgaWriter.WriteTga24Bits(bitmap, dlg.FileName);
                }
                MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void JoEngineCompatibilityCcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SuspendLayout();
            Editor.Config.Write("user", "only_show_jo_engine_compatible_sprites", this.JoEngineCompatibilityCcheckBox.Checked ? "1" : "0");
            this.Populate(Editor.Config.Read("directory", "sprites"));
            this.ResumeLayout();
            Cursor.Current = Cursors.Default;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                    return;
                Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "edit";
                startInfo.FileName = sprite.Path;
                Process proc = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.Logger.Error(ex.Message, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolBox1_ItemSelectionChanged(ToolBoxItem sender, EventArgs e)
        {
            this.SpriteContextMenuStrip.Enabled = this.toolBox1.SelectedTab != null && this.toolBox1.SelectedTab.SelectedItem != null && this.toolBox1.SelectedTab.SelectedItem.Object is Sprite;
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                    return;
                Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.Verb = "open";
                startInfo.FileName = Path.GetDirectoryName(sprite.Path);
                Process proc = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.Logger.Error(ex.Message, ex);
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportAsJoEngineBinFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
            !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                        return;
            Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(sprite.Path), String.Empty).ToUpper();
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.BIN", spriteName);
                dlg.DefaultExt = ".BIN";
                dlg.Filter = "Jo Engine binary file|*.BIN";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (Bitmap bitmap = new Bitmap(sprite.Img))
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

        private void exportAsRaw15BitsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.toolBox1.SelectedTab == null || this.toolBox1.SelectedTab.SelectedItem == null ||
                       !(this.toolBox1.SelectedTab.SelectedItem.Object is Sprite))
                return;
            Sprite sprite = (Sprite)this.toolBox1.SelectedTab.SelectedItem.Object;
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string spriteName = rgx.Replace(Path.GetFileNameWithoutExtension(sprite.Path), String.Empty).ToUpper();
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.FileName = String.Format("{0}.RAW", spriteName);
                dlg.DefaultExt = ".RAW";
                dlg.Filter = "15 bits raw image|*.RAW";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (Bitmap bitmap = new Bitmap(sprite.Img))
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

        private void SpriteToolBox_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
