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
        private Terrain occupiedSpace;
        
        /// --  End of Instance Variables   --

            // This list is giant. I'm okay with that, but if anybody has suggestions for how to split up the inheritance, I'm
            // open to ideas. Maybe take the inventory & stat stuff out and throw them in a Unit class that derives from MovableGridOccupant?
            
            // The more I think about it, the more I like that idea. Let me guys know what you think (I will, of course, be happy to 
            // alter the affected classes myself - and in a timely fashion - if it sounds good).

        /// --  Properties  --

        public Terrain OccupiedSpace
        {
            set { occupiedSpace = value; }  // Could check here to make sure they're not on an impassable space
            get { return occupiedSpace; }
        }

        
        /// --  End of Properties   --



        /// --  Constructors    --
        public MovableGridOccupant()
            : base()
        {
            occupiedSpace = null;
        }

        public MovableGridOccupant(int xCoord, int yCoord, int txtrIndex)
            : base(xCoord, yCoord, txtrIndex)
        {

        }

        public MovableGridOccupant(int xCoord, int yCoord)
            : base(xCoord, yCoord)
        {

        }

        /// --  End of Contructors  --



        /// --  Methods --
        
        

        /// <summary>
        /// Attempts to move the unit, in a direction given by the method parameter
        /// </summary>
        /// <param name="map"> The main game map. </param>
        /// <param name="dir"> The direction the unit is trying to move.
        ///                     0 --> up, 1 --> down, 2 --> left, 3 --> right </param>
        /// <returns> a boolean representing whether or not the move was successful. Completely optional to implement. </returns>
        public bool Move(GridOccupant[ , ] map, int dir)
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

            if (testX >= 0 && testX < map.Length && testY >= 0 && testY < map.GetLength(0))    //Ensure it's in-bounds
            {
                try
                {
                    if (map[testX, testY].GetType() == occupiedSpace.GetType())     // If it's terrain
                    {
                        Terrain holder = (Terrain)map[testX, testY];            // Casting is lossy, so this might not work
                        if (holder.Impassable == false)                         // If it isn't a wall
                        {
                            map[this.X, this.Y] = this.OccupiedSpace;           // Restore the terrain
                            this.OccupiedSpace = (Terrain)map[testX, testY];    // Store the space you're about to walk into
                            map[testX, testY] = this;                           // Finally, move the unit

                            //Update unit info
                            this.X = testX;
                            this.Y = testY;

                            resultFlag = true;
                        }
                    }//end if
                }//end try
                catch { }

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
        public virtual void RemoveCorpse(GridOccupant[ , ] map)
        {
            // Removal logic here - this method should be overwritten by derived classes
        }

        /// --  End of Methods

    }//End of MovableGridOccupant class
}
