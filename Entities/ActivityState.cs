using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
	public enum Activity
	{
		None,
		CaughtInCobweb,
		FreeFromCobweb,
		CaughtInSwamp,
		FreeFromSwamp,
		KnockOverTree,
		BalanceOverTree,
		PushRock,
		SlipThroughRock,
		Crawl,
		JumpOverGap,
		LegUp,
		LegUpGrab,
		UseKey,
		PullDoor,
		UseChalk,
		UseWell,
		UseItem,
		SwitchItem,
	}

	public class ActivityState : BaseObject
	{
		#region Properties

		/// <summary>
		/// Free -> Caught. KnockOver -> BalanceOver. UseKey -> PullDoor. PullDoor -> None. PushRock -> None.
		/// </summary>
		protected bool m2ndState;

		/// <summary>
		/// InputMovementFactor: 0-1
		/// </summary>
		protected float mMovementSpeedReductionHansel;
		/// <summary>
		/// InputMovementFactor: 0-1
		/// </summary>
		protected float mMovementSpeedReductionGretel;

		#endregion

		#region Constructor

		public ActivityState()
			:base()
		{
			Initialize();
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			
		}

		public override void Update()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Methods

		#endregion
	}
}
