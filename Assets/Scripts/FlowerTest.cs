using System;
using UnityEngine;

public class FlowerTest : MonoBehaviour
{
    public float timeAmount = 0f;
    public void OnTriggerEnter(Collider other)
    {

        if (!other.TryGetComponent<IInteractible>(out IInteractible interactionObject))
        return;
        
        if (other.TryGetComponent<FlowerBase>(out FlowerBase flower))
        timeAmount += flower.GetTime();
        
        interactionObject.Interact(this.gameObject);

 
    }
}
