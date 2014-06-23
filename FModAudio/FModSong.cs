using FMOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.FModAudio
{
	public class FModSong
	{
		private FMOD.Sound []sound;
		private Channel []channel;
		private float[] volume;
		private float[] fadeSpeed;

		public int MaxChannelCount;
		public bool PlayDone 
		{
			get
			{
				bool done = false;
				channel[0].isPlaying(ref done);
				if (!done)
					return true;
				else return false;
			}
		}
		public float FadingSpeed;
		private const float FADING_MAX_VOLUME = 1.0f;
		private const float FADING_MIN_VOLUME = 0.0f;


		public Sound CurrentSound { get { return sound[0]; } set { sound[0] = value; } }
		public FModSong()
		{
			sound = new FMOD.Sound[1];
			channel = new Channel[1];
		}

		public FModSong(List<string> pSongNameList)
		{
			MaxChannelCount = pSongNameList.Count;

			sound = new FMOD.Sound[MaxChannelCount];
			channel = new Channel[MaxChannelCount];
			volume = new float[MaxChannelCount];
			fadeSpeed = new float[MaxChannelCount];

			RESULT r;
			for(int i = 0; i < pSongNameList.Count; i++)
			{
				r = EngineSettings.FMODDevice.createSound("./Content/sfx/" + pSongNameList[i] + ".mp3", MODE.HARDWARE, ref sound[i]);
				sound[i].setMode(MODE.LOOP_NORMAL);
				volume[i] = 0.0f;
				fadeSpeed[i] = 0.0f;

				if (r == RESULT.ERR_FILE_NOTFOUND)
					;
			}
		}

		public FModSong(string pSongName)
		{
			MaxChannelCount = 1;

			sound = new FMOD.Sound[MaxChannelCount];
			channel = new Channel[MaxChannelCount];
			volume = new float[MaxChannelCount];
			
			RESULT r;
			
			r = EngineSettings.FMODDevice.createSound("./Content/sfx/" + pSongName + ".mp3", MODE.HARDWARE, ref sound[0]);
			EngineSettings.FMODDevice.playSound(CHANNELINDEX.FREE, sound[0], false, ref channel[0]);
		}

		public void StartSong()
		{
			for (int i = 0; i < MaxChannelCount; i++)
			{
				EngineSettings.FMODDevice.playSound(CHANNELINDEX.FREE, sound[i], false, ref channel[i]);
				channel[i].setVolume(0.0f);
			}
		}

		public void StartFade(int index, float pSeed)
		{
			fadeSpeed[index] = pSeed;
		}

		public void FadeVolume(int index)
		{
			volume[index] += fadeSpeed[index];

			if (volume[index] < FADING_MIN_VOLUME)
				volume[index] = FADING_MIN_VOLUME;
			if (volume[index] > FADING_MAX_VOLUME)
				volume[index] = FADING_MAX_VOLUME;

			channel[index].setVolume(volume[index]);
		}

		public void Release()
		{
			for (int i = 0; i < MaxChannelCount; i++)
				sound[i].release();
		}
		
	}
}
