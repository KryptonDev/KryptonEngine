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
		public int ItemFocus;
		protected Texture2D mInventoryBackground;
		protected static Vector2[] DrawPosition = new Vector2[5] { new Vector2(-80, 50), new Vector2(-50, 20), new Vector2(0, 0), new Vector2(50, 20), new Vector2(80, 50) };
		protected static float[] DrawRotation = new float[5] { -1f, -0.5f, 0f, 0.5f, 1f };
		protected static float[] DrawScale = new float[5] { 0.6f, 0.6f, 1f, 0.6f, 0.6f };

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

		public InventorySlot CurrentSlot { get { return ItemSlots[ItemFocus]; } }

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
			ItemFocus = 1;
		}

		public override void LoadContent()
		{
			mInventoryBackground = TextureManager.Instance.GetElementByString("inventory");
		}

		#endregion

		#region Methods

		public void Draw(SpriteBatch pSpriteBatch, Vector2 pPosition, float pVisibility, int pFocus = 3)
		{
			//Default Focus
			if (pFocus == 3)
				pFocus = ItemFocus;
			//Draw InventoryBackground
			for (int i = 0; i < 3; i++)
			{
				int TmpSlot = 2 - pFocus + i;
				Vector2 TmpOrigin = new Vector2(mInventoryBackground.Bounds.Center.X, mInventoryBackground.Bounds.Center.Y);
				pSpriteBatch.Draw(mInventoryBackground, pPosition + DrawPosition[TmpSlot], null, Color.White * pVisibility, DrawRotation[TmpSlot], TmpOrigin, DrawScale[TmpSlot], SpriteEffects.None, 1f);
			}
			//Draw Items
			for (int i = 0; i < 3; i++)
			{
				if (ItemSlots[i].Item != null)
				{
					int TmpSlot = 2 - pFocus + i;
					Vector2 TmpOrigin = new Vector2(ItemSlots[i].Item.Textures[0].Bounds.Center.X, ItemSlots[i].Item.Textures[0].Bounds.Center.Y);
					pSpriteBatch.Draw(ItemSlots[i].Item.Textures[0], pPosition + DrawPosition[TmpSlot], null, Color.White * pVisibility, DrawRotation[TmpSlot], TmpOrigin, DrawScale[TmpSlot], SpriteEffects.None, 1f);
				}
			}
		}

		public bool TryToStore (Item item)
		{
			if (ItemSlots[1].Item == null)
			{
				ItemSlots[1].Item = item;
				if (ItemSlots[0].Item == null && ItemSlots[2].Item == null)
					ItemFocus = 1;
				return true;
			}
			if (ItemSlots[0].Item == null)
			{
				ItemSlots[0].Item = item;
				if (ItemSlots[1].Item == null && ItemSlots[2].Item == null)
					ItemFocus = 0;
				return true;
			}
			if (ItemSlots[2].Item == null)
			{
				ItemSlots[2].Item = item;
				if (ItemSlots[0].Item == null && ItemSlots[1].Item == null)
					ItemFocus = 2;
				return true;
			}
			return false;
		}

		public bool Contains(Type pItem, bool InCurrentSlot = true)
		{
			if (InCurrentSlot)
			{
				if (ItemSlots[ItemFocus].Item != null && ItemSlots[ItemFocus].Item.GetType() == pItem)
					return true;
				return false;
			}
			foreach (InventorySlot slot in ItemSlots)
				if (slot.Item != null && slot.Item.GetType() == pItem)
					return true;
			return false;
		}

		public void LoadTextures()
		{
			foreach (InventorySlot slot in ItemSlots)
				slot.LoadTextures();
			LoadContent();
		}

		#endregion
	}
}
