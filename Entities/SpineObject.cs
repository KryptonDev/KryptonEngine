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

		new protected Color mDebugColor = Color.Yellow;

        #region Getter & Setter

        public string Name { get { return mName; } }
        new public Vector2 Position { get { return new Vector2(mSkeleton.X, mSkeleton.Y); } set { mSkeleton.X = value.X; mSkeleton.Y = value.Y; } }
		new public int PositionX { set { mSkeleton.X = value; } get { return (int)mSkeleton.X; } }
		new public int PositionY { set { mSkeleton.Y = value; } get { return (int)mSkeleton.Y; } }
        public bool Flip { get { return mSkeleton.FlipX; } set { mSkeleton.FlipX = value; } }
        public bool FlipY { get { return mSkeleton.FlipY; } set { mSkeleton.FlipY = value; } }
        public Skeleton Skeleton { get { return mSkeleton; } }
        public AnimationState AnimationState { get { return mAnimationState; } }//set { mAnimationState = value; } }

        #endregion

        #endregion

        #region Constructor

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

        public void Load()
        {
            mBounds = new SkeletonBounds();

            mSkeleton = SpineDataManager.Instance.NewSkeleton(mName, mScale); //Fixed Scale from here. Main instanciation.
            mSkeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
            mAnimationState = SpineDataManager.Instance.NewAnimationState(mSkeleton.Data);
            mSkeleton.X = mInitPosition.X;
            mSkeleton.Y = mInitPosition.Y;
        }

        #region Update

        public override void Update()
        {
            UpdateAnimation();
        }

        protected void UpdateAnimation()
        {
            //Player -> Drawposition
            //skeleton.X = position.X - camera.viewport.X;
            //skeleton.Y = position.Y - camera.viewport.Y;
            //skeleton.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            mAnimationState.Update(EngineSettings.Time.ElapsedGameTime.Milliseconds / 1000f);
            mAnimationState.Apply(mSkeleton);
            mSkeleton.UpdateWorldTransform();
        }

        #endregion

		public void Draw(SpriteBatch pSpriteBatch, Vector2 pCameraPosition)
        {
			Draw(pSpriteBatch, pCameraPosition, Vector2.Zero);
        }

        public void Draw(SpriteBatch pSpriteBatch, Vector2 pCameraPosition, Vector2 pOffset)
        {
            Vector2 TmpPosition = Position;
            Position -= pCameraPosition - pOffset;
            EngineSettings.SpineRenderer.Begin();
			EngineSettings.SpineRenderer.Draw(mSkeleton);
			EngineSettings.SpineRenderer.End();
            Position = TmpPosition;
            if (EngineSettings.IsDebug)
                pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX + (int)pOffset.X, PositionY + (int)pOffset.Y, 10, 10), mDebugColor);
        }

		//private bool BoundingBoxCollision(Rectangle cbox) //Checken ob Rectangle mit bb-Attachement (z.B. Keule) kollidiert
		//{
		//	mBounds.Update(mSkeleton, true);
		//	bool collision = false;
		//	if (mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height)
		//		|| mBounds.AabbIntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height)
		//		|| mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y)
		//		|| mBounds.AabbIntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height)
		//		)
		//	{
		//		if (mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height) != null
		//			||
		//			mBounds.IntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
		//			||
		//			mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y) != null
		//			||
		//			mBounds.IntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
		//			)
		//		{
		//			collision = true;
		//		}
		//	}
		//	return collision;
		//}

		public void ChangeDrawScaling(float pScale)
		{
			Bone TmpRootBone = mSkeleton.FindBone("root");
			TmpRootBone.scaleX = pScale;
			TmpRootBone.scaleY = pScale;
		}

        #endregion
    }
}
