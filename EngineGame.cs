using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using KryptonEngine.Manager;
using KryptonEngine.SceneManagement;
using KryptonEngine.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using KryptonEngine.FModAudio;

namespace KryptonEngine
{
    public class EngineGame : Game
    {
        SpriteBatch spriteBatch;

        public EngineGame()
            : base()
        {
            Content.RootDirectory = "Content";
            EngineSettings.Content = Content;
            EngineSettings.Graphics = new GraphicsDeviceManager(this);
			EngineSettings.InitializeFMOD();
			FmodMediaPlayer.FadingSpeed = SceneManager.Instance.FadeSpeed;
        }

        protected override void Initialize()
        {
            SceneManager.Instance.Initialize();
			EngineSettings.Randomizer = new Random();
			base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);
			EngineSettings.TextureLoader = new XnaTextureLoader(EngineSettings.Graphics.GraphicsDevice);

            TextureManager.Instance.LoadContent();
            FontManager.Instance.LoadContent();
			ShaderManager.Instance.LoadContent();
            SpineDataManager.Instance.LoadContent();
            InteractiveObjectDataManager.Instance.LoadContent();
            SceneManager.Instance.LoadContent();
        }

		protected override void UnloadContent()
		{
			base.UnloadContent();
		}

        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;

            EngineSettings.Time = gameTime;

            if (EngineSettings.OnWindows)
                if (Keyboard.GetState().IsKeyDown(EngineSettings.Exitkey)) Exit();

			if (EngineSettings.OnWindows)
			{
				MouseHelper.Update();
				InputHelper.Update();
			}
            SceneManager.Instance.Update();
			FmodMediaPlayer.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SceneManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
