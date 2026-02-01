using Assets.Code.Characters.Rats.RatStates;
using Code.Characters.Rats.RatStates;
using Systems.LayerClassGenerator;
using UnityEngine;

namespace Code.Characters.RatsMovBig
{

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class BigRatMovement : MonoBehaviour, IStatable
    {
        [SerializeField] float _movementForce = 600f;

        private RatState _currentState;

        private RatState _horizontalMovement;
        private RatState _verticalMovement;
        private RatState _fallingMovement;

        private void Awake()
        {
            Rigidbody _rb = GetComponent<Rigidbody>();
            _rb.excludeLayers = LayerMask.GetMask(Layers.Rat);

            _verticalMovement = new BigRatVerticalMovement(this, _rb, _movementForce, IStatable.MovementAxis.Horizontal);
            _horizontalMovement = new BigRatHorizontalMovement(this, _rb, _movementForce, IStatable.MovementAxis.Vertical);
            _fallingMovement = new RatFallState(this, _rb, _movementForce, GetComponent<BoxCollider>());

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
            _currentState = state switch
            {
                IStatable.MovementAxis.Horizontal => _horizontalMovement,
                IStatable.MovementAxis.Vertical => _verticalMovement,
                IStatable.MovementAxis.Falling => _fallingMovement,
                _ => _currentState
            };
        }

    }
}
