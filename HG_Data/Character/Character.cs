using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Entities;
using Microsoft.Xna.Framework;
using KryptonEngine;
using KryptonEngine.Manager;
using KryptonEngine.Physics;
using System.Xml.Serialization;

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
		[XmlIgnoreAttribute]
		public float Speed { get { return mSpeed; } }

		#region Redirect Position to Skeleton

		// Zum Bewegen MoveInteractiveObject aufrufen !!!

		/*new public Vector2 Position
		//{
		//	set
		//	{
		//		mPosition = value;
		//		mCollisionBox.X = (int)value.X;
		//		mCollisionBox.Y = (int)value.Y;
		//	}
		//	get { return mPosition; }
		//}
		//new public int PositionX
		//{
		//	set
		//	{
		//		mPosition.X = value;
		//		mCollisionBox.X = value;
		//	}
		//	get { return (int)mPosition.X; }
		//}
		//new public int PositionY
		//{
		//	set
		//	{
		//		mPosition.Y = value;
		//		mCollisionBox.Y = value;
		//	}
		//	get { return (int)mPosition.Y; }
		//}
		*/
		#endregion
		[XmlIgnoreAttribute]
		public DrawPackage DrawPackage { get { return new DrawPackage(SkeletonPosition, 0, CollisionBox, mDebugColor, Skeleton); } }

		#endregion

		#region Constructor

		public Character()
		{

		}

		public Character(String name)
			:base(name)
		{

		}

		public Character(String name, Vector2 pPosition)
			:base(name)
		{
			this.Position = pPosition;
		}

		#endregion

		#region Override Methods

		public override void Initialize()
		{
			base.Initialize();
			mDebugColor = Color.LightYellow;
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
			this.AnimationState.ClearTracks();
			this.AnimationState.SetAnimation(0, "idle", true);
		}

		/// <summary>
		/// Animiert den Character für idle und Movement.
		/// </summary>
		/// <param name="pMovement"></param>
		public void AnimBasicAnimation(Vector2 pMovement)
		{
			if (pMovement == Vector2.Zero)
			{
				this.SetAnimation();
				return;
			}
			string TmpAnimation;
			Vector2 TmpMovement = pMovement;
			TmpMovement.Normalize();
			//Flip?
			if (TmpMovement.X > 0)
				this.Flip= true;
			else if (TmpMovement.X < 0)
				this.Flip = false;
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
			this.SetAnimation(TmpAnimation);
		}

		#endregion

		#endregion
	}
}
