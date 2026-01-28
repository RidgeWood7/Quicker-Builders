using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField] 
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private List<Score> scoreScripts;

    private void Update()
    {
        for (int i = 0; i < scoreScripts.Count; i++)
        {
            scores[i].text = scoreScripts[i].currentScore.ToString();
        }
    }
}
