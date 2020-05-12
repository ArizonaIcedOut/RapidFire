// AUthor: Eric Pu
// File Name: ClickableCircle.cs
// Project Name: RapidFire
// Creation Date: June 10th, 2019
// Modified Date: June 10th, 2019 
// Description: Class for circles in the Circle Clicker minigame

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

/// <summary>
/// 
/// </summary>
namespace MONO_TEST
{
    public class Circle
    {
        // Spawn location 
        public Vector2 StartLoc;

        // Properties of the circle's button
        public Button Btn;

        public Rectangle Rec;

        // Cooldown for the duration of the circle
        public Cooldown Duration;

        // Diameter properties
        public int Diameter;

        public int MaxDiameterGrowth;

        public float DiameterGrowth;

        public float DiameterGrowthRate;

        // Color of the circle
        public Color CircleColor;

        public Circle()
        {
            // Diameter properties
            Diameter = 75;
            DiameterGrowth = 0;
            MaxDiameterGrowth = 75;
            DiameterGrowthRate = 0.5f;

            // Each circle lasts for 2.5 seconds
            Duration = new Cooldown(150);

            // The circle spawns at an X and Y position between 1/4 of the screen and 3/4 of the screen
            StartLoc = new Vector2(
                Globals.Rng.Next((int)(Globals.ScreenWidth * (1.0 / 4)), (int)((Globals.ScreenWidth * (3.0 / 4)))), 
                Globals.Rng.Next((int)(Globals.ScreenHeight * (1.0 / 4)), (int)((Globals.ScreenHeight * (3.0 / 4)))));

            // Creates the rectangle for the circle with the starting location
            Rec = new Rectangle((int)StartLoc.X, (int)StartLoc.Y, Diameter, Diameter);

            // Produces a button with the rectangle of the circle 
            Btn = new Button(Game1.blankCircleImg, Rec);

            // Generates a random color for the circle
            CircleColor = new Color(Globals.Rng.Next(0, 255), Globals.Rng.Next(0, 255), Globals.Rng.Next(0, 255));
        }

        public void Update()
        {
            // If the game timer surpasses the timer, the game ends. Otherwise, the timer is updated.
            if (Duration.CheckCooldown())
            {
                MinigameSelection.LostGame();
            }
            else
            {
                Duration.UpdateCooldown();
            }

            // The diameter increases by the growth rate
            DiameterGrowth += DiameterGrowthRate;

            // Produces a new button with the updated diameter
            Btn = new Button(Game1.blankCircleImg, 
                new Rectangle((int)StartLoc.X - (int)DiameterGrowth / 2, (int)StartLoc.Y - (int)DiameterGrowth / 2, Diameter + (int)DiameterGrowth, Diameter + (int)DiameterGrowth));
        }
    }
}
