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

        private SkeletonRenderer mSkeletonRenderer;
        private Skeleton mSkeleton;
        private AnimationState mAnimationState;
        private SkeletonBounds mBounds;

        private string mName;
        private Vector2 mInitPosition;
        private Vector2 mMovePosition;
        private float mScale;

        #region Getter & Setter

        public string Name { get { return mName; } }
        public Vector2 Position { get { return new Vector2(mSkeleton.X, mSkeleton.Y); } set { mSkeleton.X = value.X; mSkeleton.Y = value.Y; } }
        public int PositionX { set { mSkeleton.X = value; } get { return (int)mSkeleton.X; } }
        public int PositionY { set { mSkeleton.Y = value; } get { return (int)mSkeleton.Y; } }
        public float Scale { get { return mScale; } }
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
            mSkeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
            mBounds = new SkeletonBounds();

            // mSkeleton = SpineManager.Instance.NewSkeleton(mName, mScale); //Fixed Scale from here. Main instanciation.
            mSkeleton.SetSlotsToSetupPose(); // Without this the skin attachments won't be attached. See SetSkin.
            // mAnimationState = SpineManager.Instance.NewAnimationState(mSkeleton.Data);
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
          // Gehört UpdateWorldTransform zu SetPosition? Da dort die Position verändert wird?  
          //mSkeleton.UpdateWorldTransform();
        }

      // Muss bei jedem Update gesetzt werden damit die Position zum zeichnen berechnet wird.
      public void SetPosition(Matrix pTranslation, Vector2 pPosition)
        {
          mMovePosition += pPosition;
          mSkeleton.X = mMovePosition.X + mInitPosition.X + pTranslation.Translation.X;
          mSkeleton.Y = mMovePosition.Y + mInitPosition.Y + pTranslation.Translation.Y;
          mSkeleton.UpdateWorldTransform();
        }

        #endregion

      public override void Draw(SpriteBatch spriteBatch)
      {
        mSkeletonRenderer.Begin();
        mSkeletonRenderer.Draw(mSkeleton);
        mSkeletonRenderer.End();

        if (EngineSettings.IsDebug)
          spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX, PositionY , 10, 10), mDebugColor);
      }
        //public void Draw(SpriteBatch pSpriteBatch, Camera pCamera)
        //{
        //    Draw(pSpriteBatch, pCamera, Vector2.Zero);
        //}

        //public void Draw(SpriteBatch pSpriteBatch, Camera pCamera, Vector2 pOffset)
        //{
        //    Vector2 TmpPosition = Position;
        //    Position -= pCamera.Position - pOffset;
        //    mSkeletonRenderer.Begin();
        //    mSkeletonRenderer.Draw(mSkeleton);
        //    mSkeletonRenderer.End();
        //    Position = TmpPosition;
        //    if (EngineSettings.IsDebug)
        //        pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX + (int)pOffset.X, PositionY + (int)pOffset.Y, 10, 10), mDebugColor);
        //}

        private bool BoundingBoxCollision(Rectangle cbox) //Checken ob Rectangle mit bb-Attachement (z.B. Keule) kollidiert
        {
            mBounds.Update(mSkeleton, true);
            bool collision = false;
            if (mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height)
                || mBounds.AabbIntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                || mBounds.AabbIntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y)
                || mBounds.AabbIntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height)
                )
            {
                if (mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X, cbox.Y + cbox.Height) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X + cbox.Width, cbox.Y, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X, cbox.Y, cbox.X + cbox.Width, cbox.Y) != null
                    ||
                    mBounds.IntersectsSegment(cbox.X, cbox.Y + cbox.Height, cbox.X + cbox.Width, cbox.Y + cbox.Height) != null
                    )
                {
                    collision = true;
                }
            }
            return collision;
        }

        #endregion
    }
}
