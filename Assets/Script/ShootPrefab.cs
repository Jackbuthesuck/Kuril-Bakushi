using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class shootPrefab : MonoBehaviour
{
    public PlayerInputAction playerControl;

    private InputAction attack;

    public GameObject bullet;

    public Transform origin;
    private Quaternion yes;
    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        attack = playerControl.Player.Attack;
        attack.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        yes.eulerAngles = origin.eulerAngles;
    }
    void FixedUpdate()
    {
        if (attack.IsInProgress())
        {
            Instantiate(bullet, this.transform, instantiateInWorldSpace:false);
        }
    }
}
