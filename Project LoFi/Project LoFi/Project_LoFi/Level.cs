﻿// Aliaksandr Shumski
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

        public Level(string mapFileName, GameVariables gameVar)
        {
            mapTextures = new List<Texture2D>();
            charTextures = new List<Texture2D>();
            enemyTextures = new List<Texture2D>();
            playerList = new List<PlayerUnit>();
            enemyList = new List<EnemyUnit>();

            AddTextures(gameVar);
            ConstructMap(mapFileName);
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
        /// Temporary method to add player units to the map - will want to consolidate all map stuff into one method eventually
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadPlayers(string fileName)
        {
            Stream inStream = null;
            StreamReader input = null;

            try
            {
                inStream = File.OpenRead(fileName);     // Open the player list for reading
                input = new StreamReader(inStream);

                while (input.EndOfStream == false)      // While there's still stuff in the file
                {
                    // Read & parse file data
                    string playerData = input.ReadLine();
                    string[] splitPlayerData = playerData.Split(',');
                    int xCoord = Int32.Parse(splitPlayerData[0]);
                    int yCoord = Int32.Parse(splitPlayerData[1]);
                    int charTextureNum = Int32.Parse(splitPlayerData[2]);

                    // Create player unit object
                    PlayerUnit pUnit = new PlayerUnit(xCoord, yCoord);
                    pUnit.Img = charTextures[charTextureNum];
                    pUnit.Name = charTextureNum.ToString();

                    // Add to playerList
                    playerList.Add(pUnit);

                    // Add to map
                    Terrain tempStorage = (Terrain)mapGrid[xCoord, yCoord];
                    pUnit.OccupiedSpace = tempStorage;
                    mapGrid[xCoord, yCoord] = pUnit;
                }
            }
            catch(FileNotFoundException fnfe)
            {

            }
            catch(Exception ex)
            {

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

                List<Point> enemyPositions = new List<Point>();         // Hold enemy positions
                List<Point> playerPositions = new List<Point>();        // Hold player positions

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
                            if (typeNum == 1)
                                playerPositions.Add(new Point(i, j));
                            else if (typeNum == 2)
                                enemyPositions.Add(new Point(i, j));

                            int terrainNumber = Int32.Parse(splitString[1]);

                            bool cannotPass = false;
                            if (Int32.Parse(splitString[2]) == 1)
                                cannotPass = true;

                            int dMod = Int32.Parse(splitString[3]);

                            Terrain gridSpace = new Terrain(i, j, cannotPass, dMod);
                            gridSpace.Img = mapTextures[terrainNumber];

                            mapGrid[i, j] = gridSpace;
                        }
                    }
                }

                // Enemy stuff
                string currentLine = inStream.ReadLine();
                int indexer = 0;
                while (inStream.EndOfStream == false && currentLine.Contains('#') == false)
                {
                    if (currentLine.Contains('*') == false)     // If it's not a comment line
                    {
                        int xCoord = enemyPositions[indexer].X;
                        int yCoord = enemyPositions[indexer].Y;
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

                        EnemyUnit eUnit = new EnemyUnit(xCoord, yCoord, name, health, defense, attack, crit, lvl, str, dex, mag, xpDrop);
                        eUnit.Img = enemyTextures[enemyTextureNum];

                        //add enemy to the list
                        enemyList.Add(eUnit);

                        Terrain tempStorage = (Terrain)mapGrid[xCoord, yCoord];
                        eUnit.OccupiedSpace = tempStorage;
                        mapGrid[xCoord, yCoord] = eUnit;
                        indexer++;
                    }
                    currentLine = inStream.ReadLine();
                }// End of while (enemy stuff)

                //Player Stuff
                currentLine = inStream.ReadLine();
                indexer = 0;
                while (inStream.EndOfStream == false)
                {
                    if (currentLine.Contains('*') == false)     // If it's not a comment line
                    {
                        int xCoord = playerPositions[indexer].X;
                        int yCoord = playerPositions[indexer].Y;
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

                        PlayerUnit pUnit = new PlayerUnit(xCoord, yCoord, name, health, defense, attack, crit, lvl, str, dex, mag, curXP);
                        pUnit.Img = charTextures[playerTextureNum];

                        //add enemy to the list
                        playerList.Add(pUnit);

                        Terrain tempStorage = (Terrain)mapGrid[xCoord, yCoord];
                        pUnit.OccupiedSpace = tempStorage;
                        mapGrid[xCoord, yCoord] = pUnit;
                        indexer++;
                    }
                    currentLine = inStream.ReadLine();
                }// End of while (player stuff)
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

        /// --  End of Methods  --
    
    }//End of Level class

    public class Point
    {
        int x;
        int y;

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Point(int i, int j)
        {
            x = i;
            y = j;
        }
    }

}
