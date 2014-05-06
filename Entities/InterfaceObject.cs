using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Manager;

namespace KryptonEngine.Entities
{
    public class InterfaceObject : GameObject
    {
        #region Properties

        protected Color mDrawColor;
        protected Color mHoverColor;
        protected Color mFontColor;
        protected Color mOutlineColor;

        protected static SpriteFont font;
        protected Rectangle mDrawRectangle;
        protected Rectangle mHoverRectangle;
        protected Rectangle mOutlineRectangle;

        protected String mTextureName;
        protected Texture2D mTexture;

        protected bool mHover;
        #endregion

        #region Getter & Setter
        public Color BackgroundColor { set { mDrawColor = value; } } 
        #endregion

        #region Constructor
        public InterfaceObject() : base() { Initialize(); }

        public InterfaceObject(Vector2 pPosition) : base(pPosition) { Initialize(); }
        public InterfaceObject(Vector2 pPosition, Rectangle pRectangle) : base(pPosition)
        {
            mDrawRectangle = pRectangle;
            mDrawRectangle.X += PositionX;
            mDrawRectangle.Y += PositionY;
            mOutlineRectangle = new Rectangle(pRectangle.X - 1 + PositionX, pRectangle.Y - 1 + PositionY, pRectangle.Width + 2, pRectangle.Height + 2);
            Initialize();
        }
        #endregion

        #region Override Methods

        public override void Initialize()
        {
            mDrawColor = Color.Gray;
            mHoverColor = Color.Blue;
            mFontColor = Color.Black;
            mOutlineColor = Color.AliceBlue;

            font = FontManager.Instance.Add("MenueFont", @"font\font");

            mTextureName = "pixel";
            mTexture = TextureManager.Instance.GetElementByString(mTextureName);
        }

        #endregion

        #region Methods

        public virtual void OpenWindow()
        {
            IsVisible = true;
        }
        #endregion
    }
}
