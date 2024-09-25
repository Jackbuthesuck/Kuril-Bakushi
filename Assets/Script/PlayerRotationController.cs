using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerRotationController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerPosition;
    public PlayerInputAction playerControl;

    private InputAction look;

    public float lookSpeed;

    private float mathAngle = 0;
    private float targetAngle = 0;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        look = playerControl.Player.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
    }
    void Start()
    {
        rb.freezeRotation = true;
    }

    void Update()
    {
        transform.position = new Vector3(playerPosition.position.x, 0.5f + playerPosition.position.y, playerPosition.position.z);

        Vector2 lookVector = look.ReadValue<Vector2>();
        if (mathAngle < targetAngle) mathAngle += lookSpeed;
        else mathAngle -= lookSpeed;
        if (lookVector != Vector2.zero) targetAngle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
        rb.MoveRotation(Quaternion.Euler(0, mathAngle, 0));
    }

    private void FixedUpdate()
    {

    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        GUI.Label(new Rect(10, 30, 0, 0), "Math Angle: " + mathAngle, style);
        GUI.Label(new Rect(10, 50, 0, 0), "Targ Angle: " + targetAngle, style);
    }
}
