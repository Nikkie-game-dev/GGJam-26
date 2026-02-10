using UnityEngine;
using UnityEngine.SceneManagement;
public class TESTKillZone : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 0.25f;

    private void Awake()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Destroy(other);
        Invoke(nameof(ReloadScene), respawnDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
