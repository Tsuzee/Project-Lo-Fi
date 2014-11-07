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
        public Texture2D selCursor;
        public Texture2D highligther;
        public Texture2D battleHighLighter;
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

            terrainTextures = new Texture2D[50];        // Default size of 20 - adjust as necessary
            charTextures = new Texture2D[6];
            enemyTextures = new Texture2D[15];
        }

        /// --  End of Constructors --

        /// <summary>
        /// Loads images using the content manager and assigns them to the appropriate terrain positions
        /// </summary>
        public void setImages()
        {
            logo = myContent.Load<Texture2D>("logo");
            cursor = myContent.Load<Texture2D>("cursor");
            selCursor = myContent.Load<Texture2D>("cursorS");
            highligther = myContent.Load<Texture2D>("highlight");
            battleHighLighter = myContent.Load<Texture2D>("battlehighlight");

            // Terrain textures
            terrainTextures[0] = myContent.Load<Texture2D>("Textures\\lightGrass");
            terrainTextures[1] = myContent.Load<Texture2D>("Textures\\NarrowRoad");
            terrainTextures[2] = myContent.Load<Texture2D>("Textures\\LeftAngleRoad");
            terrainTextures[3] = myContent.Load<Texture2D>("Textures\\RoadUp");
            terrainTextures[4] = myContent.Load<Texture2D>("Textures\\TRoad");
            terrainTextures[5] = myContent.Load<Texture2D>("Textures\\BottomConerRoad");
            terrainTextures[6] = myContent.Load<Texture2D>("Textures\\FlippedTRoad");
            terrainTextures[7] = myContent.Load<Texture2D>("Textures\\TopRightAngleRoad");
            terrainTextures[8] = myContent.Load<Texture2D>("Textures\\lightWater");
            terrainTextures[9] = myContent.Load<Texture2D>("Textures\\ShipOnWater");
            terrainTextures[10] = myContent.Load<Texture2D>("Textures\\HalfGrassAndStones");
            terrainTextures[11] = myContent.Load<Texture2D>("Textures\\AngleStoneAndGrass");
            terrainTextures[12] = myContent.Load<Texture2D>("Textures\\BottomRightCornerStoneAndGrass");
            terrainTextures[13] = myContent.Load<Texture2D>("Textures\\BottomLeftStoneCornerAndGrass");
            terrainTextures[14] = myContent.Load<Texture2D>("Textures\\LeftSideStoneRightSideGrass");
            terrainTextures[15] = myContent.Load<Texture2D>("Textures\\StoneLeftBottomCornerAndGrass");
            terrainTextures[16] = myContent.Load<Texture2D>("Textures\\AngleHouse");
            terrainTextures[17] = myContent.Load<Texture2D>("Textures\\SingleHouse");
            terrainTextures[18] = myContent.Load<Texture2D>("Textures\\GoldRoofHouse");
            terrainTextures[19] = myContent.Load<Texture2D>("Textures\\MarketPlace");
            terrainTextures[20] = myContent.Load<Texture2D>("Textures\\TopLeftCornerBuilding");
            terrainTextures[21] = myContent.Load<Texture2D>("Textures\\TopRightCornerBuilding");
            terrainTextures[22] = myContent.Load<Texture2D>("Textures\\TopTower");
            terrainTextures[23] = myContent.Load<Texture2D>("Textures\\AngleWall");
            terrainTextures[24] = myContent.Load<Texture2D>("Textures\\MiddleWall");
            terrainTextures[25] = myContent.Load<Texture2D>("Textures\\RightSideAngleWall");
            terrainTextures[26] = myContent.Load<Texture2D>("Textures\\SideRightWall");
            terrainTextures[27] = myContent.Load<Texture2D>("Textures\\Palace");
            terrainTextures[28] = myContent.Load<Texture2D>("Textures\\Fountain");
            terrainTextures[29] = myContent.Load<Texture2D>("Textures\\GreylightRoad");
            terrainTextures[30] = myContent.Load<Texture2D>("Textures\\ManInGreen");
            terrainTextures[31] = myContent.Load<Texture2D>("Textures\\SecondManInGreen");
            terrainTextures[32] = myContent.Load<Texture2D>("Textures\\ManBottom");
            terrainTextures[33] = myContent.Load<Texture2D>("Textures\\Forest");
            terrainTextures[34] = myContent.Load<Texture2D>("Textures\\stump");
            terrainTextures[35] = myContent.Load<Texture2D>("Textures\\Sign");
            terrainTextures[36] = myContent.Load<Texture2D>("Textures\\RockAndGrass");
            terrainTextures[37] = myContent.Load<Texture2D>("Textures\\RockAndGrass2");
            terrainTextures[38] = myContent.Load<Texture2D>("Textures\\lightSand");
            terrainTextures[39] = myContent.Load<Texture2D>("Textures\\FirstStepToTheDesert");
            terrainTextures[40] = myContent.Load<Texture2D>("Textures\\SandStepToTheLeft");
            terrainTextures[41] = myContent.Load<Texture2D>("Textures\\lightLavaRoad");
            terrainTextures[42] = myContent.Load<Texture2D>("Textures\\HalfLava");
            terrainTextures[43] = myContent.Load<Texture2D>("Textures\\chestsOnLava");
            terrainTextures[44] = myContent.Load<Texture2D>("Textures\\Torch");
            terrainTextures[45] = myContent.Load<Texture2D>("Textures\\Wheil");
            terrainTextures[46] = myContent.Load<Texture2D>("Textures\\GoblinHouse");
            terrainTextures[47] = myContent.Load<Texture2D>("Textures\\HayStack");


            // Enemy textures
            enemyTextures[0] = myContent.Load<Texture2D>("Textures\\Devil_Scorpion");
            enemyTextures[1] = myContent.Load<Texture2D>("Textures\\Wyvern_Chick");
            enemyTextures[2] = myContent.Load<Texture2D>("Textures\\Boss1");
            enemyTextures[3] = myContent.Load<Texture2D>("Textures\\Mauler");
            enemyTextures[4] = myContent.Load<Texture2D>("Textures\\Goblin");
            enemyTextures[5] = myContent.Load<Texture2D>("Textures\\Nightmare");
            enemyTextures[6] = myContent.Load<Texture2D>("Textures\\Tornado_Lizard");


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
