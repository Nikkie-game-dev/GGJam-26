using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDataContainer : ScriptableObject
{
    [SerializeField] private List<ScoreData> playersScore;

    public void SaveScore(string playerName, int score)
    {
        playersScore.Add(new ScoreData(playerName, score));
    }

    public void ClearScores()
    {
        playersScore.Clear();
    }
}


public struct ScoreData
{
    private readonly string playerName;
    private readonly int score;

    public ScoreData(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}