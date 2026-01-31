using Code.Characters.Rats.RatStates;
using UnityEngine;

namespace Code.Characters.Rats
{
    enum RatsStates
    {
        Verital,
        Orizontal
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class RatMovement : MonoBehaviour, IStatable
    {
        private Rigidbody _rb;

        private RatState _currentState;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _currentState = new RatHorizontalMovement(this, _rb, 400f);
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
