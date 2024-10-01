using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class ShootBoltTopLoad : MonoBehaviour
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
            if (attack.WasPressedThisFrame()) isReloading = false;
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                magazineNow++;
                reloadTime = reloadDuration;
            }
            if (magazineNow == magazineMax)
            {
                chamberTime = chamberDuration;
                isReloading = false;
                isChambering = true;
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
        yes.eulerAngles = this.transform.eulerAngles;
        GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
        instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
        chamberTime = chamberDuration;
        isChambered = false;
    }
}
