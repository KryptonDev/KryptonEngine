using KryptonEngine.Entities;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
		protected Texture2D mInventoryBackground;
		protected Texture2D mInventoryFocus;

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

		public override void LoadContent()
		{
			mInventoryBackground = TextureManager.Instance.GetElementByString("inventory");
			mInventoryFocus = TextureManager.Instance.GetElementByString("inventory_focus");
		}

		#endregion

		#region Methods

		public List<DrawPackage> GetDrawPackages(Vector2 pPosition, float pVisibility, int pFocus = 3)
		{
			List<DrawPackage> TmpDrawPackages = new List<DrawPackage>();
			//Background
			TmpDrawPackages.Add(new DrawPackage(pPosition, 0, Rectangle.Empty, mDebugColor, mInventoryBackground, pVisibility));
			//Items
			if (ItemSlots[0].Item != null)
				TmpDrawPackages.Add(new DrawPackage(pPosition + new Vector2(0, 0), 0, Rectangle.Empty, mDebugColor, ItemSlots[0].Item.Texture, pVisibility));
			if (ItemSlots[1].Item != null)
				TmpDrawPackages.Add(new DrawPackage(pPosition + new Vector2(64, 0), 0, Rectangle.Empty, mDebugColor, ItemSlots[1].Item.Texture, pVisibility));
			if (ItemSlots[2].Item != null)
				TmpDrawPackages.Add(new DrawPackage(pPosition + new Vector2(128, 0), 0, Rectangle.Empty, mDebugColor, ItemSlots[2].Item.Texture, pVisibility));
			//Focus
			if (pFocus < 3)
				TmpDrawPackages.Add(new DrawPackage(pPosition + new Vector2(64 * pFocus, 0), 0, Rectangle.Empty, mDebugColor, mInventoryFocus, pVisibility));
			return TmpDrawPackages;
		}

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
			LoadContent();
		}

		#endregion
	}
}
