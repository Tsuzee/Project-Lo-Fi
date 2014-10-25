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

    }//end class
}
