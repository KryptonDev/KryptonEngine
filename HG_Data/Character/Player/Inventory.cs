using KryptonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Inventory : GameObject
	{
		#region Properties

		public InventorySlot[] ItemSlots;

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public Inventory()
		{

		}

		#endregion

		#region OverrideMethods

		#endregion

		#region Methods

		public bool Contains(Type pItem)
		{
			return true;
		}

		#endregion
	}
}
