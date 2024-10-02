using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getWeapon();
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
            }
        }
    }
}
