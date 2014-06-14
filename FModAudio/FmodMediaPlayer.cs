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

			foreach (KeyValuePair<string, FModSong> pair in mSongList)
				pair.Value.Release();
		}

		// Spielt den SoundEffekt ab. Übergabe ist nur der Name der Datei ( ohne .mp3)
		public void AddSong(string pName)
		{
			if (mSongList.ContainsKey(pName)) return;

			FModSong newSong = new FModSong(pName);
			mSongList.Add(pName, newSong);
		}

		// Setzt die Hintergrund Musik. Muss gemacht werden sobald die Scene gewechselt wird und in der neuen Scene ein anderes SoundSetting ist.
		public void SetBackgroundSong(List<string> pMusicList)
		{
			BackgroundSong = new FModSong(pMusicList);
			BackgroundSong.StartSong();
			FadeBackgroundIn();
		}

		// Fadet den die erste Spur der Hintergrund Musik ein.
		public void FadeBackgroundIn()
		{
			BackgroundSong.StartFade(0, FadingSpeed);
		}

		// Fadet die Hintergrund Musik aus. Muss gemacht werden wenn die neue Scene ein anderes SoundSetting hat.
		public void FadeBackgroundOut()
		{
			for (int i = 0; i < BackgroundSong.MaxChannelCount; i++ )
				BackgroundSong.StartFade(i, -FadingSpeed);
		}

		// Fadet einen Channel ein wie z.B. ein Wolf ist in sichtweite. Dann einfach 1.mal die vorgegebene Channel ID übergeben.
		public void FadeBackgroundChannelIn(int index)
		{
			BackgroundSong.StartFade(index, FadingSpeed);
		}

		// Fadet einen Channel aus. Z.B. der Wolf ist wieder aus dem Sichtfeld.
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
					delete.Add(pair.Key); 
					pair.Value.Release();
				}
			}

			foreach (string s in delete)
				mSongList.Remove(s);

			delete.Clear();
		}
	}
}
