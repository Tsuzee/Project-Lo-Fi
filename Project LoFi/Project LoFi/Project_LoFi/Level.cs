// Aliaksandr Shumski
// Level class creates level for the game. 
// Has AddTextureToTheList method and Draw method

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Project_LoFi
{
    public class Level
    {
        /// --  Instance Variables  --

        private GridOccupant[,] mapGrid;                // A 2D-array to store the map while we build it in level, before passing it to game.
        private List<Texture2D> mapTextures;            // Store textures for maps
        private List<Texture2D> enemyTextures;          // Store textures for maps
        private List<Texture2D> charTextures;           // Store textures for maps
        private List<PlayerUnit> playerList;            // Store the players, so Game1 knows who the human can control
        public List<EnemyUnit> enemyList;
        public List<Item> itemList;
        private List<EnemyUnit> enemyDataBase;

        /// -- End of Instance Variables    --



        /// --  Properties  --

        public GridOccupant[,] MapGrid
        {
            get { return mapGrid; }
        }

        public List<PlayerUnit> PlayerList
        {
            set { playerList = value; }
            get { return playerList; }
        }

        /// --  End of Properties   --



        /// --  Constructors    --

        public Level(string mapFileName, string itemDBFileName, string playerListFileName, string monsterDBFileName, GameVariables gameVar)
        {
            mapTextures = new List<Texture2D>();
            charTextures = new List<Texture2D>();
            enemyTextures = new List<Texture2D>();
            playerList = new List<PlayerUnit>();
            enemyList = new List<EnemyUnit>();
            itemList = new List<Item>();
            enemyDataBase = new List<EnemyUnit>();

            AddTextures(gameVar);
            ReadItems(itemDBFileName);
            ReadPlayers(playerListFileName);
            ReadMonsters(monsterDBFileName);
            // Note that items, players, and monsters MUST be read in that order, and MUST be read before the map
            ReadMap(mapFileName);
        }

        /// --  End of Constructors --



        /// --  Methods --

        /// <summary>
        /// Wrapper method that calls the appropriate read file methods
        /// </summary>
        /// <param name="mapFile"> Name of the file storing the map. </param>
        /// <param name="charFile"> Name of the file storing the players. </param>
        /// <param name="enemyFile"> Name of the file storing the enemies. </param>
        public void ConstructMap(string mapFile, string charFile = null, string enemyFile = null)
        {
            ReadMap(mapFile);
            //ReadPlayers(charFile); may be needed later
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public void ReadItems(string databaseName)
        {
            Stream input = null;
            StreamReader inStream = null;
            try
            {
                input = File.OpenRead(databaseName);
                inStream = new StreamReader(input);

                string currentLine = inStream.ReadLine();
                while (inStream.EndOfStream == false)
                {
                    if (currentLine.Contains('*') == false)     // If it's not a comment line
                    {
                        string[] splitItemData = currentLine.Split(',');
                        int itemTextureNum = Int32.Parse(splitItemData[0]);     // Currently unused
                        string name = splitItemData[1];
                        int strMod = Int32.Parse(splitItemData[2]);
                        int dexMod = Int32.Parse(splitItemData[3]);
                        int magMod = Int32.Parse(splitItemData[4]);
                        int amrMod = Int32.Parse(splitItemData[5]);
                        int dmgMod = Int32.Parse(splitItemData[6]);
                        double critMod = Double.Parse(splitItemData[7]);
                        string type = splitItemData[8];
                        string desc = splitItemData[9];

                        Item newItem = new Item(name, strMod, dexMod, magMod, amrMod, dmgMod, critMod, type, desc);
                        //newItem.Img = itemTextures[itemTextureNum];

                        //add item to the list
                        itemList.Add(newItem);
                    }
                    currentLine = inStream.ReadLine();
                }// End of while (enemy stuff)
            }
            catch (FileNotFoundException fnfe)       // Basically takes the place of If(File.Exists(mapName))
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (inStream != null)
                    inStream.Dispose();
                if (input != null)
                    input.Dispose();
            }
        }


        /// <summary>
        /// Temporary method to add player units to the map - will want to consolidate all map stuff into one method eventually
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadPlayers(string fileName)
        {
            Stream input = null;
            StreamReader inStream = null;

            try
            {
                input = File.OpenRead(fileName);     // Open the player list for reading
                inStream = new StreamReader(input);

                string currentLine = inStream.ReadLine();
                while (inStream.EndOfStream == false)      // While there's still stuff in the file
                {
                    if (currentLine.Contains('*') == false)     // If it's not a comment line
                    {
                        int xCoord = 0;
                        int yCoord = 0;
                        string[] splitPlayerData = currentLine.Split(',');
                        int playerTextureNum = Int32.Parse(splitPlayerData[0]);
                        string name = splitPlayerData[1];
                        int health = Int32.Parse(splitPlayerData[2]);
                        int defense = Int32.Parse(splitPlayerData[3]);
                        int attack = Int32.Parse(splitPlayerData[4]);
                        double crit = Double.Parse(splitPlayerData[5]);
                        int lvl = Int32.Parse(splitPlayerData[6]);
                        int str = Int32.Parse(splitPlayerData[7]);
                        int dex = Int32.Parse(splitPlayerData[8]);
                        int mag = Int32.Parse(splitPlayerData[9]);
                        int curXP = Int32.Parse(splitPlayerData[10]);
                        int eqpWpn = Int32.Parse(splitPlayerData[11]);      // Currently unused
                        int eqpAmr = Int32.Parse(splitPlayerData[12]);      // Currently unused

                        PlayerUnit pUnit = new PlayerUnit(xCoord, yCoord, name, health, defense, attack, crit, lvl, str, dex, mag, curXP);
                        pUnit.Img = charTextures[playerTextureNum];

                        //add player to the list
                        playerList.Add(pUnit);
                    }
                    currentLine = inStream.ReadLine();
                }// End of while
            }
            catch(FileNotFoundException fnfe)
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (inStream != null)
                    inStream.Dispose();
                if (input != null)
                    input.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public void ReadMonsters(string databaseName)
        {
            Stream input = null;
            StreamReader inStream = null;
            try
            {
                input = File.OpenRead(databaseName);
                inStream = new StreamReader(input);

                // Enemy stuff
                string currentLine = inStream.ReadLine();
                while (inStream.EndOfStream == false)
                {
                    if (currentLine.Contains('*') == false)     // If it's not a comment line
                    {
                        int xCoord = 0;
                        int yCoord = 0;
                        string[] splitEnemyData = currentLine.Split(',');
                        int enemyTextureNum = Int32.Parse(splitEnemyData[0]);
                        string name = splitEnemyData[1];
                        int health = Int32.Parse(splitEnemyData[2]);
                        int defense = Int32.Parse(splitEnemyData[3]);
                        int attack = Int32.Parse(splitEnemyData[4]);
                        double crit = Double.Parse(splitEnemyData[5]);
                        int lvl = Int32.Parse(splitEnemyData[6]);
                        int str = Int32.Parse(splitEnemyData[7]);
                        int dex = Int32.Parse(splitEnemyData[8]);
                        int mag = Int32.Parse(splitEnemyData[9]);
                        int xpDrop = Int32.Parse(splitEnemyData[10]);
                        int boss = Int32.Parse(splitEnemyData[11]);
                        int wpnIndex = Int32.Parse(splitEnemyData[12]);     // Currently unused
                        int amrIndex = Int32.Parse(splitEnemyData[13]);     // Currently unused

                        EnemyUnit eUnit = new EnemyUnit(xCoord, yCoord, name, health, defense, attack, crit, lvl, str, dex, mag, xpDrop, boss);
                        eUnit.Img = enemyTextures[enemyTextureNum];

                        //add enemy to the list
                        enemyDataBase.Add(eUnit);
                    }
                    currentLine = inStream.ReadLine();
                }// End of while (enemy stuff)
            }
            catch (FileNotFoundException fnfe)       // Basically takes the place of If(File.Exists(mapName))
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (inStream != null)
                    inStream.Dispose();
                if (input != null)
                    input.Dispose();
            }
        }



        /// <summary>
        /// Reads in terrain data and puts it in the map - should eventually handle all gridOccupant objects (units, enemies)
        /// </summary>
        /// <param name="mapName"> The name of the file storing our map. </param>
        public void ReadMap(string mapName)
        {
            Stream input = null;
            StreamReader inStream = null;
            try
            {
                input = File.OpenRead(mapName);
                inStream = new StreamReader(input);

                int playerIndexer = 0;
                int mapWidth = 0;
                int mapHeight = 0;

                mapWidth = Int32.Parse(inStream.ReadLine());
                mapHeight = Int32.Parse(inStream.ReadLine());

                mapGrid = new GridOccupant[mapWidth, mapHeight];

                for (int j = 0; j < mapHeight; j++)
                {
                    for (int i = 0; i < mapWidth; i++)
                    {
                        string data = inStream.ReadLine();
                        if (data.Contains("*"))
                            i--;                // Comment line, ignore
                        else
                        {
                            string[] splitString = data.Split(',');

                            int typeNum = Int32.Parse(splitString[0]);

                            int terrainNumber = Int32.Parse(splitString[1]);

                            bool cannotPass = false;
                            if (Int32.Parse(splitString[2]) == 1)
                                cannotPass = true;

                            int dMod = Int32.Parse(splitString[3]);

                            Terrain gridSpace = new Terrain(i, j, cannotPass, dMod);
                            gridSpace.Img = mapTextures[terrainNumber];

                            if (typeNum == 0)
                                mapGrid[i, j] = gridSpace;
                            else if (typeNum == 1)
                            {
                                PlayerUnit pUnit = playerList[playerIndexer];   // Grab the next player in the list
                                pUnit.OccupiedSpace = gridSpace;
                                pUnit.X = i;
                                pUnit.Y = j;
                                mapGrid[i, j] = pUnit;
                                playerIndexer++;                                // Move the indexer to the next player
                            }
                            else if (typeNum >= 2)
                            {
                                EnemyUnit eUnit = enemyDataBase[typeNum - 2];       // Grab the appropriate enemy - minus 2 to offset 0 for terrain and 1 for player
                                eUnit.OccupiedSpace = gridSpace;
                                eUnit.X = i;
                                eUnit.Y = j;
                                mapGrid[i, j] = eUnit;
                                enemyList.Add(eUnit);
                            }

                        }
                    }// End of inner (i) for
                }// End of outer (j) for

            }
            catch (FileNotFoundException fnfe)       // Basically takes the place of If(File.Exists(mapName))
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (inStream != null)
                    inStream.Dispose();
                if (input != null)
                    input.Dispose();
            }
        }

        /// <summary>
        /// Helper method that adds all the relevant game textures to our mapTextures list
        /// </summary>
        /// <param name="gv"> Uses Game1's GameVariables object to get texture data </param>
        private void AddTextures(GameVariables gv)
        {
            for (int i = 0; i < gv.terrainTextures.Length; i++)
                mapTextures.Add(gv.terrainTextures[i]);

            for (int i = 0; i < gv.enemyTextures.Length; i++)
                enemyTextures.Add(gv.enemyTextures[i]);

            for (int i = 0; i < gv.charTextures.Length; i++)
                charTextures.Add(gv.charTextures[i]);
        }

        /*
        public void ResetGameForNextLevel()
        {
            PlayerList.Clear();
            enemyList.Clear();
            mapTextures.Clear();
            enemyTextures.Clear();
            charTextures.Clear();
            itemList.Clear();
        }*/

        /// --  End of Methods  --
    
    }//End of Level class

}
