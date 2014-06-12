using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering.Components
{
    public class GBuffer
    {
        #region properties
        #region Rendertragets

        // Diffuse = 32 Byte (RGBA)
        // Normal  = 24 Byte (RGB)
        // AO = 8 Byte = (A)
        // Depth = 32 Byte (RGBA)
        private RenderTarget2D mColorTarget;
        private RenderTarget2D mNormalTarget;
        private RenderTarget2D mDepthTarget;
        #endregion


        private bool isGBufferSet = false;

        private GraphicsDevice mGraphicsDevice;

        private Effect mClearTargets;

       

        #endregion

        #region Getter und Setter
        #endregion


        #region Constructor
        public GBuffer(GraphicsDevice pGraphicsDevice)
        {
            this.mGraphicsDevice = pGraphicsDevice;

            int width = this.mGraphicsDevice.Viewport.Width;
            int height = this.mGraphicsDevice.Viewport.Height;


            this.mColorTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            this.mNormalTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None);
            this.mDepthTarget = new RenderTarget2D(this.mGraphicsDevice, width, height, false, SurfaceFormat.Single, DepthFormat.None);

            


        }
        #endregion


        #region Methods

        public void SetGBuffer()
        {
            RenderTargetBinding[] renderTargets = new RenderTargetBinding[] { this.mColorTarget, this.mNormalTarget, this.mDepthTarget};
            this.mGraphicsDevice.SetRenderTargets(renderTargets);
            isGBufferSet = true;
        }

        public void Clear()
        {
            if (!isGBufferSet) throw new Exception("GBuffer must be set before it can be Cleared!");
            mClearTargets.CurrentTechnique.Passes[0].Apply();
            QuadRenderer.Render(mGraphicsDevice);
        }

        public void DisposeGBuffer()
        {
            this.mGraphicsDevice.SetRenderTarget(null);
            this.isGBufferSet = false;
        }

        #endregion





    }
}
