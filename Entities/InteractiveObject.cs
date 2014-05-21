using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
	public class InteractiveObject : GameObject
	{
		#region Properties

		protected List<Rectangle> mActionRectList = new List<Rectangle>();
		protected List<Rectangle> mCollisionRectList = new List<Rectangle>();

		protected Vector2 mActionPosition1;
		protected Vector2 mActionPosition2;

		protected int mActionId;
		//protected GameObject mDrawObject;
		protected Texture2D mTexture;
		protected String mTextureName;
		#endregion

		#region Getter & Setter

		public List<Rectangle> ActionRectList { get { return mActionRectList; } set { mActionRectList = value; } }
		public List<Rectangle> CollisionRectList { get { return mCollisionRectList; } set { mCollisionRectList = value; } }
		public Vector2 ActionPosition1 { get { return mActionPosition1; } set { mActionPosition1 = value; } }
		public Vector2 ActionPosition2 { get { return mActionPosition2; } set { mActionPosition2 = value; } }
		public int ActionId { get { return mActionId; } set { mActionId = value; } }
		public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
		public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
		public DrawPackage DrawPackage { get { return new DrawPackage(Position, DrawZ, CollisionRectList[0], mDebugColor); } }
		#endregion

		#region Constructor

		public InteractiveObject() { }
		#endregion

		#region Override Methods

		public override void Update()
		{

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (mTexture != null)
			{
				spriteBatch.Draw(mTexture, Position, Color.White);
				if (EngineSettings.IsDebug)
				{
					foreach (Rectangle r in ActionRectList)
						spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), r, Color.Yellow);
					foreach (Rectangle r in CollisionRectList)
						spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), r, Color.Blue);
					spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX, DrawZ, mTexture.Width, 1), Color.Red);
				}
			}
		}
		#endregion

		#region Methods

		public Vector2 GetNearestStartPosition(Vector2 PlayerPosition)
		{
			float Distance1 = Vector2.Distance(PlayerPosition, mActionPosition1);
			float Distance2 = Vector2.Distance(PlayerPosition, mActionPosition2);

			return (Math.Min(Distance1, Distance2) == Distance1) ? mActionPosition1 : mActionPosition2;
		}

		public void CopyFrom(InteractiveObject io)
		{
			this.ActionRectList = new List<Rectangle>(io.ActionRectList);
			this.CollisionRectList = new List<Rectangle>(io.CollisionRectList);
			this.ActionPosition1 = io.ActionPosition1;
			this.ActionPosition2 = io.ActionPosition2;
			this.DrawZ = io.DrawZ;
			this.ActionId = io.ActionId;
			this.mTexture = io.Texture;
			this.mTextureName = io.TextureName;

			this.Position = io.Position;
		}
		#endregion
	}
}
