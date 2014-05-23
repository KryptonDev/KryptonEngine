using KryptonEngine.Entities;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HanselAndGretel.Data
{
	[Serializable, XmlInclude(typeof(DirectionLight)), XmlInclude(typeof(PointLight)), XmlInclude(typeof(SpotLight))]
	public class Light : GameObject
	{		
		#region Properties

		protected float mIntensity;
		protected float mRange;
		protected float mDepth;
		protected Color mColor;
		#endregion

		#region Getter & Setter

		public float Intensity { get { return mIntensity; } set { mIntensity = value; } }
		public float Range { get { return mRange; } set { mRange = value; } }
		public float Depth { get { return mDepth; } set {mDepth = value;} }
		public Color LightColor { get { return mColor; } set { mColor = value; } }
		#endregion

		#region Constructor

		public Light() : base()
		{

		}

		public Light(Vector2 pPositon)
			: base(pPositon)
		{
			// 64x64 Icongroße Hitbox
			mCollisionBox = new Rectangle((int)pPositon.X, (int)pPositon.Y, 64, 64);
		}
		#endregion

		#region Override Methods

		public override string GetInfo()
		{
			string temp;
			temp = base.GetInfo();
			temp += "\nRange: " + mRange;
			return temp;
		}
		#endregion

		#region Methods
		protected virtual void DrawPartCircel(SpriteBatch spriteBatch,float radius, float startAngel, float endAngel, Vector2 pos)
		{

			for (float i = startAngel; i <= endAngel; i += 0.1f)
			{
				float x = (float)(pos.X + Math.Cos(i * Math.PI / 180) * radius);
				float y = (float)(pos.Y - Math.Sin(i * Math.PI / 180) * radius);

				Vector2 pixelpos = new Vector2(x, y) + new Vector2(32,32);
				spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), pixelpos, Color.Yellow);
			}
		}
		#endregion
	}
}
