// AUthor: Eric Pu
// File Name: Menu.cs
// Project Name: RapidFire
// Creation Date: May 29th, 2019
// Modified Date: June 6th, 2019
// Description: Class for menu elements and graphics

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
    public class Menu
    {
        // Screen constants
        public const int START = 0;

        public const int PRACTICE = 1;

        public const int DIFFICULTY = 2;

        public const int HELP = 3;

        public static int Screen = START;

        // Start screen buttons
        public static Button StartBtn;

        public static Button PracticeBtn;

        public static Button LeaderboardBtn;

        public static Button HelpBtn;

        // Practice screen buttons
        public static Button IncDifficultyBtn;

        public static Button DecDifficultyBtn;

        public static Button BackBtn;

        // Difficulty buttons
        public static Button EasyBtn;

        public static Button MediumBtn;

        public static Button HardBtn;

        // Help Screen
        public static List<string> HelpMessages = new List<string>
        {
            "WELCOME TO RAPIDFIRE!",
            "RAPIDFIRE! IS A COLLECTION OF SHORT MINIGAMES",
            "YOUR GOAL IS TO COMPLETE AS MANY AS POSSIBLE",
            "EVERY 5 GAMES, THERE IS A BOSS GAME",
            "FAIL ONE MINIGAME AND YOU LOSE ONE LIFE",
            "RUN OUT OF ALL THREE LIVES AND YOU LOSE!"
        };


        // Creates a list of buttons for the games, and a list of the names of each game
        public static List<Button> PracticeGames = new List<Button>();

        public static List<string> GameNames = new List<string>
        {
            "COLOR GUESSER",
            "LUCKY DAY",
            "RAINDROPS",
            "JUGGLER",
            "TERMINAL VELOCITY",
            "BACKWARDS",
            "TYPEWRITER",
            "CIRCLE CLICKER",
            "MILKSHAKER",
            "FLOOR IS LAVA"
        };

        public static void Update()
        {
            switch (Screen)
            {
                case START:
                    {
                        // Checks each of the buttons on the start screen
                        if (StartBtn.CheckButton())
                        {
                            Screen = DIFFICULTY;
                        }
                        else if (PracticeBtn.CheckButton())
                        {
                            Screen = PRACTICE;
                            Globals.Difficulty = Globals.MEDIUM;
                        }
                        else if (LeaderboardBtn.CheckButton())
                        {
                            Globals.Gamestate = Globals.LEADERBOARD;
                        }
                        else if (HelpBtn.CheckButton())
                        {
                            Screen = HELP;
                        }
                        break;
                    }
                case PRACTICE:
                    {
                        // Checks the button for each of the practice games. If it is clicked, it starts the game.
                        for (int i = 0; i < PracticeGames.Count; i++)
                        {
                            if (PracticeGames[i].CheckButton())
                            {
                                MinigameSelection.StartPractice(i);
                                Globals.Fade.Start(Globals.GAMEPLAY, 1);
                                break;
                            }
                        }

                        // Difficulty changing buttons
                        if (IncDifficultyBtn.CheckButton() && Globals.Difficulty != Globals.HARD)
                        {
                            Globals.Difficulty++;
                        }
                        else if (DecDifficultyBtn.CheckButton() && Globals.Difficulty != Globals.EASY)
                        {
                            Globals.Difficulty--;
                        }

                        // Back button
                        if (BackBtn.CheckButton())
                        {
                            Screen = START;
                        }
                        break;
                    }
                case DIFFICULTY:
                    {
                        // Checks each of the difficulty buttons, and starts the game with the corresponding difficulty
                        if (EasyBtn.CheckButton())
                        {
                            Globals.Fade.Start(Globals.GAMEPLAY, 1);
                            MinigameSelection.NextGame();
                            Globals.Difficulty = Globals.EASY;
                            MinigameSelection.Reset();
                        }
                        else if (MediumBtn.CheckButton())
                        {
                            Globals.Fade.Start(Globals.GAMEPLAY, 1);
                            MinigameSelection.NextGame();
                            Globals.Difficulty = Globals.MEDIUM;
                            MinigameSelection.Reset();
                        }
                        else if (HardBtn.CheckButton())
                        {
                            Globals.Fade.Start(Globals.GAMEPLAY, 1);
                            MinigameSelection.NextGame();
                            Globals.Difficulty = Globals.HARD;
                            MinigameSelection.Reset();
                        }
                        else if (BackBtn.CheckButton())
                        {
                            Screen = START;
                        }
                        break;
                    }
                case HELP:
                    {
                        // Back button
                        if (BackBtn.CheckButton())
                        {
                            Screen = START;
                        }
                        break;
                    }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Background
            spriteBatch.Draw(Game1.meadowBg, Globals.GetRec(), Color.White * .7f);

            // Draws the current screen of the menu
            switch (Screen)
            {
                case START:
                    {
                        // Draws the logo
                        spriteBatch.Draw(Game1.rapidfireImg, new Rectangle(400, 50, 400, 100), Color.White);

                        // Draws each of the buttons
                        spriteBatch.Draw(StartBtn.Img, StartBtn.Rec, Color.White);
                        spriteBatch.Draw(PracticeBtn.Img, PracticeBtn.Rec, Color.White);
                        spriteBatch.Draw(LeaderboardBtn.Img, LeaderboardBtn.Rec, Color.White);
                        spriteBatch.Draw(HelpBtn.Img, HelpBtn.Rec, Color.White);
                        break;
                    }
                case PRACTICE:
                    {
                        // Draws each of the practice buttons
                        for (int i = 0; i < PracticeGames.Count; i++)
                        {
                            spriteBatch.Draw(PracticeGames[i].Img, PracticeGames[i].Rec, Color.White);
                            spriteBatch.DrawString(Game1.smallFont, GameNames[i], Globals.CentreText(PracticeGames[i].Rec, GameNames[i], Game1.smallFont), Color.White);
                        }

                        // Back button
                        spriteBatch.Draw(BackBtn.Img, BackBtn.Rec, Color.White);

                        // Draws the difficulty changing buttons if they can be used
                        if (Globals.Difficulty != Globals.HARD)
                        {
                            spriteBatch.Draw(IncDifficultyBtn.Img, IncDifficultyBtn.Rec, Color.White);
                        }

                        if (Globals.Difficulty != Globals.EASY)
                        {
                            spriteBatch.Draw(DecDifficultyBtn.Img, DecDifficultyBtn.Rec, Color.White);
                        }

                        // Draws the current difficulty
                        switch (Globals.Difficulty)
                        {
                            case Globals.EASY:
                                {
                                    spriteBatch.Draw(EasyBtn.Img, new Rectangle(500, 650, 200, 70), Color.White);
                                    break;
                                }
                            case Globals.MEDIUM:
                                {
                                    spriteBatch.Draw(MediumBtn.Img, new Rectangle(500, 650, 200, 70), Color.White);
                                    break;
                                }
                            case Globals.HARD:
                                {
                                    spriteBatch.Draw(HardBtn.Img, new Rectangle(500, 650, 200, 70), Color.White);
                                    break;
                                }
                        }
                        break;
                    }
                case DIFFICULTY:
                    {
                        // Draws each of the difficulty buttons
                        spriteBatch.Draw(EasyBtn.Img, EasyBtn.Rec, Color.White);
                        spriteBatch.Draw(MediumBtn.Img, MediumBtn.Rec, Color.White);
                        spriteBatch.Draw(HardBtn.Img, HardBtn.Rec, Color.White);

                        // Back button
                        spriteBatch.Draw(BackBtn.Img, BackBtn.Rec, Color.White);
                        break;
                    }
                case HELP:
                    {
                        // Displays each of the help messages
                        for (int i = 0; i < HelpMessages.Count; i++)
                        {
                            spriteBatch.DrawString(Game1.medFont, HelpMessages[i], 
                                new Vector2(Globals.CentreText(Globals.GetRec(), HelpMessages[i], Game1.medFont).X, 50 + (i * 100)), Color.Black);
                        }

                        // Back button
                        spriteBatch.Draw(BackBtn.Img, BackBtn.Rec, Color.White);

                        break;
                    }
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Loads in the menu buttons. Buttons cannot be defined at the start because the images in Game1 must be loaded in first.
        /// </summary>
        public static void LoadButtons()
        {
            // Creates a button for each of the minigames
            for (int i = 0; i < Globals.GamesNum; i++)
            {
                PracticeGames.Add(new Button(Game1.emptyBtnImg, new Rectangle(300, (i * 60) + 50, 600, 40)));
            }

            // Start buttons
            StartBtn = new Button(Game1.startBtnImg, new Rectangle(450, 200, 300, 100));
            PracticeBtn = new Button(Game1.practiceBtnImg, new Rectangle(450, 350, 300, 100));
            LeaderboardBtn = new Button(Game1.rankingsBtnImg, new Rectangle(450, 500, 300, 100));
            HelpBtn = new Button(Game1.helpBtnImg, new Rectangle(450, 650, 300, 100));

            // Practice buttons
            IncDifficultyBtn = new Button(Game1.addBtnImg, new Rectangle(750, 660, 50, 50));
            DecDifficultyBtn = new Button(Game1.subtractBtnImg, new Rectangle(400, 660, 50, 50));
            BackBtn = new Button(Game1.backBtnImg, new Rectangle(450, 750, 300, 100));

            // Difficulty buttons
            EasyBtn = new Button(Game1.easyBtnImg, new Rectangle(450, 50, 300, 100));
            MediumBtn = new Button(Game1.mediumBtnImg, new Rectangle(450, 200, 300, 100));
            HardBtn = new Button(Game1.hardBtnImg, new Rectangle(450, 350, 300, 100));
        }
    }
}
