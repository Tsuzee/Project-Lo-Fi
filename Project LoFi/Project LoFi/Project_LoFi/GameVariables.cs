// Terrain textures were taking from http://opengameart.org/textures/ just for testing purposes.
// Characters and Monsters texture were taking from http://untamed.wild-refuge.net/rmxpresources.php?characters just for testing purposes.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
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

        /// -- Screens --
        public Texture2D[] screens;

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
        public Image[] charImages;
       
        /// --  Constructors    --

        public GameVariables(ContentManager content)
        {
            myContent = content;

            terrainTextures = new Texture2D[130];        // Default size of 20 - adjust as necessary
            charTextures = new Texture2D[6];
            charImages = new Image[6];
            enemyTextures = new Texture2D[21];
            screens = new Texture2D[4];
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

            //game screens
            screens[0] = myContent.Load<Texture2D>("Main Menu2");
            screens[1] = myContent.Load<Texture2D>("Next Level");
            screens[2] = myContent.Load<Texture2D>("Win");
            screens[3] = myContent.Load<Texture2D>("Credits");

            // Terrain textures For Level 1
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

            //Terrain textures For Level 2
            terrainTextures[48] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientGates");
            terrainTextures[49] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientStonesBottom");
            terrainTextures[50] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientStonesLeftAngle");
            terrainTextures[51] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientStonesLeftSide");
            terrainTextures[52] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientStonesRightAngle");
            terrainTextures[53] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\AncientStonesRightSide");
            terrainTextures[54] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BeaconOnStone");
            terrainTextures[55] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BeaconOnSandCompleted");
            terrainTextures[56] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BridgeCenter");
            terrainTextures[57] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BridgeLeftSIde");
            terrainTextures[58] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BridgeRightSide");
            terrainTextures[59] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BrokenCornerstone");
            terrainTextures[60] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\Cactus2");
            terrainTextures[61] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\PurplePlant");
            terrainTextures[62] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\CrateOnStone");
            terrainTextures[63] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\DragonStatue");
            terrainTextures[64] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\GrassInStone");
            terrainTextures[65] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\CrumbledRock");
            terrainTextures[66] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\lava");
            terrainTextures[67] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StoneFloor");
            terrainTextures[68] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\CrackedBrick");
            terrainTextures[69] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StonePath");
            terrainTextures[70] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StonePathDown");
            terrainTextures[71] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StonePathLeft");
            terrainTextures[72] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StonePathUp");
            terrainTextures[73] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\SkullOnStone");
            terrainTextures[74] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BrokenFloor");
            terrainTextures[75] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\StatueInSand");
            terrainTextures[76] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BrazierLeftSide");
            terrainTextures[77] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BrazierWithFireLeftSide");
            terrainTextures[78] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BrazierWithFireRightSide");
            terrainTextures[79] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Ground\\BookWithStand");

            //Terrain textures for Level 3
            terrainTextures[80] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleCenter");
            terrainTextures[81] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleCenterMagicWall");
            terrainTextures[82] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleLeftSide");
            terrainTextures[83] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleLeftSideMagicWall");
            terrainTextures[84] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleRightSide");
            terrainTextures[85] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CarpetCastleRightSideMagicWall");
            terrainTextures[86] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBookRoomTable");
            terrainTextures[87] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBookShelf");
            terrainTextures[88] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBookShelfBottom");
            terrainTextures[89] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBottomSideWall");
            terrainTextures[90] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBottomSideWallAndRock");
            terrainTextures[91] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleBottomSideWallAndWood");
            terrainTextures[92] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCell3Guys");
            terrainTextures[93] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCellCell");
            terrainTextures[94] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCellDeadGuyAndAGirl");
            terrainTextures[95] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCellDoor");
            terrainTextures[96] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCellSide");
            terrainTextures[97] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCellSideWithTop");
            terrainTextures[98] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCornerRightSideWall");
            terrainTextures[99] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleCornerRightSideWallAndWood");
            terrainTextures[100] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleGround");
            terrainTextures[101] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleGroundWithCarpet");
            terrainTextures[102] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleGroundWithTable");
            terrainTextures[103] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleLeftCornerSideWall");
            terrainTextures[104] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleLeftSideWall");
            terrainTextures[105] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleRightSideWall");
            terrainTextures[106] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleRightSideWallLeftSideWood");
            terrainTextures[107] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleTopCornerAngleSideWall");
            terrainTextures[108] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleTopSideWall");
            terrainTextures[109] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleWoodFloorSide");
            terrainTextures[110] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleWoodFloorWithBed");
            terrainTextures[111] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleWoodFloorWithDresser");
            terrainTextures[112] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleTopCornerAngleSideWallAndRock");
            terrainTextures[113] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleTopSideWallAndRock");
            terrainTextures[114] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\StoneGround");
            terrainTextures[115] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleLeftSideWallAndRock");
            terrainTextures[116] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Ground\\CastleLeftCornerSideWallAndRock");



            // Enemy textures Level 1
            enemyTextures[0] = myContent.Load<Texture2D>("Textures\\Devil_Scorpion");
            enemyTextures[1] = myContent.Load<Texture2D>("Textures\\Wyvern_Chick");
            enemyTextures[2] = myContent.Load<Texture2D>("Textures\\Boss1");
            enemyTextures[3] = myContent.Load<Texture2D>("Textures\\Mauler");
            enemyTextures[4] = myContent.Load<Texture2D>("Textures\\Goblin");
            enemyTextures[5] = myContent.Load<Texture2D>("Textures\\Nightmare");
            enemyTextures[6] = myContent.Load<Texture2D>("Textures\\Tornado_Lizard");

            // Enemy textures Level 2
            enemyTextures[7] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Ant_Lion");
            enemyTextures[8] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Bone_Fighter");
            enemyTextures[9] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Boss2");
            enemyTextures[10] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Foul_Mummy");
            enemyTextures[11] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Living_Armor");
            enemyTextures[12] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Sentinel");
            enemyTextures[13] = myContent.Load<Texture2D>("Textures\\Level2Textures\\Monsters\\Zombie");

            
            //Enemy textures Level 3
            enemyTextures[14] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\boss3");
            enemyTextures[15] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\Deadbeard");
            enemyTextures[16] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\Hummer");
            enemyTextures[17] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\Lich_2");
            enemyTextures[18] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\Satrage");
            enemyTextures[19] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\Tiamat");
            enemyTextures[20] = myContent.Load<Texture2D>("Textures\\Level3Textures\\Monsters\\WhiteKnight");


            // Character textures
            charTextures[0] = myContent.Load<Texture2D>("Textures\\indianajones");
            charTextures[1] = myContent.Load<Texture2D>("Textures\\grannyweatherwax");
            charTextures[2] = myContent.Load<Texture2D>("Textures\\tremel");

            // Fancy way to load charactr textures to images for the character sheet without needing file paths.
            
            //player 1
            MemoryStream mem = new MemoryStream();
            charTextures[0].SaveAsPng(mem, 50, 60);
            Image temp = Image.FromStream(mem);
            charImages[0] = temp;

            //player 2
            mem = new MemoryStream();
            charTextures[1].SaveAsPng(mem, 45, 60);
            temp = Image.FromStream(mem);
            charImages[1] = temp;

            //player 3
            mem = new MemoryStream();
            charTextures[2].SaveAsPng(mem, 50, 60);
            temp = Image.FromStream(mem);
            charImages[2] = temp;
        }


        //set font
        public void setFont()
        {
            Font1 = myContent.Load<SpriteFont>("Font1");
            Font1Bold = myContent.Load<SpriteFont>("Font1Bold");
        }
    }//end class
}
