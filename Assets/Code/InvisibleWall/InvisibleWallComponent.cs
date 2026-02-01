using System;
using System.Collections;
using UnityEngine;

//Attach this component to an invisible trigger inside the invisible wall room

[RequireComponent(typeof(BoxCollider))]
public class InvisibleWallComponent : MonoBehaviour, IInteractible
{
    [Header("Attributes")] 
    [SerializeField][Range(0f, 1f)] 
    private float _opacityWhenActived = 0.2f;
    [SerializeField][Range(0.1f, 2f)]
    private float _timeToFadeInSeconds = 0.3f;
    [SerializeField][Tooltip("This mesh must have OpacityTweak Shader")]
    private Renderer _wallMesh;
    private Material _material;

    public void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Awake()
    {
        _material = _wallMesh.material;
    }

    public void Interact(GameObject interactor)
    {
        StartCoroutine(Interact_Implementation());
    }
    public void ExitInteraction(GameObject interactionOrigin)
    {
        StartCoroutine(ExitInteraction_Implementation());
    }
    private IEnumerator Interact_Implementation()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / _timeToFadeInSeconds;
            _material.SetFloat("_Opacity", Mathf.Lerp(1f, _opacityWhenActived, t));
            yield return null;
        }
        _material.SetFloat("_Opacity", _opacityWhenActived);
    }

    private IEnumerator ExitInteraction_Implementation()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / _timeToFadeInSeconds;
            _material.SetFloat("_Opacity", Mathf.Lerp(_opacityWhenActived, 1f, t));
            yield return null;
        }
        _material.SetFloat("_Opacity", 1f);
    }
}
