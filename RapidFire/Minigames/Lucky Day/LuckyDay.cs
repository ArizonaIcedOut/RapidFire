// AUthor: Eric Pu
// File Name: LuckyDay.cs
// Project Name: RapidFire
// Creation Date: May 29th, 2019
// Modified Date: MAy 29th, 2019 
// Description: Class for the Lucky Day minigame

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
    public class LuckyDay
    {
        // List for all of the coins
        public List<Projectile> Coins;

        // Properties of the player
        public Vector2 PlayerSize;

        public Vector2 PlayerLoc;

        public Vector2 PlayerVel;

        // Goal and the amount of coins collected
        public int Goal;

        public int Collected;

        // Spawn cooldown for gold
        public Cooldown GoldCd;

        // The range of the randomly generated X velocity
        public Vector2 VelocityXRange;

        public LuckyDay()
        {
            // Player has a width and height of 50
            PlayerSize = new Vector2(100, 100);
            // Player starts at middle of the screen, at the bottom
            PlayerLoc = Globals.StartAtMiddle(PlayerSize);
            // Player has an X velocity of 10 and a starting Y velocity of 0
            PlayerVel = new Vector2(10, 0);

            Globals.Player = new Player(PlayerLoc, PlayerSize, PlayerVel);

            // Gold spawns every 25 frames
            GoldCd = new Cooldown(25);

            // Difficulty affects amount of gold needed
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        Goal = 6;
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        Goal = 10;
                        break;
                    }
                case Globals.HARD:
                    {
                        Goal = 14;
                        break;
                    }
            }

            Collected = 0;

            // Game lasts 10 seconds
            Globals.GameDuration = new Cooldown(600);

            Coins = new List<Projectile>();

            // X velocity can be between 5 and 10
            VelocityXRange = new Vector2(5, 10);
        }

        public void Update()
        {
            // If the player has collected enough coins to win, the player moves on. Otherwise, they lose.
            if (Collected >= Goal)
            {
                MinigameSelection.WonGame();
            }
            else if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.LostGame();
            }

            // The player moves
            Globals.Player.PlayerMovement();

            // If the gold spawn cooldown is met, two coins are spawned on each side. Otherwise, the cooldown is updated.
            if (GoldCd.CheckCooldown())
            {
                // Left coin
                Coins.Add(new Projectile
                (
                    // Spawns at X of 0, a Y value between 0 (top of the screen) and 2/3 of the screen's height 
                    new Vector2(0, Globals.Rng.Next(0, (int)((2.0 / 3.0) * Globals.ScreenHeight))),

                    // An X range is chosen between the range, Y velocity starts at 0
                    new Vector2(Globals.Rng.Next((int)VelocityXRange.X, (int)VelocityXRange.Y), 0),
                    
                    // Size is 30x30
                    new Vector2(30, 30))
                );
                
                // Right Coin
                Coins.Add(new Projectile
                (
                    // Spawns at the right of the screen instead
                    new Vector2(Globals.ScreenWidth - 50, Globals.Rng.Next(0, (int)(2.0 / 3.0 * Globals.ScreenHeight))),

                    // Makes the velocity negative, as it is travelling left
                    new Vector2(-1 * Globals.Rng.Next((int)VelocityXRange.X, (int)VelocityXRange.Y), 0),

                    new Vector2(30, 30))
               );
            }
            else
            {
                GoldCd.UpdateCooldown();
            }

            // Moves each of the coins. If the coin collides with the player, it is removed and the amount is collected. If it touches the ground, it is just removed. 
            for (int i = 0; i < Coins.Count; i++)
            {
                Coins[i].Move();

                if (Coins[i].CheckCollision(Globals.Player.GetRec()))
                {
                    Collected++;
                    Game1.coinSnd.CreateInstance().Play();

                    Coins.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (Coins[i].CheckBoundaries())
                {
                    Coins.RemoveAt(i);
                    i--;
                    continue;
                }
            }

            Globals.GameDuration.UpdateCooldown();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White);

            // Draws each of the coins
            foreach (Projectile coin in Coins)
            {
                spriteBatch.Draw(Game1.coinImg, coin.GetRec(), Color.Yellow);
            }

            // Draws the player
            Game1.playerAni.Update(gameTime);
            Game1.playerAni.destRec = Globals.Player.GetRec();
            Game1.playerAni.Draw(spriteBatch, Color.White, SpriteEffects.None);
        }
    }

}
