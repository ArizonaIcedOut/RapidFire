// AUthor: Eric Pu
// File Name: TypewriterKey.cs
// Project Name: RapidFire
// Creation Date: June 6th, 2019
// Modified Date: June 6th, 2019
// Description: Class for the keys in the Typewriter minigame

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
    public class TypewriterKey
    {
        public char Letter;

        public Button KeyButton;

        public Rectangle Rec;

        public Vector2 Size;

        public int Row;

        public int PlaceFromLeft;
        
        public int RowSizeX;

        public int RowStartX;

        public int SpaceBetweenKeys;

        public Dictionary<int, int> RowY;

        public Dictionary<int, int> RowLength;

        public TypewriterKey(char letter, int row, int placeFromLeft)
        {
            RowY = new Dictionary<int, int>
            {
                {1, 300 },
                {2, 450 },
                {3, 600 }
            };

            RowLength = new Dictionary<int, int>
            {
                {1, 7 },
                {2, 10 },
                {3, 9 }
            };

            Row = row;

            SpaceBetweenKeys = 25;

            Size = new Vector2(75, 75);

            RowSizeX = (int)Size.X * RowLength[Row] + ((RowLength[Row] - 2) * SpaceBetweenKeys);

            RowStartX = Globals.ScreenWidth / 2 - RowSizeX / 2 - SpaceBetweenKeys / 2;

            Letter = letter;

            PlaceFromLeft = placeFromLeft;

            Rec = new Rectangle(RowStartX + (int)((Size.X + SpaceBetweenKeys) * PlaceFromLeft), RowY[Row], (int)Size.X, (int)Size.Y);

            KeyButton = new Button(Game1.blankRecImg, Rec);
        }
    }
}
