using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project_LoFi
{
    public class Item
    {
        private int dmgMod;//value of item that will modify the
        public int DmgMod //damage that the character can do per attack
        {
            get { return dmgMod; }
            set { dmgMod = value; }
        }

        private int critMod;//value of item that will modify the
        public int CritMod //chance the character has to critical hit
        {
            get { return critMod; }
            set { critMod = value; }
        }

        private string itemType;//string that tells what type of item it is
        public string ItemType //such as weapon, consumable, etc
        {
            get { return itemType; }
            set { itemType = value; }
        }

        private string itemDesc;//string tells description of the item
        public string ItemDesc //can be flavour text and/or notify the stats of the item
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }

        private int range;//how far the items effect goes
        public int Range //can be 1 for melee, more for ranged weapons
        {
            get { return range; }
            set { range = value; }
        }

        private Texture2D itemImage;//has the image file for the item
        public Texture2D ItemImage
        {
            get { return itemImage; }
            set { itemImage = value; }
        }
    }//End of Item class
}
