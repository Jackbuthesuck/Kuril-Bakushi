using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class shootShotgun: MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    public GameObject bullet;

    public int magazineMax;
    public int magazineNow;
    public int chamberNow;

    public float chamberDuration;
    public float reloadDuration;

    public bool isReloading;
    public bool isChamber;

    public GameObject parent;
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
        parent = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        yes.eulerAngles = this.transform.eulerAngles;
    }
    void FixedUpdate()
    {
        if (attack.IsPressed())
        {
            if (magazineNow > 0)
            {
                chamberNow -= 1;
                GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
                instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
                isChamber = true;
                Invoke(nameof(Chamber), chamberDuration);
            }
            else
            {
                isReloading = true;
                Invoke(nameof(Reload), reloadDuration);
            }
        }
    }

    private void Chamber()
    {
        chamberNow += 1;
        magazineNow -= 1;
        isChamber = false;
    }
    private void Reload()
    {
        if (magazineNow < magazineMax)
        {
            magazineNow += 1;
            isReloading = true;
            Invoke(nameof(Reload), reloadDuration);
        }
        else isReloading = false;
    }
}
