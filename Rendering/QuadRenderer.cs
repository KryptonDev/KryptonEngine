using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering
{
    public class QuadRenderer
    {

        #region Properties
        private readonly VertexPositionTexture[] mVertexBuffer = null;
        private readonly short[] mIndexBuffer = null;
        #endregion

        #region Constructor
        public QuadRenderer()
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

        public void Render()
        {
            EngineSettings.Graphics.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
				mVertexBuffer, 0, 4, mIndexBuffer, 0, 2,
				VertexPositionTexture.VertexDeclaration);
        }

        #endregion

    }
}
