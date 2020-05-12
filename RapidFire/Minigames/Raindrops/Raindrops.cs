// AUthor: Eric Pu
// File Name: Raindrops.cs
// Project Name: RapidFire
// Creation Date: May 29th, 2019
// Modified Date: May 29th, 2019
// Description: Class for the Raindrops minigame

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
    public class Raindrops
    {
        // Properties of the player
        public Vector2 PlayerSize;

        public Vector2 PlayerLoc;

        public Vector2 PlayerVel;

        // List of the raindrops and the spawn cooldown
        public List<Projectile> Projectiles;

        public Cooldown RaindropCd;

        public Raindrops()
        {
            // Creates the player 
            PlayerSize = new Vector2(100, 100);
            PlayerLoc = Globals.StartAtMiddle(PlayerSize);
            PlayerVel = new Vector2(10, 0);
            Globals.Player = new Player(PlayerLoc, PlayerSize, PlayerVel);

            // Difficulty affects spawn rate of the raindrops
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        RaindropCd = new Cooldown(8);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        RaindropCd = new Cooldown(5);
                        break;
                    }
                case Globals.HARD:
                    {
                        RaindropCd = new Cooldown(3);
                        break;
                    }
            }

            Globals.GameDuration = new Cooldown(600);

            Projectiles = new List<Projectile>();
        }

        public void Update()
        {
            // If the player runs out of time, the game ends. 
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.WonGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            Globals.Player.PlayerMovement();

            // If the cooldown for riandrop spawning is met, a new raindrop is spawned
            if (RaindropCd.CheckCooldown())
            {
                Projectiles.Add(new Projectile
                (
                    // Spawns at a random X location
                    new Vector2(Globals.Rng.Next(0, Globals.ScreenWidth), 0),

                    // No X velocity, Y velocity of 5
                    new Vector2(0, 5),

                    // Each raindrop is 10x20
                    new Vector2(10, 20))
               );
            }
            else
            {
                RaindropCd.UpdateCooldown();
            }

            // Moves each of the raindrops. If it touches the player, the game ends
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Move();

                if (Projectiles[i].CheckCollision(Globals.Player.GetRec()))
                {
                    MinigameSelection.LostGame();
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White);

            // Draws each of the raindrops and the player
            foreach (Projectile raindrop in Projectiles)
            {
                spriteBatch.Draw(Game1.raindropImg, raindrop.GetRec(), Color.Blue);
            }

            // Draws the player
            Game1.playerAni.Update(gameTime);
            Game1.playerAni.destRec = Globals.Player.GetRec();
            Game1.playerAni.Draw(spriteBatch, Color.White, SpriteEffects.None);
        }
    }
}
