using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [Header("Patient Data")]
    [SerializeField] private PatientType patientType;
    [SerializeField] private int pointsGranted = 100;
    private bool cured = false;

    public void Interact()
    {
        if (cured) 
            return;

        Cure();
    }

    private void Cure()
    {
        cured = true;

        //ScoreManager.Instance.AddScore(pointsGranted);

        OnCured();
    }

    protected virtual void OnCured()
    {
        //Aca puede ir cualquier tipo de animacion o efecto.
        gameObject.SetActive(false);
    }
}
