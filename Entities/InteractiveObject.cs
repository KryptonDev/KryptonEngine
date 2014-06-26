using KryptonEngine.Interface;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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

		#region Redirect Position to CollisionBox

		//Zur Verschiebung bei z.B. PushRock.

		[XmlIgnoreAttribute]
		new public Vector2 Position
		{
			set
			{
				mPosition = value;
				for (int i = 0; i < mCollisionRectList.Count; ++i)
					mCollisionRectList[i] = new Rectangle((int)value.X, (int)value.Y, mCollisionRectList[i].Width, mCollisionRectList[i].Height);
			}
			get { return mPosition; }
		}
		[XmlIgnoreAttribute]
		new public int PositionX
		{
			set
			{
				mPosition.X = value;
				for (int i = 0; i < mCollisionRectList.Count; ++i)
					mCollisionRectList[i] = new Rectangle(value, mCollisionRectList[i].Y, mCollisionRectList[i].Width, mCollisionRectList[i].Height);
			}
			get { return (int)mPosition.X; }
		}
		[XmlIgnoreAttribute]
		new public int PositionY
		{
			set
			{
				mPosition.Y = value;
				for (int i = 0; i < mCollisionRectList.Count; ++i)
					mCollisionRectList[i] = new Rectangle(mCollisionRectList[i].X, value, mCollisionRectList[i].Width, mCollisionRectList[i].Height);
			}
			get { return (int)mPosition.Y; }
		}

		#endregion

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
		public ActivityState ActivityState { get { return mActivityState; } set { mActivityState = value; } }

		#endregion

		#region Constructor

		public InteractiveObject() : base() { }

		public InteractiveObject(string pName) 
			: base(pName) 
		{
			Initialize();
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
			base.CopyFrom(io);
			this.ActionRectList = new List<Rectangle>(io.ActionRectList);
			this.CollisionRectList = new List<Rectangle>(io.CollisionRectList);
			this.ActionPosition1 = io.ActionPosition1;
			this.ActionPosition2 = io.ActionPosition2;
			this.ActionId = io.ActionId;
		}

		public override string GetInfo()
		{
			String tmp = base.GetInfo();
			String actID = "";
			switch(mActionId)
			{
				case 0: actID = "None";
					break;
				case 1: actID = "CaughtInCobweb";
					break;
				case 3: actID = "CaughtInSwamp";
					break;
				case 5: actID = "KnockOverTree";
					break;
				case 6: actID = "BalanceOverTree";
					break;
				case 7: actID = "PushRock";
					break;
				case 8: actID = "SlipThroughRock";
					break;
				case 9: actID = "JumpOverGap";
					break;
				case 10: actID = "LegUp";
					break;
				case 11: actID = "LegUpGrab";
					break;
				case 12: actID = "UseKey";
					break;
				case 13: actID = "PullDoor";
					break;
				case 14: actID = "UseChalk";
					break;
				case 15: actID = "UseWell";
					break;
			}
			tmp += "\nActionID: " + actID;
			return tmp;
		}

		#endregion
	}
}
