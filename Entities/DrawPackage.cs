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
		protected float mPositionZ;
		protected Rectangle mCollisionBox;
		protected Color mDebugColor;
		protected Texture2D mTexture;
		protected Skeleton mSkeleton;
		protected bool Spine;
		protected float mAlpha;

		#endregion

		#region Constructor

		public DrawPackage(Vector2 pPosition, float pPositionZ, Rectangle pCollisionBox, Color pDebugColor, float pAlpha = 1f)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mTexture = TextureManager.Instance.GetElementByString("pixel");
			mAlpha = pAlpha;
			Spine = false;
		}

		public DrawPackage(Vector2 pPosition, float pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Texture2D pTexture, float pAlpha = 1f)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mTexture = pTexture;
			mAlpha = pAlpha;
			Spine = false;
		}

		public DrawPackage(Vector2 pPosition, float pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Skeleton pSkeleton, float pAlpha = 1f)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mSkeleton = pSkeleton;
			mAlpha = pAlpha;
			Spine = true;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Drawed den Package-Inhalt
		/// </summary>
		/// <param name="pSpriteBatch">SpriteBatch zum drawen von Texturen</param>
		/// <param name="pSkeletonRenderer">SkeletonRenderer zum drawen von Texturen</param>
		public void Draw(SpriteBatch pSpriteBatch, SkeletonRenderer pSkeletonRenderer)
		{
			if (!Spine)
			{
				pSpriteBatch.Draw(mTexture, mPosition, new Color(mAlpha, mAlpha, mAlpha, mAlpha));
			}
			else
			{
				pSkeletonRenderer.Begin();
				pSkeletonRenderer.Draw(mSkeleton);
				pSkeletonRenderer.End();
			}
			if (EngineSettings.IsDebug)
				pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), mCollisionBox, mDebugColor);
		}

		#endregion
	}
}
