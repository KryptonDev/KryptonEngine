using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KryptonEngine.Rendering.Components;

namespace KryptonEngine.Rendering.Core
{
    public abstract class BaseRenderer
    {
        //Gebuffer hält verschiedenen Rendertargets, die gesetzt werden müssen
        private GBuffer mGBuffer;
        //private FPSCounter mFpsCounter;        
    }
}
