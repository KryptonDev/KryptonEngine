using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Wolf : Enemy
	{
		#region Properties

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public Wolf(Vector2 pPosition)
			:base(pPosition)
		{

		}

		public Wolf()
			:base()
		{

		}

		#endregion

		#region OverrideMethods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("EnemyWolf"), mPosition, Color.White);
		}
		#endregion

		#region Methods

		#endregion
	}
}
