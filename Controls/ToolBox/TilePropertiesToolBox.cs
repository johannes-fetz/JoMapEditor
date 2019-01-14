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
using WeifenLuo.WinFormsUI.Docking;

namespace JoMapEditor
{
    public partial class TilePropertiesToolBox : DockContent
    {
        public TilePropertiesToolBox()
        {
            InitializeComponent();
            this.TileAttributeNumericUpDown.TextChanged += this.TileAttributeNumericUpDown_TextChanged;
        }

        public MapViewer CurrentMapViewer
        {
            get
            {
                return this.currentMapViewer;
            }
            set
            {
                if (this.currentMapViewer != value)
                    this.currentMapViewer = value;
            }
        }

        public MapViewer.EditorControl CurrentEditorControl
        {
            get
            {
                return this.currentEditorControl;
            }
            set
            {
                if (this.currentEditorControl != value)
                    this.currentEditorControl = value;
            }
        }

        public Tile CurrrentTile
        {
            get
            {
                return this.currentTile;
            }
            set
            {
                if (this.currentTile != value)
                {
                    this.currentTile = value;
                    this.UpdateGUI();
                }
            }
        }

        public void UpdateGUI()
        {
            if (this.CurrrentTile == null || this.CurrentEditorControl == null || this.CurrentMapViewer == null)
            {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = false;

                this.LocationX.Text = String.Empty;
                this.LocationY.Text = String.Empty;
                this.WidthTextBox.Text = String.Empty;
                this.HeightTextBox.Text = String.Empty;
            }
            else
            {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = true;
                this.groupBox3.Enabled = true;

                int x = this.CurrentEditorControl.HorizontalScroll.Value + this.currentTile.Left;
                this.LocationX.Text = ((int)(x * this.CurrentMapViewer._zoomFactor)).ToString();
                this.WidthTextBox.Text = ((int)(this.CurrrentTile.Width * this.CurrentMapViewer._zoomFactor)).ToString();

                int y = this.CurrentEditorControl.VerticalScroll.Value + this.currentTile.Top;
                this.LocationY.Text = ((int)(y * this.CurrentMapViewer._zoomFactor)).ToString();
                this.HeightTextBox.Text = ((int)(this.CurrrentTile.Height * this.CurrentMapViewer._zoomFactor)).ToString();

                if (!this.CurrrentTile.Attribute.HasValue)
                    this.radioButton1.Checked = true;
                else
                {
                    if (this.CurrrentTile.Attribute.Value >= this.TileAttributeNumericUpDown.Minimum && this.CurrrentTile.Attribute.Value <= this.TileAttributeNumericUpDown.Maximum)
                        this.TileAttributeNumericUpDown.Value = this.CurrrentTile.Attribute.Value;
                    else
                        this.TileAttributeNumericUpDown.Value = this.TileAttributeNumericUpDown.Minimum;
                    this.radioButton2.Checked = true;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.TileAttributeNumericUpDown.Enabled = this.radioButton2.Checked;
            if (this.CurrrentTile != null)
            {
                if (this.radioButton1.Checked)
                    this.CurrrentTile.Attribute = null;
                else
                    this.CurrrentTile.Attribute = (int)this.TileAttributeNumericUpDown.Value;
            }
        }

        private void TileAttributeNumericUpDown_TextChanged(object sender, EventArgs e)
        {
            if (this.CurrrentTile != null && this.radioButton2.Checked)
                this.CurrrentTile.Attribute = (int)this.TileAttributeNumericUpDown.Value;
        }

        private void TileGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Tile.ShowGrid = this.TileGridCheckBox.Checked;
            foreach (MapViewer viewer in Editor.Instance._documents)
            {
                viewer.panelGrid.SuspendLayout();
                viewer.panelGrid.Refresh();
                viewer.panelGrid.ResumeLayout();
            }
        }

        private Tile currentTile;
        private MapViewer.EditorControl currentEditorControl;
        private MapViewer currentMapViewer;
    }
}
