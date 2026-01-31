using System;
using Code.Service;
using Systems.CentralizeEventSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public delegate void OnLifeTimeEqualsZero();
public class LifeTimeComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("This is the maximum lifetime the player can get")]
    private float _maximumLifeTime;
    [SerializeField]
    [Tooltip("This is the dynmic variable of lifetime, when zero OnDie() ")]
    private float _currentLifeTime;
    [SerializeField]
    [Range(0f, 2f)]
    [Tooltip("This is the time drain multiplier")]
    private float _drainFactor = 1;

    private CentralizeEventSystem _centralizeEventSystem => ServiceProvider.Instance.GetService<CentralizeEventSystem>();

    private void Start()
    {
        _currentLifeTime = _maximumLifeTime;
    }
    private void Update()
    {
        _currentLifeTime -= Time.deltaTime * _drainFactor;

        if (_currentLifeTime <= 0 && _centralizeEventSystem != null)
        {
            _centralizeEventSystem.Get<OnLifeTimeEqualsZero>()?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<FlowerBase>(out FlowerBase flower)) return;

        AddTime(flower.GetTime());
        flower.Interact(this.gameObject);
    }

    public void AddTime(float timeAmount)
    {
        _currentLifeTime = Mathf.Clamp(_currentLifeTime + timeAmount, 0, _maximumLifeTime);
    }
    public void ModifyDrainFactor(float timeAmount)
    {
        _drainFactor = Mathf.Clamp(timeAmount, 0, 2);
    }
    private void OnDestroy()
    {
        if (_centralizeEventSystem != null)
        {
            _centralizeEventSystem.Unregister<OnLifeTimeEqualsZero>();
        }
    }
}
