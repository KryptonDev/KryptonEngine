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
using KryptonEngine.HG_Data;

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
		public Sprite BackgroundTexture;

		public List<InteractiveObject> InteractiveObjects;
		public List<Collectable> Collectables;
		public List<Item> Items;
		public List<Light> Lights;

		public List<EventTrigger> Events;

		public AmbientLight SceneAmbientLight;
		public DirectionLight SceneDirectionLight;

		public List<Enemy> Enemies;
		//public List<Emitter> Emitter;
		//public List<SoundAreas> SoundAreas;

		[XmlIgnoreAttribute]
		public List<InteractiveObject> RenderList;

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

			BackgroundTexture = new Sprite();

			InteractiveObjects = new List<InteractiveObject>();
			Collectables = new List<Collectable>();
			Items = new List<Item>();
			Enemies = new List<Enemy>();
			Lights = new List<Light>();
			Events = new List<EventTrigger>();

			RenderList = new List<InteractiveObject>();

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
			InteractiveObjects.Clear();
			Collectables.Clear();
			Items.Clear();
			Lights.Clear();
			Events.Clear();
			BackgroundSprites.Clear();
		}

		// Laden Texturen usw. von Manager das nicht mitserialisiert wird
		public void LoadContent(string pBackgroundTextureName)
		{
			BackgroundTexture.TextureName = pBackgroundTextureName;
			BackgroundTexture.LoadContent();
			foreach (InteractiveObject iObj in InteractiveObjects)
			{
				iObj.LoadContent();
				iObj.ApplySettings();
			}
			foreach (Item item in Items)
				item.LoadContent();
			foreach (Collectable col in Collectables)
			{
				col.LoadContent();
				col.ApplySettings();
			}
		}

		public void SetupRenderList(Hansel pHansel, Gretel pGretel)
		{
			RenderList.Clear();
			RenderList.AddRange(InteractiveObjects);
			RenderList.AddRange(Items);
			RenderList.AddRange(Collectables);
			RenderList.AddRange(Enemies);
			RenderList.Add(pHansel);
			RenderList.Add(pGretel);
		}

		#endregion
	}
}
