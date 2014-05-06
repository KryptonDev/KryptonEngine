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

namespace KryptonEngine.SceneManagement
{
    public class Scene
    {
        #region Properties

        protected String mName;
        protected String mBackgroundName;
        protected Texture2D mBackgroundTexture;
        protected SpriteBatch mSpriteBatch;
        protected RenderTarget2D mRenderTarget;
        protected CameraEditor mCamera;

        protected Color mClearColor = Color.YellowGreen;

        #region Getter & Setter

        public String Name { get { return this.mName; } }
        public String Background 
        { 
            set 
            { 
                mBackgroundName = value;
                mBackgroundTexture = TextureManager.Instance.GetElementByString(value);
                mClearColor = Color.White; 
            } 
        }

        #endregion

        #endregion

        #region Constructor

        public Scene(String pSceneName)
        {
            this.mName = pSceneName;
            mSpriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
            mRenderTarget = new RenderTarget2D(EngineSettings.Graphics.GraphicsDevice, EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight);
        }

        #endregion

        #region Methods

        public virtual void LoadContent()
        {

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

            mSpriteBatch.Begin();
            mSpriteBatch.Draw(mRenderTarget, new Rectangle(0, 0, EngineSettings.DisplayWidth, EngineSettings.DisplayHeight), Color.White);
            mSpriteBatch.End();
        }

        #endregion

    }
}
