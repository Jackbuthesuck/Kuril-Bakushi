using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    public Magazine magazine;

    void Start()
    {
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
    }

    void Update()
    {
       this.transform.localScale = new Vector3 ((magazine.reloadTime / magazine.reloadDuration) - 1,1 ,1);
    }
    public void DoTheThing()
    {
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
        this.transform.localScale = new Vector3(0, 1, 1);
    }
}
