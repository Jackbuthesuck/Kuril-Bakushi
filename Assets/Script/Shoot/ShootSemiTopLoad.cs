using UnityEngine;
using UnityEngine.InputSystem;

public class ShootSemiTopLoad : MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    private InputAction reload;
    public GameObject bullet;

    public bool isReloading;
    public bool isChambering;
    public bool isChambered;

    public Magazine magazine;
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
        magazine = this.GetComponent<Magazine>();
        magazine.chamberDuration = (60 / magazine.rpm);
    }

    void Update()
    {
        if (isChambering)
        {
            if (magazine.now <= 0)
            {
                isChambering = false;
            }
            if (isChambered)
            {
                isChambering = false;
                magazine.chamberTime = 0;
            }
            magazine.chamberTime -= Time.deltaTime;
            if (magazine.chamberTime <= 0)
            {
                magazine.now--;
                magazine.Change();
                isChambered = true;
                isChambering = false;
            }
        }
        if (isReloading)
        {
            if (attack.WasPressedThisFrame())
            {
                isReloading = false;
                magazine.ReloadInterrupted();
            }
            magazine.reloadTime -= Time.deltaTime;
            if (magazine.reloadTime <= 0)
            {
                magazine.now++;
                magazine.Change();
                magazine.reloadTime = magazine.reloadDuration;
            }
            if (magazine.now == magazine.max)
            {
                if (!isChambered)
                {
                    isChambering = true;
                    magazine.chamberTime = magazine.chamberDuration;
                }
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
                        magazine.chamberTime = magazine.chamberDuration;
                        isChambering = true;
                    }
                }
            }

            if (reload.WasPressedThisFrame())
            {
                magazine.reloadTime = magazine.reloadDuration;
                magazine.Reload();
                isReloading = true;
            }
        }
    }

    void FixedUpdate()
    {

    }

    private void Shoot()
    {
        for (int pellet = bullet.GetComponent<Bullet>().pellet; pellet > 0; pellet--)
        {
            yes.eulerAngles = this.transform.eulerAngles + new Vector3(0, Random.Range(bullet.GetComponent<Bullet>().spread, -bullet.GetComponent<Bullet>().spread), 0);
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
        }
        magazine.chamberTime = magazine.chamberDuration;
        isChambered = false;
        isChambering = true;
    }
}
