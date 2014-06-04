/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.SceneManagement;
using Microsoft.Xna.Framework;
using System.IO;

namespace KryptonEngine.Manager
{
	public class TextureManager : Manager<Texture2D>
	{
		#region Singleton

		private static TextureManager mInstance;
		public static TextureManager Instance { get { if (mInstance == null) mInstance = new TextureManager(); return mInstance; } }

		#endregion

		#region Constructor

		TextureManager() { }

		#endregion

		#region Methods

		public override void LoadContent()
		{
			Texture2D tex;

			if (!mRessourcen.ContainsKey("pixel"))
			{
				tex = new Texture2D(EngineSettings.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
				tex.SetData<Color>(new Color[] { Color.White });
				mRessourcen.Add("pixel", tex);
			}

			DirectoryInfo environmentPath = new DirectoryInfo(Environment.CurrentDirectory + @"\Content\gfx\");

			if (!environmentPath.Exists)
				return;

			foreach (FileInfo f in environmentPath.GetFiles())
			{
				if (f.Name.Contains(".xnb"))
				{
					string fileName = f.Name.Substring(0, f.Name.Length - 4);
					tex = EngineSettings.Content.Load<Texture2D>(@"gfx\" + fileName);
					mRessourcen.Add(fileName, tex);
				}
			}
		}

		/// <summary>
		/// Fügt ein neues Element in mRessourcenManager ein.
		/// </summary>
		/// 
		/// <param name="pName">ID der Texture für den Zugriff.</param>
		/// <param name="pPath">Pfad zur Texture.</param>
		public override Texture2D Add(String pName, String pPath)
		{
			if (!mRessourcen.ContainsKey(pName))
			{
				Texture2D tex = EngineSettings.Content.Load<Texture2D>(pPath);
				mRessourcen.Add(pName, tex);

				return tex;
			}

			return (Texture2D)mRessourcen[pName];
		}

		/// <summary>
		/// Gibt eine Texture2D zurück.
		/// </summary>
		public override Texture2D GetElementByString(string pElementName)
		{
			if (mRessourcen.ContainsKey(pElementName))
				return mRessourcen[pElementName];

			return null;
		}

		public override void Unload()
		{
			mRessourcen.Clear();
		}

		public Dictionary<String, Texture2D> GetAllGameEntities()
		{
			Dictionary<String, Texture2D> result = new Dictionary<String, Texture2D>();

			foreach (string num in this.mRessourcen.Keys)
			{
				if (num.IndexOf("Engine") == -1)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		public Dictionary<String, Texture2D> GetAllEntitiesByName(String pName)
		{
			Dictionary<String, Texture2D> result = new Dictionary<String, Texture2D>();

			foreach (string num in this.mRessourcen.Keys)
			{
				if (num.IndexOf(pName) != -1)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		public Dictionary<String, Texture2D> GetAllEntetiesWithout(List<String> pStringlist)
		{
			Dictionary<String, Texture2D> result = new Dictionary<string, Texture2D>();

			foreach(string num in this.mRessourcen.Keys)
			{
				bool found = false;
				foreach(String s in pStringlist)
				{
					if(num.IndexOf(s) != -1)
					{
						found = true;
						//result.Add(num, this.mRessourcen[num]);
						break;
					}
				}
				if(!found)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		#endregion
	}
}
