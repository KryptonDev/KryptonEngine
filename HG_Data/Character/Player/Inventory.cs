﻿using KryptonEngine.Entities;
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
			ItemFocus = 0;
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
				int TmpSlot = 2 + pFocus - i;
				Vector2 TmpOrigin = new Vector2(mInventoryBackground.Bounds.Center.X, mInventoryBackground.Bounds.Center.Y);
				pSpriteBatch.Draw(mInventoryBackground, pPosition + DrawPosition[TmpSlot], null, Color.White * pVisibility, DrawRotation[TmpSlot], TmpOrigin, DrawScale[TmpSlot], SpriteEffects.None, 1f);
			}
			//Draw Items
			for (int i = 0; i < 3; i++)
			{
				if (ItemSlots[i].Item != null)
				{
					int TmpSlot = 2 + pFocus - i;
					Vector2 TmpOrigin = new Vector2(ItemSlots[i].Item.Texture.Bounds.Center.X, ItemSlots[i].Item.Texture.Bounds.Center.Y);
					pSpriteBatch.Draw(ItemSlots[i].Item.Texture, pPosition + DrawPosition[TmpSlot], null, Color.White * pVisibility, DrawRotation[TmpSlot], TmpOrigin, DrawScale[TmpSlot], SpriteEffects.None, 1f);
				}
			}
				
			/*
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
			 */
		}

		public bool TryToStore (Item item)
		{
			if (ItemSlots[1].Item == null)
			{
				ItemSlots[1].Item = item;
				return true;
			}
			if (ItemSlots[0].Item == null)
			{
				ItemSlots[0].Item = item;
				return true;
			}
			if (ItemSlots[2].Item == null)
			{
				ItemSlots[2].Item = item;
				return true;
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
