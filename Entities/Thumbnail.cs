using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class Thumbnail : Sprite
    {
        #region Properties

        public const int THUMBNAIL_WIDTH = 64;
        public const int THUMBNAIL_HEIGHT = 64;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        public Thumbnail() { }

        public Thumbnail(Vector2 pPosition, String pTextureName, String pPathName)
            : base(pPosition, pTextureName, pPathName)
        {
            Initialize();
        }

        public Thumbnail(Vector2 pPosition, String pTextureName)
            : base(pPosition, pTextureName)
        {
            Initialize();
        }

        #endregion

        #region Override Methods

        public override void Initialize()
        {
            mCollisionBox = new Rectangle(PositionX, PositionY, THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, new Rectangle(PositionX, PositionY, THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT), Color.White);
            if (EngineSettings.IsDebug)
                spriteBatch.Draw(mTexture, new Rectangle(PositionX, PositionY, mWidth, mHeight), mDebugColor);
        }
        #endregion
    }
}
