using UnityEngine;
using Code.Service;
using Systems.CentralizeEventSystem;
using static GameEventsDelegates;

public class LevelCompletionHandler : MonoBehaviour
{
    [SerializeField] private ScoreStateSO scoreState;
    [SerializeField] private VictoryScreenController victoryScreen;

    private CentralizeEventSystem Central =>
        ServiceProvider.Instance.GetService<CentralizeEventSystem>();

    private void OnEnable()
    {
        Central?.AddListener<OnLevelCompleted>(HandleWin);
    }

    private void OnDisable()
    {
        Central?.RemoveListener<OnLevelCompleted>(HandleWin);
    }

    private void HandleWin()
    {
        int finalScore = scoreState != null ? scoreState.Score : 0;
        victoryScreen.Show(finalScore);
    }
}