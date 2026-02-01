using UnityEngine;

public interface IInteractible
{
    public void Interact(GameObject interactionOrigin);
    public void ExitInteraction(GameObject interactionOrigin);
}

