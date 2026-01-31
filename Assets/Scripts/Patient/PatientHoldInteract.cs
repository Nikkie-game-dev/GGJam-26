using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PatientHoldInteract : MonoBehaviour, IHoldInteractable
{
    [Header("Config")]
    [SerializeField] private PatientData data;

    [Header("Visual")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color curedColor = new Color(0.7f, 1f, 0.7f, 1f);
    [SerializeField] private string colorProperty = "_BaseColor";


    private bool playerInRange;
    private bool isHolding;
    private bool cured;
    private float holdTimer;
    private GameObject currentInteractor;
    private MaterialPropertyBlock mpb;


    private void Reset()
    {
        targetRenderer = GetComponentInChildren<Renderer>();
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        var col = GetComponent<Collider>();
        col.isTrigger = true;

        mpb = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (!isHolding || cured) return;

        holdTimer += Time.deltaTime;

        if (holdTimer >= data.healHoldSeconds)
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

        //HighscoreManager.Instance.AddScore(data.scoreValue);

        //ApplyColor(curedColor);
        //Debug.Log($"[Patient] CURED CONFIRMED '{name}' at time={Time.time:F2}", this);
        GetComponent<Collider>().enabled = false;


    }
    private void ApplyColor(Color c)
    {
        if (targetRenderer = null) return;

        targetRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(colorProperty, c);
        targetRenderer.SetPropertyBlock(mpb);
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