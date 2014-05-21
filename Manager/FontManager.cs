/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace KryptonEngine.Manager
{
    public class FontManager : Manager<SpriteFont>
    {
        #region Singleton

        private static FontManager mInstance;
        public static FontManager Instance { get { if (mInstance == null) mInstance = new FontManager(); return mInstance; } }

        #endregion

        #region Constructor

        FontManager() { }

        #endregion

        #region Methods

        public override void LoadContent()
        {
			SpriteFont font;

			DirectoryInfo environmentPath = new DirectoryInfo(Environment.CurrentDirectory + @"\Content\font\");

			if (!environmentPath.Exists)
				return;

			foreach (FileInfo f in environmentPath.GetFiles())
			{
				if (f.Name.Contains(".xnb"))
				{
					string fileName = f.Name.Substring(0, f.Name.Length - 4);
					font = EngineSettings.Content.Load<SpriteFont>(@"font\" + fileName);
					mRessourcen.Add(fileName, font);
				}
			}
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// 
        /// <param name="pName">ID der Texture für den Zugriff.</param>
        /// <param name="pPath">Pfad zur Texture.</param>
        public override SpriteFont Add(String pName, String pPath)
        {
            SpriteFont font;
            if (!mRessourcen.ContainsKey(pName))
            {
                font = EngineSettings.Content.Load<SpriteFont>(pPath);
                mRessourcen.Add(pName, font);
            }
            else
                font = (SpriteFont)mRessourcen[pName];

            return font;
        }

        /// <summary>
        /// Gibt eine Texture2D zurück.
        /// </summary>
        public override SpriteFont GetElementByString(string pElementName)
        {
            if (mRessourcen.ContainsKey(pElementName))
                return mRessourcen[pElementName];

            throw new ArgumentException("Element not found!");
        }


        public override void Unload()
        {
            mRessourcen.Clear();
        }

        #endregion
    }
}
