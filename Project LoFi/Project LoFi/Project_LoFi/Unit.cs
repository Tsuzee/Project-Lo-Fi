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
   public class Unit : MovableGridOccupant
    {
        /// --  Instance Variables  --
        public bool isBoss;
        private string name;
        private int health;
        private int defenseModifier;        // Reduces damage taken. This is how terrain/armor works.
        private int attackModifier;         // Represents current damage output. Affected by items.
        private double critChance;
        private int level;
        private int strength;
        private int dexterity;
        private int magic;
        private Item equippedWeapon;
        private Item equippedArmor;
        private List<Item> inventory;
        private Random critical = new Random();
        /// --  End of Instance Variables   --

        /// -- Properties --
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
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

        public double CritChance
        {
            set { critChance = value; }
            get { return critChance; }
        }

        public int Level
        {
            set { level = value; }
            get { return level; }
        }

        public int Strength
        {
            set { strength = value; }
            get { return strength; }
        }

        public int Dexterity
        {
            set { dexterity = value; }
            get { return dexterity; }
        }

        public int Magic
        {
            set { magic = value; }
            get { return magic; }
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

        public List<Item> Inventory
        {
            set { inventory = value; }
            get { return inventory; }
        }
        /// --  End of Properties   --
        
        /// -- Constructors --
        
        public Unit()
            : base()
        {
            health = 1;
            defenseModifier = 0;
            attackModifier = 0;
            critChance = 0.0;
            level = 1;
            strength = 1;
            dexterity = 1;
            magic = 1;
            equippedWeapon = null;
            equippedArmor = null;
            Inventory = new List<Item>();
        }

        public Unit(int xCoord, int yCoord)
            : base(xCoord, yCoord)
        {
            health = 1;
            defenseModifier = 0;
            attackModifier = 0;
            critChance = 0.0;
            level = 1;
            strength = 1;
            dexterity = 1;
            magic = 1;
            equippedWeapon = null;
            equippedArmor = null;
            Inventory = new List<Item>();
        }

        public Unit(int xCoord, int yCoord, string unitName, int hp, int dMod, int aMod, double cChance,
                        int lvl, int str, int dex, int mag)
            : base(xCoord, yCoord)
        {
            Name = unitName;
            health = hp;
            defenseModifier = dMod;
            attackModifier = aMod;
            critChance = cChance;
            level = lvl;
            strength = str;
            dexterity = dex;
            magic = mag;
        }

        public Unit(int xCoord, int yCoord, Texture2D unitImage, string unitName, int hp, int dMod, int aMod, double cChance,
                        int lvl, int str, int dex, int mag)
            : base(xCoord, yCoord, unitImage)
        {
            Name = unitName;
            health = hp;
            defenseModifier = dMod;
            attackModifier = aMod;
            critChance = cChance;
            level = lvl;
            strength = str;
            dexterity = dex;
            magic = mag;
        }

        public Unit(int xCoord, int yCoord, Texture2D unitImage, string unitName, int hp, int dMod, int aMod, double cChance,
                        int lvl, int str, int dex, int mag, Item equippedWpn, Item equippedAmr, List<Item> inv)
            : base(xCoord, yCoord, unitImage)
        {
            Name = unitName;
            health = hp;
            defenseModifier = dMod;
            attackModifier = aMod;
            critChance = cChance;
            level = lvl;
            strength = str;
            dexterity = dex;
            magic = mag;
            equippedWeapon = equippedWpn;
            equippedArmor = equippedAmr;
            Inventory = inv;
        }
        
        /// -- End of Constructors --


        /// --  Methods --

        /// <summary>
        /// Method checks to see if the unit has health left, and returns true or false based on the check.
        /// </summary>
        public bool IsDead()
        {
            bool resultFlag = false;    //Assume they aren't dead
            if (health <= 0)            // health <= 0?
                resultFlag = true;

            return resultFlag;
        }

        /// <summary>
        /// Method applies damage to a unit, taking into account the unit's defense.
        /// </summary>
        /// <param name="dmg"> dmg should be a positive number </param>
        public int TakeDamage(int dmg)
        {
            if (dmg < 0)                // If they did "negative" damage (healed)
            {
                Health -= dmg;          // then don't include the defense modifier
                return dmg;
            }
            else if (dmg - DefenseModifier > 0)     // As long as they're doing *some* damage
            {
                Health -= (dmg - DefenseModifier);  // Subtract your defense from the dmg, then apply the dmg)
                return (dmg - DefenseModifier);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Method to allow enemies to attack each other.
        /// </summary>
        /// <param name="mgo"></param>
        public virtual double Attack(Unit target)
        {
            return target.TakeDamage(this.AttackModifier);
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
            if (equipment != null && equipment.ItemType == "weapon")
            {
                unequipItem(equippedWeapon);
                EquippedWeapon = equipment;
                this.Strength += equipment.StrengthMod;
                this.Dexterity += equipment.DexterityMod;
                this.Magic += equipment.MagicMod;
                this.AttackModifier += equipment.DmgMod;
                this.CritChance += equipment.CritMod;
            }
            else if (equipment != null && equipment.ItemType == "armor")
            {
                unequipItem(equipment);
                EquippedArmor = equipment;
                this.Strength += equipment.StrengthMod;
                this.Dexterity += equipment.DexterityMod;
                this.Magic += equipment.MagicMod;
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
            if (equipment != null && equipment == EquippedWeapon)
            {
                this.Strength -= equipment.StrengthMod;
                this.Dexterity -= equipment.DexterityMod;
                this.Magic -= equipment.MagicMod;
                this.AttackModifier -= equipment.DmgMod;
                this.CritChance -= equipment.CritMod;
                EquippedWeapon = null;
                resultFlag = true;
            }
            else if (equipment != null && equipment == EquippedArmor)
            {
                this.Strength -= equipment.StrengthMod;
                this.Dexterity -= equipment.DexterityMod;
                this.Magic -= equipment.MagicMod;
                this.DefenseModifier -= EquippedArmor.ArmorMod;
                EquippedArmor = null;
                resultFlag = true;
            }
            return resultFlag;
        }

        public void setStats()
        {

            health = (int)((double)(strength) * 2 + (double)(dexterity) * 1 + (double)(magic) * 0.75);
            defenseModifier = (int)((double)(strength) * 1 + (double)(dexterity) * 0.5 + (double)(magic) * 0.25);

            if (dexterity == 0)
            {
                critChance = 1;
            }
            else
            {
                critChance = (int)Math.Round(((double)dexterity * 2.5)); // formula for crit chance;
            }

            //If strength is the main attribute - characters is a strength based.  He is getting more health, defense and attack
            if ((strength > dexterity) && (strength > magic))
            {
                attackModifier = (int)Math.Round((double)(strength / 2));
            }

            // If dexterity is the main attribute - character is a dexterity based. He is getting less health and defense but higher crit chance
            else if ((dexterity > strength) && (dexterity > magic))
            {
                attackModifier = (int)Math.Round((double)(dexterity / 2.5));
            }

            // If magic is the main attribute - character is a magic based.  He is getting less health and defense but has higher attack.
            else if ((magic > strength) && (magic > dexterity))
            {
                attackModifier = (int)Math.Round((double)(magic) / 1.3);
            }

            //If character has equal attributes, he is getting "middle attributes"
            else
            {
                attackModifier = (int)Math.Round((double)(strength / 2) + (double)(dexterity / 2) + (double)(magic / 2));
            } 

        }
       /// <summary>
       /// Check if critical strike occured or not. 
       /// </summary>
       /// <param name="criticalStrikeChance"></param>
       /// <returns></returns>
        public bool criticalStrike (double criticalStrikeChance)
        {
            int chance = critical.Next(101);

            if (criticalStrikeChance >= chance)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// --  End of Methods  --

    }//end class
}
