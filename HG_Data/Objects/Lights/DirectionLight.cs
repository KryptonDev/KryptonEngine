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
		#endregion

		#region Getter & Setter
		#endregion

		#region Constructor

		public DirectionLight() : base() { }

		public DirectionLight(Vector2 pPosition)
			:base(pPosition)
		{

		}
		#endregion

		#region Override Methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("IconDirectionLight"), mPosition, new Rectangle(0, 0, 64, 64), Color.White);
			this.DrawPartCircel(spriteBatch, mRange, 0, 360, mPosition);
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