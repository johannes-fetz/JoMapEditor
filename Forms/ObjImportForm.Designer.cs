using System.ComponentModel;
using System.Windows.Forms;

namespace JoMapEditor
{
    partial class ObjImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjImportForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openSourcePathButton = new System.Windows.Forms.Button();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openDestinationPathButton = new System.Windows.Forms.Button();
            this.destinationPathTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.screenDoorsCheckBox = new System.Windows.Forms.CheckBox();
            this.useLightCheckBox = new System.Windows.Forms.CheckBox();
            this.textureCheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zoomFactorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dualPlaneCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactorNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.openSourcePathButton);
            this.groupBox1.Controls.Add(this.sourcePathTextBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(769, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // openSourcePathButton
            // 
            this.openSourcePathButton.Location = new System.Drawing.Point(706, 20);
            this.openSourcePathButton.Name = "openSourcePathButton";
            this.openSourcePathButton.Size = new System.Drawing.Size(57, 23);
            this.openSourcePathButton.TabIndex = 1;
            this.openSourcePathButton.Text = "...";
            this.openSourcePathButton.UseVisualStyleBackColor = true;
            this.openSourcePathButton.Click += new System.EventHandler(this.openSourcePathButton_Click);
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Location = new System.Drawing.Point(7, 20);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.Size = new System.Drawing.Size(692, 20);
            this.sourcePathTextBox.TabIndex = 0;
            this.sourcePathTextBox.TextChanged += new System.EventHandler(this.SourceOrDestinationTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.openDestinationPathButton);
            this.groupBox2.Controls.Add(this.destinationPathTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // openDestinationPathButton
            // 
            this.openDestinationPathButton.Location = new System.Drawing.Point(706, 20);
            this.openDestinationPathButton.Name = "openDestinationPathButton";
            this.openDestinationPathButton.Size = new System.Drawing.Size(57, 23);
            this.openDestinationPathButton.TabIndex = 1;
            this.openDestinationPathButton.Text = "...";
            this.openDestinationPathButton.UseVisualStyleBackColor = true;
            this.openDestinationPathButton.Click += new System.EventHandler(this.openDestinationPathButton_Click);
            // 
            // destinationPathTextBox
            // 
            this.destinationPathTextBox.Location = new System.Drawing.Point(7, 20);
            this.destinationPathTextBox.Name = "destinationPathTextBox";
            this.destinationPathTextBox.Size = new System.Drawing.Size(692, 20);
            this.destinationPathTextBox.TabIndex = 0;
            this.destinationPathTextBox.TextChanged += new System.EventHandler(this.SourceOrDestinationTextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dualPlaneCheckBox);
            this.groupBox3.Controls.Add(this.zoomFactorNumericUpDown);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.screenDoorsCheckBox);
            this.groupBox3.Controls.Add(this.useLightCheckBox);
            this.groupBox3.Controls.Add(this.textureCheckBox);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(13, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(769, 162);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // screenDoorsCheckBox
            // 
            this.screenDoorsCheckBox.AutoSize = true;
            this.screenDoorsCheckBox.Location = new System.Drawing.Point(7, 66);
            this.screenDoorsCheckBox.Name = "screenDoorsCheckBox";
            this.screenDoorsCheckBox.Size = new System.Drawing.Size(360, 17);
            this.screenDoorsCheckBox.TabIndex = 3;
            this.screenDoorsCheckBox.Text = "Enable screen doors transparency (can be activated later in Jo Engine)";
            this.screenDoorsCheckBox.UseVisualStyleBackColor = true;
            // 
            // useLightCheckBox
            // 
            this.useLightCheckBox.AutoSize = true;
            this.useLightCheckBox.Checked = true;
            this.useLightCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useLightCheckBox.Location = new System.Drawing.Point(7, 43);
            this.useLightCheckBox.Name = "useLightCheckBox";
            this.useLightCheckBox.Size = new System.Drawing.Size(268, 17);
            this.useLightCheckBox.TabIndex = 2;
            this.useLightCheckBox.Text = "Enable lighting (can be activated later in Jo Engine)";
            this.useLightCheckBox.UseVisualStyleBackColor = true;
            // 
            // textureCheckBox
            // 
            this.textureCheckBox.AutoSize = true;
            this.textureCheckBox.Location = new System.Drawing.Point(7, 20);
            this.textureCheckBox.Name = "textureCheckBox";
            this.textureCheckBox.Size = new System.Drawing.Size(350, 17);
            this.textureCheckBox.TabIndex = 1;
            this.textureCheckBox.Text = "Import textures if available (▲ texture mapping use a lot of memory ▲)";
            this.textureCheckBox.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::JoMapEditor.Properties.Resources.wavefront;
            this.pictureBox1.Location = new System.Drawing.Point(439, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(324, 118);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // convertButton
            // 
            this.convertButton.Enabled = false;
            this.convertButton.Location = new System.Drawing.Point(706, 289);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 3;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Image = global::JoMapEditor.Properties.Resources.info;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(11, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "This tool is still in development. Please email me, if you find a bug :)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zoom factor :";
            // 
            // zoomFactorNumericUpDown
            // 
            this.zoomFactorNumericUpDown.DecimalPlaces = 2;
            this.zoomFactorNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zoomFactorNumericUpDown.Location = new System.Drawing.Point(85, 136);
            this.zoomFactorNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.zoomFactorNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zoomFactorNumericUpDown.Name = "zoomFactorNumericUpDown";
            this.zoomFactorNumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.zoomFactorNumericUpDown.TabIndex = 5;
            this.zoomFactorNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dualPlaneCheckBox
            // 
            this.dualPlaneCheckBox.AutoSize = true;
            this.dualPlaneCheckBox.Checked = true;
            this.dualPlaneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dualPlaneCheckBox.Location = new System.Drawing.Point(7, 89);
            this.dualPlaneCheckBox.Name = "dualPlaneCheckBox";
            this.dualPlaneCheckBox.Size = new System.Drawing.Size(318, 17);
            this.dualPlaneCheckBox.TabIndex = 6;
            this.dualPlaneCheckBox.Text = "Polygons must be visible on both sides (can solve some glitch)";
            this.dualPlaneCheckBox.UseVisualStyleBackColor = true;
            // 
            // ObjImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(794, 324);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ObjImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wavefront to Jo Engine Source File";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactorNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button openSourcePathButton;
        private TextBox sourcePathTextBox;
        private GroupBox groupBox2;
        private Button openDestinationPathButton;
        private TextBox destinationPathTextBox;
        private GroupBox groupBox3;
        private PictureBox pictureBox1;
        private CheckBox textureCheckBox;
        private CheckBox useLightCheckBox;
        private CheckBox screenDoorsCheckBox;
        private Button convertButton;
        private Label label1;
        private Label label2;
        private NumericUpDown zoomFactorNumericUpDown;
        private CheckBox dualPlaneCheckBox;
    }
}