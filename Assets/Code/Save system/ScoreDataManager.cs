using UnityEngine;

public class ScoreDataManager : MonoBehaviour
{
    [SerializeField] private ScoreDataContainer scoreDataContainer;

    public ScoreDataContainer ScoreDataContainer { get => scoreDataContainer; }

    public void SaveScore(string playerName, int score)
    {
        scoreDataContainer.SaveScore(playerName, score);
    }

}
