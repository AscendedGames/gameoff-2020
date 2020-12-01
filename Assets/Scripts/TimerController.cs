using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    public bool doRunTimer;
    public float time;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        doRunTimer = true;
    }

    void Update()
    {
        if (doRunTimer) RunTimer();
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }

    void RunTimer()
    {
        time = Time.time - startTime;

        var formattedTime = FormatTime(time);

        timerText.text = formattedTime;
    }
}
