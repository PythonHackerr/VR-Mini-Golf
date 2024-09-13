using UnityEngine;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    public TimeCounter timeCounter;
    private bool isPaused = false;
    private float originalTimeScale;
    private Tween timeTween;
    public float defaultTimeChangeDuration = 0.25f;

    public float targetTimeScale = 0.2f;


    private void Awake()
    {
        Time.timeScale = 1;
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        originalTimeScale = Time.timeScale; // Store the default time scale
    }


    public void Pause(float duration = -1f)
    {
        if (duration == -1f) { duration = defaultTimeChangeDuration; }
        if (!isPaused)
        {
            isPaused = true;
            timeTween?.Kill(); // Kill any ongoing tween
            timeTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, targetTimeScale, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => Time.timeScale = targetTimeScale);
        }
    }


    public void Resume(float duration = -1f)
    {
        if (duration == -1f) { duration = defaultTimeChangeDuration; }
        if (isPaused)
        {
            isPaused = false;
            timeTween?.Kill(); // Kill any ongoing tween
            timeTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, originalTimeScale, duration)
                .SetEase(Ease.InOutQuad);
        }
    }

    public void ResumeImmidiately()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = originalTimeScale;
        }
    }


    public void SetTimeScale(float newTimeScale, float duration = 0.5f)
    {
        timeTween?.Kill(); // Kill any ongoing tween
        timeTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, newTimeScale, duration)
            .SetEase(Ease.InOutQuad);
    }


    private void OnDestroy()
    {
        // Clean up the tween when the object is destroyed
        timeTween?.Kill();
    }
}
