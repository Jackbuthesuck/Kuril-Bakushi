using UnityEngine;
using UnityEngine.InputSystem;

public class ShootShotgun : MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    private InputAction reload;
    public GameObject bullet;

    public int magazineMax;
    public int magazineNow;

    public float rpm;
    public float chamberDuration; // use rpm instead
    public float reloadDuration;

    public float chamberTime;
    public float reloadTime;

    public bool isReloading;
    public bool isChambering;
    public bool isChambered;

    public int pellet;

    public GameObject parent;
    private Quaternion yes;

    private void Awake()
    {
        playerControl = new PlayerInputAction();
    }

    private void OnEnable()
    {
        reload = playerControl.Player.Reload;
        attack = playerControl.Player.Attack;
        attack.Enable();
        reload.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
        reload.Disable();
    }

    void Start()
    {
        parent = GameObject.Find("Player");
        chamberDuration = (60 / rpm);
    }

    void Update()
    {
        if (isChambering)
        {
            if (magazineNow <= 0) isChambering = false;
            if (isChambered) isChambering = false;
            chamberTime -= Time.deltaTime;
            if (chamberTime <= 0)
            {
                magazineNow--;
                isChambered = true;
                isChambering = false;
            }
        }
        if (isReloading)
        {
            if (attack.WasPressedThisFrame()) { if (magazineNow < 0) isReloading = false; }
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                magazineNow = magazineMax;
                chamberTime = chamberDuration;
                isChambering = true;
                isReloading = false;
            }
        }
        else
        {
            if (attack.WasPressedThisFrame())
            {
                if (isChambered == true) Shoot();
                else
                {
                    if (isChambering) { }
                    else
                    {
                        chamberTime = chamberDuration;
                        isChambering = true;
                    }
                }
            }

            if (reload.WasPressedThisFrame())
            {
                reloadTime = reloadDuration;
                isReloading = true;
            }
        }
    }

    void FixedUpdate()
    {

    }

    private void Shoot()
    {
        for (pellet = bullet.GetComponent<Bullet>().pellet; pellet > 0; pellet--)
        {
            yes.eulerAngles = this.transform.eulerAngles + new Vector3 (0, Random.Range(1f,-1f), 0);
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
        }
        chamberTime = chamberDuration;
        isChambered = false;
        isChambering = true;
    }
}
