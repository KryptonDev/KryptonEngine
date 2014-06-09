using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Witch : Enemy
	{
		#region Properties

		#endregion

		#region Getter & Setter

		#endregion

		#region Constructor

		public Witch(Vector2 pPosition)
			:base(pPosition)
		{

		}

		public Witch()
			:base()
		{

		}

		#endregion

		#region OverrideMethods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("EnemyWitch"), mPosition, Color.White);
		}
		#endregion

		#region Methods

		#endregion
	}
}
