// AUthor: Eric Pu
// File Name: Juggler.cs
// Project Name: RapidFire
// Creation Date: May 30th, 2019
// Modified Date: June 4th, 2019
// Description: Class for the JUggler minigame

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
    public class Juggler
    {
        // Properties of the player
        public Vector2 PlayerSize;

        public Vector2 PlayerLoc;

        public Vector2 PlayerVel;

        // The projectile for the ball
        public Projectile Ball;

        public Juggler()
        {
            PlayerSize = new Vector2(100, 100);
            PlayerLoc = Globals.StartAtMiddle(PlayerSize);
            PlayerVel = new Vector2(10, 0);
            Globals.Player = new Player(PlayerLoc, PlayerSize, PlayerVel);

            Ball = new Projectile
            (
                // Spawns a ball at any X value on the screen, 100 pixels below the top of the screen
                new Vector2(Globals.Rng.Next(Globals.ScreenWidth - 100), 100), 

                // Generates an X velocity between -5 and 5, with a Y velocity of 0 (which is then increased by gravity)
                new Vector2(Globals.Rng.Next(-5, 5), 0), 

                // The ball has a width and height of 100
                new Vector2(100, 100)
            );

            // Difficulty affects duration of the game and the X velocity of the ball
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        Globals.GameDuration = new Cooldown(400);

                        Ball.Vel = new Vector2(Globals.Rng.Next(-5, 5), 0);

                        // This loop ensures that the X velocity is not 0 (which would break the game, as it would not move horizontally)
                        while (Ball.Vel.X == 0)
                        {
                            Ball.Vel.X = Globals.Rng.Next(-5, 5);
                        }
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        Globals.GameDuration = new Cooldown(600);

                        Ball.Vel = new Vector2(Globals.Rng.Next(-7, 7), 0);
                        
                        while (Ball.Vel.X == 0)
                        {
                            Ball.Vel.X = Globals.Rng.Next(-7, 7);
                        }
                        break;
                    }
                case Globals.HARD:
                    {
                        Globals.GameDuration = new Cooldown(900);

                        Ball.Vel = new Vector2(Globals.Rng.Next(-10, 10), 0);
                        
                        while (Ball.Vel.X == 0)
                        {
                            Ball.Vel.X = Globals.Rng.Next(-10, 10);
                        }
                        break;
                    }
            }
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

            // Moves the ball and the player
            Ball.Move();
            Globals.Player.PlayerMovement();

            // If the ball touches any of the boundaries, it bounces off
            if (Ball.CheckCollision(Globals.Player.GetRec()) || Ball.Pos.Y <= 0)
            {
                Ball.Vel.Y *= -1;
                Game1.bounceSnd.CreateInstance().Play();
            }
            else if (Ball.Pos.X + Ball.Size.X >= Globals.ScreenWidth || Ball.Pos.X <= 0)
            {
                Ball.Vel.X *= -1;
                Game1.bounceSnd.CreateInstance().Play();
            }

            // If the ball reaches the ground, the game ends
            if (Ball.Pos.Y + Ball.Size.Y >= Globals.ScreenHeight)
            {
                MinigameSelection.LostGame();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Background
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White);

            // Draws the player and the ball
            Game1.playerAni.Update(gameTime);
            Game1.playerAni.destRec = Globals.Player.GetRec();
            Game1.playerAni.Draw(spriteBatch, Color.White, SpriteEffects.None);

            Game1.ballAni.Update(gameTime);
            Game1.ballAni.destRec = Ball.GetRec();
            Game1.ballAni.Draw(spriteBatch, Color.White, SpriteEffects.None);
        }
    }
}
