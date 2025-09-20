using UnityEngine;

public class PlayerDeathOnFall : MonoBehaviour
{
    [SerializeField] private string deathTrigger = "Die";
    [SerializeField] private MonoBehaviour[] controlsToDisable;
    private CameraControls cam;
    private Rigidbody2D playerPhysics;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private bool dead;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraControls>();
        playerPhysics = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dead || cam == null || !cam.startScrolling) return;

        float bottomY = cam.BottomVisibleWorldY();
        if (transform.position.y < bottomY)
        {
            DieAndFreezeOnBottom();
        }
    }

    private void DieAndFreezeOnBottom()
    {
        dead = true;

        cam.StopScrolling();

        // 2) Player rest on the bottom of the camera
        float offsetY = spriteRenderer.bounds.extents.y;
        var p = transform.position;
        p.y = cam.BottomVisibleWorldY() + offsetY;
        transform.position = p;

        // 3) Freeze the player from falliing
        playerPhysics.linearVelocity = Vector2.zero;
        playerPhysics.angularVelocity = 0f;
        playerPhysics.gravityScale = 0f;
        playerPhysics.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        // 4) Disable all of the scripts that controls the player
        foreach (var component in controlsToDisable)
            if (component) component.enabled = false;

        // 5) Execute death animation
        if (_animator) _animator.SetTrigger(deathTrigger);
    }
}
