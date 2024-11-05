using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public GameObject bullet;

    public bool isTopLoad;
    public bool isFullAuto;

    public bool attack;
    public bool reload;

    public bool isReloading;
    public bool isChambering;
    public bool isChambered;
    public bool triggerReleased;

    public bool soundStart;
    public bool soundReloading = true;
    public bool soundFireing;
    public bool soundFireingEnd = false;


    public AudioManager audioManager;
    public Magazine magazine;
    public GameObject parent;
    private Quaternion yes;


    void Start()
    {
        parent = GameObject.Find("Player");
        magazine = this.GetComponent<Magazine>();
        magazine.chamberDuration = (60 / magazine.rpm);
        audioManager = this.GetComponent<AudioManager>();
        audioManager.PlayStart();
    }

    void Update()
    {
        if (isChambering)
        {
            Chambering();
        }
        if (isReloading)
        {
            if (isTopLoad) reloadingTopLoad();
            else reloading();
        }
        else
        {
            if (isFullAuto) fireFullAuto();
            else fireSemi();

        }
    }

    void FixedUpdate()
    {

    }

    private void Shoot()
    {
        soundFireingEnd = true;
        audioManager.PlayFire();

        for (int pellet = bullet.GetComponent<Bullet>().pellet; pellet > 0; pellet--)
        {
            yes.eulerAngles = this.transform.eulerAngles + new Vector3(0, Random.Range(bullet.GetComponent<Bullet>().moa * 0.0166667f, -bullet.GetComponent<Bullet>().moa * 0.0166667f), 0);
            GameObject instantiatedBullet = Instantiate(bullet, this.transform.position, yes);
            instantiatedBullet.GetComponent<Bullet>().whoShotMe = parent.gameObject;
        }
        magazine.chamberTime = magazine.chamberDuration;
        isChambered = false;
        isChambering = true;
        triggerReleased = false;
    }

    private void Chambering()
    {
        if (magazine.now <= 0)
        {
            isChambering = false;
        }
        if (isChambered)
        {
            isChambering = false;
            magazine.chamberTime = 0;
        }
        if (magazine.chamberTime <= 0)
        {
            magazine.now--;
            magazine.Change();
            isChambered = true;
            isChambering = false;
        }
        if (isChambering) magazine.chamberTime -= Time.deltaTime;
    }

    private void reloading()
    {
        if (soundReloading) audioManager.PlayReload();
        soundReloading = false;
        if (attack)
        {
            if (magazine.now < 0)
            {
                soundReloading = true;
                isReloading = false;
                magazine.ReloadInterrupted();
            }
        }
        magazine.reloadTime -= Time.deltaTime;
        if (magazine.reloadTime <= 0)
        {
            soundReloading = true;
            magazine.now = magazine.max;
            magazine.Change();
            if (!isChambered)
            {
                isChambering = true;
                magazine.chamberTime = magazine.chamberDuration;
            }
            isReloading = false;
        }
    }

    private void reloadingTopLoad()
    {
        if (soundReloading) audioManager.PlayReload();
        soundReloading = false;
        if (attack)
        {
            isReloading = false;
            magazine.ReloadInterrupted();
        }
        magazine.reloadTime -= Time.deltaTime;
        if (magazine.reloadTime <= 0)
        {
            soundReloading = true;
            magazine.now++;
            magazine.Change();
            magazine.reloadTime = magazine.reloadDuration;
        }
        if (magazine.now == magazine.max)
        {
            if (!isChambered)
            {
                isChambering = true;
                magazine.chamberTime = magazine.chamberDuration;
            }
            isReloading = false;
        }
    }
    private void fireFullAuto()
    {
        if (attack)
        {
            if (isChambered) Shoot();
            else
            {
                if (isChambering) { }
                else
                {
                    magazine.chamberTime = magazine.chamberDuration;
                    isChambering = true;
                }
            }
        }

        if (reload)
        {
            magazine.reloadTime = magazine.reloadDuration;
            magazine.Reload();
            isReloading = true;
        }
    }
    private void fireSemi()
    {
        if (attack && triggerReleased)
        {
            if (isChambered) Shoot();
            else
            {
                if (isChambering) { }
                else
                {
                    magazine.chamberTime = magazine.chamberDuration;
                    isChambering = true;
                }
            }
        }
        else
        {
            triggerReleased = true;
            if (soundFireingEnd) audioManager.PlayFireEnd();
            soundFireingEnd = false;
        }

        if (reload)
        {
            magazine.reloadTime = magazine.reloadDuration;
            magazine.Reload();
            isReloading = true;
        }
    }
}
