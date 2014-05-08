/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KryptonEngine.Entities
{
    public class Camera : BaseObject
    {
        #region Properties

        private Vector2 mCameraOffset;
        private Vector2 mPositionCamera;
		private float mScale;
        private int mBoundSize = 100;
        private Rectangle mGameScreen;
		private Matrix mTransform;
        #endregion

        #region Getter & Setter
        public Vector2 Position { get { return mPositionCamera; } set { mPositionCamera = value; } }
        public Rectangle GameScreen { get { return mGameScreen; } set { mGameScreen = value; } }
		public Matrix Transform { get { return mTransform; } }
		public int BoundSize { get { return mBoundSize; } set { mBoundSize = value; } }
        #endregion

        #region Constructor

        public Camera()
        {
            mCameraOffset = Vector2.Zero;
            Initialize();
        }

        public Camera(Vector2 pOffset)
        {
            mCameraOffset = pOffset;
            Initialize();
        }

        public Camera(Vector2 pPosition, Rectangle pGameScreen)
        {
            mGameScreen = pGameScreen;
            Initialize();
        }

        #endregion

        #region Override Methods

        public override void Initialize()
        {
            Position = Vector2.Zero;
            mCameraOffset = Vector2.Zero;
			mScale = 1.0f;
        }

        public override void Update()
        {
            
        }

        #endregion

        #region Methods

        public Matrix GetTranslationMatrix()
        {
			return mTransform;
        }

		public void Zoom(float f)
		{
			mScale += f;
		}

		/// <summary>
		/// Erstellt eine Translations/ Scalierungs Matrix wenn sich 2 Spieler bewegen.
		/// </summary>
		/// <param name="pos1">Spieler 1 Origin</param>
		/// <param name="pos2">Spieler 2 Origin</param>
		public void MoveCamera(Vector2 pos1, Vector2 pos2)
		{

			Vector2 Distance = (pos1 - pos2) / 2;
			Position = (pos2 + Distance);

			mTransform = Matrix.CreateTranslation(new Vector3(-Position, 0)) 
				* Matrix.CreateScale(mScale, mScale, 1.0f) 
				* Matrix.CreateTranslation(EngineSettings.VirtualResWidth / 2, EngineSettings.VirtualResHeight / 2, 0);
		}

        public void MoveCamera(Vector2 mSpeed)
        {
            // Links Bewegung
            if (mSpeed.X > 0)
                if (mPositionCamera.X < (mBoundSize))
                    mPositionCamera.X += mSpeed.X;
                else
                  mPositionCamera.X = mBoundSize;
            // Rechts Bewegung
            else if (mSpeed.X < 0)
              if (mPositionCamera.X <= (-GameScreen.Width + mBoundSize))
                  mPositionCamera.X = (-GameScreen.Width + mBoundSize );
                else
                    mPositionCamera.X += mSpeed.X;

            // Bewegung Oben
            if (mSpeed.Y > 0)
              if (mPositionCamera.Y < mBoundSize)
                    mPositionCamera.Y += mSpeed.Y;
                else
                mPositionCamera.Y = mBoundSize;
            //Bewegung Unten
            else if (mSpeed.Y < 0)
              if (mPositionCamera.Y <= (-mGameScreen.Height + EngineSettings.VirtualResHeight- mBoundSize))
                mPositionCamera.Y = (-mGameScreen.Height + EngineSettings.VirtualResHeight - mBoundSize);
                else
                    mPositionCamera.Y += mSpeed.Y;

        }
        #endregion

    }
}


/*
 *
 * public class Camera
 * {
 * 
 *  #region Properties
 *  Vector2 mPosition;
 *  Vector2 mOffset;
 *  Vector2 mOrigin // zum zoomen
 *  
 * Rectangle GameScreen;
 *  
 * int mBoundSize = 100;
 *  
 * Matrix TranslationMatrix;
 *  #endregion
 *  
 * #region Constructor
 * 
 * public Camera()
 * {
 *  mPosition = Vector2.Zero;
 *  mOffset = Vector2.Zero;
 *  mOrigin = new Vector2(EngineSettings.VirtualResWidth / 2, EngineSettings.VirtualResHeight / 2);
 * }
 * 
 * public Camera(Vector2 pOffset)
 * {
 * mPosition = Vector2.Zero;
 * mOffset = pOffset;
 * mOrigin = new Vector2(EngineSettings.VirtualResWidth / 2, EngineSettings.VirtualResHeight / 2);
 * }
 * #endregion
 * }
 * 
 * public void MoveCamera(Vector2 pMoveSpeed)
 * {
 *  mPosition += MoveSpeed;
 *  CheckBounds();
 *  UpdateTranslationMatrix();
 * }
 * 
 * public void UpdateTranslationMatrix()
 * {
 *    TranslationMatrix = Matrix.CreateTranslation(new Vector3(mPosition + mOffset, 0));
 * }
 * 
 * private void CheckBounds()
 * {
 *             // Links Bewegung
            if (mSpeed.X > 0)
                if (mPositionCamera.X < mBoundSize)
                    mPositionCamera.X += mSpeed.X;
                else
                    mPositionCamera.X = mBoundSize;
            // Rechts Bewegung
            else if (mSpeed.X < 0)
                if (mPositionCamera.X <= (-GameScreen.Width + mBoundSize))
                    mPositionCamera.X = (-GameScreen.Width + mBoundSize);
                else
                    mPositionCamera.X += mSpeed.X;

            // Bewegung Oben
            if (mSpeed.Y > 0)
                if (mPositionCamera.Y < mBoundSize)
                    mPositionCamera.Y += mSpeed.Y;
                else
                    mPositionCamera.Y = mBoundSize;
            //Bewegung Unten
            else if (mSpeed.Y < 0)
                if (mPositionCamera.Y <= (-mGameScreen.Height + mBoundSize))
                    mPositionCamera.Y = (-mGameScreen.Height + mBoundSize);
                else
                    mPositionCamera.Y += mSpeed.Y;
 * }
 * 
 */

