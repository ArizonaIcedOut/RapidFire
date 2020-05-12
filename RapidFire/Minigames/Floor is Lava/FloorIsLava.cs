// AUthor: Eric Pu
// File Name: FloorIsLava.cs
// Project Name: RapidFire
// Creation Date: June 10th, 2019
// Modified Date: June 10th, 2019
// Description: Class for the Floor is Lava minigame

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
    public class FloorIsLava
    {
        // Properties of the player
        public Vector2 PlayerSize;

        public Vector2 PlayerLoc;

        public Vector2 PlayerVel;

        public bool PlayerInAir;

        public int StartY;

        public float Gravity;

        public bool IsOnPlatform;

        // Lava properties
        public Rectangle Lava;

        public int LavaHeight;

        public int LavaRiseRate;

        // Platforms and platform properties
        public List<Rectangle> Platforms;

        public Vector2 PlatformSize;

        public int PlatformSpawnInterval;

        // Platform spawning values
        public int ExploredY;

        public int CurrentY;

        public int LastPlatformSpawn;

        public FloorIsLava()
        {
            // Creates the player
            StartY = Globals.ScreenHeight / 2;
            PlayerSize = new Vector2(50, 50);
            PlayerLoc = new Vector2(Globals.StartAtMiddle(PlayerSize).X, StartY);
            PlayerVel = new Vector2(10, 0);
            Globals.Player = new Player(PlayerLoc, PlayerSize, PlayerVel);

            // Player motion properties
            PlayerInAir = false;
            Gravity = .1f;
            IsOnPlatform = false;

            // Creates the lava
            LavaHeight = 200;
            LavaRiseRate = 3;
            Lava = new Rectangle(0, Globals.ScreenHeight - LavaHeight, Globals.ScreenWidth, LavaHeight);

            // Difficulty affects duration of the game and the width of platforms
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        Globals.GameDuration = new Cooldown(900);
                        PlatformSize = new Vector2(200, 20);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        Globals.GameDuration = new Cooldown(1800);
                        PlatformSize = new Vector2(150, 20);
                        break;
                    }
                case Globals.HARD:
                    {
                        Globals.GameDuration = new Cooldown(2700);
                        PlatformSize = new Vector2(100, 20);
                        break;
                    }
            }

            // Sets platform spawning values
            CurrentY = StartY;
            ExploredY = CurrentY;
            PlatformSpawnInterval = 200;
            LastPlatformSpawn = StartY;

            // Creates the initial platforms at the start of the game
            Platforms = new List<Rectangle>();
            SpawnInitialPlatforms();
        }

        public void Update()
        {
            // Player wins if he survives for the length of the game, and loses if they touch the lava
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.WonGame();
            }
            else if (Globals.Player.CheckCollision(Lava))
            {
                MinigameSelection.LostGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            // If the player is in the air (indicated by a non-zero Y velocity), it is indicated and their velocity is increased by gravity 
            if (Globals.Player.Vel.Y != 0)
            {
                Globals.Player.Vel.Y += Gravity;
                PlayerInAir = true;
            }

            // Sets IsOnPlatform to false, which is checked right after
            IsOnPlatform = false;

            // Goes through each platform
            // If the player is in the air and they touch a platform, it is now indicated that they are on a platform, no longer in the air, and their Y velocity is reset
            // If the player is in the air and they touch a platform, their Y velocity is reset and it is indicated
            // Otherwise, if the platform touches the lava, it disappears
            for (int i = 0; i < Platforms.Count; i++)
            {
                if (PlayerInAir && Globals.Player.CheckPlatformCollision(Platforms[i]))
                {
                    IsOnPlatform = true;
                    PlayerInAir = false;
                    Globals.Player.Vel.Y = 0;
                }
                else if (Globals.Player.CheckOnPlatform(Platforms[i]))
                {
                    Globals.Player.Vel.Y = 0;
                    IsOnPlatform = true;
                }
                else if (Platforms[i].Y > Lava.Y)
                {
                    Platforms.RemoveAt(i);
                    i++;
                }
            }

            // If the player is not on a platform and they are not in the air, they begin falling (Y velocity has to be set to .001f so it is not zero)
            if (!IsOnPlatform && !PlayerInAir)
            {
                PlayerInAir = true;
                Globals.Player.Vel.Y = .001f;
            }

            // Moves player, platforms, and lava
            Globals.Player.PlayerMovement();
            for (int i = 0; i < Platforms.Count; i++)
            {
                Platforms[i] = MovePlatform(Platforms[i], (int)Globals.Player.Vel.Y);
            }
            MoveLava();

            // If the player's velocity is below 0 (going up), the CurrentY increases (you must subtract since the Y velocity is negative) 
            if (Globals.Player.Vel.Y < 0) CurrentY -= (int)Globals.Player.Vel.Y;

            // If the CurrentY is greater than ExploredY, it is updated
            if (CurrentY > ExploredY)
            {
                ExploredY = CurrentY;
            }

            // Checks if the player has moved enough to spawn another platform, and spawns a new platform if so
            if (ExploredY > LastPlatformSpawn + PlatformSpawnInterval)
            {
                SpawnPlatforms();
                LastPlatformSpawn = ExploredY;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draws player, lava, and platforms
            spriteBatch.Draw(Game1.volcanoBg, Globals.GetRec(), Color.White * .7f);

            foreach (Rectangle platform in Platforms)
            {
                spriteBatch.Draw(Game1.platformImg, platform, Color.White);
            }

            // Draws lava and player animations
            Game1.lavaAni.Update(gameTime);
            Game1.lavaAni.destRec = Lava;
            Game1.lavaAni.Draw(spriteBatch, Color.White, SpriteEffects.None);

            Game1.playerAni.Update(gameTime);
            Game1.playerAni.destRec = Globals.Player.GetRec();
            Game1.playerAni.Draw(spriteBatch, Color.White, SpriteEffects.None);
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Spawns a new platform at a random X location, the top of the screen, with the dimensions of the platform
        /// </summary>
        public void SpawnPlatforms()
        {
            Platforms.Add(new Rectangle(Globals.Rng.Next(Globals.ScreenWidth - (int)PlatformSize.X), 0,
                (int)PlatformSize.X, (int)PlatformSize.Y));
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Spawns the initial platforms at the start of the game
        /// </summary>
        public void SpawnInitialPlatforms()
        {
            // Spawns a platform in the middle of the screen, right under the player
            Platforms.Add(new Rectangle(Globals.ScreenWidth / 2 - (int)PlatformSize.X / 2, StartY + (int)PlayerSize.Y,
                (int)PlatformSize.X, (int)PlatformSize.Y));

            // Spawns the rest of the platforms at the start of the game
            for (int i = 0; i <= StartY / PlatformSpawnInterval - 1; i++)
            {
                Platforms.Add(new Rectangle(Globals.Rng.Next(Globals.ScreenWidth - (int)PlatformSize.X), (int)Globals.Player.Size.Y + (i * PlatformSpawnInterval), 
                    (int)PlatformSize.X, (int)PlatformSize.Y));
            }
        }

        /// <summary>
        /// Pre: platform as the platform being moved, velocityY as the Y velocity of the player
        /// Post: Returns a rectangle with the updated Y position
        /// Description: Moves a platform according to the player's Y velocity
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="velocityY"></param>
        /// <returns></returns>
        public Rectangle MovePlatform(Rectangle platform, int velocityY)
        {
            return new Rectangle(platform.X, platform.Y - velocityY, platform.Width, platform.Height);
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Desecription: Moves the lava according to whether the player is on a platform or not
        /// </summary>
        public void MoveLava()
        {
            // If the player is on the platform, the lava rises up like normal. 
            // If they are not, the lava rises at half the rate as normal. The Y position is updated by the player's Y velocity, so jumping up decreases the height
            if (IsOnPlatform)
            {
                int lavaHeight = Lava.Height + LavaRiseRate;
                Lava = new Rectangle(0, Globals.ScreenHeight - lavaHeight, Globals.ScreenWidth, lavaHeight);
            }
            else
            {
                int lavaHeight = Lava.Height + (int)Globals.Player.Vel.Y + LavaRiseRate / 2;
                Lava = new Rectangle(0, Globals.ScreenHeight - lavaHeight, Globals.ScreenWidth, lavaHeight);
            }
        }

    }
}
