using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Code.InputMG;
using Code.Service;

public class Stairs : MonoBehaviour, IInteractible
{
    [SerializeField] 
    private List<Transform> _wayPoints;
    [SerializeField] [Range(0f, 3f)] 
    private float _timeToWalkAllTheZiplineInSeconds;

    private bool _bIsActive = false;
    private bool _bCanMove = false;
    private void CanMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("works");
        if (context.ReadValue<Vector2>().y > 0)
        {
            
            _bCanMove = true;
        }
        else
        {
            _bCanMove = false;
        }
    }
    private void CantMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _bCanMove = false;
    }
    public void Interact(GameObject interactionOrigin)
    {
        if (!_bIsActive)
            StartCoroutine(Interact_Implementation(interactionOrigin));
    }
    public IEnumerator Interact_Implementation(GameObject interactionOrigin)
    {
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.Move.started += CanMove;
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.Move.canceled += CantMove;
        
        
        _bIsActive = true;
        float individualTimeForEachSegment = _timeToWalkAllTheZiplineInSeconds / _wayPoints.Count;
        float t = 0;
        Transform targetTransform = interactionOrigin.transform;
        Rigidbody rb = interactionOrigin.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        
        for (int i = 0; i < _wayPoints.Count; i++)
        {
            Vector3 initialPosition = targetTransform.position;
            while (t < 1)
            {
                if (_bCanMove)
                {
                    t += Time.deltaTime / individualTimeForEachSegment;
                    targetTransform.position = Vector3.Lerp(initialPosition, _wayPoints[i].position, t);
                }
                yield return null;
            }
            t = 0f;
        }
        
        rb.isKinematic = false;
        _bIsActive = false;
        
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.Move.started -= CanMove;
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.Move.canceled -= CantMove;
    }
}
