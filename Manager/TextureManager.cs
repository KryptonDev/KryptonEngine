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
          foreach (FileInfo f in environmentPath.GetFiles())
          {
            string fileName = f.Name.Substring(0, f.Name.Length - 4);
            tex = EngineSettings.Content.Load<Texture2D>(@"gfx\" + fileName);
              mRessourcen.Add(fileName, tex);
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

            throw new ArgumentException("Element not found!");
        }

        public override void Unload()
        {
            mRessourcen.Clear();
        }

        public Dictionary<String, Texture2D> GetAllEntities()
        {
            Dictionary<String, Texture2D> result = new Dictionary<String, Texture2D>();

            foreach(string num in this.mRessourcen.Keys)
            {
              if(num.IndexOf("Engine") == -1)
                result.Add(num, this.mRessourcen[num]);

            }

            return result;
        }

        #endregion
    }
}
