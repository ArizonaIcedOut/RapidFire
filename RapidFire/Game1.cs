// AUthor: Eric Pu
// File Name: Game1.cs
// Project Name: RapidFire
// Creation Date: May 22th, 2019
// Modified Date: June 12th, 2019
// Description: Main class for the epic summer blockbuster game RapidFire!

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

namespace RapidFire
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Minigames
        public static LuckyDay luckyDay;
        public static ColorGuesser colorGuesser;
        public static Raindrops raindrops;
        public static Juggler juggler;
        public static TerminalVelocity terminalVelocity;
        public static Backwards backwards;
        public static Typewriter typewriter;
        public static FloorIsLava floorIsLava;
        public static Milkshaker milkshaker;
        public static CircleClicker circleClicker;

        // Sprites
        public static Texture2D blankRecImg;
        public static Texture2D blankCircleImg;
        public static Texture2D logoImg;
        public static Texture2D lavaImg;
        public static Texture2D platformImg;
        public static Texture2D krampusImg;
        public static Texture2D coinImg;
        public static Texture2D ballImg;
        public static Texture2D heartImg;
        public static Texture2D keyImg;
        public static Texture2D leftSpikeImg;
        public static Texture2D rightSpikeImg;
        public static Texture2D milkshakeImg;
        public static Texture2D raindropImg;

        // Misc
        public static Texture2D timeBarImg;
        public static Texture2D progressBarImg;
        public static Texture2D successImg;
        public static Texture2D failureImg;
        public static Texture2D rapidfireImg;

        // Buttons
        public static Texture2D startBtnImg;
        public static Texture2D rankingsBtnImg;
        public static Texture2D helpBtnImg;
        public static Texture2D practiceBtnImg;
        public static Texture2D backBtnImg;
        public static Texture2D easyBtnImg;
        public static Texture2D mediumBtnImg;
        public static Texture2D hardBtnImg;
        public static Texture2D addBtnImg;
        public static Texture2D subtractBtnImg;
        public static Texture2D emptyBtnImg;
        public static Texture2D menuBtnImg;
        public static Texture2D uncheckedBtnImg;
        public static Texture2D checkedBtnImg;

        // Backgrounds
        public static Texture2D volcanoBg;
        public static Texture2D meadowBg;
        public static Texture2D pitBg;
        public static Texture2D lightPitBg;
        public static Texture2D menuBg;

        // Fonts
        public static SpriteFont bigFont;
        public static SpriteFont medFont;
        public static SpriteFont smallFont;

        // Animations
        public static Animation lavaAni;
        public static Animation playerAni;
        public static Animation ballAni;
        public static Animation menuAni;

        // Music
        static Song bgMusic;

        // Sound Effects
        public static SoundEffect clickSnd;
        public static SoundEffect coinSnd;
        public static SoundEffect lossSnd;
        public static SoundEffect winSnd;
        public static SoundEffect hitSnd;
        public static SoundEffect buttonSnd;
        public static SoundEffect jumpSnd;
        public static SoundEffect bounceSnd;

        // Misc
        public static bool paused;
        float pausedOpacity;
        Button menuBtn;

        Cooldown logoDuration;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.SynchronizeWithVerticalRetrace = false;

            IsMouseVisible = true;

            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Globals.Gamestate = Globals.LOGO;

            // Sprites
            blankRecImg = Content.Load<Texture2D>("Images/Misc/blankrec");
            blankCircleImg = Content.Load<Texture2D>("Images/Misc/blankcircle");
            logoImg = Content.Load<Texture2D>("Images/Misc/logo");
            lavaImg = Content.Load<Texture2D>("Images/Sprites/lavasheet");
            platformImg = Content.Load<Texture2D>("Images/Sprites/platform");
            krampusImg = Content.Load<Texture2D>("Images/Sprites/krampus");
            coinImg = Content.Load<Texture2D>("Images/Sprites/coin");
            ballImg = Content.Load<Texture2D>("Images/Sprites/ball");
            heartImg = Content.Load<Texture2D>("Images/Sprites/heart");
            keyImg = Content.Load<Texture2D>("Images/Sprites/key");
            leftSpikeImg = Content.Load<Texture2D>("Images/Sprites/leftspike");
            rightSpikeImg = Content.Load<Texture2D>("Images/Sprites/rightspike");
            milkshakeImg = Content.Load<Texture2D>("Images/Sprites/milkshake");
            raindropImg = Content.Load<Texture2D>("Images/Sprites/raindrop");

            // Misc
            timeBarImg = Content.Load<Texture2D>("Images/Misc/timebar");
            progressBarImg = Content.Load<Texture2D>("Images/Misc/progressbar");
            successImg = Content.Load<Texture2D>("Images/Misc/success");
            failureImg = Content.Load<Texture2D>("Images/Misc/failure");
            rapidfireImg = Content.Load<Texture2D>("Images/Misc/rapidfire");

            // Buttons
            startBtnImg = Content.Load<Texture2D>("Images/Misc/startbtn");
            rankingsBtnImg = Content.Load<Texture2D>("Images/Misc/rankingsbtn");
            helpBtnImg = Content.Load<Texture2D>("Images/Misc/helpbtn");
            practiceBtnImg = Content.Load<Texture2D>("Images/Misc/practicebtn");
            backBtnImg = Content.Load<Texture2D>("Images/Misc/back");
            easyBtnImg = Content.Load<Texture2D>("Images/Misc/easybtn");
            mediumBtnImg = Content.Load<Texture2D>("Images/Misc/mediumbtn");
            hardBtnImg = Content.Load<Texture2D>("Images/Misc/hardbtn");
            addBtnImg = Content.Load<Texture2D>("Images/Misc/add");
            subtractBtnImg = Content.Load<Texture2D>("Images/Misc/subtract");
            emptyBtnImg = Content.Load<Texture2D>("Images/Misc/emptybtn");
            menuBtnImg = Content.Load<Texture2D>("Images/Misc/menubtn");
            checkedBtnImg = Content.Load<Texture2D>("Images/Misc/checkedbtn");
            uncheckedBtnImg = Content.Load<Texture2D>("Images/Misc/uncheckedbtn");

            // Backgrounds
            volcanoBg = Content.Load<Texture2D>("Images/Backgrounds/floorislava");
            meadowBg = Content.Load<Texture2D>("Images/Backgrounds/meadow");
            pitBg = Content.Load<Texture2D>("Images/Backgrounds/pitwall");
            lightPitBg = Content.Load<Texture2D>("Images/Backgrounds/lightpit");
            menuBg = Content.Load<Texture2D>("Images/Sprites/menubg");

            // Fonts
            bigFont = Content.Load<SpriteFont>("Fonts/bigFont");
            medFont = Content.Load<SpriteFont>("Fonts/medFont");
            smallFont = Content.Load<SpriteFont>("Fonts/smallFont");

            // Animations
            lavaAni = new Animation(lavaImg, 1, 6, 6, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true);
            playerAni = new Animation(krampusImg, 4, 1, 4, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true);
            ballAni = new Animation(ballImg, 10, 1, 10, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true);
            menuAni = new Animation(menuBg, 1, 10, 10, 1, 1, Animation.ANIMATE_FOREVER, 10, new Vector2(0, 0), 1, true);

            // Audio
            bgMusic = Content.Load<Song>("Sound/Music/song");

            clickSnd = Content.Load<SoundEffect>("Sound/Effects/buttonclick");
            coinSnd = Content.Load<SoundEffect>("Sound/Effects/coin");
            lossSnd = Content.Load<SoundEffect>("Sound/Effects/losssound");
            winSnd = Content.Load<SoundEffect>("Sound/Effects/nextstage");
            hitSnd = Content.Load<SoundEffect>("Sound/Effects/hit");
            buttonSnd = Content.Load<SoundEffect>("Sound/Effects/buttonclick");
            jumpSnd = Content.Load<SoundEffect>("Sound/Effects/jump");
            bounceSnd = Content.Load<SoundEffect>("Sound/Effects/bounce");

            // Loads in words for typing games
            Globals.LoadWords();

            // Resets and loads leaderboard
            Leaderboard.Load();

            // Loads in menu buttons
            Menu.LoadButtons();

            paused = false;
            pausedOpacity = .7f;
            menuBtn = new Button(menuBtnImg, new Rectangle(450, 550, 300, 100));

            logoDuration = new Cooldown(120);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = .6f;
            MediaPlayer.Play(bgMusic);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            // Updates keyboard and mouse states
            Globals.UpdateGlobals();

            // If there is a fade active, no other update code is run
            // If the game is paused, no other update code is run and there is a check to see if the player has left it. If the help screen is open, it runs that logic. 
            // Otherwise, it runs the logic for the current mode.
            if (Globals.Fade.Active)
            {
                Globals.Fade.Update();
            }
            else if (paused)
            {
                // Checks if the player unpauses, or if they press the menu button
                if (Globals.CheckKey(Keys.Escape)) paused = false;
                else if (menuBtn.CheckButton())
                {
                    Globals.Fade.Start(Globals.MENU, 1);
                    paused = false;
                }
            }
            else if (Globals.HelpScreenOpen)
            {
                HelpScreen.Update();
            }
            else
            {
                switch (Globals.Gamestate)
                {
                    case Globals.LOGO:
                        {
                            // Once the logo has lasted for its entire duration, the fade to the menu begins
                            if (logoDuration.CheckCooldown())
                            {
                                Globals.Fade.Start(Globals.MENU, 1);
                            }
                            else
                            {
                                logoDuration.UpdateCooldown();
                            }
                            break;
                        }
                    case Globals.TRANSITION:
                        {
                            TransitionScreen.Update();
                            break;
                        }
                    case Globals.MENU:
                        {
                            Menu.Update();
                            break;
                        }
                    case Globals.GAMEPLAY:
                        {
                            // Checks if the player has paused
                            if (Globals.CheckKey(Keys.Escape))
                            {
                                paused = true;
                            }

                            // Updates the game that is currently running
                            switch (Globals.Minigame)
                            {
                                case Globals.LUCKY_DAY:
                                    {
                                        luckyDay.Update();
                                        break;
                                    }
                                case Globals.COLOR_GUESSER:
                                    {
                                        colorGuesser.Update();
                                        break;
                                    }
                                case Globals.RAINDROPS:
                                    {
                                        raindrops.Update();
                                        break;
                                    }
                                case Globals.JUGGLER:
                                    {
                                        juggler.Update();
                                        break;
                                    }
                                case Globals.TERMINAL_VELOCITY:
                                    {
                                        terminalVelocity.Update();
                                        break;
                                    }
                                case Globals.BACKWARDS:
                                    {
                                        backwards.Update();
                                        break;
                                    }
                                case Globals.TYPEWRITER:
                                    {
                                        typewriter.Update();
                                        break;
                                    }
                                case Globals.FLOOR_IS_LAVA:
                                    {
                                        floorIsLava.Update();
                                        break;
                                    }
                                case Globals.MILKSHAKER:
                                    {
                                        milkshaker.Update();
                                        break;
                                    }
                                case Globals.CIRCLE_CLICKER:
                                    {
                                        circleClicker.Update();
                                        break;
                                    }
                            }
                            break;
                        }
                    case Globals.LEADERBOARD:
                        {
                            Leaderboard.Update();
                            break;
                        }
                    case Globals.RESULTS:
                        {
                            Results.Update();
                            break;
                        }

                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            switch (Globals.Gamestate)
            {
                case Globals.LOGO:
                    {
                        // Draws the logo
                        spriteBatch.Draw(logoImg, new Rectangle(300, 360, 600, 80), Color.White);
                        break;
                    }
                case Globals.MENU:
                    {
                        Menu.Draw(spriteBatch, gameTime);
                        break;
                    }
                case Globals.TRANSITION:
                    {
                        TransitionScreen.Draw(spriteBatch);
                        break;
                    }
                case Globals.GAMEPLAY:
                    {
                        // Draws the assets of the appropriate game
                        switch (Globals.Minigame)
                        {
                            case Globals.LUCKY_DAY:
                                {
                                    luckyDay.Draw(spriteBatch, gameTime);
                                    break;
                                }
                            case Globals.COLOR_GUESSER:
                                {
                                    colorGuesser.Draw(spriteBatch);
                                    break;
                                }
                            case Globals.RAINDROPS:
                                {
                                    raindrops.Draw(spriteBatch, gameTime);
                                    break;
                                }
                            case Globals.JUGGLER:
                                {
                                    juggler.Draw(spriteBatch, gameTime);
                                    break;
                                }
                            case Globals.TERMINAL_VELOCITY:
                                {
                                    terminalVelocity.Draw(spriteBatch, gameTime);
                                    break;
                                }
                            case Globals.BACKWARDS:
                                {
                                    backwards.Draw(spriteBatch);
                                    break;
                                }
                            case Globals.TYPEWRITER:
                                {
                                    typewriter.Draw(spriteBatch);
                                    break;
                                }
                            case Globals.FLOOR_IS_LAVA:
                                {
                                    floorIsLava.Draw(spriteBatch, gameTime);
                                    break;
                                }
                            case Globals.MILKSHAKER:
                                {
                                    milkshaker.Draw(spriteBatch);
                                    break;
                                }
                            case Globals.CIRCLE_CLICKER:
                                {
                                    circleClicker.Draw(spriteBatch);
                                    break;
                                }
                        }

                        // Draws the hud elements
                        Hud.Draw(spriteBatch);
                        break;
                    }
                case Globals.RESULTS:
                    {
                        Results.Draw(spriteBatch);
                        break;
                    }
                case Globals.LEADERBOARD:
                    {
                        Leaderboard.Draw(spriteBatch);
                        break;
                    }
            }

            // Draws the help screen if it is open
            if (Globals.HelpScreenOpen && !Globals.Fade.Active)
            {
                HelpScreen.Draw(spriteBatch);
            }

            // Draws the pause screen if it is active
            if (paused)
            {
                spriteBatch.Draw(blankRecImg, Globals.GetRec(), Color.Black * pausedOpacity);
                spriteBatch.DrawString(bigFont, "PAUSED", Globals.CentreText(Globals.GetRec(), "PAUSED", bigFont), Color.White);

                spriteBatch.Draw(menuBtn.Img, menuBtn.Rec, Color.White);
            }

            // Draws the fade if it is active
            if (Globals.Fade.Active)
            {
                Globals.Fade.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
