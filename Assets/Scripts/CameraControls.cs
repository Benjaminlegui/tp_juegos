using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float scrollSpeed = 2.5f;
    public bool onlyMoveUp = true;
    public bool startScrolling = false;
    [SerializeField] private GameObject cameraTrigger;

    [SerializeField] private Transform player;
    private Camera cam;
    private float targetY;
    private float halfHeight;

    void Awake()
    {
        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        targetY = transform.position.y;
    }

    void Update()
    {
        if (!startScrolling || player == null) return;

        // objetivo mínimo: seguir al jugador pero solo hacia arriba
        float desiredY = player.position.y;

        if (onlyMoveUp)
            targetY = Mathf.Max(targetY, desiredY); // nunca bajar el target
        else
            targetY = desiredY;

        // además, la cámara sube por su cuenta a una velocidad constante
        targetY += scrollSpeed * Time.deltaTime;

        // suavizado simple
        float newY = Mathf.Lerp(transform.position.y, targetY, 0.15f);
        if (onlyMoveUp)
            newY = Mathf.Max(transform.position.y, newY); // evita bajar por lerp

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void StartScrolling()
    {
        startScrolling = true;
        targetY = Mathf.Max(targetY, transform.position.y);
    }

    public void StopScrolling()
    {
        Debug.Log("stop");
        startScrolling = false;
    }

    // Helper para Game Over por “alcance”
    public float BottomVisibleWorldY()
    {
        return transform.position.y - halfHeight;
    }
}
