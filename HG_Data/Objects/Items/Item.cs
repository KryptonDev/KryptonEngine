using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HanselAndGretel.Data
{
	[Serializable, XmlInclude(typeof(Branch)), XmlInclude(typeof(Key)), XmlInclude(typeof(Knife)), XmlInclude(typeof(Lantern)), XmlInclude(typeof(Matches))]
	public abstract class Item : InteractiveObject
	{
		#region Properties

		protected bool mIsHidden;

		#endregion

		#region Getter & Setter

		public bool IsHidden { get { return mIsHidden; } set { mIsHidden = value; } }

		#endregion

		#region Constructor

		public Item() : base() { }

		public Item(string pName)
			:base(pName)
		{

		}

		#endregion

		#region OverrideMethods

		#endregion
	}
}
