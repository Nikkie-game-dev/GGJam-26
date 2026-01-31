using Code.Characters.Rats.RatStates;
using UnityEngine;

namespace Code.Characters.RatsMovBig
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class BigRatMovement : MonoBehaviour, IStatable
    {
        private Rigidbody _rb;

        private RatState _currentState;

        private RatState _horizontalMovement;
        private RatState _verticalMovement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _verticalMovement = new BigRatVerticalMovement(this, _rb, 700f, IStatable.MovementAxis.Horizontal);
            _horizontalMovement = new BigRatHorizontalMovement(this, _rb, 500f , IStatable.MovementAxis.Vertical);

            _currentState = _horizontalMovement;
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
