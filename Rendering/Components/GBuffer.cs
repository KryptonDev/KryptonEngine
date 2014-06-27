using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering.Components
{
    public class GBuffer
    {
        #region Properties
        #region Rendertragets

        // Diffuse = 32 Byte (RGBA)
        // Normal  = 24 Byte (RGB)
        // AO = 8 Byte = (A)
        // Depth = 32 Byte (RGBA)

        //                
        // _________________________________________
        // | Diffuse | Diffuse | Diffuse |  Alpha  |
        // |    R    |    G    |    B    |    A    |
        // |  8Byte  |  8Byte  |  8Byte  |  8Byte  |
        // |_________|_________|_________|_________|
        // Diffuse RGB = 24 Byte
        // Diffuse A   =  8 Byte
        //               _______
        // Diffuse     = 32 Byte  

        // _________________________________________
        // |  Normal |  Normal |  Normal |   Ao    |
        // |    R    |    G    |    B    |    A    |
        // |  8Byte  |  8Byte  |  8Byte  |  8Byte  |
        // |_________|_________|_________|_________|
        // Normal XYZ  = 24 Byte
        // Ao     A    =  8 Byte
        //               _______
        // Normal+Ao   = 32 Byte  

        // _________________________________________
        // |                 Depth                 |
        // |                 RGBA                  |
        // |                 32Byte                |
        // |_______________________________________|
        // Depth RGBA  = 32 Byte
        //                _______
        // Depth       = 32 Byte  

        private RenderTarget2D mColorTarget;
        private RenderTarget2D mNormalTarget;
        private RenderTarget2D mDepthTarget;

        private Texture2D[] mRenderTargetTextureArray;
        #endregion


        private bool isGBufferSet = false;

        private GraphicsDevice mGraphicsDevice;

        private Effect mClearTargets;

       

        #endregion

        #region Getter und Setter

        public Texture2D[] RenderTargets
        {
            get
            {
                this.mRenderTargetTextureArray[0] = (Texture2D)this.mColorTarget;
                this.mRenderTargetTextureArray[1] = (Texture2D)this.mNormalTarget;
                this.mRenderTargetTextureArray[2] = (Texture2D)this.mDepthTarget;

                return this.mRenderTargetTextureArray;
            }
        }

        public bool IsGBufferActive { get { return isGBufferSet; } }

        #endregion


        #region Constructor
        public GBuffer(GraphicsDevice pGraphicsDevice)
        {
            this.mGraphicsDevice = pGraphicsDevice;

            int width = this.mGraphicsDevice.Viewport.Width;
            int height = this.mGraphicsDevice.Viewport.Height;

            this.mRenderTargetTextureArray = new Texture2D[3];

            this.mColorTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
            this.mNormalTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            this.mDepthTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
        }
        #endregion

         
        #region Methods

        public void SetGBuffer()
        {
            RenderTargetBinding[] renderTargets = new RenderTargetBinding[] { this.mColorTarget, this.mNormalTarget ,this.mDepthTarget};
            this.mGraphicsDevice.SetRenderTargets(renderTargets);
            isGBufferSet = true;
        }

        public void Clear()
        {
            if (!isGBufferSet) throw new Exception("GBuffer must be set before it can be Cleared!");
            KryptonEngine.EngineSettings.Graphics.GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Green, 0, 0);

            mClearTargets.CurrentTechnique.Passes[0].Apply();
            QuadRenderer.Render(mGraphicsDevice);
        }

        public void DisposeGBuffer()
        {
            this.mGraphicsDevice.SetRenderTarget(null);
            this.isGBufferSet = false;
        }


        public void LoadContent()
        {
            this.mClearTargets =  KryptonEngine.Manager.ShaderManager.Instance.GetElementByString("ClearRenderTargets");
        }
        #endregion





    }
}
