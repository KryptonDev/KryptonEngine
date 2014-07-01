using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.Entities
{
    public class SpineData : BaseObject
    {

		public class AnimationMix
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
			public AnimationMix()
			{

			}

			public AnimationMix(string pFrom, string pTo)
			{
				from = pFrom;
				to = pTo;
				fading = 0.2f;
			}

			public AnimationMix(string pFrom, string pTo, float pFading)
			{
				from = pFrom;
				to = pTo;
				fading = pFading;
			}

			#endregion
		}

		public struct SpineDataSettings
		{
			public List<AnimationMix> AnimationFading;
			public float Scaling;
		}

        #region Properties

        public Atlas atlas;
        public SkeletonJson json;
		public SpineDataSettings settings;

        #endregion

        #region Constructor

        public SpineData(string pSkeletonName, SpineDataSettings pSettings)
        {
			Initialize();
			settings = pSettings;
			atlas = new Atlas(EngineSettings.DefaultPathSpine + "\\" + pSkeletonName + ".atlas", EngineSettings.TextureLoader);
            json = new SkeletonJson(atlas);
        }

        #endregion
    }
}
