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
	public class InteractiveObject : GameObject
	{
		#region Properties

		protected List<Rectangle> mActionRectList = new List<Rectangle>();
		protected List<Rectangle> mCollisionRectList = new List<Rectangle>();

		protected Vector2 mActionPosition1;
		protected Vector2 mActionPosition2;

		protected int mActionId;
		protected Texture2D mTexture;
		protected String mTextureName;

		protected ActivityState mActivityState;
		#endregion

		#region Getter & Setter

		public List<Rectangle> ActionRectList { get { return mActionRectList; } set { mActionRectList = value; } }
		public List<Rectangle> CollisionRectList { get { return mCollisionRectList; } set { mCollisionRectList = value; } }
		public Vector2 ActionPosition1 { get { return mActionPosition1; } set { mActionPosition1 = value; } }
		public Vector2 ActionPosition2 { get { return mActionPosition2; } set { mActionPosition2 = value; } }
		public int ActionId { get { return mActionId; } set { mActionId = value; } }
		[XmlIgnoreAttribute]
		public Activity Activity { get { return (Activity)ActionId; } }
		[XmlIgnoreAttribute]
		public Texture2D Texture { get { return mTexture; } set { mTexture = value; } }
		public String TextureName { get { return mTextureName; } set { mTextureName = value; } }
		[XmlIgnoreAttribute]
		public List<DrawPackage> DrawPackages { get
		{
			List<DrawPackage> TmpPackages = new List<DrawPackage>();
			//Main Package
			//TmpPackages.Add(new DrawPackage(Position, DrawZ, CollisionBox, mDebugColor, TextureManager.Instance.GetElementByString("pixel")));
			TmpPackages.Add(new DrawPackage(Position, DrawZ, new Rectangle(CollisionBox.Center.X - 100, CollisionBox.Center.Y - 100, 200, 200), mDebugColor, TextureManager.Instance.GetElementByString("pixel")));
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
		
		public InteractiveObject() 
			: base() 
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			mDropDown = new DropDownMenu(Vector2.Zero,
				new List<String>() { "Activity: None", "Activity: CaughtInCobweb",  "Activity: CaughtInSwamp",
								"Activity: KnockOverTree", "Activity: BalanceOverTree", "Activity: PushRock", "Activity: SlipThroughRock", "Activity: Crawl", "Activity: JumpOverGap",
								"Activity: LegUp", "Activity: LegUpGrab", "Activity: UseKey", "Activity: PullDoor", "Activity: UseChalk", "Activity: UseWell" },
				new List<Action>() { SetActivityState0, SetActivityState1, SetActivityState3, SetActivityState5, SetActivityState6, SetActivityState7,
								SetActivityState8,SetActivityState9,SetActivityState10,SetActivityState11,SetActivityState12,SetActivityState13,SetActivityState14,
								SetActivityState15,SetActivityState16,SetActivityState17});
		}

		public override void Update()
		{
			mDropDown.Update();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (mTexture != null)
			{
				spriteBatch.Draw(mTexture, Position, Color.White);
				if (EngineSettings.IsDebug)
				{
					foreach (Rectangle r in ActionRectList)
						spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), r, Color.Yellow);
					foreach (Rectangle r in CollisionRectList)
						spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), r, Color.Blue);
					spriteBatch.Draw(TextureManager.Instance.GetElementByString("pixel"), new Rectangle(PositionX, DrawZ, mTexture.Width, 1), Color.Red);
				}
			}
		}
		#endregion

		#region Methods

		public Vector2 GetNearestStartPosition(Vector2 PlayerPosition)
		{
			float Distance1 = Vector2.Distance(PlayerPosition, mActionPosition1);
			float Distance2 = Vector2.Distance(PlayerPosition, mActionPosition2);

			return (Math.Min(Distance1, Distance2) == Distance1) ? mActionPosition1 : mActionPosition2;
		}

		public void SetupForDeserialization()
		{
			Texture = TextureManager.Instance.GetElementByString(TextureName);
		}

		public void CopyFrom(InteractiveObject io)
		{
			this.ActionRectList = new List<Rectangle>(io.ActionRectList);
			this.CollisionRectList = new List<Rectangle>(io.CollisionRectList);
			this.ActionPosition1 = io.ActionPosition1;
			this.ActionPosition2 = io.ActionPosition2;
			this.DrawZ = io.DrawZ;
			this.ActionId = io.ActionId;
			this.mTexture = io.Texture;
			this.mTextureName = io.TextureName;

			this.Position = io.Position;
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
				case 9: actID = "Crawl";
					break;
				case 10: actID = "JumpOverGap";
					break;
				case 11: actID = "LegUp";
					break;
				case 12: actID = "LegUpGrab";
					break;
				case 13: actID = "UseKey";
					break;
				case 14: actID = "PullDoor";
					break;
				case 15: actID = "UseChalk";
					break;
				case 16: actID = "UseWell";
					break;
			}
			tmp += "\nActionID: " + actID;
			return tmp;
		}

		#region DropDownMenu
		private void SetActivityState0()
		{
			mActionId = 0;
		}

		private void SetActivityState1()
		{
			mActionId = 1;
		}

		private void SetActivityState2()
		{
			mActionId = 2;
		}

		private void SetActivityState3()
		{
			mActionId = 3;
		}

		private void SetActivityState4()
		{
			mActionId = 4;
		}

		private void SetActivityState5()
		{
			mActionId = 5;
		}

		private void SetActivityState6()
		{
			mActionId = 6;
		}

		private void SetActivityState7()
		{
			mActionId = 7;
		}

		private void SetActivityState8()
		{
			mActionId = 8;
		}

		private void SetActivityState9()
		{
			mActionId = 9;
		}

		private void SetActivityState10()
		{
			mActionId = 10;
		}

		private void SetActivityState11()
		{
			mActionId = 11;
		}

		private void SetActivityState12()
		{
			mActionId = 12;
		}

		private void SetActivityState13()
		{
			mActionId = 13;
		}

		private void SetActivityState14()
		{
			mActionId = 14;
		}

		private void SetActivityState15()
		{
			mActionId = 15;
		}

		private void SetActivityState16()
		{
			mActionId = 16;
		}

		private void SetActivityState17()
		{
			mActionId = 17;
		}

		#endregion
		#endregion
	}
}
