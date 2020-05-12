// AUthor: Eric Pu
// File Name: Cooldown.cs
// Project Name: RapidFire
// Creation Date: May 22nd, 2019
// Modified Date: May 22nd, 2019
// Description: Class for cooldowns and timers

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

namespace MONO_TEST
{
    public class Cooldown
    {
        public int Duration;

        public int Timer;

        public Cooldown(int duration)
        {
            Duration = duration;
            Timer = Duration;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Decrements the cooldown timer by 1
        /// </summary>
        public void UpdateCooldown()
        {
            Timer--;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: bool indicating if the cooldown has been met yet
        /// Description: Checks if the cooldown has been met yet. If it has, the cooldown is reset.
        /// </summary>
        /// <returns></returns>
        public bool CheckCooldown()
        {
            if (Timer <= 0)
            {
                ResetCooldown();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Pre: n/a
        /// Post: n/a
        /// Description: Resets the cooldown
        /// </summary>
        public void ResetCooldown()
        {
            Timer = Duration;
        }
    }
}
