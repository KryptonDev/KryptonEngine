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

		protected List<Vector2> mCircleSize;
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
			mCircleSize = new List<Vector2>();
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

		public void SetDrawCircle()
		{
			mCircleSize.Clear();

			for(int i = 0; i < 360; i += 2)
			{
				float x = (float)(Position.X + Math.Cos(i * Math.PI / 180) * Range);
				float y = (float)(Position.Y - Math.Sin(i * Math.PI / 180) * Range);

				Vector2 pixelpos = new Vector2(x, y) + new Vector2(32, 32);
				mCircleSize.Add(pixelpos);
			}

			mCircleSize.Add(mCircleSize[mCircleSize.Count - 1]);
		}

		protected void DrawPartCircel(SpriteBatch spriteBatch,float radius, float startAngel, float endAngel, Vector2 pos)
		{

			foreach(Vector2 v in mCircleSize)
				spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), v, Color.Yellow);

			for(int i = 0; i < mCircleSize.Count - 1; i++)
				DrawLine(mCircleSize[i], mCircleSize[i + 1], TextureManager.Instance.GetElementByString("pixel"), 1.0f, spriteBatch);
		}

		protected void DrawLine(Vector2 from, Vector2 to, Texture2D texture, float size, SpriteBatch spriteBatch)
		{

			double degress = Math.Atan2((to - from).Y, (to - from).X);
			float length = Vector2.Distance(from, to);
			spriteBatch.Draw(texture, from, new Rectangle(0, 0, 1, 1), Color.White, (float)degress, Vector2.Zero, new Vector2(length, size), SpriteEffects.None, 0);

		}
		#endregion
	}
}
