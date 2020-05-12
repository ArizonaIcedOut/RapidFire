// AUthor: Eric Pu
// File Name: ColorGUesser.cs
// Project Name: RapidFire
// Creation Date: May 26th, 2019
// Modified Date: May 29th, 2019 
// Description: Class for the Color Guesser minigame

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
    public class ColorGuesser
    {
        // Properties for each of the three colors in the game
        public Color CorrectColor;

        public Color InvertedColor;

        public Color IncorrectColor;

        // List for the order the circles appear in, which is randomized
        public List<int> CircleOrder;

        // Dictionary holding the place of the circle and the circle itself
        public Dictionary<int, ColorCircle> ColorCircles;

        public ColorGuesser()
        {
            // Generates a random color for the correct color
            CorrectColor = new Color(Globals.Rng.Next(0, 255), Globals.Rng.Next(0, 255), Globals.Rng.Next(0, 255));

            // Inverts that color to produce the second color
            InvertedColor = Globals.InvertColor(CorrectColor);

            // Alters the correct color to produce the last one
            IncorrectColor = AlterRgbValue(CorrectColor);

            // Produces a list for the order of the circles, which is then randomized
            CircleOrder = new List<int> { 0, 1, 2 };
            Globals.ShuffleIntList(CircleOrder);

            // Produces a dictionary with a random place for the circle as the key, and produces a new circle for the value
            ColorCircles = new Dictionary<int, ColorCircle>
            {
                { CircleOrder[0], new ColorCircle(new Rectangle(350, 400, 100, 100), CorrectColor, true)},
                { CircleOrder[1], new ColorCircle(new Rectangle(550, 400, 100, 100), InvertedColor, false)},
                { CircleOrder[2], new ColorCircle(new Rectangle(750, 400, 100, 100), IncorrectColor, false)}
            };

            // Difficulty affects game duration
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
                        Globals.GameDuration = new Cooldown(400);
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

            // Checks if each of the circles have been clicked
            for (int i = 0; i < ColorCircles.Count(); i++)
            {
                if (ColorCircles[i].CheckButton())
                {
                    // If it is incorrect, the player loses. Otherwise, the player wins.
                    if (!ColorCircles[i].Correct)
                    {
                        MinigameSelection.LostGame();
                    }
                    else
                    {
                        MinigameSelection.WonGame();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Game is only drawn when the help screen is not open, so players cannot see the colors before the game starts
            if (!Globals.HelpScreenOpen)
            {
                // Draws each of the circles
                foreach (ColorCircle circle in ColorCircles.Values)
                {
                    spriteBatch.Draw(Game1.blankCircleImg, circle.Rec, circle.Color);
                }

                // Shows the player the RGB values of the correct color
                spriteBatch.DrawString(Game1.bigFont, GetRgbString(CorrectColor),
                    new Vector2(Globals.CentreText(Globals.GetRec(), GetRgbString(CorrectColor), Game1.bigFont).X, 200), Color.Black);
            }
        }

        /// <summary>
        /// Pre: color as the color being altered
        /// Post: color as the newly altered color
        /// Description: Function for altering the RGB values of a color, which is slightly different
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color AlterRgbValue(Color color)
        {
            // Chooses a difference between 75 and 125 (arbitrary values)
            byte difference = (byte)Globals.Rng.Next(75, 125);

            // Chooses a number between 1 and 3 to determine which RGB value is being changed (0 = Red, 1 = Blue, 2 = Green)
            switch (Globals.Rng.Next(0, 3))
            { 
                case 0:
                    {
                        // If the color value is greater than the difference, then it is subtracted. Otherwise, it is added. This prevents any possible RGB values below 0 or above 255.
                        if (color.R >= difference)
                        {
                            color.R -= difference;
                        }
                        else if (color.R <= difference)
                        {
                            color.R += difference;
                        }
                        break;
                    }
                case 1:
                    {
                        if (color.B >= difference)
                        {
                            color.B -= difference;
                        }
                        else if (color.B <= difference)
                        {
                            color.B += difference;
                        }
                        break;
                    }
                case 2:
                    {
                        if (color.G >= difference)
                        {
                            color.G -= difference;
                        }
                        else if (color.G <= difference)
                        {
                            color.G += difference;
                        }
                        break;
                    }
            }
            return color;
        }

        /// <summary>
        /// Pre: color as the color being turned into a string
        /// Post: a string representing the RGB values of the string
        /// Description: Produces a string representing the RGB values of a given color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public string GetRgbString(Color color)
        {
            return "rgb(" + color.R + "," + color.G + "," + color.B + ")";
        }
    }
}