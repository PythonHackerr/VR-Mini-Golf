using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace SlimUI.ModernMenu
{
	public class UIMenuManager : MonoBehaviour
	{
		// campaign button sub menu
		[Header("MENUS")]
		[Tooltip("The Menu for when the MAIN menu buttons")]
		public GameObject mainMenu;
		[Tooltip("THe first list of buttons")]
		public GameObject firstMenu;
		[Tooltip("The Menu for when the PLAY button is clicked")]
		public GameObject playMenu;
		[Tooltip("The Menu for when the EXIT button is clicked")]
		public GameObject exitMenu;
		[Tooltip("Optional 4th Menu")]
		public GameObject extrasMenu;

		public enum Theme { custom1, custom2, custom3 };
		[Header("THEME SETTINGS")]
		public Theme theme;
		private int themeIndex;
		[Header("PANELS")]
		[Tooltip("The UI Panel parenting all sub menus")]
		public GameObject mainCanvas;
		[Tooltip("The UI Panel that holds the CONTROLS window tab")]
		public GameObject PanelControls;
		[Tooltip("The UI Panel that holds the VIDEO window tab")]
		public GameObject PanelVideo;
		[Tooltip("The UI Panel that holds the GAME window tab")]
		public GameObject PanelGame;


		// highlights in settings screen
		[Header("SETTINGS SCREEN")]
		[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
		public GameObject lineGame;
		[Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
		public GameObject lineVideo;
		[Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
		public GameObject lineControls;

		[Header("SFX")]
		[Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
		public AudioSource hoverSound;
		[Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
		public AudioSource sliderSound;
		[Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
		public AudioSource swooshSound;
		public LevelData[] levelDatas;

		void Start()
		{
			playMenu.SetActive(false);
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			firstMenu.SetActive(true);
			mainMenu.SetActive(true);
		}


		public void PlayCampaign()
		{
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
		}

		public void PlayCampaignMobile()
		{
			exitMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			playMenu.SetActive(true);
			mainMenu.SetActive(false);
		}

		public void ReturnMenu()
		{
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(false);
			exitMenu.SetActive(false);
			mainMenu.SetActive(true);
		}

		public void LoadScene(string scene)
		{
			if (scene != "")
				LevelLoader.Instance.LoadScene(scene);
		}

		public void LoadSceneWithErase(string scene)
		{
			foreach (var level in levelDatas)
			{
				level.isCompleted = false;
				level.completionTime = -1f;
				level.completionShotsTaken = -1;
				if (PlayerPrefs.HasKey(level.levelName))
				{
					PlayerPrefs.DeleteKey(level.levelName);
				}
			}
			PlayerPrefs.Save();

			LoadScene(scene);
		}

		public void DisablePlayCampaign()
		{
			playMenu.SetActive(false);
		}

		public void Position2()
		{
			DisablePlayCampaign();
		}

		public void Position1()
		{

		}

		void DisablePanels()
		{
			PanelControls.SetActive(false);
			PanelVideo.SetActive(false);
			PanelGame.SetActive(false);

			lineGame.SetActive(false);
			lineControls.SetActive(false);
			lineVideo.SetActive(false);
		}

		public void GamePanel()
		{
			DisablePanels();
			PanelGame.SetActive(true);
			lineGame.SetActive(true);
		}

		public void VideoPanel()
		{
			DisablePanels();
			PanelVideo.SetActive(true);
			lineVideo.SetActive(true);
		}

		public void ControlsPanel()
		{
			DisablePanels();
			PanelControls.SetActive(true);
			lineControls.SetActive(true);
		}

		public void PlayHover()
		{
			hoverSound.Play();
		}

		public void PlaySFXHover()
		{
			sliderSound.Play();
		}

		public void PlaySwoosh()
		{
			swooshSound.Play();
		}

		// Are You Sure - Quit Panel Pop Up
		public void AreYouSure()
		{
			exitMenu.SetActive(true);
			if (extrasMenu) extrasMenu.SetActive(false);
			DisablePlayCampaign();
		}

		public void AreYouSureMobile()
		{
			exitMenu.SetActive(true);
			if (extrasMenu) extrasMenu.SetActive(false);
			mainMenu.SetActive(false);
			DisablePlayCampaign();
		}

		public void ExtrasMenu()
		{
			playMenu.SetActive(false);
			if (extrasMenu) extrasMenu.SetActive(true);
			exitMenu.SetActive(false);
		}

		public void QuitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}