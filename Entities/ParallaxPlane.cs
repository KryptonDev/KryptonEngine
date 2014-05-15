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

        protected float mSpeed;
		protected List<Sprite> mTiles = new List<Sprite>();

		#endregion
		
		#region Get & Set

		public List<Sprite> Tiles { get { return mTiles; } set { mTiles = value; } }
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

        #region Constructor

        public ParallaxPlane()
            : base()
        {
			Initialize();
        }

        public ParallaxPlane(float pSpeed)
          : base()
        {
			Initialize();
          mSpeed = pSpeed;
        }

        #endregion

        #region Methods

		public override void Initialize()
		{
			mDebugColor = Color.Tomato;
		}

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
