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
        private int defenseMod;
        /// --  End of Instance Variables   --


        
        /// --  Properties  --
        public Boolean Impassable
        {
            set { impassable = value; }
            get { return impassable; }
        }
        public int DefenseMod
        {
            set { defenseMod = value; }
            get { return defenseMod; }
        }
        /// --  End of Properties   --



        /// --  Constructors    --

        public Terrain()
            : base()
        { }

        public Terrain(int xValue, int yValue)
            : base(xValue, yValue)
        { }

        public Terrain(int xValue, int yValue, bool impass, int dMod)
            : base(xValue, yValue)
        {
            Impassable = impass;
            DefenseMod = dMod;
        }
        /// -- End of Constructors  --

    }//End of Terrain class
}
