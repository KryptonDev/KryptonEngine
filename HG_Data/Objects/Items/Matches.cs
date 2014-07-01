using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Matches : Item
	{
		#region Properties

		protected int mCount;

		#endregion

		#region Getter & Setter

		public int Count { get { return mCount; } set { mCount = value; } }

		#endregion

		#region Constructor

		public Matches()
			:base()
		{
			Initialize();
		}

		public Matches(string pTextureName)
			:base(pTextureName)
		{
			Initialize();
		}

		#endregion

		#region OverrideMethods

		public override void Initialize()
		{
			mCount = 10;
		}

		#endregion

		#region Methods

		#endregion
	}
}
