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
        int frameNum = 0;

        GameVariables gameVars;
        SpriteBatch spriteBatch;
        SpriteBatch drawTexture;
        GraphicsDevice graphicsDevice;
        PlayerUnit selectedUnit;
        public List<EnemyUnit> enemyList;

        public PlayerUnit SelectedUnit
        {
            set { selectedUnit = value; }
        }


        // -- Constructor --
        public Drawer(GameVariables vars, SpriteBatch sprite, GraphicsDevice device)
        {
            gameVars = vars;
            spriteBatch = sprite;
            drawTexture = sprite;
            graphicsDevice = device;
            SelectedUnit = null;
        }


        //  --  Methods --

        //keep graphics device updated
        public void update(GraphicsDevice device, SpriteBatch sprite)
        {
            graphicsDevice = device;
            spriteBatch = sprite;
            drawTexture = sprite;
        }

        /// <summary>
        /// Draws the map and the units on it
        /// </summary>
        /// <param name="drawTexture"></param>
        public void DrawMap(GridOccupant[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Terrain)
                    {
                        
                        Texture2D terrainTxtr = map[i, j].Img;
                        drawTexture.Draw(terrainTxtr, new Rectangle(i * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                        if (selectedUnit != null)
                        {
                            if (((selectedUnit.X == i - 1) && selectedUnit.Y == j) || ((selectedUnit.X == i + 1) && selectedUnit.Y == j)
                                || ((selectedUnit.Y == j - 1) && selectedUnit.X == i) || ((selectedUnit.Y == j + 1) && selectedUnit.X == i))
                            {
                                drawTexture.Draw(gameVars.highligther, new Rectangle(i * txtrWidth, j * txtrHeight, txtrWidth, txtrHeight), Color.White);
                            }
                        }
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

                        //if player is attemping combat draw the battle highlighters
                        if (selectedUnit != null)
                        {
                            if(map[selectedUnit.X+1, selectedUnit.Y] is EnemyUnit)
                            {
                                drawTexture.Draw(gameVars.battleHighLighter, new Rectangle((selectedUnit.X + 1) * txtrWidth, selectedUnit.Y * txtrHeight, txtrWidth, txtrHeight), Color.White);
                            }
                            if(map[selectedUnit.X-1, selectedUnit.Y] is EnemyUnit)
                            {
                                drawTexture.Draw(gameVars.battleHighLighter, new Rectangle((selectedUnit.X - 1) * txtrWidth, selectedUnit.Y * txtrHeight, txtrWidth, txtrHeight), Color.White);
                            }
                            if(map[selectedUnit.X, selectedUnit.Y+1] is EnemyUnit)
                            {
                                drawTexture.Draw(gameVars.battleHighLighter, new Rectangle(selectedUnit.X * txtrWidth, (selectedUnit.Y + 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                            }
                            if(map[selectedUnit.X, selectedUnit.Y-1] is EnemyUnit)
                            {
                                drawTexture.Draw(gameVars.battleHighLighter, new Rectangle(selectedUnit.X * txtrWidth, (selectedUnit.Y - 1) * txtrHeight, txtrWidth, txtrHeight), Color.White);
                            }  
                        }
                    }
                }//End of inner for loop
            }//End of outer for loop
        }//End of DrawMap method


        /// <summary>
        /// Draw game intro
        /// </summary>
        public void DrawIntro()
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
        /// draws the game menu
        /// </summary>
        public void DrawMenu()
        {
            spriteBatch.DrawString(gameVars.Font1Bold, "PROJECT LO-FI", new Vector2((graphicsDevice.Viewport.Width / 2) - 375,
                                (graphicsDevice.Viewport.Height / 2) - 300), Color.DarkRed, 0.0f, Vector2.Zero, 5.0f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(gameVars.Font1Bold, "press Enter to play", new Vector2((graphicsDevice.Viewport.Width / 2) - 350,
                    (graphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);
        }//end drawmenu


        //draw Credits
        public void DrawCredits()
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

    }//End of Draw class
}
