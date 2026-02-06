using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    [SerializeField]
    private List<Image> playerBars;
    public int playerPoints;
    public int winningPoints = 100;
    public Image playerbar;
    private Timer _timer;
    public float timeAtFinish;
    public bool isFinished = false;
    public bool isDead = false;

    [Range(0, 100)] public int currentScore = 0;
    private void Start()
    {
        _timer = FindFirstObjectByType<Timer>();
    }

    private void Update()
    {
        playerbar.fillAmount = currentScore / (float)winningPoints;
        playerbar.fillAmount = playerPoints / (float)winningPoints;

        if (playerPoints >= winningPoints)
        {
            Debug.Log("Player Wins!");
        }
    }

    public void SetTime()
    {
        timeAtFinish = _timer.FinishTime();
    }
}
