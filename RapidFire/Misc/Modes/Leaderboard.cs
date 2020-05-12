// AUthor: Eric Pu
// File Name: Leaderboard.cs
// Project Name: RapidFire
// Creation Date: June 11th, 2019
// Modified Date: June 11th, 2019
// Description: Class for the leaderboard screen

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
    public class Leaderboard
    {
        // Properties of the leaderboard list
        public static List<PlayerRanking> Rankings = new List<PlayerRanking>();

        public static int Size = 8;

        // Object for player's name
        public static NameInput Name = new NameInput();

        public static void Update()
        {
            // Switches screen to menu if player leaves
            if (Globals.CheckKey(Keys.Space) || Globals.CheckKey(Keys.Enter) || Menu.BackBtn.CheckButton())
            {
                Globals.Gamestate = Globals.MENU;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Background
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White * .7f);

            // Shows which column is names and which is scores
            spriteBatch.DrawString(Game1.medFont, "NAME: ", new Vector2(400, 50), Color.Red);
            spriteBatch.DrawString(Game1.medFont, "SCORE: ", new Vector2(750, 50), Color.Red);

            // Draws each user on the leaderboard
            for (int i = 0; i < Size; i++)
            {
                spriteBatch.DrawString(Game1.medFont, Rankings[i].Name, new Vector2(400, 150 + (i * 50)), Color.Black);
                spriteBatch.DrawString(Game1.medFont, Convert.ToString(Rankings[i].Score), new Vector2(800, 150 + (i * 50)), Color.Black);
            }

            // Back button
            spriteBatch.Draw(Menu.BackBtn.Img, Menu.BackBtn.Rec, Color.White);
        }

        /// <summary>
        /// Pre: rankingsList is a list of PlayerRankings
        /// Post: Returns the loaded leaderboard
        /// Description: Reads through leaderboard document and returns the leaderboard.
        /// </summary>
        /// <param name="rankingsList"></param>
        /// <returns></returns>
        public static void Load()
        {
            using (StreamReader sr = new StreamReader("leaderboard.txt"))
            {
                for (int i = 0; i < Size; i++)
                {
                    Rankings.Add(new PlayerRanking(sr.ReadLine(), Convert.ToInt32(sr.ReadLine())));
                }
            }
        }
    }
}
