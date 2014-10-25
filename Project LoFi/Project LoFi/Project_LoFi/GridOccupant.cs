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
///Class description: base class for the main inheritance tree - holds x & y attributes, and a sprite.

namespace Project_LoFi
{
    public abstract class GridOccupant
    {
        /// --  Instance Variables  --
        private int x;
        private int y;
        private Texture2D img;
        private int textureIndex;
        ///  --  End of Instance Variables   --



        /// --  Properties  --
        public int X
        {
            set { x = value; }
            get { return x; }
        }

        public int Y
        {
            set { y = value; }
            get { return y; }
        }

        public Texture2D Img
        {
            set { img = value; }
            get { return img; }
        }


        public int TextureIndex
        {
            set { textureIndex = value; }
            get { return textureIndex; }
        }
        /// --  End of Properties   --



        /// --  Constructors --
        public GridOccupant()
        {
            x = 0;
            y = 0;
            //img = DefaultTexture (probably an image of a field?)
        }
        /// --  End of Constructors --



        /// --  No Methods --


    }//End of GridOccupant class
}
