using KryptonEngine.Interface;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


//******************************************************
// Beim erstellen eines Spine/ InteractiveObject muss folgendes beachtet werden:
// Zuerst wird das Object via Konstruktor normal erstellt.
// Danach muss LoadContent() aufgerufen werden, um die benötigten Texturen zu laden.
// Dann muss ApplySettings aufgerufen werden. Die Funktion sorft dafür, dass das Interactive Object einmal geupdatet wird
// und passt dann die Collisions und ActionListen an.
/*
 *	InteractiveObject io = new InteractiveObject("InteractiveObjectName");
 *	io.LoadContent();
 *	io.ApplySettings();
 */
//******************************************************
namespace KryptonEngine.Entities
{
	public class InteractiveObject : SpineObject
	{
		#region Properties

		protected List<Rectangle> mActionRectList = new List<Rectangle>();
		protected List<Rectangle> mCollisionRectList = new List<Rectangle>();

		protected Vector2 mActionPosition1;
		protected Vector2 mActionPosition2;

		protected int mActionId;

		protected ActivityState mActivityState;
		#endregion

		#region Getter & Setter

		public List<Rectangle> ActionRectList { get { return mActionRectList; } set { mActionRectList = value; } }
		public List<Rectangle> CollisionRectList { get { return mCollisionRectList; } set { mCollisionRectList = value; } }
		public Vector2 ActionPosition1 { get { return mActionPosition1; } set { mActionPosition1 = value; } }
		public Vector2 ActionPosition2 { get { return mActionPosition2; } set { mActionPosition2 = value; } }
		public int ActionId { get { return mActionId; } set { mActionId = value; } }
		public int Height;
		public int Width;

		[XmlIgnoreAttribute]
		public Activity Activity { get { return (Activity)ActionId; } }
		[XmlIgnoreAttribute]
		public List<DrawPackage> DrawPackages { get
		{
			List<DrawPackage> TmpPackages = new List<DrawPackage>();
			//Main Package
			TmpPackages.Add(DrawPackage);
			//Debug Stuff
			foreach (Rectangle rect in CollisionRectList) //Collision Rectangles
				TmpPackages.Add(new DrawPackage(rect, Color.Red));
			foreach (Rectangle rect in ActionRectList) //Action Rectangles
				TmpPackages.Add(new DrawPackage(rect, Color.Violet));
			//Action Positions
			TmpPackages.Add(new DrawPackage(new Rectangle((int)ActionPosition1.X-5, (int)ActionPosition1.Y-5, 10, 10), Color.Blue));
			TmpPackages.Add(new DrawPackage(new Rectangle((int)ActionPosition2.X-5, (int)ActionPosition2.Y-5, 10, 10), Color.Blue));
			return TmpPackages;
		} }
		[XmlIgnoreAttribute]
		public ActivityState ActivityState { get { return mActivityState; } set { mActivityState = value; } }
		#endregion

		#region Constructor
		
		public InteractiveObject() 
			: base() 
		{

		}

		public InteractiveObject(String pName)
			:base(pName)
		{

		}

		#endregion

		#region Methods

		/*public Vector2 GetNearestStartPosition(Vector2 PlayerPosition)
		{
			float Distance1 = Vector2.Distance(PlayerPosition, mActionPosition1);
			float Distance2 = Vector2.Distance(PlayerPosition, mActionPosition2);

			return (Math.Min(Distance1, Distance2) == Distance1) ? mActionPosition1 : mActionPosition2;
		}*/

		public Vector2 NearestActionPosition(Vector2 pPosition)
		{
			return ((ActionPosition1 - pPosition).Length() < (ActionPosition2 - pPosition).Length()) ? ActionPosition1 : ActionPosition2;
		}

		public Vector2 DistantActionPosition(Vector2 pPosition)
		{
			return ((ActionPosition1 - pPosition).Length() > (ActionPosition2 - pPosition).Length()) ? ActionPosition1 : ActionPosition2;
		}

		public void CopyFrom(InteractiveObject io)
		{
			this.ActionRectList = new List<Rectangle>(io.ActionRectList);
			this.CollisionRectList = new List<Rectangle>(io.CollisionRectList);
			this.ActionPosition1 = io.ActionPosition1;
			this.ActionPosition2 = io.ActionPosition2;
			this.ActionId = io.ActionId;

			this.Name = io.Name;
			this.mTextures = new Texture2D[4];
			mTextures[0] = TextureManager.Instance.GetElementByString(Name);
			mTextures[1] = TextureManager.Instance.GetElementByString(Name + "Normal");
			mTextures[2] = TextureManager.Instance.GetElementByString(Name + "AO");
			mTextures[3] = TextureManager.Instance.GetElementByString(Name + "Depth");

			this.Position = io.Position;
		}

		// Muss nach laden der Texture/deserializierung einmalig ausgeführt werden
		public override void ApplySettings()
		{
			base.ApplySettings();
			MoveInteractiveObject(Vector2.Zero);
		}

		public void MoveInteractiveObject(Vector2 mDirection)
		{
			SkeletonPosition += mDirection;

			for (int i = 0; i < mActionRectList.Count; i++)
			{
				Rectangle temp = mActionRectList[i];
				temp.X += (int)(SkeletonPosition.X);
				temp.Y += (int)(SkeletonPosition.Y);
			}

			for (int i = 0; i < mCollisionRectList.Count; i++)
			{
				Rectangle temp = mCollisionRectList[i];
				temp.X += (int)(SkeletonPosition.X);
				temp.Y += (int)(SkeletonPosition.Y);
			}
		}
		#endregion
	}
}
