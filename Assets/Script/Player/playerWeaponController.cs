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
    public GameObject option1, option2, option3, option4, option5;
    public MasterVariableContainer masterVariableContainer;
    public StartingWeapon startingWeapon;

    private Quaternion yes;
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
        ammoHud = GameObject.Find("Magazine Hud").GetComponent<Ammohud>();
        reloadBar = GameObject.Find("Reload Bar").GetComponent<ReloadBar>();
        chamberBar = GameObject.Find("Chamber Bar").GetComponent<ChamberBar>();
        startingWeapon = GameObject.Find("Cool Variable Container").GetComponent<MasterVariableContainer>().startingWeapon;
        yes.eulerAngles = this.transform.eulerAngles;
        switch (startingWeapon)
        {
            case StartingWeapon.option1:
                Instantiate(option1, this.transform, worldPositionStays: false);
                break;
            case StartingWeapon.option2:
                Instantiate(option2, this.transform, worldPositionStays: false);
                break;
            case StartingWeapon.option3:
                Instantiate(option3, this.transform, worldPositionStays: false);
                break;
            case StartingWeapon.option4:
                Instantiate(option4, this.transform, worldPositionStays: false);
                break;
            case StartingWeapon.option5:
                Instantiate(option5, this.transform, worldPositionStays: false);
                break;
        }
        getWeapon();
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
