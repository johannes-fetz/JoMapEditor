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
using System.IO;
using System.Windows.Forms;
using JoMapEditor.SegaSaturn;

namespace JoMapEditor
{
    public partial class ObjImportForm : Form
    {
        public ObjImportForm()
        {
            InitializeComponent();
        }

        private void openSourcePathButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = false;
                dlg.Filter = "Wavefront object file (OBJ)|*.obj";
                dlg.CheckFileExists = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                this.sourcePathTextBox.Text = dlg.FileName;
            }
        }

        private void openDestinationPathButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                if (!String.IsNullOrWhiteSpace(this.sourcePathTextBox.Text))
                    saveDialog.FileName = String.Format("{0}.h", Path.GetFileNameWithoutExtension(this.sourcePathTextBox.Text));
                saveDialog.Filter = "C header file|*.h";
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;
                this.destinationPathTextBox.Text = saveDialog.FileName;
            }
        }

        private void SourceOrDestinationTextChanged(object sender, EventArgs e)
        {
            this.convertButton.Enabled = !String.IsNullOrWhiteSpace(this.sourcePathTextBox.Text) && !String.IsNullOrWhiteSpace(this.destinationPathTextBox.Text);
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            Obj.Option option = new Obj.Option
            {
                UseLight = this.useLightCheckBox.Checked,
                UseScreenDoors = this.screenDoorsCheckBox.Checked,
                WorkingDir = Path.GetDirectoryName(this.sourcePathTextBox.Text),
                UseTexture = this.textureCheckBox.Checked,
                ZoomFactor = this.zoomFactorNumericUpDown.Value,
                DualPlane = this.dualPlaneCheckBox.Checked
            };
            if (!File.Exists(this.sourcePathTextBox.Text))
            {
                MessageBox.Show(@"Source file doesn't exists", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Obj obj = new Obj();
                    List<SegaSaturnPolygonData> list = obj.Parse(this.sourcePathTextBox.Text, option);
                    string output = Obj.ToSourceFile(this.destinationPathTextBox.Text, list);
                    File.WriteAllText(this.destinationPathTextBox.Text, output);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                MessageBox.Show("OK", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Program.Logger.Error(String.Format("Unsupported file : {0}", ex.Message), ex);
                MessageBox.Show(String.Format("Unsupported file : {0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
