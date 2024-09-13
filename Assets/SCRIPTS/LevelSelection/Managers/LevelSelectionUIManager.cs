using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUIManager : MonoBehaviour
{
    public LevelSelectionManager levelManager;
    public List<SingleLevelManager> levelManagers;
    public bool doLevelOrderNumeration = true;

    private void Start()
    {
        UpdateLevelSelectionUI();
    }

    public void UpdateLevelSelectionUI()
    {
        for (int i = 0; i < levelManagers.Count; i++)
        {
            if (doLevelOrderNumeration)
                levelManagers[i].currentLevel = i + 1;

            if (levelManager.IsLevelUnlocked(i))
            {
                if (i == levelManagers.Count - 1 || !levelManager.IsLevelUnlocked(i + 1))
                {
                    levelManagers[i].SetLevelState(LevelState.Current);
                }
                else
                {
                    levelManagers[i].SetLevelState(LevelState.Unlocked);
                }
            }
            else
            {
                levelManagers[i].SetLevelState(LevelState.Locked);
            }
        }
    }
}
