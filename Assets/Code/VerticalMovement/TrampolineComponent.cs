using UnityEngine;

public class TrampolineComponent : MonoBehaviour, IJumpable
{
    [Header("Attributes")]
    [SerializeField][Range(0f, 100f)] 
    private float _trampolineForce;

    public void JumpOn(GameObject origin)
    {
        if (!origin.TryGetComponent<Rigidbody>(out Rigidbody rb)) 
            return;
        rb.AddForce(Vector3.up * _trampolineForce, ForceMode.Impulse);
    }
}
