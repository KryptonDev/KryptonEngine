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
	public abstract class Item : Sprite
	{
		#region Properties

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public Item()
			:base()
		{

		}

		public Item(Vector2 pPosition, string pTextureName)
			:base(pPosition, pTextureName)
		{

		}

		#endregion

		#region OverrideMethods

		#endregion

		#region Methods

		public void SetupDeserialized()
		{
			LoadTextures();
		}

		#endregion
	}
}
