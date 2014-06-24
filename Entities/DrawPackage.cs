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
		protected Texture2D[] mTextures;
		protected Skeleton mSkeleton;
		protected bool mSpine;
		protected float mAlpha;
		protected bool mOnlyDebug;

		#endregion

		#region Getter & Setter

		public bool Spine { get { return mSpine; } }
		public bool OnlyDebug { get { return mOnlyDebug; } }

		#endregion

		#region Constructor

		public DrawPackage(Rectangle pCollisionBox, Color pDebugColor, float pAlpha = 1f)
		{
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mTextures = new Texture2D[1] { TextureManager.Instance.GetElementByString("pixel") };
			mAlpha = pAlpha;
			mSpine = false;
			mOnlyDebug = true;
		}

		public DrawPackage(Vector2 pPosition, float pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Texture2D[] pTextures, float pAlpha = 1f)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mTextures = pTextures;
			mAlpha = pAlpha;
			mSpine = false;
			mOnlyDebug = false;
		}

		public DrawPackage(Vector2 pPosition, float pPositionZ, Rectangle pCollisionBox, Color pDebugColor, Skeleton pSkeleton, Texture2D[] pTextures, float pAlpha = 1f)
		{
			mPosition = pPosition;
			mPositionZ = pPositionZ;
			mCollisionBox = pCollisionBox;
			mDebugColor = pDebugColor;
			mSkeleton = pSkeleton;
			mTextures = pTextures;
			mAlpha = pAlpha;
			mSpine = true;
			mOnlyDebug = false;
		}

		#endregion

		#region Methods

		/*
		/// <summary>
		/// Drawed den Package-Inhalt
		/// </summary>
		/// <param name="pSpriteBatch">SpriteBatch zum drawen von Texturen</param>
		/// <param name="pSkeletonRenderer">SkeletonRenderer zum drawen von Texturen</param>
		public void Draw(SpriteBatch pSpriteBatch, SkeletonRenderer pSkeletonRenderer)
		{
			if (EngineSettings.IsDebug)
			{
				pSpriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), mCollisionBox, mDebugColor * mAlpha);
			}
			if (mOnlyDebug)
				return;
			if (!mSpine)
			{
				pSpriteBatch.Draw(mTexture, mPosition, new Color(mAlpha, mAlpha, mAlpha, mAlpha));
			}
			else
			{
				pSkeletonRenderer.Begin();
				pSkeletonRenderer.Draw(mSkeleton);
				pSkeletonRenderer.End();
			}
		}
		*/
		#endregion
	}
}
