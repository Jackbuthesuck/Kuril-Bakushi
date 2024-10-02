using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int max;
    public int now;

    public float rpm;
    public float chamberDuration; // use rpm instead
    public float reloadDuration;

    public float chamberTime;
    public float reloadTime;

    public Ammohud ammoHud;
    public ReloadBar reloadBar;
    public ChamberBar chamberBar;
    void Start()
    {
        ammoHud = GameObject.Find("Magazine Hud").GetComponent<Ammohud>();
        reloadBar = GameObject.Find("Reload Bar").GetComponent<ReloadBar>();
        chamberBar = GameObject.Find("Chamber Bar").GetComponent <ChamberBar>();
    }

    void Update()
    {

    }
    public void Change()
    {
        ammoHud.DoTheThing();
        chamberBar.DoTheThing();
    }
    public void Reload()
    {
        reloadBar.DoTheThing();
    }
    public void ReloadInterrupted()
    {
        reloadBar.ReloadInterrupted();
    }
}
