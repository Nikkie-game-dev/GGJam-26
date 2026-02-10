using UnityEngine;
using Code.Service;
using Systems.CentralizeEventSystem;
using static GameEventsDelegates;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private bool _triggered;

    private CentralizeEventSystem Central =>
        ServiceProvider.Instance.GetService<CentralizeEventSystem>();

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if (!other.CompareTag(playerTag)) return;

        _triggered = true;

        Central?.Get<OnLevelCompleted>()?.Invoke();
    }
}