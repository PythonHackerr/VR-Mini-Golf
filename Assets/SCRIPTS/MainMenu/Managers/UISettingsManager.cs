using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu
{
	public class UISettingsManager : MonoBehaviour
	{
		public enum Platform { Desktop, Mobile };
		public Platform platform;

		[Header("GRAPHICS SETTINGS")]
		public GameObject shadowOffHighlight;
		public GameObject shadowLowHighlight;
		public GameObject shadowHighHighlight;
		public GameObject textureLowHighlight;
		public GameObject textureMedHighlight;
		public GameObject textureHighHighlight;

		[Header("GAME SETTINGS")]
		public GameObject difficultyNormalHighlight;
		public GameObject difficultyHardHighlight;

		[Header("CONTROLS SETTINGS")]

		// sliders
		public Slider musicSlider;
		public Slider soundSlider;


		public GameObject goodQualityTextLine;
		public GameObject beautifulQualityTextLine;
		public GameObject fantasticQualityTextLine;

		public void Start()
		{
			// check difficulty
			if (PlayerPrefs.GetInt("NormalDifficulty") == 1)
			{
				difficultyNormalHighlight.gameObject.SetActive(true);
				difficultyHardHighlight.gameObject.SetActive(false);
			}
			else
			{
				difficultyHardHighlight.gameObject.SetActive(true);
				difficultyNormalHighlight.gameObject.SetActive(false);
			}

			// check slider values
			musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.35f);
			soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1);

			// check shadow distance/enabled
			if (platform == Platform.Desktop)
			{
				if (PlayerPrefs.GetInt("Shadows", 2) == 0)
				{
					QualitySettings.shadowCascades = 0;
					QualitySettings.shadowDistance = 0;
					shadowOffHighlight.gameObject.SetActive(true);
					shadowLowHighlight.gameObject.SetActive(false);
					shadowHighHighlight.gameObject.SetActive(false);
				}
				else if (PlayerPrefs.GetInt("Shadows", 2) == 1)
				{
					QualitySettings.shadowCascades = 2;
					QualitySettings.shadowDistance = 75;
					shadowOffHighlight.gameObject.SetActive(false);
					shadowLowHighlight.gameObject.SetActive(true);
					shadowHighHighlight.gameObject.SetActive(false);
				}
				else if (PlayerPrefs.GetInt("Shadows", 2) == 2)
				{
					QualitySettings.shadowCascades = 4;
					QualitySettings.shadowDistance = 500;
					shadowOffHighlight.gameObject.SetActive(false);
					shadowLowHighlight.gameObject.SetActive(false);
					shadowHighHighlight.gameObject.SetActive(true);
				}
			}


			// check texture quality
			if (PlayerPrefs.GetInt("Textures", 2) == 0)
			{
				QualitySettings.globalTextureMipmapLimit = 2;
				textureLowHighlight.gameObject.SetActive(true);
				textureMedHighlight.gameObject.SetActive(false);
				textureHighHighlight.gameObject.SetActive(false);
			}
			else if (PlayerPrefs.GetInt("Textures", 2) == 1)
			{
				QualitySettings.globalTextureMipmapLimit = 1;
				textureLowHighlight.gameObject.SetActive(false);
				textureMedHighlight.gameObject.SetActive(true);
				textureHighHighlight.gameObject.SetActive(false);
			}
			else if (PlayerPrefs.GetInt("Textures", 2) == 2)
			{
				QualitySettings.globalTextureMipmapLimit = 0;
				textureLowHighlight.gameObject.SetActive(false);
				textureMedHighlight.gameObject.SetActive(false);
				textureHighHighlight.gameObject.SetActive(true);
			}

			SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 2));
		}


		public void MusicSlider()
		{
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
		}

		public void SoundSlider()
		{
			PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
		}


		public void NormalDifficulty()
		{
			difficultyHardHighlight.gameObject.SetActive(false);
			difficultyNormalHighlight.gameObject.SetActive(true);
			PlayerPrefs.SetInt("NormalDifficulty", 1);
			PlayerPrefs.SetInt("HardCoreDifficulty", 0);
		}

		public void HardcoreDifficulty()
		{
			difficultyHardHighlight.gameObject.SetActive(true);
			difficultyNormalHighlight.gameObject.SetActive(false);
			PlayerPrefs.SetInt("NormalDifficulty", 0);
			PlayerPrefs.SetInt("HardCoreDifficulty", 1);
		}

		public void ShadowsOff()
		{
			PlayerPrefs.SetInt("Shadows", 0);
			QualitySettings.shadowCascades = 0;
			QualitySettings.shadowDistance = 0;
			shadowOffHighlight.gameObject.SetActive(true);
			shadowLowHighlight.gameObject.SetActive(false);
			shadowHighHighlight.gameObject.SetActive(false);
		}

		public void ShadowsLow()
		{
			PlayerPrefs.SetInt("Shadows", 1);
			QualitySettings.shadowCascades = 2;
			QualitySettings.shadowDistance = 75;
			shadowOffHighlight.gameObject.SetActive(false);
			shadowLowHighlight.gameObject.SetActive(true);
			shadowHighHighlight.gameObject.SetActive(false);
		}

		public void ShadowsHigh()
		{
			PlayerPrefs.SetInt("Shadows", 2);
			QualitySettings.shadowCascades = 4;
			QualitySettings.shadowDistance = 500;
			shadowOffHighlight.gameObject.SetActive(false);
			shadowLowHighlight.gameObject.SetActive(false);
			shadowHighHighlight.gameObject.SetActive(true);
		}



		public void TexturesLow()
		{
			PlayerPrefs.SetInt("Textures", 0);
			QualitySettings.globalTextureMipmapLimit = 2;
			textureLowHighlight.gameObject.SetActive(true);
			textureMedHighlight.gameObject.SetActive(false);
			textureHighHighlight.gameObject.SetActive(false);
		}

		public void TexturesMed()
		{
			PlayerPrefs.SetInt("Textures", 1);
			QualitySettings.globalTextureMipmapLimit = 1;
			textureLowHighlight.gameObject.SetActive(false);
			textureMedHighlight.gameObject.SetActive(true);
			textureHighHighlight.gameObject.SetActive(false);
		}

		public void TexturesHigh()
		{
			PlayerPrefs.SetInt("Textures", 2);
			QualitySettings.globalTextureMipmapLimit = 0;
			textureLowHighlight.gameObject.SetActive(false);
			textureMedHighlight.gameObject.SetActive(false);
			textureHighHighlight.gameObject.SetActive(true);
		}



		public void SetQualityLevel(int qualityLevel)
		{
			QualitySettings.SetQualityLevel(qualityLevel);
			PlayerPrefs.SetInt("QualityLevel", qualityLevel);

			// Update UI elements
			goodQualityTextLine.SetActive(qualityLevel == 0); // Assuming "Good" is at index 0
			beautifulQualityTextLine.SetActive(qualityLevel == 1); // Assuming "Beautiful" is at index 1
			fantasticQualityTextLine.SetActive(qualityLevel == 2); // Assuming "Fantastic" is at index 2
		}

		public void SetGoodQuality()
		{
			SetQualityLevel(0);
		}

		public void SetBeautifulQuality()
		{
			SetQualityLevel(1);
		}

		public void SetFantasticQuality()
		{
			SetQualityLevel(2);
		}
	}
}