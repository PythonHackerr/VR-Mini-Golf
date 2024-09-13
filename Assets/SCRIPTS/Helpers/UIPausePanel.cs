using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour
{
    public void ResumeScene()
    {
        TimeManager.instance.ResumeImmidiately();
        UIManager.instance.SetPausePanel(false);
    }

    public void RestartScene()
    {
        TimeManager.instance.ResumeImmidiately();
        UIManager.instance.LevelRestart();
    }

    public void BackToLevelSelection()
    {
        TimeManager.instance.ResumeImmidiately();
        UIManager.instance.LevelExit();
    }
}
