using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreStateSO scoreState;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        if (scoreState == null) return;

        scoreState.OnScoreChanged += HandleScoreChanged;
        HandleScoreChanged(scoreState.Score);
    }

    private void OnDisable()
    {
        if (scoreState == null) return;
        scoreState.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {newScore}";
    }
}
