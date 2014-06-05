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
		private Vector2 mViewportDimensions;
		private float mScale;
		private float mScaleMax;
		private int mBoundSize;
        private Rectangle mGameScreen;
		private Matrix mTransform;
        #endregion

        #region Getter & Setter
		public Vector2 Position { get { return mPositionCamera; } set { mPositionCamera.X = value.X; mPositionCamera.Y = value.Y; } }
		public Vector2 Offset { get { return mCameraOffset; } }
		public Rectangle GameScreen { get { return mGameScreen; } 
			set 
			{
				mGameScreen = value;
				int x = mGameScreen.Width / EngineSettings.VirtualResWidth;
				int y = mGameScreen.Height / EngineSettings.VirtualResHeight;

				mScaleMax = (x <= y) ? x : y;
				mViewportDimensions = new Vector2(EngineSettings.VirtualResWidth * mScaleMax, EngineSettings.VirtualResHeight * mScaleMax);
			} 
		}
		public Matrix Transform { get { return mTransform; } }
		public int BoundSize { get { return mBoundSize; } set { mBoundSize = value; } }
		public float Scale { get { return mScale; } }
		public Vector2 ViewportMaxDimension { get { return mViewportDimensions; } }
        #endregion

        #region Constructor

        public Camera()
        {
			Position = Vector2.Zero;
            mCameraOffset = Vector2.Zero;
            Initialize();
        }

        public Camera(Vector2 pOffset)
        {
			Position = Vector2.Zero;
            mCameraOffset = pOffset;
            Initialize();
        }

        public Camera(Vector2 pPosition, Rectangle pGameScreen)
        {
			Position = Vector2.Zero;
			mCameraOffset = Vector2.Zero;
            mGameScreen = pGameScreen;
            Initialize();
        }

        #endregion

        #region Override Methods

        public override void Initialize()
        {
			mScale = 1.0f;
        }

        #endregion

        #region Methods

        public Matrix GetTranslationMatrix()
        {
			return mTransform;
        }

		/// <summary>
		/// Updatet mTransform wenn sich 2 Spieler bewegen.
		/// </summary>
		/// <param name="pos1">Spieler 1 Origin</param>
		/// <param name="pos2">Spieler 2 Origin</param>
		public void MoveCamera(Rectangle pos1, Rectangle pos2)
		{

			Vector2 origin1 = new Vector2(pos1.Center.X, pos1.Center.Y);
			Vector2 origin2 = new Vector2(pos2.Center.X, pos2.Center.Y);
			Vector2 Distance = (origin1 - origin2);
			Position = (origin2 + Distance / 2);

			SetScale(Distance);
			CheckBounds();

			mTransform = Matrix.CreateTranslation(new Vector3(-Position, 0)) 
				* Matrix.CreateScale(mScale) 
				* Matrix.CreateTranslation(EngineSettings.VirtualResWidth / 2, EngineSettings.VirtualResHeight / 2, 0);
		}

		/// <summary>
		/// Berechnet den aktuellen Scalewert anhand der Distanc der Spieler zueinander.
		/// </summary>
		/// <param name="pDistance">Distance zwischen den zwei Spielern.</param>
		protected void SetScale(Vector2 pDistance)
		{
			// 200 für Charakter Höhe/ Breite
			float mScaleX = Math.Abs(pDistance.X) / (EngineSettings.VirtualResWidth - 200);
			float mScaleY = Math.Abs(pDistance.Y) / (EngineSettings.VirtualResHeight - 200);

			if (mScaleX < 1) mScaleX = 1;
			if (mScaleX > mScaleMax) mScaleX = mScaleMax;
			mScaleX = 1 / mScaleX;

			if (mScaleY < 1) mScaleY = 1;
			if (mScaleY > mScaleMax) mScaleY = mScaleMax;
			mScaleY = 1 / mScaleY;

			mScale = (mScaleX <= mScaleY) ? mScaleX : mScaleY;
		}

		/// <summary>
		/// Überprüft ob die Kamera Position inerhalb des GameScreens ist.
		/// </summary>
		protected void CheckBounds()
		{
			if (mPositionCamera.X < EngineSettings.VirtualResWidth / 2 / mScale)
				mPositionCamera.X = EngineSettings.VirtualResWidth / 2 / mScale;
			else if (mPositionCamera.X > GameScreen.Width - EngineSettings.VirtualResWidth / 2 / mScale)
				mPositionCamera.X = GameScreen.Width - EngineSettings.VirtualResWidth / 2 / mScale;

			if (mPositionCamera.Y < EngineSettings.VirtualResHeight / 2 / mScale)
				mPositionCamera.Y = EngineSettings.VirtualResHeight / 2 / mScale;
			else if (mPositionCamera.Y > GameScreen.Height - EngineSettings.VirtualResHeight / 2 / mScale)
				mPositionCamera.Y = GameScreen.Height- EngineSettings.VirtualResHeight / 2 / mScale;
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
              if (mPositionCamera.X < (-GameScreen.Width + EngineSettings.VirtualResWidth / 2 - mBoundSize * 2))
				  mPositionCamera.X = (int)(-GameScreen.Width + EngineSettings.VirtualResWidth / 2 - mBoundSize * 2);
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
			mTransform = Matrix.CreateTranslation(new Vector3(mPositionCamera, 0));
        }

		public void ResetScale()
		{
			mScale = 1;
		}

		public void ZoomOut(int pZoomFactor)
		{
			mScale = 1 / pZoomFactor;
		}
        #endregion

    }
}