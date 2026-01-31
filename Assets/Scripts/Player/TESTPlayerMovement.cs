using UnityEngine;
using UnityEngine.InputSystem;
public class TESTPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.18f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;

        moveAction = new InputAction(
            name: "Move",
            binding: "<Keyboard>/a"
        );
        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/d")
            .With("Positive", "<Keyboard>/rightArrow");

        jumpAction = new InputAction(
            name: "Jump",
            binding: "<Keyboard>/space"
        );

        moveAction.Enable();
        jumpAction.Enable();
    }

    private void Update()
    {
        if (groundCheck != null)
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            var v = rb.linearVelocity;
            v.y = 0f;
            rb.linearVelocity = v;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        float x = moveAction.ReadValue<float>();

        var v = rb.linearVelocity;
        v.x = x * moveSpeed;
        rb.linearVelocity = v;
    }

    private void OnDestroy()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }
}
