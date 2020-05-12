// AUthor: Eric Pu
// File Name: Backwards.cs
// Project Name: RapidFire
// Creation Date: June 6th, 2019
// Modified Date: June 6th, 2019
// Description: Class for the sadistic minigame Backwards

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
using RapidFire;

namespace MONO_TEST
{
    public class Backwards
    {
        // Properties of the word
        public string Word;

        public string ReversedWord;

        public string Blanks;

        public Backwards()
        {
            // Generates a random word and a reversed version
            Word = Globals.GetWord();
            ReversedWord = Globals.ReverseString(Word);

            // An empty string is created to hold the blanks
            Blanks = "";

            // Difficulty affects duration of the timer
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        Globals.GameDuration = new Cooldown(900);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        Globals.GameDuration = new Cooldown(600);
                        break;
                    }
                case Globals.HARD:
                    {
                        Globals.GameDuration = new Cooldown(300);
                        break;
                    }
            }
        }

        public void Update()
        {
            // If the game timer surpasses the timer, the game ends. Otherwise, the timer is updated.
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.LostGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            // If the player inputs the correct key, the game progresses. Otherwise, they lose.
            if (Globals.CheckKey(Globals.GetFirstLetter(ReversedWord)))
            {
                Game1.hitSnd.CreateInstance().Play();

                if (ReversedWord.Length == 1)
                {
                    MinigameSelection.WonGame();
                }
                else
                {
                    ReversedWord = ReversedWord.Substring(1);
                    Blanks += "_";
                }
            }
            else if (Globals.IsWrongKey(Globals.GetFirstLetter(ReversedWord)))
            {
                MinigameSelection.LostGame();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the word the player is typing and the string of blanks, representing their progress on the word
            spriteBatch.DrawString(Game1.bigFont, Word, new Vector2(Globals.CentreText(Globals.GetRec(), Word, Game1.bigFont).X, 100), Color.Black);
            spriteBatch.DrawString(Game1.bigFont, Blanks, new Vector2(Globals.CentreText(Globals.GetRec(), Blanks, Game1.bigFont).X, 250), Color.Black);
        }
    }
}
