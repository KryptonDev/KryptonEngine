using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Rendering.Components;

namespace KryptonEngine.Rendering.RenderModuls
{
    class Batch
    {

        #region Properties

        public int TextCount;
        
        private GraphicsDevice mGraphicsDevice;

        private int mCurrentTextureID;

        public List<Texture2D> mDiffuseTextureBuffer;
        public List<Texture2D> mNormalTextureBuffer;
        public List<Texture2D> mAoTextureBuffer;
        public List<Texture2D> mDepthTextureBuffer;

        public List<SpriteData> mBatchItems;
        public Queue<SpriteData> mFreeItems;


        public List<List<VertexPositionTexture>> mVertexDataBuffer;

        public VertexPositionTexture[] mVertexBuffer;
        public int[] mIndexBuffer;

        #endregion

        #region Constructor

        public Batch(GraphicsDevice pGraphicsDevice)
        {
            this.mGraphicsDevice = pGraphicsDevice;

            this.mVertexDataBuffer = new List<List<VertexPositionTexture>>();

            this.mBatchItems = new List<SpriteData>();
            this.mFreeItems = new Queue<SpriteData>();

            this.mDiffuseTextureBuffer = new List<Texture2D>();
            this.mNormalTextureBuffer = new List<Texture2D>();
            this.mAoTextureBuffer = new List<Texture2D>();
            this.mDepthTextureBuffer = new List<Texture2D>();
        }

        #endregion

        #region Render Methods

        public void Render()
        {
            Texture2D testTexture = null;
            if (mBatchItems.Count == 0)
                return;

            int batchCount = this.mBatchItems.Count;

            while(batchCount > 0)
            {
                short startIndex   = 0;
                short currentIndex = 0;
                int offset = 0;
                int currentTextureId = 0;

                int batchesToProcess = batchCount;
                EnsureIndexArraySize(batchesToProcess);

                for(int i = 0; i < batchesToProcess;i++)
                {
                    SpriteData item = mBatchItems[i];

                    //if(mVertexDataBuffer.Count-1 < item.TextureID)
                    //    mVertexDataBuffer.Add(new List<VertexPositionTexture>());
                        
                    //mVertexDataBuffer[item.TextureID].Add(item.vertexTL);
                    //mVertexDataBuffer[item.TextureID].Add(item.vertexTR);
                    //mVertexDataBuffer[item.TextureID].Add(item.vertexBL);
                    //mVertexDataBuffer[item.TextureID].Add(item.vertexBR);

                    if (!ReferenceEquals(mDiffuseTextureBuffer[item.TextureID], testTexture))
                    {
                        if (i > offset)
                        {
                            this.Flush(currentTextureId, offset, i - offset);
                        }
                        offset = i;
                        currentTextureId = item.TextureID;
                        currentIndex = 0;
                        testTexture = mDiffuseTextureBuffer[item.TextureID];
                    }

                    this.mVertexBuffer[currentIndex++] = item.vertexTL;
                    this.mVertexBuffer[currentIndex++] = item.vertexTR;
                    this.mVertexBuffer[currentIndex++] = item.vertexBL;
                    this.mVertexBuffer[currentIndex++] = item.vertexBR;

                    this.mFreeItems.Enqueue(item);
                }

                Flush(currentTextureId,offset, batchesToProcess-offset);
                batchCount -= batchesToProcess;
            }

            this.TextCount = mDiffuseTextureBuffer.Count;

            mBatchItems.Clear();
            this.clearTextures();
        }

        public void Flush(int TextureID, int offset, int count)
        {
           int vertexCount = count*4;
           int indexCount  = count*2;

           int vertexOffset = offset * 4;
           int indexOffset  = offset * 6;

           
           mGraphicsDevice.Textures[1] = mDiffuseTextureBuffer[TextureID];
           mGraphicsDevice.Textures[2] = mNormalTextureBuffer[TextureID];
           mGraphicsDevice.Textures[3] = mAoTextureBuffer[TextureID];
           mGraphicsDevice.Textures[4] = mDepthTextureBuffer[TextureID];

           this.mGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                                                          this.mVertexBuffer, 0, vertexCount,
                                                          this.mIndexBuffer, 0, indexCount,
                                                          VertexPositionTexture.VertexDeclaration);

        }
        #endregion

        #region Methods

        public SpriteData createBatchItem()
        {
            SpriteData item;

            if (mFreeItems.Count > 0)
                item = mFreeItems.Dequeue();
            else
                item = new SpriteData();
            this.mBatchItems.Add(item);
            return item;

        }

        public int getTextureIndex(Texture2D pTexture)
        {
            return mDiffuseTextureBuffer.IndexOf(pTexture);
        }

        public int AddTextures(Texture2D[] pTextureArray)
        {
            this.mDiffuseTextureBuffer.Add(pTextureArray[0]);
            this.mNormalTextureBuffer.Add(pTextureArray[1]);
            this.mAoTextureBuffer.Add(pTextureArray[2]);
            this.mDepthTextureBuffer.Add(pTextureArray[3]);

            return this.mDiffuseTextureBuffer.Count - 1;
        }

        public void clearTextures()
        {
            this.mDiffuseTextureBuffer.Clear();
            this.mNormalTextureBuffer.Clear();
            this.mAoTextureBuffer.Clear();
            this.mDepthTextureBuffer.Clear();
        }

        private void EnsureIndexArraySize(int itemAmount)
        {

            int[] newIndex = new int[6 * (itemAmount)];
            int start = 0;

            //if (mIndexBuffer != null)
            //{
            //    mIndexBuffer.CopyTo(newIndex, 0);
            //    start = mIndexBuffer.Length / 6;
            //}

            for (int i = start; i < itemAmount; i++)
            {
                // Triangle 1
                newIndex[i * 6 + 0] = (short)(i * 4);
                newIndex[i * 6 + 1] = (short)(i * 4 + 1);
                newIndex[i * 6 + 2] = (short)(i * 4 + 2);

                // Triangle 2
                newIndex[i * 6 + 3] = (short)(i * 4 + 1);
                newIndex[i * 6 + 4] = (short)(i * 4 + 3);
                newIndex[i * 6 + 5] = (short)(i * 4 + 2);
            }

            mIndexBuffer = newIndex;
            mVertexBuffer = new VertexPositionTexture[itemAmount*4];
        }
        #endregion

    }
}
