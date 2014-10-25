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
        
        Terrain[,] terrain;
        Terrain addingThisTerrain = new Terrain();
        List<Terrain[,]> listOfTerrain = new List<Terrain[,]>();
        List<Texture2D> mapTextures = new List<Texture2D>(); // Store textures for maps



        int textureHeight = 48; // used to keep the size of the texture
        int textureWidth = 48; // // used to keep the size of the texture




        /// <summary>
        ///  get array height
        /// </summary>
        public int mapHeight
        {
            get
            {
           
                return listOfTerrain[0].GetLength(1);    
            }
        }

        /// <summary>
        /// get array width
        /// </summary>
        public int mapWidth
        {
            get 
            {
            
                return listOfTerrain[0].GetLength(0);
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
  

                    terrain = new Terrain[jaggedArray.GetLength(0), jaggedArray[0].Length];
                    for (int i = 0; i < jaggedArray.Length; i++)
                    {

                        for (int y = 0; y < jaggedArray[i].Length; y++)
                        {
                   
                            addingThisTerrain.X = i;
                            addingThisTerrain.Y = y;
                            addingThisTerrain.TextureIndex = int.Parse(jaggedArray[i][y]);
                            terrain[i, y] = addingThisTerrain;

                            addingThisTerrain = new Terrain();
                        }

                    }
          
                    listOfTerrain.Add(terrain);
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
        /// Draw the map
        /// </summary>
        /// <param name="drawTexture"></param>
        public void Draw(SpriteBatch drawTexture)
        {
            int texturePosition;
            Texture2D texture;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {

                    texturePosition = listOfTerrain[0][x, y].TextureIndex;
                    texture = mapTextures[texturePosition]; // get the texture number and assign it to the temporary texture variable

                    // draw that texture
                    drawTexture.Draw(
                        texture,
                        new Rectangle(x * textureWidth, y * textureHeight, textureWidth, textureHeight),
                        Color.White);
                        
                }
            }
        }

    }//End of Level class
}
