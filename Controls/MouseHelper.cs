using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Controls
{
    public class MouseHelper
    {
        #region Singleton

        private static MouseHelper mInstance;
        public static MouseHelper Instance
        {
            get
            {
                if (mInstance == null) mInstance = new MouseHelper();
                return mInstance;
            }
        }
        #endregion

        #region Properties

        private static MouseState msLast;
        private static MouseState msCurrent;

        private static Vector2 mousePosition;
        #endregion

        #region Getter & Setter

        public static Vector2 Position { get { return mousePosition * new Vector2(EngineSettings.AspectRatioX, EngineSettings.AspectRatioY); } }
        public static Point PositionPoint { get { return new Point((int)(mousePosition.X * EngineSettings.AspectRatioX), (int)(mousePosition.Y * EngineSettings.AspectRatioY)); } }
		public static Vector2 RawPosition { get { return mousePosition; } }
        public bool IsClickedLeft
        {
            get
            {
                return (msCurrent.LeftButton == ButtonState.Released && msLast.LeftButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsClickedRight
        {
            get
            {
                return (msCurrent.RightButton == ButtonState.Released && msLast.RightButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsClickedMiddle
        {
            get
            {
                return (msCurrent.MiddleButton == ButtonState.Released && msLast.MiddleButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsPressedLeft
        {
            get
            {
                return (msCurrent.LeftButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsPressedRight
        {
            get
            {
                return (msCurrent.RightButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsPressedMiddle
        {
            get
            {
                return (msCurrent.MiddleButton == ButtonState.Pressed) ? true : false;
            }
        }

        public bool IsReleasedLeft
        {
          get
          {
            return (msCurrent.LeftButton == ButtonState.Released) ? true : false;
          }
        }

        public bool IsReleasedRight
        {
          get
          {
            return (msCurrent.RightButton == ButtonState.Released) ? true : false;
          }
        }

        public bool IsReleasedMiddle
        {
          get
          {
            return (msCurrent.MiddleButton == ButtonState.Released) ? true : false;
          }
        }


        public bool IsWheelUp
        {
            get
            {
                return (msCurrent.ScrollWheelValue > msLast.ScrollWheelValue) ? true : false;
            }
        }

        public bool IsWheelDown
        {
            get
            {
                return (msCurrent.ScrollWheelValue < msLast.ScrollWheelValue) ? true : false;
            }
        }
        
        #endregion

        #region Constructor 
        MouseHelper() { }
        #endregion

        #region Methods
        public static void Update()
        {
            msLast = msCurrent;
            msCurrent = Mouse.GetState();

            mousePosition.X = Mouse.GetState().X;
            mousePosition.Y = Mouse.GetState().Y;
        }

        public static void ResetClick()
        {
            msLast = msCurrent;
            msCurrent = Mouse.GetState();
        }
        #endregion
    }
}
