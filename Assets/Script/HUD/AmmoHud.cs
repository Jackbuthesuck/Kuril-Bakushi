using System;
using TMPro;
using UnityEngine;

public class Ammohud : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Magazine magazine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DoTheThing()
    {
        textMeshPro = this.GetComponent<TextMeshProUGUI>();
        magazine = GameObject.Find("Player").GetComponent<PlayerWeaponController>().weapon.GetComponent<Magazine>();
        textMeshPro.text = string.Format("{0} / {1}", magazine.now, magazine.max);
    }
}
