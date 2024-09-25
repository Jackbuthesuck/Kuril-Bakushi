using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraHeight = 10;
    public Transform playerPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        transform.position = new UnityEngine.Vector3(playerPosition.position.x,cameraHeight ,playerPosition.position.z);
    }
}
