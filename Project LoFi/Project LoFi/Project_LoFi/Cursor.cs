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
    class Cursor
    {
        Rectangle cursorPos;
        public bool isVisible;
        
        public Rectangle CursorPos
        {
            get { return cursorPos;}
            set { cursorPos = value; }
        }

        //constructor to set up the cursor in pos 0,0
        public Cursor()
        {
            CursorPos = new Rectangle(0, 0, GameVariables.textureWidth, GameVariables.textureHeight);
            isVisible = true;
        }


        /// <summary>
        /// Draw the map
        /// </summary>
        /// <param name="drawTexture"></param>
        public void Draw(SpriteBatch drawTexture, Texture2D texture)
        {
            // draw that texture if it is the players turn
            if (isVisible)
            {
                drawTexture.Draw(texture, new Rectangle(CursorPos.X * GameVariables.textureWidth,
                    CursorPos.Y * GameVariables.textureHeight, GameVariables.textureWidth,
                    GameVariables.textureHeight), Color.White);
            }
        }

    }//end class
}
