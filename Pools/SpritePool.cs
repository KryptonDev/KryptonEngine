using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Entities;

namespace KryptonEngine.Pools
{
    public class SpritePool : Pool<Sprite>
    {
        #region Singleton

        private static SpritePool mInstance;
        public static SpritePool Instance { get { if (mInstance == null) mInstance = new SpritePool(); return mInstance; } }

        #endregion

        #region Constructor

        public SpritePool() : base()
        {
        }

        #endregion

        #region Methods

        protected override Sprite CreateInstance()
        {
            return new Sprite();
        }

        protected override void CleanUpInstance(Sprite pObject)
        {
            //pObject.CleanUp();
        }

        #endregion
    }
}
