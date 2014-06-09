using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class DirectionLight : Light
	{		
		#region Properties
		private Vector3 mDirection;
		#endregion

		#region Getter & Setter
		public Vector3 Direction { get { return mDirection; } set { mDirection = value; } }
		#endregion

		#region Constructor

		public DirectionLight() : base() 
		{
			mDirection = Vector3.One;
			LightColor = Vector3.One;
		}

		public DirectionLight(Vector2 pPosition)
			:base(pPosition)
		{

		}
		#endregion

		#region Override Methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("IconDirectionLight"), mPosition, new Rectangle(0, 0, 64, 64), Color.White);
		}
		#endregion

		#region Methods
		#endregion
	}
}




//		public void drawLine(Vector2 from, Vector2 to, Texture2D texture, float size, SpriteBatch spriteBatch)
//		{

//			double degress = Math.Atan2((to - from).Y, (to - from).X);
//			float length = Vector2.Distance(from, to);
//			spriteBatch.Draw(texture, from, new Rectangle(0, 0, 1, 1), Color.White, (float)degress, Vector2.Zero, new Vector2(length, size), SpriteEffects.None, 0);

//		}