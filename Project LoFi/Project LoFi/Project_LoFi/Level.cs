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
      //  List<int[,]> listOfMaps = new List<int[,]>(); //  Store maps
        List<Terrain[,]> listOfTerrain = new List<Terrain[,]>();
        List<Texture2D> mapTextures = new List<Texture2D>(); // Store textures for maps

      //  int[,] twoDArray;

        int textureHeight = 48; // used to keep the size of the texture
        int textureWidth = 48; // // used to keep the size of the texture




        /// <summary>
        ///  get array height
        /// </summary>
        public int mapHeight
        {
            get
            {
                //return listOfMaps[0].GetLength(1);    
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
                // return listOfMaps[0].GetLength(0);   
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
                    string[] readAllLines = File.ReadAllLines(mapName);
                    string[][] jaggedArray = readAllLines.Select(line => line.Split(',').ToArray()).ToArray();

               //     twoDArray = new int[jaggedArray.GetLength(0), jaggedArray[0].Length];
                    terrain = new Terrain[jaggedArray.GetLength(0), jaggedArray[0].Length];
                    for (int i = 0; i < jaggedArray.Length; i++)
                    {

                        for (int y = 0; y < jaggedArray[i].Length; y++)
                        {
                           // twoDArray[i, y] = int.Parse(jaggedArray[i][y]);
                            addingThisTerrain.X = i;
                            addingThisTerrain.Y = y;
                            addingThisTerrain.Index = int.Parse(jaggedArray[i][y]);
                            terrain[i, y] = addingThisTerrain;
                            

                        }

                    }
               //     listOfMaps.Add(twoDArray);
                    listOfTerrain.Add(terrain);
                }
                catch (Exception ex)
                { 
                    
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
                    //texturePosition = listOfMaps[0][x,y];
                    //texturePosition = terrain[x,y].Index; // retrieve position for the texture
                    texturePosition = listOfTerrain[0][x, y].Index;
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
