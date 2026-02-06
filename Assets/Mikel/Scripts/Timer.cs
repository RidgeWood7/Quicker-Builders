using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;


public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public UnityEvent onZero;
    public string timerFormat;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            onZero.Invoke();
            Time.timeScale = 1f;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
        timerText.text = timeSpan.ToString(@timerFormat);
    }

    //public void Resume()
    //{
    // pauseMenu.SetActive(false);
    // Time.timeScale = 1f;
    //  }
    public void Restart()
    {
        
    }

    public float FinishTime()
    {
        return remainingTime;
    }
}
