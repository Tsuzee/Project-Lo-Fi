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
        int unitWidth = 60;
        int unitHeight = 60;
        int frameNum = 0;
        string player;
        bool moved;
        bool attack = false;


        // -- Constructor --
        public Drawer() 
        {
            moved = false;

        }


        //  --  Methods --

        public void updateDrawInfo(bool pMoved, PlayerUnit pUnit = null, EnemyUnit eUnit = null)
        {
            moved = pMoved;
            if (pUnit != null)
            {
                player = pUnit.Name;
            }
        }//end updatedrawinfo

        /// <summary>
        /// Draws the map and the units on it
        /// </summary>
        /// <param name="drawTexture"></param>
        public void DrawMap(GridOccupant[,] map, SpriteBatch drawTexture)
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
                    else if (map[i, j] is MovableGridOccupant && map[i,j] is EnemyUnit)
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
                    else if(map[i, j] is PlayerUnit)
                    {
                        MovableGridOccupant pUnit = (MovableGridOccupant)map[i, j];
                        Texture2D unitTxtr = pUnit.Img;
                        Texture2D terrainTxtr = pUnit.OccupiedSpace.Img;

                        // First draw the terrain under the unit
                        drawTexture.Draw(terrainTxtr, new Rectangle(i * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);

                        if (!attack)
                        {
                            // Then draw the unit itself
                            drawTexture.Draw(unitTxtr,
                                new Rectangle(i * txtrWidth, j * txtrHeight, unitWidth, unitHeight),
                                new Rectangle(0, 0, unitWidth, unitHeight),
                                Color.White);
                        }
                        else if (attack)
                        {
                            //needs the map to be finished with fully loaded units
                        }
                    }
                }//End of inner for loop
            }//End of outer for loop
        }//End of DrawMap method




        /// <summary>
        /// Handles all the highlighter details
        /// </summary>
        /// <param name="map"></param>
        /// <param name="selectedUnit"> Whatever unit is selected in Game1 </param>
        /// <param name="gameVars"> Game1's GameVariables object </param>
        public void DrawHighlighter(GridOccupant[,] map, PlayerUnit selectedUnit, GameVariables gameVars, SpriteBatch drawTexture)
        {
            // Local "mask" variables to make code cleaner
            int i = selectedUnit.X;
            int j = selectedUnit.Y;


            //if player is attemping combat draw the battle highlighters
            if (selectedUnit != null)
            {
                if (i + 1 < map.GetLength(0))                // If it's in bounds
                {
                    if (map[i + 1, j] is EnemyUnit)          // If it's an enemy, color the square red
                        drawTexture.Draw(gameVars.battleHighLighter, new Rectangle((i + 1) * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                    else if (map[i + 1, j] is Terrain)      // If it's a terrain space, color it blue
                        drawTexture.Draw(gameVars.highligther, new Rectangle((i + 1) * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                }
                if (i - 1 >= 0)
                {
                    if (map[i - 1, j] is EnemyUnit)          // Enemy red
                        drawTexture.Draw(gameVars.battleHighLighter, new Rectangle((i - 1) * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                    else if (map[i - 1, j] is Terrain)      // Terrain blue
                        drawTexture.Draw(gameVars.highligther, new Rectangle((i - 1) * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                }
                if ((j + 1) < map.GetLength(1))
                {
                    if (map[i, j + 1] is EnemyUnit)          // Enemy red
                        drawTexture.Draw(gameVars.battleHighLighter, new Rectangle(i * txtrWidth, (j + 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                    else if (map[i, j + 1] is Terrain)      // Terrain blue
                        drawTexture.Draw(gameVars.highligther, new Rectangle(i * txtrWidth, (j + 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                }
                if ((j - 1) >= 0)
                {
                    if (map[i, j - 1] is EnemyUnit)          // Enemy red
                        drawTexture.Draw(gameVars.battleHighLighter, new Rectangle(i * txtrWidth, (j - 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                    else if (map[i, j - 1] is Terrain)      // Terrain blue
                        drawTexture.Draw(gameVars.highligther, new Rectangle(i * txtrWidth, (j - 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                }
            }// End of outerMost if
        }// End of DrawHighlighter method


        /// <summary>
        /// Draws the game introduction.
        /// </summary>
        /// <param name="gameVars"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public void DrawIntro(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (frameNum < 100)
            {
                spriteBatch.Draw(gameVars.logo, new Rectangle(((graphicsDevice.Viewport.Width / 2) - 450),
                    (graphicsDevice.Viewport.Height / 2) - 360, 900, 750), Color.White);
            }

            if (frameNum > 99 && frameNum < 200)
            {
                spriteBatch.DrawString(gameVars.Font1Bold, "presents", new Vector2((graphicsDevice.Viewport.Width / 2) - 150,
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);
            }

            if (frameNum > 199)
            {
                spriteBatch.DrawString(gameVars.Font1Bold, "PROJECT LO-FI", new Vector2((graphicsDevice.Viewport.Width / 2) - 375,
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 5.0f, SpriteEffects.None, 0.0f);
            }
            
            frameNum++;
        }//end drawintro

        /// <summary>
        /// Draws the Main Menu.
        /// </summary>
        /// <param name="gameVars"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public void DrawMenu(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            frameNum = 0;
            spriteBatch.DrawString(gameVars.Font1Bold, "PROJECT LO-FI", new Vector2((graphicsDevice.Viewport.Width / 2) - 375,
                                (graphicsDevice.Viewport.Height / 2) - 300), Color.DarkRed, 0.0f, Vector2.Zero, 5.0f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(gameVars.Font1Bold, "press Enter to play", new Vector2((graphicsDevice.Viewport.Width / 2) - 350,
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);
        }//end drawmenu


        /// <summary>
        /// Draw the Game Credits.
        /// </summary>
        /// <param name="gameVars"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public void DrawCredits(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            string output;
            Vector2 FontOrigin;

            output = "Credits";
            FontOrigin = gameVars.Font1Bold.MeasureString(output) / 2;
            FontOrigin.Y = 10;
            spriteBatch.DrawString(gameVars.Font1Bold, output, new Vector2((graphicsDevice.Viewport.Width / 2) - 50, 10),
                Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);


            output = "Design - Aliaksandr Shumski";
            FontOrigin = gameVars.Font1Bold.MeasureString(output) / 2;
            FontOrigin.Y = 100;
            spriteBatch.DrawString(gameVars.Font1Bold, output,
                FontOrigin,
                Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);



            output = "Architecture - Jesse Cooper";
            FontOrigin = gameVars.Font1Bold.MeasureString(output) / 2;
            FontOrigin.Y = 200;
            spriteBatch.DrawString(gameVars.Font1Bold, output,
                FontOrigin,
                Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);

            output = "Interface - Phillip Fowler";
            FontOrigin = gameVars.Font1Bold.MeasureString(output) / 2;
            FontOrigin.Y = 300;
            spriteBatch.DrawString(gameVars.Font1Bold, output,
                FontOrigin,
                Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);


            output = "Group Lead - Darren Farr";
            FontOrigin = gameVars.Font1Bold.MeasureString(output) / 2;
            FontOrigin.Y = 400;
            spriteBatch.DrawString(gameVars.Font1Bold, output,
                FontOrigin,
                Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);
        }

        //draw text information for player
        public void DrawGameInfo(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, TurnState turn, int numTurns)
        {
            spriteBatch.DrawString(gameVars.Font1Bold, "You have " + numTurns + " turns remaining.", new Vector2(10, 661), 
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            if(moved)
            {
                spriteBatch.DrawString(gameVars.Font1Bold, player + " moved", new Vector2(10, 680),
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
        }//end draw gameinfo



    }//End of Draw class
}
