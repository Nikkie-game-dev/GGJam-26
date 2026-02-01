using Code.Service;
using Systems.CentralizeEventSystem;
using Systems.TagClassGenerator;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void PlayerDies();
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
        if (!other.CompareTag(Tags.Player))
            return;

        ServiceProvider.Instance.GetService<CentralizeEventSystem>().Get<PlayerDies>()?.Invoke();

        Invoke(nameof(ReloadScene), respawnDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
