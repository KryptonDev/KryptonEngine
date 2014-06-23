using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Controls;

namespace KryptonEngine.Interface
{
    public class Window : InterfaceObject
    {
        #region Properties

        protected const int TITLEBAR_HEIGHT = 22;
        private string mWindowName;
        protected Vector2 mWindowDimension;
        private Vector2 mMoveOffset = Vector2.Zero;

        private Rectangle mMoveRectangle;

        private Button mButtonClose;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor

        public Window() { }

        public Window(Vector2 pPosition, String pName, Vector2 pSize)
            : base(pPosition)
        {
            Position = pPosition;
            mWindowDimension = pSize;
            mWindowName = pName;

            mOutlineRectangle = new Rectangle(PositionX - 1, PositionY - 1 - TITLEBAR_HEIGHT, (int)mWindowDimension.X + 2, (int)mWindowDimension.Y + TITLEBAR_HEIGHT + 2);
            mDrawRectangle = new Rectangle(PositionX , PositionY, (int)mWindowDimension.X, (int)mWindowDimension.Y);

            mMoveRectangle = new Rectangle(PositionX - 1, PositionY - 1 - TITLEBAR_HEIGHT, (int)mWindowDimension.X + 2, TITLEBAR_HEIGHT);
            //mButtonClose = new Button(new Vector2(PositionX + (int)mWindowDimension.X - 19, PositionY + 1 - TITLEBAR_HEIGHT), "EngineClose", @"Engine\EngineClose");
            mButtonClose.OnButtonPressed = CloseWindow;
            IsVisible = false;
        }
        #endregion

        #region Override Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible) return;

            spriteBatch.Draw(mTexture, mOutlineRectangle, mOutlineColor);
            spriteBatch.Draw(mTexture, mDrawRectangle, mDrawColor);
            mButtonClose.Draw(spriteBatch);
            spriteBatch.DrawString(font, mWindowName, new Vector2(PositionX + 5, PositionY - 5 - TITLEBAR_HEIGHT), mFontColor);
        }

        public override void Update()
        {
            if (!IsVisible) return;

            DragAndDrop();
            mButtonClose.Update();

        }

        public override void OpenWindow()
        {
            SortEntitesOnScreen(Vector2.Zero);
            base.OpenWindow();
        }

        protected virtual void SortEntitesOnScreen(Vector2 pMoveOffset)
        {

        }
        #endregion

        #region Methods

        public void CloseWindow()
        {
            IsVisible = false;
        }

        protected void DragAndDrop()
        {
            if (mMoveRectangle.Contains((int)MouseHelper.Position.X, (int)MouseHelper.Position.Y)
                && MouseHelper.Instance.IsPressedLeft
                && mMoveOffset == Vector2.Zero)
            {
                mMoveOffset = MouseHelper.Position - Position;
            }
            else if (MouseHelper.Instance.IsPressedLeft && mMoveOffset != Vector2.Zero)
            {
                Position = MouseHelper.Position - mMoveOffset;
                mOutlineRectangle.X = (int)(PositionX - 1);
                mOutlineRectangle.Y = (int)(PositionY - 1 - TITLEBAR_HEIGHT);
                mDrawRectangle.X = (int)(PositionX);
                mDrawRectangle.Y = (int)(PositionY);
                mMoveRectangle.X = (int)(PositionX - 1);
                mMoveRectangle.Y = (int)(PositionY - 1 - TITLEBAR_HEIGHT);
                mButtonClose.Position = Position + new Vector2((int)mWindowDimension.X - 19, 1 - TITLEBAR_HEIGHT);
                SortEntitesOnScreen(mMoveOffset);
            }
            else
                mMoveOffset = Vector2.Zero;
        }
        #endregion
    }
}
