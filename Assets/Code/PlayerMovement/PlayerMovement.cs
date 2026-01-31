using Assets.Code.Service;
using Systems.TagClassGenerator;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerMovement : MonoBehaviour
    {
        private float MovementDir => ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.MovingDir.ReadValue<float>();

        private bool _canJump = false;

        private Rigidbody _rb;

        [SerializeField] private float _jumpForce = 10;
        [SerializeField] private float _moveForce = 100;

        private bool _isMoving = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Jump.started += OnJump;
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Move.started += OnMoveStarted;
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Move.canceled += OnMoveCanceled;
        }

        private void Update()
        {
            if (_isMoving)
                OnMove();
        }

        private void OnMoveStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _isMoving = true;
        }

        private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _isMoving = false;
        }

        private void OnMove()
        {
            _rb.AddForce((MovementDir > 0 ? Vector3.right : Vector3.left) * Time.deltaTime * _moveForce, ForceMode.Force);
        }

        private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext _)
        {
            if (_canJump)
            {
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _canJump = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(Tags.Ground))
                _canJump = true;
        }

        private void OnDisable()
        {
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Jump.started -= OnJump;
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Move.started -= OnMoveStarted;
            ServiceProvider.Instance.GetService<InputMG.InputManager>().InputSystem.Player.Move.canceled -= OnMoveCanceled;
        }
    }
}
