using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
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
        private Image charImage;
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

        public Image CharImg
        {
            set { charImage = value; }
            get { return charImage; }
        }
        /// --  End of Properties   --



        /// --  Constructors --
        public GridOccupant()
        {
            x = 0;
            y = 0;
            //img = DefaultTexture (probably an image of a field?)
        }

        public GridOccupant(int xCoord, int yCoord)
        {
            this.X = xCoord;
            this.Y = yCoord;
            // Should also assign default texture
        }

        public GridOccupant(int xCoord, int yCoord, Texture2D unitImage)
        {
            this.X = xCoord;
            this.Y = yCoord;
            this.Img = unitImage;
        }

        /// --  End of Constructors --



        /// --  No Methods --


    }//End of GridOccupant class
}
