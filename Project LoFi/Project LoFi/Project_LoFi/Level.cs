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
        
     
        Terrain addingThisTerrain = new Terrain();
        List<Texture2D> mapTextures = new List<Texture2D>(); // Store textures for maps
        List<Texture2D> charactersAndMonsters = new List<Texture2D>();
        GridOccupant[,] gridOccupantsArray;
        PlayerUnit firstCharacter = new PlayerUnit();
        public PlayerUnit FirstCharacter
        {
            set { firstCharacter = value; }
            get { return firstCharacter; }
        }
        PlayerUnit secondCharacter = new PlayerUnit();
        public PlayerUnit SecondCharacter
        {
            set { secondCharacter = value; }
            get { return secondCharacter; }
        }
        PlayerUnit thirdCharacter = new PlayerUnit();
        public PlayerUnit ThirdCharacter
        {
            set { thirdCharacter = value; }
            get { return thirdCharacter; }
        }
        List<EnemyUnit> allMonsters = new List<EnemyUnit>();
        List<PlayerUnit> allPlayers = new List<PlayerUnit>();


        int textureHeight = 60; // used to keep the size of the texture
        int textureWidth = 60; // // used to keep the size of the texture
        int charactersWidth = 32;
        int charactersHeight = 48;

        public GridOccupant[,] GridOccupantsArray       //Public property so we can pass the map around between classes
        {
            set { gridOccupantsArray = value; }
            get { return gridOccupantsArray; }
        }

        

        /// <summary>
        ///  get array height
        /// </summary>
        public int mapWidth
        {
            get
            {

                return gridOccupantsArray.GetLength(1);    
            }
        }

        /// <summary>
        /// get array width
        /// </summary>
        public int mapHeight
        {
            get 
            {

                return gridOccupantsArray.GetLength(0);
            }
        }

        /// <summary>
        /// Reading map from the file.
        /// </summary>
        /// <param name="mapName"></param>
        public void readMap(string mapName)
        {
              if (File.Exists(mapName))
              {
                try
                {
                    string[] readAllLines = File.ReadAllLines(mapName); // Reading all the lines from a text file

                    //Iterate through readAllLines array.Select each line from readAllLines array and split it using comma as a delimeter. 
                    //Add values that where separated to a jagged array.
                    string[][] jaggedArray = readAllLines.Select(line => line.Split(',')).ToArray();  
 
                    gridOccupantsArray = new GridOccupant[jaggedArray.GetLength(0), jaggedArray[0].Length]; // set the size of the gridOccupantArray

                    for (int i = 0; i < jaggedArray.Length; i++)
                    {

                        for (int y = 0; y < jaggedArray[i].Length; y++)
                        {
                   
                            addingThisTerrain.X = i;
                            addingThisTerrain.Y = y;
                            addingThisTerrain.TextureIndex = int.Parse(jaggedArray[i][y]);
                            gridOccupantsArray[i, y] = addingThisTerrain; // adding terrain object to the gridOccupantArray

                            addingThisTerrain = new Terrain();
                        }

                    }
          
                }
                catch (Exception ex)
                { 
                    // I'm assuming this will be more fleshed out later?
                }
            }
        }


        /// <summary>
        /// adding texture to the list
        /// </summary>
        /// <param name="texture"></param>
        public void AddTextureToTheList(Texture2D textureAdd)
        {
            mapTextures.Add(textureAdd);
        }


        /// <summary>
        /// Adding characters and monsters to the list
        /// </summary>
        /// <param name="characterAndMonsterTextures"></param>
        public void AddCharacterAndMonstersToTheList(Texture2D characterAndMonsterTextures)
        {
            charactersAndMonsters.Add(characterAndMonsterTextures);
        }

        /// <summary>
        /// Draw the map
        /// </summary>
        /// <param name="drawTexture"></param>
        public void Draw(SpriteBatch drawTexture)
        {
            int texturePosition;
            Texture2D texture;
            for (int x = 0; x < mapHeight; x++)
            {
                for (int y = 0; y < mapWidth; y++)
                {

                    texturePosition = gridOccupantsArray[x, y].TextureIndex; // Get the texture index of the Terrain in the gridOccupantsArray
                    texture = mapTextures[texturePosition]; // get the texture number and assign it to the temporary texture variable

                    // draw that texture
                    drawTexture.Draw(
                        texture,
                        new Rectangle(x * textureWidth, y * textureHeight, textureWidth, textureHeight),
                        Color.White);
                        
                }
            }
        }

        /// <summary>
        /// Draw Characters and monsters on the screen
        /// </summary>
        /// <param name="drawTexture"></param>
        public void DrawCharactersAndMonsters(SpriteBatch drawTexture)
        {
            Texture2D characterTexture;
            int textureIndex = 0;
            Texture2D monstersTexture;
            foreach (PlayerUnit player in allPlayers)
            {
                textureIndex = player.TextureIndex;
                characterTexture = charactersAndMonsters[textureIndex];
                drawTexture.Draw(
               characterTexture,
               new Rectangle(player.X * textureWidth, player.Y * textureHeight, charactersWidth, charactersHeight),
               new Rectangle(0, 0, 32, 48),
               Color.White
            );
            }
            /*
            drawTexture.Draw(
                characterTexture,
                new Rectangle(firstCharacter.X * textureWidth, firstCharacter.Y * textureHeight, charactersWidth, charactersHeight),
                new Rectangle(0, 0, 32, 48),
                Color.White
                );
            drawTexture.Draw(
                     characterTexture,
                new Rectangle(secondCharacter.X * textureWidth, secondCharacter.Y * textureHeight, charactersWidth, charactersHeight),
                new Rectangle(0, 0, 32, 48),
                Color.White
                );
            drawTexture.Draw(
            characterTexture,
            new Rectangle(thirdCharacter.X * textureWidth, thirdCharacter.Y * textureHeight, charactersWidth, charactersHeight),
            new Rectangle(0, 0, 32, 48),
            Color.White
           );
             * */
            foreach (EnemyUnit enemy in allMonsters)
            {
                monstersTexture = charactersAndMonsters[enemy.TextureIndex];
                drawTexture.Draw(
                    monstersTexture,
                    new Rectangle(enemy.X * textureWidth, enemy.Y * textureHeight, charactersWidth, charactersHeight),
                    new Rectangle(0, 48, 32, 48),
                    Color.White
                    );
            }
        }

        /// <summary>
        /// Reading data from a setUpCharacters file and set monsters and characters.
        /// </summary>
        /// <param name="charactersFile"></param>
        public void setCharacters(string charactersFile)
        {
            StreamReader input = null;
            if (File.Exists(charactersFile))
            {
                try
                {
                    input = new StreamReader(charactersFile);
                    string line = "";
                    SetupPlayer(input.ReadLine(), firstCharacter);
                    SetupPlayer(input.ReadLine(), secondCharacter);
                    SetupPlayer(input.ReadLine(), thirdCharacter);

                    while ((line = input.ReadLine()) != null)
                    {
                        SetupMonsters(line);
                    }

                }
                catch
                {

                }
            }
        }


        /// <summary>
        /// Setting up monsters
        /// </summary>
        /// <param name="monsterInfo"></param>
        public void SetupMonsters(string monsterInfo)
        {

            int startingX = 0;
            int startingY = 0;
            int monsterTextureIndex = 0;

            try
            {
                string[] splitString = monsterInfo.Split(',');
                startingX = Convert.ToInt32(splitString[0]);
                startingY = Convert.ToInt32(splitString[1]);
                monsterTextureIndex = Convert.ToInt32(splitString[2]);
            }
            catch (FormatException e)
            {

            }

            EnemyUnit newEnemy = new EnemyUnit(startingX, startingY, monsterTextureIndex);
            allMonsters.Add(newEnemy);
        }

        /// <summary>
        /// Setting up Player
        /// </summary>
        /// <param name="playerInfo"></param>
        /// <param name="player"></param>
        public void SetupPlayer(string playerInfo, PlayerUnit player)
        {
            int startingX = 0;
            int startingY = 0;
            int playerTextureIndex = 0;

            try
            {
                string[] splitString = playerInfo.Split(',');
                startingX = Convert.ToInt32(splitString[0]);
                startingY = Convert.ToInt32(splitString[1]);
                playerTextureIndex = Convert.ToInt32(splitString[2]);
            }
            catch (FormatException e)
            {

            }

            player.X = startingX;
            player.Y = startingY;
            player.TextureIndex = playerTextureIndex;
            allPlayers.Add(player);
        }



    }//End of Level class
}
