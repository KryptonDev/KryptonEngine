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
using System.Xml.Serialization;

namespace KryptonEngine.Entities
{
    public class Sprite : GameObject
    {
        #region Properties

        protected String mTextureName;
        protected Texture2D mTexture;
		protected Texture2D mNormalTexture;
		protected Texture2D mDepthTexture;
        protected Color mTint = Color.White;
        protected int mWidth;
        protected int mHeight;

        protected Vector2 mOrigin;
        protected int mRotation = 0;
        protected SpriteEffects mEffekt = SpriteEffects.None;

        #region Getter & Setter

        public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
		[XmlIgnoreAttribute]
        public Color Tint { set { mTint = value; } }
		public int Width { get { return mWidth; } set { mWidth = value; } }
		public int Height { get { return mHeight; } set { mHeight = value; } }

        public Vector2 Origin { get { return mOrigin; } }
        public int Rotation { get { return mRotation; } set { mRotation = value; } }
		[XmlIgnoreAttribute]
        public SpriteEffects Effect { get { return mEffekt; } set { mEffekt = value; } }

		public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
		[XmlIgnoreAttribute]
		public Texture2D NormalTexture { get { return mNormalTexture; } set { mNormalTexture = value; } }
		[XmlIgnoreAttribute]
		public Texture2D DepthTexture { get { return mDepthTexture; } set { mDepthTexture = value; } }

		public DrawPackage DrawPackage { get { return new DrawPackage(Position, mDrawZ, CollisionBox, mDebugColor, mTexture); } }

        #endregion

        #endregion

        #region Constructor

        public Sprite() { }

        public Sprite(Vector2 pPosition, String pTextureName, String pPathName)
            : base(pPosition)
        {
            TextureName = pTextureName;
			mTexture = TextureManager.Instance.GetElementByString(TextureName);
            
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
			mNormalTexture = TextureManager.Instance.GetElementByString(TextureName + "Normal");
			mDepthTexture = TextureManager.Instance.GetElementByString(TextureName + "Depth");
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

		public override void DrawNormal(SpriteBatch spriteBatch)
		{
			if (mNormalTexture == null) return;

			spriteBatch.Draw(mNormalTexture, new Rectangle(PositionX + (int)mOrigin.X, PositionY + (int)mOrigin.Y, mWidth, mHeight), new Rectangle(0, 0, mWidth, mHeight), mTint, MathHelper.ToRadians(mRotation), mOrigin, mEffekt, 0.0f);
		}

		public override void DrawDepth(SpriteBatch spriteBatch)
		{
			if (mNormalTexture == null) return;

			spriteBatch.Draw(mDepthTexture, new Rectangle(PositionX + (int)mOrigin.X, PositionY + (int)mOrigin.Y, mWidth, mHeight), new Rectangle(0, 0, mWidth, mHeight), mTint, MathHelper.ToRadians(mRotation), mOrigin, mEffekt, 0.0f);
		}

		public void LoadTextures()
		{
			mTexture = TextureManager.Instance.GetElementByString(TextureName);
			mNormalTexture = TextureManager.Instance.GetElementByString(TextureName + "Normal");
			mDepthTexture = TextureManager.Instance.GetElementByString(TextureName + "Depth");
		}
        #endregion
    }
}
