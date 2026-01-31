using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class FlowerBase : MonoBehaviour, IInteractible
{
    [Header("Time properties")]
    [SerializeField] [Tooltip("The amount of time the item adds to the player")] private float _timeAmount;
    public float GetTime() { return _timeAmount; }

    [Header("Components")]
    private ParticleSystem _onDestroyParticles;
    private Animation _onDestroyAnimation;
    private AudioClip _onDestroySound;

    private bool bIsAlreadyActived = false;
    public void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    public void Awake()
    {
        if(TryGetComponent<ParticleSystem>(out _onDestroyParticles));
        if (TryGetComponent<Animation>(out _onDestroyAnimation)) ;
        if (TryGetComponent<AudioClip>(out _onDestroySound)) ;
    }
    
    public void Interact(GameObject interactionOrigin)
    {
        if (!bIsAlreadyActived)
        {
            bIsAlreadyActived = true;
            StartCoroutine(Interact_Implementation(interactionOrigin));
        }
    }
    private IEnumerator Interact_Implementation(GameObject interactionOrigin)
    {
        StartCoroutine(OnDestroyAnimation_Execution());
        StartCoroutine(OnDestroySound_Execution());
        yield return StartCoroutine(OnDestroyParticles_Execution());
        
        Destroy(this.gameObject);
    }

    private IEnumerator OnDestroyAnimation_Execution()
    {
        yield return null;
    }

    private IEnumerator OnDestroySound_Execution()
    {
        yield return null;
    }

    private IEnumerator OnDestroyParticles_Execution()
    {
        yield return null;
    }
}
