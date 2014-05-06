/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Controls;
using KryptonEngine.Entities;

namespace KryptonEngine.Interface
{
    public class Button : TiledSprite
    {
        #region Proberties

        private int mBtnId;
        public Action OnButtonPressed;
        #endregion

        #region Getter & Setter
        public int ButtonId { get { return mBtnId; } set { mBtnId = value; } }
        #endregion

        #region Constructor

        public Button() { }

        public Button(Vector2 pPosition, String pTextureName, String pPath, int pRectangleWidth, int pRectangleHeight)
            : base(pPosition, pTextureName, pPath, pRectangleWidth, pRectangleHeight)
        {   }

        public Button(Vector2 pPosition, String pTextureName, String pPath, List<Rectangle> pSourceRectangleList)
            : base(pPosition, pTextureName, pPath, pSourceRectangleList)
        {
            mSourceRectangle = new Rectangle[pSourceRectangleList.Count];

            for (int i = 0; i < pSourceRectangleList.Count; i++)
                mSourceRectangle[i] = pSourceRectangleList[i];
        }

        public Button(Vector2 pPosition, String pTextureName, String pPath)
            : base(pPosition, pTextureName, pPath)
        {
            TextureName = pTextureName;
        }
        #endregion

        #region Override Methods

        public void Update()
        {
            IsButtonPressed(MouseHelper.Position);
        }
        #endregion

        #region Methods

        public void IsButtonPressed(Vector2 pClickArea)
        {
            if(mCollisionBox.Contains((int)pClickArea.X, (int)pClickArea.Y)
                && OnButtonPressed != null
                && MouseHelper.Instance.IsClickedLeft
                )
                OnButtonPressed();
        }

        #endregion
    }
}
