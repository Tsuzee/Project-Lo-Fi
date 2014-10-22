// Aliaksandr Shumski
// Level class creates level for the game. 
// Has AddTextureToTheList method and Draw method

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Project_LoFi
{
    public class Level
    {

        
        
        
        List<int[,]> listOfMaps = new List<int[,]>(); //  Store maps
        List<Texture2D> mapTextures = new List<Texture2D>(); // Store textures for maps


        int textureHeight = 64; // used to keep the size of the texture
        int textureWidth = 64; // // used to keep the size of the texture


        /// <summary>
        /// Constructor, accepts map
        /// </summary>
        /// <param name="map"></param>
        public Level(int[,] map)
        {
            listOfMaps.Add(map);
        }

        /// <summary>
        ///  get array height
        /// </summary>
        public int mapHeight
        {
            get
            {
                return listOfMaps[0].GetLength(1);    
            }
        }

        /// <summary>
        /// get array width
        /// </summary>
        public int mapWidth
        {
            get 
            {
                return listOfMaps[0].GetLength(0);
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
            int texturePosition = 0;
            Texture2D texture;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    texturePosition = listOfMaps[0][x, y]; // retrieve position for the texture
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
