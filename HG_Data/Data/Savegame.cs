using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HanselAndGretel.Data
{
	public class Savegame
	{
		#region Properties

		public List<Collectable> Collectables;

		public Inventory InventoryHansel;
		public Inventory InventoryGretel;
		public int Chalk;

		public SceneData[] Scenes;

		public Vector2 PositionHansel;
		public Vector2 PositionGretel;

		/// <summary>
		/// Saved Scene to start from.
		/// </summary>
		public int SceneId;

		[XmlIgnoreAttribute]
		protected static string ScenePath;
		[XmlIgnoreAttribute]
		protected static string SavegamePath;
		[XmlIgnoreAttribute]
		protected static XmlSerializer SceneSerializer;
		[XmlIgnoreAttribute]
		protected static XmlSerializer SavegameSerializer;
		[XmlIgnoreAttribute]
		protected static StreamReader xmlReader;
		[XmlIgnoreAttribute]
		protected static StreamWriter xmlWriter;

		#endregion

		#region Constructor

		public Savegame()
		{
			Initialize();
		}

		public Savegame(Savegame pSavegame)
		{
			Initialize();
			Collectables = pSavegame.Collectables;
			InventoryHansel = pSavegame.InventoryHansel;
			InventoryGretel = pSavegame.InventoryGretel;
			Chalk = pSavegame.Chalk;
			PositionHansel = pSavegame.PositionHansel;
			PositionGretel = pSavegame.PositionGretel;
			SceneId = pSavegame.SceneId;
			Scenes = pSavegame.Scenes;
		}

		#endregion

		#region Methods

		public void Initialize()
		{
			ScenePath = Environment.CurrentDirectory + @"\Content\hug";
			SavegamePath = Environment.CurrentDirectory + @"\save.hugs"; //Hänsel Und Gretel Savegame
			SceneSerializer = new XmlSerializer(typeof(SceneData));
			SavegameSerializer = new XmlSerializer(typeof(Savegame));
			Collectables = new List<Collectable>();
			InventoryHansel = new Inventory();
			InventoryGretel = new Inventory();
			Chalk = 0;
			PositionHansel = new Vector2(80, 500); //ToDo: Init Position setzen !---!---!---!---!
			PositionGretel = new Vector2(150, 500); //ToDo: Init Position setzen !---!---!---!---!
			SceneId = 0;
			Scenes = new SceneData[2]; //ToDo: Anzahl Scenes setzen !---!---!---!---!
			for (int i = 0; i < Scenes.Length; i++)
				Scenes[i] = new SceneData(); //Scenes initialisieren
		}

		public static Savegame Load() //Muss static sein damit das Savegame als solches gesetzt werden kann.
		{
			Savegame TmpSavegame;
			FileInfo file = new FileInfo(Savegame.SavegamePath);
			if (!file.Exists)
			{
				TmpSavegame = new Savegame();
				TmpSavegame.Reset();
				Savegame.Save(TmpSavegame);
				return TmpSavegame;
			}
			xmlReader = new StreamReader(Savegame.SavegamePath);
			TmpSavegame = (Savegame)SavegameSerializer.Deserialize(xmlReader); //Savegame aus File laden
			xmlReader.Close();
			
			return TmpSavegame;
		}

		/// <summary>
		/// Für den Editor und für Reset(): Lädt die Scenes in ScenePath in Scenes[].
		/// </summary>
		/// <param name="pLevelId">000 - 999</param>
		public void LoadLevel(int pLevelId)
		{
			Scenes[pLevelId].ResetLevel();
			FileInfo file = new FileInfo(ScenePath + "\\" + LevelNameFromId(pLevelId) + ".hug");
			if (!file.Exists)
				throw new FileNotFoundException("Die Scene {0} existiert nicht! WIESO?!?", LevelNameFromId(pLevelId));
			xmlReader = new StreamReader(file.FullName);
			Scenes[pLevelId] = (SceneData)SceneSerializer.Deserialize(xmlReader); //sData File in SpineData Object umwandeln
			xmlReader.Close();
			Scenes[pLevelId].SetupDeserialized();
		}

		/// <summary>
		/// Speichert pSavegame an pSavegame.SavegamePath.
		/// </summary>
		/// <param name="pSavegame">Savegame, das gesaved werden soll.</param>
		public static void Save(Savegame pSavegame) //Muss static sein damit das Savegame als solches serialisiert werden kann.
		{
			xmlWriter = new StreamWriter(Savegame.SavegamePath);
			SavegameSerializer.Serialize(xmlWriter, pSavegame); //Savegame in File schreiben
			xmlWriter.Close();
		}

		/// <summary>
		/// Für den Editor: Speichert eine Scene als pLevelId.hug
		/// </summary>
		/// <param name="pLevelId">000 - 999</param>
		public void SaveLevel(int pLevelId)
		{
			xmlWriter = new StreamWriter(ScenePath + "\\" + LevelNameFromId(pLevelId) + ".hug");
			SceneSerializer.Serialize(xmlWriter, Scenes[pLevelId]);
			xmlWriter.Close();
		}

		public void Reset()
		{
			Initialize(); //Flush Savegame mit default Werten
			for (int i = 0; i < Scenes.Length; i++)
				LoadLevel(i); //Scenes neu laden
		}

		protected string LevelNameFromId(int pLevelId)
		{
			string LevelName = "";
			for (int i = 0; i < 3 - pLevelId.ToString().Length; i++)
				LevelName += "0";
			LevelName += pLevelId.ToString();
			return LevelName;
		}

		public void SetupDeserialized()
		{
			foreach (SceneData scene in Scenes)
			{
				scene.SetupDeserialized();
			}
		}

		#endregion
	}
}
