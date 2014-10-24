using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

///Orig. Author: Jesse C.
///Class description: Terrain object. Holds impassable, etc.

namespace Project_LoFi
{
    public class Terrain : GridOccupant
    {
        /// --  Instance Variables  --
        private Boolean impassable;                 // Tells you if the terrain is a wall or not
        private int textureIndex;                   // Not sure what this is used for?
        /// --  End of Instance Variables   --


        
        /// --  Properties  --
        public Boolean Impassable
        {
            set { impassable = value; }
            get { return impassable; }
        }

        public int TextureIndex
        {
            set { textureIndex = value; }
            get { return textureIndex; }
        }
        /// --  End of Properties   --




    }//End of Terrain class
}
