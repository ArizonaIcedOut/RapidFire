// AUthor: Eric Pu
// File Name: ColorCircle.cs
// Project Name: RapidFire
// Creation Date: May 26th, 2019
// Modified Date: May 29th, 2019
// Description: Class for the color circles inside the Color Guesser minigame

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;

namespace MONO_TEST
{ 
    public class ColorCircle
    {
        // Each color circle has a rectangle (for its dimensions), a color, and a boolean indicating whether it is the correct color or not.
        public Rectangle Rec;

        public Color Color;

        public bool Correct;

        public ColorCircle(Rectangle rec, Color color, bool correct)
        {
            Rec = rec;
            Color = color;
            Correct = correct;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Checks if the color circle is being clicked on, same function as for the Button class
        /// </summary>
        /// <returns></returns>
        public bool CheckButton()
        {
            if (Globals.CheckLeftClick() && Globals.CheckMouseCollision(Rec)) return true;
            else return false;
        }
    }
}
