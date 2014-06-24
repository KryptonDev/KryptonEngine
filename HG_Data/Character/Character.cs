using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using KryptonEngine;
using KryptonEngine.Manager;
using KryptonEngine.Physics;

namespace HanselAndGretel.Data
{
	public class Character : InteractiveObject
	{
		#region Properties

		protected float mSpeed;

		//References
		protected Camera rCamera;

		#endregion

		#region Getter & Setter

		public float Speed { get { return mSpeed; } }

		#region Redirect Position to CollisionBox

		new public Vector2 Position
		{
			set
			{
				mPosition = value;
				mCollisionRectList[0] = new Rectangle((int)value.X, (int)value.Y, mCollisionRectList[0].Width, mCollisionRectList[0].Height);
			}
			get { return mPosition; }
		}
		new public int PositionX
		{
			set
			{
				mPosition.X = value;
				mCollisionRectList[0] = new Rectangle(value, mCollisionRectList[0].Y, mCollisionRectList[0].Width, mCollisionRectList[0].Height);
			}
			get { return (int)mPosition.X; }
		}
		new public int PositionY
		{
			set
			{
				mPosition.Y = value;
				mCollisionRectList[0] = new Rectangle(mCollisionRectList[0].X, value, mCollisionRectList[0].Width, mCollisionRectList[0].Height);
			}
			get { return (int)mPosition.Y; }
		}

		#endregion

		#endregion

		#region Constructor

		public Character() : base() { }

		public Character(string pName)
			:base(pName)
		{

		}


		#endregion

		#region Override Methods

		public override void Initialize()
		{
			base.Initialize();
			mDebugColor = Color.LightYellow;
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override void Update()
		{
			base.Update();
		}

		#endregion

		#region Methods

		public void LoadReferences(Camera pCamera)
		{
			rCamera = pCamera;
		}

		/// <summary>
		/// Moved den Character um pDelta, prüft vorher auf Collision mit pMoveArea.
		/// </summary>
		public Vector2 Move(Vector2 pDelta, List<Rectangle> pMoveArea)
		{
			Vector2 TmpMovement = Collision.CollisionCheckedVector(CollisionBox, (int)pDelta.X, (int)pDelta.Y, pMoveArea);
			Position += TmpMovement;
			return TmpMovement;
		}

		/// <summary>
		/// Gibt entsprechend den Bedingungen potentielles Movement zurück.
		/// </summary>
		protected Vector2 GetMovement(Vector2 pMovementDirection, float pMovementSpeedFactor = 1f)
		{
			if (pMovementDirection.Length() != 1f)
				pMovementDirection.Normalize();
			return pMovementDirection * mSpeed * pMovementSpeedFactor * (EngineSettings.Time.ElapsedGameTime.Milliseconds / 1000f);
		}

		#region Animation

		/// <summary>
		/// Clear alle Tracks und started gelooped die "idle" Animation.
		/// </summary>
		public void AnimCutToIdle()
		{
			AnimationState.ClearTracks();
			AnimationState.SetAnimation(0, "idle", true);
		}

		/// <summary>
		/// Animiert den Character für idle und Movement.
		/// </summary>
		/// <param name="pMovement"></param>
		public void AnimBasicAnimation(Vector2 pMovement)
		{
			if (pMovement == Vector2.Zero)
			{
				SetAnimation();
				return;
			}
			string TmpAnimation;
			Vector2 TmpMovement = pMovement;
			TmpMovement.Normalize();
			//Flip?
			if (TmpMovement.X > 0)
				Flip = true;
			else if (TmpMovement.X < 0)
				Flip = false;
			//Get correct Animation
			//if (TmpMovement.Y > Math.Sin(67.5)) //Hoch
			//	TmpAnimation = "walkUp";
			//else if (TmpMovement.Y > Math.Sin(22.5)) //Seitlich hoch
			//	TmpAnimation = "walkSideUp";
			//else if (TmpMovement.Y > -Math.Sin(22.5)) //Seitlich
			//	TmpAnimation = "walkSide";
			//else if (TmpMovement.Y > -Math.Sin(67.5)) //Seitlich runter
			//	TmpAnimation = "walkSideDown";
			//else //Runter
			//	TmpAnimation = "WalkDown";
			TmpAnimation = "idle";
			SetAnimation(TmpAnimation);
		}

		#endregion

		#endregion
	}
}
