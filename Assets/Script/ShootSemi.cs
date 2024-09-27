using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class shootSemi: MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    private InputAction reload;
    public GameObject bullet;

    public int magazineMax;
    public int magazineNow;

    public float chamberDuration;
    public float reloadDuration;

    public float chamberTime;
    public float reloadTime;

    public bool isReloading;
    public bool isChamberd;

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
        if (isReloading == true)
        {
            if (attack.IsPressed())
                isReloading = false;
        }
        else
        {
            if (attack.IsPressed())
            {
                if (magazineNow > 0)
                {
                    if (isChamberd == true)
                    {
                        Shoot();
                    }
                    else Chamber();
                }
                else Reload();
            }
            if (reload.IsPressed())
            {
                if (isChamberd == false) Chamber();
                else Reload();
            }
        }
    }
    private void Shoot()
    {
        GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
        instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
        isChamberd = false;
        Chamber();
    }
    private void Chamber()
    {
        magazineNow -= 1;
        isChamberd = true;
    }
    private void Reload()
    {
        if (magazineNow < magazineMax)
        {
            isReloading = true;
            magazineNow += 1;
            Reload();
        }
        else isReloading = false;
    }
}
