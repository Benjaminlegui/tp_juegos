using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference kickAction;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string runningAnimation = "isRunning";
    [SerializeField] private string kickAnimation = "Kick";
    [SerializeField] private PlayerAudio playerAudio;
    private Vector2 moveInput;
    private Rigidbody2D playerPhysics;
    private bool isGrounded;
    private bool jumpRequested;
    public static float minX;
    public static float maxX;

    void Awake()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        moveAction.action.performed += HandleInput;
        moveAction.action.canceled += HandleInput;

        kickAction.action.performed += HandleKick;

        jumpAction.action.performed += HandleJump;

        moveAction.action.Enable();
        jumpAction.action.Enable();
        kickAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.performed -= HandleInput;
        moveAction.action.canceled -= HandleInput;

        kickAction.action.performed -= HandleKick;

        jumpAction.action.performed -= HandleJump;

        moveAction.action.Disable();
        jumpAction.action.Disable();
        kickAction.action.Disable();
    }

    void Start()
    {
        playerPhysics.freezeRotation = true;

        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        minX = -halfWidth;
        maxX = halfWidth;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        playerPhysics.linearVelocity = new Vector2(moveInput.x * speed, playerPhysics.linearVelocityY);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        transform.position = clampedPos;

        if (isGrounded && jumpRequested)
        {
            playerPhysics.linearVelocity = new Vector2(moveInput.x * speed, jumpForce);
            playerAudio.PlayJumpSfx();
            jumpRequested = false;
        }

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            spriteRenderer.flipX = moveInput.x < 0f;
        }
    }

    private void HandleInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0)
        {
            _animator.SetBool(runningAnimation, true);
        }
        else
        {
            _animator.SetBool(runningAnimation, false);
        }

        if (moveInput.x != 0 && isGrounded)
        {
            playerAudio.PlayRunSfx();
        }
        else
        {
            playerAudio.StopRunSfx();
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void HandleKick(InputAction.CallbackContext ctx)
    {
        _animator.SetTrigger(kickAnimation);
        playerAudio.PlayKickSfx();
    }
}
