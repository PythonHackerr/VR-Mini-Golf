using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int currentLevelIndexOverride = -1;
    public int targetFPS;
    public static GameManager instance;

    [HideInInspector]
    public int currentLevelIndex;
    [HideInInspector]
    public GameStatus gameStatus = GameStatus.None;

    private InputDevice leftController;

    private bool wasLeftXButtonPressed = false;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;

        if (currentLevelIndexOverride == -1)
        {
            currentLevelIndex = PlayerPrefs.GetInt("LoadedLevel", 1) - 1;
        }
        else
        {
            currentLevelIndex = currentLevelIndexOverride;
        }

        // Init controller devices
        InitControllers();
    }

    private void InitControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }
    }


    private void Update()
    {
        if (!leftController.isValid)
        {
            InitControllers();
        }

        // Check for the X button press (PrimaryButton) on either controller
        bool leftXButtonPressed = false;

        if (leftController.isValid)
        {
            leftController.TryGetFeatureValue(CommonUsages.primaryButton, out leftXButtonPressed);
        }

        // Check if the button was just pressed (transition from unpressed to pressed)
        bool leftXButtonJustPressed = leftXButtonPressed && !wasLeftXButtonPressed;

        // If the X button is just pressed on either controller
        if (leftXButtonJustPressed)
        {
            if (gameStatus != GameStatus.Pause)
            {
                GamePause();
            }
            else if (gameStatus == GameStatus.Pause)
            {
                GameResume();
            }
        }

        // Update the previous button states for the next frame
        wasLeftXButtonPressed = leftXButtonPressed;
    }

    public void GamePause()
    {
        TimeManager.instance.Pause();
        UIManager.instance.SetPausePanel(true);
        gameStatus = GameStatus.Pause;
    }

    public void GameResume()
    {
        TimeManager.instance.Resume();
        UIManager.instance.SetPausePanel(false);
        gameStatus = GameStatus.Playing;
    }

}

[System.Serializable]
public enum GameStatus
{
    None,
    Playing,
    Pause,
    Failed,
    Complete
}
