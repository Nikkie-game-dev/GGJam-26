using UnityEngine;

[CreateAssetMenu(fileName = "PatientData", menuName = "Scriptable Objects/PatientData")]
public class PatientSO : ScriptableObject
{
    public PatientType patientType;
    public int scoreValue;

    [Min(0.1f)]
    [SerializeField] public float healHoldSeconds = 2f;
}
