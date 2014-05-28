using KryptonEngine;
using KryptonEngine.Controls;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HanselAndGretel.Data
{
	public class Player : Character
	{
		#region Properties

		public Inventory Inventory;
		public List<Activity> mHandicaps;
		public ActivityState mCurrentActivity;
		protected InputHelper mInput;

		//References
		protected Player rOtherPlayer;

		#endregion

		#region Getter & Setter

		public InputHelper Input { get { return mInput; } }

		#endregion

		#region Constructor

		public Player()
		{
			
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			base.Initialize();
			mDebugColor = Color.LimeGreen;
			mHandicaps = new List<Activity>();
			mSpeed = 400;
		}

		#endregion

		#region Methods

		public void LoadReferences(Camera pCamera, Player pOtherPlayer)
		{
			base.LoadReferences(pCamera);
			rOtherPlayer = pOtherPlayer;
		}

		public void Update(bool pMayMove, float pMovementSpeedFactor, SceneData pScene)
		{
			base.Update();
			if (pMayMove && pMovementSpeedFactor > 0)
				AnimBasicAnimation(Move(ViewportCheckedVector(GetMovement(mInput.Movement, pMovementSpeedFactor)), GetBodiesForCollisionCheck(pScene)));
		}

		#region Update Movement Helper

		public void MoveManually(Vector2 pMovementDirection, float pMovementSpeedFactor = 1f, SceneData pScene = null, bool pIgnoreCollision = true)
		{
			Vector2 TmpMovement;
			List<Rectangle> TmpBodies;
			if (pIgnoreCollision)
			{
				TmpMovement = ViewportCheckedVector(GetMovement(pMovementDirection, pMovementSpeedFactor));
				TmpBodies = new List<Rectangle>();
			}
			else
			{
				TmpMovement = GetMovement(pMovementDirection, pMovementSpeedFactor);
				TmpBodies = GetBodiesForCollisionCheck(pScene);
			}
			AnimBasicAnimation(Move(TmpMovement, TmpBodies));
		}

		public void MoveAgainstPoint(Vector2 pTargetPoint, float pMovementSpeedFactor = 1f, SceneData pScene = null, bool pIgnoreCollision = true)
		{
			Vector2 TmpMovementDirection = pTargetPoint - Position;
			TmpMovementDirection.Normalize();
			Vector2 TmpMovement;
			List<Rectangle> TmpBodies;
			if (pIgnoreCollision)
			{
				TmpMovement = ViewportCheckedVector(GetMovement(TmpMovementDirection, pMovementSpeedFactor));
				TmpBodies = new List<Rectangle>();
			}
			else
			{
				TmpMovement = GetMovement(TmpMovementDirection, pMovementSpeedFactor);
				TmpBodies = GetBodiesForCollisionCheck(pScene);
			}
			if ((pTargetPoint - Position).Length() < TmpMovement.Length()) //Nicht über Punkt hinaus gehen.
				TmpMovement = pTargetPoint - Position;
			AnimBasicAnimation(Move(TmpMovement, TmpBodies));
		}

		/// <summary>
		/// Get Scene.MoveArea + OtherPlayers CollisionBox as Rectangle-List.
		/// </summary>
		protected List<Rectangle> GetBodiesForCollisionCheck(SceneData pScene)
		{
			List<Rectangle> TmpMoveArea = new List<Rectangle>(pScene.MoveArea);
			TmpMoveArea.Add(rOtherPlayer.CollisionBox);
			return TmpMoveArea;
		}

		/// <summary>
		/// Vector Checked for rCamera.GameScreen & MaxScaling (/rCamera.ViewportMaxDimension).
		/// </summary>
		protected Vector2 ViewportCheckedVector(Vector2 pMovement)
		{
			if (InCameraBounds(pMovement))
			{
				return pMovement;
			}
			Vector2 TmpMovementInBounds = Vector2.Zero;
			int TmpSteps = (pMovement.X < pMovement.Y) ? (int)pMovement.Y : (int)pMovement.X;
			for (int i = TmpSteps; i > 0; i--) //Move Player step für step weniger, bis er in den Camera Viewport passt.
			{
				TmpMovementInBounds = (pMovement / TmpSteps) * i;
				if (InCameraBounds(TmpMovementInBounds))
					return TmpMovementInBounds;
			}
			return Vector2.Zero;
		}

		/// <summary>
		/// Check for rCamera.GameScreen & MaxScaling (/rCamera.ViewportMaxDimension) Collision.
		/// </summary>
		protected bool InCameraBounds(Vector2 pMovement)
		{
			if (CollisionBox.X + pMovement.X < 0 || CollisionBox.Y + pMovement.Y < 0 || CollisionBox.Right + pMovement.X > rCamera.GameScreen.Right || CollisionBox.Bottom + pMovement.Y > rCamera.GameScreen.Bottom)
				return false;
			Rectangle TmpThisPlayer = new Rectangle(PositionX + (int)pMovement.X, PositionY + (int)pMovement.Y, CollisionBox.Width, CollisionBox.Height);
			//Horizontal
			Rectangle TmpPlayerLeft;
			Rectangle TmpPlayerRight;
			if (rOtherPlayer.CollisionBox.X < Position.X)
			{
				TmpPlayerLeft = rOtherPlayer.CollisionBox;
				TmpPlayerRight = TmpThisPlayer;
			}
			else
			{
				TmpPlayerLeft = TmpThisPlayer;
				TmpPlayerRight = rOtherPlayer.CollisionBox;
			}
			//Vertical
			Rectangle TmpPlayerUp;
			Rectangle TmpPlayerDown;
			if (rOtherPlayer.CollisionBox.Y < Position.Y)
			{
				TmpPlayerUp = rOtherPlayer.CollisionBox;
				TmpPlayerDown = TmpThisPlayer;
			}
			else
			{
				TmpPlayerUp = TmpThisPlayer;
				TmpPlayerDown = rOtherPlayer.CollisionBox;
			}
			//Test
			Rectangle TmpRectangleToCheck = new Rectangle(TmpPlayerLeft.Left, TmpPlayerUp.Top, TmpPlayerRight.Right - TmpPlayerLeft.Left, TmpPlayerDown.Bottom - TmpPlayerUp.Top);
			return (TmpRectangleToCheck.Width <= rCamera.ViewportMaxDimension.X && TmpRectangleToCheck.Height <= rCamera.ViewportMaxDimension.Y) ? true : false;
		}

		#endregion

		public bool CheckForAbility(Activity pAcitvity)
		{
			if (mHandicaps.Contains(pAcitvity))
				return false;
			return true;
		}

		#endregion
	}
}
