using System.ComponentModel;
using System.Windows.Forms;

namespace JoMapEditor
{
    partial class TileSizeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileSizeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.TileWidthNnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.TileHeightNnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.widthWarningSegaSaturnLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.resolveTileSizeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TileWidthNnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeightNnumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tile width :";
            // 
            // TileWidthNnumericUpDown
            // 
            this.TileWidthNnumericUpDown.Location = new System.Drawing.Point(80, 6);
            this.TileWidthNnumericUpDown.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.TileWidthNnumericUpDown.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.TileWidthNnumericUpDown.Name = "TileWidthNnumericUpDown";
            this.TileWidthNnumericUpDown.Size = new System.Drawing.Size(120, 22);
            this.TileWidthNnumericUpDown.TabIndex = 1;
            this.TileWidthNnumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.TileWidthNnumericUpDown.ValueChanged += new System.EventHandler(this.TileWidthNnumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tile height :";
            // 
            // TileHeightNnumericUpDown
            // 
            this.TileHeightNnumericUpDown.Location = new System.Drawing.Point(80, 38);
            this.TileHeightNnumericUpDown.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.TileHeightNnumericUpDown.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.TileHeightNnumericUpDown.Name = "TileHeightNnumericUpDown";
            this.TileHeightNnumericUpDown.Size = new System.Drawing.Size(120, 22);
            this.TileHeightNnumericUpDown.TabIndex = 3;
            this.TileHeightNnumericUpDown.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Image = global::JoMapEditor.Properties.Resources.explode;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(6, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Explode image in tile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // widthWarningSegaSaturnLabel
            // 
            this.widthWarningSegaSaturnLabel.AutoSize = true;
            this.widthWarningSegaSaturnLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.widthWarningSegaSaturnLabel.Location = new System.Drawing.Point(206, 8);
            this.widthWarningSegaSaturnLabel.Name = "widthWarningSegaSaturnLabel";
            this.widthWarningSegaSaturnLabel.Size = new System.Drawing.Size(299, 16);
            this.widthWarningSegaSaturnLabel.TabIndex = 5;
            this.widthWarningSegaSaturnLabel.Text = "▲ Sprite width not supported by the Sega Saturn ▲";
            this.toolTip1.SetToolTip(this.widthWarningSegaSaturnLabel, "Width must be a multiple of 8");
            this.widthWarningSegaSaturnLabel.Visible = false;
            // 
            // resolveTileSizeCheckBox
            // 
            this.resolveTileSizeCheckBox.AutoSize = true;
            this.resolveTileSizeCheckBox.ForeColor = System.Drawing.Color.Firebrick;
            this.resolveTileSizeCheckBox.Location = new System.Drawing.Point(219, 27);
            this.resolveTileSizeCheckBox.Name = "resolveTileSizeCheckBox";
            this.resolveTileSizeCheckBox.Size = new System.Drawing.Size(298, 20);
            this.resolveTileSizeCheckBox.TabIndex = 6;
            this.resolveTileSizeCheckBox.Text = "Let Jo map Editor solve this problem for you ☺";
            this.resolveTileSizeCheckBox.UseVisualStyleBackColor = true;
            this.resolveTileSizeCheckBox.Visible = false;
            // 
            // TileSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 101);
            this.Controls.Add(this.resolveTileSizeCheckBox);
            this.Controls.Add(this.widthWarningSegaSaturnLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TileHeightNnumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TileWidthNnumericUpDown);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "TileSizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tile size";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.TileWidthNnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeightNnumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private NumericUpDown TileWidthNnumericUpDown;
        private Label label2;
        private NumericUpDown TileHeightNnumericUpDown;
        private Button button1;
        private Label widthWarningSegaSaturnLabel;
        private ToolTip toolTip1;
        private CheckBox resolveTileSizeCheckBox;
    }
}