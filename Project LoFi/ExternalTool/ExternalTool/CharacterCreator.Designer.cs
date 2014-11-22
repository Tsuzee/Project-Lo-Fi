namespace ExternalTool
{
    partial class CharacterCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.charactersPictureBox = new System.Windows.Forms.PictureBox();
            this.StrengthStat = new System.Windows.Forms.NumericUpDown();
            this.DexterityStat = new System.Windows.Forms.NumericUpDown();
            this.MagicStat = new System.Windows.Forms.NumericUpDown();
            this.UnitName = new System.Windows.Forms.TextBox();
            this.PreviousPortrait = new System.Windows.Forms.Button();
            this.NextPortrait = new System.Windows.Forms.Button();
            this.PreviousMember = new System.Windows.Forms.Button();
            this.NextMemeber = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.StrengthLabel = new System.Windows.Forms.Label();
            this.DexterityLabel = new System.Windows.Forms.Label();
            this.MagicLabel = new System.Windows.Forms.Label();
            this.PointLabel = new System.Windows.Forms.Label();
            this.PointsLabel = new System.Windows.Forms.Label();
            this.SaveDataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.charactersPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrengthStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DexterityStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MagicStat)).BeginInit();
            this.SuspendLayout();
            // 
            // charactersPictureBox
            // 
            this.charactersPictureBox.Image = global::ExternalTool.Properties.Resources.indianajones;
            this.charactersPictureBox.InitialImage = null;
            this.charactersPictureBox.Location = new System.Drawing.Point(47, 30);
            this.charactersPictureBox.Name = "charactersPictureBox";
            this.charactersPictureBox.Size = new System.Drawing.Size(42, 60);
            this.charactersPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.charactersPictureBox.TabIndex = 0;
            this.charactersPictureBox.TabStop = false;
            // 
            // StrengthStat
            // 
            this.StrengthStat.Location = new System.Drawing.Point(266, 125);
            this.StrengthStat.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.StrengthStat.Name = "StrengthStat";
            this.StrengthStat.Size = new System.Drawing.Size(120, 20);
            this.StrengthStat.TabIndex = 3;
            this.StrengthStat.ValueChanged += new System.EventHandler(this.StrengthStat_ValueChanged);
            // 
            // DexterityStat
            // 
            this.DexterityStat.Location = new System.Drawing.Point(266, 172);
            this.DexterityStat.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.DexterityStat.Name = "DexterityStat";
            this.DexterityStat.Size = new System.Drawing.Size(120, 20);
            this.DexterityStat.TabIndex = 4;
            this.DexterityStat.ValueChanged += new System.EventHandler(this.DexterityStat_ValueChanged);
            // 
            // MagicStat
            // 
            this.MagicStat.Location = new System.Drawing.Point(266, 224);
            this.MagicStat.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.MagicStat.Name = "MagicStat";
            this.MagicStat.Size = new System.Drawing.Size(120, 20);
            this.MagicStat.TabIndex = 5;
            this.MagicStat.ValueChanged += new System.EventHandler(this.MagicStat_ValueChanged);
            // 
            // UnitName
            // 
            this.UnitName.Location = new System.Drawing.Point(162, 30);
            this.UnitName.Name = "UnitName";
            this.UnitName.Size = new System.Drawing.Size(225, 20);
            this.UnitName.TabIndex = 0;
            // 
            // PreviousPortrait
            // 
            this.PreviousPortrait.Location = new System.Drawing.Point(7, 115);
            this.PreviousPortrait.Name = "PreviousPortrait";
            this.PreviousPortrait.Size = new System.Drawing.Size(59, 23);
            this.PreviousPortrait.TabIndex = 1;
            this.PreviousPortrait.Text = "<--";
            this.PreviousPortrait.UseVisualStyleBackColor = true;
            this.PreviousPortrait.Click += new System.EventHandler(this.PreviousPortrait_Click);
            // 
            // NextPortrait
            // 
            this.NextPortrait.Location = new System.Drawing.Point(72, 115);
            this.NextPortrait.Name = "NextPortrait";
            this.NextPortrait.Size = new System.Drawing.Size(59, 23);
            this.NextPortrait.TabIndex = 2;
            this.NextPortrait.Text = "-->";
            this.NextPortrait.UseVisualStyleBackColor = true;
            this.NextPortrait.Click += new System.EventHandler(this.NextPortrait_Click);
            // 
            // PreviousMember
            // 
            this.PreviousMember.Location = new System.Drawing.Point(12, 294);
            this.PreviousMember.Name = "PreviousMember";
            this.PreviousMember.Size = new System.Drawing.Size(110, 40);
            this.PreviousMember.TabIndex = 7;
            this.PreviousMember.Text = "Previous Character";
            this.PreviousMember.UseVisualStyleBackColor = true;
            this.PreviousMember.Click += new System.EventHandler(this.PreviousMember_Click);
            // 
            // NextMemeber
            // 
            this.NextMemeber.Location = new System.Drawing.Point(288, 294);
            this.NextMemeber.Name = "NextMemeber";
            this.NextMemeber.Size = new System.Drawing.Size(110, 40);
            this.NextMemeber.TabIndex = 6;
            this.NextMemeber.Text = "Next Character";
            this.NextMemeber.UseVisualStyleBackColor = true;
            this.NextMemeber.Click += new System.EventHandler(this.NextMemeber_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(188, 13);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Name";
            // 
            // StrengthLabel
            // 
            this.StrengthLabel.AutoSize = true;
            this.StrengthLabel.Location = new System.Drawing.Point(165, 125);
            this.StrengthLabel.Name = "StrengthLabel";
            this.StrengthLabel.Size = new System.Drawing.Size(47, 13);
            this.StrengthLabel.TabIndex = 10;
            this.StrengthLabel.Text = "Strength";
            // 
            // DexterityLabel
            // 
            this.DexterityLabel.AutoSize = true;
            this.DexterityLabel.Location = new System.Drawing.Point(165, 172);
            this.DexterityLabel.Name = "DexterityLabel";
            this.DexterityLabel.Size = new System.Drawing.Size(48, 13);
            this.DexterityLabel.TabIndex = 12;
            this.DexterityLabel.Text = "Dexterity";
            // 
            // MagicLabel
            // 
            this.MagicLabel.AutoSize = true;
            this.MagicLabel.Location = new System.Drawing.Point(165, 224);
            this.MagicLabel.Name = "MagicLabel";
            this.MagicLabel.Size = new System.Drawing.Size(36, 13);
            this.MagicLabel.TabIndex = 14;
            this.MagicLabel.Text = "Magic";
            // 
            // PointLabel
            // 
            this.PointLabel.AutoSize = true;
            this.PointLabel.Location = new System.Drawing.Point(165, 70);
            this.PointLabel.Name = "PointLabel";
            this.PointLabel.Size = new System.Drawing.Size(60, 13);
            this.PointLabel.TabIndex = 9;
            this.PointLabel.Text = "Points Left:";
            // 
            // PointsLabel
            // 
            this.PointsLabel.AutoSize = true;
            this.PointsLabel.Location = new System.Drawing.Point(231, 70);
            this.PointsLabel.Name = "PointsLabel";
            this.PointsLabel.Size = new System.Drawing.Size(19, 13);
            this.PointsLabel.TabIndex = 15;
            this.PointsLabel.Text = "15";
            // 
            // SaveDataButton
            // 
            this.SaveDataButton.Location = new System.Drawing.Point(288, 294);
            this.SaveDataButton.Name = "SaveDataButton";
            this.SaveDataButton.Size = new System.Drawing.Size(110, 40);
            this.SaveDataButton.TabIndex = 16;
            this.SaveDataButton.Text = "Save all data";
            this.SaveDataButton.UseVisualStyleBackColor = true;
            this.SaveDataButton.Click += new System.EventHandler(this.SaveDataButton_Click);
            // 
            // CharacterCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 354);
            this.Controls.Add(this.SaveDataButton);
            this.Controls.Add(this.PointsLabel);
            this.Controls.Add(this.PointLabel);
            this.Controls.Add(this.MagicLabel);
            this.Controls.Add(this.DexterityLabel);
            this.Controls.Add(this.StrengthLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NextMemeber);
            this.Controls.Add(this.PreviousMember);
            this.Controls.Add(this.NextPortrait);
            this.Controls.Add(this.PreviousPortrait);
            this.Controls.Add(this.UnitName);
            this.Controls.Add(this.MagicStat);
            this.Controls.Add(this.DexterityStat);
            this.Controls.Add(this.StrengthStat);
            this.Controls.Add(this.charactersPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CharacterCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CharacterCreator";
            ((System.ComponentModel.ISupportInitialize)(this.charactersPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrengthStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DexterityStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MagicStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox charactersPictureBox;
        private System.Windows.Forms.NumericUpDown StrengthStat;
        private System.Windows.Forms.NumericUpDown DexterityStat;
        private System.Windows.Forms.NumericUpDown MagicStat;
        private System.Windows.Forms.TextBox UnitName;
        private System.Windows.Forms.Button PreviousPortrait;
        private System.Windows.Forms.Button NextPortrait;
        private System.Windows.Forms.Button PreviousMember;
        private System.Windows.Forms.Button NextMemeber;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label StrengthLabel;
        private System.Windows.Forms.Label DexterityLabel;
        private System.Windows.Forms.Label MagicLabel;
        private System.Windows.Forms.Label PointLabel;
        private System.Windows.Forms.Label PointsLabel;
        private System.Windows.Forms.Button SaveDataButton;
    }
}

