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

        private GraphicsDevice mGraphicsDevice;

        private int mCurrentTextureID;

        private List<Texture2D> mDiffuseTextureBuffer;
        private List<Texture2D> mNormalTextureBuffer;
        private List<Texture2D> mAoTextureBuffer;
        private List<Texture2D> mDepthTextureBuffer;

        private List<SpriteData> mBatchItems;
        private Queue<SpriteData> mFreeItems;


        private List< List <VertexPositionTexture>> mVertexDataBuffer;
        
        private VertexPositionTexture[] mVertexBuffer;
        private int[] mIndexBuffer;

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
            if (mBatchItems.Count == 0)
                return;

            int batchCount = this.mBatchItems.Count;

            while(batchCount > 0)
            {
                short startIndex   = 0;
                short currentIndex = 0;

                int batchesToProcess = batchCount;
                EnsureIndexArraySize(batchesToProcess);

                for(int i = 0; i < batchesToProcess;i++)
                {
                    SpriteData item = mBatchItems[i];

                    if(mVertexDataBuffer.Count-1 < item.TextureID)
                        mVertexDataBuffer.Add(new List<VertexPositionTexture>());
                        

                    mVertexDataBuffer[item.TextureID].Add(item.vertexTL);
                    mVertexDataBuffer[item.TextureID].Add(item.vertexTR);
                    mVertexDataBuffer[item.TextureID].Add(item.vertexBL);
                    mVertexDataBuffer[item.TextureID].Add(item.vertexBR);

                    //this.mVertexBuffer[currentIndex++] = item.vertexTL;
                    //this.mVertexBuffer[currentIndex++] = item.vertexTR;
                    //this.mVertexBuffer[currentIndex++] = item.vertexBL;
                    //this.mVertexBuffer[currentIndex++] = item.vertexBR;

                    this.mFreeItems.Enqueue(item);
                }

                Flush();
                batchCount -= batchesToProcess;
            }

            mBatchItems.Clear();
        }

        public void Flush()
        {
            int vertexCount = 0;
           for(int i = 0; i < this.mVertexDataBuffer.Count; i++)
           {
               mGraphicsDevice.Textures[1] = mDiffuseTextureBuffer[i];
               vertexCount = this.mVertexDataBuffer[i].Count;
               EnsureIndexArraySize(vertexCount);
               mVertexBuffer = this.mVertexDataBuffer[i].ToArray();
               this.mGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, this.mVertexBuffer, 0, vertexCount, this.mIndexBuffer, 0, (vertexCount / 4) * 2, VertexPositionTexture.VertexDeclaration);
      
           }
            

        }
        #endregion

        #region Methods

        public SpriteData createBatchItem()
        {
            SpriteData item;

            //if (_freeBatchItemQueue.Count > 0)
            //    item = mFreeBatchItems.Dequeue();
            //else
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

        private void EnsureIndexArraySize(int itemAmount)
        {

            int[] newIndex = new int[6 * itemAmount];
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
            mVertexBuffer = new VertexPositionTexture[4 * itemAmount];
        }
        #endregion

    }
}
