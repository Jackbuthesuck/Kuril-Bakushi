using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 400;
    public float armor = 1000;
    public float armorActual = 10000f;
    public int armorClass = 8;
    public int armorClassActual = 8;
    public float healthRegenTimer = 0f;
    public float healthRegenRate = 1f;
    public float healthRegenRateActual = 1f;
    public float armorRegenTimer = 0f;
    public float armorRegenRate = 5f;
    public float armorRegenRateActual = 5f;
    public float armorDamageMultiplier = 1;
    public float healthDamage = 0f;
    public float armorDamage = 0f;
    public float armorActualDamage = 0f;
    public string hit1 = "none";
    public string hit2 = "none";
    public float bleed = 0;
    public float fire = 0;
    public float gas = 0;
    public float toxic = 0;
    public float shock = 0;

    public HealthBar healthbar;
    public ArmorBar armorbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armorbar = GameObject.Find("Armor Bar").GetComponent<ArmorBar>();
        healthbar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bleeding, Damage overtime to health
        if (bleed > 0f)
        {
            healthRegenTimer = 2.5f;
            health -= 1 * Time.deltaTime;
            bleed -= 1 * Time.deltaTime;
        }
        //Flammed, Damage overtime to armor (Scale), Fragile armor
        if (fire > 0f)
        {
            armorActualDamage = Mathf.Clamp(armor - (1 * fire * Time.deltaTime), 0f, 100f);
            armorDamageMultiplier = 2f;
            fire -= 1 * Time.deltaTime;
        }
        else
        {
            armorDamageMultiplier = 1f;
        }
        //gassed, Less health regen
        if (gas > 0f)
        {
            healthRegenRateActual = healthRegenRate / 4;
            gas -= 1 * Time.deltaTime;
        }
        else
        {
            healthRegenRateActual = healthRegenRate;
        }
        //toxin, Less both regen
        if (toxic > 0f)
        {
            healthRegenRateActual = healthRegenRate / 2;
            armorRegenRateActual = armorRegenRate / 2;
            toxic -= 1 * Time.deltaTime;
        }
        else
        {
            healthRegenRateActual = healthRegenRate;
            armorRegenRateActual = armorRegenRate;
        }
        //Shocked, big damage overtime to armor
        if (shock > 0f)
        {
            armorActualDamage = Mathf.Clamp(armor - (4 * shock * Time.deltaTime), 0f, 100f);
            shock -= 1 * Time.deltaTime;
        }
        //Bullet type hit
        switch (hit1)
        {
            case "normal":
                healthRegenTimer = 5f;
                armorRegenTimer = 5f;
                hit1 = "none";
                break;
            case "bleed":
                armorRegenTimer = 5f;
                bleed += 5f;
                hit1 = "none";
                break;
            case "fire":
                healthRegenTimer = 2f;
                armorRegenTimer = 10f;
                fire += 5f;
                hit1 = "none";
                break;
            case "shock":
                shock += 5f;
                hit1 = "none";
                break;
            case "none":
                break;
        }
        //Enviroment type hit
        switch (hit2)
        {
            case "normal": //Fall damage
                healthRegenTimer = 5f;
                armorRegenTimer = 10f;
                hit2 = "none";
                break;
            case "fire":
                healthRegenTimer = 5f;
                armorRegenTimer = 10f;
                fire += 5f;
                hit2 = "none";
                break;
            case "gas":
                healthRegenTimer = 5f;
                gas += 5f;
                hit2 = "none";
                break;
            case "toxic":
                healthRegenTimer = 5f;
                armorRegenTimer = 1;
                gas += 5f;
                hit2 = "none";
                break;
            case "shock":
                shock += 5f;
                hit2 = "none";
                break;
            case "none":
                break;
        }
        //Armor Class
        armorClass = Mathf.Clamp(1 + Mathf.FloorToInt(armor / 200), 0, 5);
        armorClassActual = Mathf.Clamp(1 + Mathf.FloorToInt(armorActual / 200), 0, 5);

        //Damage
        health -= healthDamage - healthDamage * (armorClassActual * 0.16f);
        healthDamage = 0f;
        healthbar.DoTheThing();
        armor -= armorDamage * armorDamageMultiplier;
        armorActual -= armorDamage;
        armorDamage = 0f;
        armorbar.DoTheThing();

        //Regen
        healthRegenTimer -= Time.deltaTime;
        armorRegenTimer -= Time.deltaTime;

        if (healthRegenTimer <= 0f)
        {
            health += healthRegenRateActual * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, 400f);
            healthRegenTimer = Mathf.Clamp(healthRegenTimer, 0f, 10f);
        }

        if (armorRegenTimer <= 0f)
        {
            armor += armorRegenRateActual * Time.deltaTime;
            armor = Mathf.Clamp(armor, 0f, armorClass * 200f);
            armorRegenTimer = Mathf.Clamp(armorRegenTimer, 0f, 10f);
            armorActual += armorRegenRate * Time.deltaTime;
            armorActual = Mathf.Clamp(armorActual, 0f, armorClassActual * 200f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.gameObject.GetComponent<Bullet>().whoShotMe.name == this.gameObject.name) { }
            else
            {
                hit1 = "normal";
                healthDamage = other.gameObject.GetComponent<Bullet>().damage;
                armorDamage = other.gameObject.GetComponent<Bullet>().damage;
            }
            if (health <= 0)
            {
                
            }
        }
    }
}
