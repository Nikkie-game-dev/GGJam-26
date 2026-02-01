using UnityEngine;

public interface IHoldInteractable
{
    void BeginHold(GameObject interactor);
    void CancelHold(GameObject interactor);
}
