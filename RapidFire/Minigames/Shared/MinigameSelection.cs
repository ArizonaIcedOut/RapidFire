// AUthor: Eric Pu
// File Name: MinigameSelection.cs
// Project Name: RapidFire
// Creation Date: June 5th, 2019
// Modified Date: June 6th, 2019
// Description: Class for selecting random minigames and resetting the game

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

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
    public class MinigameSelection
    {
        public static List<int> PastMinigames = new List<int>();

        // The same game cannot be chosen again for 5 games
        public static int CannotRepeatPeriod = 5;

        public static int SelectedGame;

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Logic for if the player won the last game
        /// </summary>
        public static void WonGame()
        {
            Game1.winSnd.CreateInstance().Play();

            // If practice mode is on, the player returns to the menu instead of the next minigame. Otherwise, they move on to the WonGame screen.
            if (Globals.Practice)
            {
                Globals.Fade.Start(Globals.TRANSITION, 1f);
                Globals.LastGameWon = true;
                return;
            }
            else
            {
                Globals.LastGameWon = true;
                Globals.Score++;

                Globals.Fade.Start(Globals.TRANSITION, 1f);
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Logic for if the player lost the last game
        /// </summary>
        public static void LostGame()
        {
            Game1.lossSnd.CreateInstance().Play();

            if (Globals.Practice)
            {
                Globals.Fade.Start(Globals.TRANSITION, 1f);
                Globals.LastGameWon = false;
                return;
            }
            else
            {
                if (Globals.Health == 0)
                {
                    Globals.Fade.Start(Globals.RESULTS, 1);
                }
                else
                {
                    Globals.Health--;
                }

                Globals.LastGameWon = false;
                Globals.Fade.Start(Globals.TRANSITION, 1f);
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Logic for starting the next game
        /// </summary>
        public static void NextGame()
        {
            // If the player's score is divisible by 5, the boss stage starts
            if (Globals.Score % 5 == 0 && Globals.Score != 0)
            {
                SelectedGame = Globals.FLOOR_IS_LAVA;
            }
            else
            {
                // If the minigame generated has already been chosen in the last 3 games, a new one is chosen. 
                while (true)
                {
                    // GamesNum is reduced by 1 as Floor is Lava is not within the normal game pool
                    SelectedGame = Globals.Rng.Next(Globals.GamesNum - 1);

                    if (!PastMinigames.Contains(SelectedGame)) break;
                }
            }

            Globals.Practice = false;

            // Logic for starting the generated minigame
            switch (SelectedGame)
            {
                case Globals.LUCKY_DAY:
                    {
                        Globals.Minigame = Globals.LUCKY_DAY;
                        Game1.luckyDay = new LuckyDay();
                        break;
                    }
                case Globals.COLOR_GUESSER:
                    {
                        Globals.Minigame = Globals.COLOR_GUESSER;
                        Game1.colorGuesser = new ColorGuesser();
                        break;
                    }
                case Globals.RAINDROPS:
                    {
                        Globals.Minigame = Globals.RAINDROPS;
                        Game1.raindrops = new Raindrops();
                        break;
                    }
                case Globals.JUGGLER:
                    {
                        Globals.Minigame = Globals.JUGGLER;
                        Game1.juggler = new Juggler();
                        break;
                    }
                case Globals.TERMINAL_VELOCITY:
                    {
                        Globals.Minigame = Globals.TERMINAL_VELOCITY;
                        Game1.terminalVelocity = new TerminalVelocity();
                        break;
                    }
                case Globals.BACKWARDS:
                    {
                        Globals.Minigame = Globals.BACKWARDS;
                        Game1.backwards = new Backwards();
                        break;
                    }
                case Globals.TYPEWRITER:
                    {
                        Globals.Minigame = Globals.TYPEWRITER;
                        Game1.typewriter = new Typewriter();
                        break;
                    }
                case Globals.FLOOR_IS_LAVA:
                    {
                        Globals.Minigame = Globals.FLOOR_IS_LAVA;
                        Game1.floorIsLava = new FloorIsLava();
                        break;
                    }
                case Globals.MILKSHAKER:
                    {
                        Globals.Minigame = Globals.MILKSHAKER;
                        Game1.milkshaker = new Milkshaker();
                        break;
                    }
                case Globals.CIRCLE_CLICKER:
                    {
                        Globals.Minigame = Globals.CIRCLE_CLICKER;
                        Game1.circleClicker = new CircleClicker();
                        break;
                    }
            }

            // Adds the chosen minigame to the list of past minigames
            PastMinigames.Add(SelectedGame);

            // If the list of past minigames surpasses its maximum length, the first element is removed (each minigame cannot be repeated for CannotRepeatPeriod, which is 3)
            if (PastMinigames.Count > CannotRepeatPeriod)
            {
                PastMinigames.RemoveAt(0);
            }

            // Opens the help screen at the start
            if (Globals.HelpScreensEnabled) Globals.HelpScreenOpen = true;

            // Gamestate switches to gameplay
            Globals.Fade.Start(Globals.GAMEPLAY, 1);
        }

        /// <summary>
        /// Pre: minigame as the minigame chosen
        /// Post: n/a
        /// description: Starts the minigame that the player chose in the practice menu
        /// </summary>
        /// <param name="minigame"></param>
        public static void StartPractice(int minigame)
        {
            Globals.Practice = true;

            // Game starting logic
            switch (minigame)
            {
                case Globals.LUCKY_DAY:
                    {
                        Globals.Minigame = Globals.LUCKY_DAY;
                        Game1.luckyDay = new LuckyDay();
                        break;
                    }
                case Globals.COLOR_GUESSER:
                    {
                        Globals.Minigame = Globals.COLOR_GUESSER;
                        Game1.colorGuesser = new ColorGuesser();
                        break;
                    }
                case Globals.RAINDROPS:
                    {
                        Globals.Minigame = Globals.RAINDROPS;
                        Game1.raindrops = new Raindrops();
                        break;
                    }
                case Globals.JUGGLER:
                    {
                        Globals.Minigame = Globals.JUGGLER;
                        Game1.juggler = new Juggler();
                        break;
                    }
                case Globals.TERMINAL_VELOCITY:
                    {
                        Globals.Minigame = Globals.TERMINAL_VELOCITY;
                        Game1.terminalVelocity = new TerminalVelocity();
                        break;
                    }
                case Globals.BACKWARDS:
                    {
                        Globals.Minigame = Globals.BACKWARDS;
                        Game1.backwards = new Backwards();
                        break;
                    }
                case Globals.TYPEWRITER:
                    {
                        Globals.Minigame = Globals.TYPEWRITER;
                        Game1.typewriter = new Typewriter();
                        break;
                    }
                case Globals.FLOOR_IS_LAVA:
                    {
                        Globals.Minigame = Globals.FLOOR_IS_LAVA;
                        Game1.floorIsLava = new FloorIsLava();
                        break;
                    }
                case Globals.MILKSHAKER:
                    {
                        Globals.Minigame = Globals.MILKSHAKER;
                        Game1.milkshaker = new Milkshaker();
                        break;
                    }
                case Globals.CIRCLE_CLICKER:
                    {
                        Globals.Minigame = Globals.CIRCLE_CLICKER;
                        Game1.circleClicker = new CircleClicker();
                        break;
                    }
            }
            Globals.HelpScreenOpen = true;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Resets variables for the game
        /// </summary>
        public static void Reset()
        {
            Globals.Score = 0;
            Globals.Health = Globals.BaseHealth;
            PastMinigames = new List<int>();
        }
    }
}
