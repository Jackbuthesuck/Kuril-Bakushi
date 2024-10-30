using UnityEngine;

public enum StartingWeapon { option1, option2, option3, option4, option5 }
public class MasterVariableContainer : MonoBehaviour
{
    public StartingWeapon startingWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void weaponOption1()
    {
        startingWeapon = StartingWeapon.option1;
    }
    public void weaponOption2()
    {
        startingWeapon = StartingWeapon.option2;
    }
    public void weaponOption3()
    {
        startingWeapon = StartingWeapon.option3;
    }
    public void weaponOption4()
    {
        startingWeapon = StartingWeapon.option4;
    }
    public void weaponOption5()
    {
        startingWeapon = StartingWeapon.option5;
    }
}

