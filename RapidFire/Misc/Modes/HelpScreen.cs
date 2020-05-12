// AUthor: Eric Pu
// File Name: HelpScreen.cs
// Project Name: RapidFire
// Creation Date: June 12th, 2019
// Modified Date: June 12th, 2019
// Description: Class for the help screens that appear before each game

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
    class HelpScreen
    {
        public static Button StartBtn = new Button(Game1.startBtnImg, new Rectangle(400, 550, 400, 200));

        public static Button DisableHelpBtn = new Button(Game1.blankRecImg, new Rectangle(50, 720, 50, 50));

        // The help messages for each of the minigames
        public static List<List<string>> HelpMessages = new List<List<string>>
        {
            // Color Guesser
            new List<string>
            {
                "AN RGB VALUE WILL APPEAR AT THE TOP",
                "THERE ARE THREE CIRCLES OF DIFFERENT COLORS",
                "CLICK ON THE CORRECT COLOR TO WIN",
                "GUESS THE WRONG COLOR AND YOU LOSE"
            },

            // Lucky Day
            new List<string>
            {
                "USE LEFT/RIGHT OR A/D TO MOVE",
                "COINS WILL APPEAR FROM BOTH SIDES",
                "PICK UP ENOUGH COINS TO WIN",
                "RUN OUT OF TIME AND YOU LOSE"
            },

            // Raindrops
            new List<string>
            {
                "USE LEFT/RIGHT OR A/D TO MOVE",
                "RAINDROPS WILL APPEAR FROM THE TOP",
                "AVOID THE RAINDROPS AND SURVIVE TO WIN",
                "GET HIT BY A RAINDROP AND YOU LOSE"
            },

            // Juggler
            new List<string>
            {
                "USE LEFT/RIGHT OR A/D TO MOVE",
                "A BALL WILL APPEAR AT THE TOP OF THE SCREEN",
                "IT WILL FALL DOWN TO THE GROUND",
                "IF IT TOUCHES YOU, IT WILL BOUNCE BACK UP",
                "KEEP THE BALL IN THE AIR TO WIN",
                "IF THE BALL TOUCHES THE GROUND, YOU LOSE"
            },

            // Terminal Velocity
            new List<string>
            {
                "USE LEFT/RIGHT OR A/D TO MOVE",
                "YOU WILL APPEAR IN THE MIDDLE OF A PIT",
                "SPIKES WILL APPEAR ON EITHER SIDE",
                "TOUCH THE SPIKES AND YOU LOSE",
                "SURVIVE AND YOU WIN"
            },

            // Backwards
            new List<string>
            {
                "A WORD WILL APPEAR AT THE TOP",
                "YOU MUST TYPE THIS WORD BACKWARDS",
                "TYPE THE WRONG LETTER, AND YOU LOSE",
                "COMPLETE THE WORD, AND YOU WIN"
            },

            // Typewriter
            new List<string>
            {
                "CLICK ON THE KEYS TO TYPE",
                "A WORD WILL APPEAR AT THE TOP OF THE SCREEN",
                "YOU MUST TYPE THE WORD OUT WITH THE ON-SCREEN KEYBOARD",
                "HOWEVER, THE KEYBOARD IS IN THE DVORAK LAYOUT",
                "PRESS THE WRONG KEY, AND YOU LOSE",
                "COMPLETE THE WORD, AND YOU WIN"
            },

            // Circle Clicker
            new List<string>
            {
                "COLORED CIRCLES WILL APPEAR ON THE SCREEN",
                "CLICK ON THE CIRCLES AND THEY WILL DISAPPEAR",
                "THE CIRCLES WILL GROW IF NOT CLICKED ON",
                "IF ANY CIRCLE GROWS TOO LARGE, YOU LOSE",
                "SURVIVE AND YOU WIN"
            },

            // Milkshaker
            new List<string>
            {
                "A MILKSHAKE WILL APPEAR ON THE SCREEN",
                "CLICK ON THE MILKSHAKE AND HOLD TO GRAB IT",
                "SHAKE IT UP AND DOWN TO FILL THE BAR",
                "COMPLETE THE BAR TO WIN",
                "RUN OUT OF TIME AND YOU LOSE"
            },

            // Floor is Lava
            new List<string>
            {
                "BOSS STAGE",
                "USE LEFT/RIGHT OR A/D TO MOVE",
                "USE SPACE OR UP TO JUMP",
                "IF YOU GO OFF SCREEN, YOU WILL APPEAR AT THE OTHER SIDE",
                "AVOID THE LAVA AT THE BOTTOM OF THE SCREEN",
                "JUMP ON PLATFORMS TO SURVIVE"
            }
        };

        public static void Update()
        {
            // If the player presses the start button, the minigame begins. If the disable help button is pressed, help screens are enabled/disabled
            if (StartBtn.CheckButton())
            {
                Globals.HelpScreenOpen = false;
            }
            else if (DisableHelpBtn.CheckButton())
            {
                if (Globals.HelpScreensEnabled) Globals.HelpScreensEnabled = false;
                else Globals.HelpScreensEnabled = true;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Makes the screen darker
            spriteBatch.Draw(Game1.blankRecImg, Globals.GetRec(), Color.Black * .9f);

            // Draws each of the help messages
            for (int i = 0; i < HelpMessages[Globals.Minigame].Count; i++)
            {
                spriteBatch.DrawString(Game1.medFont, HelpMessages[Globals.Minigame][i], 
                    new Vector2(Globals.CentreText(Globals.GetRec(), HelpMessages[Globals.Minigame][i], Game1.medFont).X, 100 + (i * 60)), Color.White);
            }

            // Start button
            spriteBatch.Draw(StartBtn.Img, StartBtn.Rec, Color.Blue);
            spriteBatch.DrawString(Game1.bigFont, "START", Globals.CentreText(StartBtn.Rec, "START", Game1.bigFont), Color.White);

            // Draws the disable help button, which depends on if help screens are currently enabled or disabled
            if (Globals.HelpScreensEnabled)
            {
                spriteBatch.Draw(Game1.uncheckedBtnImg, DisableHelpBtn.Rec, Color.White);
            }
            else
            {
                spriteBatch.Draw(Game1.checkedBtnImg, DisableHelpBtn.Rec, Color.White);
            }

            spriteBatch.DrawString(Game1.medFont, "DISABLE HELP SCREENS", new Vector2(DisableHelpBtn.Rec.X, DisableHelpBtn.Rec.Y + 60), Color.White);
        }
    }
}
