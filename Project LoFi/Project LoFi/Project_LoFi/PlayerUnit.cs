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
        /// -- End of Constructors  --



        /// --  Methods    --
        
        /// <summary>
        /// Overwrites MovableGridOccupant's Attack method. Whatever object called this method should
        /// call target.IsDead() immediately after, so it can call target.RemoveCorpse() if necessary
        /// </summary>
        /// <param name="target"></param>
        
         //public override void Attack(MovableGridOccupant target) Getting an error. Changed to Unit, so it compiles.
        public override void Attack(Unit target)
        {
            EnemyUnit holder = (EnemyUnit)target;
            holder.TakeDamage(this.AttackModifier);
            if (holder.IsDead() == true)
                this.currentExp += holder.ExpDrop;      //If the player killed the enemy, give them exp
        }
        /// -- End of Methods  --


    }//End of PlayerUnit class
}
