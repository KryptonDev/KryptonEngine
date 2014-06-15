using HanselAndGretel.Data;
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
	}

	public class ActivityState : BaseObject
	{
		#region Properties
		
		public static Dictionary<Activity, String> ActivityInfo = new Dictionary<Activity, string>
		{
			{Activity.FreeFromCobweb, "Befreien [Netz]"},
			{Activity.FreeFromSwamp, "Befreien [Sumpf]"},
			{Activity.KnockOverTree, "Umwerfen [Baum]"},
			{Activity.BalanceOverTree, "Balancieren [Baum]"},
			{Activity.PushRock, "Drücken [Fels]"},
			{Activity.SlipThroughRock, "Durch schlüpfen [Fels]"},
			{Activity.JumpOverGap, "Springen [Abgrund]"},
			{Activity.LegUp, "Räuberleiter"},
			{Activity.LegUpGrab, "Hoch heben"},
			{Activity.UseKey, "Schlüssel benutzen [Tür]"},
			{Activity.PushDoor, "Drücken [Tür]"},
			{Activity.PullDoor, "Ziehen [Tür]"},
			{Activity.UseChalk, "Markieren [Kreide]"},
			{Activity.UseWell, "Herablassen [Brunnen]"},
			{Activity.BalanceOverBrokenTree, "Balancieren [Brüchiger Baum]"}
		};

		protected InteractiveObject rIObj;
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

		public ActivityState(InteractiveObject pIObj = null)
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

		/// <summary>
		/// Gibt die Activity zurück, die hier gerade ausgeführt werden kann anhand der Information ob der Spieler nur Intersected oder Contained wird. Ob der konkrete Spieler das dann auch ausführen kann muss extern getestet werden.
		/// </summary>
		/// <param name="pContains">Intersected der Spieler nur oder wird er Contained vom ActionRectangle?</param>
		/// <returns>Nichts ausführbar = Activity.None</returns>
		public virtual Activity GetPossibleActivity(Player pPlayer, Player pOtherPlayer) { return Activity.None; }
		public virtual void Update(Player pPlayer, Player pOtherPlayer) { }

		#endregion
	}
}
