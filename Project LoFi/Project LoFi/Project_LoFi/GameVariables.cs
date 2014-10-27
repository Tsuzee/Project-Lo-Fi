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
        public Texture2D lavaTexture;
        public Texture2D flowersTexture;
        public Texture2D roadTexture;
        public Texture2D player;
        public Texture2D monster1;
        public Texture2D monster2;
        public Texture2D monster3; 
       


        //set Images
        public void setImages()
        {
            Logo = myContent.Load<Texture2D>("logo");
            grassTexture = myContent.Load<Texture2D>("Textures\\grass");
            sandTexture = myContent.Load<Texture2D>("Textures\\sand");
            lavaTexture = myContent.Load<Texture2D>("Textures\\lava");
            flowersTexture = myContent.Load<Texture2D>("Textures\\flowers");
            roadTexture = myContent.Load<Texture2D>("Textures\\road");
            player = myContent.Load<Texture2D>("Textures\\indianajones");
            monster1 = myContent.Load<Texture2D>("Textures\\devil");
            monster2 = myContent.Load<Texture2D>("Textures\\shiva");
            monster3 = myContent.Load<Texture2D>("Textures\\goldbat");
       
        }


        //set font
        public void setFont()
        {
            Font1 = myContent.Load<SpriteFont>("Font1");
            Font1Bold = myContent.Load<SpriteFont>("Font1Bold");
        }
    }//end class
}
