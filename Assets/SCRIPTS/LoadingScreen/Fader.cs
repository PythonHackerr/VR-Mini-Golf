
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    string sceneToLoad;
    public float fadeDuration = 1f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float delayBeforeRaycastDisable = 0f;

    private void OnEnable()
    {
        FadeIn();
    }


    public void FadeIntoLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName).allowSceneActivation = false;

        sceneToLoad = sceneName;

        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 1;

        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            DOVirtual.DelayedCall(delayBeforeRaycastDisable, () =>
            {
                canvasGroup.blocksRaycasts = false;
            });
        });
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 0;

        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
        {

        });
    }
}
