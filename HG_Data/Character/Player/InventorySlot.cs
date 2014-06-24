using KryptonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class InventorySlot : BaseObject
	{
		#region Properties

		public Item Item;

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public InventorySlot()
		{

		}

		#endregion

		#region OverrideMethods

		#endregion

		#region Methods

		public void LoadContent()
		{
			if (Item != null)
				Item.LoadContent();
		}

		#endregion
	}
}
