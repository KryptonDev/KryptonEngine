/**************************************************************
 * (c) Jens Richter, Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Pools
{
    public abstract class Pool<T>
    {
        #region Properties

        protected static List<T> mFreeRessources;
        protected static List<T> mUsedRessources;

        #region Getter & Setter
        
        #endregion

        #endregion

        #region Constructor

        public Pool()
        {
            mFreeRessources = new List<T>();
            mUsedRessources = new List<T>();
        }

        #endregion

        #region Methods

        public T GetObject()
        {
            if (mFreeRessources.Count != 0)
            {
                T TmpObject = mFreeRessources[0];
                mUsedRessources.Add(TmpObject);
                mFreeRessources.RemoveAt(0);
                return TmpObject;
            }
            else
            {
                T TmpObject = CreateInstance();
                mUsedRessources.Add(TmpObject);
                return TmpObject;
            }
        }

        public void ReleaseObject(T pObject)
        {
            CleanUpInstance(pObject);
            mFreeRessources.Add(pObject);
            mUsedRessources.Remove(pObject);
        }

        #region Abstract

        protected abstract T CreateInstance();
        protected abstract void CleanUpInstance(T pObject);
        
        #endregion
        
        #endregion
    }
}
