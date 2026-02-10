using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHoldInteractor : MonoBehaviour
{


    private MonoBehaviour current;
    private InputAction interactAction;

    private void Awake()
    {
        interactAction = new InputAction("Interact", binding: "<Keyboard>/e");
        interactAction.Enable();
    }

    private void Update()
    {
        if (current == null) return; 

        var interactable = current as IHoldInteractable;
        if (interactable == null)
        {
            current = null;
            return;
        }

        if (interactAction.WasPressedThisFrame())
        {
            Debug.Log($"[Interactor] E Down -> BeginHold on {current.name}", current);
            interactable.BeginHold(GetPlayerRoot());
        }

        if (interactAction.WasReleasedThisFrame())
        {
            Debug.Log($"[Interactor] E Up -> CancelHold on {current.name}", current);
            interactable.CancelHold(GetPlayerRoot());
        }
    }

    private GameObject GetPlayerRoot()
    {
        return transform.root.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        var mb = FindHoldInteractableBehaviour(other);
        if (mb == null)
        {
            return;
        }

        current = mb;
        Debug.Log($"[Interactor] Target set -> {current.name} (from collider '{other.name}')", current);
    }

    private void OnTriggerExit(Collider other)
    {
        if (current == null) return;

        var mb = FindHoldInteractableBehaviour(other);
        if (mb != null && mb == current)
        {
            Debug.Log($"[Interactor] Exit -> cancel + clear {current.name}", current);
            (current as IHoldInteractable)?.CancelHold(GetPlayerRoot());
            current = null;
        }
    }

    private MonoBehaviour FindHoldInteractableBehaviour(Collider other)
    {
        var list = other.GetComponents<MonoBehaviour>();
        for (int i = 0; i < list.Length; i++)
            if (list[i] is IHoldInteractable) return list[i];

        list = other.GetComponentsInParent<MonoBehaviour>(true);
        for (int i = 0; i < list.Length; i++)
            if (list[i] is IHoldInteractable) return list[i];

        return null;
    }

    private void OnDestroy()
    {
        interactAction.Disable();
    }
}
