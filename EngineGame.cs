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

            SpineSettings.Setup();
        }

        protected override void Initialize()
        {
            base.Initialize();
            SceneManager.Instance.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(EngineSettings.Graphics.GraphicsDevice);

            TextureManager.Instance.LoadContent();
            InteractiveObjectDataManager.Instance.LoadContent();
            FontManager.Instance.LoadContent();
            SceneManager.Instance.LoadContent();
            SpineManager.Instance.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;

            EngineSettings.Time = gameTime;

            if (EngineSettings.OnWindows)
                if (Keyboard.GetState().IsKeyDown(EngineSettings.Exitkey)) Exit();

            if(EngineSettings.OnWindows)
                MouseHelper.Update();
            SceneManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SceneManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
