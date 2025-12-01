using UnityEngine;

public class PlayerDeathOnFall : MonoBehaviour
{
    [SerializeField] private string deathTrigger = "Die";
    [SerializeField] private MonoBehaviour[] controlsToDisable;
    private CameraControls cam;
    private Rigidbody2D playerPhysics;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private bool dead = false;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraControls>();
        playerPhysics = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dead || cam == null) return;

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
        PositionAtBottom();
        FreezePlayer();
        DisablePlayerScripts();
        DeathAnimation();
        AudioManager.instance.StopBGM();
        SceneController.instance.ChangeScene("MainMenu");
    }

    private void DeathAnimation()
    {
        if (_animator) _animator.SetTrigger(deathTrigger);
    }

    private void DisablePlayerScripts()
    {
        foreach (var component in controlsToDisable)
            if (component) component.enabled = false;
    }

    private void FreezePlayer()
    {
        playerPhysics.linearVelocity = Vector2.zero;
        playerPhysics.angularVelocity = 0f;
        playerPhysics.gravityScale = 0f;
        playerPhysics.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    private void PositionAtBottom()
    {
        float offsetY = spriteRenderer.bounds.extents.y;
        var p = transform.position;
        p.y = cam.BottomVisibleWorldY() + offsetY;
        transform.position = p;
    }
}
