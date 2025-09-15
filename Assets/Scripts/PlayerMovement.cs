using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Animator _animator;
    private Vector2 moveInput;
    private Rigidbody2D playerPhysics;
    private bool isGrounded;
    private bool jumpRequested;

    void Awake()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.action.performed += HandleInput;
        moveAction.action.canceled += HandleInput;

        jumpAction.action.performed += HandleJump;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        playerPhysics.linearVelocity = new Vector2(moveInput.x * speed, playerPhysics.linearVelocityY);

        if (isGrounded && jumpRequested)
        {
            playerPhysics.linearVelocity = new Vector2(moveInput.x * speed, jumpForce);
            jumpRequested = false;
        }
    }

    private void HandleInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move input: {moveInput.x}");
        if (moveInput.x != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        if (moveInput.x == -1)
        {

            transform.Rotate(0, 180f, 0);
        }

        if (moveInput.x == 1)
        {
            transform.Rotate(0, 180f, 0);
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            jumpRequested = true;
        }
    }

    public void EventTriggered()
    {
        // Debug.Log("test");
    }
}
