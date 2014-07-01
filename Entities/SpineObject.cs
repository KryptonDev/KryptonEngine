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
		protected Texture2D[] mTextures;

		new protected Color mDebugColor = Color.Yellow;

		#endregion

		#region Getter & Setter

		public string Name { get { return mName; } set { mName = value; } }
		[XmlIgnoreAttribute]
        public bool Flip { get { return mSkeleton.FlipX; } set { mSkeleton.FlipX = value; } }
		[XmlIgnoreAttribute]
        public bool FlipY { get { return mSkeleton.FlipY; } set { mSkeleton.FlipY = value; } }
		[XmlIgnoreAttribute]
        public Skeleton Skeleton { get { return mSkeleton; } }
		[XmlIgnoreAttribute]
        public AnimationState AnimationState { get { return mAnimationState; } }//set { mAnimationState = value; } }
		[XmlIgnoreAttribute]
		public bool AnimationComplete { get
		{
			if (AnimationState.GetCurrent(0) == null || AnimationState.GetCurrent(0).Time >= AnimationState.GetCurrent(0).EndTime)
				return true;
			return false;
		} }
		[XmlIgnoreAttribute]
		public Texture2D[] Textures { get { return mTextures; } set { mTextures = value; } }
		[XmlIgnoreAttribute]
		public Vector2 SkeletonPosition { set { Skeleton.x = value.X; Skeleton.y = value.Y; } get { return new Vector2(Skeleton.x, Skeleton.y); } }
		
		[XmlIgnoreAttribute]
		public DrawPackage DrawPackage { get { return new DrawPackage(Position, DrawZ, CollisionBox, mDebugColor, Skeleton, mTextures); } }

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

        #endregion

        #region Methods

        public override void LoadContent() 
        {
            mBounds = new SkeletonBounds();

            mSkeleton = SpineDataManager.Instance.NewSkeleton(mName, mScale); //Fixed Scale from here. Main instanciation.
            mSkeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
            mAnimationState = SpineDataManager.Instance.NewAnimationState(mSkeleton.Data);
            mSkeleton.X = mInitPosition.X;
            mSkeleton.Y = mInitPosition.Y;

			mTextures = new Texture2D[4];
			mTextures[0] = TextureManager.Instance.GetElementByString(mName);
			mTextures[1] = TextureManager.Instance.GetElementByString(mName + "Normal");
			mTextures[2] = TextureManager.Instance.GetElementByString(mName + "AO");
			mTextures[3] = TextureManager.Instance.GetElementByString(mName + "Depth");

			mSkeleton.FlipY = true;
			mSkeleton.SetSkin("front");
        }

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

		public override void Draw(Rendering.TwoDRenderer renderer)
		{
			renderer.Draw(mSkeleton, mTextures, mNormalZ);
		}

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

		public virtual void ApplySettings()
		{
			SkeletonPosition = mPosition;
			mAnimationState.Apply(mSkeleton);
			mSkeleton.UpdateWorldTransform();
		}

        #endregion
    }
}
