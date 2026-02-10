using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathReceiver : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private DeathEventChannelSO deathChannel;
    [SerializeField] private ScoreStateSO scoreState;

    [Header("Reset")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool reloadSceneInstead = true;

    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;

    private bool dead;

    private void OnEnable()
    {
        if (deathChannel != null)
            deathChannel.OnDeathEvent += HandleDeath;
    }

    private void OnDisable()
    {
        if (deathChannel != null)
            deathChannel.OnDeathEvent -= HandleDeath;
    }

    private void HandleDeath(DeathCause cause)
    {
        if (dead) return;
        dead = true;

        if (debugLogs)
            Debug.Log($"[DEATH] Cause: {cause}");

        if (scoreState != null)
            scoreState.ResetScore();

        if (reloadSceneInstead)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
            return;
        }

        if (player != null && spawnPoint != null)
        {
            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            player.position = spawnPoint.position;
        }

        dead = false;
    }
}