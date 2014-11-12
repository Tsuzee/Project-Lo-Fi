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

        public Level(string mapFileName, string playerFileName, string enemyFileName, GameVariables gameVar)
        {
            mapTextures = new List<Texture2D>();
            charTextures = new List<Texture2D>();
            enemyTextures = new List<Texture2D>();
            playerList = new List<PlayerUnit>();
            enemyList = new List<EnemyUnit>();

            AddTextures(gameVar);
            ConstructMap(mapFileName, playerFileName, enemyFileName);
        }

        /// --  End of Constructors --



        /// --  Methods --

        /// <summary>
        /// Wrapper method that calls the appropriate read file methods
        /// </summary>
        /// <param name="mapFile"> Name of the file storing the map. </param>
        /// <param name="charFile"> Name of the file storing the players. </param>
        /// <param name="enemyFile"> Name of the file storing the enemies. </param>
        public void ConstructMap(string mapFile, string charFile, string enemyFile)
        {
            ReadMap(mapFile);
            ReadPlayers(charFile);
            ReadEnemies(enemyFile);
        }

        /// <summary>
        /// Reads in terrain data and puts it in the map - should eventually handle all gridOccupant objects (units, enemies)
        /// </summary>
        /// <param name="mapName"> The name of the file storing our map. </param>
        public void ReadMap(string mapName)
        {
            try
            {
                string[] allLines = File.ReadAllLines(mapName); // Reading all the lines from a text file

                /* Iterate through allLines array. Select each line from allLines and split it using commas as the delimeter. 
                 * Add values that where separated to a jagged array. */
                string[][] jaggedArray = allLines.Select(line => line.Split(',')).ToArray();
 
                mapGrid = new GridOccupant[jaggedArray.GetLength(0), jaggedArray[0].Length];    // Set the size of mapGrid

                for (int i = 0; i < jaggedArray.Length; i++)
                {
                    for (int j = 0; j < jaggedArray[i].Length; j++)
                    {
                        int terrainNumber = Int32.Parse(jaggedArray[i][j]);     // Get the appropriate index for mapTextures
                        Terrain gridSpace = new Terrain(i, j);                  // Make a new terrain instance to add
                        gridSpace.Img = mapTextures[terrainNumber];             // Add the appropriate image from mapTextures

                        mapGrid[i, j] = gridSpace;
                    }
                }
          
            }
            catch(FileNotFoundException fnfe)       // Basically takes the place of If(File.Exists(mapName))
            {

            }
            catch(Exception ex)
            {

            }
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
        /// Temporary method to add enemy units to the map - will want to consolidate all map stuff into one method eventually
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadEnemies(string fileName)
        {
            Stream inStream = null;
            StreamReader input = null;

            try
            {
                inStream = File.OpenRead(fileName);     // Open the player list for reading
                input = new StreamReader(inStream);

                while (input.EndOfStream == false)      // While there's still stuff in the file
                {
                    string enemyData = input.ReadLine();
                    string[] splitEnemyData = enemyData.Split(',');
                    int xCoord = Int32.Parse(splitEnemyData[0]);
                    int yCoord = Int32.Parse(splitEnemyData[1]);
                    int enemyTextureNum = Int32.Parse(splitEnemyData[2]);

                    EnemyUnit eUnit = new EnemyUnit(xCoord, yCoord);
                    eUnit.Img = enemyTextures[enemyTextureNum];

                    //add enemy to the list
                    enemyList.Add(eUnit);

                    Terrain tempStorage = (Terrain)mapGrid[xCoord, yCoord];
                    eUnit.OccupiedSpace = tempStorage;
                    mapGrid[xCoord, yCoord] = eUnit;
                }
            }
            catch (FileNotFoundException fnfe)
            {

            }
            catch (Exception ex)
            {

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
}
