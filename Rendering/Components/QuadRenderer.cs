using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering.Components
{
    public static class QuadRenderer
    {
        #region Properties
        static readonly VertexPositionTexture[] mVertexBuffer = null;
        static readonly short[] mIndexBuffer = null;
        #endregion

        #region Constructor
       static QuadRenderer()
        {
            mVertexBuffer = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1,1,0),new Vector2(0,0)),
				new VertexPositionTexture(new Vector3(1,1,0),new Vector2(1,0)),
				new VertexPositionTexture(new Vector3(-1,-1,0),new Vector2(0,1)),
				new VertexPositionTexture(new Vector3(1,-1,0),new Vector2(1,1)),
            };

            mIndexBuffer = new short[] { 0, 1, 2, 1, 3, 2 };
        }

        #endregion

        #region Method

        public static void Render(GraphicsDevice pGraphicsDevice)
        {
            pGraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, mVertexBuffer, 0, 4, mIndexBuffer, 0, 2, VertexPositionTexture.VertexDeclaration);
        }

        #endregion

    }
}
