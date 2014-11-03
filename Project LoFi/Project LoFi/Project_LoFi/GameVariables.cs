// Terrain textures were taking from http://opengameart.org/textures/ just for testing purposes.
// Characters and Monsters texture were taking from http://untamed.wild-refuge.net/rmxpresources.php?characters just for testing purposes.


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

// If all this holds are *texture* data, we should consider renaming the class to something more descriptive (like TextureData.cs)

// Whoever made this should shove their name in here somewhere

namespace Project_LoFi
{
    public class GameVariables
    {
        
        private ContentManager myContent;           // Allows us to use ContentManager.Load()
    
        //static texture sizes
        public static int textureWidth = 60;
        public static int textureHeight = 60;

        //add images, fonts, music, sounds, and other content here.
        /// --  Fonts  --
        public SpriteFont Font1;
        public SpriteFont Font1Bold;

        /// -- Images --
        public Texture2D logo;
        public Texture2D cursor;
        public Texture2D[] terrainTextures;
        /*
         * [0] = grass
         * [1] = sand
         * [2] = lava
         * [3] = flower
         * [4] = road
         */
        public Texture2D[] enemyTextures;
        public Texture2D[] charTextures;
       
        /// --  Constructors    --

        public GameVariables(ContentManager content)
        {
            myContent = content;

            terrainTextures = new Texture2D[20];        // Default size of 20 - adjust as necessary
            charTextures = new Texture2D[6];
            enemyTextures = new Texture2D[10];
        }

        /// --  End of Constructors --

        /// <summary>
        /// Loads images using the content manager and assigns them to the appropriate terrain positions
        /// </summary>
        public void setImages()
        {
            logo = myContent.Load<Texture2D>("logo");
            cursor = myContent.Load<Texture2D>("cursor");

            // Terrain textures
            terrainTextures[0] = myContent.Load<Texture2D>("Textures\\grass");
            terrainTextures[1] = myContent.Load<Texture2D>("Textures\\sand");
            terrainTextures[2] = myContent.Load<Texture2D>("Textures\\lava");
            terrainTextures[3] = myContent.Load<Texture2D>("Textures\\flowers");
            terrainTextures[4] = myContent.Load<Texture2D>("Textures\\road");

            // Enemy textures
            enemyTextures[0] = myContent.Load<Texture2D>("Textures\\devil");
            enemyTextures[1] = myContent.Load<Texture2D>("Textures\\shiva");
            enemyTextures[2] = myContent.Load<Texture2D>("Textures\\goldbat");

            // Character textures
            charTextures[0] = myContent.Load<Texture2D>("Textures\\indianajones");
            charTextures[1] = myContent.Load<Texture2D>("Textures\\grannyweatherwax");
            charTextures[2] = myContent.Load<Texture2D>("Textures\\tremel");
        }


        //set font
        public void setFont()
        {
            Font1 = myContent.Load<SpriteFont>("Font1");
            Font1Bold = myContent.Load<SpriteFont>("Font1Bold");
        }
    }//end class
}
