using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Manager;

namespace KryptonEngine.Entities
{
    public abstract class InterfaceObject
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

		protected bool mVisible;
		protected Vector2 mPosition;
		protected Rectangle mCollisionBox;
        #endregion

        #region Getter & Setter
		public Vector2 Position
		{
			set
			{
				mPosition = value;
				mCollisionBox.X = (int)value.X;
				//mCollisionBox.Y = (int)value.Y;
				//mDrawRectangle.X = (int)value.X;
				//mDrawRectangle.Y = (int)value.Y;

				//mHoverRectangle.X = (int)value.X;
				//mOutlineRectangle.X = (int)value.X;

				//mHoverRectangle.Y = (int)value.Y;
				//mOutlineRectangle.Y = (int)value.Y;
			}
			get { return mPosition; }
		}
		public int PositionX
		{
			set
			{
				mPosition.X = value;
				//mCollisionBox.X = value;
				//mDrawRectangle.X = value;
				//mHoverRectangle.X = value;
				//mOutlineRectangle.X = value;
			}
			get { return (int)mPosition.X; }
		}
		public int PositionY
		{
			set
			{
				mPosition.Y = value;
				mCollisionBox.Y = value;
				mDrawRectangle.Y = (int)value;
			}
			get { return (int)mPosition.Y; }
		}

		public Color BackgroundColor { set { mDrawColor = value; } get { return mDrawColor; } }
		public bool IsVisible { get { return mVisible; } set { mVisible = value; } }
		public Rectangle CollisionBox { get { return mCollisionBox; } set { mCollisionBox = value; } }
        #endregion

        #region Constructor
		public InterfaceObject() { Initialize(); }

		public InterfaceObject(Vector2 pPosition) { mPosition = pPosition; Initialize(); }
        public InterfaceObject(Vector2 pPosition, Rectangle pRectangle)
        {
			mPosition = pPosition;
            mDrawRectangle = pRectangle;
            mDrawRectangle.X += (int)mPosition.X;
			mDrawRectangle.Y += (int)mPosition.Y;
			mOutlineRectangle = new Rectangle(pRectangle.X - 1 + (int)mPosition.X, pRectangle.Y - 1 + (int)mPosition.Y, pRectangle.Width + 2, pRectangle.Height + 2);
            Initialize();
        }
        #endregion

        #region Override Methods

        public virtual void Initialize()
        {
            mDrawColor = Color.Gray;
            mHoverColor = Color.Blue;
            mFontColor = Color.Black;
            mOutlineColor = Color.AliceBlue;

			font = FontManager.Instance.GetElementByString("font");

            mTextureName = "pixel";
            mTexture = TextureManager.Instance.GetElementByString(mTextureName);
			mVisible = true;
        }

        #endregion

        #region Methods

        public virtual void OpenWindow()
        {
            IsVisible = true;
        }

		public abstract void Draw(SpriteBatch mSpriteBatch);
		public abstract void Update();
        #endregion
    }
}
