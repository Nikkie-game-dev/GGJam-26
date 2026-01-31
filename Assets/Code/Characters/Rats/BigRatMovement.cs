using Assets.Code.Characters.Rats.RatStates;
using UnityEngine;

namespace Assets.Code.Characters.Rats
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class BigRatMovement : MonoBehaviour, IStatable
    {
        private Rigidbody _rb;
        private BoxCollider _collider;

        private RatState _currentState;

        private RatState horizontalMovement;
        private RatState verticalMovement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();

            verticalMovement = new BigRatVerticalMovement(this, _rb, _collider, 700f, IStatable.MovementAxis.Horizontal);
            horizontalMovement = new BigRatHorizontalMovement(this, _rb, _collider, 500f , IStatable.MovementAxis.Vertical);

            _currentState = horizontalMovement;
        }

        private void Update()
        {
            _currentState.Tick(Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _currentState.OnCollision(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _currentState.OnCollisionExit(collision);
        }

        public void SetState(IStatable.MovementAxis state)
        {
            switch ((IStatable.MovementAxis)state)
            {
                case IStatable.MovementAxis.Horizontal:
                    _currentState = horizontalMovement;
                    break;

                case IStatable.MovementAxis.Vertical:
                    _currentState = verticalMovement;
                    break;
            }
        }

    }
}
