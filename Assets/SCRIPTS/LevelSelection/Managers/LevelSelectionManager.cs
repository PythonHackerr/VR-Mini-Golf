using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public List<LevelData> levels;
    public TimeCounter timeCounter;
    public bool OverridePlayerPrefs_EditorOnly = false;
    public bool[] levelsBool;

    private void Start()
    {
        if (OverridePlayerPrefs_EditorOnly == true)
        {
            int index = 0;
            foreach (bool b in levelsBool)
            {
                if (b == true)
                {
                    CompleteLevel(index);
                }
                else
                {
                    UnCompleteLevel(index);
                }
                index += 1;
            }
        }
        LoadLevelData();
    }

    public void CompleteLevel(int levelIndex)
    {
        if (levelIndex < levels.Count)
        {
            levels[levelIndex].isCompleted = true;
            if (timeCounter != null) levels[levelIndex].completionTime = timeCounter.timer;
            SaveLevelData();
        }
    }

    public void UnCompleteLevel(int levelIndex)
    {
        if (levelIndex < levels.Count)
        {
            levels[levelIndex].isCompleted = false;
            if (timeCounter != null) levels[levelIndex].completionTime = timeCounter.timer;
            SaveLevelData();
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex == 0) return true;
        if (levelIndex > 0 && levelIndex < levels.Count)
        {
            return levels[levelIndex - 1].isCompleted;
        }
        return false;
    }

    private void SaveLevelData()
    {
        foreach (var level in levels)
        {
            string json = JsonUtility.ToJson(level);
            PlayerPrefs.SetString(level.levelName, json);
        }
        PlayerPrefs.Save();
    }

    private void LoadLevelData()
    {
        foreach (var level in levels)
        {
            if (PlayerPrefs.HasKey(level.levelName))
            {
                string json = PlayerPrefs.GetString(level.levelName);
                JsonUtility.FromJsonOverwrite(json, level);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        LevelLoader.Instance.LoadScene(sceneName);
    }
}
