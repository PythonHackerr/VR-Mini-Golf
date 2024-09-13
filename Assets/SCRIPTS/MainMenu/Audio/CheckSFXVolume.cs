using UnityEngine;
using System.Collections;

namespace SlimUI.ModernMenu
{
	public class CheckSFXVolume : MonoBehaviour
	{
		public void Start()
		{
			UpdateVolume();
		}

		public void UpdateVolume()
		{
			GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
		}
	}
}