using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelData[] levelDatas;
    int shotsLeft = 0;

    public ParticleSystem winParticleSystem;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitLevel(GameManager.instance.currentLevelIndex);
    }



    public void InitLevel(int levelIndex)
    {
        shotsLeft = levelDatas[levelIndex].shotCount;
        UIManager.instance.ShotTakenText.SetText(shotsLeft.ToString());
        UIManager.instance.ShotTotalText.SetText(shotsLeft.ToString());
        GameManager.instance.gameStatus = GameStatus.Playing;
        TimeManager.instance.Resume();
    }



    public void ShotTaken()
    {
        if (shotsLeft > 0)
        {
            shotsLeft--;
            UIManager.instance.ShotTakenText.SetText(shotsLeft.ToString());

            if (shotsLeft <= 0)
            {
                LevelFailed();
            }
        }
    }



    public void LevelFailed()
    {
        if (GameManager.instance.gameStatus == GameStatus.Playing)
        {
            StartCoroutine(DelayLevelFailed(1f));
        }
    }



    public void LevelComplete()
    {
        winParticleSystem.Play();

        if (GameManager.instance.gameStatus == GameStatus.Playing)
        {
            // Save level data!!!
            int currentLevelIndex = GameManager.instance.currentLevelIndex;
            levelDatas[currentLevelIndex].isCompleted = true;
            levelDatas[currentLevelIndex].completionShotsTaken = levelDatas[currentLevelIndex].shotCount - shotsLeft;
            levelDatas[currentLevelIndex].completionTime = TimeManager.instance.timeCounter.timer;

            string json = JsonUtility.ToJson(levelDatas[currentLevelIndex]);
            PlayerPrefs.SetString(levelDatas[currentLevelIndex].levelName, json);
            PlayerPrefs.Save();

            StartCoroutine(DelayLevelComplete(1f));
        }
    }



    private IEnumerator DelayLevelFailed(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.gameStatus = GameStatus.Failed;
        UIManager.instance.GameResult();
        TimeManager.instance.Pause();
    }


    private IEnumerator DelayLevelComplete(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.gameStatus = GameStatus.Complete;
        UIManager.instance.GameResult();
        TimeManager.instance.Pause();
    }
}
