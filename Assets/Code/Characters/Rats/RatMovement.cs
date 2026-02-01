using Code.Characters.Rats.RatStates;
using Systems.LayerClassGenerator;
using UnityEngine;

namespace Code.Characters.Rats
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class RatMovement : MonoBehaviour, IStatable
    {
        private RatState _currentState;

        [SerializeField] private float _movementSpeed;

        private void Awake()
        {
            Rigidbody _rb = GetComponent<Rigidbody>();

            _rb.excludeLayers = LayerMask.GetMask(Layers.Rat);

            _currentState = new RatHorizontalMovement(this, _rb, _movementSpeed);
        }

        private void Update()
        {
            _currentState.Tick(Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _currentState.OnCollision(collision);
        }

        public void SetState(IStatable.MovementAxis state)
        {
        }
    }
}
