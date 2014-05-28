using HanselAndGretel.Data;
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
		SwitchItem
	}

	//public enum ActivityInfo
	//{
	//	Error,
	//	CaughtInCobwebError,
	//	Befreien,
	//	CaughtInSwampError,
	//	Befreien,
	//	Umwerfen,
	//	Balancieren,
	//	Drücken,
	//	SlipThroughRock,
	//	Kriechen,
	//	Springen,
	//	Raueberleiter,
	//	RaueberleiterGreifen,
	//	Schluessel,
	//	Oeffnen,
	//	Kreide,
	//	Herablassen,
	//	UseItemError,
	//	SwitchItemError,
	//}

	public class ActivityState : BaseObject
	{
		public enum State
		{
			Idle,
			Preparing,
			Starting,
			Running
		}

		#region Properties
		
		public static Dictionary<Activity, String> ActivityInfo = new Dictionary<Activity, string>
		{
			{Activity.FreeFromCobweb, "Befreien [Netz]"},
			{Activity.FreeFromSwamp, "Befreien [Sumpf]"},
			{Activity.KnockOverTree, "Umwerfen [Baum]"},
			{Activity.BalanceOverTree, "Balancieren [Baum]"},
			{Activity.PushRock, "Drücken [Fels]"},
			{Activity.SlipThroughRock, "Durch schlüpfen [Fels]"},
			{Activity.Crawl, "Kriechen [Felsspalt]"},
			{Activity.JumpOverGap, "Springen [Abgrund]"},
			{Activity.LegUp, "Räuberleiter"},
			{Activity.LegUpGrab, "Hoch heben"},
			{Activity.UseKey, "Schlüssel benutzen [Tür]"},
			{Activity.PullDoor, "Ziehen [Tür]"},
			{Activity.UseChalk, "Markieren [Kreide]"},
			{Activity.UseWell, "Herablassen [Brunnen]"}
		};

		protected InteractiveObject rIObj;
		protected Hansel rHansel;
		protected Gretel rGretel;
		public State mStateHansel;
		public State mStateGretel;

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
			mStateHansel = State.Idle;
			mStateGretel = State.Idle;
		}

		public ActivityState(Hansel pHansel, Gretel pGretel, InteractiveObject pIObj)
			:base()
		{
			Initialize();
			rHansel = pHansel;
			rGretel = pGretel;
			rIObj = pIObj;
			mStateHansel = State.Idle;
			mStateGretel = State.Idle;
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

		public Action<Player> GetUpdateMethodForPlayer(Player pPlayer)
		{
			if (pPlayer.GetType() == typeof(Hansel))
			{
				switch (mStateHansel)
				{
					case State.Preparing:
						return PrepareAction;
					case State.Starting:
						return StartAction;
					case State.Running:
						return UpdateAction;
					default:
						return new Action<Player>((NeverUsedObject) => { });
				}
			}
			else if (pPlayer.GetType() == typeof(Gretel))
			{
				switch (mStateGretel)
				{
					case State.Preparing:
						return PrepareAction;
					case State.Starting:
						return StartAction;
					case State.Running:
						return UpdateAction;
					default:
						return new Action<Player>((NeverUsedObject) => { });
				}
			}
			else
			{
				throw new Exception("Nicht existenten PlayerString angegeben!");
			}
		}

		/// <summary>
		/// Gibt die Activity zurück, die hier gerade ausgeführt werden kann anhand der Information ob der Spieler nur Intersected oder Contained wird. Ob der konkrete Spieler das dann auch ausführen kann muss extern getestet werden.
		/// </summary>
		/// <param name="pContains">Intersected der Spieler nur oder wird er Contained vom ActionRectangle?</param>
		/// <returns>Nichts ausführbar = Activity.None</returns>
		public virtual Activity GetPossibleActivity(bool pContains) { return Activity.None; }
		public virtual void PrepareAction(Player pPlayer)
		{
			if (pPlayer.GetType() == typeof(Hansel))
			{
				if (rHansel.Input.ActionJustPressed)
					mStateHansel = State.Starting;
			}
			else if (pPlayer.GetType() == typeof(Gretel))
			{
				if (rGretel.Input.ActionJustPressed)
					mStateGretel = State.Starting;
			}
		}
		public virtual void StartAction(Player pPlayer)
		{
			if (pPlayer.GetType() == typeof(Hansel))
			{
				mStateHansel = State.Running;
			}
			else if (pPlayer.GetType() == typeof(Gretel))
			{
				mStateGretel = State.Running;
			}
		}
		public virtual void UpdateAction(Player pPlayer) { }

		#endregion
	}
}
