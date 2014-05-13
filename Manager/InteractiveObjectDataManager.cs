using KryptonEngine.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KryptonEngine.Manager
{
  public class InteractiveObjectDataManager : Manager<InteractiveObject>
  {
        #region Singleton

        private static InteractiveObjectDataManager mInstance;
        public static InteractiveObjectDataManager Instance { get { if (mInstance == null) mInstance = new InteractiveObjectDataManager(); return mInstance; } }

        #endregion

        #region Constructor

        InteractiveObjectDataManager() { }

        #endregion

        #region Methods

        public override void LoadContent()
        {
          InteractiveObject iObj = new InteractiveObject();
          XmlSerializer xml = new XmlSerializer(typeof(InteractiveObject));
          TextReader reader;

          DirectoryInfo environmentPath = new DirectoryInfo(Environment.CurrentDirectory + @"\Content\iObj\");
		  if (!environmentPath.Exists)
			  return;
          foreach (FileInfo f in environmentPath.GetFiles())
          {
            if (f.Name.EndsWith(".iObj"))
            {
              reader = new StreamReader(f.FullName);
              iObj = (InteractiveObject)xml.Deserialize(reader);
              iObj.Texture = TextureManager.Instance.GetElementByString(iObj.TextureName);
              reader.Close();

              mRessourcen.Add(iObj.TextureName, iObj);
            }
          }
        }

        /// <summary>
        /// Fügt ein neues Element in mRessourcenManager ein.
        /// </summary>
        /// 
        /// <param name="pName">ID der Texture für den Zugriff.</param>
        /// <param name="pPath">Pfad zur Texture.</param>
        public override InteractiveObject Add(String pName, String pPath)
        {
            if (!mRessourcen.ContainsKey(pName))
            {
              InteractiveObject iObj = EngineSettings.Content.Load<InteractiveObject>(pPath);
              mRessourcen.Add(pName, iObj);

              return iObj;
            }

            return (InteractiveObject)mRessourcen[pName];
        }

        /// <summary>
        /// Gibt eine Texture2D zurück.
        /// </summary>
        public override InteractiveObject GetElementByString(string pElementName)
        {
			if (mRessourcen.ContainsKey(pElementName))
			{
				InteractiveObject io = new InteractiveObject();
				io.CopyFrom(mRessourcen[pElementName]);
				return io;
			}

            throw new ArgumentException("Element not found!");
        }

        public override void Unload()
        {
            mRessourcen.Clear();
        }

		public bool HasElement(String pElementName)
		{
			if (mRessourcen.ContainsKey(pElementName))
				return true;
			else
				return false;
		}
        #endregion
    }
}

