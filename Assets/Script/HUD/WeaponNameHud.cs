using TMPro;
using UnityEngine;

public class WeaponNameHud : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject playerWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DoTheThing()
    {
        playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon;
        textMeshPro.text = string.Format("{0}", playerWeapon.name);
    }
}
