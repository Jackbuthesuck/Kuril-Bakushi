using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Ammohud ammoHud;
    public ReloadBar reloadBar;
    public ChamberBar chamberBar;
    public GameObject weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
