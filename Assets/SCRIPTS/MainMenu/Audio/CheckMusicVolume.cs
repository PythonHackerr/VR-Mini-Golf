using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SlimUI.ModernMenu
{
	public class CheckMusicVolume : MonoBehaviour
	{
		public Slider musicSlider;
		public Slider soundSlider;

		public void Start()
		{
			UpdateVolume();
		}

		public void UpdateVolume()
		{
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
		}

		public void MusicSlider()
		{
			if (musicSlider == null) return;
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
			UpdateVolume();
		}

		public void SoundSlider()
		{
			if (soundSlider == null) return;
			PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
		}
	}
}