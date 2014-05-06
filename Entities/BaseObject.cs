/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class BaseObject
    {
        #region Properties

        protected int mId;
        protected static int mIdAll = 0;
        #endregion

        #region Getter & Setter
        
        #endregion

        #region Constructor

        public BaseObject() 
        {
            mId = mIdAll;
            mIdAll++;
        }

        #endregion

        #region Methods

        public virtual void Update() { }
        public virtual void LoadContent() { }
        public virtual void Initialize() { }
        public virtual String GetInfo() { return ""; }

        #endregion
    }
}
