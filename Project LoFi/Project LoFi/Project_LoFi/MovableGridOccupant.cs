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
        private int attackModifier;         // Represents current damage output. Affected by items.
        //crit chance
        private int level;
        //stats
        private Item equippedWeapon;
        private Item equippedArmor;
        private Terrain occupiedSpace;
        private List<Item> inventory;
        /// --  End of Instance Variables   --

            // Do all enemies have stats, by the way? Because if not we should put them in player,
            // but if so we should put them here.

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
            get { return defenseModifier; }
        }

        public int AttackModifier
        {
            set { attackModifier = value; }
            get { return attackModifier; }
        }

        public int Level
        {
            set { level = value; }
            get { return level; }
        }

        public Item EquippedWeapon
        {
            set { equippedWeapon = value; }
            get { return equippedWeapon; }
        }

        public Item EquippedArmor
        {
            set { equippedArmor = value; }
            get { return equippedArmor; }
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

        /// <summary>
        /// Method to allow enemies to attack each other.
        /// </summary>
        /// <param name="mgo"></param>
        public virtual void Attack(MovableGridOccupant target)
        {
            target.TakeDamage(this.AttackModifier);
        }

        /// <summary>
        /// Not sure how we want to handle this/ I'm thinking either give items an "equipped" boolean attribute,
        /// or give players an "equippedWeapon" Item attribute. The former is information more relevant to the player
        /// than to the item, so I don't love that idea, but the latter is incredibly inflexible, esp. if we wanted
        /// to add things like armor
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns> a boolean representing whether or not we were able to equip the item successfully. </returns>
        public bool equipItem(Item equipment)
        {
            bool resultFlag = true;                 //Assume they'll equip successfully
            if (equipment.ItemType == "weapon")
            {
                unequipItem(equipment);
                EquippedWeapon = equipment;
                this.AttackModifier += equipment.DmgMod;
            }
            else if (equipment.ItemType == "armor")
            {
                unequipItem(equipment);
                EquippedArmor = equipment;
                this.DefenseModifier += equipment.ArmorMod;
            }

            return resultFlag;
        }

        /// <summary>
        /// Helper method for equipItem() that handles stat changes for unequipping
        /// </summary>
        /// <param name="equipment"> The piece of equipment to be removed. </param>
        /// <returns> A bool representing is the unequip was successful; </returns>
        private bool unequipItem(Item equipment)
        {
            bool resultFlag = false;                                // Assume there will be problems
            if (equipment == EquippedWeapon)
            {
                this.AttackModifier -= EquippedWeapon.DmgMod;
                EquippedWeapon = null;
                resultFlag = true;
            }
            else if (equipment == EquippedArmor)
            {
                this.DefenseModifier -= EquippedArmor.ArmorMod;
                EquippedArmor = null;
                resultFlag = true;
            }
            return resultFlag;
        }

        /// <summary>
        /// Attempts to move the unit, in a direction given by the method parameter
        /// </summary>
        /// <param name="map"> The main game map. </param>
        /// <param name="dir"> The direction the unit is trying to move.
        ///                     0 --> up, 1 --> down, 2 --> left, 3 --> right </param>
        /// <returns> a boolean representing whether or not the move was successful. Completely optional to implement. </returns>
        public bool Move(GridOccupant[][] map, int dir)
        {
            bool resultFlag = false;        // Assume the move will fail
            // Create holder variables for testing the array
            int testX = this.X;
            int testY = this.Y;

            if (dir == 0)
                testY -= 1;                 // Go up 1 (remember our map starts at the top-left corner
            else if (dir == 1)
                testY += 1;                 // Go down 1
            else if (dir == 2)
                testX -= 1;                 // Go left 1
            else
                testX += 1;                 // Go right 1

            if (testX >= 0 && testX < map.Length && testY >= 0 && testY < map[0].Length)    //Ensure it's in-bounds
            {
                if (map[testX][testY].GetType() == occupiedSpace.GetType())     // If it's terrain
                {
                    Terrain holder = (Terrain)map[testX][testY];            // Casting is lossy, so this might not work
                    if (holder.Impassable == false)                         // If it isn't a wall
                    {
                        map[this.X][this.Y] = this.OccupiedSpace;           // Restore the terrain
                        this.OccupiedSpace = (Terrain)map[testX][testY];    // Store the space you're about to walk into
                        map[testX][testY] = this;                           // Finally, move the unit

                        //Update unit info
                        this.X = testX;
                        this.Y = testY;

                        resultFlag = true;
                    }
                }
            }// And we're done, because units can only move into passable terrain spaces

            return resultFlag;

            /*
             * I personally hold an irrational hatred for switch statements, but here's one of those
             * if you'd prefer that for the test on dir's value
             *
                switch (dir)
                {
                    case 0:
                        {
                            testY -= 1;         // Go up 1 (remember our map starts at the top-left corner
                            break;
                        }
                    case 1:
                        {
                            testY += 1;         // Go down 1
                            break;
                        }
                    case 2:
                        {
                            testX -= 1;         // Go left 1
                            break;
                        }
                    case 3:
                        {
                            testX += 1;         // Go right 1
                            break;
                        }
                }//End of switch
             * 
             * Yeah. Tell me you prefer that to an 8 line if
             */
            
        }//End of Move method

        /// <summary>
        /// Method to remove a unit from the array, if it's dead
        /// </summary>
        /// <param name="map"></param>
        public virtual void RemoveCorpse(GridOccupant[][] map)
        {
            // Removal logic here - this method should be overwritten by derived classes
        }

        /// --  End of Methods

    }//End of MovableGridOccupant class
}
