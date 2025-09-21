using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraCollider;
    private CameraControls camControls;

    void Awake()
    {
        camControls = Camera.main.GetComponent<CameraControls>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            camControls.StartScrolling();
        }

        if (other.transform == cameraCollider)
        {
            camControls.StopScrolling();
        }
    }
}
