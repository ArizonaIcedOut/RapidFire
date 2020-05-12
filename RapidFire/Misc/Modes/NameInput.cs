// AUthor: Eric Pu
// File Name: NameInput.cs
// Project Name: RapidFire
// Creation Date: June 11th, 2019
// Modified Date: June 11th, 2019
// Description: Class for player name and input

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

namespace MONO_TEST
{
    public class NameInput
    {
        // Properties of the name
        public string Name;

        public int MaxLength;

        public int MinLength;

        public NameInput()
        {
            Name = "";

            MaxLength = 6;

            MinLength = 3;
        }

        public void Update()
        {
            // Backspace for deleting last letter in name
            if (Globals.CheckKey(Keys.Back) && Name.Length > 0)
            {
                Name = Name.Substring(0, Name.Length - 1);
            }

            // Logic for typing letters for name
            if (Name.Length < MaxLength)
            {
                // Iterates through each of the keys and checks if it has been pressed
                for (int i = (int)Keys.A; i < (int)Keys.Z; i++)
                {
                    if (Globals.CheckKey((Keys)i))
                    {
                        Name += (char)i;
                    }
                }
            }
        }

        /// <summary>
        /// Pre: n/a
        /// Post: Returns an int representing the length of the name
        /// Description: Gets the length of the player's name
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return Name.Length;
        }
    }
}
