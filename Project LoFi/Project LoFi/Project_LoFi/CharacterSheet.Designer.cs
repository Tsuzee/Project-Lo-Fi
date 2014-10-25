namespace Project_LoFi
{
    partial class CharacterSheet
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
            this.tabCharacterSheet = new System.Windows.Forms.TabControl();
            this.tabPlayer1 = new System.Windows.Forms.TabPage();
            this.tabPlayer2 = new System.Windows.Forms.TabPage();
            this.tabPlayer3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabCharacterSheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCharacterSheet
            // 
            this.tabCharacterSheet.Controls.Add(this.tabPlayer1);
            this.tabCharacterSheet.Controls.Add(this.tabPlayer2);
            this.tabCharacterSheet.Controls.Add(this.tabPlayer3);
            this.tabCharacterSheet.Location = new System.Drawing.Point(12, 12);
            this.tabCharacterSheet.Name = "tabCharacterSheet";
            this.tabCharacterSheet.SelectedIndex = 0;
            this.tabCharacterSheet.Size = new System.Drawing.Size(521, 564);
            this.tabCharacterSheet.TabIndex = 0;
            // 
            // tabPlayer1
            // 
            this.tabPlayer1.Location = new System.Drawing.Point(4, 22);
            this.tabPlayer1.Name = "tabPlayer1";
            this.tabPlayer1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayer1.Size = new System.Drawing.Size(513, 538);
            this.tabPlayer1.TabIndex = 0;
            this.tabPlayer1.Text = "P1 Name Here";
            this.tabPlayer1.UseVisualStyleBackColor = true;
            // 
            // tabPlayer2
            // 
            this.tabPlayer2.Location = new System.Drawing.Point(4, 22);
            this.tabPlayer2.Name = "tabPlayer2";
            this.tabPlayer2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayer2.Size = new System.Drawing.Size(641, 565);
            this.tabPlayer2.TabIndex = 1;
            this.tabPlayer2.Text = "P2 Name Here";
            this.tabPlayer2.UseVisualStyleBackColor = true;
            // 
            // tabPlayer3
            // 
            this.tabPlayer3.Location = new System.Drawing.Point(4, 22);
            this.tabPlayer3.Name = "tabPlayer3";
            this.tabPlayer3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayer3.Size = new System.Drawing.Size(641, 565);
            this.tabPlayer3.TabIndex = 2;
            this.tabPlayer3.Text = "P3 Name Here";
            this.tabPlayer3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(554, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 184);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "P1 Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(554, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(577, 184);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "P2 Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(554, 392);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(577, 184);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "P3 Name";
            // 
            // CharacterSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 591);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabCharacterSheet);
            this.Name = "CharacterSheet";
            this.Text = "Character Sheet";
            this.tabCharacterSheet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCharacterSheet;
        private System.Windows.Forms.TabPage tabPlayer1;
        private System.Windows.Forms.TabPage tabPlayer2;
        private System.Windows.Forms.TabPage tabPlayer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}