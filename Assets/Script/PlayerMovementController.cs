using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerInputAction playerControl;

    private InputAction move;
    private InputAction sprint;

    public PlayerRotationController rotationController;

    public float moveSpeed = 1.0f;
    public float sprintMultiplier = 2.0f;

    public bool isSprinting;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        sprint = playerControl.Player.Sprint;
        sprint.Enable();
        move = playerControl.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable(); 
        sprint.Disable();
    }
    void Start()
    {
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (sprint.IsInProgress())
        {
            isSprinting = true;
            moveDirection *= sprintMultiplier;
        }
        else isSprinting = false;
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.y * moveSpeed);
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 18;
        GUI.Label(new Rect(10, 10, 0, 0), "isSprinting: " + isSprinting, style);
    }
}
