using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KryptonEngine.Manager
{
	public class ShaderManager : Manager<Effect>
	{
		#region Singleton

		private static ShaderManager mInstance;
		public static ShaderManager Instance { get { if (mInstance == null) mInstance = new ShaderManager(); return mInstance; } }

		#endregion

		#region Constructor

		ShaderManager() { }

		#endregion

		#region Methods

		public override void LoadContent()
		{
			Effect ef;

			DirectoryInfo environmentPath = new DirectoryInfo(Environment.CurrentDirectory + @"\Content\cfx\");

			if (!environmentPath.Exists)
				return;

			foreach (FileInfo f in environmentPath.GetFiles())
			{
				if (f.Name.Contains(".xnb"))
				{
					string fileName = f.Name.Substring(0, f.Name.Length - 4);
					ef = EngineSettings.Content.Load<Effect>(@"cfx\" + fileName);
					mRessourcen.Add(fileName, ef);
				}
			}
		}

		/// <summary>
		/// Fügt ein neues Element in mRessourcenManager ein.
		/// </summary>
		/// 
		/// <param name="pName">ID der Texture für den Zugriff.</param>
		/// <param name="pPath">Pfad zur Texture.</param>
		public override Effect Add(String pName, String pPath)
		{
			if (!mRessourcen.ContainsKey(pName))
			{
				Effect ef = EngineSettings.Content.Load<Effect>(pPath);
				mRessourcen.Add(pName, ef);

				return ef;
			}

			return (Effect)mRessourcen[pName];
		}

		/// <summary>
		/// Gibt eine Texture2D zurück.
		/// </summary>
		public override Effect GetElementByString(string pElementName)
		{
			if (mRessourcen.ContainsKey(pElementName))
				return mRessourcen[pElementName];

			return null;
		}

		public override void Unload()
		{
			mRessourcen.Clear();
		}

		public Dictionary<String, Effect> GetAllGameEntities()
		{
			Dictionary<String, Effect> result = new Dictionary<String, Effect>();

			foreach (string num in this.mRessourcen.Keys)
			{
				if (num.IndexOf("Engine") == -1)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		public Dictionary<String, Effect> GetAllEntitiesByName(String pName)
		{
			Dictionary<String, Effect> result = new Dictionary<String, Effect>();

			foreach (string num in this.mRessourcen.Keys)
			{
				if (num.IndexOf(pName) != -1)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		public Dictionary<String, Effect> GetAllEntetiesWithout(List<String> pStringlist)
		{
			Dictionary<String, Effect> result = new Dictionary<string, Effect>();

			foreach (string num in this.mRessourcen.Keys)
			{
				bool found = false;
				foreach (String s in pStringlist)
				{
					if (num.IndexOf(s) != -1)
					{
						found = true;
						//result.Add(num, this.mRessourcen[num]);
						break;
					}
				}
				if (!found)
					result.Add(num, this.mRessourcen[num]);

			}
			return result;
		}

		#endregion
	}
}
