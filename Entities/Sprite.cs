/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Manager;

namespace KryptonEngine.Entities
{
    public class Sprite : GameObject
    {
        #region Properties

        protected String mTextureName;
        protected Texture2D mTexture;
        protected Color mTint = Color.White;
        protected int mWidth;
        protected int mHeight;

        protected Vector2 mOrigin;
        protected int mRotation = 0;
        protected SpriteEffects mEffekt = SpriteEffects.None;

        #region Getter & Setter

        public String TextureName;
        public Color Tint { set { mTint = value; } }
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }

        public Vector2 Origin { get { return mOrigin; } }
        public int Rotation { get { return mRotation; } set { mRotation = value; } }
        public SpriteEffects Effect { get { return mEffekt; } set { mEffekt = value; } }

        public Texture2D Texture { get { return mTexture; } }

        #endregion

        #endregion

        #region Constructor

        public Sprite() { }

        public Sprite(Vector2 pPosition, String pTextureName, String pPathName)
            : base(pPosition)
        {
            TextureName = pTextureName;
            mTexture = TextureManager.Instance.Add(pTextureName, @"gfx\" + pPathName);
            
            mWidth = mTexture.Width;
            mHeight = mTexture.Height;
            mOrigin = new Vector2(mWidth / 2, mHeight / 2);

            mCollisionBox = new Rectangle((int)pPosition.X, (int)pPosition.Y, mWidth, mHeight);
        }

        public Sprite(Vector2 pPosition, String pTextureName)
            : base(pPosition)
        {
            TextureName = pTextureName;

            mTexture = TextureManager.Instance.GetElementByString(TextureName);
            mWidth = mTexture.Width;
            mHeight = mTexture.Height;
            mOrigin = new Vector2(mWidth / 2, mHeight / 2);

            mCollisionBox = new Rectangle((int)pPosition.X, (int)pPosition.Y, mWidth, mHeight);
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, new Rectangle(PositionX + (int)mOrigin.X, PositionY + (int)mOrigin.Y, mWidth, mHeight), new Rectangle(0, 0, mWidth, mHeight), mTint, MathHelper.ToRadians(mRotation), mOrigin, mEffekt, 0.0f);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(mTexture, new Rectangle(PositionX, PositionY, mWidth, mHeight), mDebugColor);
        }
        #endregion
    }
}
