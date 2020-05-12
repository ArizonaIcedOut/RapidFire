// AUthor: Eric Pu
// File Name: MinigameSelection.cs
// Project Name: RapidFire
// Creation Date: June 11th, 2019
// Modified Date: June 11gh, 2019
// Description: Class for storing name and score of each player on the ranking, needed because 2D arrays cannot be used with different types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONO_TEST
{
    public class PlayerRanking
    {
        // Properties of the player
        public string Name;

        public int Score;

        public PlayerRanking(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
