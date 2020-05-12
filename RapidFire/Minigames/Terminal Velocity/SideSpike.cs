// AUthor: Eric Pu
// File Name: SideSpike.cs
// Project Name: RapidFire
// Creation Date: June 5th, 2019
// Modified Date: June 5th, 2019
// Description: Class for the side spike obstacle in the Terminal Velocity minigame

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
using MONO_TEST;

namespace MONO_TEST
{
    public class SideSpike
    {
        // Dimensions and velocity of the spike
        public int Y;

        public int VelocityY;

        public Vector2 Size;

        // Range for the randomized width
        public int MinWidthRange;

        public int MaxWidthRange;

        // Constants for the side the spike has spawned at
        public const int LEFT = 0;

        public const int RIGHT = 1;   

        public int Side;

        public SideSpike()
        {
            // There is a 50% chance of the spike to spawn on the left, and 50% for the right
            if (Globals.Rng.Next(2) == 1)
            {
                Side = LEFT;
            }
            else
            {
                Side = RIGHT;
            }

            // Y is set to the bottom of the screen
            Y = Globals.ScreenHeight;
            
            VelocityY = 14;

            // Sets the dimensions of the spike
            MinWidthRange = 150;
            MaxWidthRange = 200;

            Size.X = Globals.Rng.Next(MinWidthRange, MaxWidthRange);
            Size.Y = 30;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Moves the spike
        /// </summary>
        public void Move()
        {
            Y -= VelocityY;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: Returns a rectangle with the dimensions of the spike
        /// Description: Generates a rectangle with the dimensions of the spike
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRec()
        {
            if (Side == LEFT) return new Rectangle(TerminalVelocity.LeftWallX, Y, (int)Size.X, (int)Size.Y);
            else return new Rectangle(TerminalVelocity.RightWallX - (int)Size.X, Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// Pre: n/a
        /// Post: Returns a bool indicating if the player is colliding with the spike
        /// Description: Checks if the player is touching the spike
        /// </summary>
        /// <returns></returns>
        public bool CheckPlayer()
        {
            if (Globals.Player.CheckCollision(GetRec())) return true;
            else return false;
        }
    }
}
