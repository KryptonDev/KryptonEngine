/**************************************************************
 * (c) Carsten Baus 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KryptonEngine.Manager;
using KryptonEngine.Entities;
using KryptonEngine.Rendering;

namespace KryptonEngine.SceneManagement
{
    public class Scene
    {
        #region Properties

		protected TwoDRenderer mRenderer;

		protected String mName;
        protected String mBackgroundName;
        protected Texture2D mBackgroundTexture;
        protected SpriteBatch mSpriteBatch;
		#region Rendertargets
		protected RenderTarget2D mRenderTargetDiffuse;
		protected RenderTarget2D mRenderTargetNormal;
		protected RenderTarget2D mRenderTargetAO;
		protected RenderTarget2D mRenderTargetDepthObject;
		protected RenderTarget2D mRenderTargetDepthGame;
		protected RenderTarget2D mRenderTargetLight;
		protected RenderTarget2D mRenderTargetFinal;
		#endregion
		protected Camera mCamera;

        protected Color mClearColor = Color.YellowGreen;

        #region Getter & Setter

        public String Name { get { return this.mName; } }
        public String Background 
        { 
            set 
            { 
                mBackgroundName = value;
                mBackgroundTexture = TextureManager.Instance.GetElementByString(value);
            } 
        }

        #endregion

        #endregion

        #region Constructor

        public Scene(String pSceneName)
        {
            this.mName = pSceneName;
            mSpriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
			mRenderer = new TwoDRenderer(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);

			mRenderTargetDiffuse = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetNormal = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetAO = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetDepthObject = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetDepthGame = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetLight = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
			mRenderTargetFinal = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
        }

        #endregion

        #region Methods

        public virtual void LoadContent()
        {
			mRenderer.LoadContent();
        }

        /// <summary>
        /// Updatet Funktionen und GameObjects
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(){ }
        public virtual void Initialize() { }

        public virtual void Draw()
        {
            DrawBackground();
            DrawOnScene();
        }

        protected void DrawBackground()
        {
            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mBackgroundTexture, new Rectangle(0, 0, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight), mClearColor);
            mSpriteBatch.End();
        }

        protected void DrawOnScene()
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);

			mRenderer.DrawRenderTargetOnScreen(mSpriteBatch, 0);
            mSpriteBatch.Begin();
            //mSpriteBatch.Draw(mRenderer.GetRenderTargetTexture(0), new Rectangle(0, 0, EngineSettings.DisplayWidth, EngineSettings.DisplayHeight), Color.White);
			mSpriteBatch.Draw(mRenderTargetFinal, Vector2.Zero, Color.White);
            mSpriteBatch.End();
        }

        #endregion

    }
}
