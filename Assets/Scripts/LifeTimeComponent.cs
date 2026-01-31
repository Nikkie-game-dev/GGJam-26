using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
    [SerializeField][Tooltip("Drag here your Die() method")] 
    private UnityEvent _onLifetimeEqualsZero;
    private void Start()
    {
        _currentLifeTime = _maximumLifeTime;
    }
    private void Update()
    {
        _currentLifeTime -= Time.deltaTime * _drainFactor;
        
        if(_currentLifeTime <= 0)
            _onLifetimeEqualsZero?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<FlowerBase>(out FlowerBase flower)) return;
        
        AddTime(flower.GetTime());
        flower.Interact(this.gameObject);
    }

    public void AddTime(float timeAmount)
    {
        _currentLifeTime += Mathf.Clamp(timeAmount, 0, _maximumLifeTime);

    }
    public void ModifyDrainFactor(float timeAmount)
    {
        _drainFactor = Mathf.Clamp(timeAmount, 0, 2);
    }
}
