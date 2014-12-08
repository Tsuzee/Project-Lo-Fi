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
        bool pMoved;
        bool eMoved;
        bool attacked;
        bool enemysTurn;
        public bool attack = false;
        string[] log;
        EnemyUnit selectedEUnit = null;


        // -- Constructor --
        public Drawer() 
        {
            pMoved = false;
            eMoved = false;
            log = new string[10];

        }


        //  --  Methods --

        public void updateTextLog(string[] info)
        {// 0:Number of turns, 1:did player move, 2:player name, 3: did enemy move, 4:enemy name, 5:was attack called, 6:dmg delt, 7:Attacker, 8:Target
            log[0] = info[0];
            log[2] = info[2];
            log[4] = info[4];
            log[6] = info[6];
            log[7] = info[7];
            log[8] = info[8];

            if( info[1] == "true")
            {
                pMoved = true;
            }
            else
            {
                pMoved = false;
            }

            if( info[3] == "true")
            {
                eMoved = true;
            }
            else
            {
                eMoved = false;
            }

            if( info[5] == "true")
            {
                attacked = true;
            }
            else
            {
                attacked = false;
            }
            
        }


        /// <summary>
        /// Draws the map and the units on it
        /// </summary>
        /// <param name="drawTexture"></param>
        public void DrawMap(GridOccupant[,] map, SpriteBatch drawTexture, GameVariables gameVars)
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

            if(enemysTurn)
            {
                drawTexture.Draw(gameVars.highligther, new Rectangle(selectedEUnit.X * txtrWidth, selectedEUnit.Y * txtrHeight, unitWidth, unitHeight), Color.White);
            }
        }//End of DrawMap method

        public void HighlightCurrentEnemy(bool enemyTurn, EnemyUnit unit)
        {
            enemysTurn = enemyTurn;
            selectedEUnit = unit;
        }



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
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            if (frameNum > 199)
            {
                spriteBatch.DrawString(gameVars.Font1Bold, "PROJECT LO-FI", new Vector2((graphicsDevice.Viewport.Width / 2) - 375,
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
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
            spriteBatch.Draw(gameVars.screens[0], new Rectangle(0, 0, 1200, 780), Color.White);
        }//end drawmenu


        public void DrawWon(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int level, int maxLevel)
        {
            if (level <= maxLevel - 1)
            {
                spriteBatch.Draw(gameVars.screens[1], new Rectangle(0, 0, 1200, 780), Color.White);
            }
            else
            {
                spriteBatch.Draw(gameVars.screens[2], new Rectangle(0, 0, 1200, 780), Color.White);
            }
        }

        /// <summary>
        /// Draw the Game Credits.
        /// </summary>
        /// <param name="gameVars"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        public void DrawCredits(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(gameVars.screens[3], new Rectangle(0, 0, 1200, 780), Color.White);
        }

        //draw text information for player
        public void DrawGameInfo(GameVariables gameVars, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, TurnState turn)
        {
            spriteBatch.DrawString(gameVars.Font1, "Turns remaining: " + log[0], new Vector2(10, 660), 
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            if(pMoved)
            {
                spriteBatch.DrawString(gameVars.Font1, log[2] + " moved", new Vector2(10, 680),
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            if (eMoved)
            {
                spriteBatch.DrawString(gameVars.Font1, log[4] + " moved", new Vector2(10, 680),
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }

            if(attacked)
            {
                string combat = log[7] + " attacks " + log[8] + " and deals " + log[6] + " damage.";

                spriteBatch.DrawString(gameVars.Font1, combat, new Vector2(10, 680),
                Color.DarkRed, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
        }//end draw gameinfo



    }//End of Draw class
}
