using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class PointLight : Light
	{
		#region Properties
		#endregion

		#region Getter & Setter
		#endregion

		#region Constructor
		public PointLight() : base() { }

		public PointLight(Vector2 pPosition)
			:base(pPosition)
		{

		}
		#endregion

		#region Override Methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("IconPointLight"), mPosition, new Rectangle(0, 0, 64, 64), Color.White);
		}
		#endregion

		#region Methods
		#endregion
	}
}
