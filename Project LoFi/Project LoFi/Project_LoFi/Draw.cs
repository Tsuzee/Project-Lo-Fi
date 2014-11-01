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

/// Orig. Author: Jesse C.
/// Class description: class that handles drawing things to the screen

namespace Project_LoFi
{
    public class Drawer
    {
        //  --  Attributes  --
        int txtrWidth = 60;
        int txtrHeight = 60;
        int unitWidth = 32;
        int unitHeight = 48;



        //  --  Methods --

        /// <summary>
        /// Draws the map and the units on it
        /// </summary>
        /// <param name="drawTexture"></param>
        public void DrawMap(SpriteBatch drawTexture, GridOccupant[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Terrain)
                    {
                        Texture2D terrainTxtr = map[i, j].Img;
                        drawTexture.Draw(terrainTxtr, new Rectangle(i * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                    }
                    else if (map[i, j] is MovableGridOccupant)
                    {
                        MovableGridOccupant mgo = (MovableGridOccupant)map[i, j];
                        Texture2D unitTxtr = mgo.Img;
                        Texture2D terrainTxtr = mgo.OccupiedSpace.Img;

                        // First draw the terrain under the unit
                        drawTexture.Draw(terrainTxtr, new Rectangle(i * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);

                        // Then draw the unit itself
                        drawTexture.Draw(unitTxtr,
                            new Rectangle(i * txtrWidth, j * txtrHeight, unitWidth, unitHeight),
                            new Rectangle(0, 0, unitWidth, unitHeight),
                            Color.White);
                    }
                }//End of inner for loop
            }//End of outer for loop
        }//End of DrawMap method


    }//End of Draw class
}
