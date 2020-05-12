// AUthor: Eric Pu
// File Name: ResultsScreen.cs
// Project Name: RapidFire
// Creation Date: June 11th, 2019
// Modified Date: June 11th, 2019
// Description: Class for the results screen

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
    public class Results
    {
        public static void Update()
        {
            // Updates the name of the player
            Leaderboard.Name.Update();

            // Checks if player has tried to leave screen
            if ((Globals.CheckKey(Keys.Space) || Globals.CheckKey(Keys.Enter)))
            {
                // Updates leaderboard and changes screen if name is entered. Otherwise, plays error sound.
                if (Leaderboard.Name.GetLength() <= Leaderboard.Name.MaxLength)
                {
                    // Iterates through each leaderboard ranking. If player is within top 10, they are placed on it
                    for (int i = 0; i < Leaderboard.Size; i++)
                    {
                        if (Leaderboard.Rankings[i].Score < Globals.Score)
                        {
                            Leaderboard.Rankings.Insert(i, new PlayerRanking(Leaderboard.Name.Name, Globals.Score));
                            Leaderboard.Rankings.RemoveAt(Leaderboard.Size - 1);
                            break;
                        }
                    }

                    // Empties leaderboard document
                    File.WriteAllText("leaderboard.txt", string.Empty);

                    // Rewrites leaderboard document with new leaderboard
                    using (StreamWriter sw = new StreamWriter("leaderboard.txt"))
                    {
                        foreach (PlayerRanking user in Leaderboard.Rankings)
                        {
                            sw.WriteLine(user.Name);
                            sw.WriteLine(user.Score);
                        }
                    }

                    // Fades to leaderboard screen
                    Globals.Fade.Start(Globals.LEADERBOARD, 1);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Background
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White * .7f);

            // Draws each of the statistics of the results screen
            spriteBatch.DrawString(Game1.medFont, "GAME OVER", new Vector2(Globals.CentreText(Globals.GetRec(), "GAME OVER", Game1.medFont).X, 50), Color.Black);
            spriteBatch.DrawString(Game1.medFont, "YOUR SCORE WAS " + Globals.Score, new Vector2(Globals.CentreText(Globals.GetRec(), "YOUR SCORE WAS " + Globals.Score, Game1.medFont).X, 200), Color.Black);
            spriteBatch.DrawString(Game1.medFont, "ENTER YOUR NAME", new Vector2(Globals.CentreText(Globals.GetRec(), "ENTER YOUR NAME", Game1.medFont).X, 300), Color.Black);
            spriteBatch.DrawString(Game1.bigFont, Leaderboard.Name.Name, new Vector2(Globals.CentreText(Globals.GetRec(), Leaderboard.Name.Name, Game1.bigFont).X, 400), Color.Black);
        }
    }
}
