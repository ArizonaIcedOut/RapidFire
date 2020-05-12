// AUthor: Eric Pu
// File Name: TerminalVelocity.cs
// Project Name: RapidFire
// Creation Date: June 5th, 2019
// Modified Date: June 5th, 2019
// Description: Class for the Terminal Velocity minigame

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
    public class TerminalVelocity
    {
        // Properties of the player
        public Vector2 PlayerSize;

        public Vector2 PlayerLoc;

        public Vector2 PlayerVel;

        // List of spikes and the maximum amount, and the spawn cooldown
        public List<SideSpike> Spikes;

        public int MaxSpikes;

        public Cooldown SpikeSpawnCd;

        // The walls of the pit
        public static int LeftWallX;

        public static int RightWallX;

        public TerminalVelocity()
        {
            // Creates the player
            PlayerSize = new Vector2(100, 100);
            PlayerLoc = new Vector2(Globals.StartAtMiddle(PlayerSize).X, 200);
            PlayerVel = new Vector2(10, 0);
            Globals.Player = new Player(PlayerLoc, PlayerSize, PlayerVel);

            Spikes = new List<SideSpike>();

            Globals.GameDuration = new Cooldown(600);

            // The walls of the pit
            LeftWallX = 400;
            RightWallX = 800;

            // Difficulty affects spawn rate of the spikes
            switch (Globals.Difficulty)
            {
                case Globals.EASY:
                    {
                        SpikeSpawnCd = new Cooldown(45);
                        break;
                    }
                case Globals.MEDIUM:
                    {
                        SpikeSpawnCd = new Cooldown(30);
                        break;
                    }
                case Globals.HARD:
                    {
                        SpikeSpawnCd = new Cooldown(20);
                        break;
                    }
            }
        }

        public void Update()
        {
            // If the player runs out of time, they lose. Otherwise, the timer is updated.
            if (Globals.GameDuration.CheckCooldown())
            {
                MinigameSelection.WonGame();
            }
            else
            {
                Globals.GameDuration.UpdateCooldown();
            }

            Globals.Player.PlayerMovement();

            // Moves each of the spikes. If they go off the screen, they are removed. If they touch the player, the game ends.
            for (int i = 0; i < Spikes.Count; i++)
            {
                Spikes[i].Move();
                if (Spikes[i].Y < 0)
                {
                    Spikes.RemoveAt(i);
                    i--;
                }
                else if (Spikes[i].CheckPlayer())
                {
                    MinigameSelection.LostGame();
                    break;
                }
            }

            // If the spawn cooldown is met, a spike is spawned. Otherwise, the cooldown is updated.
            if (SpikeSpawnCd.CheckCooldown())
            {
                Spikes.Add(new SideSpike());
            }
            else
            {
                SpikeSpawnCd.UpdateCooldown();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        { 
            // Draws the walls
            spriteBatch.Draw(Game1.pitBg, new Rectangle(0, 0, LeftWallX, Globals.ScreenHeight), Color.White);
            spriteBatch.Draw(Game1.pitBg, new Rectangle(RightWallX, 0, LeftWallX, Globals.ScreenHeight), Color.White);

            // Draws the player
            Game1.playerAni.Update(gameTime);
            Game1.playerAni.destRec = Globals.Player.GetRec();
            Game1.playerAni.Draw(spriteBatch, Color.White, SpriteEffects.None);


            // Draws each of the spikes
            foreach (SideSpike spike in Spikes)
            {
                if (spike.Side == SideSpike.LEFT) spriteBatch.Draw(Game1.leftSpikeImg, spike.GetRec(), Color.White);
                else spriteBatch.Draw(Game1.rightSpikeImg, spike.GetRec(), Color.White);
            }
        }

    }
}
