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
        public Rectangle cursorPos;
        public bool isVisible;
        private bool selected;
        public Texture2D texture;
        private Texture2D selTexture;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        //constructor to set up the cursor in pos 0,0
        public Cursor()
        {
            cursorPos = new Rectangle(0, 0, GameVariables.textureWidth, GameVariables.textureHeight);
            isVisible = false;
            selected = false;
        }

        //set cursor textures
        public void setCursorTextures(Texture2D text, Texture2D text2)
        {
            texture = text;
            selTexture = text2;
         }
        /// <summary>
        /// Draw the map
        /// </summary>
        /// <param name="drawTexture"></param>
        public void Draw(SpriteBatch drawTexture)
        {
            // draw that texture if it is the players turn
            if (isVisible && !selected)
            {
                drawTexture.Draw(texture, new Rectangle(cursorPos.X * GameVariables.textureWidth,
                    cursorPos.Y * GameVariables.textureHeight, GameVariables.textureWidth,
                    GameVariables.textureHeight), Color.White);
            }
            else if(isVisible && selected)
            {
                drawTexture.Draw(selTexture, new Rectangle(cursorPos.X * GameVariables.textureWidth,
                    cursorPos.Y * GameVariables.textureHeight, GameVariables.textureWidth,
                    GameVariables.textureHeight), Color.White);

            }
        }

    }//end class
}
