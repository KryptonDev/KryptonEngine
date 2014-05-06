/**************************************************************
 * (c) Carsten Baus, Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KryptonEngine.Entities
{
    public class Timer : GameObject
    {
        #region Properties

        protected double mStartTime;
        protected double mDuration;
        protected double mGoalTime;
        protected double mRestTime;

        protected bool mLooped = false;
        protected bool mFinished = false;
        protected bool mRunning = false;
        //public delegate void SelfDestroyer(object sender, EventArgs ea);
        //public event SelfDestroyer Destroy;

        #region Getter & Setter

        public double RestTimePercentage { get { return mRestTime / mDuration; } }
        public bool IsRunning { set { mRunning = value; } get { return mRunning; } }
        public bool Finish { get { return mFinished; } set { mFinished = value; } }

        #endregion
        
        #endregion

        #region Constructor

        public Timer() { }

        public Timer (double pDuration)
        {
            mDuration = pDuration;
        }

        public Timer(double pDuration, bool pLooped)
        {
            mDuration = pDuration;
            mLooped = pLooped;
        }

        #endregion

        #region Methods

        public bool IsTimerFinished()
        {
            if (mRunning)
            {
                CalculateRestTime();

                if (mRestTime < 0)
                {
                    mFinished = true;
                    mRunning = false;
                }
            }
            bool TmpFinished = mFinished;
            if (mLooped) //Loop Timer?
            {
                double TmpGoalTime = mGoalTime;
                ResetTimer();
                StartTimer();
                mGoalTime = TmpGoalTime; //Deutlich genauer, da Zeit zwischen alter mGoalTime und Aufrug von IsTimerFinished() wegfällt!
            }

            return TmpFinished;
        }

        public void StartTimer()
        {
            if (!mRunning)
            {
                mStartTime = EngineSettings.Time.TotalGameTime.TotalMilliseconds;
                mGoalTime = mStartTime + mDuration;
                mRunning = true;
            }
        }

        public String GetInfo()
        {
            return "StartTime = " + mStartTime + ".\nGoalTime = " + mGoalTime + "\nDuration = " + mDuration + "\nRestTime = " + mRestTime + "\nPercent = " + RestTimePercentage + "\nFinished : " + mFinished;
        }

        public void CalculateRestTime()
        {
            mRestTime = mGoalTime - EngineSettings.Time.TotalGameTime.TotalMilliseconds;
            if (mRestTime < 0)
            {
                mFinished = true;
                mRunning = false;
            }
        }

        public void ResetTimer()
        {
            mStartTime = EngineSettings.Time.TotalGameTime.TotalMilliseconds;
            mGoalTime = mStartTime + mDuration;
            mFinished = false;
            mRunning = false;
        }

        #endregion
    }

}
