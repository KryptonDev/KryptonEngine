using KryptonEngine.Entities;
using KryptonEngine.Interface;
using KryptonEngine.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Waypoint : GameObject
	{
		#region Properties

		protected int mDestinationSceneId;
		protected int mDesinationWaypointId;
		protected bool mOneWay;
		protected const float mLeaveSpeed = 1;
		protected Vector2 mMovementOnEnter;

		#endregion

		#region Getter & Setter

		public int DestinationScene { get { return mDestinationSceneId; } set { mDestinationSceneId = value; } }
		public int DestinationWaypoint { get { return mDesinationWaypointId; } set { mDesinationWaypointId = value; } }

		/// <summary>
		/// Wenn True: Dieser Waypoint kann nur Betreten aber nicht Verlassen werden.
		/// </summary>
		public bool OneWay { get { return mOneWay; } set { mOneWay = value; } }

		/// <summary>
		/// Bewegung beim Betreten des Wegpunkts.
		/// </summary>
		public Vector2 MovementOnEnter { get { return mMovementOnEnter; } set { mMovementOnEnter = value; } }

		public DrawPackage DrawPackage { get { return new DrawPackage(CollisionBox, mDebugColor); } }

		// Zum zeichnen im Editor
		public Texture2D Texture;

		#endregion

		#region Constructor

		public Waypoint()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			mDebugColor = Color.DarkGreen;
			mOneWay = false;
			mDropDown = new DropDownMenu(Vector2.Zero, new List<String>() { "Change One Way", "Verlasse: Norden", "Verlasse: Westen", "Verlasse: Süden", "Verlasse: Osten" }, new List<Action>() { ChangeOneWay, LeaveNorth, LeaveWest, LeaveSouth, LeaveEast });
			mMovementOnEnter = new Vector2(-1f, 0);
		}

		// Wird nur im Editor gezeichnet
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureManager.Instance.GetElementByString("IconMoveArea"), CollisionBox, Color.White);
		}

		/// <summary>
		/// Wird für die Infobox im Editor benötigt.
		/// </summary>
		/// <returns></returns>
		public override string GetInfo()
		{
			String tmpInfo;

			tmpInfo = base.GetInfo();
			tmpInfo += "\nZiel Scene: " + mDestinationSceneId;
			tmpInfo += "\nZiel Waypoint: " + mDesinationWaypointId;
			tmpInfo += "\nOneway:" + mOneWay;

			String leave = "";
			if (mMovementOnEnter.X > 0)
				leave = "\nVerlassen : Osten";
			else if(mMovementOnEnter.X < 0)
				leave = "\nVerlassen : Westen";
			else if(mMovementOnEnter.Y > 0)
				leave = "\nVerlassen : Süden";
			else if (mMovementOnEnter.Y < 0)
				leave = "\nVerlassen : Norden";

			tmpInfo += leave;

			return tmpInfo;
		}

		#endregion

		#region Methods
		#region DropDownMethods

		private void ChangeOneWay()
		{
			mOneWay = !mOneWay;
		}

		private void LeaveNorth()
		{
			mMovementOnEnter = new Vector2(0, -mLeaveSpeed);
		}

		private void LeaveSouth()
		{
			mMovementOnEnter = new Vector2(0, mLeaveSpeed);
		}

		private void LeaveEast()
		{
			mMovementOnEnter = new Vector2(mLeaveSpeed, 0);
		}

		private void LeaveWest()
		{
			mMovementOnEnter = new Vector2(-mLeaveSpeed, 0);
		}
		#endregion

		#endregion
	}
}
