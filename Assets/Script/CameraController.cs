using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction move;
    private InputAction look;
    public Transform playerPosition;

    public PlayerMovementController playerMovementController;
    public PlayerRotationController playerRotationController;

    public float cameraHeight = 10f;
    public float lerpStrenght = 0.25f;
    public float sprintLookDistanceMultiplier = 5f;
    public float aimLookDistanceMultiplier = 2f;

    private float targetAngle;
    private float nowAngle;

    public Vector2 nowCameraPositionAdjust;
    public Vector2 targetCameraPositionAdjust;

    private Vector2 lookVector;
    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        look = playerControl.Player.Look;
        move = playerControl.Player.Move;
        look.Enable();
        move.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
        move.Disable();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (playerMovementController.isSprinting == true)
            targetCameraPositionAdjust = sprintLookDistanceMultiplier * move.ReadValue<Vector2>();
        else if (playerRotationController.isAiming == true)
        {
            lookVector = (look.ReadValue<Vector2>());
            if (lookVector.x > playerRotationController.minForce || lookVector.y > playerRotationController.minForce || lookVector.x < -playerRotationController.minForce || lookVector.y < -playerRotationController.minForce)
            {
                targetAngle = Mathf.Atan2(lookVector.x, lookVector.y);
                targetCameraPositionAdjust.x = aimLookDistanceMultiplier * Mathf.Sin(targetAngle);
                targetCameraPositionAdjust.y = aimLookDistanceMultiplier * Mathf.Cos(targetAngle);
            }
        }
        else targetCameraPositionAdjust = Vector2.zero;

        nowCameraPositionAdjust.x = Mathf.Lerp(nowCameraPositionAdjust.x, targetCameraPositionAdjust.x, lerpStrenght);
        nowCameraPositionAdjust.y = Mathf.Lerp(nowCameraPositionAdjust.y, targetCameraPositionAdjust.y, lerpStrenght);

        transform.position = new Vector3(playerPosition.position.x + nowCameraPositionAdjust.x ,cameraHeight ,playerPosition.position.z + nowCameraPositionAdjust.y);
    }
}
