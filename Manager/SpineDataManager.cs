///**************************************************************
// * (c) Jens Richter 2014
// *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using KryptonEngine.Entities;
using System.IO;
using System.Xml.Serialization;


namespace KryptonEngine.Manager
{
	public class SpineDataManager : Manager<RawSpineData>
	{
		private struct AnimationMix
		{
			#region Properties

			private string from;
			private string to;
			private float fading;

			public string From { get { return from; } set { from = value; } }
			public string To { get { return to; } set { to = value; } }
			public float Fading { get { return fading; } set { fading = Math.Abs(value); } }

			#endregion

			#region Constructor

			public AnimationMix(string pFrom, string pTo)
			{
				from = pFrom;
				to = pTo;
				fading = DefaultFading;
			}

			public AnimationMix(string pFrom, string pTo, float pFading)
			{
				from = pFrom;
				to = pTo;
				fading = pFading;
			}

			#endregion
		}

	#region Singleton

	private static SpineDataManager mInstance;
	public static SpineDataManager Instance { get { if (mInstance == null) mInstance = new SpineDataManager(); return mInstance; } }

	#endregion

	#region Properties

	private static Dictionary<String, List<AnimationMix>> AnimationFading = new Dictionary<string, List<AnimationMix>>();
	private static Dictionary<String, float> Scaling = new Dictionary<string, float>();

	public static float DefaultFading = 0.2f;
	public static bool PremultipliedAlphaRendering = true;
	public static string DefaultDataPath = "Content/spine/";

	#endregion

	#region Constructor

	SpineDataManager()
	{
	}

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
		Add("fluffy");


		List<AnimationMix> AnimationFadingList; //Zu bearbeitende Liste, damit die nicht immer neu im Dictionary nachgeschlagen werden muss

		#region Fluffy
		Scaling.Add("fluffy", 1.0f);
		AnimationFading.Add("fluffy", new List<AnimationMix>());
		AnimationFadingList = AnimationFading["fluffy"];

		AnimationFadingList.Add(new AnimationMix("attack", "die"));
		AnimationFadingList.Add(new AnimationMix("attack", "die"));
		AnimationFadingList.Add(new AnimationMix("attack", "smash_die"));
		AnimationFadingList.Add(new AnimationMix("attack", "idle"));
		AnimationFadingList.Add(new AnimationMix("attack", "walk"));

		AnimationFadingList.Add(new AnimationMix("idle", "die"));
		AnimationFadingList.Add(new AnimationMix("idle", "smash_die"));
		AnimationFadingList.Add(new AnimationMix("idle", "attack"));
		AnimationFadingList.Add(new AnimationMix("idle", "walk"));

		AnimationFadingList.Add(new AnimationMix("walk", "die"));
		AnimationFadingList.Add(new AnimationMix("walk", "smash_die"));
		AnimationFadingList.Add(new AnimationMix("walk", "attack"));
		AnimationFadingList.Add(new AnimationMix("walk", "idle"));
		#endregion

		#region Skeleton XY
		//Scaling.Add("skeleton", 1.0f);
		//AnimationFading.Add("skeleton", new List<AnimationMix>());
		//AnimationFadingList = AnimationFading["skeleton"];

		//AnimationFadingList.Add(new AnimationMix("attack", "die"));
		//AnimationFadingList.Add(new AnimationMix("attack", "die"));
		//AnimationFadingList.Add(new AnimationMix("attack", "smash_die"));
		//AnimationFadingList.Add(new AnimationMix("attack", "idle"));
		//AnimationFadingList.Add(new AnimationMix("attack", "walk"));
		//usw...
		#endregion

	}

	public override void Unload()
	{
		mRessourcen.Clear();
	}

	/// <summary>
	/// Fügt ein neues Element in mRessourcenManager ein.
	/// </summary>
	/// <param name="pName">Name des Skeletons.</param>
	/// <param name="pPath">Pfad zu den Skeleton-Daten.</param>
	public override RawSpineData Add(String pName, String pPath)
	{
		if (!mRessourcen.ContainsKey(pName))
		{
		RawSpineData data = new RawSpineData(pPath, pName);

		mRessourcen.Add(pName, data);

		return data;
		}
    
		return (RawSpineData)mRessourcen[pName];
	}

	/// <summary>
	/// Fügt ein neues Element in mRessourcenManager ein.
	/// </summary>
	/// <param name="pName">Name des Skeletons.</param>
	public void Add(String pName)
	{
		if (!mRessourcen.ContainsKey(pName))
		{
		RawSpineData data = new RawSpineData(pName);
		mRessourcen.Add(pName, data);
		}
	}

	/// <summary>
	/// Gibt RawSpineData zurück.
	/// </summary>
	public override RawSpineData GetElementByString(string pElementName)
	{
		if (mRessourcen.ContainsKey(pElementName))
		return mRessourcen[pElementName];

		throw new ArgumentException("Element not found!");
	}

	public Skeleton NewSkeleton(string pName, float pScale)
	{
		RawSpineData TmpSpineData = this.GetElementByString(pName);
		TmpSpineData.json.Scale = Scaling[pName]; //Set Scaling
		SkeletonData TmpSkeletonData = TmpSpineData.json.ReadSkeletonData(DefaultDataPath + pName + ".json"); //Apply Json with Scaling to get skelData
		return new Skeleton(TmpSkeletonData);
	}

	public AnimationState NewAnimationState(SkeletonData pSkeletonData)
	{
		AnimationStateData TmpAnimationStateData = new AnimationStateData(pSkeletonData);
		SetFadingSettings(TmpAnimationStateData);//Set mixing between animations
		return new AnimationState(TmpAnimationStateData);
	}

		#region SpineSettings

	/// <summary>
	/// Wendet alle zum Skeleton passenden AnimationMixes auf animationStateData an.
	/// </summary>
	private static void SetFadingSettings(AnimationStateData pAnimationStateData)
	{
		List<AnimationMix> AnimationFadingList;
		AnimationFadingList = AnimationFading[pAnimationStateData.SkeletonData.Name];

		foreach (AnimationMix animMix in AnimationFadingList)
		{
			pAnimationStateData.SetMix(animMix.From, animMix.To, animMix.Fading);
		}
	}

		#endregion

	#endregion
	}
}
