using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project_LoFi
{
    public class GamePlay
    {


        public void OpenInventory()
        {

        }//end open inventory


        public void OpenCharacterSheet(CharacterSheet sheet)
        {
            sheet.Show();
            sheet.open = true;
        }//end open character sheet

        
        public void CloseCharacterSheet(CharacterSheet sheet)
        {
            sheet.Hide();
            sheet.open = false;
        }


        /// <summary>
        /// calls the approiate attack method and pass it something to attack
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        public void Combat(Unit attacker, Unit defender)
        {
            attacker.Attack(defender);
            if (defender.IsDead())
            {
                //need something to tell if attacker is a player so xp can be given
                //defender.RemoveCorpse(currentMap);  This needs the code for map which is a GridOccupant[][]
            }
        }//end combat



        public void UseItem(Item item, Unit character)
        {
            
        }//end use item


        /// <summary>
        /// equips an equipable piece of gear
        /// </summary>
        /// <param name="equipment"></param>
        /// <param name="character"></param>
        public void EquipItem(Item equipment, Unit character)
        {
            character.equipItem(equipment);
        }

        /// <summary>
        /// Move the selection cursor around the map based on keyboard input
        /// </summary>
        /// <param name="keyState"></param>
        public void MoveCursor(KeyboardState keyState)
        {
            //check key input and move cursor accordingly, will need code to slow down how fast the cursor moves
            if (keyState.IsKeyDown(Keys.Up))//up
            {
                //move cursor up
            }//end up key

            if (keyState.IsKeyDown(Keys.Down))//down
            {
                //move cursor down
            }//end down key

            if (keyState.IsKeyDown(Keys.Left))//left
            {
                //move cursor left
            }//end left key

            if (keyState.IsKeyDown(Keys.Right))//right
            {
                //move cursor right
            }//end right key
        }//end move cursor

    }//end class

}
