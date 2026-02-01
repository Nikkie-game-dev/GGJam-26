using System;

public static class GameEvents
{
    public static event Action OnLevelCompleted;

    public static void RaiseLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
}