using FMOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptonEngine.FModAudio
{
	public class FmodMediaPlayer
	{
#region Singleton

		private static FmodMediaPlayer instance;
		public static FmodMediaPlayer Instance 
		{ 
			get 
			{ 
				if (instance == null) instance = new FmodMediaPlayer(); 
				return instance; 
			} 
		}
#endregion

		private FModSong BackgroundSong;
		private Dictionary<string, FModSong> mSongList = new Dictionary<string,FModSong>();
		private List<string> delete = new List<string>();

		private const float FADING_MAX = 1.0f;
		private const float FADING_MIN = 0.0f;
		public static float FadingSpeed = 0.0f;

		public FmodMediaPlayer()
		{
			mSongList = new Dictionary<string, FModSong>();
		}

		~FmodMediaPlayer()
		{
			if(BackgroundSong != null)
				BackgroundSong.Release();
		}

		public void AddSong(string pName)
		{
			if (mSongList.ContainsKey(pName)) return;

			FModSong newSong = new FModSong(pName);
			mSongList.Add(pName, newSong);
		}

		public void SetBackgroundSong(List<string> pMusicList)
		{
			BackgroundSong = new FModSong(pMusicList);
			BackgroundSong.StartSong();
			FadeBackgroundIn();
		}


		public void FadeBackgroundIn()
		{
			BackgroundSong.StartFade(0, FadingSpeed);
		}

		public void FadeBackgroundOut()
		{
			for (int i = 0; i < BackgroundSong.MaxChannelCount; i++ )
				BackgroundSong.StartFade(i, -FadingSpeed);
		}

		public void FadeBackgroundChannelIn(int index)
		{
			BackgroundSong.StartFade(index, FadingSpeed);
		}

		public void FadeBackgroundChannelOut(int index)
		{
			BackgroundSong.StartFade(index, -FadingSpeed);
		}

		public void Update()
		{
			EngineSettings.FMODDevice.update();

			if (BackgroundSong != null)
			{
				for (int i = 0; i < BackgroundSong.MaxChannelCount; i++)
					BackgroundSong.FadeVolume(i);
			}

			foreach (KeyValuePair<string, FModSong> pair in mSongList)
			{
				if (pair.Value.PlayDone)
				{
					delete.Add(pair.Key); pair.Value.Release();
				}
			}

			foreach (string s in delete)
				mSongList.Remove(s);

			delete.Clear();
		}
	}
}
