/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Manager
{
    public abstract class Manager<T>
    {
        #region Properties

        protected Dictionary<String, T> mRessourcen = new Dictionary<string, T>();
        
        #region Getter & Setter

        #endregion

        #endregion

        #region Constructor

        #endregion

        #region Methods

        abstract public T Add(String pName, String pPath);

        abstract public void LoadContent();

        abstract public void Unload();

        abstract public T GetElementByString(String pElementName);

        #endregion
    }
}
