using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Interface
{
    public class Box : InterfaceObject
    {
        #region Properties

        protected Rectangle mBoxDimension;
        #endregion

        #region Getter & Setter
        #endregion

        #region Constructor
        public Box(Vector2 pPosition, Rectangle pRectangle)
            : base(pPosition, pRectangle)
        {
            mBoxDimension = pRectangle;
        }
        #endregion

        #region Methods

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //// return wenn Dropdown nicht gezeigt wird
            if (!IsVisible) return;

            // Draw Outlines
            spriteBatch.Draw(mTexture, Position - new Vector2(1, 1), mOutlineRectangle, mOutlineColor);

            // Draw Box
            spriteBatch.Draw(mTexture, Position, mDrawRectangle, mDrawColor);
            // Draw Selected Hover
            if (mHover)
                spriteBatch.Draw(mTexture, mHoverRectangle, mHoverColor);
        }
        #endregion
    }
}
