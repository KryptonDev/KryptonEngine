using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;


namespace Spine
{

    public struct NormalBatchItem
    {
        public Texture2D Texture;
        public VertexPositionTexture vertexTL;
        public VertexPositionTexture vertexTR;
        public VertexPositionTexture vertexBL;
        public VertexPositionTexture vertexBR;

    }

    class NormalBatcher
    {
        private const int InitialBatchSize = 256;
        private const int MaxBatchSize = short.MaxValue / 6;
        private const int InitialVertexArraySize = 256;

        private readonly List<NormalBatchItem> _batchItemList;
        private readonly Queue<NormalBatchItem> _freeBatchItemQueue;

        private short[] _index;

        private VertexPositionTexture [] _vertexArray;

        public NormalBatcher()
        {
            _batchItemList = new List<NormalBatchItem>(InitialBatchSize);
            _freeBatchItemQueue = new Queue<NormalBatchItem>(InitialBatchSize);

            EnsureArrayCapacity(InitialBatchSize);
        }

        public void Draw(GraphicsDevice device)
        {
            if (_batchItemList.Count == 0)
                return;

            int batchIndex = 0;
            int batchCount = _batchItemList.Count;

            while (batchCount > 0)
            {
                var startIndex = 0;
                var index = 0;
                Texture2D tex = null;

                int numBatchesToProcess = batchCount;
                if (numBatchesToProcess > MaxBatchSize)
                {
                    numBatchesToProcess = MaxBatchSize;
                }

                EnsureArrayCapacity(numBatchesToProcess);

                for (int i = 0; i < numBatchesToProcess; i++, batchIndex++)
                {
                    NormalBatchItem item = _batchItemList[batchIndex];
                    tex = item.Texture;
                    startIndex = index = 0;
                    device.Textures[0] = tex;

                    _vertexArray[index++] = item.vertexTL;
                    _vertexArray[index++] = item.vertexTR;
                    _vertexArray[index++] = item.vertexBL;
                    _vertexArray[index++] = item.vertexBR;

                }
                FlushVertexArray(device, startIndex, index);
               
                batchCount -= numBatchesToProcess;
            }
        }

        private void FlushVertexArray(GraphicsDevice device, int start, int end)
        {
            if (start == end)
                return;

            var vertexCount = end - start;

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertexArray, 0,vertexCount, _index, 0,(vertexCount / 4) * 2,VertexPositionTexture.VertexDeclaration);
        }

        public NormalBatchItem CreateBatchItem()
        {
            NormalBatchItem item;
            if(_freeBatchItemQueue.Count > 0)
            {
                item = _freeBatchItemQueue.Dequeue();
            }
            else { item = new NormalBatchItem(); }
            _batchItemList.Add(item);
            return item;
        }


        private void EnsureArrayCapacity(int numBatchItems)
        {
            int neededCapacity = 6 * numBatchItems;
            if (_index != null && neededCapacity <= _index.Length)
            {
                // Short circuit out of here because we have enough capacity.
                return;
            }

            short[] newIndex = new short[6 * numBatchItems];
            int start = 0;

            if (_index != null)
            {
                _index.CopyTo(newIndex, 0);
                start = _index.Length / 6;
            }

            for (var i = start; i < numBatchItems; i++)
            {
                /*
                 *  TL    TR
                 *   0----1 0,1,2,3 = index offsets for vertex indices
                 *   |   /| TL,TR,BL,BR are vertex references in SpriteBatchItem.
                 *   |  / |
                 *   | /  |
                 *   |/   |
                 *   2----3
                 *  BL    BR
                 */
                // Triangle 1
                newIndex[i * 6 + 0] = (short)(i * 4);
                newIndex[i * 6 + 1] = (short)(i * 4 + 1);
                newIndex[i * 6 + 2] = (short)(i * 4 + 2);
                // Triangle 2
                newIndex[i * 6 + 3] = (short)(i * 4 + 1);
                newIndex[i * 6 + 4] = (short)(i * 4 + 3);
                newIndex[i * 6 + 5] = (short)(i * 4 + 2);
            }
            _index = newIndex;

            _vertexArray = new VertexPositionTexture[4 * numBatchItems];
        }

    }

   
}
