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
        lookVector += look.ReadValue<Vector2>() / 100;
        if (lookVector.x > playerRotationController.minForce || lookVector.y > playerRotationController.minForce || lookVector.x < -playerRotationController.minForce 
                        || lookVector.y < -playerRotationController.minForce)
        {
            lookVector.x = Mathf.Clamp(lookVector.x, -5, 5);
            lookVector.y = Mathf.Clamp(lookVector.y, -5, 5);
            targetAngle = Mathf.Atan2(lookVector.x, lookVector.y);
        }
        if (playerMovementController.isSprinting == true)
            targetCameraPositionAdjust = sprintLookDistanceMultiplier * move.ReadValue<Vector2>();
        if (playerRotationController.isAiming == true)
        {
            nowAngle = Mathf.LerpAngle(nowAngle, targetAngle * Mathf.Rad2Deg, lerpStrenght / playerRotationController.aimSpeedMultiplier);
            targetCameraPositionAdjust.x = aimLookDistanceMultiplier * Mathf.Sin(nowAngle * Mathf.Deg2Rad);
            targetCameraPositionAdjust.y = aimLookDistanceMultiplier * Mathf.Cos(nowAngle * Mathf.Deg2Rad);
        }
        else
        {
            lookVector += look.ReadValue<Vector2>() / 100;
            lookVector.x = Mathf.Clamp(lookVector.x, -5, 5);
            lookVector.y = Mathf.Clamp(lookVector.y, -5, 5);
            targetAngle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
            nowAngle = targetAngle * Mathf.Rad2Deg;
        }

        nowCameraPositionAdjust.x = Mathf.Lerp(nowCameraPositionAdjust.x, targetCameraPositionAdjust.x, lerpStrenght);
        nowCameraPositionAdjust.y = Mathf.Lerp(nowCameraPositionAdjust.y, targetCameraPositionAdjust.y, lerpStrenght);
        transform.position = new Vector3(playerPosition.position.x + nowCameraPositionAdjust.x ,cameraHeight ,playerPosition.position.z + nowCameraPositionAdjust.y);

        targetCameraPositionAdjust = Vector2.zero;
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;

    }
}
