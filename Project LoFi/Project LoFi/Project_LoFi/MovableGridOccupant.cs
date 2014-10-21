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
///Class description: base class for units. Holds health, inventory, attack & movement logic, inventory, etc.

namespace Project_LoFi
{
    public abstract class MovableGridOccupant : GridOccupant
    {
        /// --  Instance Variables  --
        private int health;
        private int defenseModifier;        // Reduces damage taken. This is how terrain/armor works.
        private Terrain occupiedSpace;
        private List<Item> inventory;
        /// --  End of Instance Variables   --

            // Do all enemies have stats, by the way?

        /// --  Properties  --
        public int Health
        {
            set
            {
                if (value >= 0)             // If they passed in a valid number
                    health = value;         // then use that number
                else                        // Else
                    health = 0;             // health would have been negative, so "cap" it at 0
            }
            get { return health; }
        }

        public int DefenseModifier
        {
            set { defenseModifier = value; }
            get { return health; }
        }

        public Terrain OccupiedSpace
        {
            set { occupiedSpace = value; }  // Could check here to make sure they're not on an impassable space
            get { return occupiedSpace; }
        }

        List<Item> Inventory
        {
            set { inventory = value; }
            get { return inventory; }
        }
        /// --  End of Properties   --



        /// --  Constructors    --
        public MovableGridOccupant()
            : base()
        {
            health = 1;
            occupiedSpace = null;
            Inventory = new List<Item>();
        }
        /// --  End of Contructors  --



        /// --  Methods --
        
        /// <summary>
        /// Method checks to see if the unit has health left, and returns true or false based on the check.
        /// </summary>
        public bool IsDead()
        {
            bool resultFlag = false;    //Assume they aren't dead
            if (health == 0)            // health <= 0?
                resultFlag = true;

            return resultFlag;
        }

        /// <summary>
        /// Method applies damage to a unit, taking into account the unit's defense.
        /// </summary>
        /// <param name="dmg"> dmg should be a positive number </param>
        public void TakeDamage(int dmg)
        {
            if (dmg > 0)                // As long as they're doing *some* damage
                Health -= (dmg - DefenseModifier);  // Subtract your defense from the dmg, then apply the dmg)
            else if (dmg < 0)           // If they did "negative" damage (healed)
                Health -= dmg;          // then don't include the defense modifier
        }




        /// --  End of Methods






    }//End of MovableGridOccupant class
}
