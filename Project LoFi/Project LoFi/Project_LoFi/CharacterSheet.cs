using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_LoFi
{
    public partial class CharacterSheet : Form
    {
        public bool open;

        public CharacterSheet()
        {
            InitializeComponent();
            open = false;
        }

        private void CharacterSheet_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            open = false;
        }


        //return whether the form is open or not
        public bool IsOpen()
        {
            if(open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CharacterSheet_Load_1(object sender, EventArgs e)
        {

        }

        public void UpdateCharSheet(List<PlayerUnit> charList)
        {
            //Player 1
            if (charList[0] != null)
            {
                P1groupBox.Text = charList[0].Name.ToString();
                //P1pictureBox.Image = charList[0].Img;
                P1LevelLabel.Text = charList[0].Level.ToString();
                P1HealthLabel.Text = charList[0].Health.ToString();
                P1StrengthLabel.Text = charList[0].Strength.ToString();
                P1DexterityLabel.Text = charList[0].Dexterity.ToString();
                P1MagicLabel.Text = charList[0].Magic.ToString();
                P1AttModLabel.Text = charList[0].AttackModifier.ToString();
                P1DefModLabel.Text = charList[0].DefenseModifier.ToString();
                P1CritChanceLabel.Text = charList[0].CritChance.ToString();
                //P1WeaponLabel.Text = charList[0].EquippedWeapon.ToString();
                //P1ArmorLabel.Text = charList[0].EquippedArmor.ToString();
            }

            //player 2
            if (charList[1] != null)
            {
                P2groupBox.Text = charList[1].Name.ToString();
                //P2pictureBox.Image = charList[1].Img;
                P2LevelLabel.Text = charList[1].Level.ToString();
                P2HealthLabel.Text = charList[1].Health.ToString();
                P2StrengthLabel.Text = charList[1].Strength.ToString();
                P2DexterityLabel.Text = charList[1].Dexterity.ToString();
                P2MagicLabel.Text = charList[1].Magic.ToString();
                P2AttModLabel.Text = charList[1].AttackModifier.ToString();
                P2DefModLabel.Text = charList[1].DefenseModifier.ToString();
                P2CritChanceLabel.Text = charList[1].CritChance.ToString();
                //P2WeaponLabel.Text = charList[1].EquippedWeapon.ToString();
                //P2ArmorLabel.Text = charList[1].EquippedArmor.ToString();
            }

            //player 3
            try
            {
                if (charList[2] != null)
                {
                    P3groupBox.Text = charList[2].Name.ToString();
                    //P3pictureBox.Image = charList[2].Img;
                    P3LevelLabel.Text = charList[2].Level.ToString();
                    P3HealthLabel.Text = charList[2].Health.ToString();
                    P3StrengthLabel.Text = charList[2].Strength.ToString();
                    P3DexterityLabel.Text = charList[2].Dexterity.ToString();
                    P3MagicLabel.Text = charList[2].Magic.ToString();
                    P3AttModLabel.Text = charList[2].AttackModifier.ToString();
                    P3DefModLabel.Text = charList[2].DefenseModifier.ToString();
                    P3CritChanceLabel.Text = charList[2].CritChance.ToString();
                    //P3WeaponLabel.Text = charList[2].EquippedWeapon.ToString();
                    //P3ArmorLabel.Text = charList[2].EquippedArmor.ToString();
                }
            }
            catch { }
        }

    }//end class
}
