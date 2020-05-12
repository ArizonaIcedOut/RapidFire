// AUthor: Eric Pu
// File Name: Hud.cs
// Project Name: RapidFire
// Creation Date: June 1st, 2019
// Modified Date: June 1st, 2019
// Description: Class for drawing HUD elements in all minigames

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
    public class Hud
    {
        public static void Draw(SpriteBatch spriteBatch)
        {
            // Bar indicating the amount of time left
            spriteBatch.Draw(Game1.blankRecImg,
                CreateBar(Globals.GameDuration.Timer, Globals.GameDuration.Duration, 10, 10, Globals.ScreenWidth - 20, 20), Color.Orange);

            switch (Globals.Minigame)
            {
                case Globals.LUCKY_DAY:
                    {
                        // Draws the bar showing the player's coins collected
                        spriteBatch.Draw(Game1.blankRecImg,
                            Hud.CreateBar(Game1.luckyDay.Collected, Game1.luckyDay.Goal, 10, 40, Globals.ScreenWidth - 20, 20), Color.Blue);

                        break;
                    }
                case Globals.MILKSHAKER:
                    { 
                        // Draws the progress bar
                        spriteBatch.Draw(Game1.blankRecImg,
                            Hud.CreateBar(Game1.milkshaker.DistanceTravelled, Game1.milkshaker.DistanceNeeded, 10, 40, Globals.ScreenWidth - 20, 20), Color.Blue);
                        break;
                    }
            }

            // If the player is not in practice mode, the other UI elements are drawn (score and health)
            if (!Globals.Practice)
            {
                spriteBatch.DrawString(Game1.bigFont, Convert.ToString(Globals.Score), new Vector2(10, Globals.ScreenHeight - 100), Color.Black);

                for (int i = 0; i < Globals.Health; i++)
                {
                    spriteBatch.Draw(Game1.heartImg, new Rectangle(Globals.ScreenWidth - 60 - (i * 70), Globals.ScreenHeight - 60, 50, 50), Color.White);
                }
            }
        }

        /// <summary>
        /// Pre: n as the amount being tracked, max as the maximum amount, x as the X position, y as the Y position, maxWidth as the width of the bar, height as the height of the bar
        /// Post: Returns a Rectangle with the dimensions of the bar
        /// Description: Creates a bar showing progress of a statistic being tracked
        /// </summary>
        /// <param name="n"></param>
        /// <param name="max"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="maxWidth"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Rectangle CreateBar(double n, double max, int x, int y, int maxWidth, int height)
        {
            return new Rectangle(x, y, (int)(n / max * maxWidth), height);
        }
    }
}
