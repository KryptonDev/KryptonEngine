using KryptonEngine.Entities;
using KryptonEngine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HanselAndGretel.Data
{
	[Serializable]
	public class Collectable : InteractiveObject
	{
		#region Properties

		protected int mCollectableId;
		protected String mShowTextureName;
		protected Texture2D mShowTexture;
		protected bool mShowDescription;
		protected bool mIsHidden;

		#endregion

		#region Getter & Setter

		// Zur auslese welche Daten das Collectable hat z.b. welche DiarySeite oder welches Spielzeug
		public int CollectableId { get { return mCollectableId; } set { mCollectableId = value; } }
		// Anzeige der Item beschreibung in einer 1280x720 Texture;
		[XmlIgnoreAttribute]
		public Texture2D ShowTexture { get { return mShowTexture; } set { mShowTexture = value; } }
		public String ShowTextureName { get { return mShowTextureName; } set { mShowTextureName = value; } }
		// Überprüf Variable ob Beschreibung angezeigt wird
		[XmlIgnoreAttribute]
		public bool ShowDescription { get { return mShowDescription; } set { mShowDescription = value; } }
		public bool IsHidden { get { return mIsHidden; } set { mIsHidden = value; } }

		#endregion

		#region Constructor

		public Collectable() : base() { }

		public Collectable(string pName)
			:base(pName)
		{
		}

		public Collectable(String pTextureName, Vector2 pPosition)
			:base(pTextureName)
		{
			this.Position = pPosition;
		}

		#endregion

		#region OverrideMethods

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Textures[0], Position, Color.White);
			if (this.ShowDescription)
				spriteBatch.Draw(ShowTexture, Vector2.Zero, Color.White);

		}
		#endregion

		#region Methods

		#endregion
	}
}
