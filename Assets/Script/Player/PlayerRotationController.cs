using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerRotationController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerPosition;
    public PlayerInputAction playerControl;

    private InputAction look;
    private InputAction move;
    private InputAction aim;

    public PlayerMovementController playerMovementController;
    public CameraController cameraController;
    public GameObject rotLongOverlay;

    public float lookSpeed;
    public float minForce;
    public float lerpStrenght;
    public float aimSpeedMultiplier;

    public bool isAiming;

    private float nowAngle = 0;
    private float targetAngle = 0;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookVector = Vector2.zero;
    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        look = playerControl.Player.Look;
        move = playerControl.Player.Move;
        aim = playerControl.Player.Aim;
        look.Enable();
        move.Enable();
        aim.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
        move.Disable();
        aim.Disable();
    }
    void Start()
    {
        rb.freezeRotation = true;
    }

    void Update()
    {
        transform.position = new Vector3(playerPosition.position.x, 0.5f + playerPosition.position.y, playerPosition.position.z);
        if (playerMovementController.isSprinting == true)
        {
            lookVector = move.ReadValue<Vector2>();
            targetAngle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
        }
        else
        {
            lookVector += look.ReadValue<Vector2>() / 100;
            lookVector.x = Mathf.Clamp(lookVector.x, -5, 5);
            lookVector.y = Mathf.Clamp(lookVector.y, -5, 5);
            targetAngle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
        }
        if (aim.IsInProgress())
        {
            isAiming = true;
            rotLongOverlay.SetActive(true);
            nowAngle = Mathf.LerpAngle(nowAngle, targetAngle, lerpStrenght / aimSpeedMultiplier);
        }
        else
        {
            isAiming = false;
            rotLongOverlay.SetActive(false);
            nowAngle = Mathf.LerpAngle(nowAngle, targetAngle, lerpStrenght);
        }
        rb.MoveRotation(Quaternion.Euler(0, nowAngle, 0));
    }

    private void FixedUpdate()
    {

    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
    }
}
