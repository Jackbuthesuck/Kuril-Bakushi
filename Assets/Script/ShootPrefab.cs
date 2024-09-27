using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class shootPrefab : MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    public GameObject bullet;
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

    }

    // Update is called once per frame
    void Update()
    {
        yes.eulerAngles = this.transform.eulerAngles;
    }
    void FixedUpdate()
    {
        if (attack.IsInProgress())
        {
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = gameObject;
        }
    }
}
