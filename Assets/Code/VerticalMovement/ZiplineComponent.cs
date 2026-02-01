using System.Collections;
using System.Collections.Generic;
using Code.InputMG;
using Code.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZiplineComponent : MonoBehaviour, IInteractible
{
    [SerializeField] 
    private List<Transform> _wayPoints;
    [SerializeField] [Range(0f, 3f)] 
    private float _timeToWalkAllTheZiplineInSeconds;


    private bool _bIsActive = false;
    private Coroutine timerHandle;
    
    InputManager InputManager => ServiceProvider.Instance.GetService<InputManager>();
    public void Interact(GameObject interactionOrigin)
    {
        if (!_bIsActive)
       timerHandle = StartCoroutine(Interact_Implementation(interactionOrigin));
        InputManager.InputSystem.Player.Move.canceled += StopMoving;
    }
    public void ExitInteraction(GameObject interactionOrigin){}
    private IEnumerator Interact_Implementation(GameObject interactionOrigin)
    {
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
                t += Time.deltaTime / individualTimeForEachSegment;
                targetTransform.position = Vector3.Lerp(initialPosition, _wayPoints[i].position, t);
                yield return null;
            }
            t = 0f;
        }
        
        rb.isKinematic = false;
        _bIsActive = false;
    }

    private void StopMoving(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        StopCoroutine(timerHandle);
       
    }
}
