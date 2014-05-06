using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Manager;
using KryptonEngine.Controls;

namespace KryptonEngine.Interface
{
    public class DropDownMenue : InterfaceObject
    {
        #region Properties
        
        private const int MENUE_COLUMN_HEIGHT = 20;

        private List<String> mMenueItems = new List<string>();
        private List<Action> mMenueActions = new List<Action>();

        private int mHeight;
        private int mWidth;
        #endregion

        #region Getter & Setter

        #endregion

        #region Constructor

        public DropDownMenue(Vector2 pPosition, List<String> pStringList, List<Action> pActionList) 
            : base(pPosition)
        {
            Position = pPosition;
            mMenueItems = pStringList;
            mMenueActions = pActionList;
            
            mHeight = MENUE_COLUMN_HEIGHT * mMenueItems.Count;
            mWidth = 0;

            foreach(String s in mMenueItems)
            {
                int tmpWidth = (int)font.MeasureString(s).X;
                if (tmpWidth > mWidth)
                    mWidth = tmpWidth;
            }

            mWidth += 10;

            mDrawRectangle = new Rectangle(0, 0, mWidth, mHeight);
            mHoverRectangle = new Rectangle(0, 0, mWidth, MENUE_COLUMN_HEIGHT);
            mOutlineRectangle = new Rectangle(0,0, mWidth+2, mHeight+2);
            mVisible = false;
        }
        #endregion

        #region Override Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            // return wenn Dropdown nicht gezeigt wird
            if (!IsVisible) return;

            // Draw Outlines
            spriteBatch.Draw(mTexture, Position - new Vector2(1, 1), mOutlineRectangle, mOutlineColor);

            // Draw Box
            spriteBatch.Draw(mTexture, Position, mDrawRectangle, mDrawColor);
            // Draw Selected Hover
            if (mHover)
                spriteBatch.Draw(mTexture, mHoverRectangle, mHoverColor);
            // Draw Methods Strings
            for (int i = 0; i < mMenueItems.Count; i++)
                spriteBatch.DrawString(font, mMenueItems[i], new Vector2(PositionX + 5, (PositionY + i * MENUE_COLUMN_HEIGHT) - 5), mFontColor);
        }

        public override void Update()
        {
            // return wenn DropDown nicht aktiv ist
            if (!IsVisible) return;

            // Überprüft ob gehoverd wird
            if(mDrawRectangle.Contains((int)MouseHelper.Position.X, (int)MouseHelper.Position.Y))
            {
                mHover = true;
                int posY = mDrawRectangle.Y - (int)MouseHelper.Position.Y; ;

                int Column = (int)(posY / MENUE_COLUMN_HEIGHT);

                mHoverRectangle.X = mDrawRectangle.X;
                mHoverRectangle.Y = mDrawRectangle.Y - MENUE_COLUMN_HEIGHT * Column;
            }
            else
                mHover = false;

            // Überprüft Klick um Action auszuführen, danach wird das Menü geschlossen
            if (MouseHelper.Instance.IsClickedLeft )//&& mDrawRectangle.Contains((int)MouseHelper.Position.X, (int)MouseHelper.Position.Y))
            {
                if (mDrawRectangle.Contains((int)MouseHelper.Position.X, (int)MouseHelper.Position.Y))
                {
                    int posY = (int)MouseHelper.Position.Y - mDrawRectangle.Y;
                    int Column = (int)(posY / MENUE_COLUMN_HEIGHT);

                    if (mMenueActions == null) return;
                    if (Column < mMenueActions.Count)
                    {
                        mMenueActions[Column]();
                        IsVisible = false;
                    }
                }
                else
                    IsVisible = false;
            }

        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Öffnet das DropDown Menü an der Position,
        /// Setzt das DrawRectangle und HoverRectangle an Öffnungsposition
        /// </summary>
        /// <param name="pPosition"></param>
        public void OpenDropDownMenue(Vector2 pPosition)
        {
            Position = pPosition;
            mDrawRectangle.X = PositionX;
            mDrawRectangle.Y = PositionY;
            mHoverRectangle.X = PositionX;
            mHoverRectangle.Y = PositionY;
            mVisible = true;
        }
        #endregion
    }
}
