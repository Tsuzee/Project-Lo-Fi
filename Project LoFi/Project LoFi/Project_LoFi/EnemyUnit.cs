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
        /// --  End of Instance Variables   --



        /// --  Properties  --
        public int ExpDrop
        {
            set { expDrop = value; }
            get { return expDrop; }
        }
        /// --  End of Properties   --



        /// --  Constructors    --
        public EnemyUnit() { }  // Skeleton no argument constructor
        // Get it? Skeleton? Because the enemies are. . .nevermind.

        public EnemyUnit(int xPosition, int yPosition, int enemyType) 
        {
            this.X = xPosition; 
            this.Y = yPosition;
            this.TextureIndex = enemyType;
        }         
                                           
        /// -- End of Constructors  --



        /// --  Methods    --
        
        /// -- End of Methods  --


    }//End of EnemyUnit class
}
