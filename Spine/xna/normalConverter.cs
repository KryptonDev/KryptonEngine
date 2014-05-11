using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Spine
{
   public class NormalConverter
    {
        GraphicsDevice device;
        NormalBatcher batcher;
        float[] vertices = new float[8];
        public Effect normalConvertShader;

        public NormalConverter(GraphicsDevice device)
        {
            this.device = device;
            batcher = new NormalBatcher();
        }

        public Texture2D convertNormalMap(Skeleton skeleton,Texture2D normalMap)
        {
            List<Slot> drawOrder = skeleton.DrawOrder;
            float rotateRad;
            Texture2D map = normalMap;
            for (int i = 0, n = drawOrder.Count; i < n; i++) 
            {
                Slot slot = drawOrder[i];
                RegionAttachment regionAttachment = slot.Attachment as RegionAttachment;
                
                if (regionAttachment != null)
                {

                    NormalBatchItem item = batcher.CreateBatchItem();
                    AtlasRegion region = (AtlasRegion)regionAttachment.RendererObject;
                    item.Texture = normalMap;
                    device.Textures[2] = map;

                    float[] uvs = regionAttachment.UVs;
					item.vertexTL.TextureCoordinate.X = uvs[RegionAttachment.X1];
					item.vertexTL.TextureCoordinate.Y = uvs[RegionAttachment.Y1];
					item.vertexBL.TextureCoordinate.X = uvs[RegionAttachment.X2];
					item.vertexBL.TextureCoordinate.Y = uvs[RegionAttachment.Y2];
					item.vertexBR.TextureCoordinate.X = uvs[RegionAttachment.X3];
					item.vertexBR.TextureCoordinate.Y = uvs[RegionAttachment.Y3];
					item.vertexTR.TextureCoordinate.X = uvs[RegionAttachment.X4];
					item.vertexTR.TextureCoordinate.Y = uvs[RegionAttachment.Y4];

                    if(region.rotate) rotateRad = 90f * 3.14159264f/180f;
                    else rotateRad = 0f;

                    normalConvertShader.Parameters["rotationAngle"].SetValue(rotateRad);
                        
                }

            }

            normalConvertShader.CurrentTechnique.Passes[0].Apply();
            batcher.Draw(device);

            return map;
        }
    }
}
