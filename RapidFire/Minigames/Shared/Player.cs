// AUthor: Eric Pu
// File Name: Player.cs
// Project Name: RapidFire
// Creation Date: May 22nd, 2019
// Modified Date: June 2nd, 2019
// Description: Class for managing the player controlled character in various minigames

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
using RapidFire;

namespace MONO_TEST
{
    public class Player
    { 
        // Dimensions and properties of the player
        public Vector2 Pos;

        public Vector2 Vel;

        public Vector2 Size;
      
        public Player(Vector2 pos, Vector2 size, Vector2 vel)
        {
            Pos = pos;
            Vel = vel;
            Size = size;
        }

        public void PlayerMovement()
        {
            // Lucky Day, Raindrops, and Juggler have simple horizontal movement
            // Terminal Velocity has horizontal movement restricted by walls instead of the edge of the screen
            // Floor is Lava allows player to jump as well
            switch (Globals.Minigame)
            {
                case Globals.LUCKY_DAY:
                case Globals.RAINDROPS:
                case Globals.JUGGLER:
                    {
                        // If the player presses left or A, they move left. If they press right or D, they move right.
                        if ((Globals.KbCurrent.IsKeyDown(Keys.Left) || Globals.KbCurrent.IsKeyDown(Keys.A)) && Pos.X - Vel.X > 0)
                        {
                            Pos.X -= Vel.X;
                        }
                        else if ((Globals.KbCurrent.IsKeyDown(Keys.Right) || Globals.KbCurrent.IsKeyDown(Keys.D)) && Pos.X + Vel.X + Size.X < Globals.ScreenWidth)
                        {
                            Pos.X += Vel.X;
                        }
                        break;
                    }
                case Globals.TERMINAL_VELOCITY:
                    {
                        if ((Globals.KbCurrent.IsKeyDown(Keys.Left) || Globals.KbCurrent.IsKeyDown(Keys.A)) && Pos.X - Vel.X > TerminalVelocity.LeftWallX)
                        {
                            Pos.X -= Vel.X;
                        }
                        else if ((Globals.KbCurrent.IsKeyDown(Keys.Right) || Globals.KbCurrent.IsKeyDown(Keys.D)) && Pos.X + Vel.X + Size.X < TerminalVelocity.RightWallX)
                        {
                            Pos.X += Vel.X;
                        }
                        break;
                    }
                case Globals.FLOOR_IS_LAVA:
                    {
                        if (Globals.KbCurrent.IsKeyDown(Keys.Left) || Globals.KbCurrent.IsKeyDown(Keys.A))
                        {
                            Pos.X -= Vel.X;

                            if (Pos.X < 0)
                            {
                                Pos.X = Globals.ScreenWidth - Size.X;
                            }
                            else if (Pos.X + Size.X > Globals.ScreenWidth)
                            {
                                Pos.X = 0;
                            }
                        }
                        else if (Globals.KbCurrent.IsKeyDown(Keys.Right) || Globals.KbCurrent.IsKeyDown(Keys.D))
                        {
                            Pos.X += Vel.X;

                            if (Pos.X < 0)
                            {
                                Pos.X = Globals.ScreenWidth - Size.X;
                            }
                            else if (Pos.X + Size.X > Globals.ScreenWidth)
                            {
                                Pos.X = 0;
                            }
                        }
                        else if (Vel.Y == 0 && (Globals.CheckKey(Keys.Space) || Globals.CheckKey(Keys.Up)))
                        {
                            Game1.jumpSnd.CreateInstance().Play();
                             Vel.Y = -8;
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: Returns a rectangle with the dimensions of the player
        /// Description: Produces a rectangle with the properties of the player
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRec()
        {
            return new Rectangle((int)Pos.X, (int)Pos.Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// Pre: rec as a rectangle being checked
        /// Post: Returns a bool indicating if the player is colliding with the rectangle or not
        /// Description: Checks if the player is currently colliding with a rectangle.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool CheckCollision(Rectangle rec)
        {
            if (rec.X + rec.Width >= Pos.X && rec.X <= Pos.X + Size.X && rec.Y + rec.Height >= Pos.Y && rec.Y <= Pos.Y + Size.Y) return true;
            else return false;
        }

        /// <summary>
        /// Pre: rec as the platform being checked
        /// Post: Returns a bool indicating if the player is colliding with the platform
        /// Description: Checks if the player is colliding with a given platform (used in Floor is Lava)
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool CheckPlatformCollision(Rectangle rec)
        {
            // If the player's X position is between the platform, their Y velocity is less than 0 (indicating that they are falling), and they are not currently within the platform but will be right after
            if (rec.X + rec.Width >= Pos.X && rec.X <= Pos.X + Size.X &&
                Vel.Y > 0 && Pos.Y + Size.Y + Vel.Y >= rec.Y && Pos.Y + Size.Y < rec.Y)
            {
                // Moves the player's Y position to above the platform, so collision will not happen again, and returns true
                Pos.Y = rec.Y - Size.Y;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Pre: rec as the rectangle being checked
        /// Post Returns a bool indicating if the player is on the platform or not
        /// Description: Checks if the player is currently on a given platform
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        public bool CheckOnPlatform(Rectangle rec)
        {
            if (rec.X + rec.Width >= Pos.X && rec.X <= Pos.X + Size.X && Pos.Y + Size.Y == rec.Y) return true;
            else return false;
        }
    }
}
