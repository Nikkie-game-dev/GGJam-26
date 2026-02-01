using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathEventChannelSO", menuName = "Scriptable Objects/DeathEventChannelSO")]
public class DeathEventChannelSO : ScriptableObject
{
    public event Action <DeathCause> OnDeathEvent;

    public void Raise(DeathCause cause)
    {
        OnDeathEvent?.Invoke(cause);
    }
}
