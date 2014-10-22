// Grass and Sand textures where taking from http://opengameart.org/textures/ just for testing purposes.


using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace Project_LoFi
{
    class GameVariables
    {
        
        private ContentManager myContent;
    

        public GameVariables(ContentManager content)
        {
            myContent = content;
        }
        
        //add images, fonts, music, sounds, and other content here.
        /// --  Fonts  --
        public SpriteFont Font1;
        public SpriteFont Font1Bold;

        /// -- Images --
        public Texture2D Logo;
        public Texture2D grassTexture;
        public Texture2D sandTexture;
       


        //set Images
        public void setImages()
        {
            Logo = myContent.Load<Texture2D>("logo");
            grassTexture = myContent.Load<Texture2D>("Textures\\grass");
            sandTexture = myContent.Load<Texture2D>("Textures\\sand");
       
        }


        //set font
        public void setFont()
        {
            Font1 = myContent.Load<SpriteFont>("Font1");
            Font1Bold = myContent.Load<SpriteFont>("Font1Bold");
        }
    }//end class
}
