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
///Class description: Class for player units. Holds exp, etc.

namespace Project_LoFi
{
    public class PlayerUnit : Unit
    {
        /// --  Instance Variables  --
        private int currentExp;                    // Variable to track how much experience a unit has gained
        /// --  End of Instance Variables   --



        /// --  Properties  --
        public int CurrentExp
        {
            set { currentExp = value; }
            get { return currentExp; }
        }
        /// --  End of Properties   --



        /// --  Constructors    --
        
        public PlayerUnit() : base() { }         // Skeleton no argument constructor

        public PlayerUnit(int xValue, int yValue)
            : base(xValue, yValue)
        {

        }
        
        public PlayerUnit(int xValue, int yValue, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, int curXP)
            : base(xValue, yValue, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic)
        {
            CurrentExp = curXP;
        }     
        
        public PlayerUnit(int xValue, int yValue, Texture2D unitImage, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, int curXP)
            : base(xValue, yValue, unitImage, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic)
        {
            CurrentExp = curXP;
        }

        public PlayerUnit(int xValue, int yValue, Texture2D unitImage, string unitName, int hp, int defenseModifier, int attackModifier,
                            double critChance, int level, int strength, int dexterity, int magic, Item equippedWeapon, Item equippedArmor,
                            List<Item> inventory, int curXP)
            : base(xValue, yValue, unitImage, unitName, hp, defenseModifier, attackModifier, critChance, level, strength, dexterity, magic,
                    equippedWeapon, equippedArmor, inventory)
        {
            CurrentExp = curXP;
        }     
        
        /// -- End of Constructors  --



        /// --  Methods    --
        
        /// <summary>
        /// Overwrites MovableGridOccupant's Attack method. Whatever object called this method should
        /// call target.IsDead() immediately after, so it can call target.RemoveCorpse() if necessary
        /// </summary>
        /// <param name="target"></param>
        
         //public override void Attack(MovableGridOccupant target) Getting an error. Changed to Unit, so it compiles.
        public override int Attack(Unit target)
        {
            EnemyUnit holder = (EnemyUnit)target;
            int dmg;
            dmg = holder.TakeDamage(this.AttackModifier);
            if (holder.IsDead() == true)
                this.currentExp += holder.ExpDrop;      //If the player killed the enemy, give them exp
            return dmg;
        }
        /// -- End of Methods  --


    }//End of PlayerUnit class
}
