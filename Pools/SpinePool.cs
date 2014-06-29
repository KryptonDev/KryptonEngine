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
		public static Dictionary<String, SpinePool> Pools = new Dictionary<string, SpinePool>(); //Wird vom SpineDataManager.LoadContent() gefüllt.

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
            TmpSpineObject.LoadContent();
            return TmpSpineObject;
        }

        protected override void CleanUpInstance(SpineObject pObject)
        {
            //pObject.CleanUp();
        }

        #endregion

    }
}
