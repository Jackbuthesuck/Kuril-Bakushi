using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1.0f;
    public PlayerInputAction playerControl;

    private InputAction move;

    UnityEngine.Vector2 moveDirection = UnityEngine.Vector2.zero;

    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        move = playerControl.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
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
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed,0 , moveDirection.y * moveSpeed);
    }
}
