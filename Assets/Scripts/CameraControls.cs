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

        float desiredY = player.position.y;

        if (onlyMoveUp)
            targetY = Mathf.Max(targetY, desiredY);
        else
            targetY = desiredY;

        targetY += scrollSpeed * Time.deltaTime;

        float newY = Mathf.Lerp(transform.position.y, targetY, 0.15f);
        if (onlyMoveUp)
            newY = Mathf.Max(transform.position.y, newY);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void StartScrolling()
    {
        startScrolling = true;
        targetY = Mathf.Max(targetY, transform.position.y);
    }

    public void StopScrolling()
    {
        startScrolling = false;
    }

    // Bottom position of the camera
    public float BottomVisibleWorldY()
    {
        return transform.position.y - halfHeight;
    }
}
