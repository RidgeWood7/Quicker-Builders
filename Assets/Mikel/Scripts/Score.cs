using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    [SerializeField] 
    private List<Image> playerBars;
    public int playerpoints;
    public int winningPoints = 100;
    public Image playerbar;

    [Range(0, 100)] public int currentScore = 0;


    private void Update()
    {
        playerbar.fillAmount = currentScore / 100f;

        if (playerpoints >= winningPoints)
        {
            Debug.Log("Player Wins!");
        }
    }








}
