using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private ScoreStateSO scoreState;
    private void Start()
    {
        scoreState.ResetScore();
    }

}
