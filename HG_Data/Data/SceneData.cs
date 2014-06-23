using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KryptonEngine.Manager;
using System.Xml.Serialization;
using System.IO;
using KryptonEngine;
using KryptonEngine.HG_Data.Objects.Lights;

namespace HanselAndGretel.Data
{
	public enum SoundSetting
	{
		Forest,
		Mountain,
		Swamp,
		Inside
	}

	public class SceneData
	{
		#region Properties

		public Rectangle GamePlane; // Damit die Camera weiß in welchem Bereich sie sich bewegen darf. 
		public SoundSetting BackgroundSoundSetting;

		public List<Rectangle> MoveArea;
		public List<Waypoint> Waypoints;

		public List<GameObject> BackgroundSprites;
		[XmlIgnoreAttribute]
		public Texture2D[] BackgroundTextures;

		public List<InteractiveObject> InteractiveObjects;
		public List<Collectable> Collectables;
		public List<Item> Items;
		public List<Light> Lights;

		public List<EventTrigger> Events;

		public AmbientLight SceneAmbientLight;
		public DirectionLight SceneDirectionLight;

		[XmlIgnoreAttribute]
		public List<Enemy> Enemies;
		//public List<Emitter> Emitter;
		//public List<SoundAreas> SoundAreas;

		#endregion

		#region Getter & Setter

		[XmlIgnoreAttribute]
		public List<DrawPackage> DrawPackages
		{
			get
			{
				List<DrawPackage> TmpList = new List<DrawPackage>();
				foreach (GameObject go in BackgroundSprites)
				{
					TmpList.Add(new DrawPackage(go.CollisionBox, Color.White));
				}
				foreach (InteractiveObject iObj in InteractiveObjects)
				{
					TmpList.AddRange(iObj.DrawPackages);
				}
				foreach (Rectangle rect in MoveArea)
				{
					TmpList.Add(new DrawPackage(rect, Color.Blue));
				}
				foreach (Waypoint wp in Waypoints)
				{
					TmpList.Add(wp.DrawPackage);
				}
				foreach (Item item in Items)
				{
					TmpList.Add(item.DrawPackage);
				}
				//Add EVERYTHING for Debug
				//foreach (InteractiveObject obj in InteractiveObjects)
				//{
				//	TmpList.Add(obj.DrawPackage);
				//}
				return TmpList;
			}
		}

		#endregion

		#region Constructor

		// Wird für die Serializierung benötigt
		public SceneData()
		{
			Initialize();
		}
		#endregion

		#region Methods

		public void Initialize()
		{
			GamePlane = Rectangle.Empty;
			MoveArea = new List<Rectangle>();
			Waypoints = new List<Waypoint>();
			BackgroundSprites = new List<GameObject>();
			BackgroundTextures = new Texture2D[4]; //LightMaps

			InteractiveObjects = new List<InteractiveObject>();
			Collectables = new List<Collectable>();
			Items = new List<Item>();
			Enemies = new List<Enemy>();
			Lights = new List<Light>();
			Events = new List<EventTrigger>();

			InteractiveObjects = new List<InteractiveObject>();

			SceneAmbientLight = new AmbientLight();
			SceneDirectionLight = new DirectionLight();
		}

		/// <summary>
		/// Leert alle Listen.
		/// </summary>
		public void ResetLevel()
		{
			MoveArea.Clear();
			Waypoints.Clear();
			BackgroundSprites.Clear();
			InteractiveObjects.Clear();
			Collectables.Clear();
			Items.Clear();
			Lights.Clear();
			Events.Clear();
		}

		// Laden Texturen usw. von Manager das nicht mitserialisiert wird
		public void SetupDeserialized()
		{
			foreach (InteractiveObject iObj in InteractiveObjects)
				iObj.SetupDeserialized();
			foreach (Item item in Items)
				item.LoadTextures();
			foreach (Collectable col in Collectables)
				col.LoadTextures();
		}
		#endregion
	}
}
