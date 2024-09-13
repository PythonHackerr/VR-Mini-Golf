using System.Collections;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI millisecondsText;
    public float timer;

    void Start()
    {
        timer = 0f;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            timer += Time.deltaTime * Time.timeScale;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            int milliseconds = Mathf.FloorToInt((timer - Mathf.Floor(timer)) * 100);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            string formattedMilliseconds = string.Format(".{0:00}", milliseconds);

            timeText.text = formattedTime;
            if (millisecondsText != null)
                millisecondsText.text = formattedMilliseconds;

            yield return null;
        }
    }
}
