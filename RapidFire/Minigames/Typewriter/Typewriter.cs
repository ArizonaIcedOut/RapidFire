// AUthor: Eric Pu
// File Name: Typewriter.cs
// Project Name: RapidFire
// Creation Date: June 6th, 2019
// Modified Date: June 6th, 2019
// Description: Class for the Typewriter minigame

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
    public class Typewriter
    {
        // Rows of the keyboard layout
        public string FirstRow;

        public string SecondRow;

        public string ThirdRow;

        public List<TypewriterKey> FirstRowKeys;

        public List<TypewriterKey> SecondRowKeys;

        public List<TypewriterKey> ThirdRowKeys;

        // Properties of the word
        public string Word;

        public string WordProgress;

        public char FirstLetter;

        public Typewriter()
        {
            // Difficulty affects duration of the game
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        Globals.GameDuration = new Cooldown(800);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        Globals.GameDuration = new Cooldown(600);
                        break;
                    }
                case Globals.HARD:
                    {
                        Globals.GameDuration = new Cooldown(400);
                        break;
                    }
            }

            WordProgress = "";

            // The keys in each of the rows of the DVORAK layout
            FirstRow = "PYFGCRL";
            SecondRow = "AOEUIDHTNS";
            ThirdRow = "QJKXBMWVZ";

            // Sets the word to uppercase
            Word = Globals.GetWord().ToUpper();

            // Creates each of the rows of keys
            FirstRowKeys = new List<TypewriterKey>();
            for (int i = 0; i < FirstRow.Length; i++)
            {
                char letter = FirstRow[i];
                FirstRowKeys.Add(new TypewriterKey(letter, 1, i));
            }

            SecondRowKeys = new List<TypewriterKey>();
            for (int i = 0; i < SecondRow.Length; i++)
            {
                char letter = SecondRow[i];
                SecondRowKeys.Add(new TypewriterKey(letter, 2, i));
            }

            ThirdRowKeys = new List<TypewriterKey>();
            for (int i = 0; i < ThirdRow.Length; i++)
            {
                char letter = ThirdRow[i];
                ThirdRowKeys.Add(new TypewriterKey(letter, 3, i));
            }
        }

        public void Update()
        {
            // If the player runs out of time, they lose
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.LostGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            // Gets the first letter of the word
            FirstLetter = Word[0];

            // Checks each of the rows of keys
            CheckKeyRow(FirstRowKeys);
            CheckKeyRow(SecondRowKeys);
            CheckKeyRow(ThirdRowKeys);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Shows the progress of the word
            spriteBatch.DrawString(Game1.bigFont, WordProgress + Word, new Vector2(Globals.CentreText(Globals.GetRec(), WordProgress + Word, Game1.bigFont).X, 100), Color.Black);

            // Keyboard is not drawn if game is paused, so players can't cheat
            if (!Game1.paused)
            {
                // Draws each of the rows of keys
                foreach (TypewriterKey key in FirstRowKeys)
                {
                    spriteBatch.Draw(Game1.keyImg, key.Rec, Color.White);
                    spriteBatch.DrawString(Game1.medFont, Convert.ToString(key.Letter), Globals.CentreText(key.Rec, Convert.ToString(key.Letter), Game1.medFont), Color.Black);
                }
                foreach (TypewriterKey key in SecondRowKeys)
                {
                    spriteBatch.Draw(Game1.keyImg, key.Rec, Color.White);
                    spriteBatch.DrawString(Game1.medFont, Convert.ToString(key.Letter), Globals.CentreText(key.Rec, Convert.ToString(key.Letter), Game1.medFont), Color.Black);
                }
                foreach (TypewriterKey key in ThirdRowKeys)
                {
                    spriteBatch.Draw(Game1.keyImg, key.Rec, Color.White);
                    spriteBatch.DrawString(Game1.medFont, Convert.ToString(key.Letter), Globals.CentreText(key.Rec, Convert.ToString(key.Letter), Game1.medFont), Color.Black);
                }
            }

        }

        /// <summary>
        /// Pre: typewriterKeys as the row of keys
        /// Post: n/a
        /// Description: Checks each of the keys in a row to see if it has been pressed.
        /// </summary>
        /// <param name="typewriterKeys"></param>
        public void CheckKeyRow(List<TypewriterKey> typewriterKeys)
        {
            foreach (TypewriterKey key in typewriterKeys)
            {
                if (key.KeyButton.CheckButton())
                {
                    // If the key is the first letter of the word, the word is shortened. Otherwise, the player loses.
                    if (key.Letter == FirstLetter)
                    {
                        ShortenWord();
                        Game1.hitSnd.CreateInstance().Play();
                    }
                    else
                    {
                        MinigameSelection.LostGame();
                    }
                }
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Shortens the word.
        /// </summary>
        public void ShortenWord()
        {
            // If the word has only one letter left, the game ends. Otherwise, it is shortened and a blank is added to the progress.
            if (Word.Length == 1)
            {
                MinigameSelection.WonGame();
            }
            else
            {
                Word = Word.Substring(1);
                WordProgress += "_";
            }
        }
    }
}
