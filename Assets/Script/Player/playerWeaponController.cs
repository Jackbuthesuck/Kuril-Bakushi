using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerInputAction playerControl;
    private InputAction attack;
    private InputAction reload;

    public Ammohud ammoHud;
    public ReloadBar reloadBar;
    public ChamberBar chamberBar;
    public GameObject weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        getWeapon();
        ammoHud = GameObject.Find("Magazine Hud").GetComponent<Ammohud>();
        reloadBar = GameObject.Find("Reload Bar").GetComponent<ReloadBar>();
        chamberBar = GameObject.Find("Chamber Bar").GetComponent<ChamberBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reload.IsPressed()) weapon.GetComponent<WeaponController>().reload = true;
        else weapon.GetComponent<WeaponController>().reload = false;
        if (attack.IsPressed()) weapon.GetComponent<WeaponController>().attack = true;
        else weapon.GetComponent<WeaponController>().attack = false;
    }
    private void getWeapon()
    {
        for (int count = 0; count < this.transform.childCount; count++)
        {
            if (this.transform.GetChild(count).gameObject.tag == "Weapon")
            {
                weapon = this.transform.GetChild(count).gameObject;
                ammoHud.DoTheThing();
                chamberBar.DoTheThing();
                reloadBar.DoTheThing();
            }
        }
    }
}
