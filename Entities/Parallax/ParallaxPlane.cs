/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class ParallaxPlane<T> : GameObject
    {
        #region Properties

        protected Vector2 mSize;
        protected float mSpeed;

        #region Get & Set

        public Vector2 Size { get { return mSize; } }
        public int Width { get { return (int)mSize.X; } }
        public int Height { get { return (int)mSize.Y; } }

        protected List<T> mTiles = new List<T>();

        #endregion

        #endregion

        #region Constructor

        public ParallaxPlane()
            : base()
        {  
        }

        public ParallaxPlane(Vector2 pPosition)
            : base(pPosition)
        {
        }

        public ParallaxPlane(float pSpeed)
          : base()
        {
          mSpeed = pSpeed;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updated die Ebenenverschiebung anhand der übergebenen Kamera, relativ zu Viewport und Viewarea.
        /// </summary>
        /// <param name="pCamera">Zu verwendende Kamera.</param>
        public virtual void Update(CameraGame pCamera)
        {
          if (mSpeed != null && mSpeed != 0)
          {
            Position = pCamera.Position * mSpeed;
            return;
          }
          PositionX = pCamera.PositionX - (int)((float)(Width - pCamera.Width) * ((float)(pCamera.PositionX - pCamera.VirtualMoveRestriction.X) / (float)(pCamera.VirtualMoveRestriction.Width - pCamera.Width)));
          PositionY = pCamera.PositionY - (int)((float)(Height - pCamera.Height) * ((float)(pCamera.PositionY - pCamera.VirtualMoveRestriction.Y) / (float)(pCamera.VirtualMoveRestriction.Height - pCamera.Height)));
        }

        #endregion


    }
}
