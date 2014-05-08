using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
	public class DrawPackage
	{
		#region Properties

		protected Vector2 mPosition;
		protected int mPositionZ;
		protected Rectangle mCollisionBox;
		protected Color mDebugColor;
		protected Texture2D mTexture;
		protected Skeleton mSkeleton;
		protected bool Spine;

		#endregion

		#region Constructor

		public DrawPackage(Vector2 pPosition, int pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Texture2D pTexture)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mTexture = pTexture;
			Spine = false;
		}

		public DrawPackage(Vector2 pPosition, int pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Skeleton pSkeleton)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mSkeleton = pSkeleton;
			Spine = true;
		}

		#endregion

		#region Methods

		public void Draw(SpriteBatch pSpriteBatch, Vector2 pCameraPosition)
		{
			if (!Spine)
			{
				pSpriteBatch.Draw(mTexture, mPosition, Color.White);
			}
			else
			{
				mSkeleton.X = mPosition.X - pCameraPosition.X;
				mSkeleton.Y = mPosition.Y - pCameraPosition.X;
				EngineSettings.SpineRenderer.Begin();
				EngineSettings.SpineRenderer.Draw(mSkeleton);
				EngineSettings.SpineRenderer.End();
			}
			if (EngineSettings.IsDebug)
				pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), mCollisionBox, mDebugColor);
		}

		#endregion
	}
}
