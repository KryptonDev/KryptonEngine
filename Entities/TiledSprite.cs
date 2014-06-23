/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class TiledSprite : Sprite
    {
        #region Properties

        protected Rectangle[] mSourceRectangle;
        protected int mSourceRectanglePosition = 0;
        protected int mSourceRectangleHeight;
        protected int mSourceRectangleWidth;

        #region Getter & Setter

        public int CurrentTile { get { return mSourceRectanglePosition; } 
            set 
            { 
                if (value < mSourceRectangle.Length && value >= 0 ) 
                    mSourceRectanglePosition = value; 
                else mSourceRectanglePosition = 0;

                mCollisionBox = new Rectangle(PositionX, PositionY, mSourceRectangle[mSourceRectanglePosition].Width, mSourceRectangle[mSourceRectanglePosition].Height);
            } 
        }

        public int CurrentTileWidth { get { return mSourceRectangle[mSourceRectanglePosition].Width; } }
        public int CurrentTileHeight { get { return mSourceRectangle[mSourceRectanglePosition].Height; } }

        public Rectangle CollisionBox { get { return mSourceRectangle[mSourceRectanglePosition]; } }
		public int HighestTile { get { return mSourceRectangle.Length - 1; } }
        #endregion
        
        #endregion

        #region Constructor

        public TiledSprite() { }

        public TiledSprite(Vector2 pPosition, String pTextureName, int pRectangleWidth, int pRectangleHeight)
            : base(pPosition, pTextureName)
        {
            mSourceRectangleWidth = pRectangleWidth;
            mSourceRectangleHeight = pRectangleHeight;

            int rectangleRows = //(mWidth % pRectangleHeight == 0) ? (int)(mWidth / pRectangleWidth) : (int)(mWidth / pRectangleWidth)
                (int)(mWidth / pRectangleWidth);

            int rectangleColumns = (int)(mHeight / pRectangleHeight);

            mSourceRectangle = new Rectangle[rectangleColumns * rectangleRows];

            for (int y = 0; y < rectangleRows; y++)
                for (int x = 0; x < rectangleColumns; x++)
                    mSourceRectangle[y * rectangleRows + x] = new Rectangle(x * pRectangleWidth, y * pRectangleHeight, pRectangleWidth, pRectangleHeight);

            mCollisionBox = new Rectangle((int)pPosition.X, (int)pPosition.Y, mSourceRectangleWidth, mSourceRectangleHeight);
            mDebugColor = Color.AliceBlue;
        }

        public TiledSprite(Vector2 pPosition, String pTextureName, List<Rectangle> pSourceRectangleList)
            : base(pPosition, pTextureName)
        {
            mSourceRectangle = new Rectangle[pSourceRectangleList.Count];

            for (int i = 0; i < pSourceRectangleList.Count; i++)
                mSourceRectangle[i] = pSourceRectangleList[i];

            mCollisionBox = mSourceRectangle[0];
            mDebugColor = Color.AliceBlue;
        }

        public TiledSprite(Vector2 pPosition, String pTextureName)
            : base(pPosition, pTextureName)
        {
            mSourceRectangleHeight = mTexture.Height;
            mSourceRectangleWidth = mTexture.Width;

            mSourceRectangle = new Rectangle[1];
            mSourceRectangle[0] = new Rectangle(0, 0, mSourceRectangleWidth, mSourceRectangleHeight);
            mDebugColor = Color.AliceBlue;
        }

        #endregion

        #region Methods

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, Position, mSourceRectangle[mSourceRectanglePosition], mTint);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX, PositionY, mSourceRectangleWidth, mSourceRectangleHeight), mDebugColor);
        }

        public Texture2D GetTileTexture2D(int pTile)
        {
            Color[] imageData = new Color[mWidth * mHeight];
            TextureManager.Instance.GetElementByString(mTextureName).GetData<Color>(imageData);

            Color[] color = new Color[mSourceRectangle[pTile].Width * mSourceRectangle[pTile].Height];
            for (int x = 0; x < mSourceRectangle[pTile].Width; x++)
                for (int y = 0; y < mSourceRectangle[pTile].Height; y++)
                  color[x + y * mSourceRectangle[pTile].Width] = imageData[x + mSourceRectangle[pTile].X + (y + mSourceRectangle[pTile].Y) * TextureManager.Instance.GetElementByString(mTextureName).Width];

            Texture2D subtexture = new Texture2D(EngineSettings.Graphics.GraphicsDevice, mSourceRectangle[pTile].Width, mSourceRectangle[pTile].Height);
            subtexture.SetData<Color>(color);

            return subtexture;
        }

		public Rectangle GetCurrentTileRectangle(int index)
		{
			return mSourceRectangle[index];
		}

        #endregion
    }
}
