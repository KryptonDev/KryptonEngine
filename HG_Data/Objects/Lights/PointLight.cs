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
		protected float mRadius;
		#endregion

		#region Getter & Setter

		public float Radius { get { return mRadius; } set { mRadius = value; } }
		#endregion

		#region Constructor
		public PointLight() : base() { }

		public PointLight(Vector2 pPosition)
			:base(pPosition)
		{
			LightColor = new Vector3(0, 0, 1);
			Intensity = 1.0f;
		}
		#endregion

		#region Override Methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("IconPointLight"), mPosition, new Rectangle(0, 0, 64, 64), Color.White);
			this.DrawPartCircel(spriteBatch, mRadius, 0, 360, mPosition);
		}

		public override string GetInfo()
		{
			string temp;
			temp = base.GetInfo();
			temp += "\nRadius: " + mRadius;
			return temp;
		}
		#endregion

		#region Methods

		public void SetDrawCircle()
		{
			mCircleSize.Clear();

			for (int i = 0; i < 360; i += 2)
			{
				float x = (float)(Position.X + Math.Cos(i * Math.PI / 180) * Radius);
				float y = (float)(Position.Y - Math.Sin(i * Math.PI / 180) * Radius);

				Vector2 pixelpos = new Vector2(x, y) + new Vector2(32, 32);
				mCircleSize.Add(pixelpos);
			}

			mCircleSize.Add(mCircleSize[mCircleSize.Count - 1]);
		}
		#endregion
	}
}
