using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Code.InputMG;
using Code.Service;


// Attach to a trigger then set thhe waypoints in order
public class StairsComponent : MonoBehaviour, IInteractible
{
    [SerializeField] 
    private List<Transform> _wayPoints;
    [SerializeField] [Range(0f, 3f)] 
    private float _timeToWalkAllTheZiplineInSeconds;

    private bool _bIsRunning = false;
    public bool _bCanMove = false;

    public void Interact(GameObject interactionOrigin)
    {
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.performed += CanMove;
        ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.canceled += CantMove;
        StartCoroutine(Interact_Implementation(interactionOrigin));
    }

    public void ExitInteraction(GameObject interactionOrigin)
    {
        if (!_bIsRunning)
        {
            StopAllCoroutines();
            ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.started -= CanMove;
            ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.canceled -= CantMove;
        }
    }
    public IEnumerator Interact_Implementation(GameObject interactionOrigin)
    {
        yield return new WaitUntil(() => _bCanMove);

            _bIsRunning = true;
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
                    else
                    {
                        if (_wayPoints.Count - i == 1)
                            break;
                    }
                    yield return null;
                }

                t = 0f;
            }

            rb.isKinematic = false;
            _bIsRunning = false;

            ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.started -= CanMove;
            ServiceProvider.Instance.GetService<InputManager>().InputSystem.Player.MoveY.canceled -= CantMove;
            _bIsRunning = false;
    }
    private void CanMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log(" " + context.ReadValue<float>());
        _bCanMove = context.ReadValue<float>() > 0;
    }
    private void CantMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _bCanMove = false;
    }
}
