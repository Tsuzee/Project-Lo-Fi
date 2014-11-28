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

/// Orig. class author: Philip Fowler
/// Most recent edit: 11/27/14, by J. Cooper

namespace Project_LoFi
{
    public class Item
    {
        /// --  Attributes & Properties
        private string itemName;//affects player stat
        public string ItemName //affects damage with swords
        {
            get { return itemName; }
            set { itemName = value; }
        }

        private int strengthMod;//affects player stat
        public int StrengthMod //affects damage with swords
        {
            get { return strengthMod; }
            set { strengthMod = value; }
        }

        private int dexterityMod;//affects player stat
        public int DexterityMod //affects crit chance with knives
        {
            get { return dexterityMod; }
            set { dexterityMod = value; }
        }

        private int magicMod;//affects player stat
        public int MagicMod //affects staff efficacy
        {
            get { return magicMod; }
            set { magicMod = value; }
        }

        private int armorMod;//value of item determines
        public int ArmorMod //damage mitigation
        {
            get { return armorMod; }
            set { armorMod = value; }
        }

        private int dmgMod;//value of item that will modify the
        public int DmgMod //damage that the character can do per attack
        {
            get { return dmgMod; }
            set { dmgMod = value; }
        }

        private double critMod;//value of item that will modify the
        public double CritMod //chance the character has to critical hit
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

        //currently ignoring this till we can decide how and if to use it
        private int range;//how far the items effect goes
        public int Range //can be 1 for melee, more for ranged weapons
        {
            get { return range; }
            set { range = value; }
        }

        private Texture2D img;//has the image file for the item
        public Texture2D Img
        {
            get { return img; }
            set { img = value; }
        }
        /// --  End Attributes & Properties



        /// --  Constructors

        public Item(){ }

        public Item(string name, int strMod, int dexMod, int magMod, int amrMod, int damageMod, double criticalMod, string type, string desc)
        {
            itemName = name;
            strengthMod = strMod;
            dexterityMod = dexMod;
            magicMod = magMod;
            armorMod = amrMod;
            dmgMod = damageMod;
            critMod = criticalMod;
            itemType = type;
            itemDesc = desc;
        }

        /// --  End Constructors


    }//End of Item class
}
