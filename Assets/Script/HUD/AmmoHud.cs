using TMPro;
using UnityEngine;

public class Ammohud : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Magazine magazine;

    void Start()
    {
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
        textMeshPro = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

    }
    public void DoTheThing()
    {
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
        textMeshPro.text = string.Format("{0} / {1}", magazine.now, magazine.max);
    }
}
