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
		/// InputMovementReduction: 0-1
		/// </summary>
		protected float mMovementSpeedFactorHansel;
		/// <summary>
		/// InputMovementReduction: 0-1
		/// </summary>
		protected float mMovementSpeedFactorGretel;

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
			m2ndState = false;
			mMovementSpeedFactorHansel = 0f;
			mMovementSpeedFactorGretel = 0f;
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
