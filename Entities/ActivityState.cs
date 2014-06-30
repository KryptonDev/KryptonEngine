﻿using HanselAndGretel.Data;
using Microsoft.Xna.Framework;
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
		JumpOverGap,
		LegUp,
		LegUpGrab,
		UseKey,
		PushDoor,
		PullDoor,
		UseChalk,
		UseWell,
		UseItem,
		SwitchItem,
		BalanceOverBrokenTree
	};

	public class ActivityState : BaseObject
	{
		#region Properties

		public InteractiveObject rIObj;
		//protected Hansel rHansel;
		//protected Gretel rGretel;

		public bool IsAvailable;

		/// <summary>
		/// Free -> Caught. KnockOver -> BalanceOver. UseKey -> PullDoor. PullDoor -> None. PushRock -> None.
		/// </summary>
		public bool m2ndState;

		/// <summary>
		/// InputMovementReduction: 0-1
		/// </summary>
		public float mMovementSpeedFactorHansel;
		/// <summary>
		/// InputMovementReduction: 0-1
		/// </summary>
		public float mMovementSpeedFactorGretel;

		#endregion

		#region Constructor

		public ActivityState()
			:base()
		{
			Initialize();
		}

		public ActivityState(Hansel pHansel, Gretel pGretel, InteractiveObject pIObj = null)
			:base()
		{
			Initialize();
			if (pIObj != null)
				rIObj = pIObj;
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			m2ndState = false;
			mMovementSpeedFactorHansel = 0f;
			mMovementSpeedFactorGretel = 0f;
			IsAvailable = true;
		}

		#endregion

		#region Methods

		public virtual Activity GetPossibleActivity(Player pPlayer, Player pOtherPlayer) { return Activity.None; }
		public virtual void Update(Player pPlayer, Player pOtherPlayer) { }

		#endregion
	}
}
