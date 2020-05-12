// AUthor: Eric Pu
// File Name: CircleClicker.cs
// Project Name: RapidFire
// Creation Date: June 10th, 2019
// Modified Date: June 10th, 2019 
// Description: Class for the Circle Clicker minigame

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
    public class CircleClicker
    {
        // List of the circles
        public List<Circle> Circles;

        // Circle spawn cooldown
        public Cooldown CircleSpawnCd;

        public CircleClicker()
        {
            // Game lasts 10 seconds long
            Globals.GameDuration = new Cooldown(600);

            // Difficulty affects spawn rate of the circles
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        CircleSpawnCd = new Cooldown(60);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        CircleSpawnCd = new Cooldown(40);
                        break;
                    }
                case Globals.HARD:
                    {
                        CircleSpawnCd = new Cooldown(20);
                        break;
                    }
            }

            // Creates a new empty list of circles
            Circles = new List<Circle>();
        }

        public void Update()
        {
            // If the game timer surpasses the timer, the game ends. Otherwise, the timer is updated.
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.WonGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            // If the circle spawn cooldown is met, a new circle is spawned. Otherwise, the timer is updated.
            if (CircleSpawnCd.CheckCooldown())
            {
                Circles.Add(new Circle());
            }
            else
            {
                CircleSpawnCd.UpdateCooldown();
            }

            // Checks each of the circles for if they are being clicked
            for (int i = 0; i < Circles.Count; i++)
            {
                // If the player has clicked on the circle, it is removed. Otherwise, the circle is updated.
                if (Circles[i].Btn.CheckButton())
                {
                    Circles.RemoveAt(i);
                    i--;
                }
                else
                {
                    Circles[i].Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws each of the circles
            foreach (Circle circle in Circles)
            {
                spriteBatch.Draw(circle.Btn.Img, circle.Btn.Rec, circle.CircleColor);
            }
        }
    }
}
