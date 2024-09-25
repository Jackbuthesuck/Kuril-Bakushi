using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerRotationController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerPosition;
    public PlayerInputAction playerControl;

    private InputAction look;

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

    }

    void Update()
    {
        transform.position = new UnityEngine.Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);

        Vector2 lookVector = look.ReadValue<Vector2>();
        float angle = Mathf.Atan2(lookVector.x, lookVector.y) * Mathf.Rad2Deg;
        if (lookVector != Vector2.zero)   rb.MoveRotation(Quaternion.Euler(0, angle, 0));
    }

    private void FixedUpdate()
    {

    }
}
