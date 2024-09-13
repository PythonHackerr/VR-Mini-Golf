using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI _shotsTakenText;
    [SerializeField] private TextMeshProUGUI _shotsTotalText;
    [SerializeField] private GameObject pausePanel, levelCompletePanel, gameOverPanel;
    public TextMeshProUGUI ShotTakenText { get { return _shotsTakenText; } }
    public TextMeshProUGUI ShotTotalText { get { return _shotsTotalText; } }

    public string levelSelectionSceneName = "LevelSelection";


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



    public void GameResult()
    {
        switch (GameManager.instance.gameStatus)
        {
            case GameStatus.Complete:
                SetLevelCompletePanel(true);
                SoundManager.instance.PlaySound(FxTypes.GAME_COMPLETE);
                break;
            case GameStatus.Failed:
                SetGameOverPanel(true);
                SoundManager.instance.PlaySound(FxTypes.GAME_OVER);
                break;
        }
    }


    public void LevelExit()
    {
        GameManager.instance.gameStatus = GameStatus.None;
        SceneManager.LoadScene(levelSelectionSceneName);
    }


    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void SetLevelCompletePanel(bool isActive)
    {
        levelCompletePanel.SetActive(isActive);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void SetGameOverPanel(bool isActive)
    {
        levelCompletePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(isActive);
    }

    public void SetPausePanel(bool isActive)
    {
        levelCompletePanel.SetActive(false);
        pausePanel.SetActive(isActive);
        gameOverPanel.SetActive(false);
    }
}
