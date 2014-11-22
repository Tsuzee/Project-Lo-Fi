using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExternalTool
{
    public partial class CharacterCreator : Form
    {
        private int CONST_POINTS = 15; // constant variablue for points.

        private static int points = 15; // points variable shared between numericupDown control
        private static int charCount = 0; // used to control and count characters 

        private int charactersCount = 0; // used to count  and set images.
        private int controlVariable = 0; // variable used to control limits of the numericUpDown Control. 
        private string data; // variable used to write data.

        Image[] characterImages = new Image[3]; // store characters texture
        string[] dataForChar = new string[3];  // store all data for characters
        bool[] saved = new bool[3]; // used to check if character was saved or not after next button pressed.
        

        /// <summary>
        /// Default constructor
        /// </summary>
        public CharacterCreator()
        {
            InitializeComponent();
            points = Int32.Parse(PointsLabel.Text);
            SaveDataButton.Visible = false;
            SaveDataButton.Enabled = false;
            PreviousMember.Visible = false;
            PreviousMember.Enabled = false;
            
            //adding images of the characters
            for (int i = 0; i < saved.Length; i++)
            {
                saved[i] = false;
            }
            characterImages[0] = ExternalTool.Properties.Resources.indianajones;
            characterImages[1] = ExternalTool.Properties.Resources.grannyweatherwax;
            characterImages[2] = ExternalTool.Properties.Resources.tremel;
        }

        /// <summary>
        /// Strength numericUpDown control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrengthStat_ValueChanged(object sender, EventArgs e)
        {
                  //Update maximum limits for dexterity and magic numericUpDown controls
                    DexterityStat.Maximum = CONST_POINTS - ((int)StrengthStat.Value + (int)MagicStat.Value); 
                    MagicStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)StrengthStat.Value);
                
                  //controlVariable used to see how many points where used
                    controlVariable = (int)StrengthStat.Value + (int)DexterityStat.Value + (int)MagicStat.Value;

                  //adjust available points
                    points = CONST_POINTS - controlVariable;
                    PointsLabel.Text = points.ToString();

                  // Set the new maximum for Strength numericUpDown control if no available points left
                    if (points < 0)
                    {
                        StrengthStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)MagicStat.Value);
                    }

        }
        
        /// <summary>
        /// Dexterity numericUpDown control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DexterityStat_ValueChanged(object sender, EventArgs e)
        {
                  //Update maximum limits for Strength and Magic numericUpDown controls
                    StrengthStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)MagicStat.Value);
                    MagicStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)StrengthStat.Value);

                  //controlVariable used to see how many points where used
                    controlVariable = (int)StrengthStat.Value + (int)DexterityStat.Value + (int)MagicStat.Value;

                  //adjust available points 
                    points = CONST_POINTS - controlVariable;
                    PointsLabel.Text = points.ToString();

                    // Set the new maximum for dexterity numericUpDown control if no available points left
                    if (points < 0)
                    {
                        DexterityStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)MagicStat.Value);
                    }
        }

        /// <summary>
        /// Magic numericUpDown control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MagicStat_ValueChanged(object sender, EventArgs e)
        {
                  //Update maximum limits for Strength and Dexterity numericUpDown controls
                    StrengthStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)MagicStat.Value);
                    DexterityStat.Maximum = CONST_POINTS - ((int)StrengthStat.Value + (int)MagicStat.Value);

                   //controlVariable used to see how many points where used
                    controlVariable = (int)StrengthStat.Value + (int)DexterityStat.Value + (int)MagicStat.Value;

                    //adjust available points 
                    points = CONST_POINTS - controlVariable;
                    PointsLabel.Text = points.ToString();

                   // Set the new maximum for Magic numericUpDown control if no available points left       
                    if (points < 0)
                    {
                        MagicStat.Maximum = CONST_POINTS - ((int)DexterityStat.Value + (int)StrengthStat.Value);
                    }
             
        }

        /// <summary>
        /// Next Character Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMemeber_Click(object sender, EventArgs e)
        {
            if (UnitName.Text.Trim().Length != 0 && points == 0)
            {
                //if this is the first character, disable previous button
                if (charCount == 0)
                {
                    PreviousMember.Enabled = true;
                    PreviousMember.Visible = true;

                }

                //check if character was previously saved.
                if (saved[charCount] == false)
                {

                    //save all the data from the form to a data string. Add string to a string array. Move to the next character
                    if (charCount < 2)
                    {

                        data = StrengthStat.Value + "," + DexterityStat.Value + "," + MagicStat.Value + "," + UnitName.Text + "," + charactersCount;
                        dataForChar[charCount] = data;
                        saved[charCount] = true;
                        charCount++;

                        // Clear all the fields for the next character
                        clear();

                        //Since we have 3 characters, when charCount gets to the last one, disable nextCharacter button, and enable save data button
                        if (charCount == 2)
                        {

                            NextMemeber.Enabled = false;
                            NextMemeber.Visible = false;

                            SaveDataButton.Visible = true;
                            SaveDataButton.Enabled = true;
                        }

                    }
                }

                // If data for characters was previously saved, load the data.
                else
                {
                    load(charCount + 1);
                    charCount++;
                    if (charCount == 2)
                    {

                        NextMemeber.Enabled = false;
                        NextMemeber.Visible = false;

                        SaveDataButton.Visible = true;
                        SaveDataButton.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter character name and use all available points");
            }

        }

        /// <summary>
        /// Clear all fields.
        /// </summary>
        private void clear()
        {
            UnitName.Text = "";
            StrengthStat.Maximum = 15;
            DexterityStat.Maximum = 15;
            MagicStat.Maximum = 15;
            StrengthStat.Value = 0;
            DexterityStat.Value = 0;
            MagicStat.Value = 0;

        }

        /// <summary>
        /// load data for particular character
        /// </summary>
        /// <param name="countNum"></param>
        private void load(int countNum)
        {
            clear();
            if (dataForChar[countNum] != null)
            {
                string[] getSeparateData = dataForChar[countNum].Split(',');
                StrengthStat.Value = Int32.Parse(getSeparateData[0]);
                DexterityStat.Value = Int32.Parse(getSeparateData[1]);
                MagicStat.Value = Int32.Parse(getSeparateData[2]);
                UnitName.Text = getSeparateData[3];
                charactersPictureBox.Image = characterImages[Int32.Parse(getSeparateData[4])];
            }
            else
            {
                clear();
            }

        }


        /// <summary>
        /// Save all data to the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDataButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog safeWindow = new SaveFileDialog();
            safeWindow.Filter = "Lo-Fi Project|*.LFP"; // file extension
            safeWindow.FileName = "Characters"; // default file name
            safeWindow.Title = "Save Lo-Fi Character File"; // title for the save dialog window

            data = StrengthStat.Value + "," + DexterityStat.Value + "," + MagicStat.Value + "," + UnitName.Text + "," + charactersCount;
            dataForChar[charCount] = data;

            if (safeWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = safeWindow.FileName;
                using (StreamWriter write = new StreamWriter(File.Create(path)))
                {
                    foreach (string s in dataForChar)
                    {
                        write.WriteLine(s);
                    }
                }

                MessageBox.Show("All your data has been saved");
            }
        }

        /// <summary>
        /// Previous character button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousMember_Click(object sender, EventArgs e)
        {
            SaveDataButton.Enabled = false;
            SaveDataButton.Visible = false;
            NextMemeber.Enabled = true;
            NextMemeber.Visible = true;
            if (charCount > 0)
            {
                charCount--;
                load(charCount);
            }
            if (charCount == 0)
            {
                PreviousMember.Visible = false;
                PreviousMember.Enabled = false;
            }

            
        }

        /// <summary>
        /// Change picture of the character. Forward button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPortrait_Click(object sender, EventArgs e)
        {
            if (charactersCount < 2)
            {
                charactersCount++;
                charactersPictureBox.Image = characterImages[charactersCount];
            }
            else
            {
                charactersCount = 0;
                charactersPictureBox.Image = characterImages[charactersCount];
            }
        }

        /// <summary>
        /// Change picture of the character. Previous button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPortrait_Click(object sender, EventArgs e)
        {
            if (charactersCount > 0)
            {
                charactersCount--;
                charactersPictureBox.Image = characterImages[charactersCount];
            }
            else
            {
                charactersCount = 2;
                charactersPictureBox.Image = characterImages[charactersCount];
            }
        }

    }
}
