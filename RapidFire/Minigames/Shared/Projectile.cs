// AUthor: Eric Pu
// File Name: Player.cs
// Project Name: RapidFire
// Creation Date: May 22nd, 2019
// Modified Date: June 5th, 2019
// Description: Class for creating and managing generic projectiles used in various minigames

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
    /// <summary>
    /// Class for all three types of targets.
    /// </summary>
    public class Projectile
    {
        // Properties of the projectile
        public Vector2 Pos;

        public Vector2 Vel;

        public Vector2 Size;

        public Projectile(Vector2 pos, Vector2 vel, Vector2 size)
        {
            Pos = pos;
            Vel = vel;
            Size = size;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Moves the projectile.
        /// </summary>
        public void Move()
        {
            // Juggler uses a weaker gravity. The other minigames use the normal gravity.
            if (Globals.Gamestate == Globals.JUGGLER)
            {
                Vel.Y += Globals.WeakGravity;
            }
            else Vel.Y += Globals.Gravity;

            // Adds the velocities to the coordinates of the player
            Pos.Y = (int)(Pos.Y + Vel.Y);
            Pos.X = (int)(Pos.X + Vel.X);
        }

        /// <summary>
        /// Pre: rec as rectangle being checked
        /// Post: Whether projectile collided with rec or not
        /// Description: Checks if projectile is colliding with given rectangle.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rec)
        {
            if (rec.X + rec.Width >= Pos.X && rec.X <= Pos.X + Size.X && rec.Y + rec.Height >= Pos.Y && rec.Y <= Pos.Y + Size.Y) return true;
            else return false;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: Returns a rectangle with entity's X, Y, width, and height values
        /// Description: Returns a rectangle with the entity's dimensions.
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRec()
        {
            return new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Checks if target is hitting edges of the screen.
        /// </summary>
        public bool CheckBoundaries()
        {
            if (Pos.Y + Size.Y >= Globals.ScreenHeight || Pos.Y <= 0) return true;
            else if (Pos.X + Size.X >= Globals.ScreenWidth || Pos.X <= 0) return true;
            else return false;
        }
    }
}
