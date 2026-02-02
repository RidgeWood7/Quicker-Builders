using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField] 
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private List<Score> scoreScripts;
    private Coin _coinScript;

    private void Start()
    {
        _coinScript = FindFirstObjectByType<Coin>();
    }

    private void Update()
    {
        for (int i = 0; i < scoreScripts.Count; i++)
        {
            scores[i].text = scoreScripts[i].currentScore.ToString();
        }
    }

    public void EndOfRound()
    {
        // Create a new list of sorted scores without changing the original.
        List<Score> sortedScores = scoreScripts.OrderBy(score => -score.playerPoints).ToList();
        // Iterate through all the players.
        for (int i = 0; i < names.Count; i++)
        {
            // For each player, determine their rank by locating their position in the sorted list.
            int rank = sortedScores.IndexOf(scoreScripts[i]);

            // Move the player on the leaderboard to the position of their rank (0 is first, 3 is last).
            players[i].transform.SetSiblingIndex(rank);
        }
    }
}
