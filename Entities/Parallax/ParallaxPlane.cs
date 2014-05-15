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
    public class ParallaxPlane : GameObject
    {
        #region Properties

        protected Vector2 mSize;
        protected float mSpeed;
		protected List<Sprite> mTiles = new List<Sprite>();

        #region Get & Set

        public Vector2 Size { get { return mSize; } }
        public int Width { get { return (int)mSize.X; } }
        public int Height { get { return (int)mSize.Y; } }

		public List<DrawPackage> DrawPackages { get
		{
			List<DrawPackage> TmpPackages = new List<DrawPackage>();
			foreach (Sprite obj in mTiles)
			{
				TmpPackages.Add(new DrawPackage(obj.Position + Position, obj.DrawZ, obj.CollisionBox, mDebugColor));
			}
			return TmpPackages;
		} }

        #endregion

        #endregion

        #region Constructor

        public ParallaxPlane()
            : base()
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
        public virtual void Update(Camera pCamera)
        {
			Position = pCamera.Position * mSpeed - pCamera.Position;
        }

        #endregion


    }
}
