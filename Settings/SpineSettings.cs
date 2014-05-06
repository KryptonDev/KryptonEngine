/**************************************************************
 * (c) Jens Richter 2014
 *************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Spine;

namespace KryptonEngine
{
    public static class SpineSettings
    {
        private struct AnimationMix
        {
            #region Properties

            private string from;
            private string to;
            private float fading;

            public string From { get { return from; } set { from = value; } }
            public string To { get { return to; } set { to = value; } }
            public float Fading { get { return fading; } set { fading = Math.Abs(value); } }

            #endregion

            #region Constructor

            public AnimationMix(string pFrom, string pTo)
            {
                from = pFrom;
                to = pTo;
                fading = DefaultFading;
            }

            public AnimationMix(string pFrom, string pTo, float pFading)
            {
                from = pFrom;
                to = pTo;
                fading = pFading;
            }

            #endregion
        }

        #region Properties

        private static Dictionary<String, List<AnimationMix>> AnimationFading = new Dictionary<string, List<AnimationMix>>();
        private static Dictionary<String, float> Scaling = new Dictionary<string, float>();

        public static float DefaultFading = 0.2f;
        public static bool PremultipliedAlphaRendering = true;
        public static string DefaultDataPath = "Content/spine/";

        #endregion

        #region Methods

        /// <summary>
        /// Läd die Settings für animationStateData.SetMix() in das AnimationFading-Dictionary.
        /// </summary>
        public static void Setup()
        {
            List<AnimationMix> AnimationFadingList; //Zu bearbeitende Liste, damit die nicht immer neu im Dictionary nachgeschlagen werden muss

            #region Fluffy
            Scaling.Add("fluffy", 1.0f);
            AnimationFading.Add("fluffy", new List<AnimationMix>());
            AnimationFadingList = AnimationFading["fluffy"];

            AnimationFadingList.Add(new AnimationMix("attack", "die"));
            AnimationFadingList.Add(new AnimationMix("attack", "die"));
            AnimationFadingList.Add(new AnimationMix("attack", "smash_die"));
            AnimationFadingList.Add(new AnimationMix("attack", "idle"));
            AnimationFadingList.Add(new AnimationMix("attack", "walk"));

            AnimationFadingList.Add(new AnimationMix("idle", "die"));
            AnimationFadingList.Add(new AnimationMix("idle", "smash_die"));
            AnimationFadingList.Add(new AnimationMix("idle", "attack"));
            AnimationFadingList.Add(new AnimationMix("idle", "walk"));

            AnimationFadingList.Add(new AnimationMix("walk", "die"));
            AnimationFadingList.Add(new AnimationMix("walk", "smash_die"));
            AnimationFadingList.Add(new AnimationMix("walk", "attack"));
            AnimationFadingList.Add(new AnimationMix("walk", "idle"));
            #endregion

            #region Skeleton XY
            //Scaling.Add("skeleton", 1.0f);
            //AnimationFading.Add("skeleton", new List<AnimationMix>());
            //AnimationFadingList = AnimationFading["skeleton"];

            //AnimationFadingList.Add(new AnimationMix("attack", "die"));
            //AnimationFadingList.Add(new AnimationMix("attack", "die"));
            //AnimationFadingList.Add(new AnimationMix("attack", "smash_die"));
            //AnimationFadingList.Add(new AnimationMix("attack", "idle"));
            //AnimationFadingList.Add(new AnimationMix("attack", "walk"));
            //usw...
            #endregion
        }

        /// <summary>
        /// Wendet alle zum Skeleton passenden AnimationMixes auf animationStateData an.
        /// </summary>
        public static void SetFadingSettings(AnimationStateData pAnimationStateData)
        {
            List<AnimationMix> AnimationFadingList;
            AnimationFadingList = AnimationFading[pAnimationStateData.SkeletonData.Name];

            foreach (AnimationMix animMix in AnimationFadingList)
            {
                pAnimationStateData.SetMix(animMix.From, animMix.To, animMix.Fading);
            }
        }

        public static float GetScaling(string pSkeletonName)
        {
            return Scaling[pSkeletonName];
        }

        #endregion
    }
}
