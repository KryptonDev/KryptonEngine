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
			if (FreeItemSlot == null)
				return false;
			return true;
		} }

		public InventorySlot FreeItemSlot { get
		{
			foreach (InventorySlot slot in ItemSlots)
			{
				if (slot.Item == null)
					return slot;
			}
			return null;
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

		public bool Contains(Type pItem)
		{
			return true;
		}

		public void SetupDeserialized()
		{
			foreach (InventorySlot slot in ItemSlots)
				slot.SetupDeserialized();
		}

		#endregion
	}
}
