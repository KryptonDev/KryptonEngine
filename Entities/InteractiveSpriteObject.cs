using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
	public class InteractiveSpriteObject : InteractiveObject
	{
		#region Properties

		protected Texture2D mTexture;
		protected String mTextureName;

		#endregion

		#region Getter & Setter

		public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
		public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
		public DrawPackage DrawPackage { get { return new DrawPackage(Position, DrawZ, CollisionBox, mDebugColor, mTexture); } }

		#endregion

		#region Constructor

		public InteractiveSpriteObject()
			:base()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
 			if (mTexture != null)
				spriteBatch.Draw(mTexture, Position, Color.White);
			base.Draw(spriteBatch);
		}
	
		#endregion

	}
}
