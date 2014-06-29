/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Entities;
using KryptonEngine.Manager;
using Spine;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace KryptonEngine.Entities
{
    public class SpineObject : GameObject
    {
        #region Properties

        private Skeleton mSkeleton;
        private AnimationState mAnimationState;
        private SkeletonBounds mBounds;

        private string mName;
        private Vector2 mInitPosition;
        private float mScale;
		[XmlIgnoreAttribute]
		protected Texture2D[] mTextures;

		new protected Color mDebugColor = Color.Yellow;

        #region Getter & Setter

		public string Name { get { return mName; } set { mName = value; } }
		new public Vector2 Position { get { return new Vector2(mSkeleton.X, mSkeleton.Y); } 
			set 
			{
				//mSkeleton.X = value.X; 
				//mSkeleton.Y = value.Y; 
			} 
		}
		//new public int PositionX { set { mSkeleton.X = value; } get { return (int)mSkeleton.X; } }
		//new public int PositionY { set { mSkeleton.Y = value; } get { return (int)mSkeleton.Y; } }
        public bool Flip { get { return mSkeleton.FlipX; } set { mSkeleton.FlipX = value; } }
        public bool FlipY { get { return mSkeleton.FlipY; } set { mSkeleton.FlipY = value; } }
        public Skeleton Skeleton { get { return mSkeleton; } }
        public AnimationState AnimationState { get { return mAnimationState; } }//set { mAnimationState = value; } }
		public bool AnimationComplete { get
		{
			if (AnimationState.GetCurrent(0) == null || AnimationState.GetCurrent(0).Time >= AnimationState.GetCurrent(0).EndTime)
				return true;
			return false;
		} }
		public Texture2D[] Textures { get { return mTextures; } set { mTextures = value; } }

        #endregion

        #endregion

        #region Constructor

		public SpineObject() : base()
		{

		}

        public SpineObject(string pName)
        {
            mName = pName;
            mInitPosition = Vector2.Zero;
            mScale = 1.0f;
        }

        //public SpineObject(string pName)
        //{
        //    mName = pName;
        //    mInitPosition = new Vector2(0, 0);
        //    mScale = 1.0f;
        //}

        //public SpineObject(string pName, float pScale)
        //{
        //    mName = pName;
        //    mInitPosition = new Vector2(0, 0);
        //    mScale = pScale;
        //}

        //public SpineObject(string pName, Vector2 pPosition)
        //{
        //    mName = pName;
        //    mInitPosition = pPosition;
        //    mScale = 1.0f;
        //}

        //public SpineObject(string pName, Vector2 pPosition, float pScale)
        //{
        //    mName = pName;
        //    mInitPosition = pPosition;
        //    mScale = pScale;
        //}

        #endregion

        #region Methods

        #region Pool

        public void CleanUp()
        {
            Position = Vector2.Zero;
            Flip = false;
            AnimationState.ClearTracks();
            Skeleton.SetToSetupPose();
            Skeleton.SetSkin("default");
        }

        #endregion

        public override void LoadContent() 
        {
            mBounds = new SkeletonBounds();

            mSkeleton = SpineDataManager.Instance.NewSkeleton(mName, mScale); //Fixed Scale from here. Main instanciation.
            mSkeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
            mAnimationState = SpineDataManager.Instance.NewAnimationState(mSkeleton.Data);
            mSkeleton.X = mInitPosition.X;
            mSkeleton.Y = mInitPosition.Y;

			mTextures = new Texture2D[4];
			mTextures[0] = TextureManager.Instance.GetElementByString(mName + "-1");
			mTextures[1] = TextureManager.Instance.GetElementByString(mName + "Normal");
			mTextures[2] = TextureManager.Instance.GetElementByString(mName + "AO");
			mTextures[3] = TextureManager.Instance.GetElementByString(mName + "Depth");

			mAnimationState.Apply(mSkeleton);
			mSkeleton.UpdateWorldTransform();
        }

        #region Update

        public override void Update()
        {
            UpdateAnimation();
        }

        protected void UpdateAnimation()
        {
            mAnimationState.Update(EngineSettings.Time.ElapsedGameTime.Milliseconds / 1000f);
			Skeleton.Update(EngineSettings.Time.ElapsedGameTime.Milliseconds / 1000f);
            mSkeleton.UpdateWorldTransform();
            mAnimationState.Apply(mSkeleton);
        }

        #endregion

		public override void Draw(Rendering.TwoDRenderer renderer)
		{
			renderer.Draw(mSkeleton, mTextures, DrawZ);
		}

		//public override void Draw(SpriteBatch spriteBatch)
		//{
		//	Draw(spriteBatch, Vector2.Zero, Vector2.Zero);
		//}

		//public void Draw(SpriteBatch pSpriteBatch, Vector2 pCameraPosition)
		//{
		//	Draw(pSpriteBatch, pCameraPosition, Vector2.Zero);
		//}

		//public void Draw(SpriteBatch pSpriteBatch, Vector2 pCameraPosition, Vector2 pOffset)
		//{
		//	Vector2 TmpPosition = Position;
		//	Position -= pCameraPosition - pOffset;
		//	EngineSettings.SpineRenderer.Begin();
		//	EngineSettings.SpineRenderer.Draw(mSkeleton);
		//	EngineSettings.SpineRenderer.End();
		//	Position = TmpPosition;
		//	if (EngineSettings.IsDebug)
		//		pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX + (int)pOffset.X, PositionY + (int)pOffset.Y, 10, 10), mDebugColor);
		//}

		#region Animation

		/// <summary>
		/// Applyed eine Animation auf dieses SpineObject.
		/// </summary>
		/// <param name="pAnimation">Animation</param>
		/// <param name="pLoop">Soll die Animation gelooped werden?</param>
		/// <param name="pForce">Soll die Animation applyed werden auch wenn schon diese Animation läuft?</param>
		/// <param name="pCut">Soll nicht gefaded werden sondern alles gestoppt und direkt die Animation abgespielt werden?</param>
		public void SetAnimation(string pAnimation = "idle", bool pLoop = true, bool pForce = false, bool pCut = false)
		{
			if (mSkeleton.Data.FindAnimation(pAnimation) == null)
				throw new Exception("Animation \"" + pAnimation + "\" ist im Skeleton \"" + mSkeleton.Data.Name + "\" nicht vorhanden.");
			if (pCut)
				AnimationState.ClearTracks();
			if (AnimationState.GetCurrent(0) == null || AnimationState.GetCurrent(0).ToString() != pAnimation || pForce)
				AnimationState.SetAnimation(0, pAnimation, pLoop);
		}

		#endregion

        #endregion
    }
}
