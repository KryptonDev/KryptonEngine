using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Rendering.Components;
using KryptonEngine.Rendering.Core;

namespace KryptonEngine.Rendering
{
    
    public class TwoDRenderer : BaseRenderer 
    {
        #region Properties
        private Batch mBatch;

        private GraphicsDevice mGraphicsDevice;

        private RasterizerState mRasterizerState;
        private BlendState mLightMapBlendState;
        private BlendState mAlphaBlend;
        private DepthStencilState mDepthStencilState;

        private Effect mDraw;
        private Effect mLightShader;
        private Effect mCombineShader;

        private RenderTarget2D mLightMap;
        private RenderTarget2D mFinalMap;

        private GBuffer mGBuffer;

        private Matrix mWorld;
        private Matrix mView;
        private Matrix mTranslatetViewMatrix;
        private Matrix mProjection;

        private bool isBegin = false;

        private Texture2D[] mTextureArray;

        private int mPlaneHeight;

        private float[] mSkeletonVertecies = new float[8];

        private FPSCounter mFPSCounter;
        #endregion

        #region Getter & Setter
        public int maxHeight { set { this.mPlaneHeight = value; } }
        #endregion

        #region Constructor

        public TwoDRenderer(GraphicsDevice pGraphicsDevice)
        {
            this.mGraphicsDevice = pGraphicsDevice;
            Initialize(this.mGraphicsDevice.Viewport.Width, this.mGraphicsDevice.Viewport.Height);
        }

        public TwoDRenderer(GraphicsDevice pGraphicsDevice, int pWidth, int pHeight)
        {
            this.mGraphicsDevice = pGraphicsDevice;
            Initialize(pWidth, pHeight);
        }

        #endregion

        #region Class Methods

        public void Initialize(int pWidth, int pHeight)
        {
            this.mFPSCounter = new FPSCounter();

            this.mRasterizerState = new RasterizerState();
            this.mRasterizerState.CullMode = CullMode.None;

            this.mLightMapBlendState = new BlendState();
            this.mLightMapBlendState.ColorSourceBlend = Blend.One;
            this.mLightMapBlendState.ColorDestinationBlend = Blend.One;
            this.mLightMapBlendState.ColorBlendFunction = BlendFunction.Add;
            this.mLightMapBlendState.AlphaSourceBlend = Blend.One;
            this.mLightMapBlendState.AlphaDestinationBlend = Blend.One;
            this.mLightMapBlendState.AlphaBlendFunction = BlendFunction.Add;



            this.mAlphaBlend = BlendState.AlphaBlend;

            this.mDepthStencilState = new DepthStencilState();
            this.mDepthStencilState.DepthBufferWriteEnable = false;
            this.mDepthStencilState.DepthBufferEnable = false;
            this.mDepthStencilState.DepthBufferFunction = CompareFunction.GreaterEqual;

           // this.mAlphaBlend = new BlendState();

            //this.mAlphaBlend.ColorDestinationBlend = Blend.InverseSourceAlpha;
            //this.mAlphaBlend.ColorSourceBlend = Blend.SourceAlpha;

            //this.mAlphaBlend.AlphaSourceBlend = Blend.Zero;
            //this.mAlphaBlend.AlphaDestinationBlend = Blend.Zero;

            //this.mAlphaBlend.ColorBlendFunction = BlendFunction.Add;
            //this.mAlphaBlend.AlphaBlendFunction = BlendFunction.Add;




           


            this.mLightMap = new RenderTarget2D(this.mGraphicsDevice, pWidth, pHeight, false, SurfaceFormat.Color, DepthFormat.None);
            this.mFinalMap = new RenderTarget2D(this.mGraphicsDevice, pWidth, pHeight, false, SurfaceFormat.Color, DepthFormat.None);

            this.mView  = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Zero, Vector3.Up);
            this.mTranslatetViewMatrix = this.mView;

            this.mWorld = Matrix.Identity;

            this.mGBuffer = new GBuffer(this.mGraphicsDevice);

            this.mTextureArray = new Texture2D[4];
            this.mBatch = new Batch(this.mGraphicsDevice);
        }

        public void UpdateFPSCounter()
        {
            this.mFPSCounter.Update();
        }

        public void UpdateFPSDrawCounter()
        {
            this.mFPSCounter.UpdateDrawCounter();
        }

        public void LoadContent()
        {
            this.mLightShader = KryptonEngine.Manager.ShaderManager.Instance.GetElementByString("Light");
            this.mCombineShader = KryptonEngine.Manager.ShaderManager.Instance.GetElementByString("Combine");
            this.mDraw = KryptonEngine.Manager.ShaderManager.Instance.GetElementByString("MRT");

            this.mGBuffer.LoadContent();
        }
        #endregion

        #region Render Methods

        #region Begin
        public void Begin()
        {
            this.Begin(Matrix.Identity);
        }

        public void Begin( Matrix pTranslation)
        {
            if (this.isBegin) throw new InvalidOperationException("End must called before Beginn");

            // Setzt States für die Grafikkarte
            //  Rasterizer Statet Culled die Vertecies
            // Blendstate ist für Alphatesting
            // DepthStenciel für draw reihen folge

            this.mGraphicsDevice.RasterizerState = this.mRasterizerState;
            this.mGraphicsDevice.BlendState = this.mAlphaBlend;
            this.mGraphicsDevice.DepthStencilState = this.mDepthStencilState;

            this.mTranslatetViewMatrix = Matrix.Multiply(mView, pTranslation);
            this.mProjection = Matrix.CreateOrthographicOffCenter(0, this.mGraphicsDevice.Viewport.Width, this.mGraphicsDevice.Viewport.Height, 0, 1f, 0f);

            this.isBegin = true;
        }

        #endregion

        #region Draw

        #region Draw Skelleton

        public void Draw(Skeleton pSkeleton,Texture2D[] pMapArray,float pDepth = 1.0f,float pScale = 1.0f)
        {
            this.mTextureArray[0] = pMapArray[0];
            this.mTextureArray[1] = pMapArray[1];
            this.mTextureArray[2] = pMapArray[2];
            this.mTextureArray[3] = pMapArray[3];

            this.InternalDraw(pSkeleton, this.mTextureArray, pDepth,pScale);
        }

        #endregion

        #region Draw Sprite
        public void Draw(Texture2D[] pMapArray,Vector2 pPosition)
        {
            Vector4 vector = default(Vector4);
            vector.X = pPosition.X;
            vector.Y = pPosition.Y;
            vector.Z = pPosition.Y/this.mPlaneHeight;
            vector.W = 1.0f;

			this.mTextureArray[0] = pMapArray[0];
            this.mTextureArray[1] = pMapArray[1];
            this.mTextureArray[2] = pMapArray[2];
            this.mTextureArray[3] = pMapArray[3];

            this.InternalDraw(this.mTextureArray, vector);
        }

        public void Draw(Texture2D[] pMapArray, Vector2 pPosition,float pScale)
        {
            Vector4 vector = default(Vector4);
            vector.X = pPosition.X;
            vector.Y = pPosition.Y;
            vector.Z = pPosition.Y / this.mPlaneHeight;
            vector.W = pScale;

			this.mTextureArray[0] = pMapArray[0];
			this.mTextureArray[1] = pMapArray[1];
			this.mTextureArray[2] = pMapArray[2];
			this.mTextureArray[3] = pMapArray[3];

            this.InternalDraw(this.mTextureArray, vector);
        }

        public void Draw(Texture2D[] pMapArray, Vector3 pPosition)
        {
            Vector4 vector = default(Vector4);
            vector.X = pPosition.X;
            vector.Y = pPosition.Y;
            vector.Z = pPosition.Z;
            vector.W = 1.0f;

			this.mTextureArray[0] = pMapArray[0];
			this.mTextureArray[1] = pMapArray[1];
			this.mTextureArray[2] = pMapArray[2];
			this.mTextureArray[3] = pMapArray[3];

            this.InternalDraw(this.mTextureArray, vector);
        }

        public void Draw(Texture2D[] pMapArray, Vector3 pPosition, float pScale)
        {
            Vector4 vector = default(Vector4);
            vector.X = pPosition.X;
            vector.Y = pPosition.Y;
            vector.Z = pPosition.Z;
            vector.W = pScale;

			this.mTextureArray[0] = pMapArray[0];
			this.mTextureArray[1] = pMapArray[1];
			this.mTextureArray[2] = pMapArray[2];
			this.mTextureArray[3] = pMapArray[3];

            this.InternalDraw(this.mTextureArray, vector);
        }
        #endregion


        #region InternalDraw
        private void InternalDraw(Texture2D[] pTextureArray, Vector4 pDestination)
        {
            if (!isBegin) throw new Exception("Beginn muss vor Draw aufgerufen werden!");
            SpriteData item = this.mBatch.createBatchItem();

            float scale = pDestination.W;
            int width = (int)(pTextureArray[0].Width * scale);
            int height = (int)(pTextureArray[0].Height * scale);
            item.TextureID = this.mBatch.AddTextures(pTextureArray);

            item.vertexTL.Position.X = pDestination.X;
            item.vertexTL.Position.Y = pDestination.Y;
            item.vertexTL.Position.Z = pDestination.Z;

            item.vertexTR.Position.X = pDestination.X+width;
            item.vertexTR.Position.Y = pDestination.Y;
            item.vertexTR.Position.Z = pDestination.Z;


            item.vertexBL.Position.X = pDestination.X;
            item.vertexBL.Position.Y = pDestination.Y + height;
            item.vertexBL.Position.Z = pDestination.Z;

            item.vertexBR.Position.X = pDestination.X + width;
            item.vertexBR.Position.Y = pDestination.Y + height;
            item.vertexBR.Position.Z = pDestination.Z;


            item.vertexTL.TextureCoordinate.X = 0;
            item.vertexTL.TextureCoordinate.Y = 0;

            item.vertexTR.TextureCoordinate.X = 1;
            item.vertexTR.TextureCoordinate.Y = 0;

            item.vertexBL.TextureCoordinate.X = 0;
            item.vertexBL.TextureCoordinate.Y = 1;

            item.vertexBR.TextureCoordinate.X = 1;
            item.vertexBR.TextureCoordinate.Y = 1;


            //if ( textId == -1) 
            //{
            //   textId = this.mBatch.AddTextures(pTextureArray);
            //}

            //item.TextureID = textId;

        }

        private void InternalDraw(Skeleton pSkeleton,Texture2D[] pTextureArray,float pDepth ,float scale)
        {
            if (!isBegin) throw new Exception("Beginn muss vor Draw aufgerufen werden!");
            List<Slot> drawOrder = pSkeleton.DrawOrder;
            float x = pSkeleton.X, y = pSkeleton.Y;
			int textId =  this.mBatch.AddTextures(pTextureArray);
            float orderDepth = 0.00000000001f;
            for (int i = 0, n = drawOrder.Count; i < n; i++)
            {
                Slot slot = drawOrder[i];
                RegionAttachment regionAttachment = slot.Attachment as RegionAttachment;
                if (regionAttachment != null)
                {
                  
                    SpriteData item = this.mBatch.createBatchItem();
                    AtlasRegion region = (AtlasRegion)regionAttachment.RendererObject;
                   // pTextureArray[0] = pTextureArray[0] != (Texture2D)region.page.rendererObject ?  (Texture2D)region.page.rendererObject : pTextureArray[0];

                    float[] vertices = this.mSkeletonVertecies;
                    regionAttachment.ComputeWorldVertices(x, y, slot.Bone, vertices);
                    item.vertexTL.Position.X = vertices[RegionAttachment.X1];
                    item.vertexTL.Position.Y = vertices[RegionAttachment.Y1];
					item.vertexTL.Position.Z = pDepth;// -orderDepth * (n - (i + 1));
                    item.vertexBL.Position.X = vertices[RegionAttachment.X2];
                    item.vertexBL.Position.Y = vertices[RegionAttachment.Y2];
					item.vertexBL.Position.Z = pDepth;// -orderDepth * (n - (i + 1));
                    item.vertexBR.Position.X = vertices[RegionAttachment.X3];
                    item.vertexBR.Position.Y = vertices[RegionAttachment.Y3];
					item.vertexBR.Position.Z = pDepth;// -orderDepth * (n - (i + 1));
                    item.vertexTR.Position.X = vertices[RegionAttachment.X4];
                    item.vertexTR.Position.Y = vertices[RegionAttachment.Y4];
					item.vertexTR.Position.Z = pDepth;// -orderDepth * (n - (i + 1));

                    float[] uvs = regionAttachment.UVs;
                    item.vertexTL.TextureCoordinate.X = uvs[RegionAttachment.X1];
                    item.vertexTL.TextureCoordinate.Y = uvs[RegionAttachment.Y1];
                    item.vertexBL.TextureCoordinate.X = uvs[RegionAttachment.X2];
                    item.vertexBL.TextureCoordinate.Y = uvs[RegionAttachment.Y2];
                    item.vertexBR.TextureCoordinate.X = uvs[RegionAttachment.X3];
                    item.vertexBR.TextureCoordinate.Y = uvs[RegionAttachment.Y3];
                    item.vertexTR.TextureCoordinate.X = uvs[RegionAttachment.X4];
                    item.vertexTR.TextureCoordinate.Y = uvs[RegionAttachment.Y4];

                    //if (textId == -1)
                    {
                        
                    }

                    item.TextureID = textId;
                    orderDepth += 0.00001f;
                }


            }
        }
        #endregion

        #endregion

        #region End
        public void End()
        {
            if (!this.isBegin) throw new InvalidOperationException("Beginn must called before End");

            this.mDraw.Parameters["World"].SetValue(this.mWorld);
            this.mDraw.Parameters["View"].SetValue(this.mTranslatetViewMatrix);
            this.mDraw.Parameters["Projection"].SetValue(this.mProjection);
            this.mDraw.CurrentTechnique.Passes[0].Apply();

            this.mBatch.Render();
            this.isBegin = false;
        }
        #endregion
        #endregion

        #region Function Methods

        public void SetGBuffer()
        {
            this.mGBuffer.SetGBuffer();
        }

        public void DisposeGBuffer()
        {
            this.mGBuffer.DisposeGBuffer();
        }

        public void ClearGBuffer()
        {
            this.mGBuffer.Clear();
        }

		public Texture2D GetRenderTargetTexture(int index)
		{
			return mGBuffer.RenderTargets[index];
		}
        #endregion

        #region Debug
        public void DrawDebugRendertargets(SpriteBatch batch)
        {
            int width = mGraphicsDevice.Viewport.Width;
            int height = mGraphicsDevice.Viewport.Height;
            int smallWidth  = width / 4;
            int smallHeigth = height / 4;

            batch.Begin(SpriteSortMode.Texture, BlendState.Opaque, SamplerState.PointClamp,DepthStencilState.None,RasterizerState.CullNone);
            batch.Draw(this.mGBuffer.RenderTargets[0], new Rectangle(smallWidth * 0, height - smallHeigth, smallWidth, smallHeigth), Color.White);
            batch.Draw(this.mGBuffer.RenderTargets[1], new Rectangle(smallWidth * 1, height - smallHeigth, smallWidth, smallHeigth), Color.White);
            batch.Draw(this.mGBuffer.RenderTargets[2], new Rectangle(smallWidth * 2, height - smallHeigth, smallWidth, smallHeigth), Color.White);
           // batch.Draw(this.mGraphicsDevice., new Rectangle(smallWidth * 3, height - smallHeigth, smallWidth, smallHeigth), Color.White);

            batch.DrawString(KryptonEngine.Manager.FontManager.Instance.GetElementByString("font"),this.mFPSCounter.FPS.ToString() + " FPS", Vector2.Zero, Color.White);
            batch.DrawString(KryptonEngine.Manager.FontManager.Instance.GetElementByString("font"), this.mBatch.mDiffuseTextureBuffer.Count.ToString() + " Textures", new Vector2(0,20), Color.White);
            batch.DrawString(KryptonEngine.Manager.FontManager.Instance.GetElementByString("font"), this.mBatch.mFreeItems.Count.ToString() + " Free Items", new Vector2(0, 40), Color.White);
            batch.DrawString(KryptonEngine.Manager.FontManager.Instance.GetElementByString("font"), this.mBatch.mVertexDataBuffer.Count.ToString() + " VertexData", new Vector2(0, 60), Color.White);
            
            batch.End();
        }
        
        public void DrawRenderTargetOnScreen(SpriteBatch batch,int index)
        {
            batch.Begin();
            batch.Draw(this.mGBuffer.RenderTargets[index], Vector2.Zero, Color.White);
            batch.End();
        }
        #endregion
    }
}
