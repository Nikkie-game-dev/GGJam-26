using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PatientHoldInteract : MonoBehaviour, IHoldInteractable
{
    [Header("Config")]
    [SerializeField] private PatientSO patientData;
    [SerializeField] private ScoreStateSO scoreState;

    private bool playerInRange;
    private bool isHolding;
    private bool cured;
    private float holdTimer;
    private GameObject currentInteractor;
    private MaterialPropertyBlock mpb;

    Collider col;

    [SerializeField] Image image;


    private void Reset()
    {
        col.isTrigger = true;
    }

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
        mpb = new MaterialPropertyBlock();
    }

    private void Start()
    {
        AdjustSelf();
    }

    private void AdjustSelf()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, float.MaxValue))
        {
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            float radious = collider.radius + collider.radius * 0.5f;

            transform.position = new Vector3(transform.position.x, hit.point.y + radious, transform.position.z);
        }
    }

    private void Update()
    {
        if (!isHolding || cured) return;

        holdTimer += Time.deltaTime;

        if (holdTimer >= patientData.healHoldSeconds)
        {
            CompleteCure();
        }
    }

    public void BeginHold(GameObject interactor)
    {
        if (cured) return;
        if (!playerInRange) return;

        if (!interactor.CompareTag("Player")) return;

        currentInteractor = interactor;
        isHolding = true;
    }

    public void CancelHold(GameObject interactor)
    {
        if (interactor != currentInteractor) return;

        isHolding = false;
        holdTimer = 0f;
        currentInteractor = null;
        Debug.Log($"[Patient] CancelHold '{name}' timer reset.", this);
    }

    private void CompleteCure()
    {
        cured = true;
        isHolding = false;
        scoreState.Add(patientData.scoreValue);
        Destroy(this.gameObject, 0.1f);

        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = false;

        if (isHolding && currentInteractor == other.gameObject)
            CancelHold(other.gameObject);
    }
}