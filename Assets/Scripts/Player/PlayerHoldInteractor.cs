using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHoldInteractor : MonoBehaviour
{
    private IHoldInteractable current;
    private InputAction interactAction;

    private void Awake()
    {
        interactAction = new InputAction(
            name: "Interact",
            binding: "<Keyboard>/e"
        );
        interactAction.Enable();
    }

    private void Update()
    {
        if (interactAction.WasPressedThisFrame())
            current?.BeginHold(GetPlayerRoot());

        if (interactAction.WasReleasedThisFrame())
            current?.CancelHold(GetPlayerRoot());
    }

    private GameObject GetPlayerRoot()
    {
        return transform.root.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHoldInteractable>(out var interactable))
            current = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<IHoldInteractable>(out var interactable))
            return;

        if (current == interactable)
        {
            current.CancelHold(GetPlayerRoot());
            current = null;
        }
    }

    private void OnDestroy()
    {
        interactAction.Disable();
    }
}
