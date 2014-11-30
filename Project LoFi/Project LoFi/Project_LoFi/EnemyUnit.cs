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

///Orig. Author: Jesse C.
///Class description: class for enemies. Implements MovableGridOccupant

namespace Project_LoFi
{
    public class EnemyUnit : Unit
    {
        /// --  Instance Variables  --
        private int expDrop;                    // Variable to track how much experience an enemy gives upon death
                                                // We can also handle this with methods, if we change a couple things
        private bool isBoss;
        /// --  End of Instance Variables   --



        /// --  Properties  --
        public int ExpDrop
        {
            set { expDrop = value; }
            get { return expDrop; }
        }

        public bool IsBoss
        {
            get { return isBoss; }
        }
        /// --  End of Properties   --



        /// --  Constructors    --
        public EnemyUnit() : base() { }  // Skeleton no argument constructor
        // Get it? Skeleton? Because the enemies are. . .nevermind.

        public EnemyUnit(int xValue, int yValue)
            : base(xValue, yValue)
        {

        }

        public EnemyUnit(int xValue, int yValue, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, int xpDrop, int boss)
            : base(xValue, yValue, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic)
        {
            ExpDrop = xpDrop;
            if(boss == 1)
            {
                isBoss = true;
            }
            else
            {
                isBoss = false;
            }
        }     
        
        public EnemyUnit(int xValue, int yValue, Texture2D unitImage, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, int xpDrop, int boss)
            : base(xValue, yValue, unitImage, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic)
        {
            ExpDrop = xpDrop;
            if (boss == 1)
            {
                isBoss = true;
            }
            else
            {
                isBoss = false;
            }
        }     
        
        public EnemyUnit(int xValue, int yValue, Texture2D unitImage, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, Item equippedWeapon, Item equippedArmor,
                            List<Item> inventory, int xpDrop, int boss)
            : base(xValue, yValue, unitImage, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic,
                    equippedWeapon, equippedArmor, inventory)
        {
            ExpDrop = xpDrop;
            if (boss == 1)
            {
                isBoss = true;
            }
            else
            {
                isBoss = false;
            }
        }     
          
        /// -- End of Constructors  --



        /// --  Methods    --
        
        /// -- End of Methods  --


    }//End of EnemyUnit class
}
