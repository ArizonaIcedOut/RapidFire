// AUthor: Eric Pu
// File Name: ResultsScreen.cs
// Project Name: RapidFire
// Creation Date: June 13th, 2019
// Modified Date: June 13th, 2019
// Description: Class for the Transition screen

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
    class TransitionScreen
    {
        // Buttons
        public static Button StartBtn = new Button(Game1.startBtnImg, new Rectangle(450, 650, 300, 100));

        // Success and failure images
        public static Rectangle successRec = new Rectangle(450, 50, 300, 100);
        public static Rectangle failureRec = new Rectangle(450, 50, 300, 100);

        public static void Update()
        {
            // If the player presses the start button in practice mode, it returns to the menu screen. 
            if (StartBtn.CheckButton())
            {
                if (Globals.Practice)
                {
                    Globals.Fade.Start(Globals.MENU, 1);
                }
                else
                {
                    // If the player's health is now at 0, the game ends and they are sent to the results screen. Otherwise, the next game starts.
                    if (Globals.Health == 0)
                    {
                        Globals.Fade.Start(Globals.RESULTS, 1);
                    }
                    else MinigameSelection.NextGame();
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Background
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White * .7f);

            // Draws whether the player won or lost
            if (Globals.LastGameWon) spriteBatch.Draw(Game1.successImg, successRec, Color.White);
            else spriteBatch.Draw(Game1.failureImg, failureRec, Color.White);


            // If the mode is not practice, the player's stats (score and health) are drawn
            if (!Globals.Practice)
            {
                // Draws the start button
                spriteBatch.Draw(Game1.emptyBtnImg, StartBtn.Rec, Color.White);
                spriteBatch.DrawString(Game1.medFont, "NEXT", Globals.CentreText(StartBtn.Rec, "NEXT", Game1.medFont), Color.White);

                spriteBatch.DrawString(Game1.bigFont, "SCORE: " + Globals.Score, new Vector2(Globals.CentreText(Globals.GetRec(), "SCORE: " + Globals.Score, Game1.bigFont).X, 200), Color.Black);

                int heartsStartX = Globals.ScreenWidth / 2 - (Globals.Health * 70) / 2;
                for (int i = 0; i < Globals.Health; i++)
                {
                    spriteBatch.Draw(Game1.heartImg, new Rectangle(heartsStartX + (70 * i), 350, 50, 50), Color.White);
                }
            }
            else
            {
                // Draws the menu button
                spriteBatch.Draw(Game1.menuBtnImg, StartBtn.Rec, Color.White);
            }
        }
    }
}
