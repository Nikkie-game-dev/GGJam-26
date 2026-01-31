using System;
using Assets.Code.Manager;
using Assets.Code.Service;
using Systems.CentralizeEventSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public delegate void OnLifeTimeEqualsZero();
public class LifeTimeComponent : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField][Tooltip("This is the maximum lifetime the player can get")] 
    private float _maximumLifeTime;
    [SerializeField][Tooltip("This is the dynmic variable of lifetime, when zero OnDie() ")]  
    private float _currentLifeTime;
    [SerializeField][Range(0f, 2f)][Tooltip("This is the time drain multiplier")]
    private float _drainFactor = 1;

    [Header("Events")]
    private OnLifeTimeEqualsZero _onLifeTimeEqualsZero;
    private CentralizeEventSystem _centralizeEventSystem;
    private void Start()
    {
        _currentLifeTime = _maximumLifeTime;
        
        _centralizeEventSystem = ServiceProvider.Instance.GetService<CentralizeEventSystem>();
        if(_centralizeEventSystem != null)
        {
            _centralizeEventSystem.Register(_onLifeTimeEqualsZero);
        }
        else
        {
            Debug.LogWarning("centralize event system is null");
        }

    }
    private void Update()
    {
        _currentLifeTime -= Time.deltaTime * _drainFactor;

        if (_currentLifeTime <= 0 && _centralizeEventSystem != null)
        {
            OnLifeTimeEqualsZero delegateInstance = _centralizeEventSystem.Get<OnLifeTimeEqualsZero>();
            if (delegateInstance != null)
                delegateInstance.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<FlowerBase>(out FlowerBase flower)) return;
        
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
