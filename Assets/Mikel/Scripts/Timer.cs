using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public UnityEvent onZero;

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

        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    //public void Resume()
    //{
    // pauseMenu.SetActive(false);
    // Time.timeScale = 1f;
    //  }
    public void Restart()
    {
        
    }
}
