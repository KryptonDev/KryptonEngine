using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering.Components
{
    public class SpriteData
    {
        public int TextureID;
        public VertexPositionTexture vertexTL;
        public VertexPositionTexture vertexTR;
        public VertexPositionTexture vertexBL;
        public VertexPositionTexture vertexBR;
    }
}
