using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
    public class RawSpineData
    {
        #region Properties

        public SkeletonRenderer skeletonRenderer;
        public Atlas atlas;
        public SkeletonJson json;
        
        #endregion

        #region Constructor

        public RawSpineData(string pSkeletonName)
        {
            skeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
            skeletonRenderer.PremultipliedAlpha = SpineSettings.PremultipliedAlphaRendering;
            atlas = new Atlas(SpineSettings.DefaultDataPath + pSkeletonName + ".atlas", new XnaTextureLoader(EngineSettings.Graphics.GraphicsDevice));
            json = new SkeletonJson(atlas);
        }

        public RawSpineData(string pSkeletonDataPath, string pSkeletonName)
        {
            skeletonRenderer = new SkeletonRenderer(EngineSettings.Graphics.GraphicsDevice);
            skeletonRenderer.PremultipliedAlpha = SpineSettings.PremultipliedAlphaRendering;
            atlas = new Atlas(pSkeletonDataPath + pSkeletonName + ".atlas", new XnaTextureLoader(EngineSettings.Graphics.GraphicsDevice));
            json = new SkeletonJson(atlas);
        }

        #endregion
    }
}
