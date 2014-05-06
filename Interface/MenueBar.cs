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
    public class MenueBar : InterfaceObject
    {
        #region Properties

        public const int MENUE_HEIGHT = 20;

        private List<String> mMenueString = new List<String>();
        private List<DropDownMenue> mDropDownList = new List<DropDownMenue>();
        private List<Vector2> mMenuePosition = new List<Vector2>();
        private List<Rectangle> mMenueRectangle = new List<Rectangle>();
        
        private Color mBackgroundColor;
        private int mMargin;

        private int mHoverOver;
        #endregion

        #region Getter & Setter

        public int Margin { set { mMargin = value; } }
        #endregion

        #region Constructor

        public MenueBar(Vector2 pPosition)
            : base(pPosition)
        {
            Initialize();
        }

        public MenueBar(Vector2 pPosition, List<String> pStringList, List<DropDownMenue> pDropDownList)
            : base(pPosition)
        {
            Initialize();
            mMenueString = pStringList;
            mDropDownList = pDropDownList;
        }
        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();

            mBackgroundColor = Color.Gray;
            mMargin = 5;
            mHoverOver = -1;
            mDrawRectangle = new Rectangle(0, 0, EngineSettings.VirtualResWidth, MENUE_HEIGHT);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mDrawRectangle, mBackgroundColor);

            if (mHover)
                spriteBatch.Draw(mTexture, mMenueRectangle[mHoverOver], mHoverColor);

            for (int i = 0; i < mMenueString.Count; i++)
                spriteBatch.DrawString(font, mMenueString[i], mMenuePosition[i], mFontColor);

            foreach (DropDownMenue ddm in mDropDownList)
                ddm.Draw(spriteBatch);

        }

        public override void Update()
        {
            mHover = false;
            mHoverOver = -1;

            for (int i = 0; i < mMenueString.Count; i++)
            {
                if (mMenueRectangle[i].Contains((int)MouseHelper.Position.X, (int)MouseHelper.Position.Y))
                {
                    mHover = true;
                    mHoverOver = i;
                    if (MouseHelper.Instance.IsClickedLeft)
                    {
                        // 6 = 5 für Verschiebung der MenueString + 1 für Verschiebung Outlines
                        mDropDownList[i].OpenDropDownMenue(mMenuePosition[i] + new Vector2(0, MENUE_HEIGHT + 6));
                        MouseHelper.ResetClick();
                    }
                }
            }

            foreach (DropDownMenue ddm in mDropDownList)
                ddm.Update();
        }

        public void AddMenueItem(String pMenueName, DropDownMenue pMenueDropDown)
        {
            mMenueString.Add(pMenueName);
            mDropDownList.Add(pMenueDropDown);
        }

        public void OrganizeButtons()
        {
            int x = mMargin;
            for (int i = 0; i < mMenueString.Count; i++)
            {
                mMenuePosition.Add(new Vector2(x,-5));
                int stringWidth = (int)font.MeasureString(mMenueString[i]).X;
                mMenueRectangle.Add(new Rectangle(x - mMargin, 0, stringWidth + 2 * mMargin, MENUE_HEIGHT));
                x += 2 * mMargin + stringWidth;

            }
        }

        #endregion
    }
}
