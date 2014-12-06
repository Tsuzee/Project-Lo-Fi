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

        public void UpdateCharSheet(PlayerUnit player1, PlayerUnit player2, PlayerUnit player3)
        {
            //Player 1
            P1groupBox.Text = player1.Name.ToString();
            //P1pictureBox.Image = player1.Img;
            P1LevelLabel.Text = player1.Level.ToString();
            P1HealthLabel.Text = player1.Health.ToString();
            P1StrengthLabel.Text = player1.Strength.ToString();
            P1DexterityLabel.Text = player1.Dexterity.ToString();
            P1MagicLabel.Text = player1.Magic.ToString();
            P1AttModLabel.Text = player1.AttackModifier.ToString();
            P1DefModLabel.Text = player1.DefenseModifier.ToString();
            P1CritChanceLabel.Text = player1.CritChance.ToString();
            //P1WeaponLabel.Text = player1.EquippedWeapon.ToString();
            //P1ArmorLabel.Text = player1.EquippedArmor.ToString();

            //player 2
            P2groupBox.Text = player2.Name.ToString();
            //P2pictureBox.Image = player2.Img;
            P2LevelLabel.Text = player2.Level.ToString();
            P2HealthLabel.Text = player2.Health.ToString();
            P2StrengthLabel.Text = player2.Strength.ToString();
            P2DexterityLabel.Text = player2.Dexterity.ToString();
            P2MagicLabel.Text = player2.Magic.ToString();
            P2AttModLabel.Text = player2.AttackModifier.ToString();
            P2DefModLabel.Text = player2.DefenseModifier.ToString();
            P2CritChanceLabel.Text = player2.CritChance.ToString();
            //P2WeaponLabel.Text = player2.EquippedWeapon.ToString();
            //P2ArmorLabel.Text = player2.EquippedArmor.ToString();

            //player 3
            P3groupBox.Text = player3.Name.ToString();
            //P3pictureBox.Image = player3.Img;
            P3LevelLabel.Text = player3.Level.ToString();
            P3HealthLabel.Text = player3.Health.ToString();
            P3StrengthLabel.Text = player3.Strength.ToString();
            P3DexterityLabel.Text = player3.Dexterity.ToString();
            P3MagicLabel.Text = player3.Magic.ToString();
            P3AttModLabel.Text = player3.AttackModifier.ToString();
            P3DefModLabel.Text = player3.DefenseModifier.ToString();
            P3CritChanceLabel.Text = player3.CritChance.ToString();
            //P3WeaponLabel.Text = player3.EquippedWeapon.ToString();
            //P3ArmorLabel.Text = player3.EquippedArmor.ToString();
        }

    }//end class
}
