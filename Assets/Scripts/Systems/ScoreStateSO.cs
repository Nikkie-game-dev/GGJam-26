using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreStateSO", menuName = "Scriptable Objects/ScoreStateSO")]
public class ScoreStateSO : ScriptableObject
{
    public int Score { get; private set; }

    public event Action<int> OnScoreChanged;

    public void Add(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}
