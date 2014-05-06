/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Entities;

namespace KryptonEngine.Pools
{
    public class SpinePool : Pool<SpineObject>
    {
        #region Singleton

        private static SpinePool mFluffy;
        private static SpinePool mNextDummySkeleton;

        public static SpinePool Fluffy { get { if (mFluffy == null) mFluffy = new SpinePool("fluffy"); return mFluffy; } }
        public static SpinePool NextDummySkeleton { get { if (mNextDummySkeleton == null) mNextDummySkeleton = new SpinePool("skeleton"); return mNextDummySkeleton; } }

        #endregion

        #region Properties

        private string mName;

        #endregion

        #region Constructor

        public SpinePool(string pName) : base()
        {
            mName = pName;
        }

        #endregion

        #region Methods

        protected override SpineObject CreateInstance()
        {
            SpineObject TmpSpineObject = new SpineObject(mName);
            TmpSpineObject.Load();
            return TmpSpineObject;
        }

        protected override void CleanUpInstance(SpineObject pObject)
        {
            pObject.CleanUp();
        }

        #endregion

    }
}
