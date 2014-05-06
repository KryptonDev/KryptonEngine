/**************************************************************
 * (c) Jens Richter 2014
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
    public class CameraGame : BaseObject
    {
        #region Properties

        private Rectangle mViewportVirtual; //Virtueller Viewport
        private Rectangle mViewportScreen; //Screen Viewport
		private Matrix mTransformToViewport; //TransformationsMatrix vom ObjectSpace zum WorldSpace(/VirtualSpace)
		private Matrix mTransformToScreen; //TransformationsMatrix vom VirtualSpace(/WorldSpace) zum ScreenSpace
		
		private Rectangle mVirtualMoveRestriction; //Bereich, in dem sich der virtuelle Viewport (/die Camera) bewegen darf.

        #endregion

        #region Getter & Setter

		//Position
		public Vector2 Position { get { return new Vector2(mViewportVirtual.X, mViewportVirtual.Y); } set { mViewportVirtual.X = (int)value.X; mViewportVirtual.Y = (int)value.Y; } }
		public int PositionX { get { return mViewportVirtual.X; } set { mViewportVirtual.X = value; } }
		public int PositionY { get { return mViewportVirtual.Y; } set { mViewportVirtual.Y = value; } }
		
		//CameraMaße (ViewportVirtual)
		public int Width { get { return mViewportVirtual.Width; } set { mViewportVirtual.Width = value; UpdateTransformationToScreen(); } }
		public int Height { get { return mViewportVirtual.Height; } set { mViewportVirtual.Height = value; UpdateTransformationToScreen(); } }

		//Viewports
		public Rectangle ViewportVirtual { get { return mViewportVirtual; } set { mViewportVirtual = value; UpdateTransformationToViewport(); UpdateTransformationToScreen(); } }
		public Rectangle ViewportScreen { get { return mViewportScreen; } set { mViewportScreen = value; UpdateTransformationToScreen(); } }

		//TransformationMatrizen
		public Matrix TransformToViewport { get { return mTransformToViewport; } }
		public Matrix TransformToScreen { get { return mTransformToScreen; } }

		public Rectangle VirtualMoveRestriction { get { return mVirtualMoveRestriction; } set { mVirtualMoveRestriction = value; } }
		
        #endregion

        #region Constructor

        public CameraGame()
        {
            Initialize();
        }

		public CameraGame(Vector2 pPosition)
        {
            Initialize();
			Position = pPosition;
        }

        public CameraGame(Vector2 pPosition, Rectangle pVirtualMoveRestriction)
        {
            Initialize();
			Position = pPosition;
			mVirtualMoveRestriction = pVirtualMoveRestriction;
        }

        #endregion

        #region Override Methods

		/// <summary>
		/// Initialisiert die Camera mit den Standardwerten und führt einmal eine Aktualisierung der Matrizen durch.
		/// </summary>
        public override void Initialize()
        {
			//Viewports auf EngineSettings setzen
			mViewportVirtual = new Rectangle(0, 0, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mViewportScreen = new Rectangle(0, 0, EngineSettings.DisplayWidth, EngineSettings.DisplayHeight);

			Position = Vector2.Zero;

			//Transformation Matrizen updaten
			UpdateTransformationToViewport();
			UpdateTransformationToScreen();

			//MoveRestriction per Default auf den Viewport setzen
			mVirtualMoveRestriction = mViewportVirtual;
        }

		/// <summary>
		/// Updated die Transformations-Matrizen.
		/// </summary>
        public override void Update()
        {
			UpdateTransformationToViewport();
        }

        #endregion

        #region Methods

		/// <summary>
		/// Updated die Object to ViewportVirtual Transformation.
		/// </summary>
		protected void UpdateTransformationToViewport()
		{
			mTransformToViewport = Matrix.CreateTranslation(-mViewportVirtual.X, -mViewportVirtual.Y, 0);
		}

		/// <summary>
		/// Updated die ViewportVirtual to ViewportScreen Transformation.
		/// </summary>
		protected void UpdateTransformationToScreen()
		{
			Matrix TmpScaleToScreen = Matrix.CreateScale((float)mViewportScreen.Width / (float)mViewportVirtual.Width, (float)mViewportScreen.Height / (float)mViewportVirtual.Height, 1);
			Matrix TmpTranslationToScreen = Matrix.CreateTranslation(mViewportScreen.X, mViewportScreen.Y, 0);
			mTransformToScreen = TmpScaleToScreen * TmpTranslationToScreen;
		}

		/// <summary>
		/// Bewegt die Camera im Virtual/World Space.
		/// </summary>
		public void Move(int pDeltaX, int pDeltaY)
		{
			Position += new Vector2(pDeltaX, pDeltaY);
			ForceInMoveRestriction();
		}

		/// <summary>
		/// Bewegt die Camera im Virtual/World Space.
		/// </summary>
		public void Move(Vector2 pDelta)
		{
			Position += pDelta;
			ForceInMoveRestriction();
		}

		/// <summary>
		/// Pointed die Camera im Virtual/World Space auf ein Rectangle. Forced danach in VirtualMoveRestriction.
		/// </summary>
		/// <param name="pFocus">Rectangle auf das Fokussiert werden soll</param>
		public void FocusOn(Rectangle pFocus)
		{
			if (mViewportVirtual.Left > pFocus.Left) //Scrolling nach links
			{
				mViewportVirtual.X = pFocus.Left;
			}
			else if (mViewportVirtual.Right < pFocus.Right) //Scrolling nach rechts
			{
				mViewportVirtual.X = pFocus.Right - mViewportVirtual.Width;
			}
			if (mViewportVirtual.Top > pFocus.Top) //Scrolling nach oben
			{
				mViewportVirtual.Y = pFocus.Top;
			}
			else if (mViewportVirtual.Bottom < pFocus.Bottom) //Scrolling nach unten
			{
				mViewportVirtual.Y = pFocus.Bottom - mViewportVirtual.Height;
			}
			ForceInMoveRestriction();
		}

		/// <summary>
		/// Catched die Camera im Virtual/World Space in VirtualMoveRestriction.
		/// </summary>
		public void ForceInMoveRestriction()
		{
			if (mViewportVirtual.Left < mVirtualMoveRestriction.Left) //Linker Rand
			{
				mViewportVirtual.X = mVirtualMoveRestriction.Left;
			}
			else if (mViewportVirtual.Right > mVirtualMoveRestriction.Right) //Rechter Rand
			{
				mViewportVirtual.X = mVirtualMoveRestriction.Right - mViewportVirtual.Width;
			}
			if (mViewportVirtual.Top < mVirtualMoveRestriction.Top) //Oberer Rand
			{
				mViewportVirtual.Y = mVirtualMoveRestriction.Top;
			}
			else if (mViewportVirtual.Bottom > mVirtualMoveRestriction.Bottom) //Unterer Rand
			{
				mViewportVirtual.Y = mVirtualMoveRestriction.Bottom - mViewportVirtual.Height;
			}
		}

        #endregion
    }
}
