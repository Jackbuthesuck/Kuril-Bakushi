using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    public Magazine magazine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       this.transform.localScale = new Vector3 (magazine.reloadTime / magazine.reloadDuration ,1 ,1);
    }
    public void DoTheThing()
    {
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
        this.transform.localScale = new Vector3(0, 1, 1);
    }
    public void ReloadInterrupted()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

}
