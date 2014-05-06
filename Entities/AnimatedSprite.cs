/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Entities
{
    public class AnimatedSprite : TiledSprite
    {
        #region Properties

        protected List<int> mFrames = new List<int>();
        protected bool mRepeatAnimation = true;
        protected bool mAnimDone = false;
        protected int mAnimSpeed;
        protected int mAnimElapsedTime = 0;
        protected int mFrameInList = 0;

        #region Getter & Setter

        #endregion

        #endregion

        #region Constructor

        public AnimatedSprite() { }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, String pPath, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed)
            : base(pPosition, pTextureName, pPath, pRectangleWidth, pRectangleHeight)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, String pPath, List<Rectangle> pSourceRectangleList, List<int> pFrames, int pAnimSpeed)
            : base(pPosition, pTextureName, pPath, pSourceRectangleList)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, String pPath, int pRectangleWidth, int pRectangleHeight, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pPath, pRectangleWidth, pRectangleHeight)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
            mRepeatAnimation = pIsRepeat;
        }

        public AnimatedSprite(Vector2 pPosition, String pTextureName, String pPath, List<Rectangle> pSourceRectangleList, List<int> pFrames, int pAnimSpeed, bool pIsRepeat)
            : base(pPosition, pTextureName, pPath, pSourceRectangleList)
        {
            mFrames = pFrames;
            mAnimSpeed = pAnimSpeed;
            mRepeatAnimation = pIsRepeat;
        }

        #endregion

        #region Methods

        public override void Update()
        {
            Animate();
        }


        protected void Animate()
        {
            mAnimElapsedTime += (EngineSettings.Time.ElapsedGameTime.Milliseconds);
            if (mAnimElapsedTime >= mAnimSpeed)
            {
                mFrameInList++;

                if (mFrameInList == mFrames.Count)
                {
                    mFrameInList = 0;
                    if (!mRepeatAnimation)
                    {
                        CurrentTile = mFrames[mFrames.Count - 1];
                        mAnimDone = true;
                        return;
                    }
                }

                CurrentTile = mFrames[mFrameInList];
                mAnimElapsedTime = 0;
            }
        }
        #endregion
    }
}
