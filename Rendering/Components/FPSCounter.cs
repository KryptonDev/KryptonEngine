using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Rendering.Components
{
    public class FPSCounter
    {
        #region Properties

        private int mFrameRate    = 0;
        private int mFrameCounter = 0;

        private TimeSpan mElapsedTime = TimeSpan.Zero;
        #endregion

        #region Getter and Setter
        public int FPS { get { return mFrameRate; } }
        #endregion

        #region Methods
        public void Update()
        {
            mElapsedTime += KryptonEngine.EngineSettings.Time.ElapsedGameTime;

            if(mElapsedTime > TimeSpan.FromSeconds(1))
            {
                mElapsedTime -= TimeSpan.FromSeconds(1);
                mFrameRate    = mFrameCounter;
                mFrameCounter = 0;
            }
      
        }

        public void UpdateDrawCounter()
        {
            mFrameCounter++;
        }
        #endregion


    }
}
