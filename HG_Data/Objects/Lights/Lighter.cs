/**************************************************************
 * (c) Joss Lattner 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using KryptonEngine.HG_Data;
using KryptonEngine.Manager;
using HanselAndGretel.Data;
using KryptonEngine;
using KryptonEngine.Rendering;

namespace HanselAndGretel.Data
{
    public class Lighter
    {
        #region Properties
        private AmbientLight mAmbientLight;
        private Effect mLightShader;
        private Effect mCombineShader;
        private float mRotationAngle;
        private float mInvert;
		
        #endregion

        #region Getter & Setter
        public AmbientLight AmbientLight { get { return this.mAmbientLight; } set { mAmbientLight = value; } }
        public float RotationAngle { get { return this.mRotationAngle; } set { this.mRotationAngle = value; } }
        public float Invert { set { this.mInvert = value; } }
        #endregion


        #region Constructor
        public Lighter()
        {
			Initialize();
        }

		public Lighter(AmbientLight pAl)
		{
			Initialize();
			this.AmbientLight = pAl;
		}
        #endregion

        #region Methods

		public void Initialize()
		{
			this.mLightShader = ShaderManager.Instance.GetElementByString("LightShader");
			this.mCombineShader = ShaderManager.Instance.GetElementByString("CombineShader");
		}

        public void GenerateLightMap(RenderTarget2D pLightMap, RenderTarget2D pNormalMap, RenderTarget2D pDepthMap, RenderTarget2D pAOMap, List<Light> pLightList)
        {
			EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(pLightMap);
			EngineSettings.Graphics.GraphicsDevice.Clear(Color.Transparent);

            EngineSettings.Graphics.GraphicsDevice.BlendState = BlendBlack;
            mLightShader.Parameters["NormalMap"].SetValue(pNormalMap);
			mLightShader.Parameters["DepthMap"].SetValue(pDepthMap);
			mLightShader.Parameters["DepthMap"].SetValue(pAOMap);
            
            foreach (Light l in pLightList)
            {
                if (!l.IsVisible) continue;

                mLightShader.Parameters["intensity"].SetValue(l.Intensity);
				mLightShader.Parameters["color"].SetValue(l.LightColor);
                mLightShader.Parameters["position"].SetValue(new Vector3(l.Position,l.Depth));
				mLightShader.Parameters["screen"].SetValue(new Vector2(EngineSettings.VirtualResWidth, EngineSettings.VirtualResHeight));
                //mLightShader.Parameters["invert"].SetValue(this.mInvert);
                
                if (l.GetType() == typeof(PointLight))
                {
                    PointLight tempPl = (PointLight)l;

                    mLightShader.Parameters["radius"].SetValue(tempPl.Radius);
                    mLightShader.CurrentTechnique.Passes[0].Apply();
                }

				//mQuadRenderer.Render();
            }
           
          
               // mLightShader.Parameters["ColorMap"].SetValue(pRenderDic["ColorMap"]);
               
                // Add Belding (Black background)
             

                // Draw some magic
              
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);
            
        }


        public void CombineMaps(RenderTarget2D pFinalMap, RenderTarget2D pLightMap, RenderTarget2D pDiffuseMap )
        {
            EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(pFinalMap);
			EngineSettings.Graphics.GraphicsDevice.Clear(Color.Transparent);
           // device.BlendState = LightBlend;

            
            mCombineShader.Parameters["ambientColor"].SetValue(AmbientLight.LightColor);
            mCombineShader.Parameters["ambientIntensity"].SetValue(AmbientLight.Intensity);
			mCombineShader.Parameters["LightMap"].SetValue(pLightMap);
			mCombineShader.Parameters["ColorMap"].SetValue(pDiffuseMap);
            

            mCombineShader.CurrentTechnique.Passes[0].Apply();

			//mQuadRenderer.Render();

			// Verursacht lilascreen
			//EngineSettings.Graphics.GraphicsDevice.SetRenderTarget(null);
            
        
        }
        #endregion

        public static BlendState LightBlend = new BlendState()
        {
            ColorBlendFunction = BlendFunction.ReverseSubtract,
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,

            AlphaBlendFunction = BlendFunction.ReverseSubtract,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend = Blend.One
        };

        public static BlendState BlendBlack = new BlendState()
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.SourceAlpha,
            AlphaDestinationBlend = Blend.One
        };
    }

   
}
