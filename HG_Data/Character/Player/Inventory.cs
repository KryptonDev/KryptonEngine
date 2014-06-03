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

		public bool IsFull { get
		{
			foreach (InventorySlot slot in ItemSlots)
			{
				if (slot.Item == null)
					return false;
			}
			return true;
		} }

		#endregion

		#region Constructor

		public Inventory()
		{
			Initialize();
		}

		#endregion

		#region OverrideMethods

		public override void Initialize()
		{
			ItemSlots = new InventorySlot[3];
			for (int i = 0; i < ItemSlots.Length; i++)
			{
				ItemSlots[i] = new InventorySlot();
			}
		}

		#endregion

		#region Methods

		public bool TryToStore (Item item)
		{
			foreach (InventorySlot slot in ItemSlots)
			{
				if (slot.Item == null)
				{
					slot.Item = item;
					return true;
				}
			}
			return false;
		}

		public bool Contains(Type pItem)
		{
			foreach (InventorySlot slot in ItemSlots)
				if (slot.Item != null && slot.Item.GetType() == pItem)
					return true;
			return false;
		}

		public void SetupDeserialized()
		{
			foreach (InventorySlot slot in ItemSlots)
				slot.SetupDeserialized();
		}

		#endregion
	}
}
