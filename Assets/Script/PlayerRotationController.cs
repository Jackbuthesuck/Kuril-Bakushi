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

    public float lookSpeed;
    public float minForce;
    public float lerpStrenght;

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
            lookVector = look.ReadValue<Vector2>();
            if (lookVector.x > minForce || lookVector.y > minForce || lookVector.x < -minForce || lookVector.y < -minForce)
                targetAngle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
        }
        nowAngle = Mathf.LerpAngle(nowAngle, targetAngle, lerpStrenght);
        rb.MoveRotation(Quaternion.Euler(0, nowAngle, 0));
    }

    private void FixedUpdate()
    {
        if (aim.IsInProgress())
        {
            isAiming = true;
        }
        else isAiming = false;
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        GUI.Label(new Rect(10, 30, 0, 0), "Math Angle: " + nowAngle, style);
        GUI.Label(new Rect(10, 50, 0, 0), "Targ Angle: " + targetAngle, style);
        GUI.Label(new Rect(10, 70, 0, 0), "Look X: " + lookVector.x, style);
        GUI.Label(new Rect(10, 90, 0, 0), "Look Y: " + lookVector.y, style);
    }
}
