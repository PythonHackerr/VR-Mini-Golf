using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleLevelManager : MonoBehaviour
{
    public string levelScenePrefix = "Level_";
    public int currentLevel = 1;
    public TextMeshProUGUI[] texts;
    public Image unlockedImage;
    public Image lockedImage;
    public Image currentImage;


    void Start()
    {
        foreach (var t in texts)
        {
            t.text = currentLevel.ToString();
        }
    }


    public void LoadLevelScene()
    {
        PlayerPrefs.SetInt("LoadedLevel", currentLevel);
        LevelLoader.Instance.LoadScene(levelScenePrefix + currentLevel.ToString());
    }


    public void SetLevelState(LevelState state)
    {
        switch (state)
        {
            case LevelState.Unlocked:
                unlockedImage.gameObject.SetActive(true);
                lockedImage.gameObject.SetActive(false);
                currentImage.gameObject.SetActive(false);
                break;
            case LevelState.Locked:
                unlockedImage.gameObject.SetActive(false);
                lockedImage.gameObject.SetActive(true);
                currentImage.gameObject.SetActive(false);
                break;
            case LevelState.Current:
                unlockedImage.gameObject.SetActive(false);
                lockedImage.gameObject.SetActive(false);
                currentImage.gameObject.SetActive(true);
                break;
        }
    }
}

public enum LevelState
{
    Unlocked,
    Locked,
    Current
}
