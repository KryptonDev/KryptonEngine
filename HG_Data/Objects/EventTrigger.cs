using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class EventTrigger
	{
		public enum EEvent
		{
			SpawnWitch,
			TreeFalling,
			PlaySound,
			ActivateTrigger,
		}

		#region Properties

		protected List<Rectangle> mTrigger;
		protected int mIdToTrigger;
		protected EEvent mEvent;
		protected bool mActivated;

		#endregion

		#region Getter & Setter

		public List<Rectangle> Trigger { get { return mTrigger; } set { mTrigger = value; } }
		public EEvent Event { get { return mEvent; } set { mEvent = value; } }
		public int Target { get { return mIdToTrigger; } set { mIdToTrigger = value; } }
		public bool IsAcitvated { get { return mActivated; } set { mActivated = value; } }

		#endregion
	}
}
