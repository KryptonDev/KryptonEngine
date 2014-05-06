/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KryptonEngine
{
    public static class PlayerSettings
    {
        #region Properties

        public static Vector2 ActualVelocity = Vector2.Zero;

        public static Vector2 Velocity = new Vector2(0, 1);
        
        public static float Acceleration = 1.0f;
        public static float Friction = 0.0f;

        public static int MaxSpeed = 25;
        public static int MinSpeed = 0;

        #endregion
    }
}
