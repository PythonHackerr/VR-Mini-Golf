using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [Header("LOADING SCREEN")]
    public GameObject loadingMenu;
    public Slider loadingBar;
    public TMP_Text loadPromptText;
    public KeyCode userPromptKey = KeyCode.Space;

    public Fader fader;

    public bool waitForInput = false;
    public bool waitForAnimation = true;
    bool isAnimationDone = false;

    private void Awake()
    {
        isAnimationDone = false;
        // Check if an instance of LevelLoader already exists
        if (Instance != null && Instance != this) { Destroy(Instance.gameObject); }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        loadingBar.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        loadingBar.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        loadingMenu.SetActive(true);
        fader.FadeOut();
        if (waitForAnimation)
        {
            StartCoroutine(WaitAnimationFinish(fader.fadeDuration));
        }

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            if (operation.progress >= 0.9f && waitForInput)
            {
                loadPromptText.text = "Press any key to continue";
                loadingBar.value = 1;

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else if (operation.progress >= 0.9f && !waitForInput && (!waitForAnimation || isAnimationDone))
            {
                operation.allowSceneActivation = true;
                isAnimationDone = false;
            }

            yield return null;
        }
    }


    private IEnumerator WaitAnimationFinish(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAnimationDone = true;
    }
}
