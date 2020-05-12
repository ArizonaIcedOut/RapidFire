// AUthor: Eric Pu
// File Name: Milkshaker.cs
// Project Name: RapidFire
// Creation Date: June 10th, 2019
// Modified Date: June 10th, 2019 
// Description: Class for the Milkshaker minigame

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
    public class Milkshaker
    {
        // Jug properties
        public Button Jug;

        public bool JugHeld;

        public Vector2 JugDistanceMoved;

        public Vector2 JugSize;

        // Distance properties
        public float DistanceTravelled;

        public float DistanceNeeded;

        public Milkshaker()
        {
            // Difficulty affects distance travelled necessary
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        DistanceNeeded = 15000;
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        DistanceNeeded = 20000;
                        break;
                    }
                case Globals.HARD:
                    {
                        DistanceNeeded = 25000;
                        break;
                    }
            }

            // Creates the jug
            JugSize = new Vector2(100, 300);
            Jug = new Button(Game1.blankRecImg, new Rectangle(500, 200, (int)JugSize.X, (int)JugSize.Y));
            JugHeld = false;

            Globals.GameDuration = new Cooldown(300);

            DistanceTravelled = 0;
        }

        public void Update()
        {
            // If the player has filled the distance bar, they move on. If they run out of time, they lose. Otherwise, the timer is updated.
            if (DistanceTravelled > DistanceNeeded)
            {
                MinigameSelection.WonGame();
            }
            else if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.LostGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            // If the jug is not currently held and the player clicks on it, it becomes held. Otherwise, the movement of the jug is checked.
            if (!JugHeld && Jug.CheckButton()) JugHeld = true;
            else if (JugHeld)
            {
                // If the player lets go of the jug, they no longer hold it. Otherwise, the distance travelled is updated and the jug is moved.
                if (Globals.MouseCurrent.LeftButton != ButtonState.Pressed) JugHeld = false;
                else
                {
                    JugDistanceMoved = new Vector2(Globals.MouseCurrent.X - Globals.MousePast.X, Globals.MouseCurrent.Y - Globals.MousePast.Y);
                    DistanceTravelled += Math.Abs(JugDistanceMoved.Y);

                    Jug = new Button(Game1.blankRecImg, new Rectangle((int)(Jug.Rec.X + JugDistanceMoved.X), (int)(Jug.Rec.Y + JugDistanceMoved.Y), (int)JugSize.X, (int)JugSize.Y));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the jug
            spriteBatch.Draw(Game1.milkshakeImg, Jug.Rec, Color.White);

            // Draws the progress bar
            spriteBatch.Draw(Game1.blankRecImg,
                Hud.CreateBar(DistanceTravelled, DistanceNeeded, 10, 40, Globals.ScreenWidth - 10, 20), Color.Blue);
        }
    }
}
